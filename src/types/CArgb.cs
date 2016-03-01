﻿using System.IO;
using m2lib_csharp.interfaces;
using m2lib_csharp.m2;

namespace m2lib_csharp.types
{
    /// <summary>
    /// A color given in values of red, green, blue and alpha.
    /// </summary>
    public class CArgb : IMarshalable
    {
        public byte R, G, B, A;

        public CArgb(byte p1 = 0, byte p2 = 0, byte p3 = 0, byte p4 = byte.MaxValue)
        {
            R = p1;
            G = p2;
            B = p3;
            A = p4;
        }

        public void Load(BinaryReader stream, M2.Format version = M2.Format.Unknown)
        {
            R = stream.ReadByte();
            G = stream.ReadByte();
            B = stream.ReadByte();
            A = stream.ReadByte();
        }

        public void Save(BinaryWriter stream, M2.Format version = M2.Format.Unknown)
        {
            stream.Write(R);
            stream.Write(G);
            stream.Write(B);
            stream.Write(A);
        }
    }
}