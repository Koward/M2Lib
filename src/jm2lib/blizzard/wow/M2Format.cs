using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace jm2lib.blizzard.wow
{


	using CSVFormat = org.apache.commons.csv.CSVFormat;
	using CSVParser = org.apache.commons.csv.CSVParser;
	using CSVRecord = org.apache.commons.csv.CSVRecord;

	using LERandomAccessFile = com.mindprod.ledatastream.LERandomAccessFile;

	using Marshalable = jm2lib.io.Marshalable;

	public abstract class M2Format : Marshalable, ICloneable
	{
		public abstract void marshal(jm2lib.io.MarshalingStream @out);
		public abstract void unmarshal(jm2lib.io.UnmarshalingStream @in);
		public int version;
		public const int CLASSIC = 256;
		public const int BURNING_CRUSADE = 260;
		public const int LICH_KING = 264;
		public const int CATACLYSM = 272;
		public const int PANDARIA = 272;
		public const int DRAENOR = 272;
		public const int LEGION = 274;

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public M2Format downConvert() throws Exception
		public virtual M2Format downConvert()
		{
			return null;
		}
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public M2Format upConvert() throws Exception
		public virtual M2Format upConvert()
		{
			return null;
		}

		/// <summary>
		/// Open a CSV file. </summary>
		/// <param name="name"> relative to jm2lib/blizzard/wow/common. </param>
		/// <returns> list of records. </returns>
		/// <exception cref="IOException"> if problem during CSV reading. </exception>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public static java.util.List<org.apache.commons.csv.CSVRecord> openCSV(String name) throws java.io.IOException
		public static IList<CSVRecord> openCSV(string name)
		{
			StringBuilder pathBuilder = new StringBuilder();
			pathBuilder.Append("jm2lib/blizzard/wow/common/");
			pathBuilder.Append(name);
			pathBuilder.Append(".dbc.csv");
			string path = pathBuilder.ToString();
			ClassLoader classLoader = Thread.CurrentThread.ContextClassLoader;
			InputStream input = classLoader.getResourceAsStream(path);
			InputStreamReader reader = new InputStreamReader(input);
			CSVParser csvParser = new CSVParser(reader, CSVFormat.DEFAULT);
			IList<CSVRecord> lines = csvParser.Records;
			reader.close();
			csvParser.close();
			return lines;
		}

		/// <summary>
		/// Check position after writing and fill with zeros
		/// </summary>
		/// <param name="file"> </param>
		/// <returns> the number of zeros written </returns>
		/// <exception cref="IOException"> </exception>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public static int align(com.mindprod.ledatastream.LERandomAccessFile out) throws java.io.IOException
		public static int align(LERandomAccessFile @out)
		{
			long pos = @out.FilePointer;
			int count = (int)(16 - (pos & 0xF));
			sbyte[] zeros = new sbyte[count];
			@out.write(zeros);
			return count;
		}

		/// <summary>
		/// Remove the extension in a file path. Works even with multiple '.' in
		/// path.
		/// 
		/// @author coobird </summary>
		/// <param name="s">
		/// @return </param>
		public static string removeExtension(string s)
		{
			// String separator = System.getProperty("file.separator");
			string filename;
			// Remove the path up to the filename.
			/*
			  int lastSeparatorIndex = s.lastIndexOf(separator); if
			  (lastSeparatorIndex == -1) { filename = s; } else { filename =
			  s.substring(lastSeparatorIndex + 1); }
			 */
			filename = s;
			int extensionIndex = filename.LastIndexOf(".", StringComparison.Ordinal);
			if (extensionIndex == -1)
			{
				return filename;
			}
			return filename.Substring(0, extensionIndex);
		}

		/// <summary>
		/// Get .skin file path.
		/// </summary>
		/// <param name="str">
		///            M2 path. </param>
		/// <param name="i">
		///            View Index. </param>
		/// <returns> final path. </returns>
		public static string getSkinName(string str, int i)
		{
			return removeExtension(str) + "0" + i + ".skin";
		}

		/// <summary>
		/// Get .anim file path
		/// </summary>
		/// <param name="modelPath"> </param>
		/// <param name="animationID"> </param>
		/// <param name="subAnimationID"> </param>
		/// <returns> final path. </returns>
		public static string getAnimFileName(string modelPath, short animationID, short subAnimationID)
		{
			StringBuilder sb = new StringBuilder();
			Formatter formatter = new Formatter(sb);
			formatter.format("%s%04d-%02d.anim", removeExtension(modelPath), animationID, subAnimationID);
			formatter.close();
			return sb.ToString();
		}

		/// <summary>
		/// Close an array of files. </summary>
		/// <param name="needPadding"> </param>
		/// <param name="files"> </param>
		/// <exception cref="IOException"> </exception>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public static void closeFiles(boolean needPadding, com.mindprod.ledatastream.LERandomAccessFile[] files) throws java.io.IOException
		public static void closeFiles(bool needPadding, LERandomAccessFile[] files)
		{
			for (int i = 0; i < files.Length; i++)
			{
				if (files[i] != null)
				{
					if (needPadding)
					{
						align(files[i]);
					}
					files[i].close();
				}
			}
		}

	}

}