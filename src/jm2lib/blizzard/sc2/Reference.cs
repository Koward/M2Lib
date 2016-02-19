using System.Collections.Generic;

namespace jm2lib.blizzard.sc2
{


	using MarshalingStream = jm2lib.io.MarshalingStream;
	using UnmarshalingStream = jm2lib.io.UnmarshalingStream;

	/// 
	/// <summary>
	/// @author Koward
	/// </summary>
	public class Reference<T> : AbstractList<T>, Indexer
	{
		internal IndexEntry entry;
		internal int nEntries;
		internal int index;
		internal int flags;

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void unmarshal(jm2lib.io.UnmarshalingStream in) throws java.io.IOException, ClassNotFoundException
		public virtual void unmarshal(UnmarshalingStream @in)
		{
			nEntries = @in.readInt();
			index = @in.readInt();
			flags = @in.readInt();
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void marshal(jm2lib.io.MarshalingStream out) throws java.io.IOException
		public virtual void marshal(MarshalingStream @out)
		{
			// TODO Auto-generated method stub

		}

		public virtual List<IndexEntry> Entries
		{
			set
			{
				entry = value[index];
				foreach (object obj in entry)
				{
					if (obj is Indexer)
					{
						((Indexer) obj).Entries = value;
					}
				}
				//if(entry.entries != nEntries) System.out.println("Yep, this exist : " + entry.entries + " and " + nEntries);//FIXME
			}
		}

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @SuppressWarnings("unchecked") @Override public T get(int index)
		public override T get(int index)
		{
			return (T) entry[index];
		}

		public override int size()
		{
			return nEntries;
		}

		/// <summary>
		/// Build a human-readable String from the characters referenced. </summary>
		/// <returns> a readable string. </returns>
		public virtual string toNameString()
		{
			if (size() == 0)
			{
				return "";
			}
			sbyte[] array = new sbyte[size()];
			for (int i = 0; i < size(); i++)
			{
				array[i] = (sbyte) get(i);
			}
			return (StringHelperClass.NewString(array, StandardCharsets.UTF_8)).Trim();
		}

		public override string ToString()
		{
			if (entry == null)
			{
				return "<Not in Version>";
			}
			return entry.ToString();
		}
	}
}