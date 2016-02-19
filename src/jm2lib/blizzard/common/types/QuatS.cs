namespace jm2lib.blizzard.common.types
{

	using MarshalingStream = jm2lib.io.MarshalingStream;
	using UnmarshalingStream = jm2lib.io.UnmarshalingStream;

	/// <summary>
	/// Represents a 4 shorts Quaternion.
	/// Please note that unlike the standard mathematical representation, the scalar part is placed at the end.
	/// @author Koward
	/// 
	/// </summary>
	public class QuatS : BlizzardVector
	{
		private short x, y, z, w;

		public QuatS(short x, short y, short z, short w)
		{
			this.x = x;
			this.y = y;
			this.z = z;
			this.w = w;
		}

		public QuatS() : this((short) 0,(short) 0,(short) 0,(short) 0)
		{
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void unmarshal(jm2lib.io.UnmarshalingStream in) throws java.io.IOException, ClassNotFoundException
		public virtual void unmarshal(UnmarshalingStream @in)
		{
			x = @in.readShort();
			y = @in.readShort();
			z = @in.readShort();
			w = @in.readShort();
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void marshal(jm2lib.io.MarshalingStream out) throws java.io.IOException
		public virtual void marshal(MarshalingStream @out)
		{
			@out.writeShort(x);
			@out.writeShort(y);
			@out.writeShort(z);
			@out.writeShort(w);
		}

		public virtual QuatF toQuatF()
		{
			return new QuatF(stf(x), stf(y), stf(z), stf(w));
		}

		/// <summary>
		/// In WoW 2.0+ Blizzard are now storing rotation data in 16bit values instead of 32bit.
		/// The conversion BC => Classic is done with this function. </summary>
		/// <param name="value"> The short to convert. </param>
		/// <returns> a converted float value.
		/// @author schlumpf </returns>
		private float stf(short value)
		{
			if (value == -1)
			{
				return 1;
			}
			return (float)((value > 0 ? value - 32767 : value + 32767) / 32767.0);
		}

		public override string ToString()
		{
			return "(" + x + ", " + y + ", " + z + ", " + w + ")";
		}
	}

}