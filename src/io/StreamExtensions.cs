using System;
using System.Collections.Generic;
using System.IO;
using m2lib_csharp.interfaces;
using m2lib_csharp.m2;

namespace m2lib_csharp.io
{
    /// <summary>
    /// Extensions to BinaryReader and BinaryWriter to hide the generic type identification done during IO.
    /// </summary>
    public static class StreamExtensions
    {
        public static T ReadGeneric<T>(this BinaryReader stream, M2.Format version, IReadOnlyList<Sequence> sequences) where T : new()
        {
            T item;
            if (typeof(IMarshalable).IsAssignableFrom(typeof(T)))
            {
                item = new T();
                if (typeof(IAnimated).IsAssignableFrom(typeof(T))) ((IAnimated) item).SetSequences(sequences);
                ((IMarshalable) item).Load(stream, version);
            }
            else if (typeof (bool).IsAssignableFrom(typeof (T)))
                item = (T) (object) stream.ReadBoolean();
            else if (typeof (byte).IsAssignableFrom(typeof (T)))
                item = (T) (object) stream.ReadByte();
            else if (typeof (short).IsAssignableFrom(typeof (T)))
                item = (T) (object) stream.ReadInt16();
            else if (typeof (int).IsAssignableFrom(typeof (T)))
                item = (T) (object) stream.ReadInt32();
            else if (typeof (ushort).IsAssignableFrom(typeof (T)))
                item = (T) (object) stream.ReadUInt16();
            else if (typeof (uint).IsAssignableFrom(typeof (T)))
                item = (T) (object) stream.ReadUInt32();
            else
                throw new NotImplementedException(typeof(T) + "type is not supported and cannot be read.");
            return item;
        }


        public static void WriteGeneric<T>(this BinaryWriter stream, M2.Format version,
            IReadOnlyList<Sequence> sequences, T item) where T : new()
        {
                if (typeof(IMarshalable).IsAssignableFrom(typeof(T)))
                {
                    if (typeof(IAnimated).IsAssignableFrom(typeof(T))) ((IAnimated) item).SetSequences(sequences);
                    ((IMarshalable) item).Save(stream, version);
                }
                else if (typeof (bool).IsAssignableFrom(typeof (T)))
                    stream.Write((bool) (object) item);
                else if (typeof (byte).IsAssignableFrom(typeof (T)))
                    stream.Write((byte) (object) item);
                else if (typeof (short).IsAssignableFrom(typeof (T)))
                    stream.Write((short)(object) item);
                else if (typeof (int).IsAssignableFrom(typeof (T)))
                    stream.Write((int)(object) item);
                else if (typeof (ushort).IsAssignableFrom(typeof (T)))
                    stream.Write((ushort)(object) item);
                else if (typeof (uint).IsAssignableFrom(typeof (T)))
                    stream.Write((uint)(object) item);
                else
                    throw new NotImplementedException(typeof(T) + "type is not supported and cannot be written.");
        }
    }
}
