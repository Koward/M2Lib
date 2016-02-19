using System.Text;

namespace jm2lib.blizzard.wow.classic
{

	using Referencer = jm2lib.blizzard.common.interfaces.Referencer;
	using QuatF = jm2lib.blizzard.common.types.QuatF;
	using Vec3F = jm2lib.blizzard.common.types.Vec3F;
	using MarshalingStream = jm2lib.io.MarshalingStream;
	using UnmarshalingStream = jm2lib.io.UnmarshalingStream;

	public class Bone : Referencer
	{
		public int keyBoneID;
		public int flags;
		public short parentBone;
		public char submeshID;
		public AnimationBlock<Vec3F> translation;
		public AnimationBlock<QuatF> rotation;
		public AnimationBlock<Vec3F> scale;
		public Vec3F pivot;

		public Bone()
		{
			keyBoneID = -1;
			flags = 0;
			parentBone = -1;
			submeshID = (char)0;
			translation = new AnimationBlock<Vec3F>(typeof(Vec3F));
			rotation = new AnimationBlock<QuatF>(typeof(QuatF));
			scale = new AnimationBlock<Vec3F>(typeof(Vec3F));
			pivot = new Vec3F();
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void unmarshal(jm2lib.io.UnmarshalingStream in) throws java.io.IOException, ClassNotFoundException
		public virtual void unmarshal(UnmarshalingStream @in)
		{
			keyBoneID = @in.readInt();
			flags = @in.readInt();
			parentBone = @in.readShort();
			submeshID = @in.readChar();
			translation.unmarshal(@in);
			rotation.unmarshal(@in);
			scale.unmarshal(@in);
			pivot.unmarshal(@in);
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void marshal(jm2lib.io.MarshalingStream out) throws java.io.IOException
		public virtual void marshal(MarshalingStream @out)
		{
			@out.writeInt(keyBoneID);
			@out.writeInt(flags);
			@out.writeShort(parentBone);
			@out.writeShort(submeshID);
			translation.marshal(@out);
			rotation.marshal(@out);
			scale.marshal(@out);
			pivot.marshal(@out);
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void writeContent(jm2lib.io.MarshalingStream out) throws InstantiationException, IllegalAccessException, java.io.IOException
		public virtual void writeContent(MarshalingStream @out)
		{
				translation.writeContent(@out);
				rotation.writeContent(@out);
				scale.writeContent(@out);
		}

		public override string ToString()
		{
			StringBuilder builder = new StringBuilder();
//JAVA TO C# CONVERTER WARNING: The .NET Type.FullName property will not always yield results identical to the Java Class.getName method:
			builder.Append(this.GetType().FullName).Append(" {\n\tkeyBoneID: ").Append(keyBoneID).Append("\n\tflags: ").Append(flags).Append("\n\tparentBone: ").Append(parentBone).Append("\n\tsubmeshID: ").Append(((int) submeshID).ToString("x")).Append("\n\tunknown: ");
			builder.Append("\n\ttranslation: ").Append(translation).Append("\n\trotation: ").Append(rotation).Append("\n\tscale: ").Append(scale).Append("\n\tpivot: ").Append(pivot).Append("\n}");
			return builder.ToString();
		}

		public virtual jm2lib.blizzard.wow.burningcrusade.Bone upConvert()
		{
			jm2lib.blizzard.wow.burningcrusade.Bone output = new jm2lib.blizzard.wow.burningcrusade.Bone();
			output.keyBoneID = keyBoneID;
			output.flags = flags;
			output.parentBone = parentBone;
			output.submeshID = submeshID;
			output.translation = translation;
			output.rotation.interpolationType = rotation.interpolationType;
			output.rotation.globalSequence = rotation.globalSequence;
			output.rotation.ranges = rotation.ranges;
			output.rotation.timestamps = rotation.timestamps;
			for (int i = 0; i < rotation.values.Count; i++)
			{
				output.rotation.values.Add(rotation.values[i].toQuatS());
			}
			output.scale = scale;
			output.pivot = pivot;
			return output;
		}
	}

}