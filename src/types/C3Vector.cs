using System.IO;
using m2lib_csharp.interfaces;

namespace m2lib_csharp.types
{
    /// <summary>
    /// A three component float vector.
    /// </summary>
    public class C3Vector : IMarshalable
    {
        public float X, Y, Z;

        public C3Vector(float p1 = 0, float p2 = 0, float p3 = 0)
        {
            X = p1;
            Y = p2;
            Z = p3;
        }

        public void Load(BinaryReader stream, int version = -1)
        {
            X = stream.ReadSingle();
            Y = stream.ReadSingle();
            Z = stream.ReadSingle();
        }

        public void Save(BinaryWriter stream, int version = -1)
        {
            stream.Write(X);
            stream.Write(Y);
            stream.Write(Z);
        }
    }
}