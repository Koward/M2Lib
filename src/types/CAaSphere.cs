using System.IO;
using m2lib_csharp.interfaces;
using m2lib_csharp.m2;

namespace m2lib_csharp.types
{
    /// <summary>
    /// An axis aligned sphere described by position and radius. 
    /// </summary>
    public class CAaSphere : IMarshalable
    {
        public C3Vector Position;
        public float Radius;

        public CAaSphere(C3Vector pos, float rad)
        {
            Position = pos;
            Radius = rad;
        }

        public CAaSphere()
        {
            Position = new C3Vector();
            Radius = 0;
        }

        public void Load(BinaryReader stream, M2.Format version = M2.Format.Unknown)
        {
            Position.Load(stream);
            Radius = stream.ReadSingle();
        }

        public void Save(BinaryWriter stream, M2.Format version = M2.Format.Unknown)
        {
            Position.Save(stream);
            stream.Write(Radius);
        }
    }
}