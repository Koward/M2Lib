using System.Collections.Generic;

namespace jm2lib.blizzard.wow
{


	using jm2lib.blizzard.common.types;
	using BlizzardFile = jm2lib.blizzard.io.BlizzardFile;
	using MCNK = jm2lib.blizzard.wow.adt.MCNK;
	using MarshalingStream = jm2lib.io.MarshalingStream;
	using UnmarshalingStream = jm2lib.io.UnmarshalingStream;

	/// <summary>
	/// ADT WoW File
	/// </summary>
	public class ADT : BlizzardFile
	{
		public Chunk<MCNK> mcnk;
//JAVA TO C# CONVERTER TODO TASK: Java wildcard generics are not converted to .NET:
//ORIGINAL LINE: public java.util.ArrayList<jm2lib.blizzard.common.types.Chunk<?>> chunks;
		public List<Chunk<?>> chunks;

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void unmarshal(jm2lib.io.UnmarshalingStream in) throws java.io.IOException, ClassNotFoundException
		public virtual void unmarshal(UnmarshalingStream @in)
		{
			while (@in.FilePointer < @in.length())
			{
				string magic = @in.readString(4);
				switch (magic)
				{
				case "MCNK":
					mcnk = new Chunk<>(typeof(MCNK), magic);
					mcnk.unmarshal(@in);
					chunks.Add(mcnk);
					break;
				default:
					Chunk<sbyte?> unknown = new Chunk<sbyte?>(sbyte.TYPE, magic);
					unknown.unmarshal(@in);
					chunks.Add(unknown);
				break;
				}
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void marshal(jm2lib.io.MarshalingStream out) throws java.io.IOException
		public virtual void marshal(MarshalingStream @out)
		{
//JAVA TO C# CONVERTER TODO TASK: Java wildcard generics are not converted to .NET:
//ORIGINAL LINE: for(jm2lib.blizzard.common.types.Chunk<?> chunk : chunks)
			foreach (Chunk<?> chunk in chunks)
			{
				chunk.marshal(@out);
			}
		}
	}

}