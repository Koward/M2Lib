using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using m2lib_csharp.interfaces;
using m2lib_csharp.io;
using m2lib_csharp.m2;

namespace m2lib_csharp.types
{
    public class M2Array<T> : List<T>, IAnimated, IArrayRef where T : new()
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

            if (ReadingAnimFile != null)
                stream = ReadingAnimFile;

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
            if (typeof (IArrayRef).IsAssignableFrom(typeof (T)))
            {
                //Replace alias arrayrefs
                for (var i = 0; i < _n; i++)
                {
                    if (!_sequencesBackRef[i].IsAlias) continue;
                    var j = i;
                    while (_sequencesBackRef[j].IsAlias)
                        j = _sequencesBackRef[j].AliasNext;
                    this[i] = this[j];
                }
            }
            if (!typeof (IReferencer).IsAssignableFrom(typeof (T))) return;

            for (var i = 0; i < _n; i++)
            {
                if (typeof (IArrayRef).IsAssignableFrom(typeof (T)) &&
                    version >= M2.Format.LichKing &&
                    _sequencesBackRef[i].GetRealSequence(_sequencesBackRef).IsExtern)
                {
                    ((IArrayRef) this[i]).ReadingAnimFile = _sequencesBackRef[i].GetRealSequence(_sequencesBackRef).ReadingAnimFile;
                }
                //Do not laod content if alias
                if (!typeof (IArrayRef).IsAssignableFrom(typeof (T)) || 
                    !_sequencesBackRef[i].IsAlias)
                ((IReferencer) this[i]).LoadContent(stream, version);
            }
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

            var mainStream = stream;
            if (WritingAnimFile != null)
            {
                stream = WritingAnimFile;
            }

            _offset = (uint) stream.BaseStream.Position;
            for(var i = 0; i < Count; i++)
            {
                if (typeof (IAnimated).IsAssignableFrom(typeof (T)))
                {
                    ((IAnimated) this[i]).SetSequences(_sequencesBackRef);
                }
                stream.WriteGeneric(version, this[i]);
            }
            if (typeof (IReferencer).IsAssignableFrom(typeof (T)))
            {
                for (var i = 0; i < Count; i++)
                {
                    if (typeof (IArrayRef).IsAssignableFrom(typeof (T)) &&
                        version >= M2.Format.LichKing &&
                        _sequencesBackRef[i].GetRealSequence(_sequencesBackRef).IsExtern)
                    {
                        ((IArrayRef) this[i]).WritingAnimFile = _sequencesBackRef[i].GetRealSequence(_sequencesBackRef).WritingAnimFile;
                    }
                    //Do NOT write content if seq[i] is an alias. Only the real should write content.
                    if (!typeof (IArrayRef).IsAssignableFrom(typeof (T)) || 
                        !_sequencesBackRef[i].IsAlias)
                    ((IReferencer) this[i]).SaveContent(stream, version);
                }
            }
            RewriteHeader(mainStream, version);
        }

        public void SetSequences(IReadOnlyList<M2Sequence> sequences)
        {
            _sequencesBackRef = sequences;
        }

        public BinaryReader ReadingAnimFile { get; set; } // .anim files handling
        public BinaryWriter WritingAnimFile { get; set; }

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
    ///     Used to call non generic methods on generic M2Array.
    /// </summary>
    internal interface IArrayRef
    {
        BinaryReader ReadingAnimFile { get; set; }
        BinaryWriter WritingAnimFile { get; set; }
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