namespace M2Lib.types
{
    /// <summary>
    ///     A three component float vector.
    /// </summary>
    public struct C3Vector
    {
        public readonly float X, Y, Z;

        public C3Vector(float p1, float p2, float p3)
        {
            X = p1;
            Y = p2;
            Z = p3;
        }

        public override string ToString()
        {
            return $"({X},{Y},{Z})";
        }
    }
}