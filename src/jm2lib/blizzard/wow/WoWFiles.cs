namespace jm2lib.blizzard.wow
{

	using FileMagic = jm2lib.blizzard.io.FileMagic;
	using ObjectTypeProvider = jm2lib.blizzard.io.ObjectTypeProvider;

	/// <summary>
	/// Mapping for implemented WoW files handling classes.
	/// @author Koward
	/// 
	/// </summary>
	public class WoWFiles : ObjectTypeProvider
	{
		public WoWFiles()
		{
			addMapping(new FileMagic("MD20"), "jm2lib.blizzard.wow.M2");

			addMapping(new FileMagic("MD21"), "jm2lib.blizzard.wow.MD21");
			addMapping(new FileMagic("PFID"), "jm2lib.blizzard.wow.legion.PFID");
			addMapping(new FileMagic("SFID"), "jm2lib.blizzard.wow.legion.SFID");
			addMapping(new FileMagic("AFID"), "jm2lib.blizzard.wow.legion.AFID");
			addMapping(new FileMagic("BFID"), "jm2lib.blizzard.wow.legion.BFID");
		}
	}

}