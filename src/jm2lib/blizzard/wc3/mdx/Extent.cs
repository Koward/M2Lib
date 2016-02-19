using System.Text;

namespace jm2lib.blizzard.wc3.mdx
{

	using Vec3F = jm2lib.blizzard.common.types.Vec3F;
	using Marshalable = jm2lib.io.Marshalable;
	using MarshalingStream = jm2lib.io.MarshalingStream;
	using UnmarshalingStream = jm2lib.io.UnmarshalingStream;

	public class Extent : Marshalable
	{
		internal float boundsRadius;
		internal Vec3F minimum;
		internal Vec3F maximum;

		public Extent()
		{
			boundsRadius = 0.0F;
			minimum = new Vec3F();
			maximum = new Vec3F();
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void unmarshal(jm2lib.io.UnmarshalingStream in) throws java.io.IOException, ClassNotFoundException
		public virtual void unmarshal(UnmarshalingStream @in)
		{
			boundsRadius = @in.readFloat();
			minimum.unmarshal(@in);
			maximum.unmarshal(@in);
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void marshal(jm2lib.io.MarshalingStream out) throws java.io.IOException
		public virtual void marshal(MarshalingStream @out)
		{
			@out.writeFloat(boundsRadius);
			minimum.marshal(@out);
			maximum.marshal(@out);
		}

		public override string ToString()
		{
			StringBuilder builder = new StringBuilder();
			builder.Append(" {\n\tboundsRadius: ").Append(boundsRadius).Append("\n\tminimum: ").Append(minimum).Append("\n\tmaximum: ").Append(maximum).Append("\n}");
			return builder.ToString();
		}

	}

}