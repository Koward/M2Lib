using System.Collections.Generic;

namespace jm2lib.blizzard.sc2
{

	using Marshalable = jm2lib.io.Marshalable;

	/// <summary>
	/// Implemented by structures which needs the M3 indexEntries or needs to pass it to, by example, a {@code Reference}
	/// @author Koward
	/// 
	/// </summary>
	public interface Indexer : Marshalable
	{
		List<IndexEntry> Entries {set;}
	}

}