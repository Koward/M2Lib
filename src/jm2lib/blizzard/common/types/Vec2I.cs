namespace jm2lib.blizzard.common.types
{

	using MarshalingStream = jm2lib.io.MarshalingStream;
	using UnmarshalingStream = jm2lib.io.UnmarshalingStream;

	public class Vec2I : BlizzardVector
	{
		private int x, y;

		/// <returns> the x </returns>
		public virtual int X
		{
			get
			{
				return x;
			}
			set
			{
				this.x = value;
			}
		}
		/// <returns> the y </returns>
		public virtual int Y
		{
			get
			{
				return y;
			}
			set
			{
				this.y = value;
			}
		}
		public Vec2I() : this(0,0)
		{
		}
		public Vec2I(int x, int y)
		{
			this.x = x;
			this.y = y;
		}
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void unmarshal(jm2lib.io.UnmarshalingStream in) throws java.io.IOException, ClassNotFoundException
		public virtual void unmarshal(UnmarshalingStream @in)
		{
			x = @in.readInt();
			y = @in.readInt();
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void marshal(jm2lib.io.MarshalingStream out) throws java.io.IOException
		public virtual void marshal(MarshalingStream @out)
		{
			@out.writeInt(x);
			@out.writeInt(y);
		}

		public override string ToString()
		{
			return "(" + x + "," + y + ")";
		}

	}

}