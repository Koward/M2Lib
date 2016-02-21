using System.IO;
using m2lib_csharp.interfaces;

namespace m2lib_csharp.types
{
    /// <summary>
    /// A one dimensional range defined by the bounds.
    /// </summary>
    public class CRange : IMarshalable
    {
        public float Min, Max;

        public CRange(float p1 = 0, float p2 = 0)
        {
            Min = p1;
            Max = p2;
        }

        public void Load(BinaryReader stream, int version = -1)
        {
            Min = stream.ReadSingle();
            Max = stream.ReadSingle();
        }

        public void Save(BinaryWriter stream, int version = -1)
        {
            stream.Write(Min);
            stream.Write(Max);
        }
    }
}