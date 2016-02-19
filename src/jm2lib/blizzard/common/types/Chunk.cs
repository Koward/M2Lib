using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Text;

namespace jm2lib.blizzard.common.types
{


	using Marshalable = jm2lib.io.Marshalable;
	using MarshalingStream = jm2lib.io.MarshalingStream;
	using UnmarshalingStream = jm2lib.io.UnmarshalingStream;

	/// <summary>
	/// Represents a Blizzard IFF Chunk which is a {@code BlizzardFile} with a size (total, in bytes) field.
	/// Implementers must specify the class of the content in the super(Class) constructor.
	/// 
	/// @author Koward
	/// </summary>
	public class Chunk<T> : List<T>, Marshalable
	{
		private const long serialVersionUID = -4836096267381071184L;
		/// <summary>
		/// The type of the content </summary>
		private readonly Type type;
		private readonly string magic;

		public Chunk(Type type, string magic) : base()
		{
			this.type = type;
			Debug.Assert(magic.Length == 4);
			this.magic = magic;
		}

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @SuppressWarnings("unchecked") @Override public final void unmarshal(jm2lib.io.UnmarshalingStream in) throws java.io.IOException, ClassNotFoundException
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
		public void unmarshal(UnmarshalingStream @in)
		{
			int size = @in.readInt();
			long endOffset = @in.FilePointer + size;
			while (@in.FilePointer < endOffset)
			{
				if (type.IsSubclassOf(typeof(Marshalable)))
				{
					if (type.DeclaredConstructors[0].ParameterCount > 0)
					{
						throw new InvalidClassException("\nNo 0-Arg constructor found for " + type + ".\nIf nested class, make sure to declare it static.");
					}
				}
				this.Add((T) @in.readGeneric(type));
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public final void marshal(jm2lib.io.MarshalingStream out) throws java.io.IOException
		public void marshal(MarshalingStream @out)
		{
			@out.write(magic.GetBytes(StandardCharsets.UTF_8));
			long sizeOffset = @out.FilePointer;
			@out.writeInt(0);
			for (int i = 0; i < this.Count; i++)
			{
				@out.writeGeneric(type, this[i]);
			}
			int size = (int)(@out.FilePointer - sizeOffset);
			long currentOffset = @out.FilePointer;
			@out.seek(sizeOffset);
			@out.writeInt(size);
			@out.seek(currentOffset);
		}

		public override string ToString()
		{
			StringBuilder builder = new StringBuilder();
			for (int i = 0; i < this.Count; i++)
			{
				builder.Append("\n\t [" + i + "] " + this[i]);
			}
			return builder.ToString();
		}
	}

}