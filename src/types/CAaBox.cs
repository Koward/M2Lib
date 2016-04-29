namespace M2Lib.types
{
    /// <summary>
    ///     An axis aligned box described by the minimum and maximum point.
    /// </summary>
    public struct CAaBox
    {
        public readonly C3Vector Min, Max;

        public CAaBox(C3Vector vec1, C3Vector vec2)
        {
            Min = vec1;
            Max = vec2;
        }

        public override string ToString()
        {
            return $"{Min}->{Max}";
        }
    }
}