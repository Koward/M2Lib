using System.IO;
using m2lib_csharp.interfaces;
using m2lib_csharp.m2;

namespace m2lib_csharp.types
{
    /// <summary>
    /// A four shorts (compressed) quaternion.
    /// </summary>
    public class CompQuat : IMarshalable
    {
        public short X, Y, Z, W;

        public CompQuat(short p1, short p2, short p3, short p4)
        {
            X = p1;
            Y = p2;
            Z = p3;
            W = p4;
        }

        public CompQuat() : this(short.MaxValue, short.MaxValue, short.MaxValue, -1)
        {
        }

        public void Load(BinaryReader stream, M2.Format version)
        {
            X = stream.ReadInt16();
            Y = stream.ReadInt16();
            Z = stream.ReadInt16();
            W = stream.ReadInt16();
        }

        public void Save(BinaryWriter stream, M2.Format version)
        {
            stream.Write(X);
            stream.Write(Y);
            stream.Write(Z);
            stream.Write(W);
        }

        public static explicit operator C4Quaternion(CompQuat comp)
        {
            return new C4Quaternion(ShortToFloat(comp.X), ShortToFloat(comp.Y), ShortToFloat(comp.Z), ShortToFloat(comp.W));
        }

        /// <summary>
        /// Decompress a short in a float.
        /// </summary>
        /// <param name="value">The short to convert.</param>
        /// <returns>A converted float value.</returns>
        private static float ShortToFloat(short value)
        {
            if (value == -1) return 1;
            return (float)((value > 0 ? value - 32767 : value + 32767) / 32767.0);
        }
    }
}