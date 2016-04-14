namespace m2lib_csharp.types
{
    /// <summary>
    ///     A three by three matrix.
    /// </summary>
    public struct C33Matrix
    {
        public readonly C3Vector[] Columns;

        public C33Matrix(C3Vector col0, C3Vector col1, C3Vector col2)
        {
            Columns = new C3Vector[3];
            Columns[0] = col0;
            Columns[1] = col1;
            Columns[2] = col2;
        }

        public override string ToString()
        {
            return $"({Columns[0]},{Columns[1]},{Columns[2]})";
        }
    }
}