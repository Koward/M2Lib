using System;

namespace jm2lib.blizzard.io
{


	using UnmarshalingStream = jm2lib.io.UnmarshalingStream;

	/// <summary>
	/// Input stream for objects from Blizzard games. The resolved object type is
	/// determined by the four character file magic number.
	/// 
	/// @author Dr Super Good
	/// </summary>
	public class BlizzardInputStream : UnmarshalingStream
	{
		private long taring = 0;

		/// <summary>
		/// Creates a {@code BlizzardInputStream} preparing it to produce objects.
		/// </summary>
		/// <param name="file">
		///            file to read/write. </param>
		/// <exception cref="FileNotFoundException">
		///             if open fails. </exception>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public BlizzardInputStream(String file) throws java.io.FileNotFoundException
		public BlizzardInputStream(string file) : base(file)
		{
		}

		/// <summary>
		/// Reads an Object from the stream.
		/// </summary>
		/// <returns> the Object instance. </returns>
		/// <exception cref="ClassNotFoundException"> </exception>
		/// <exception cref="IOException"> </exception>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public Object readObject() throws ClassNotFoundException, java.io.IOException
		public virtual object readObject()
		{
			Type oclass = ObjectTypeResolver.resolver.resolveClass(new FileMagic(readInt()));
			return unmarshalObject(oclass);
		}

		/// <summary>
		/// Some formats stores offsets relative to their own beginning.
		/// Example : {@code M2}
		/// </summary>
		/// <exception cref="IOException"> </exception>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public Object readTared() throws java.io.IOException, ClassNotFoundException
		public virtual object readTared()
		{
			taring = FilePointer;
			object res = readObject();
			taring = 0;
			return res;
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