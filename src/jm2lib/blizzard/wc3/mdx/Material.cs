using System.Collections.Generic;
using System.Text;

namespace jm2lib.blizzard.wc3.mdx
{


	using Marshalable = jm2lib.io.Marshalable;
	using MarshalingStream = jm2lib.io.MarshalingStream;
	using UnmarshalingStream = jm2lib.io.UnmarshalingStream;

	public class Material : Marshalable
	{
		public int priorityPlane;
		public int flags;
		internal List<Layer> layers;

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void unmarshal(jm2lib.io.UnmarshalingStream in) throws java.io.IOException, ClassNotFoundException
		public virtual void unmarshal(UnmarshalingStream @in)
		{
			long endOffset = @in.FilePointer + @in.readInt();
			priorityPlane = @in.readInt();
			flags = @in.readInt();
			if (@in.FilePointer < endOffset)
			{
				@in.readInt();
				layers = new List<Layer>();
				int layersCount = @in.readInt();
				for (int i = 0; i < layersCount; i++)
				{
					layers.Add(new Layer());
					layers[i].unmarshal(@in);
				}
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void marshal(jm2lib.io.MarshalingStream out) throws java.io.IOException
		public virtual void marshal(MarshalingStream @out)
		{
			long sizeOffset = @out.FilePointer;
			@out.writeInt(0);
			@out.writeInt(priorityPlane);
			@out.writeInt(flags);
			if (layers != null)
			{
				@out.write("LAYS".GetBytes(StandardCharsets.UTF_8));
				@out.writeInt(layers.Count);
				foreach (Layer layer in layers)
				{
					layer.marshal(@out);
				}
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
			builder.Append(this.GetType().FullName).Append(" {\n\tpriorityPlane: ").Append(priorityPlane).Append("\n\tflags: ").Append(flags).Append("\n\tlayers: ").Append(layers).Append("\n}");
			return builder.ToString();
		}

		public class Layer : Marshalable
		{
			public int filterMode;
			public int shadingFlags;
			public int textureID;
			public int textureAnimationID;
			public int coordID;
			public float alpha;
			public Track<int?> materialTextureID;
			public Track<float?> materialTextureAlpha;

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void unmarshal(jm2lib.io.UnmarshalingStream in) throws java.io.IOException, ClassNotFoundException
			public virtual void unmarshal(UnmarshalingStream @in)
			{
				long endOffset = @in.FilePointer + @in.readInt();
				filterMode = @in.readInt();
				shadingFlags = @in.readInt();
				textureID = @in.readInt();
				textureAnimationID = @in.readInt();
				coordID = @in.readInt();
				alpha = @in.readFloat();
				while (@in.FilePointer < endOffset)
				{
					string magic = @in.readString(4);
					switch (magic)
					{
					case("KMTF"):
						materialTextureID = new Track<>(int.TYPE, magic);
						materialTextureID.unmarshal(@in);
						break;
					case("KMTA"):
						materialTextureAlpha = new Track<>(float.TYPE, magic);
						materialTextureAlpha.unmarshal(@in);
						break;
					default:
						throw new ClassNotFoundException(magic + " at " + @in.FilePointer + " is not referenced in the code.");
					}
				}
			}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void marshal(jm2lib.io.MarshalingStream out) throws java.io.IOException
			public virtual void marshal(MarshalingStream @out)
			{
				long sizeOffset = @out.FilePointer;
				@out.writeInt(0);
				@out.writeInt(filterMode);
				@out.writeInt(shadingFlags);
				@out.writeInt(textureID);
				@out.writeInt(textureAnimationID);
				@out.writeInt(coordID);
				@out.writeFloat(alpha);
				if (materialTextureID != null)
				{
					materialTextureID.marshal(@out);
				}
				if (materialTextureAlpha != null)
				{
					materialTextureAlpha.marshal(@out);
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
				builder.Append(this.GetType().FullName).Append(" {\n\tfilterMode: ").Append(filterMode).Append("\n\tshadingFlags: ").Append(shadingFlags).Append("\n\ttextureID: ").Append(textureID).Append("\n\ttextureAnimationID: ").Append(textureAnimationID).Append("\n\tcoordID: ").Append(coordID).Append("\n\talpha: ").Append(alpha).Append("\n\tmaterialTextureID: ").Append(materialTextureID).Append("\n\tmaterialTextureAlpha: ").Append(materialTextureAlpha).Append("\n}");
				return builder.ToString();
			}
		}
	}

}