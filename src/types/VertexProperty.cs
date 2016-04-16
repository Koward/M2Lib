namespace m2lib_csharp.types
{
    public struct VertexProperty
    {
        public readonly byte[] Properties;

        public VertexProperty(byte p1, byte p2, byte p3, byte p4)
        {
            Properties = new byte[4];
            Properties[0] = p1;
            Properties[1] = p2;
            Properties[2] = p3;
            Properties[3] = p4;
        }

        public override string ToString()
        {
            return $"[{Properties[0]} {Properties[1]} {Properties[2]} {Properties[3]}]";
        }
    }
}