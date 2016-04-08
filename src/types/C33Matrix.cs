using System.IO;
using m2lib_csharp.interfaces;
using m2lib_csharp.m2;

namespace m2lib_csharp.types
{
    /// <summary>
    ///     A three by three matrix.
    /// </summary>
    public class C33Matrix : IMarshalable
    {
        public C33Matrix(C3Vector col0, C3Vector col1, C3Vector col2)
        {
            Columns = new C3Vector[3];
            Columns[0] = col0;
            Columns[1] = col1;
            Columns[2] = col2;
        }

        public C33Matrix() : this(new C3Vector(), new C3Vector(), new C3Vector())
        {
        }

        public C3Vector[] Columns { get; set; }

        public void Load(BinaryReader stream, M2.Format version)
        {
            foreach (var vec in Columns)
            {
                vec.Load(stream, version);
            }
        }

        public void Save(BinaryWriter stream, M2.Format version)
        {
            foreach (var vec in Columns)
            {
                vec.Save(stream, version);
            }
        }
    }
}