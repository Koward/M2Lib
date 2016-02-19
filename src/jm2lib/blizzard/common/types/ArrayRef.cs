using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Text;

namespace jm2lib.blizzard.common.types
{


	using LERandomAccessFile = com.mindprod.ledatastream.LERandomAccessFile;

	using Referencer = jm2lib.blizzard.common.interfaces.Referencer;
	using AnimFilesHandler = jm2lib.blizzard.wow.common.AnimFilesHandler;
	using Marshalable = jm2lib.io.Marshalable;
	using MarshalingStream = jm2lib.io.MarshalingStream;
	using UnmarshalingStream = jm2lib.io.UnmarshalingStream;

	/// <summary>
	/// Generic data type to handle number&offset pairs and their unmarshaling. Can lead
	/// to primitive types and to another BlizzardFile. The class it is leading to
	/// must be given in argument. When leading to another ArrayRef, the inside class
	/// must be also be given in argument (type erasure Java language limit).
	/// 
	/// @author Koward </summary>
	/// @param <T> </param>
	/// <seealso cref= <a href=
	///      "http://www.pxr.dk/wowdev/wiki/index.php?title=Talk:M2/WotLK#On_animations">
	///      M2 ArrayRef specification</a> </seealso>
	public class ArrayRef<T> : List<T>, Referencer
	{
		private const long serialVersionUID = -6755334429400482754L;
		private Type type;
		private Type subType;
		private long startOfs; //Where the ArrayRef is
		private bool shouldWriteContent;
		private LERandomAccessFile animFile;
		private LERandomAccessFile[] animFiles;
		private int ofs; //Where the ArrayRef points

		public ArrayRef(Type type) : base()
		{
			this.type = (Type) type;
			if (type == typeof(ArrayRef))
			{
				Console.Error.WriteLine("WARNING : ArrayRef subType not specified");
			}
			this.subType = null;
			shouldWriteContent = false;
			this.ofs = 0;
		}

		/// <param name="type"> </param>
		/// <param name="subType"> when leading to another ArrayRef </param>
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @SuppressWarnings({ "rawtypes", "unchecked" }) public ArrayRef(Class type, Class subType)
		public ArrayRef(Type type, Type subType) : base()
		{
			this.type = (Type) type;
			if (type == typeof(ArrayRef) && subType == null)
			{
				Console.Error.WriteLine("WARNING : ArrayRef subType not specified");
			}
			this.subType = subType;
			shouldWriteContent = false;
			this.ofs = 0;
		}

		/// <summary>
		/// Converts a String into null-terminated UTF-8 bytes ArrayRef. </summary>
		/// <param name="str"> </param>
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @SuppressWarnings("unchecked") public ArrayRef(String str)
		public ArrayRef(string str) : this((Type) sbyte.TYPE)
		{
			sbyte[] array = Arrays.copyOf(str.GetBytes(StandardCharsets.UTF_8), str.Length + 1); //Converts to byte[] and add a 0
			foreach (sbyte character in array)
			{
				this.Add((T)(sbyte?) character);
			}
		}

		public virtual Type Type
		{
			get
			{
				return type;
			}
		}
		public virtual Type SubType
		{
			get
			{
				return subType;
			}
		}

		/// <summary>
		/// Used to give a reference to the .anim files. At this
		/// stage the ArrayRef doesn't know which file its children are
		/// going to use them. </summary>
		/// <param name="animFiles"> the elements can be either UnmarshalingStreams or MarshalingStreams. 
		/// TODO Do proper in&out methods so one can change the underlying stream without changing ArrayRef code. </param>
		public virtual LERandomAccessFile[] AnimFiles
		{
			set
			{
				this.animFiles = value;
			}
		}

		/// <summary>
		/// Used when the ArrayRef needs to read&write its content in a .anim file
		/// </summary>
		private LERandomAccessFile AnimFile
		{
			set
			{
				Debug.Assert(type == typeof(ArrayRef));
				this.animFile = value;
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public final void unmarshal(jm2lib.io.UnmarshalingStream in) throws java.io.IOException, ClassNotFoundException
		public void unmarshal(UnmarshalingStream @in)
		{
			int n = @in.readInt();
			ofs = @in.readInt();
			if (n < 0 || ofs < 0)
			{
				Console.Error.WriteLine("Error : Tried to read " + n + "elements at offset " + ofs);
			}
			try
			{
				readContent(@in, n);
			}
//JAVA TO C# CONVERTER TODO TASK: There is no equivalent in C# to Java 'multi-catch' syntax:
			catch (InstantiationException | IllegalAccessException e)
			{
				throw new InvalidClassException("Could not read ArrayRef content");
			}
		}

		/// <summary>
		/// Extract read n times the referenced content.
		/// </summary>
		/// <param name="in"> </param>
		/// <param name="n"> the number of elements. </param>
		/// <exception cref="IOException"> </exception>
		/// <exception cref="InstantiationException"> </exception>
		/// <exception cref="IllegalAccessException"> </exception>
		/// <exception cref="ClassNotFoundException"> </exception>
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @SuppressWarnings("unchecked") private void readContent(jm2lib.io.UnmarshalingStream in, int n) throws java.io.IOException, InstantiationException, IllegalAccessException, ClassNotFoundException
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
		private void readContent(UnmarshalingStream @in, int n)
		{
			if (n == 0)
			{
				return;
			}

			long currentOfs = @in.FilePointer;

			// Switch stream to .anim file
			UnmarshalingStream mainStream = @in;
			if (animFile != null)
			{
				@in = (UnmarshalingStream) animFile;
			}
			@in.seek(ofs);
			for (int i = 0; i < n; i++)
			{
				if (type == typeof(ArrayRef))
				{
					this.Add((T) new ArrayRef<>(subType));
//JAVA TO C# CONVERTER TODO TASK: Java wildcard generics are not converted to .NET:
//ORIGINAL LINE: ((ArrayRef<?>) get(i)).setAnimFile(animFiles[i]);
					((ArrayRef<?>) this[i]).AnimFile = animFiles[i];
//JAVA TO C# CONVERTER TODO TASK: Java wildcard generics are not converted to .NET:
//ORIGINAL LINE: ((ArrayRef<?>) get(i)).unmarshal(in);
					((ArrayRef<?>) this[i]).unmarshal(@in);
				}
				else if (type.IsSubclassOf(typeof(AnimFilesHandler)))
				{
					this.Add((T) type.newInstance());
					((AnimFilesHandler) this[i]).AnimFiles = animFiles;
					((Marshalable) this[i]).unmarshal(@in);
				}
				else
				{
					this.Add((T) @in.readGeneric(type));
				}
			}
			// Switch back to main stream
			if (animFile != null)
			{
				@in = mainStream;
			}
			@in.seek(currentOfs);
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public final void marshal(jm2lib.io.MarshalingStream out) throws java.io.IOException
		public void marshal(MarshalingStream @out)
		{
			startOfs = @out.FilePointer;
			@out.writeInt(this.Count);
			@out.writeInt(ofs);
			shouldWriteContent = true;
		}

		/// <summary>
		/// Write the content pointed by ArrayRef. 
		/// This is not part of marshal() because the content is written AFTER the marshalling of the whole structure.
		/// Updates itself the offset. </summary>
		/// <param name="out"> </param>
		/// <exception cref="IOException"> </exception>
		/// <exception cref="IllegalAccessException"> </exception>
		/// <exception cref="InstantiationException"> </exception>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void writeContent(jm2lib.io.MarshalingStream out) throws java.io.IOException, InstantiationException, IllegalAccessException
		public virtual void writeContent(MarshalingStream @out)
		{
			if (shouldWriteContent == false)
			{
				throw new IOException("ArrayRef<" + type + "> was not marshal() before writing content.\n" + "It needs it in order to update its n&ofs so the data can be found in the file again.");
			}
			if (this.Count == 0)
			{
				return;
			}

			// Switch stream to .anim file
			MarshalingStream mainStream = @out;
			if (animFile != null)
			{
				@out = (MarshalingStream) animFile;
			}

			ofs = (int) @out.FilePointer;
			for (int i = 0; i < this.Count; i++)
			{
				if (type == typeof(ArrayRef))
				{
//JAVA TO C# CONVERTER TODO TASK: Java wildcard generics are not converted to .NET:
//ORIGINAL LINE: ((ArrayRef<?>) get(i)).setAnimFile(animFiles[i]);
					((ArrayRef<?>) this[i]).AnimFile = animFiles[i];
//JAVA TO C# CONVERTER TODO TASK: Java wildcard generics are not converted to .NET:
//ORIGINAL LINE: ((ArrayRef<?>) get(i)).marshal(out);
					((ArrayRef<?>) this[i]).marshal(@out);
				}
				else if (type.IsSubclassOf(typeof(AnimFilesHandler)))
				{
					((AnimFilesHandler) this[i]).AnimFiles = animFiles;
					((AnimFilesHandler) this[i]).marshal(@out);
				}
				else
				{
					@out.writeGeneric(type, this[i]);
				}
			}
			if (type.IsSubclassOf(typeof(Referencer)))
			{
				for (int i = 0; i < this.Count; i++)
				{
					((Referencer) this[i]).writeContent(@out);
				}
			}
			// Switch back to main stream
			if (animFile != null)
			{
				@out = mainStream;
			}

			// Rewrites itself with updated offset
			long currentOfs = @out.FilePointer;
			@out.seek(startOfs);
			this.marshal(@out);
			shouldWriteContent = false;
			@out.seek(currentOfs);
		}

		/// <summary>
		/// Build a human-readable String from the characters stored. </summary>
		/// <returns> a readable string. </returns>
		public virtual string toNameString()
		{
			if (this.Count == 0)
			{
				return "";
			}
			if (type == sbyte.TYPE)
			{
				sbyte[] array = new sbyte[this.Count];
				for (int i = 0; i < this.Count; i++)
				{
					array[i] = (sbyte) this[i];
				}
				return (StringHelperClass.NewString(array, StandardCharsets.UTF_8)).Trim();
			}
			if (type == char.TYPE)
			{
				char[] array = new char[this.Count];
				for (int i = 0; i < this.Count; i++)
				{
					array[i] = (char) this[i];
				}
				return (new string(array)).Trim();
			}
			else
			{
				throw new System.NotSupportedException("Can't print an ArrayRef<" + type + "> as a readable String");
			}
		}

		public override string ToString()
		{
			StringBuilder result = new StringBuilder();
			string NEW_LINE = System.getProperty("line.separator");
			result.Append("[number: " + this.Count + "]");
			if (this.Count != 0)
			{
				if (!(type.IsPrimitive || type.IsSubclassOf(typeof(BlizzardVector))))
				{
					result.Append(NEW_LINE);
				}
				for (int i = 0; i < this.Count; i++)
				{
					if (!(type.IsPrimitive || type.IsSubclassOf(typeof(BlizzardVector))))
					{
						result.Append("[" + i + "] ");
					}
					else
					{
						result.Append(NEW_LINE);
					}
					if (type == char.TYPE)
					{
						result.Append((char)(int)((char?) this[i]));
					}
					else
					{
						result.Append(this[i]);
					}
				}
				result.Append(NEW_LINE);
			}
			return result.ToString();
		}

		/// <summary>
		/// Adds new instance in the list. </summary>
		/// <param name="hint"> This parameter changes the default value of the instance.
		/// 0 = default
		/// 1 often equals non-0 default values :
		///    Vec3F : scaling (1,1,1)
		///    Byte : enabled 1
		///    Short : alpha 0x7FFF
		///    QuatS : rotation (32767, 32767, 32767, -1) </param>
		/// <exception cref="InstantiationException"> </exception>
		/// <exception cref="IllegalAccessException"> </exception>
		/// <exception cref="InvalidClassException"> </exception>
		/// <exception cref="SecurityException"> </exception>
		/// <exception cref="NoSuchMethodException"> </exception>
		/// <exception cref="InvocationTargetException"> </exception>
		/// <exception cref="IllegalArgumentException">  </exception>
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @SuppressWarnings("unchecked") public void addNew(int hint) throws InstantiationException, IllegalAccessException, java.io.InvalidClassException
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
		public virtual void addNew(int hint)
		{
			if (hint == 1)
			{
				if (type == typeof(Vec3F))
				{
					this.Add((T) new Vec3F(1.0F, 1.0F, 1.0F));
				}
				else if (type == typeof(QuatS))
				{
					this.Add((T) new QuatS((short) 32767,(short) 32767,(short) 32767,(short) -1));
				}
				else if (type == sbyte.TYPE)
				{
					this.Add((T) new sbyte?((sbyte) 1));
				}
				else if (type == short.TYPE)
				{
					this.Add((T) new short?((short) 32767));
				}
				else
				{
					throw new InvalidClassException("Hint 1 unsupported for class " + type);
				}
			}
			else if (hint == 0)
			{
				if (type.IsSubclassOf(typeof(Marshalable)))
				{
					this.Add((T) type.newInstance());
				}
				else if (type == int.TYPE)
				{
					this.Add((T) new int?(0));
				}
				else if (type == float.TYPE)
				{
					this.Add((T) new float?(0));
				}
				else if (type == short.TYPE)
				{
					this.Add((T) new short?((short) 0));
				}
				else if (type == long.TYPE)
				{
					this.Add((T) new long?(0));
				}
				else if (type == sbyte.TYPE)
				{
					this.Add((T) new sbyte?((sbyte) 0));
				}
				else if (type == char.TYPE)
				{
					this.Add((T) new char?((char) 0));
				}
				else
				{
					throw new InvalidClassException("Hint 0 unsupported for class " + type);
				}
			}
			else
			{
				Console.Error.WriteLine("Hint value " + hint + " not recognized");
				Environment.Exit(1);
			}
		}
	}

}