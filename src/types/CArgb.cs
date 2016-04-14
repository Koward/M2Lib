using System.IO;
using m2lib_csharp.interfaces;
using m2lib_csharp.m2;

namespace m2lib_csharp.types
{
    /// <summary>
    ///     A color given in values of red, green, blue and alpha.
    /// </summary>
    public class CArgb : IMarshalable
    {
        public byte R, G, B, A;

        public CArgb(byte p1, byte p2, byte p3, byte p4)
        {
            R = p1;
            G = p2;
            B = p3;
            A = p4;
        }

        public void Load(BinaryReader stream, M2.Format version)
        {
            R = stream.ReadByte();
            G = stream.ReadByte();
            B = stream.ReadByte();
            A = stream.ReadByte();
        }

        public void Save(BinaryWriter stream, M2.Format version)
        {
            stream.Write(R);
            stream.Write(G);
            stream.Write(B);
            stream.Write(A);
        }
    }
}