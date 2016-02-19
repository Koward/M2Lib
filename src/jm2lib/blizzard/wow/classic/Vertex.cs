using System.Text;

namespace jm2lib.blizzard.wow.classic
{

	using Vec2F = jm2lib.blizzard.common.types.Vec2F;
	using Vec3F = jm2lib.blizzard.common.types.Vec3F;
	using Marshalable = jm2lib.io.Marshalable;
	using MarshalingStream = jm2lib.io.MarshalingStream;
	using UnmarshalingStream = jm2lib.io.UnmarshalingStream;

	public class Vertex : Marshalable
	{
		public Vec3F pos;
		public short[] boneWeights; //byte
		public short[] boneIndices; //byte
		public Vec3F normal;
		public Vec2F[] texCoords;

		public Vertex()
		{
			pos = new Vec3F();
			boneWeights = new short[4];
			boneIndices = new short[4];
			normal = new Vec3F();
			texCoords = new Vec2F[2];
			for (sbyte i = 0; i < 2; i++)
			{
				texCoords[i] = new Vec2F();
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void unmarshal(jm2lib.io.UnmarshalingStream in) throws java.io.IOException, ClassNotFoundException
		public virtual void unmarshal(UnmarshalingStream @in)
		{
			pos.unmarshal(@in);
			for (sbyte i = 0; i < 4; i++)
			{
				boneWeights[i] = @in.readByte();
			}
			for (sbyte i = 0; i < 4; i++)
			{
				boneIndices[i] = @in.readByte();
			}
			normal.unmarshal(@in);
			for (sbyte i = 0; i < 2; i++)
			{
				texCoords[i].unmarshal(@in);
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void marshal(jm2lib.io.MarshalingStream out) throws java.io.IOException
		public virtual void marshal(MarshalingStream @out)
		{
			pos.marshal(@out);
			for (sbyte i = 0; i < 4; i++)
			{
				@out.writeByte(boneWeights[i]);
			}
			for (sbyte i = 0; i < 4; i++)
			{
				@out.writeByte(boneIndices[i]);
			}
			normal.marshal(@out);
			for (sbyte i = 0; i < 2; i++)
			{
				texCoords[i].marshal(@out);
			}
		}

		public override string ToString()
		{
			StringBuilder result = new StringBuilder();
			string NEW_LINE = System.getProperty("line.separator");
			result.Append("pos : " + pos + NEW_LINE);
			result.Append("boneWeights : " + "(" + boneWeights[0] + "," + boneWeights[1] + "," + boneWeights[2] + "," + boneWeights[3] + ")" + NEW_LINE);
			result.Append("boneIndices : " + "(" + boneIndices[0] + "," + boneIndices[1] + "," + boneIndices[2] + "," + boneIndices[3] + ")" + NEW_LINE);
			result.Append("Normal : " + normal + NEW_LINE);
			result.Append("texCoords : " + "(" + texCoords[0] + "," + texCoords[1] + ")" + NEW_LINE);
			return result.ToString();
		}
	}

}