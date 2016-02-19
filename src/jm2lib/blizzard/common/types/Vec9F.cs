namespace jm2lib.blizzard.common.types
{

	using MarshalingStream = jm2lib.io.MarshalingStream;
	using UnmarshalingStream = jm2lib.io.UnmarshalingStream;

	public class Vec9F : BlizzardVector
	{
		private Vec3F x, y, z;

		public Vec9F()
		{
			x = new Vec3F();
			y = new Vec3F();
			z = new Vec3F();
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void unmarshal(jm2lib.io.UnmarshalingStream in) throws java.io.IOException, ClassNotFoundException
		public virtual void unmarshal(UnmarshalingStream @in)
		{
			x.unmarshal(@in);
			y.unmarshal(@in);
			z.unmarshal(@in);
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void marshal(jm2lib.io.MarshalingStream out) throws java.io.IOException
		public virtual void marshal(MarshalingStream @out)
		{
			x.marshal(@out);
			y.marshal(@out);
			z.marshal(@out);
		}

		public override string ToString()
		{
			return "(" + x + "," + y + "," + z + ")";
		}

	}

}