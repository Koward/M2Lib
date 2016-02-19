using System.Text;

namespace jm2lib.blizzard.wc3.mdx
{

	using jm2lib.blizzard.common.types;
	using Marshalable = jm2lib.io.Marshalable;
	using MarshalingStream = jm2lib.io.MarshalingStream;
	using UnmarshalingStream = jm2lib.io.UnmarshalingStream;

	public class Texture : Marshalable
	{
		public int replaceableID;

		public string fileName;
		public int flags;

		public Texture()
		{
			fileName = "Skin01.blp";
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void unmarshal(jm2lib.io.UnmarshalingStream in) throws java.io.IOException, ClassNotFoundException
		public virtual void unmarshal(UnmarshalingStream @in)
		{
			replaceableID = @in.readInt();
			fileName = @in.readString(260);
			flags = @in.readInt();
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void marshal(jm2lib.io.MarshalingStream out) throws java.io.IOException
		public virtual void marshal(MarshalingStream @out)
		{
			@out.writeInt(replaceableID);
			@out.writeString(fileName, 260);
			@out.writeInt(flags);
		}

		public virtual jm2lib.blizzard.wow.classic.Texture upConvert()
		{
			jm2lib.blizzard.wow.classic.Texture output = new jm2lib.blizzard.wow.classic.Texture();
			output.type = replaceableID;
			output.flags = flags;
			output.fileName = new ArrayRef<sbyte?>(fileName);
			return output;
		}

		public override string ToString()
		{
			StringBuilder builder = new StringBuilder();
//JAVA TO C# CONVERTER WARNING: The .NET Type.FullName property will not always yield results identical to the Java Class.getName method:
			builder.Append(this.GetType().FullName).Append(" {\n\treplaceableID: ").Append(replaceableID).Append("\n\tfileName: ").Append(fileName).Append("\n\tflags: ").Append(flags).Append("\n}");
			return builder.ToString();
		}
	}
}