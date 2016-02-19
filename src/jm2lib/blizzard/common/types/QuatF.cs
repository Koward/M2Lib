namespace jm2lib.blizzard.common.types
{

	using MarshalingStream = jm2lib.io.MarshalingStream;
	using UnmarshalingStream = jm2lib.io.UnmarshalingStream;

	/// <summary>
	/// Represents a 4 floats Quaternion. Used up to World of Warcraft 1.12.1.
	/// Please note that unlike the standard mathematical representation, the scalar part is placed at the end.
	/// @author Koward
	/// 
	/// </summary>
	public class QuatF : BlizzardVector
	{
		private float x, y, z, w;

		public QuatF(float x, float y, float z, float w)
		{
			this.x = x;
			this.y = y;
			this.z = z;
			this.w = w;
		}

		public QuatF() : this((float) 0,(float) 0,(float) 0,(float) 0)
		{
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void unmarshal(jm2lib.io.UnmarshalingStream in) throws java.io.IOException, ClassNotFoundException
		public virtual void unmarshal(UnmarshalingStream @in)
		{
			x = @in.readFloat();
			y = @in.readFloat();
			z = @in.readFloat();
			w = @in.readFloat();
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void marshal(jm2lib.io.MarshalingStream out) throws java.io.IOException
		public virtual void marshal(MarshalingStream @out)
		{
			@out.writeFloat(x);
			@out.writeFloat(y);
			@out.writeFloat(z);
			@out.writeFloat(w);
		}

		/// <summary>
		/// Converts to a 4 shorts representation.
		/// @return
		/// </summary>
		public virtual QuatS toQuatS()
		{
			return new QuatS(fts(x), fts(y), fts(z), fts(w));
		}

		/// <param name="value"> The float to convert. </param>
		/// <returns> a converted short value.
		/// @author schlumpf </returns>
		private short fts(float value)
		{
			return unchecked((short)(value > 0 ? value * 32767.0 - 32768 : value * 32767.0 + 32768));
		}

		public override string ToString()
		{
			return "(" + x + ", " + y + ", " + z + ", " + w + ")";
		}
	}

}