using System;
using System.Text;

namespace jm2lib.blizzard.wow.cataclysm.skin
{

	using Vec3F = jm2lib.blizzard.common.types.Vec3F;
	using Marshalable = jm2lib.io.Marshalable;
	using MarshalingStream = jm2lib.io.MarshalingStream;
	using UnmarshalingStream = jm2lib.io.UnmarshalingStream;

	public class Submesh : Marshalable
	{
		public short submeshID;
		public short level;
		public short startVertex;
		public short nVertices;
		public short startTriangle;
		public short nTriangles;
		public short nBones;
		public short startBones;
		public short boneInfluences;
		public short rootBone;
		public Vec3F centerMass;
		public Vec3F centerBoundingBox;
		public float radius;

		public Submesh()
		{
			submeshID = 0;
			level = 0;
			startVertex = 0;
			nVertices = 0;
			startTriangle = 0;
			nTriangles = 0;
			nBones = 0;
			startBones = 0;
			boneInfluences = 0;
			rootBone = 0;
			centerMass = new Vec3F();
			centerBoundingBox = new Vec3F();
			radius = 0;

		}
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void unmarshal(jm2lib.io.UnmarshalingStream in) throws java.io.IOException, ClassNotFoundException
		public virtual void unmarshal(UnmarshalingStream @in)
		{
			submeshID = @in.readShort();
			level = @in.readShort();
			startVertex = @in.readShort();
			nVertices = @in.readShort();
			startTriangle = @in.readShort();
			nTriangles = @in.readShort();
			nBones = @in.readShort();
			startBones = @in.readShort();
			boneInfluences = @in.readShort();
			rootBone = @in.readShort();
			centerMass.unmarshal(@in);
			centerBoundingBox.unmarshal(@in);
			radius = @in.readFloat();
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void marshal(jm2lib.io.MarshalingStream out) throws java.io.IOException
		public virtual void marshal(MarshalingStream @out)
		{
			if (level > 0)
			{
				System.err.format("WARNING : Submesh Level higher than 0. Hash : " + GetHashCode() + "\n" + "If you're using a client older than Mists of Pandaria, the model may be bugged. Untested.%n" + "See : http://modcraft.superparanoid.de/viewtopic.php?f=20&t=9389 \n");
			}
			@out.writeShort(submeshID);
			@out.writeShort(level);
			@out.writeShort(startVertex);
			@out.writeShort(nVertices);
			@out.writeShort(startTriangle);
			@out.writeShort(nTriangles);
			@out.writeShort(nBones);
			@out.writeShort(startBones);
			@out.writeShort(boneInfluences);
			@out.writeShort(rootBone);
			centerMass.marshal(@out);
			centerBoundingBox.marshal(@out);
			@out.writeFloat(radius);
		}

		public override string ToString()
		{
			StringBuilder builder = new StringBuilder();
//JAVA TO C# CONVERTER WARNING: The .NET Type.FullName property will not always yield results identical to the Java Class.getName method:
			builder.Append(this.GetType().FullName).Append(" {\n\tsubmeshID: ").Append(submeshID).Append("\n\tlevel: ").Append(level).Append("\n\tstartVertex: ").Append(startVertex).Append("\n\tnVertices: ").Append(nVertices).Append("\n\tstartTriangle: ").Append(startTriangle).Append("\n\tnTriangles: ").Append(nTriangles).Append("\n\tnBones: ").Append(nBones).Append("\n\tstartBones: ").Append(startBones).Append("\n\tboneInfluences: ").Append(boneInfluences).Append("\n\trootBone: ").Append(rootBone).Append("\n\tcenterMass: ").Append(centerMass).Append("\n\tcenterBoundingBox: ").Append(centerBoundingBox).Append("\n\tradius: ").Append(radius).Append("\n}");
			return builder.ToString();
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public jm2lib.blizzard.wow.burningcrusade.Submesh downConvert() throws Exception
		public virtual jm2lib.blizzard.wow.burningcrusade.Submesh downConvert()
		{
			jm2lib.blizzard.wow.burningcrusade.Submesh output = new jm2lib.blizzard.wow.burningcrusade.Submesh();
			if (level > 0)
			{
				throw new Exception("Submesh level > 0. Your model has too much triangles and can't be converted.\n" + "See : http://modcraft.superparanoid.de/viewtopic.php?f=7&t=8859 \n");
			}
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
			output.centerBoundingBox = centerBoundingBox;
			output.radius = radius;
			/// <summary>
			/// @author Mjollna </summary>
			if (output.boneInfluences == 0)
			{
				output.boneInfluences = 1;
			}
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