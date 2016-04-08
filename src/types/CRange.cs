using System.IO;
using m2lib_csharp.interfaces;
using m2lib_csharp.m2;

namespace m2lib_csharp.types
{
    /// <summary>
    ///     A one dimensional range defined by the bounds.
    /// </summary>
    public class CRange : IMarshalable
    {
        public float Min, Max;

        public CRange(float p1 = 0, float p2 = 0)
        {
            Min = p1;
            Max = p2;
        }

        public void Load(BinaryReader stream, M2.Format version)
        {
            Min = stream.ReadSingle();
            Max = stream.ReadSingle();
        }

        public void Save(BinaryWriter stream, M2.Format version)
        {
            stream.Write(Min);
            stream.Write(Max);
        }
    }
}