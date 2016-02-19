namespace jm2lib.blizzard.common.types
{

	using MarshalingStream = jm2lib.io.MarshalingStream;
	using UnmarshalingStream = jm2lib.io.UnmarshalingStream;

	/// <summary>
	/// Represents a 4 bytes color.
	/// @author Koward
	/// 
	/// </summary>
	public class BGRA : BlizzardVector
	{
		private Vec3B bgr;
		private sbyte alpha;

		public BGRA(sbyte blue, sbyte green, sbyte red, sbyte alpha)
		{
			this.bgr = new Vec3B(blue, green, red);
			this.alpha = alpha;
		}

		public BGRA() : this((sbyte) 0,(sbyte) 0,(sbyte) 0,(sbyte) 0)
		{
		}

		/// <summary>
		/// Creates a BGRA structure from a 3 floats RGB and 1 short A. </summary>
		/// <param name="rgb"> </param>
		/// <param name="alpha"> </param>
		public BGRA(Vec3F rgb, short alpha) : this((sbyte) rgb.Z,(sbyte) rgb.Y,(sbyte) rgb.X, (sbyte) 0)
		{
			this.alpha = unchecked((sbyte)(alpha / 128));
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void unmarshal(jm2lib.io.UnmarshalingStream in) throws java.io.IOException, ClassNotFoundException
		public virtual void unmarshal(UnmarshalingStream @in)
		{
			bgr.unmarshal(@in);
			alpha = @in.readByte();
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void marshal(jm2lib.io.MarshalingStream out) throws java.io.IOException
		public virtual void marshal(MarshalingStream @out)
		{
			bgr.marshal(@out);
			@out.writeByte(alpha);
		}

		public override string ToString()
		{
			return "(" + bgr + ", " + alpha + ")";
		}
	}

}