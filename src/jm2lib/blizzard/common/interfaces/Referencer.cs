namespace jm2lib.blizzard.common.interfaces
{

	using Marshalable = jm2lib.io.Marshalable;
	using MarshalingStream = jm2lib.io.MarshalingStream;

	/// <summary>
	/// Implemented by structures which have some variable length content
	/// pointed by ArrayRefs. This kind of content has to be written AFTER the
	/// structures to not break them, so it must be called "manually" when a block has finished its
	/// marshaling.
	/// 
	/// @author Koward
	/// 
	/// </summary>
	public interface Referencer : Marshalable
	{
		/// <summary>
		/// Marshals the referenced content. </summary>
		/// <param name="out"> </param>
		/// <exception cref="IOException"> </exception>
		/// <exception cref="InstantiationException"> </exception>
		/// <exception cref="IllegalAccessException"> </exception>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void writeContent(jm2lib.io.MarshalingStream out) throws java.io.IOException, InstantiationException, IllegalAccessException;
		void writeContent(MarshalingStream @out);
	}

}