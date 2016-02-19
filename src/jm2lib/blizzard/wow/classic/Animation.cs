using System.Collections.Generic;
using System.Text;

namespace jm2lib.blizzard.wow.classic
{


	using Vec3F = jm2lib.blizzard.common.types.Vec3F;
	using Marshalable = jm2lib.io.Marshalable;
	using MarshalingStream = jm2lib.io.MarshalingStream;
	using UnmarshalingStream = jm2lib.io.UnmarshalingStream;

	public class Animation : Marshalable
	{
		public short animationID;
		public short subAnimationID;
		public int timeStart;
		public int timeEnd;
		public float movingSpeed;
		public int flags;
		public short probability;
		public short padding;
		public int[] repetitions;
		public int blendTime;
		public Vec3F minimumExtent;
		public Vec3F maximumExtent;
		public float boundRadius;
		public short nextAnimation;
		public short aliasNext;
		public Animation()
		{
			animationID = 0;
			subAnimationID = 0;
			timeStart = 0;
			timeEnd = 0;
			movingSpeed = 0;
			flags = 0x20;
			probability = 0;
			padding = 0;
			repetitions = new int[2];
			blendTime = 0;
			minimumExtent = new Vec3F();
			maximumExtent = new Vec3F();
			boundRadius = 0;
			nextAnimation = -1;
			aliasNext = 0;
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void unmarshal(jm2lib.io.UnmarshalingStream in) throws java.io.IOException, ClassNotFoundException
		public virtual void unmarshal(UnmarshalingStream @in)
		{
			animationID = @in.readShort();
			subAnimationID = @in.readShort();
			timeStart = @in.readInt();
			timeEnd = @in.readInt();
			movingSpeed = @in.readFloat();
			flags = @in.readInt();
			probability = @in.readShort();
			padding = @in.readShort();
			for (sbyte i = 0;i < 2; i++)
			{
				repetitions[i] = @in.readInt();
			}
			blendTime = @in.readInt();
			minimumExtent.unmarshal(@in);
			maximumExtent.unmarshal(@in);
			boundRadius = @in.readFloat();
			nextAnimation = @in.readShort();
			aliasNext = @in.readShort();
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void marshal(jm2lib.io.MarshalingStream out) throws java.io.IOException
		public virtual void marshal(MarshalingStream @out)
		{
			@out.writeShort(animationID);
			@out.writeShort(subAnimationID);
			@out.writeInt(timeStart);
			@out.writeInt(timeEnd);
			@out.writeFloat(movingSpeed);
			@out.writeInt(flags);
			@out.writeShort(probability);
			@out.writeShort(padding);
			for (sbyte i = 0;i < 2; i++)
			{
				@out.writeInt(repetitions[i]);
			}
			@out.writeInt(blendTime);
			minimumExtent.marshal(@out);
			maximumExtent.marshal(@out);
			@out.writeFloat(boundRadius);
			@out.writeShort(nextAnimation);
			@out.writeShort(aliasNext);
		}

		/// <summary>
		/// Generates {@code Animation#subAnimationID}, {@code Animation#aliasNext}
		/// and {@code Animation#nextAnimation} in a list of {@code Animation}. </summary>
		/// <param name="list"> the data will be generated according to the order of this ArrayList. </param>
		public static void linkAnimations(List<Animation> list)
		{
			Dictionary<short?, short?> counters = new Dictionary<short?, short?>();
			Dictionary<short?, short?> indexLast = new Dictionary<short?, short?>();
			foreach (Animation anim in list)
			{
				counters[anim.animationID] = (short) 0;
				indexLast[anim.animationID] = (short) -1;
			}
			for (short i = 0; i < list.Count; i++)
			{
				Animation anim = list[i];
				anim.subAnimationID = counters[anim.animationID];
				anim.aliasNext = i;

				counters[anim.animationID] = (short)(counters[anim.animationID] + 1);
			}
			for (int i = list.Count - 1; i >= 0; i--)
			{
				Animation anim = list[i];
				anim.nextAnimation = indexLast[anim.animationID];
				indexLast[anim.animationID] = (short) i;

			}
		}

		public override string ToString()
		{
			StringBuilder result = new StringBuilder();
			string NEW_LINE = System.getProperty("line.separator");
			result.Append("animationID : " + animationID + NEW_LINE);
			result.Append("subAnimationID : " + subAnimationID + NEW_LINE);
			result.Append("timeStart, timeEnd : " + timeStart + ":" + timeEnd + NEW_LINE);
			result.Append("movingSpeed : " + movingSpeed + NEW_LINE);
			result.Append("flags : " + int.toBinaryString(flags) + NEW_LINE);
			result.Append("probability : " + probability + NEW_LINE);
			result.Append("padding : " + padding + NEW_LINE);
			result.Append("unknown : (" + repetitions[0] + ',' + repetitions[1] + ')' + NEW_LINE);
			result.Append("blendTime : " + blendTime + NEW_LINE);
			result.Append("minimumExtent : " + minimumExtent.ToString() + NEW_LINE);
			result.Append("maximumExtent : " + maximumExtent.ToString() + NEW_LINE);
			result.Append("boundRadius : " + boundRadius + NEW_LINE);
			result.Append("nextAnimation : " + nextAnimation + NEW_LINE);
			result.Append("aliasNext : " + aliasNext + NEW_LINE);
			return result.ToString();
		}
	}

}