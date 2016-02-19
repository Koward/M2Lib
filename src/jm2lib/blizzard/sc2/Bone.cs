using System.Collections.Generic;
using System.Text;

namespace jm2lib.blizzard.sc2
{


	using jm2lib.blizzard.common.types;
	using QuatF = jm2lib.blizzard.common.types.QuatF;
	using QuatS = jm2lib.blizzard.common.types.QuatS;
	using Vec3F = jm2lib.blizzard.common.types.Vec3F;
	using jm2lib.blizzard.wow.lichking;
	using MarshalingStream = jm2lib.io.MarshalingStream;
	using UnmarshalingStream = jm2lib.io.UnmarshalingStream;

	public class Bone : Indexer
	{
		internal int unknown0;
		internal Reference<sbyte?> name;
		internal int flags;
		internal short parent;
		internal char unknown1;
		internal AnimationReference<Vec3F> translation = new AnimationReference<Vec3F>(typeof(Vec3F));
		internal AnimationReference<QuatF> rotation = new AnimationReference<QuatF>(typeof(QuatF));
		internal AnimationReference<Vec3F> scale = new AnimationReference<Vec3F>(typeof(Vec3F));
		internal AnimationReference<int?> visibility = new AnimationReference<int?>(int.TYPE);

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void unmarshal(jm2lib.io.UnmarshalingStream in) throws java.io.IOException, ClassNotFoundException
		public virtual void unmarshal(UnmarshalingStream @in)
		{
			unknown0 = @in.readInt();
			name.unmarshal(@in);
			flags = @in.readInt();
			parent = @in.readShort();
			unknown1 = @in.readChar();
			translation.unmarshal(@in);
			rotation.unmarshal(@in);
			scale.unmarshal(@in);
			visibility.unmarshal(@in);
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void marshal(jm2lib.io.MarshalingStream out) throws java.io.IOException
		public virtual void marshal(MarshalingStream @out)
		{
			// TODO Auto-generated method stub

		}

		public virtual List<IndexEntry> Entries
		{
			set
			{
				name.Entries = value;
			}
		}

		public virtual jm2lib.blizzard.wow.lichking.Bone toWoW(Reference<STG> stgList, Reference<STC> stcList)
		{
			//TODO
			jm2lib.blizzard.wow.lichking.Bone output = new jm2lib.blizzard.wow.lichking.Bone();
			output.parentBone = parent;
			output.translation = translation.toWoW(stgList, stcList);
			AnimationBlock<QuatF> floatRotation = rotation.toWoW(stgList, stcList);
			foreach (ArrayRef<QuatF> values in floatRotation.values)
			{
				ArrayRef<QuatS> shortQuats = new ArrayRef<QuatS>(typeof(QuatS));
				foreach (QuatF value in values)
				{
					shortQuats.Add(value.toQuatS());
				}
				output.rotation.values.Add(shortQuats);
			}
			output.scale = scale.toWoW(stgList, stcList);

			return output;
		}

		public override string ToString()
		{
			StringBuilder builder = new StringBuilder();
//JAVA TO C# CONVERTER WARNING: The .NET Type.FullName property will not always yield results identical to the Java Class.getName method:
			builder.Append(this.GetType().FullName).Append(" {\n\tunknown0: ").Append(unknown0).Append("\n\tname: ").Append(name).Append("\n\tflags: ").Append(flags).Append("\n\tparent: ").Append(parent).Append("\n\tunknown1: ").Append(unknown1).Append("\n\ttranslation: ").Append(translation).Append("\n\trotation: ").Append(rotation).Append("\n\tscale: ").Append(scale).Append("\n\tvisibility: ").Append(visibility).Append("\n}");
			return builder.ToString();
		}
	}

}