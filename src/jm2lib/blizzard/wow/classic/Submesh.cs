using System.Text;

namespace jm2lib.blizzard.wow.classic
{

	using Vec3F = jm2lib.blizzard.common.types.Vec3F;
	using Marshalable = jm2lib.io.Marshalable;
	using MarshalingStream = jm2lib.io.MarshalingStream;
	using UnmarshalingStream = jm2lib.io.UnmarshalingStream;

	public class Submesh : Marshalable
	{
		public int submeshID;
		public short startVertex;
		public short nVertices;
		public short startTriangle;
		public short nTriangles;
		public short nBones;
		public short startBones;
		public short boneInfluences;
		public short rootBone;
		public Vec3F centerMass;

		public Submesh()
		{
			submeshID = 0;
			startVertex = 0;
			nVertices = 0;
			startTriangle = 0;
			nTriangles = 0;
			nBones = 0;
			startBones = 0;
			boneInfluences = 0;
			rootBone = 0;
			centerMass = new Vec3F();
		}
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void unmarshal(jm2lib.io.UnmarshalingStream in) throws java.io.IOException, ClassNotFoundException
		public virtual void unmarshal(UnmarshalingStream @in)
		{
			submeshID = @in.readInt();
			startVertex = @in.readShort();
			nVertices = @in.readShort();
			startTriangle = @in.readShort();
			nTriangles = @in.readShort();
			nBones = @in.readShort();
			startBones = @in.readShort();
			boneInfluences = @in.readShort();
			rootBone = @in.readShort();
			centerMass.unmarshal(@in);
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void marshal(jm2lib.io.MarshalingStream out) throws java.io.IOException
		public virtual void marshal(MarshalingStream @out)
		{
			@out.writeInt(submeshID);
			@out.writeShort(startVertex);
			@out.writeShort(nVertices);
			@out.writeShort(startTriangle);
			@out.writeShort(nTriangles);
			@out.writeShort(nBones);
			@out.writeShort(startBones);
			@out.writeShort(boneInfluences);
			@out.writeShort(rootBone);
			centerMass.marshal(@out);
		}
		public override string ToString()
		{
			StringBuilder builder = new StringBuilder();
			builder.Append("Submesh [submeshID=");
			builder.Append(submeshID);
			builder.Append(", startVertex=");
			builder.Append(startVertex);
			builder.Append(", nVertices=");
			builder.Append(nVertices);
			builder.Append(", startTriangle=");
			builder.Append(startTriangle);
			builder.Append(", nTriangles=");
			builder.Append(nTriangles);
			builder.Append(", nBones=");
			builder.Append(nBones);
			builder.Append(", startBones=");
			builder.Append(startBones);
			builder.Append(", boneInfluences=");
			builder.Append(boneInfluences);
			builder.Append(", rootBone=");
			builder.Append(rootBone);
			builder.Append(", centerMass=");
			builder.Append(centerMass);
			return builder.ToString();
		}
		public virtual jm2lib.blizzard.wow.burningcrusade.Submesh upConvert()
		{
			jm2lib.blizzard.wow.burningcrusade.Submesh output = new jm2lib.blizzard.wow.burningcrusade.Submesh();
			output.submeshID = submeshID;
			output.startVertex = startVertex;
			output.nVertices = nVertices;
			output.startTriangle = startTriangle;
			output.nTriangles = nTriangles;
			output.nBones = nBones;
			output.startBones = startBones;
			output.boneInfluences = boneInfluences;
			output.rootBone = rootBone;
			output.centerMass = centerMass;
			return output;
		}

		/*
		@Override
		public String toString() {
			StringBuilder result = new StringBuilder();
			String NEW_LINE = System.getProperty("line.separator");
			result.append("submeshID : "+ NEW_LINE);
			return result.toString();
		}
		*/
	}

}