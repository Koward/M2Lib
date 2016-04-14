using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using m2lib_csharp.interfaces;
using m2lib_csharp.io;

namespace m2lib_csharp.m2
{
    public class M2Array<T> : List<T>, IMarshalable where T : new()
    {
        private uint _n; // n&ofs are only used in loading. When writing the real number is used.
        private uint _offset;

        private IReadOnlyList<M2Sequence> _sequencesBackRef; // Only used to give the reference to contained IAnimated.
        private long _startOffset = -1; // Where the n&offset are located

        public void Load(BinaryReader stream, M2.Format version = M2.Format.Useless)
        {
            _n = stream.ReadUInt32();
            _offset = stream.ReadUInt32();
        }

        //TODO A bit of optimization would be nice.
        /// <summary>
        /// Load referenced content. Instances are created, then loaded, then each of their referenced content is loaded.
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="version"></param>
        public void LoadContent(BinaryReader stream, M2.Format version = M2.Format.Useless)
        {
            if (_n == 0) return;

            stream.BaseStream.Seek(_offset, SeekOrigin.Begin);
            for (var i = 0; i < _n; i++)
            {
                if (typeof (IAnimated).IsAssignableFrom(typeof (T)))
                {
                    Add(new T());
                    ((IAnimated)this[i]).SetSequences(_sequencesBackRef);
                    ((IMarshalable)this[i]).Load(stream, version);
                }
                else Add(stream.ReadGeneric<T>(version));
            }
            if (!typeof (IReferencer).IsAssignableFrom(typeof (T))) return;

            for (var i = 0; i < _n; i++)
                ((IReferencer) this[i]).LoadContent(stream, version);
        }

        public void Save(BinaryWriter stream, M2.Format version = M2.Format.Useless)
        {
            _startOffset = stream.BaseStream.Position;
            stream.Write(Count);
            stream.Write(_offset);
        }

        public void SaveContent(BinaryWriter stream, M2.Format version = M2.Format.Useless)
        {
            if (Count == 0) return;
            _offset = (uint) stream.BaseStream.Position;
            for(var i = 0; i < Count; i++)
            {
                if (typeof (IAnimated).IsAssignableFrom(typeof (T)))
                    ((IAnimated) this[i]).SetSequences(_sequencesBackRef);
                stream.WriteGeneric(version, this[i]);
            }
            if (typeof (IReferencer).IsAssignableFrom(typeof (T)))
            {
                for (var i = 0; i < Count; i++)
                    ((IReferencer) this[i]).SaveContent(stream, version);
            }
            RewriteHeader(stream, version);
        }

        public void PassSequences(IReadOnlyList<M2Sequence> sequences)
        {
            Debug.Assert(typeof(IAnimated).IsAssignableFrom(typeof(T)), "M2Array<"+typeof(T)+"> while T does not implement IAnimated");
            _sequencesBackRef = sequences;
        }

        private void RewriteHeader(BinaryWriter stream, M2.Format version)
        {
            Debug.Assert(_startOffset > -1, "M2Array not saved before saving referenced content.");
            var currentOffset = (uint) stream.BaseStream.Position;
            stream.BaseStream.Seek(_startOffset, SeekOrigin.Begin);
            Save(stream, version);
            stream.BaseStream.Seek(currentOffset, SeekOrigin.Begin);
        }

        public override string ToString()
        {
            var result = new StringBuilder();
            result.Append("[N: " + Count + "]");
            if (Count == 0) return result.ToString();
            result.Append("\r\n");
            for(var i = 0; i < Count; i++)
            {
                result.Append(this[i]);
                result.Append("\r\n");
            }
            result.Append("\r\n");
            return result.ToString();
        }
    }

    /// <summary>
    /// Allows special behaviors for specific M2Arrays.
    /// </summary>
    public static class M2ArrayExtensions
    {
        public static string ToNameString(this M2Array<byte> array)
        {
            return Encoding.UTF8.GetString(array.ToArray()).Trim('\0');
        }

        public static void SetString(this M2Array<byte> array, string str)
        {
            array.Clear();
            array.AddRange(Encoding.UTF8.GetBytes(str + "\0"));
        }
    }
}