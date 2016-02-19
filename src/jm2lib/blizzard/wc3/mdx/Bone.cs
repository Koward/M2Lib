using System.Collections.Generic;
using System.Text;

namespace jm2lib.blizzard.wc3.mdx
{


	using Vec3F = jm2lib.blizzard.common.types.Vec3F;
	using Marshalable = jm2lib.io.Marshalable;
	using MarshalingStream = jm2lib.io.MarshalingStream;
	using UnmarshalingStream = jm2lib.io.UnmarshalingStream;

	public class Bone : Marshalable
	{
		internal Node node;
		internal int geosetID;
		internal int geosetAnimationID;

		public Bone()
		{
			node = new Node();
			geosetID = -1;
			geosetAnimationID = -1;
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void unmarshal(jm2lib.io.UnmarshalingStream in) throws java.io.IOException, ClassNotFoundException
		public virtual void unmarshal(UnmarshalingStream @in)
		{
			node.unmarshal(@in);
			geosetID = @in.readInt();
			geosetAnimationID = @in.readInt();
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void marshal(jm2lib.io.MarshalingStream out) throws java.io.IOException
		public virtual void marshal(MarshalingStream @out)
		{
			node.marshal(@out);
			@out.writeInt(geosetID);
			@out.writeInt(geosetAnimationID);
		}

		public virtual jm2lib.blizzard.wow.classic.Bone upConvert(List<Vec3F> pivots)
		{
			jm2lib.blizzard.wow.classic.Bone output = new jm2lib.blizzard.wow.classic.Bone();

			//TODO keyboneID
			int boneFlags = node.flags & (0x8 + 0x10 + 0x20 + 0x40);
			output.flags |= boneFlags;
			output.parentBone = (short) node.parentID;
			output.submeshID = (char) geosetID;
			output.translation = node.translation.upConvert();
			output.rotation = node.rotation.upConvert();
			output.scale = node.scale.upConvert();
			output.pivot = pivots[node.objectID];
			if (!(output.translation.Empty && output.rotation.Empty && output.scale.Empty))
			{
				output.flags |= 0x200;
			}
			return output;
		}

		public override string ToString()
		{
			StringBuilder builder = new StringBuilder();
//JAVA TO C# CONVERTER WARNING: The .NET Type.FullName property will not always yield results identical to the Java Class.getName method:
			builder.Append(this.GetType().FullName).Append(" {\n\tnode: ").Append(node).Append("\n\tgeosetID: ").Append(geosetID).Append("\n\tgeosetAnimationID: ").Append(geosetAnimationID).Append("\n}");
			return builder.ToString();
		}
	}

}