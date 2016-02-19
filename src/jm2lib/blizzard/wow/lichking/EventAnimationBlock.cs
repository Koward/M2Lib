using System;
using System.Text;

namespace jm2lib.blizzard.wow.lichking
{

	using LERandomAccessFile = com.mindprod.ledatastream.LERandomAccessFile;

	using jm2lib.blizzard.common.types;
	using Vec2I = jm2lib.blizzard.common.types.Vec2I;
	using AnimFilesHandler = jm2lib.blizzard.wow.common.AnimFilesHandler;
	using MarshalingStream = jm2lib.io.MarshalingStream;
	using UnmarshalingStream = jm2lib.io.UnmarshalingStream;

	public class EventAnimationBlock : AnimFilesHandler
	{
		public short interpolationType;
		public short globalSequence;
		public ArrayRef<ArrayRef<int?>> timestamps;


		public EventAnimationBlock()
		{
			interpolationType = 0;
			globalSequence = 0;
			timestamps = new ArrayRef<ArrayRef<int?>>(typeof(ArrayRef), int.TYPE);
		}
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void unmarshal(jm2lib.io.UnmarshalingStream in) throws java.io.IOException, ClassNotFoundException
		public virtual void unmarshal(UnmarshalingStream @in)
		{
			interpolationType = @in.readShort();
			globalSequence = @in.readShort();
			timestamps.unmarshal(@in);
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void marshal(jm2lib.io.MarshalingStream out) throws java.io.IOException
		public virtual void marshal(MarshalingStream @out)
		{
			@out.writeShort(interpolationType);
			@out.writeShort(globalSequence);
			timestamps.marshal(@out);
		}

		public virtual LERandomAccessFile[] AnimFiles
		{
			set
			{
				timestamps.AnimFiles = value;
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void writeContent(jm2lib.io.MarshalingStream out) throws java.io.IOException, InstantiationException, IllegalAccessException
		public virtual void writeContent(MarshalingStream @out)
		{
			timestamps.writeContent(@out);
		}

		private string printInterpolationType()
		{
			return AnimationBlock.printInterpolationType(interpolationType);
		}

		/// <summary>
		/// Converts an AnimationBlock down to Burning Crusade </summary>
		/// <param name="animations">
		/// @return </param>
		/// <exception cref="Exception"> </exception>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public jm2lib.blizzard.wow.classic.EventAnimationBlock downConvert(jm2lib.blizzard.common.types.ArrayRef<jm2lib.blizzard.wow.classic.Animation> animations) throws Exception
		public virtual jm2lib.blizzard.wow.classic.EventAnimationBlock downConvert(ArrayRef<jm2lib.blizzard.wow.classic.Animation> animations)
		{
			jm2lib.blizzard.wow.classic.EventAnimationBlock output = new jm2lib.blizzard.wow.classic.EventAnimationBlock();
			output.interpolationType = interpolationType;
			output.globalSequence = globalSequence;
			if (timestamps.Count == 0)
			{
				// Nothing
			} // Constant for all animations
			else if (interpolationType == 0 && timestamps.Count == 1)
			{
				output.timestamps.AddRange(timestamps[0]);
				output.ranges.Add(new Vec2I(0, 1)); //* @author Stan84
			} // Global sequence
			else if (globalSequence == 0)
			{
				output.timestamps.AddRange(timestamps[0]);
			} // Standard behavior
			else if (timestamps.Count == animations.Count)
			{
				int oldSize = 0;
				for (int i = 0; i < timestamps.Count; i++)
				{
					int timeStart = animations[i].timeStart;
					int timeEnd = animations[i].timeEnd;
					if (timestamps[i].size() == 1) // Constant for animation i
					{
						output.timestamps.Add(timestamps[i].get(0) + timeStart);
						output.timestamps.Add(timestamps[i].get(0) + timeEnd);
						output.ranges.Add(new Vec2I(oldSize, output.timestamps.Count));
					}
					else if (timestamps[i].size() > 1)
					{
						for (int j = 0; j < timestamps[i].size(); j++)
						{
							output.timestamps.Add(timestamps[i].get(j) + timeStart);
						}
						output.ranges.Add(new Vec2I(oldSize, output.timestamps.Count));
					}
					else // No value for animation i
					{
						output.ranges.Add(new Vec2I(oldSize, oldSize + 0));
					}
					oldSize = output.timestamps.Count;
				}
				output.ranges.Add(new Vec2I());
			}
			else
			{
				StringBuilder builder = new StringBuilder();
				builder.Append("Unknown event animation block conversion error.\n");
				builder.Append(this.ToString());
				throw new Exception(builder.ToString());
			}
			return output;
		}
		public override string ToString()
		{
			StringBuilder result = new StringBuilder();
			string NEW_LINE = System.getProperty("line.separator");
			result.Append("Interpolation type : " + printInterpolationType() + NEW_LINE);
			result.Append("Global Sequence : " + globalSequence + NEW_LINE);
			result.Append("Timestamps : " + timestamps.ToString() + NEW_LINE);
			return result.ToString();
		}
	}

}