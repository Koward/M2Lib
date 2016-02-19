using System;

namespace jm2lib.io
{


	using LERandomAccessFile = com.mindprod.ledatastream.LERandomAccessFile;

	/// <summary>
	/// Output Stream for serializing non-Java formated objects.
	/// <para>
	/// A subclass can be used to write objects to specific and unusual data formats.
	/// Such file formats might have been designed for use with other programming
	/// languages or even specific programs.
	/// </para>
	/// <para>
	/// How objects are written and what objects are accepted is up to
	/// implementations.
	/// </para>
	/// <para>
	/// This class is non-buffering so it is recommended to buffer the output stream
	/// for better performance. All basic I/O will be out-sourced to the underlying
	/// stream for efficiency.
	/// 
	/// @author Dr Super Good
	/// </para>
	/// </summary>
	public class MarshalingStream : LERandomAccessFile
	{
		/// <summary>
		/// Used by inheriting classes to construct the base stream. From there they
		/// should only use the methods defined by this class to access the stream.
		/// </summary>
		/// <param name="file">
		///            file to read/write. </param>
		/// <exception cref="FileNotFoundException"> 
		///             if open fails. </exception>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public MarshalingStream(String file) throws java.io.FileNotFoundException
		public MarshalingStream(string file) : base(file, "rw")
		{
		}

		/// <summary>
		/// Marshals the object in this stream. </summary>
		/// <param name="obj"> </param>
		/// <exception cref="IOException"> </exception>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: protected void marshalObject(Marshalable obj) throws java.io.IOException
		protected internal virtual void marshalObject(Marshalable obj)
		{
			obj.marshal(this);
		}

		/// <summary>
		/// Write generic types given in parameter. FIXME Dangerous ?
		/// @author Koward </summary>
		/// <param name="type"> </param>
		/// <param name="value"> </param>
		/// <exception cref="IOException"> </exception>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void writeGeneric(Class type, Object value) throws java.io.IOException
		public virtual void writeGeneric(Type type, object value)
		{
			if (type.IsSubclassOf(typeof(Marshalable)))
			{
				marshalObject((Marshalable) value);
			}
			else if (type == int.TYPE)
			{
				writeInt((int) value);
			}
			else if (type == float.TYPE)
			{
				writeFloat((float) value);
			}
			else if (type == short.TYPE)
			{
				writeShort((short) value);
			}
			else if (type == long.TYPE)
			{
				writeLong((long) value);
			}
			else if (type == sbyte.TYPE)
			{
				writeByte((sbyte) value);
			}
			else if (type == char.TYPE)
			{
				writeChar((char) value);
			}
			else
			{
				throw new InvalidClassException("Class not recognized : " + type);
			}
		}

		/// <summary>
		/// Writes UTF-8 String on n bytes. 0 values are added to fill the space. </summary>
		/// <param name="n"> number of bytes. </param>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void writeString(String str, int n) throws java.io.IOException
		public virtual void writeString(string str, int n)
		{
			if (n < str.Length)
			{
				throw new IOException("String is too long to be written on " + n + " bytes.");
			}
			write(Arrays.copyOf(str.GetBytes(StandardCharsets.UTF_8), n));
		}
	}

}