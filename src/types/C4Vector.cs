using System.IO;
using m2lib_csharp.interfaces;
using m2lib_csharp.m2;

namespace m2lib_csharp.types
{
    /// <summary>
    ///     A four component float vector.
    /// </summary>
    public class C4Vector : IMarshalable
    {
        public float X, Y, Z, W;

        public C4Vector(float p1 = 0, float p2 = 0, float p3 = 0, float p4 = 0)
        {
            X = p1;
            Y = p2;
            Z = p3;
            W = p4;
        }

        public void Load(BinaryReader stream, M2.Format version)
        {
            X = stream.ReadSingle();
            Y = stream.ReadSingle();
            Z = stream.ReadSingle();
            W = stream.ReadSingle();
        }

        public void Save(BinaryWriter stream, M2.Format version)
        {
            stream.Write(X);
            stream.Write(Y);
            stream.Write(Z);
            stream.Write(W);
        }
    }
}