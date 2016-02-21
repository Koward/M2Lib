using System.Collections.Generic;
using System.IO;
using m2lib_csharp.interfaces;

namespace m2lib_csharp.types
{
    public class ArrayRef<T> : List<T>, IReferencer where T : new()
    {
        private long _startOffset;// Where the n&offset are located
        private uint _n; // n&ofs are only used in loading. When writing the real number is used.
        private uint _offset;

        public ArrayRef()
        {
            _startOffset = 0;
            _n = 0;
            _offset = 0;
        }

        public void Load(BinaryReader stream, int version = -1)
        {
            _n = stream.ReadUInt32();
            _offset = stream.ReadUInt32();
        }

        public void LoadContent(BinaryReader stream, int version = -1, BinaryReader[] animFiles = null)
        {
            if (_n == 0) return;
            long currentOfs = stream.BaseStream.Position;
            stream.BaseStream.Seek(_offset, SeekOrigin.Begin);
            for (int i = 0; i < _n; i++)
            {
                if (typeof(IMarshalable).IsAssignableFrom(typeof(T)))
                {
                    Add(new T());
                    ((IMarshalable) this[i]).Load(stream, version);
                }
                else if (typeof (bool).IsAssignableFrom(typeof (T)))
                {
                    Add((T) (object) stream.ReadBoolean());
                }
                else if (typeof (byte).IsAssignableFrom(typeof (T)))
                {
                    Add((T) (object) stream.ReadByte());
                }
                else if (typeof (short).IsAssignableFrom(typeof (T)))
                {
                    Add((T) (object) stream.ReadInt16());
                }
                else if (typeof (int).IsAssignableFrom(typeof (T)))
                {
                    Add((T) (object) stream.ReadInt32());
                }
                else if (typeof (ushort).IsAssignableFrom(typeof (T)))
                {
                    Add((T) (object) stream.ReadUInt16());
                }
                else if (typeof (uint).IsAssignableFrom(typeof (T)))
                {
                    Add((T) (object) stream.ReadUInt32());
                }
                else
                {
                    throw new System.NotImplementedException(typeof(T) + "type is not supported and cannot be read.");
                }
            }
            if (typeof(IReferencer).IsAssignableFrom(typeof(T)))
            {
                for (int i = 0; i < _n; i++)
                {
                    if (typeof(ArrayRef<>).IsAssignableFrom(typeof(T)) && animFiles != null)
                        ((IReferencer) this[i]).LoadContent(animFiles[i], version, animFiles);
                    else
                        ((IReferencer) this[i]).LoadContent(stream, version, animFiles);
                }
            }
            stream.BaseStream.Seek(currentOfs, SeekOrigin.Begin);
        }

        public void Save(BinaryWriter stream, int version = -1)
        {
            _startOffset = stream.BaseStream.Position;
            stream.Write(Count);
            stream.Write(_offset);
        }

        public void SaveContent(BinaryWriter stream, int version = -1, BinaryWriter[] animFiles = null)
        {
            if (Count == 0) return;
            // Rewrites offset
            _offset = (uint) stream.BaseStream.Position;
            stream.BaseStream.Seek(_startOffset, SeekOrigin.Begin);
            Save(stream, version);

            stream.BaseStream.Seek(_offset, SeekOrigin.Begin);
            for (var i = 0; i < Count; i++)
            {
                if (typeof(IMarshalable).IsAssignableFrom(typeof(T)))
                {
                    ((IMarshalable) this[i]).Save(stream, version);
                }
                else if (typeof (bool).IsAssignableFrom(typeof (T)))
                {
                    stream.Write((bool) (object) this[i]);
                }
                else if (typeof (byte).IsAssignableFrom(typeof (T)))
                {
                    stream.Write((byte) (object) this[i]);
                }
                else if (typeof (short).IsAssignableFrom(typeof (T)))
                {
                    stream.Write((short)(object) this[i]);
                }
                else if (typeof (int).IsAssignableFrom(typeof (T)))
                {
                    stream.Write((int)(object) this[i]);
                }
                else if (typeof (ushort).IsAssignableFrom(typeof (T)))
                {
                    stream.Write((ushort)(object) this[i]);
                }
                else if (typeof (uint).IsAssignableFrom(typeof (T)))
                {
                    stream.Write((uint)(object) this[i]);
                }
                else
                {
                    throw new System.NotImplementedException(typeof(T) + "type is not supported and cannot be written.");
                }
            }
            if (typeof (IReferencer).IsAssignableFrom(typeof (T))) 
            {
                for (int i = 0; i < _n; i++)
                {
                    if (typeof(ArrayRef<>).IsAssignableFrom(typeof(T)) && animFiles != null)
                        ((IReferencer) this[i]).SaveContent(animFiles[i], version, animFiles);
                    else
                        ((IReferencer) this[i]).SaveContent(stream, version, animFiles);
                }
            }
        }
    }
}