namespace m2lib_csharp.types
{
    /// <summary>
    ///     A two component float vector.
    /// </summary>
    public struct C2Vector
    {
        public readonly float X, Y;

        public C2Vector(float p1, float p2)
        {
            X = p1;
            Y = p2;
        }

        public override string ToString()
        {
            return $"({X},{Y})";
        }
    }
}