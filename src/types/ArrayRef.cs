using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using m2lib_csharp.interfaces;
using m2lib_csharp.io;
using m2lib_csharp.m2;

namespace m2lib_csharp.types
{
    public class ArrayRef<T> : List<T>, IAnimated, IArrayRef where T : new()
    {
        private uint _n; // n&ofs are only used in loading. When writing the real number is used.
        private uint _offset;

        private IReadOnlyList<Sequence> _sequencesBackRef; // Only used to give the reference to contained IAnimated.
        private long _startOffset; // Where the n&offset are located

        public ArrayRef()
        {
            _startOffset = 0;
            _n = 0;
            _offset = 0;
        }

        /// <summary>
        ///     Build ArrayRef from string.
        /// </summary>
        /// <param name="str">String that will be converted in bytes.</param>
        public ArrayRef(string str) : this()
        {
            if (!typeof (byte).IsAssignableFrom(typeof (T))) throw new NotSupportedException();
            AddRange((IEnumerable<T>) (object) Encoding.UTF8.GetBytes(str + "\0"));
        }

        public void Load(BinaryReader stream, M2.Format version = M2.Format.Useless)
        {
            _n = stream.ReadUInt32();
            _offset = stream.ReadUInt32();
        }

        public void LoadContent(BinaryReader stream, M2.Format version = M2.Format.Useless)
        {
            if (_n == 0) return;

            if (ReadingAnimFile != null) stream = ReadingAnimFile;

            stream.BaseStream.Seek(_offset, SeekOrigin.Begin);
            for (var i = 0; i < _n; i++)
            {
                Add(stream.ReadGeneric<T>(version, _sequencesBackRef));
            }
            if (!typeof (IReferencer).IsAssignableFrom(typeof (T))) return;

            for (var i = 0; i < _n; i++)
            {
                if (typeof (ArrayRef<>).IsAssignableFrom(typeof (T)) &&
                    version >= M2.Format.LichKing &&
                    !_sequencesBackRef[i].Flags.HasFlag(Sequence.SequenceFlags.NoAnimFile))
                {
                    var animFileStream =
                        new FileStream(
                            _sequencesBackRef[i].GetRealSequence(_sequencesBackRef)
                                .GetAnimFilePath(((FileStream) stream.BaseStream).Name), FileMode.Open);
                    ((IArrayRef) this[i]).ReadingAnimFile = new BinaryReader(animFileStream);
                }
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
            if (WritingAnimFile != null) stream = WritingAnimFile;

            _offset = (uint) stream.BaseStream.Position;
            foreach (var item in this)
                stream.WriteGeneric(version, _sequencesBackRef, item);
            if (typeof (IReferencer).IsAssignableFrom(typeof (T)))
            {
                for (var i = 0; i < Count; i++)
                {
                    if (typeof (ArrayRef<>).IsAssignableFrom(typeof (T)) &&
                        version >= M2.Format.LichKing &&
                        !_sequencesBackRef[i].Flags.HasFlag(Sequence.SequenceFlags.NoAnimFile))
                    {
                        var animFileStream =
                            new FileStream(
                                _sequencesBackRef[i].GetRealSequence(_sequencesBackRef)
                                    .GetAnimFilePath(((FileStream) stream.BaseStream).Name), FileMode.Create);
                        ((IArrayRef) this[i]).WritingAnimFile = new BinaryWriter(animFileStream);
                    }
                    ((IReferencer) this[i]).SaveContent(stream, version);
                }
            }
            RewriteHeader(mainStream, version);
        }

        public void SetSequences(IReadOnlyList<Sequence> sequences)
        {
            _sequencesBackRef = sequences;
        }

        public BinaryReader ReadingAnimFile { get; set; } // .anim files handling
        public BinaryWriter WritingAnimFile { get; set; }

        private void RewriteHeader(BinaryWriter stream, M2.Format version)
        {
            var currentOffset = (uint) stream.BaseStream.Position;
            stream.BaseStream.Seek(_startOffset, SeekOrigin.Begin);
            Save(stream, version);
            stream.BaseStream.Seek(currentOffset, SeekOrigin.Begin);
        }

        public string ToNameString()
        {
            if (!typeof (byte).IsAssignableFrom(typeof (T)))
                throw new NotImplementedException("Cannot convert ArrayRef<" + typeof (T) + "> to " + typeof (string));
            return Encoding.UTF8.GetString((byte[]) (object) ToArray()).Trim('\0');
        }
    }

    /// <summary>
    ///     Used to call non generic methods on generic ArrayRef.
    /// </summary>
    internal interface IArrayRef
    {
        BinaryReader ReadingAnimFile { get; set; }
        BinaryWriter WritingAnimFile { get; set; }
    }
}