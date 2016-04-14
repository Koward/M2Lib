using System;
using System.IO;
using m2lib_csharp.interfaces;
using m2lib_csharp.m2;
using m2lib_csharp.types;

namespace m2lib_csharp.io
{
    /// <summary>
    ///     Extensions to BinaryReader and BinaryWriter to hide the generic type identification done during IO.
    /// </summary>
    public static class StreamExtensions
    {
        public static T ReadGeneric<T>(this BinaryReader stream, M2.Format version)
            where T : new()
        {
            T item;
            if (typeof (IMarshalable).IsAssignableFrom(typeof (T)))
            {
                item = new T();
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
            else if (typeof (C2Vector).IsAssignableFrom(typeof (T)))
                item = (T) (object) stream.ReadC2Vector();
            else if (typeof (C33Matrix).IsAssignableFrom(typeof (T)))
                item = (T) (object) stream.ReadC33Matrix();
            else if (typeof (C3Vector).IsAssignableFrom(typeof (T)))
                item = (T) (object) stream.ReadC3Vector();
            else if (typeof (C44Matrix).IsAssignableFrom(typeof (T)))
                item = (T) (object) stream.ReadC44Matrix();
            else if (typeof (C4Plane).IsAssignableFrom(typeof (T)))
                item = (T) (object) stream.ReadC4Plane();
            else if (typeof (C4Quaternion).IsAssignableFrom(typeof (T)))
                item = (T) (object) stream.ReadC4Quaternion();
            else if (typeof (C4Vector).IsAssignableFrom(typeof (T)))
                item = (T) (object) stream.ReadC4Vector();
            else if (typeof (C44Matrix).IsAssignableFrom(typeof (T)))
                item = (T) (object) stream.ReadC44Matrix();
            else if (typeof (C4Plane).IsAssignableFrom(typeof (T)))
                item = (T) (object) stream.ReadC4Plane();
            else if (typeof (C4Quaternion).IsAssignableFrom(typeof (T)))
                item = (T) (object) stream.ReadC4Quaternion();
            else if (typeof (C4Vector).IsAssignableFrom(typeof (T)))
                item = (T) (object) stream.ReadC4Vector();
            else if (typeof (CAaBox).IsAssignableFrom(typeof (T)))
                item = (T) (object) stream.ReadCAaBox();
            else if (typeof (CAaSphere).IsAssignableFrom(typeof (T)))
                item = (T) (object) stream.ReadCAaSphere();
            else if (typeof (CArgb).IsAssignableFrom(typeof (T)))
                item = (T) (object) stream.ReadCArgb();
            else if (typeof (CompQuat).IsAssignableFrom(typeof (T)))
                item = (T) (object) stream.ReadCompQuat();
            else if (typeof (CRange).IsAssignableFrom(typeof (T)))
                item = (T) (object) stream.ReadCRange();
            else
                throw new NotImplementedException(typeof (T) + "type is not supported and cannot be read.");
            return item;
        }


        public static void WriteGeneric<T>(this BinaryWriter stream, M2.Format version, T item) where T : new()
        {
            if (typeof (IMarshalable).IsAssignableFrom(typeof (T)))
                ((IMarshalable) item).Save(stream, version);
            else if (typeof (bool).IsAssignableFrom(typeof (T)))
                stream.Write((bool) (object) item);
            else if (typeof (byte).IsAssignableFrom(typeof (T)))
                stream.Write((byte) (object) item);
            else if (typeof (short).IsAssignableFrom(typeof (T)))
                stream.Write((short) (object) item);
            else if (typeof (int).IsAssignableFrom(typeof (T)))
                stream.Write((int) (object) item);
            else if (typeof (ushort).IsAssignableFrom(typeof (T)))
                stream.Write((ushort) (object) item);
            else if (typeof (uint).IsAssignableFrom(typeof (T)))
                stream.Write((uint) (object) item);
            else if (typeof (C2Vector).IsAssignableFrom(typeof (T)))
                stream.Write((C2Vector) (object) item);
            else if (typeof (C33Matrix).IsAssignableFrom(typeof (T)))
                stream.Write((C33Matrix) (object) item);
            else if (typeof (C3Vector).IsAssignableFrom(typeof (T)))
                stream.Write((C3Vector) (object) item);
            else if (typeof (C44Matrix).IsAssignableFrom(typeof (T)))
                stream.Write((C44Matrix) (object) item);
            else if (typeof (C4Plane).IsAssignableFrom(typeof (T)))
                stream.Write((C4Plane) (object) item);
            else if (typeof (C4Quaternion).IsAssignableFrom(typeof (T)))
                stream.Write((C4Quaternion) (object) item);
            else if (typeof (C4Vector).IsAssignableFrom(typeof (T)))
                stream.Write((C4Vector) (object) item);
            else if (typeof (CAaBox).IsAssignableFrom(typeof (T)))
                stream.Write((CAaBox) (object) item);
            else if (typeof (CAaSphere).IsAssignableFrom(typeof (T)))
                stream.Write((CAaSphere) (object) item);
            else if (typeof (CArgb).IsAssignableFrom(typeof (T)))
                stream.Write((CArgb) (object) item);
            else if (typeof (CompQuat).IsAssignableFrom(typeof (T)))
                stream.Write((CompQuat) (object) item);
            else if (typeof (CRange).IsAssignableFrom(typeof (T)))
                stream.Write((CRange) (object) item);
            else
                throw new NotImplementedException(typeof (T) + "type is not supported and cannot be written.");
        }

        //READING OF STRUCTS
        public static C2Vector ReadC2Vector(this BinaryReader stream)
        {
            return new C2Vector(stream.ReadSingle(), stream.ReadSingle());
        }

        public static C33Matrix ReadC33Matrix(this BinaryReader stream)
        {
            return new C33Matrix(stream.ReadC3Vector(), stream.ReadC3Vector(), stream.ReadC3Vector());
        }

        public static C3Vector ReadC3Vector(this BinaryReader stream)
        {
            return new C3Vector(stream.ReadSingle(), stream.ReadSingle(), stream.ReadSingle());
        }

        public static C44Matrix ReadC44Matrix(this BinaryReader stream)
        {
            return new C44Matrix(stream.ReadC3Vector(), stream.ReadC3Vector(), stream.ReadC3Vector(),
                stream.ReadC3Vector());
        }

        public static C4Plane ReadC4Plane(this BinaryReader stream)
        {
            return new C4Plane(stream.ReadC3Vector(), stream.ReadSingle());
        }

        public static C4Quaternion ReadC4Quaternion(this BinaryReader stream)
        {
            return new C4Quaternion(stream.ReadSingle(), stream.ReadSingle(), stream.ReadSingle(), stream.ReadSingle());
        }

        public static C4Vector ReadC4Vector(this BinaryReader stream)
        {
            return new C4Vector(stream.ReadSingle(), stream.ReadSingle(), stream.ReadSingle(), stream.ReadSingle());
        }

        public static CAaBox ReadCAaBox(this BinaryReader stream)
        {
            return new CAaBox(stream.ReadC3Vector(), stream.ReadC3Vector());
        }

        public static CAaSphere ReadCAaSphere(this BinaryReader stream)
        {
            return new CAaSphere(stream.ReadC3Vector(), stream.ReadSingle());
        }

        public static CArgb ReadCArgb(this BinaryReader stream)
        {
            return new CArgb(stream.ReadByte(), stream.ReadByte(), stream.ReadByte(), stream.ReadByte());
        }

        public static CompQuat ReadCompQuat(this BinaryReader stream)
        {
            return new CompQuat(stream.ReadInt16(), stream.ReadInt16(), stream.ReadInt16(), stream.ReadInt16());
        }

        public static CRange ReadCRange(this BinaryReader stream)
        {
            return new CRange(stream.ReadSingle(), stream.ReadSingle());
        }

        //WRITING OF STRUCTS
        public static void Write(this BinaryWriter stream, C2Vector item)
        {
            stream.Write(item.X);
            stream.Write(item.Y);
        }

        public static void Write(this BinaryWriter stream, C33Matrix item)
        {
            // ReSharper disable once ForCanBeConvertedToForeach
            for (var i = 0; i < item.Columns.Length; i++)
            {
                stream.Write(item.Columns[i]);
            }
        }

        public static void Write(this BinaryWriter stream, C3Vector item)
        {
            stream.Write(item.X);
            stream.Write(item.Y);
            stream.Write(item.Z);
        }

        public static void Write(this BinaryWriter stream, C44Matrix item)
        {
            // ReSharper disable once ForCanBeConvertedToForeach
            for (var i = 0; i < item.Columns.Length; i++)
            {
                stream.Write(item.Columns[i]);
            }
        }

        public static void Write(this BinaryWriter stream, C4Plane item)
        {
            stream.Write(item.Normal);
            stream.Write(item.Distance);
        }

        public static void Write(this BinaryWriter stream, C4Quaternion item)
        {
            stream.Write(item.X);
            stream.Write(item.Y);
            stream.Write(item.Z);
            stream.Write(item.W);
        }

        public static void Write(this BinaryWriter stream, C4Vector item)
        {
            stream.Write(item.W);
            stream.Write(item.X);
            stream.Write(item.Y);
            stream.Write(item.Z);
        }

        public static void Write(this BinaryWriter stream, CAaBox item)
        {
            stream.Write(item.Min);
            stream.Write(item.Max);
        }

        public static void Write(this BinaryWriter stream, CAaSphere item)
        {
            stream.Write(item.Position);
            stream.Write(item.Radius);
        }

        public static void Write(this BinaryWriter stream, CArgb item)
        {
            stream.Write(item.R);
            stream.Write(item.G);
            stream.Write(item.B);
            stream.Write(item.A);
        }

        public static void Write(this BinaryWriter stream, CompQuat item)
        {
            stream.Write(item.X);
            stream.Write(item.Y);
            stream.Write(item.Z);
            stream.Write(item.W);
        }

        public static void Write(this BinaryWriter stream, CRange item)
        {
            stream.Write(item.Min);
            stream.Write(item.Max);
        }
    }
}