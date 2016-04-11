using System.IO;
using m2lib_csharp.interfaces;
using m2lib_csharp.m2;

namespace m2lib_csharp.types
{
    /// <summary>
    ///     A three component float vector.
    /// </summary>
    public class C3Vector : IMarshalable
    {
        public float X, Y, Z;

        public C3Vector(float p1, float p2, float p3)
        {
            X = p1;
            Y = p2;
            Z = p3;
        }

        public C3Vector() : this(0, 0, 0)
        {
        }

        public void Load(BinaryReader stream, M2.Format version)
        {
            X = stream.ReadSingle();
            Y = stream.ReadSingle();
            Z = stream.ReadSingle();
        }

        public void Save(BinaryWriter stream, M2.Format version)
        {
            stream.Write(X);
            stream.Write(Y);
            stream.Write(Z);
        }

        public override string ToString()
        {
            return $"({X},{Y},{Z})";
        }
    }
}