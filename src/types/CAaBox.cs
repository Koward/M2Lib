﻿using System.IO;
using m2lib_csharp.interfaces;
using m2lib_csharp.m2;

namespace m2lib_csharp.types
{
    /// <summary>
    /// An axis aligned box described by the minimum and maximum point.
    /// </summary>
    public class CAaBox : IMarshalable
    {
        public C3Vector Min, Max;

        public CAaBox(C3Vector vec1, C3Vector vec2)
        {
            Min = vec1;
            Max = vec2;
        }

        public CAaBox() : this(new C3Vector(), new C3Vector())
        {
        }

        public void Load(BinaryReader stream, M2.Format version = M2.Format.Unknown)
        {
            Min.Load(stream);
            Max.Load(stream);
        }

        public void Save(BinaryWriter stream, M2.Format version = M2.Format.Unknown)
        {
            Min.Save(stream);
            Max.Save(stream);
        }
    }
}