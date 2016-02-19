using System.Collections.Generic;
using System.Text;

namespace jm2lib.blizzard.sc2
{


	using MarshalingStream = jm2lib.io.MarshalingStream;
	using UnmarshalingStream = jm2lib.io.UnmarshalingStream;

	public class SD<T> : Indexer
	{
		public Reference<int?> timestamps;
		public int flags;
		public int biggestKey;
		public Reference<T> values;

		public SD()
		{
			timestamps = new Reference<>();
			values = new Reference<>();
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void unmarshal(jm2lib.io.UnmarshalingStream in) throws java.io.IOException, ClassNotFoundException
		public virtual void unmarshal(UnmarshalingStream @in)
		{
			timestamps.unmarshal(@in);
			flags = @in.readInt();
			biggestKey = @in.readInt();
			values.unmarshal(@in);
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void marshal(jm2lib.io.MarshalingStream out) throws java.io.IOException
		public virtual void marshal(MarshalingStream @out)
		{
			timestamps.marshal(@out);
			@out.writeInt(flags);
			@out.writeInt(biggestKey);
			values.marshal(@out);
		}

		public virtual bool Empty
		{
			get
			{
				return timestamps.Empty;
			}
		}

		public virtual List<IndexEntry> Entries
		{
			set
			{
				timestamps.Entries = value;
				values.Entries = value;
			}
		}

		public override string ToString()
		{
			StringBuilder builder = new StringBuilder();
			builder.Append("{\n\tflags: ").Append(flags).Append("\t biggestKey: ").Append(biggestKey);
			for (int i = 0; i < timestamps.size(); i++)
			{
				builder.Append("\n\t\t" + timestamps.get(i) + ":" + values.get(i));
			}

			builder.Append("\n}");
			return builder.ToString();
		}
	}

}