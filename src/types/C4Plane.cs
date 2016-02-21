using System.IO;
using m2lib_csharp.interfaces;

namespace m2lib_csharp.types
{
    /// <summary>
    /// A 3D plane defined by four floats
    /// </summary>
    public class C4Plane : IMarshalable
    {
        public C3Vector Normal;
        public float Distance;

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

        public void Load(BinaryReader stream, int version = -1)
        {
            Normal.Load(stream);
            Distance = stream.ReadSingle();
        }

        public void Save(BinaryWriter stream, int version = -1)
        {
            Normal.Save(stream);
            stream.Write(Distance);
        }
    }
}