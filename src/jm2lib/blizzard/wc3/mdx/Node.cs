using System.Text;

namespace jm2lib.blizzard.wc3.mdx
{

	using QuatF = jm2lib.blizzard.common.types.QuatF;
	using Vec3F = jm2lib.blizzard.common.types.Vec3F;
	using Marshalable = jm2lib.io.Marshalable;
	using MarshalingStream = jm2lib.io.MarshalingStream;
	using UnmarshalingStream = jm2lib.io.UnmarshalingStream;

	public class Node : Marshalable
	{
		public string name;
		public int objectID;
		public int parentID;
		public int flags;
		public Track<Vec3F> translation;
		public Track<QuatF> rotation;
		public Track<Vec3F> scale;

		public Node()
		{
			name = "";
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void unmarshal(jm2lib.io.UnmarshalingStream in) throws java.io.IOException, ClassNotFoundException
		public virtual void unmarshal(UnmarshalingStream @in)
		{
			long endOffset = @in.FilePointer + @in.readInt();
			name = @in.readString(80);
			objectID = @in.readInt();
			parentID = @in.readInt();
			flags = @in.readInt();
			while (@in.FilePointer < endOffset)
			{
				string magic = StringHelperClass.NewString(new sbyte[] {@in.readByte(), @in.readByte(), @in.readByte(), @in.readByte()});
				switch (magic)
				{
				case "KGTR":
					translation = new Track<>(typeof(Vec3F), magic);
					translation.unmarshal(@in);
					break;
				case "KGRT":
					rotation = new Track<>(typeof(QuatF), magic);
					rotation.unmarshal(@in);
					break;
				case "KGSC":
					scale = new Track<>(typeof(Vec3F), magic);
					scale.unmarshal(@in);
					break;
				default:
					throw new ClassNotFoundException(magic + "not handled in " + this.GetType());
				}
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void marshal(jm2lib.io.MarshalingStream out) throws java.io.IOException
		public virtual void marshal(MarshalingStream @out)
		{
			long sizeOffset = @out.FilePointer;
			@out.writeInt(0);
			@out.writeString(name, 80);
			@out.writeInt(objectID);
			@out.writeInt(parentID);
			@out.writeInt(flags);
			if (translation != null)
			{
				translation.marshal(@out);
			}
			if (rotation != null)
			{
				rotation.marshal(@out);
			}
			if (scale != null)
			{
				scale.marshal(@out);
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
//JAVA TO C# CONVERTER WARNING: The .NET Type.FullName property will not always yield results identical to the Java Class.getName method:
			builder.Append(this.GetType().FullName).Append(" {\n\tname: ").Append(name).Append("\n\tobjectID: ").Append(objectID).Append("\n\tparentID: ").Append(parentID).Append("\n\tflags: ").Append(flags).Append("\n\ttranslation: ").Append(translation).Append("\n\trotation: ").Append(rotation).Append("\n\tscale: ").Append(scale).Append("\n}");
			return builder.ToString();
		}
	}

}