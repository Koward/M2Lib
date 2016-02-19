using System.Text;

namespace jm2lib.blizzard.wow.lichking
{

	using LERandomAccessFile = com.mindprod.ledatastream.LERandomAccessFile;

	using jm2lib.blizzard.common.types;
	using QuatS = jm2lib.blizzard.common.types.QuatS;
	using Vec3F = jm2lib.blizzard.common.types.Vec3F;
	using AnimFilesHandler = jm2lib.blizzard.wow.common.AnimFilesHandler;
	using MarshalingStream = jm2lib.io.MarshalingStream;
	using UnmarshalingStream = jm2lib.io.UnmarshalingStream;

	public class Bone : AnimFilesHandler
	{
		public int keyBoneID;
		public int flags;
		public short parentBone;
		public char submeshID;
		public char[] unknown;
		public AnimationBlock<Vec3F> translation;
		public AnimationBlock<QuatS> rotation;
		public AnimationBlock<Vec3F> scale;
		public Vec3F pivot;

		public Bone()
		{
			keyBoneID = -1;
			flags = 0;
			parentBone = -1;
			submeshID = (char)0;
			unknown = new char[2];
			translation = new AnimationBlock<Vec3F>(typeof(Vec3F));
			rotation = new AnimationBlock<QuatS>(typeof(QuatS), 1);
			scale = new AnimationBlock<Vec3F>(typeof(Vec3F), 1);
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
			for (sbyte i = 0; i < 2; i++)
			{
				unknown[i] = @in.readChar();
			}
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
			for (sbyte i = 0; i < unknown.Length; i++)
			{
				@out.writeShort(unknown[i]);
			}
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

		public virtual LERandomAccessFile[] AnimFiles
		{
			set
			{
				translation.AnimFiles = value;
				rotation.AnimFiles = value;
				scale.AnimFiles = value;
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public jm2lib.blizzard.wow.burningcrusade.Bone downConvert(jm2lib.blizzard.common.types.ArrayRef<jm2lib.blizzard.wow.classic.Animation> animations) throws Exception
		public virtual jm2lib.blizzard.wow.burningcrusade.Bone downConvert(ArrayRef<jm2lib.blizzard.wow.classic.Animation> animations)
		{
			jm2lib.blizzard.wow.burningcrusade.Bone output = new jm2lib.blizzard.wow.burningcrusade.Bone();
			output.keyBoneID = keyBoneID;
			output.flags = flags;
			output.parentBone = parentBone;
			output.submeshID = submeshID;
			output.unknown = unknown;
			output.translation = translation.downConvert(animations);
			output.rotation = rotation.downConvert(animations);
			output.scale = scale.downConvert(animations);
			output.pivot = pivot;
			return output;
		}

		public override string ToString()
		{
			StringBuilder builder = new StringBuilder();
//JAVA TO C# CONVERTER WARNING: The .NET Type.FullName property will not always yield results identical to the Java Class.getName method:
			builder.Append(this.GetType().FullName).Append(" {\n\tkeyBoneID: ").Append(keyBoneID).Append("\n\tflags: ").Append(int.toBinaryString(flags)).Append("\n\tparentBone: ").Append(parentBone).Append("\n\tsubmeshID: ").Append(((int) submeshID).ToString("x")).Append("\n\tunknown: ");
			builder.Append("[" + ((int) unknown[0]) + " " + ((int) unknown[1]) + "]");
			builder.Append("\n\ttranslation: ").Append(translation).Append("\n\trotation: ").Append(rotation).Append("\n\tscale: ").Append(scale).Append("\n\tpivot: ").Append(pivot).Append("\n}");
			return builder.ToString();
		}

	}

}