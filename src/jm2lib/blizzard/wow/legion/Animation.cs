using System.Collections.Generic;
using System.Text;

namespace jm2lib.blizzard.wow.legion
{


	using Vec3F = jm2lib.blizzard.common.types.Vec3F;
	using Marshalable = jm2lib.io.Marshalable;
	using MarshalingStream = jm2lib.io.MarshalingStream;
	using UnmarshalingStream = jm2lib.io.UnmarshalingStream;

	public class Animation : Marshalable
	{
		public short animationID;
		public short subAnimationID;
		public int length;
		public float movingSpeed;
		public int flags;
		public short probability;
		public short padding;
		public int[] repetitions;
		public char startBlendTime;
		public char endBlendTime;
		public Vec3F minimumExtent;
		public Vec3F maximumExtent;
		public float boundRadius;
		public short nextAnimation;
		public short aliasNext;
		public Animation()
		{
			animationID = 0;
			subAnimationID = 0;
			length = 0;
			movingSpeed = 0;
			flags = 0x20;
			probability = 0;
			padding = 0;
			repetitions = new int[2];
			startBlendTime = (char)150;
			endBlendTime = (char)150;
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
			length = @in.readInt();
			movingSpeed = @in.readFloat();
			flags = @in.readInt();
			probability = @in.readShort();
			padding = @in.readShort();
			for (sbyte i = 0;i < 2; i++)
			{
				repetitions[i] = @in.readInt();
			}
			startBlendTime = @in.readChar();
			endBlendTime = @in.readChar();
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
			@out.writeInt(length);
			@out.writeFloat(movingSpeed);
			@out.writeInt(flags);
			@out.writeShort(probability);
			@out.writeShort(padding);
			for (sbyte i = 0;i < 2; i++)
			{
				@out.writeInt(repetitions[i]);
			}
			@out.writeChar(startBlendTime);
			@out.writeChar(endBlendTime);
			minimumExtent.marshal(@out);
			maximumExtent.marshal(@out);
			@out.writeFloat(boundRadius);
			@out.writeShort(nextAnimation);
			@out.writeShort(aliasNext);
		}

		/// 
		/// <returns> true if the animations is an alias </returns>
		public virtual bool Alias
		{
			get
			{
				return (flags & 0x40) != 0;
			}
		}

		/// <summary>
		/// Check if the animation data are stored in the model </summary>
		/// <returns> true if the content will be in .anim files </returns>
		public virtual bool Extern
		{
			get
			{
				return (flags & 0x130) == 0;
			}
		}

		public static int getRealPos(int i, List<Animation> list)
		{
			if (!list[i].Alias)
			{
				return i;
			}
			else
			{
				return getRealPos(list[i].aliasNext, list);
			}
		}

		public virtual jm2lib.blizzard.wow.lichking.Animation downConvert()
		{
			jm2lib.blizzard.wow.lichking.Animation output = new jm2lib.blizzard.wow.lichking.Animation();
			output.animationID = animationID;
			output.subAnimationID = subAnimationID;
			output.length = length;
			output.movingSpeed = movingSpeed;
			output.flags = flags;
			output.probability = probability;
			output.padding = padding;
			output.repetitions = repetitions;
			output.blendTime = (int) startBlendTime;
			output.minimumExtent = minimumExtent;
			output.maximumExtent = maximumExtent;
			output.boundRadius = boundRadius;
			output.nextAnimation = nextAnimation;
			output.aliasNext = aliasNext;
			return output;
		}

		public override string ToString()
		{
			StringBuilder result = new StringBuilder();
			string NEW_LINE = System.getProperty("line.separator");
			result.Append("animationID : " + animationID + NEW_LINE);
			result.Append("subAnimationID : " + subAnimationID + NEW_LINE);
			result.Append("length : " + length + NEW_LINE);
			result.Append("movingSpeed : " + movingSpeed + NEW_LINE);
			result.Append("flags : " + int.toBinaryString(flags) + NEW_LINE);
			result.Append("probability : " + probability + NEW_LINE);
			result.Append("padding : " + padding + NEW_LINE);
			result.Append("unknown : (" + repetitions[0] + ',' + repetitions[1] + ')' + NEW_LINE);
			result.Append("startBlendTime : " + (int) startBlendTime + NEW_LINE);
			result.Append("endBlendTime : " + (int) endBlendTime + NEW_LINE);
			result.Append("minimumExtent : " + minimumExtent.ToString() + NEW_LINE);
			result.Append("maximumExtent : " + maximumExtent.ToString() + NEW_LINE);
			result.Append("boundRadius : " + boundRadius + NEW_LINE);
			result.Append("nextAnimation : " + nextAnimation + NEW_LINE);
			result.Append("aliasNext : " + aliasNext + NEW_LINE);
			return result.ToString();
		}
	}

}