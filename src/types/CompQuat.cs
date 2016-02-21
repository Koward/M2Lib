using System.IO;
using m2lib_csharp.interfaces;

namespace m2lib_csharp.types
{
    /// <summary>
    /// A four shorts (compressed) quaternion.
    /// </summary>
    public class CompQuat : IMarshalable
    {
        public short X, Y, Z, W;

        public CompQuat(short p1 = short.MaxValue, short p2 = short.MaxValue, short p3 = short.MaxValue, short p4 = short.MinValue)
        {
            X = p1;
            Y = p2;
            Z = p3;
            W = p4;
        }

        public void Load(BinaryReader stream, int version = -1)
        {
            X = stream.ReadInt16();
            Y = stream.ReadInt16();
            Z = stream.ReadInt16();
            W = stream.ReadInt16();
        }

        public void Save(BinaryWriter stream, int version = -1)
        {
            stream.Write(X);
            stream.Write(Y);
            stream.Write(Z);
            stream.Write(W);
        }
    }
}