namespace jm2lib.io
{

	/// <summary>
	/// Parent interface for any custom marshal implementation.
	/// 
	/// All marshal stream implementations should test if an object or class owns a
	/// subclass of this interface specific to the type of stream. It does not extend
	/// {@code Serializable} rendering it incompatible with standard object streams.
	/// These measures are to prevent the production of invalid output streams.
	/// <para>
	/// All implementations require zero argument constructor. Marshal stream
	/// implementations will call this constructor before calling {@code unmarshal}.
	/// 
	/// @author Dr Super Good
	/// 
	/// </para>
	/// </summary>
	public interface Marshalable
	{
		/// <summary>
		/// Reads the object state from an input stream.
		/// </summary>
		/// <param name="in">
		///            - the source of marshaled data. </param>
		/// <exception cref="IOException">
		///             - if an IO exception occurs. </exception>
		/// <exception cref="ClassNotFoundException">
		///             - if there is problem resolving other classes in the class
		///             graph. </exception>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void unmarshal(UnmarshalingStream in) throws java.io.IOException, ClassNotFoundException;
		void unmarshal(UnmarshalingStream @in);

		/// <summary>
		/// Writes the object state to an output stream.
		/// </summary>
		/// <param name="out">
		///            - the destination of marshaled data. </param>
		/// <exception cref="IOException">
		///             - if an IO exception occurs. </exception>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void marshal(MarshalingStream out) throws java.io.IOException;
		void marshal(MarshalingStream @out);
	}

}