using System.Collections.Generic;

namespace jm2lib.blizzard.sc2
{


	using MarshalingStream = jm2lib.io.MarshalingStream;
	using UnmarshalingStream = jm2lib.io.UnmarshalingStream;

	public class STG : Indexer
	{
		internal Reference<sbyte?> name = new Reference<sbyte?>();
		internal Reference<int?> stcIndices = new Reference<int?>();

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void unmarshal(jm2lib.io.UnmarshalingStream in) throws java.io.IOException, ClassNotFoundException
		public virtual void unmarshal(UnmarshalingStream @in)
		{
			name.unmarshal(@in);
			stcIndices.unmarshal(@in);
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void marshal(jm2lib.io.MarshalingStream out) throws java.io.IOException
		public virtual void marshal(MarshalingStream @out)
		{
			name.marshal(@out);
			stcIndices.marshal(@out);
		}

		public virtual List<IndexEntry> Entries
		{
			set
			{
				name.Entries = value;
				stcIndices.Entries = value;
			}
		}

	}

}