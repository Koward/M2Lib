namespace jm2lib.blizzard.common.types
{

	using MarshalingStream = jm2lib.io.MarshalingStream;
	using UnmarshalingStream = jm2lib.io.UnmarshalingStream;

	public class Vec2F : BlizzardVector
	{
		private float x, y;

		/// <returns> the x </returns>
		public virtual float X
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
		public virtual float Y
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


		public Vec2F(float x, float y)
		{
			this.x = x;
			this.y = y;
		}
		public Vec2F() : this(0.0F, 0.0F)
		{
		}
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void unmarshal(jm2lib.io.UnmarshalingStream in) throws java.io.IOException, ClassNotFoundException
		public virtual void unmarshal(UnmarshalingStream @in)
		{
			x = @in.readFloat();
			y = @in.readFloat();
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void marshal(jm2lib.io.MarshalingStream out) throws java.io.IOException
		public virtual void marshal(MarshalingStream @out)
		{
			@out.writeFloat(x);
			@out.writeFloat(y);
		}

		public override string ToString()
		{
			return "(" + x + "," + y + ")";
		}

	}

}