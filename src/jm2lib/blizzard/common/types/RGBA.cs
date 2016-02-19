namespace jm2lib.blizzard.common.types
{

	using MarshalingStream = jm2lib.io.MarshalingStream;
	using UnmarshalingStream = jm2lib.io.UnmarshalingStream;

	/// <summary>
	/// Represents a 4 bytes color.
	/// @author Koward
	/// 
	/// </summary>
	public class RGBA : BlizzardVector
	{
		private Vec3B rgb;
		private sbyte alpha;

		public RGBA(sbyte red, sbyte green, sbyte blue, sbyte alpha)
		{
			this.rgb = new Vec3B(red, green, blue);
			this.alpha = alpha;
		}

		public RGBA() : this((sbyte) 0,(sbyte) 0,(sbyte) 0,(sbyte) 0)
		{
		}

		/// <summary>
		/// Creates a structure from a 3 floats RGB and 1 short A. </summary>
		/// <param name="rgb"> </param>
		/// <param name="alpha"> </param>
		public RGBA(Vec3F rgb, short alpha) : this((sbyte) rgb.Z,(sbyte) rgb.Y,(sbyte) rgb.X, (sbyte) 0)
		{
			this.alpha = unchecked((sbyte)(alpha / 128));
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void unmarshal(jm2lib.io.UnmarshalingStream in) throws java.io.IOException, ClassNotFoundException
		public virtual void unmarshal(UnmarshalingStream @in)
		{
			rgb.unmarshal(@in);
			alpha = @in.readByte();
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void marshal(jm2lib.io.MarshalingStream out) throws java.io.IOException
		public virtual void marshal(MarshalingStream @out)
		{
			rgb.marshal(@out);
			@out.writeByte(alpha);
		}

		public override string ToString()
		{
			return "(" + rgb + ", " + alpha + ")";
		}
	}

}