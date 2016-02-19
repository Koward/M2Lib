namespace jm2lib.blizzard.common.types
{

	using MarshalingStream = jm2lib.io.MarshalingStream;
	using UnmarshalingStream = jm2lib.io.UnmarshalingStream;

	public class Vec3B : BlizzardVector
	{
		private sbyte x, y, z;

		public Vec3B(sbyte x, sbyte y, sbyte z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}

		public Vec3B() : this((sbyte) 0,(sbyte) 0,(sbyte) 0)
		{
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void unmarshal(jm2lib.io.UnmarshalingStream in) throws java.io.IOException, ClassNotFoundException
		public virtual void unmarshal(UnmarshalingStream @in)
		{
			x = @in.readByte();
			y = @in.readByte();
			z = @in.readByte();
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void marshal(jm2lib.io.MarshalingStream out) throws java.io.IOException
		public virtual void marshal(MarshalingStream @out)
		{
			@out.writeByte(x);
			@out.writeByte(y);
			@out.writeByte(z);
		}

		public override string ToString()
		{
			return "(" + x + "," + y + "," + z + ")";
		}

	}

}