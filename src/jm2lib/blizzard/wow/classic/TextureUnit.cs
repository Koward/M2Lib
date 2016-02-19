using System.Text;

namespace jm2lib.blizzard.wow.classic
{

	using Marshalable = jm2lib.io.Marshalable;
	using MarshalingStream = jm2lib.io.MarshalingStream;
	using UnmarshalingStream = jm2lib.io.UnmarshalingStream;

	public class TextureUnit : Marshalable
	{
		public sbyte flags;
		public sbyte flags2;
		public char shaderID;
		public char submeshIndex;
		public char submeshIndex2;
		public short colorIndex = -1;
		public char renderFlags;
		public char texUnitNumber;
		public char opCount;
		public char texture;
		public char texUnitNumber2;
		public char transparency;
		public char textureAnim;

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void unmarshal(jm2lib.io.UnmarshalingStream in) throws java.io.IOException, ClassNotFoundException
		public virtual void unmarshal(UnmarshalingStream @in)
		{
			flags = @in.readByte();
			flags2 = @in.readByte();
			shaderID = @in.readChar();
			submeshIndex = @in.readChar();
			submeshIndex2 = @in.readChar();
			colorIndex = @in.readShort();
			renderFlags = @in.readChar();
			texUnitNumber = @in.readChar();
			opCount = @in.readChar();
			texture = @in.readChar();
			texUnitNumber2 = @in.readChar();
			transparency = @in.readChar();
			textureAnim = @in.readChar();
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void marshal(jm2lib.io.MarshalingStream out) throws java.io.IOException
		public virtual void marshal(MarshalingStream @out)
		{
			@out.writeByte(flags);
			@out.writeByte(flags2);
			@out.writeChar(shaderID);
			@out.writeChar(submeshIndex);
			@out.writeChar(submeshIndex2);
			@out.writeShort(colorIndex);
			@out.writeChar(renderFlags);
			@out.writeChar(texUnitNumber);
			@out.writeChar(opCount);
			@out.writeChar(texture);
			@out.writeChar(texUnitNumber2);
			@out.writeChar(transparency);
			@out.writeChar(textureAnim);
		}

		public override string ToString()
		{
			StringBuilder builder = new StringBuilder();
//JAVA TO C# CONVERTER WARNING: The .NET Type.FullName property will not always yield results identical to the Java Class.getName method:
			builder.Append(this.GetType().FullName).Append(" {\n\tflags: ").Append(flags).Append("\n\tflags2: ").Append(flags2).Append("\n\tshaderID: ").Append((int) shaderID).Append("\n\tsubmeshIndex: ").Append((int) submeshIndex).Append("\n\tsubmeshIndex2: ").Append((int) submeshIndex2).Append("\n\tcolorIndex: ").Append(colorIndex).Append("\n\trenderFlags: ").Append((int) renderFlags).Append("\n\ttexUnitNumber: ").Append((int) texUnitNumber).Append("\n\topCount: ").Append((int) opCount).Append("\n\ttexture: ").Append((int) texture).Append("\n\ttexUnitNumber2: ").Append((int) texUnitNumber2).Append("\n\ttransparency: ").Append((int) transparency).Append("\n\ttextureAnim: ").Append((int) textureAnim).Append("\n}");
			return builder.ToString();
		}
	}

}