namespace jm2lib.blizzard.io
{

	using MagicInt = jm2lib.lang.MagicInt;

	/// <summary>
	/// Class representing the file type magic numbers for Blizzard files.
	/// 
	/// @author Dr Super Good
	/// </summary>
	public sealed class FileMagic : MagicInt
	{
		/// <seealso cref= super </seealso>
		/// <param name="value"> </param>
		public FileMagic(int value) : base(value)
		{
		}

		/// <seealso cref= super </seealso>
		/// <param name="value"> </param>
		public FileMagic(string value) : base(value)
		{
		}

	}

}