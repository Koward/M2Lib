using System.IO;
using m2lib_csharp.interfaces;
using m2lib_csharp.m2;

namespace m2lib_csharp.types
{
    /// <summary>
    /// A four floats quaternion.
    /// </summary>
    public class C4Quaternion : IMarshalable
    {
        public float X, Y, Z, W;

        public C4Quaternion(float p1, float p2, float p3, float p4)
        {
            X = p1;
            Y = p2;
            Z = p3;
            W = p4;
        }

        public C4Quaternion() : this(0,0,0,1)
        {
        }

        public void Load(BinaryReader stream, M2.Format version = M2.Format.Unknown)
        {
            X = stream.ReadSingle();
            Y = stream.ReadSingle();
            Z = stream.ReadSingle();
            W = stream.ReadSingle();
        }

        public void Save(BinaryWriter stream, M2.Format version = M2.Format.Unknown)
        {
            stream.Write(X);
            stream.Write(Y);
            stream.Write(Z);
            stream.Write(W);
        }
    }
}