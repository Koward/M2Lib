using System.Text;

namespace jm2lib.blizzard.wow.cataclysm.skin
{

	using Marshalable = jm2lib.io.Marshalable;
	using MarshalingStream = jm2lib.io.MarshalingStream;
	using UnmarshalingStream = jm2lib.io.UnmarshalingStream;

	public class ShadowBatch : Marshalable
	{
		public sbyte flags;
		public sbyte flags2;
		public short unknown1;
		public short submeshID;
		public short textureID;
		public short colorID;
		public short transparencyID;

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void unmarshal(jm2lib.io.UnmarshalingStream in) throws java.io.IOException, ClassNotFoundException
		public virtual void unmarshal(UnmarshalingStream @in)
		{
			flags = @in.readByte();
			flags2 = @in.readByte();
			unknown1 = @in.readShort();
			submeshID = @in.readShort();
			textureID = @in.readShort();
			colorID = @in.readShort();
			transparencyID = @in.readShort();
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void marshal(jm2lib.io.MarshalingStream out) throws java.io.IOException
		public virtual void marshal(MarshalingStream @out)
		{
			@out.writeByte(flags);
			@out.writeByte(flags2);
			@out.writeShort(unknown1);
			@out.writeShort(submeshID);
			@out.writeShort(textureID);
			@out.writeShort(colorID);
			@out.writeShort(transparencyID);
		}

		/* (non-Javadoc)
		 * @see java.lang.Object#toString()
		 */
		public override string ToString()
		{
			StringBuilder builder = new StringBuilder();
//JAVA TO C# CONVERTER WARNING: The .NET Type.FullName property will not always yield results identical to the Java Class.getName method:
			builder.Append(this.GetType().FullName).Append(" {\n\tflags: ").Append(flags).Append("\n\tflags2: ").Append(flags2).Append("\n\tunknown1: ").Append(unknown1).Append("\n\tsubmeshID: ").Append(submeshID).Append("\n\ttextureID: ").Append(textureID).Append("\n\tcolorID: ").Append(colorID).Append("\n\ttransparencyID: ").Append(transparencyID).Append("\n}");
			return builder.ToString();
		}
	}

}