namespace m2lib_csharp.types
{
    /// <summary>
    ///     A four by four matrix.
    /// </summary>
    public struct C44Matrix
    {
        public readonly C3Vector[] Columns;

        public C44Matrix(C3Vector col0, C3Vector col1, C3Vector col2, C3Vector col3)
        {
            Columns = new C3Vector[4];
            Columns[0] = col0;
            Columns[1] = col1;
            Columns[2] = col2;
            Columns[3] = col3;
        }

        public override string ToString()
        {
            return $"({Columns[0]},{Columns[1]},{Columns[2]},{Columns[3]})";
        }
    }
}