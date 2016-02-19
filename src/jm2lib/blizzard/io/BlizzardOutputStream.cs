namespace jm2lib.blizzard.io
{


	using Marshalable = jm2lib.io.Marshalable;
	using MarshalingStream = jm2lib.io.MarshalingStream;

	/// <summary>
	/// Output stream for objects from Blizzard games. The file magic number is
	/// determined by the class of object.
	/// 
	/// @author Dr Super Good
	/// </summary>
	public class BlizzardOutputStream : MarshalingStream
	{
		private long taring = 0;

		/// <summary>
		/// Creates a {@code BlizzardOutputStream} preparing it for objects.
		/// </summary>
		/// <param name="file">
		///            file to read/write. </param>
		/// <exception cref="FileNotFoundException">
		///             if open fails. </exception>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public BlizzardOutputStream(String file) throws java.io.FileNotFoundException
		public BlizzardOutputStream(string file) : base(file)
		{
		}

		/// <summary>
		/// Writes an Object to the stream.
		/// </summary>
		/// <param name="obj"> </param>
		/// <exception cref="IOException"> </exception>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void writeObject(Object obj) throws java.io.IOException
		public virtual void writeObject(object obj)
		{
			if (!(obj is BlizzardFile))
			{
//JAVA TO C# CONVERTER WARNING: The .NET Type.FullName property will not always yield results identical to the Java Class.getName method:
				throw new NotSerializableException(string.Format("{0} is not a Blizzard object so cannot be marshaled", obj.GetType().FullName));
			}
			writeInt(ObjectTypeResolver.resolver.resolveMagic(obj.GetType()).toInt());
			marshalObject((Marshalable) obj);
		}

		/// <summary>
		/// Some formats stores offsets relative to their own beginning.
		/// Example : {@code M2}
		/// </summary>
		/// <exception cref="IOException"> </exception>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void writeTared(Object obj) throws java.io.IOException
		public virtual void writeTared(object obj)
		{
			taring = FilePointer;
			writeObject(obj);
			taring = 0;
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void seek(long pos) throws java.io.IOException
		public override void seek(long pos)
		{
			base.seek(pos + taring);
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public long getFilePointer() throws java.io.IOException
		public override long FilePointer
		{
			get
			{
				return base.FilePointer - taring;
			}
		}
	}

}