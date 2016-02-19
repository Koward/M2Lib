using System.Collections.Generic;
using System.Text;

namespace jm2lib.blizzard.sc2
{


	using Animation = jm2lib.blizzard.wow.lichking.Animation;
	using MarshalingStream = jm2lib.io.MarshalingStream;
	using UnmarshalingStream = jm2lib.io.UnmarshalingStream;

	public class Sequence : Indexer
	{
		private readonly int version;

		internal int unknown0;
		internal int unknown1;
		internal Reference<sbyte?> name = new Reference<sbyte?>();
		internal int timeStart;
		internal int timeEnd;
		internal float movementSpeed;
		internal int flags;
		internal int frequency;
		internal int unknown2;
		internal int unknown3;
		internal int blendTime; //experimental
		internal int unknown5; //if v < 2
		internal BoundingSphere boundingSphere = new BoundingSphere();
		internal int unknown6;
		internal int unknown7;
		internal int unknown8;

		public Sequence(int version)
		{
			this.version = version;
		}


//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void unmarshal(jm2lib.io.UnmarshalingStream in) throws java.io.IOException, ClassNotFoundException
		public virtual void unmarshal(UnmarshalingStream @in)
		{
			unknown0 = @in.readInt();
			unknown1 = @in.readInt();
			name.unmarshal(@in);
			timeStart = @in.readInt();
			timeEnd = @in.readInt();
			movementSpeed = @in.readFloat();
			flags = @in.readInt();
			frequency = @in.readInt();
			unknown2 = @in.readInt();
			unknown3 = @in.readInt();
			blendTime = @in.readInt();
			if (version < 2)
			{
				unknown5 = @in.readInt();
			}
			boundingSphere.unmarshal(@in);
			unknown6 = @in.readInt();
			unknown7 = @in.readInt();
			unknown8 = @in.readInt();
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void marshal(jm2lib.io.MarshalingStream out) throws java.io.IOException
		public virtual void marshal(MarshalingStream @out)
		{
			@out.writeInt(unknown0);
			@out.writeInt(unknown1);
			name.marshal(@out);
			@out.writeInt(timeStart);
			@out.writeInt(timeEnd);
			@out.writeFloat(movementSpeed);
			@out.writeInt(flags);
			@out.writeInt(frequency);
			@out.writeInt(unknown2);
			@out.writeInt(unknown3);
			@out.writeInt(blendTime);
			if (version < 2)
			{
				@out.writeInt(unknown5);
			}
			boundingSphere.marshal(@out);
			@out.writeInt(unknown6);
			@out.writeInt(unknown7);
			@out.writeInt(unknown8);
		}

		public virtual List<IndexEntry> Entries
		{
			set
			{
				name.Entries = value;
			}
		}

		public virtual Animation toWoW()
		{
			Animation output = new Animation();
			//todo IDs. See art tools doc animation tokens
			output.length = timeEnd - timeStart;
			output.movingSpeed = movementSpeed;
			output.flags = flags;
			output.probability = (short) frequency;
			output.blendTime = blendTime;
			output.minimumExtent = boundingSphere.minimum;
			output.maximumExtent = boundingSphere.maximum;
			output.boundRadius = boundingSphere.radius;
			return output;
		}

		public override string ToString()
		{
			StringBuilder builder = new StringBuilder();
//JAVA TO C# CONVERTER WARNING: The .NET Type.FullName property will not always yield results identical to the Java Class.getName method:
			builder.Append(this.GetType().FullName).Append(" {\n\tunknown0: ").Append(unknown0).Append("\n\tunknown1: ").Append(unknown1).Append("\n\tname: ").Append(name.toNameString()).Append("\n\ttimeStart: ").Append(timeStart).Append("\n\ttimeEnd: ").Append(timeEnd).Append("\n\tmovementSpeed: ").Append(movementSpeed).Append("\n\tflags: ").Append(flags).Append("\n\tfrequency: ").Append(frequency).Append("\n\tunknown2: ").Append(unknown2).Append("\n\tunknown3: ").Append(unknown3).Append("\n\tunknown4: ").Append(blendTime).Append("\n\tunknown5: ").Append(unknown5).Append("\n\tboundingSphere: ").Append(boundingSphere).Append("\n\tunknown6: ").Append(unknown6).Append("\n\tunknown7: ").Append(unknown7).Append("\n\tunknown8: ").Append(unknown8).Append("\n}");
			return builder.ToString();
		}

	}

}