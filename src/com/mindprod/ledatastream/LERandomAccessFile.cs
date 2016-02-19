using System.Text;

/*
 * [LERandomAccessFile.java]
 *
 * Summary: Little-endian version of RandomAccessFile.
 *
 * Copyright: (c) 1998-2015 Roedy Green, Canadian Mind Products, http://mindprod.com
 *
 * Licence: This software may be copied and used freely for any purpose but military.
 *          http://mindprod.com/contact/nonmil.html
 *
 * Requires: JDK 1.7+
 *
 * Created with: JetBrains IntelliJ IDEA IDE http://www.jetbrains.com/idea/
 *
 * Version History:
 *  1.8 2007-05-24
 */
namespace com.mindprod.ledatastream
{


	/// <summary>
	/// Little-endian version of RandomAccessFile.
	/// 
	/// @author Roedy Green, Canadian Mind Products
	/// @version 1.8 2007-05-24
	/// @since 1998
	/// </summary>
	public class LERandomAccessFile : DataInput, DataOutput, Closeable
	{
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @SuppressWarnings("unused") private static final int FIRST_COPYRIGHT_YEAR = 1999;
		private const int FIRST_COPYRIGHT_YEAR = 1999;

		/// <summary>
		/// undisplayed copyright notice.
		/// 
		/// @noinspection UnusedDeclaration
		/// </summary>
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @SuppressWarnings("unused") private static final String EMBEDDED_COPYRIGHT = "Copyright: (c) 1999-2015 Roedy Green, Canadian Mind Products, http://mindprod.com";
		private const string EMBEDDED_COPYRIGHT = "Copyright: (c) 1999-2015 Roedy Green, Canadian Mind Products, http://mindprod.com";

		/// <summary>
		/// to get at the big-endian methods of RandomAccessFile .
		/// 
		/// @noinspection WeakerAccess
		/// </summary>
		protected internal RandomAccessFile raf;

		/// <summary>
		/// work array for buffering input/output.
		/// 
		/// @noinspection WeakerAccess
		/// </summary>
		protected internal sbyte[] work;

		/// <summary>
		/// the name of the currently open stream.
		/// </summary>
		private string fileName;

		/// <summary>
		/// constructor.
		/// </summary>
		/// <param name="file">
		///            file to read/write. </param>
		/// <param name="rw">
		///            like <seealso cref="java.io.RandomAccessFile"/> where "r" for read "rw"
		///            for read and write, "rws" for read-write sync, and "rwd" for
		///            read-write dsync. Sync ensures the physical I/O has completed
		///            befor the method returns.
		/// </param>
		/// <exception cref="java.io.FileNotFoundException">
		///             if open fails. </exception>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public LERandomAccessFile(java.io.File file, String rw) throws java.io.FileNotFoundException
		public LERandomAccessFile(File file, string rw)
		{
			fileName = file.AbsolutePath;
			raf = new RandomAccessFile(file, rw);
			work = new sbyte[8];
		}

		/// <summary>
		/// constructors.
		/// </summary>
		/// <param name="file">
		///            name of file. </param>
		/// <param name="rw">
		///            string "r" or "rw" depending on read or read/write.
		/// </param>
		/// <exception cref="java.io.FileNotFoundException">
		///             if open fails.
		/// @noinspection SameParameterValue </exception>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public LERandomAccessFile(String file, String rw) throws java.io.FileNotFoundException
		public LERandomAccessFile(string file, string rw)
		{
			fileName = file;
			raf = new RandomAccessFile(file, rw);
			work = new sbyte[8];
		}

		/// <summary>
		/// Get file name specified in constructor
		/// 
		/// @return
		/// </summary>
		public virtual string FileName
		{
			get
			{
				return this.fileName;
			}
		}

		/// <summary>
		/// close the file.
		/// </summary>
		/// <exception cref="IOException">
		///             if close fails. </exception>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public final void close() throws java.io.IOException
		public void close()
		{
			raf.close();
		}

		/// <summary>
		/// Get file descriptor.
		/// </summary>
		/// <returns> file descriptor (handle to open file) </returns>
		/// <exception cref="IOException">
		///             if get fails. </exception>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public final java.io.FileDescriptor getFD() throws java.io.IOException
		public FileDescriptor FD
		{
			get
			{
				return raf.FD;
			}
		}

		/// <summary>
		/// get position of marker in the file.
		/// </summary>
		/// <returns> offset where we are in the file. </returns>
		/// <exception cref="IOException">
		///             if get fails. </exception>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public long getFilePointer() throws java.io.IOException
		public virtual long FilePointer
		{
			get
			{
				return raf.FilePointer;
			}
		}

		/// <summary>
		/// get length of the file.
		/// </summary>
		/// <returns> length in bytes, note value is a long. </returns>
		/// <exception cref="IOException">
		///             if get fails. </exception>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public final long length() throws java.io.IOException
		public long length()
		{
			return raf.length();
		}

		/// <summary>
		/// ready one unsigned byte.
		/// </summary>
		/// <returns> unsigned byte read. </returns>
		/// <exception cref="IOException">
		///             if read fails. </exception>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public final int read() throws java.io.IOException
		public int read()
		{
			return raf.read();
		}

		/// <summary>
		/// read an array of bytes.
		/// </summary>
		/// <param name="ba">
		///            byte array to accept the bytes.
		/// </param>
		/// <returns> how many bytes actually read. </returns>
		/// <exception cref="IOException">
		///             if read fails. </exception>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public final int read(byte ba[]) throws java.io.IOException
		public int read(sbyte[] ba)
		{
			return raf.read(ba);
		}

		/// <summary>
		/// Read a byte array.
		/// </summary>
		/// <param name="ba">
		///            byte array to accept teh bytes. </param>
		/// <param name="off">
		///            offset into the array to place the bytes, <strong>not</strong>
		///            offset in file. </param>
		/// <param name="len">
		///            how many bytes to read.
		/// </param>
		/// <returns> how many bytes actually read. </returns>
		/// <exception cref="IOException">
		///             if read fails. </exception>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public final int read(byte ba[] , int off, int len) throws java.io.IOException
		public int read(sbyte[] ba, int off, int len)
		{
			return raf.read(ba, off, len);
		}

		/// <summary>
		/// OK, reads only only 1 byte boolean.
		/// </summary>
		/// <returns> true or false. </returns>
		/// <exception cref="IOException">
		///             if read fails. </exception>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public final boolean readBoolean() throws java.io.IOException
		public bool readBoolean()
		{
			return raf.readBoolean();
		}

		/// <summary>
		/// read byte.
		/// </summary>
		/// <returns> byte read. </returns>
		/// <exception cref="IOException">
		///             if read fails. </exception>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public final byte readByte() throws java.io.IOException
		public sbyte readByte()
		{
			return raf.readByte();
		}

		/// <summary>
		/// Read a char. like RandomAccessFile.readChar except little endian.
		/// </summary>
		/// <returns> char read. </returns>
		/// <exception cref="IOException">
		///             if read fails. </exception>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public final char readChar() throws java.io.IOException
		public char readChar()
		{
			raf.readFully(work, 0, 2);
			return (char)((work[1] & 0xff) << 8 | (work[0] & 0xff));
		}

		/// <summary>
		/// read a double. like RandomAccessFile.readDouble except little endian.
		/// </summary>
		/// <returns> the double read. </returns>
		/// <exception cref="IOException">
		///             if read fails. </exception>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public final double readDouble() throws java.io.IOException
		public double readDouble()
		{
			return double.longBitsToDouble(readLong());
		}

		/// <summary>
		/// read a float. like RandomAccessFile.readFloat except little endian.
		/// </summary>
		/// <returns> float read. </returns>
		/// <exception cref="IOException">
		///             if read fails. </exception>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public final float readFloat() throws java.io.IOException
		public float readFloat()
		{
			return float.intBitsToFloat(readInt());
		}

		/// <summary>
		/// Read a full array.
		/// </summary>
		/// <param name="ba">
		///            the array to hold the results.
		/// </param>
		/// <exception cref="IOException">
		///             if read fails. </exception>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public final void readFully(byte ba[]) throws java.io.IOException
		public void readFully(sbyte[] ba)
		{
			raf.readFully(ba, 0, ba.Length);
		}

		/// <summary>
		/// read an array of bytes until the count is satisfied.
		/// </summary>
		/// <param name="ba">
		///            the array to hold the results. </param>
		/// <param name="off">
		///            offset. </param>
		/// <param name="len">
		///            count of bytes to read.
		/// </param>
		/// <exception cref="IOException">
		///             if read fails. </exception>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public final void readFully(byte ba[] , int off, int len) throws java.io.IOException
		public void readFully(sbyte[] ba, int off, int len)
		{
			raf.readFully(ba, off, len);
		}

		/// <summary>
		/// read signed little endian 32-bit int.
		/// </summary>
		/// <returns> signed int </returns>
		/// <exception cref="IOException">
		///             if read fails. </exception>
		/// <seealso cref= java.io.RandomAccessFile#readInt except little endian. </seealso>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public final int readInt() throws java.io.IOException
		public int readInt()
		{
			raf.readFully(work, 0, 4);
			return (work[3]) << 24 | (work[2] & 0xff) << 16 | (work[1] & 0xff) << 8 | (work[0] & 0xff);
		}

		/// <summary>
		/// Read a line.
		/// </summary>
		/// <returns> line read. </returns>
		/// <exception cref="IOException">
		///             if read fails. </exception>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public final String readLine() throws java.io.IOException
		public string readLine()
		{
			return raf.readLine();
		}

		/// <summary>
		/// Read a long, 64 bits.
		/// </summary>
		/// <returns> long read. like RandomAccessFile.readLong except little endian. </returns>
		/// <exception cref="IOException">
		///             if read fails. </exception>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public final long readLong() throws java.io.IOException
		public long readLong()
		{
			raf.readFully(work, 0, 8);
			return (long)(work[7]) << 56 | (long)(work[6] & 0xff) << 48 | (long)(work[5] & 0xff) << 40 | (long)(work[4] & 0xff) << 32 | (long)(work[3] & 0xff) << 24 | (long)(work[2] & 0xff) << 16 | (long)(work[1] & 0xff) << 8 | (long)(work[0] & 0xff);
					/* long cast necessary or shift done modulo 32 */
		}

		/// <summary>
		/// Read a short, 16 bits.
		/// </summary>
		/// <returns> short read. like RandomAccessFile.readShort except little endian. </returns>
		/// <exception cref="IOException">
		///             if read fails. </exception>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public final short readShort() throws java.io.IOException
		public short readShort()
		{
			raf.readFully(work, 0, 2);
			return (short)((work[1] & 0xff) << 8 | (work[0] & 0xff));
		}

		/// <summary>
		/// Read a counted UTF-8 string.
		/// </summary>
		/// <returns> string read. </returns>
		/// <exception cref="IOException">
		///             if read fails. </exception>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public final String readUTF() throws java.io.IOException
		public string readUTF()
		{
			return raf.readUTF();
		}

		/// <summary>
		/// return an unsigned byte. Noote: returns an int, even though says Byte.
		/// </summary>
		/// <returns> the byte read. </returns>
		/// <exception cref="IOException">
		///             if read fails. </exception>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public final int readUnsignedByte() throws java.io.IOException
		public int readUnsignedByte()
		{
			return raf.readUnsignedByte();
		}

		/// <summary>
		/// Read an unsigned short, 16 bits. Like RandomAccessFile.readUnsignedShort
		/// except little endian. Note, returns int even though it reads a short.
		/// </summary>
		/// <returns> little-endian unsigned short, as an int. </returns>
		/// <exception cref="IOException">
		///             if read fails. </exception>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public final int readUnsignedShort() throws java.io.IOException
		public int readUnsignedShort()
		{
			raf.readFully(work, 0, 2);
			return ((work[1] & 0xff) << 8 | (work[0] & 0xff));
		}

		/// <summary>
		/// seek to a place in the file
		/// </summary>
		/// <param name="pos">
		///            0-based offset to seek to.
		/// </param>
		/// <exception cref="IOException">
		///             if read fails.
		/// @noinspection SameParameterValue </exception>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void seek(long pos) throws java.io.IOException
		public virtual void seek(long pos)
		{
			raf.seek(pos);
		}

		/// <summary>
		/// Skip over bytes.
		/// </summary>
		/// <param name="n">
		///            number of bytes to skip over.
		/// </param>
		/// <returns> the actual number of bytes skipped. </returns>
		/// <exception cref="IOException">
		///             if read fails. </exception>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public final int skipBytes(int n) throws java.io.IOException
		public int skipBytes(int n)
		{
			return raf.skipBytes(n);
		}

		/// <summary>
		/// Write a byte. Only writes one byte even though says int.
		/// </summary>
		/// <param name="ib">
		///            byte to write.
		/// </param>
		/// <exception cref="IOException">
		///             if read fails. </exception>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public final synchronized void write(int ib) throws java.io.IOException
		public void write(int ib)
		{
			lock (this)
			{
				raf.write(ib);
			}
		}

		/// <summary>
		/// Write an array of bytes.
		/// </summary>
		/// <param name="ba">
		///            array to write.
		/// </param>
		/// <exception cref="IOException">
		///             if read fails. </exception>
		/// <seealso cref= java.io.DataOutput#write(byte[]) </seealso>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public final void write(byte ba[]) throws java.io.IOException
		public void write(sbyte[] ba)
		{
			raf.write(ba, 0, ba.Length);
		}

		/// <summary>
		/// Write part of an array of bytes.
		/// </summary>
		/// <param name="ba">
		///            array to write. </param>
		/// <param name="off">
		///            offset </param>
		/// <param name="len">
		///            count of bytes to write.
		/// </param>
		/// <exception cref="IOException">
		///             if read fails. </exception>
		/// <seealso cref= java.io.DataOutput#write(byte[], int, int) </seealso>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public final synchronized void write(byte ba[] , int off, int len) throws java.io.IOException
		public void write(sbyte[] ba, int off, int len)
		{
			lock (this)
			{
				raf.write(ba, off, len);
			}
		}

		/// <summary>
		/// write a boolean as one byte.
		/// </summary>
		/// <param name="v">
		///            boolean to write.
		/// </param>
		/// <exception cref="IOException">
		///             if read fails. </exception>
		/// <seealso cref= java.io.DataOutput#writeBoolean(boolean) </seealso>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public final void writeBoolean(boolean v) throws java.io.IOException
		public void writeBoolean(bool v)
		{
			raf.writeBoolean(v);
		}

		/// <summary>
		/// Write a byte. Note param is an int though only a byte is written.
		/// </summary>
		/// <param name="v">
		///            byte to write.
		/// </param>
		/// <exception cref="IOException">
		///             if read fails. </exception>
		/// <seealso cref= java.io.DataOutput#writeByte(int) </seealso>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public final void writeByte(int v) throws java.io.IOException
		public void writeByte(int v)
		{
			raf.writeByte(v);
		}

		/// <summary>
		/// Write bytes from a String.
		/// </summary>
		/// <param name="s">
		///            string source of the bytes.
		/// </param>
		/// <exception cref="IOException">
		///             if read fails. </exception>
		/// <seealso cref= java.io.DataOutput#writeBytes(java.lang.String) </seealso>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public final void writeBytes(String s) throws java.io.IOException
		public void writeBytes(string s)
		{
			raf.writeBytes(s);
		}

		/// <summary>
		/// Write a char. note param is an int though writes a char.
		/// </summary>
		/// <param name="v">
		///            char to write. like RandomAccessFile.writeChar. Note the parm
		///            is an int even though this as a writeChar
		/// </param>
		/// <exception cref="IOException">
		///             if read fails. </exception>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public final void writeChar(int v) throws java.io.IOException
		public void writeChar(int v)
		{
			// same code as writeShort
			work[0] = (sbyte) v;
			work[1] = (sbyte)(v >> 8);
			raf.write(work, 0, 2);
		}

		/// <summary>
		/// Write a string, even though method called writeChars. like
		/// RandomAccessFile.writeChars, has to flip each char.
		/// </summary>
		/// <param name="s">
		///            String to write.
		/// </param>
		/// <exception cref="IOException">
		///             if read fails. </exception>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public final void writeChars(String s) throws java.io.IOException
		public void writeChars(string s)
		{
			int len = s.Length;
			for (int i = 0; i < len; i++)
			{
				writeChar(s[i]);
			}
		} // end writeChars

		/// <summary>
		/// Write a double. Like RandomAccessFile.writeDouble.
		/// </summary>
		/// <param name="v">
		///            double to write.
		/// </param>
		/// <exception cref="IOException">
		///             if read fails. </exception>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public final void writeDouble(double v) throws java.io.IOException
		public void writeDouble(double v)
		{
			writeLong(double.doubleToLongBits(v));
		}

		/// <summary>
		/// Write a float. Like RandomAccessFile.writeFloat.
		/// </summary>
		/// <param name="v">
		///            float to write.
		/// </param>
		/// <exception cref="java.io.IOException">
		///             if read fails. </exception>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public final void writeFloat(float v) throws java.io.IOException
		public void writeFloat(float v)
		{
			writeInt(float.floatToIntBits(v));
		}

		/// <summary>
		/// write an int, 32-bits. Like RandomAccessFile.writeInt.
		/// </summary>
		/// <param name="v">
		///            int to write.
		/// </param>
		/// <exception cref="IOException">
		///             if read fails. </exception>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public final void writeInt(int v) throws java.io.IOException
		public void writeInt(int v)
		{
			work[0] = (sbyte) v;
			work[1] = (sbyte)(v >> 8);
			work[2] = (sbyte)(v >> 16);
			work[3] = (sbyte)(v >> 24);
			raf.write(work, 0, 4);
		}

		/// <summary>
		/// Write i long, 64 bits. Like java.io.RandomAccessFile.writeLong.
		/// </summary>
		/// <param name="v">
		///            long write.
		/// </param>
		/// <exception cref="IOException">
		///             if read fails. </exception>
		/// <seealso cref= java.io.RandomAccessFile#writeLong </seealso>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public final void writeLong(long v) throws java.io.IOException
		public void writeLong(long v)
		{
			work[0] = (sbyte) v;
			work[1] = (sbyte)(v >> 8);
			work[2] = (sbyte)(v >> 16);
			work[3] = (sbyte)(v >> 24);
			work[4] = (sbyte)(v >> 32);
			work[5] = (sbyte)(v >> 40);
			work[6] = (sbyte)(v >> 48);
			work[7] = (sbyte)(v >> 56);
			raf.write(work, 0, 8);
		}

		/// <summary>
		/// Write an signed short even though parameter is an int. Like
		/// java.io.RandomAccessFile#writeShort. also acts as a writeUnsignedShort.
		/// </summary>
		/// <param name="v">
		///            signed number to write
		/// </param>
		/// <exception cref="IOException">
		///             if read fails. </exception>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public final void writeShort(int v) throws java.io.IOException
		public void writeShort(int v)
		{
			work[0] = (sbyte) v;
			work[1] = (sbyte)(v >> 8);
			raf.write(work, 0, 2);
		}

		/// <summary>
		/// Write a counted UTF string.
		/// </summary>
		/// <param name="s">
		///            String to write.
		/// </param>
		/// <exception cref="IOException">
		///             if read fails. </exception>
		/// <seealso cref= java.io.DataOutput#writeUTF(java.lang.String) </seealso>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public final void writeUTF(String s) throws java.io.IOException
		public void writeUTF(string s)
		{
			raf.writeUTF(s);
		}

		/// <seealso cref= Object#toString() </seealso>
		public override string ToString()
		{
			StringBuilder builder = new StringBuilder();
			builder.Append("LERandomAccessFile [raf=").Append(raf).Append(", work=").Append(Arrays.ToString(work)).Append(", fileName=").Append(fileName).Append(", getFilePointer()=");

			try
			{
				builder.Append(FilePointer);
			}
			catch (IOException e)
			{
				Console.WriteLine(e.ToString());
				Console.Write(e.StackTrace);
			}

			builder.Append("]");
			return builder.ToString();
		}
	}

}