namespace jm2lib.blizzard.sc2
{

	using FileMagic = jm2lib.blizzard.io.FileMagic;
	using ObjectTypeProvider = jm2lib.blizzard.io.ObjectTypeProvider;

	/// <summary>
	/// Mapping for Warcraft 3 files handling classes.
	/// @author Koward
	/// 
	/// </summary>
	public class Sc2Files : ObjectTypeProvider
	{
		public Sc2Files()
		{
			addMapping(new FileMagic("43DM"), "jm2lib.blizzard.sc2.M3");
		}
	}

}