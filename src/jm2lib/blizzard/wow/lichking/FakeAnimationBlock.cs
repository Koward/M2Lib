using System;
using System.Text;

namespace jm2lib.blizzard.wow.lichking
{

	using Referencer = jm2lib.blizzard.common.interfaces.Referencer;
	using jm2lib.blizzard.common.types;
	using MarshalingStream = jm2lib.io.MarshalingStream;
	using UnmarshalingStream = jm2lib.io.UnmarshalingStream;

	public class FakeAnimationBlock<T> : Referencer
	{
		public ArrayRef<short?> timestamps;
		public ArrayRef<T> values;

		public FakeAnimationBlock(Type type)
		{
			timestamps = new ArrayRef<short?>(short.TYPE);
			values = new ArrayRef<T>(type);
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void unmarshal(jm2lib.io.UnmarshalingStream in) throws java.io.IOException, ClassNotFoundException
		public virtual void unmarshal(UnmarshalingStream @in)
		{
			timestamps.unmarshal(@in);
			values.unmarshal(@in);
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void marshal(jm2lib.io.MarshalingStream out) throws java.io.IOException
		public virtual void marshal(MarshalingStream @out)
		{
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

		public override string ToString()
		{
			StringBuilder builder = new StringBuilder();
//JAVA TO C# CONVERTER WARNING: The .NET Type.FullName property will not always yield results identical to the Java Class.getName method:
			builder.Append(this.GetType().FullName).Append("\n\ttimestamps: ").Append(timestamps).Append("\n\tvalues: ").Append(values).Append("\n}");
			return builder.ToString();
		}
	}

}