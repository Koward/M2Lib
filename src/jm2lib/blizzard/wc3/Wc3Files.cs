namespace jm2lib.blizzard.wc3
{

	using FileMagic = jm2lib.blizzard.io.FileMagic;
	using ObjectTypeProvider = jm2lib.blizzard.io.ObjectTypeProvider;

	/// <summary>
	/// Mapping for Warcraft 3 files handling classes.
	/// @author Koward
	/// 
	/// </summary>
	public class Wc3Files : ObjectTypeProvider
	{
		public Wc3Files()
		{
			addMapping(new FileMagic("MDLX"), "jm2lib.blizzard.wc3.MDX");
		}
	}

}