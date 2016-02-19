using System.Text;

namespace jm2lib.blizzard.wc3.mdx
{

	using Marshalable = jm2lib.io.Marshalable;
	using MarshalingStream = jm2lib.io.MarshalingStream;
	using UnmarshalingStream = jm2lib.io.UnmarshalingStream;

	public class Model : Marshalable
	{
		public string name;
		public string animationFileName;
		public Extent extent;
		public int blendTime;

		public Model()
		{
			name = "NullModel";
			animationFileName = "";
			extent = new Extent();
			blendTime = 150;
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void unmarshal(jm2lib.io.UnmarshalingStream in) throws java.io.IOException, ClassNotFoundException
		public virtual void unmarshal(UnmarshalingStream @in)
		{
			name = @in.readString(80);
			animationFileName = @in.readString(260);
			extent.unmarshal(@in);
			blendTime = @in.readInt();
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void marshal(jm2lib.io.MarshalingStream out) throws java.io.IOException
		public virtual void marshal(MarshalingStream @out)
		{
			@out.writeString(name, 80);
			@out.writeString(animationFileName, 260);
			extent.marshal(@out);
			@out.writeInt(blendTime);
		}
		public override string ToString()
		{
			StringBuilder builder = new StringBuilder();
//JAVA TO C# CONVERTER WARNING: The .NET Type.FullName property will not always yield results identical to the Java Class.getName method:
			builder.Append(this.GetType().FullName).Append(" {\n\tname: ").Append(name).Append("\n\tanimationFileName: ").Append(animationFileName).Append("\n\textent: ").Append(extent).Append("\n\tblendTime: ").Append(blendTime).Append("\n}");
			return builder.ToString();
		}
	}
}