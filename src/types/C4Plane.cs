using System.IO;
using m2lib_csharp.interfaces;
using m2lib_csharp.m2;

namespace m2lib_csharp.types
{
    /// <summary>
    ///     A 3D plane defined by four floats
    /// </summary>
    public class C4Plane : IMarshalable
    {
        public float Distance;
        public C3Vector Normal;

        public C4Plane(C3Vector vec, float dist)
        {
            Normal = vec;
            Distance = dist;
        }

        public C4Plane()
        {
            Normal = new C3Vector();
            Distance = 0;
        }

        public void Load(BinaryReader stream, M2.Format version)
        {
            Normal.Load(stream, version);
            Distance = stream.ReadSingle();
        }

        public void Save(BinaryWriter stream, M2.Format version)
        {
            Normal.Save(stream, version);
            stream.Write(Distance);
        }
    }
}