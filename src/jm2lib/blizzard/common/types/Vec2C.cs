namespace jm2lib.blizzard.common.types
{

	using MarshalingStream = jm2lib.io.MarshalingStream;
	using UnmarshalingStream = jm2lib.io.UnmarshalingStream;

	public class Vec2C : BlizzardVector
	{
		private char x, y;

		/// <returns> the x </returns>
		public virtual char X
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
		public virtual char Y
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
		public Vec2C() : this((char) 0, (char) 0)
		{
		}
		public Vec2C(char x, char y)
		{
			this.x = x;
			this.y = y;
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void unmarshal(jm2lib.io.UnmarshalingStream in) throws java.io.IOException, ClassNotFoundException
		public virtual void unmarshal(UnmarshalingStream @in)
		{
			x = @in.readChar();
			y = @in.readChar();
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void marshal(jm2lib.io.MarshalingStream out) throws java.io.IOException
		public virtual void marshal(MarshalingStream @out)
		{
			@out.writeChar(x);
			@out.writeChar(y);
		}

		public override string ToString()
		{
			return "(" + (int) x + "," + (int) y + ")";
		}

	}

}