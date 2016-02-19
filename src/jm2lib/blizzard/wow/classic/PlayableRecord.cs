using System.Text;

namespace jm2lib.blizzard.wow.classic
{

	using Marshalable = jm2lib.io.Marshalable;
	using MarshalingStream = jm2lib.io.MarshalingStream;
	using UnmarshalingStream = jm2lib.io.UnmarshalingStream;

	public class PlayableRecord : Marshalable
	{
		public short fallbackID;
		public short flags;

		public PlayableRecord() : this((short) 0,(short) 0)
		{
		}

		public PlayableRecord(short fallbackID, short flags)
		{
			this.fallbackID = fallbackID;
			this.flags = flags;
		}
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void unmarshal(jm2lib.io.UnmarshalingStream in) throws java.io.IOException, ClassNotFoundException
		public virtual void unmarshal(UnmarshalingStream @in)
		{
			fallbackID = @in.readShort();
			flags = @in.readShort();
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void marshal(jm2lib.io.MarshalingStream out) throws java.io.IOException
		public virtual void marshal(MarshalingStream @out)
		{
			@out.writeShort(fallbackID);
			@out.writeShort(flags);
		}

		public override string ToString()
		{
			StringBuilder builder = new StringBuilder();
//JAVA TO C# CONVERTER WARNING: The .NET Type.FullName property will not always yield results identical to the Java Class.getName method:
			builder.Append(this.GetType().FullName).Append(" {\n\tfallbackID: ").Append(fallbackID).Append("\n\tflags: ").Append(flags).Append("\n}");
			return builder.ToString();
		}
	}

}