using System.IO;
using m2lib_csharp.interfaces;
using m2lib_csharp.m2;

namespace m2lib_csharp.types
{
    /// <summary>
    /// A four by four matrix.
    /// </summary>
    public class C44Matrix : IMarshalable
    {
        public C3Vector[] Columns { get; set; }

        public C44Matrix(C3Vector col0, C3Vector col1, C3Vector col2, C3Vector col3)
        {
            Columns = new C3Vector[4];
            Columns[0] = col0;
            Columns[1] = col1;
            Columns[2] = col2;
            Columns[3] = col3;
        }

        public C44Matrix() : this(new C3Vector(), new C3Vector(), new C3Vector(), new C3Vector())
        {
        }

        public void Load(BinaryReader stream, M2.Format version = M2.Format.Unknown)
        {
            foreach (C3Vector vec in Columns)
            {
                vec.Load(stream);
            }
        }

        public void Save(BinaryWriter stream, M2.Format version = M2.Format.Unknown)
        {
            foreach (C3Vector vec in Columns)
            {
                vec.Save(stream);
            }
        }
    }
}