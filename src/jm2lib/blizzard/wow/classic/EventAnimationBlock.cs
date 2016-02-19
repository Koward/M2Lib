using System.Text;

namespace jm2lib.blizzard.wow.classic
{

	using Referencer = jm2lib.blizzard.common.interfaces.Referencer;
	using jm2lib.blizzard.common.types;
	using Vec2I = jm2lib.blizzard.common.types.Vec2I;
	using MarshalingStream = jm2lib.io.MarshalingStream;
	using UnmarshalingStream = jm2lib.io.UnmarshalingStream;

	public class EventAnimationBlock : Referencer
	{
		public short interpolationType;
		public short globalSequence;
		public ArrayRef<Vec2I> ranges;
		public ArrayRef<int?> timestamps;


		public EventAnimationBlock()
		{
			interpolationType = 0;
			globalSequence = 0;
			ranges = new ArrayRef<Vec2I>(typeof(Vec2I));
			timestamps = new ArrayRef<int?>(int.TYPE);
		}
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void unmarshal(jm2lib.io.UnmarshalingStream in) throws java.io.IOException, ClassNotFoundException
		public virtual void unmarshal(UnmarshalingStream @in)
		{
			interpolationType = @in.readShort();
			globalSequence = @in.readShort();
			ranges.unmarshal(@in);
			timestamps.unmarshal(@in);
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void marshal(jm2lib.io.MarshalingStream out) throws java.io.IOException
		public virtual void marshal(MarshalingStream @out)
		{
			@out.writeShort(interpolationType);
			@out.writeShort(globalSequence);
			ranges.marshal(@out);
			timestamps.marshal(@out);
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void writeContent(jm2lib.io.MarshalingStream out) throws java.io.IOException, InstantiationException, IllegalAccessException
		public virtual void writeContent(MarshalingStream @out)
		{
			ranges.writeContent(@out);
			timestamps.writeContent(@out);
		}

		private string printInterpolationType()
		{
			return AnimationBlock.printInterpolationType(interpolationType);
		}

		public override string ToString()
		{
			StringBuilder builder = new StringBuilder();
//JAVA TO C# CONVERTER WARNING: The .NET Type.FullName property will not always yield results identical to the Java Class.getName method:
			builder.Append(this.GetType().FullName).Append(" {\n\tinterpolationType: ").Append(printInterpolationType()).Append("\n\tglobalSequence: ").Append(globalSequence).Append("\n\tranges: ").Append(ranges).Append("\n\ttimestamps: ").Append(timestamps).Append("\n}");
			return builder.ToString();
		}
	}

}