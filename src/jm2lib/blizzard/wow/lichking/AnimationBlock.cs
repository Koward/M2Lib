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

	public class AnimationBlock<T> : AnimFilesHandler
	{
		public short interpolationType;
		public short globalSequence;
		public ArrayRef<ArrayRef<int?>> timestamps;
		public ArrayRef<ArrayRef<T>> values;
		private int hint = 0;

		public AnimationBlock(Type type) : this(type, 0)
		{
		}

		/// <summary>
		/// Hints are used to change the default value inside of an AnimationBlock.
		/// Useful for adding new values or down converting </summary>
		/// <seealso cref= ArrayRef#addNew(int) </seealso>
		/// <param name="hint"> </param>
		public AnimationBlock(Type type, int hint)
		{
			this.hint = hint;
			interpolationType = 0;
			globalSequence = 0;
			timestamps = new ArrayRef<ArrayRef<int?>>(typeof(ArrayRef), int.TYPE);
			values = new ArrayRef<ArrayRef<T>>(typeof(ArrayRef), type);
		}

		public virtual LERandomAccessFile[] AnimFiles
		{
			set
			{
				timestamps.AnimFiles = value;
				values.AnimFiles = value;
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void unmarshal(jm2lib.io.UnmarshalingStream in) throws java.io.IOException, ClassNotFoundException
		public virtual void unmarshal(UnmarshalingStream @in)
		{
			interpolationType = @in.readShort();
			globalSequence = @in.readShort();
			timestamps.unmarshal(@in);
			values.unmarshal(@in);
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void marshal(jm2lib.io.MarshalingStream out) throws java.io.IOException
		public virtual void marshal(MarshalingStream @out)
		{
			@out.writeShort(interpolationType);
			@out.writeShort(globalSequence);
			timestamps.marshal(@out);
			values.marshal(@out);
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void writeContent(jm2lib.io.MarshalingStream out) throws java.io.IOException, InstantiationException, IllegalAccessException
		public virtual void writeContent(MarshalingStream @out)
		{
			timestamps.writeContent(@out);
			values.writeContent(@out);
		}

		private string printInterpolationType()
		{
			return printInterpolationType(interpolationType);
		}

		public static string printInterpolationType(int interpolationType)
		{
//JAVA TO C# CONVERTER WARNING: The original Java variable was marked 'final':
//ORIGINAL LINE: final String map[] = { "None (static value)", "Linear", "Hermite", "Bezier" };
			string[] map = new string[] {"None (static value)", "Linear", "Hermite", "Bezier"};
			return (interpolationType <= 3 && interpolationType >= 0) ? map[interpolationType] : "Unknown (" + interpolationType + ")";
		}

		/// <summary>
		/// Converts an AnimationBlock down to Burning Crusade </summary>
		/// <param name="animations">
		/// @return </param>
		/// <exception cref="Exception"> </exception>
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @SuppressWarnings("unchecked") public jm2lib.blizzard.wow.classic.AnimationBlock<T> downConvert(jm2lib.blizzard.common.types.ArrayRef<jm2lib.blizzard.wow.classic.Animation> animations) throws Exception
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
		public virtual jm2lib.blizzard.wow.classic.AnimationBlock<T> downConvert(ArrayRef<jm2lib.blizzard.wow.classic.Animation> animations)
		{
			jm2lib.blizzard.wow.classic.AnimationBlock<T> output = new jm2lib.blizzard.wow.classic.AnimationBlock<T>((Type) values.SubType);
			output.interpolationType = interpolationType;
			output.globalSequence = globalSequence;
			if (timestamps.Count == 0)
			{
				/*
				if(hint == 1) {
					//Default value. Example : 
					//If no information about when a particle is enabled is given, that means it is always enabled.
					output.timestamps.add(0);
					output.values.addNew(hint);
				}
				*/
			}
			else if (timestamps.Count != values.Count)
			{
				throw new Exception("Number of timestamps (" + timestamps.Count + ") different than number of values (" + values.Count + ")");
			} // Global sequence
			else if (globalSequence >= 0)
			{
				output.timestamps.AddRange(timestamps[0]);
				output.values.AddRange(values[0]);
			} // Standard behavior
			else if (timestamps.Count == animations.Count)
			{
				for (int i = 0; i < timestamps.Count; i++)
				{
					int timeStart = animations[i].timeStart;
					int timeEnd = animations[i].timeEnd;
					if (timestamps[i].size() == 1) // Constant for animation i
					{
						output.timestamps.Add(timestamps[i].get(0) + timeStart);
						output.timestamps.Add(timestamps[i].get(0) + timeEnd);
						output.values.Add(values[i].get(0));
						output.values.Add(values[i].get(0));
					}
					else if (timestamps[i].size() > 1)
					{
						for (int j = 0; j < timestamps[i].size(); j++)
						{
							output.timestamps.Add(timestamps[i].get(j) + timeStart);
							output.values.Add(values[i].get(j));
						}
					}
					else // No value for animation i
					{
						output.timestamps.Add(timeStart);
						output.timestamps.Add(timeEnd);
						output.values.addNew(hint);
						output.values.addNew(hint);
					}
				}
				int rangeTime = 0;
				for (int j = 0; j < timestamps.Count; j++)
				{
						Vec2I range = new Vec2I();
						range.X = rangeTime;
					if (timestamps[j].size() == 1)
					{
						rangeTime++;
					}
					else if (timestamps[j].size() == 0)
					{
						rangeTime++;
					} //n > 1
					else
					{
						rangeTime += timestamps[j].size() - 1;
					}
						range.Y = rangeTime;
						output.ranges.Add(range);
						rangeTime++;
				}
				output.ranges.Add(new Vec2I());
			} // Constant for all animations ?
			else if (timestamps.Count == 1)
			{
				output.timestamps.AddRange(timestamps[0]);
				output.values.AddRange(values[0]);
			}
			else
			{
				StringBuilder builder = new StringBuilder();
				builder.Append("Unknown animation block conversion error.\n");
				builder.Append(this.ToString());
				throw new Exception(builder.ToString());
			}
			return output;
		}

		public override string ToString()
		{
			StringBuilder builder = new StringBuilder();
//JAVA TO C# CONVERTER WARNING: The .NET Type.FullName property will not always yield results identical to the Java Class.getName method:
			builder.Append(this.GetType().FullName).Append("\n\tinterpolationType: ").Append(printInterpolationType()).Append("\n\tglobalSequence: ").Append(globalSequence).Append("\n\ttimestamps: ").Append(timestamps).Append("\n\tvalues: ").Append(values).Append("\n}");
			return builder.ToString();
		}

		public virtual bool Empty
		{
			get
			{
				return timestamps.Count == 0;
			}
		}
	}

}