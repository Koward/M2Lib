using System;

namespace jm2lib.io
{


	using LERandomAccessFile = com.mindprod.ledatastream.LERandomAccessFile;

	/// <summary>
	/// @author Dr Super Good
	/// </summary>
	public class UnmarshalingStream : LERandomAccessFile
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
//ORIGINAL LINE: public UnmarshalingStream(String file) throws java.io.FileNotFoundException
		public UnmarshalingStream(string file) : base(file, "r")
		{
		}

		/// <summary>
		/// Creates a new instance of the incoming Object and unmarshal it. </summary>
		/// <param name="clazz"> The class of the Object
		/// @return </param>
		/// <exception cref="ClassNotFoundException"> </exception>
		/// <exception cref="IOException"> </exception>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: protected Object unmarshalObject(Class clazz) throws ClassNotFoundException, java.io.IOException
		protected internal virtual object unmarshalObject(Type clazz)
		{
			object obj;
			try
			{
				obj = clazz.newInstance();
			}
			catch (InstantiationException)
			{
//JAVA TO C# CONVERTER WARNING: The .NET Type.FullName property will not always yield results identical to the Java Class.getName method:
				throw new InvalidClassException(clazz.FullName, "missing no argument constructor");
			}
			catch (IllegalAccessException)
			{
//JAVA TO C# CONVERTER WARNING: The .NET Type.FullName property will not always yield results identical to the Java Class.getName method:
				throw new InvalidClassException(clazz.FullName, "cannot access no argument constructor");
			}
			if (!(obj is Marshalable))
			{
//JAVA TO C# CONVERTER WARNING: The .NET Type.FullName property will not always yield results identical to the Java Class.getName method:
				throw new InvalidClassException(clazz.FullName, "not unmarshalable");
			}

			((Marshalable) obj).unmarshal(this);
			return obj;
		}

		/// <summary>
		/// Utility to read generic types given in parameter. FIXME Dangerous ?
		/// @author Koward </summary>
		/// <param name="type">
		/// @return </param>
		/// <exception cref="ClassNotFoundException"> </exception>
		/// <exception cref="IOException"> </exception>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public Object readGeneric(Class type) throws ClassNotFoundException, java.io.IOException
		public virtual object readGeneric(Type type)
		{
			if (type.IsSubclassOf(typeof(Marshalable)))
			{
				return unmarshalObject(type);
			}
			else if (type == int.TYPE)
			{
				return readInt();
			}
			else if (type == float.TYPE)
			{
				return readFloat();
			}
			else if (type == short.TYPE)
			{
				return readShort();
			}
			else if (type == long.TYPE)
			{
				return readLong();
			}
			else if (type == sbyte.TYPE)
			{
				return readByte();
			}
			else if (type == char.TYPE)
			{
				return readChar();
			}
			else
			{
				throw new InvalidClassException("Class not recognized : " + type);
			}
		}

		/// <summary>
		/// Reads n bytes and return them as a byte array. </summary>
		/// <param name="n"> number of bytes. </param>
		/// <returns> a byte[] of length n. </returns>
		/// <exception cref="IOException"> </exception>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public byte[] readByteArray(int n) throws java.io.IOException
		public virtual sbyte[] readByteArray(int n)
		{
			sbyte[] byteArray = new sbyte[n];
			read(byteArray);
			return byteArray;
		}

		/// <summary>
		/// Reads a UTF-8 String represented on n bytes. 0 values are trimmed. </summary>
		/// <param name="n">
		/// @return </param>
		/// <exception cref="IOException"> </exception>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public String readString(int n) throws java.io.IOException
		public virtual string readString(int n)
		{
			return (StringHelperClass.NewString(readByteArray(n), StandardCharsets.UTF_8)).Trim();
		}
	}

}