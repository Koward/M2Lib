namespace jm2lib.blizzard.wow.common
{

	using LERandomAccessFile = com.mindprod.ledatastream.LERandomAccessFile;

	using Referencer = jm2lib.blizzard.common.interfaces.Referencer;

	/// <summary>
	/// Implemented by structures which may use .anim files.
	/// Allows to give them a reference to the .anim files list.
	/// @author Koward
	/// 
	/// </summary>
	public interface AnimFilesHandler : Referencer
	{
		LERandomAccessFile[] AnimFiles {set;}
	}

}