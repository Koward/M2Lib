using System;
using System.Collections.Generic;
using System.Text;

namespace jm2lib.blizzard.wow.classic
{


	using LERandomAccessFile = com.mindprod.ledatastream.LERandomAccessFile;

	using Referencer = jm2lib.blizzard.common.interfaces.Referencer;
	using jm2lib.blizzard.common.types;
	using Vec2I = jm2lib.blizzard.common.types.Vec2I;
	using MarshalingStream = jm2lib.io.MarshalingStream;
	using UnmarshalingStream = jm2lib.io.UnmarshalingStream;

	public class AnimationBlock<T> : Referencer
	{
		public short interpolationType;
		public short globalSequence;
		public ArrayRef<Vec2I> ranges;
		public ArrayRef<int?> timestamps;
		public ArrayRef<T> values;

		public AnimationBlock(Type type) : this(type, null)
		{
		}

		public AnimationBlock(Type type, List<LERandomAccessFile> animFiles)
		{
			interpolationType = 0;
			globalSequence = 0;
			ranges = new ArrayRef<Vec2I>(typeof(Vec2I));
			timestamps = new ArrayRef<int?>(int.TYPE);
			values = new ArrayRef<T>(type);
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void unmarshal(jm2lib.io.UnmarshalingStream in) throws java.io.IOException, ClassNotFoundException
		public virtual void unmarshal(UnmarshalingStream @in)
		{
			interpolationType = @in.readShort();
			globalSequence = @in.readShort();
			ranges.unmarshal(@in);
			timestamps.unmarshal(@in);
			values.unmarshal(@in);
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void marshal(jm2lib.io.MarshalingStream out) throws java.io.IOException
		public virtual void marshal(MarshalingStream @out)
		{
			@out.writeShort(interpolationType);
			@out.writeShort(globalSequence);
			ranges.marshal(@out);
			timestamps.marshal(@out);
			values.marshal(@out);
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void writeContent(jm2lib.io.MarshalingStream out) throws java.io.IOException, InstantiationException, IllegalAccessException
		public virtual void writeContent(MarshalingStream @out)
		{
			ranges.writeContent(@out);
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
//ORIGINAL LINE: final String map[] = {"None (static value)", "Linear", "Hermite", "Bezier"};
			string[] map = new string[] {"None (static value)", "Linear", "Hermite", "Bezier"};
			return (interpolationType <= 3 && interpolationType >= 0) ? map[interpolationType] : "Unknown (" + interpolationType + ")";
		}

		public override string ToString()
		{
			StringBuilder builder = new StringBuilder();
//JAVA TO C# CONVERTER WARNING: The .NET Type.FullName property will not always yield results identical to the Java Class.getName method:
			builder.Append(this.GetType().FullName).Append("\n\tinterpolationType: ").Append(printInterpolationType()).Append("\n\tglobalSequence: ").Append(globalSequence).Append("\n\tranges: ").Append(ranges).Append("\n\ttimestamps: ").Append(timestamps).Append("\n\tvalues: ").Append(values).Append("\n}");
			return builder.ToString();
		}

		public virtual bool Empty
		{
			get
			{
				return timestamps.Count == 0;
			}
		}

		public virtual void addKeyframe(int timestamp, T value)
		{
			timestamps.Add(timestamp);
			values.Add(value);
		}

		/// <summary>
		/// Computes interpolation {@code this#ranges}. </summary>
		/// <param name="animations"> </param>
		public virtual void computeRanges(IList<Animation> animations)
		{
			if (timestamps.Count <= 1)
			{
				return;
			}
			foreach (Animation animation in animations)
			{
//JAVA TO C# CONVERTER TODO TASK: Java lambdas satisfy functional interfaces, while .NET lambdas satisfy delegates - change the appropriate interface to a delegate:
				int firstTime = Collections.max(timestamps.stream().filter(s => s <= animation.timeStart).collect(Collectors.toList()));
//JAVA TO C# CONVERTER TODO TASK: Java lambdas satisfy functional interfaces, while .NET lambdas satisfy delegates - change the appropriate interface to a delegate:
				int lastTime = Collections.min(timestamps.stream().filter(s => s >= animation.timeEnd).collect(Collectors.toList()));
				ranges.Add(new Vec2I(timestamps.IndexOf(firstTime), timestamps.IndexOf(lastTime)));
			}
			ranges.Add(new Vec2I());
		}
	}

}