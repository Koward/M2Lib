namespace jm2lib.blizzard.wc3.mdx
{

	using QuatF = jm2lib.blizzard.common.types.QuatF;
	using Vec3F = jm2lib.blizzard.common.types.Vec3F;
	using UVAnimation = jm2lib.blizzard.wow.classic.UVAnimation;
	using Marshalable = jm2lib.io.Marshalable;
	using MarshalingStream = jm2lib.io.MarshalingStream;
	using UnmarshalingStream = jm2lib.io.UnmarshalingStream;

	public class TextureAnimation : Marshalable
	{
		public Track<Vec3F> translation;
		public Track<QuatF> rotation;
		public Track<Vec3F> scale;

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void unmarshal(jm2lib.io.UnmarshalingStream in) throws java.io.IOException, ClassNotFoundException
		public virtual void unmarshal(UnmarshalingStream @in)
		{
			long endOffset = @in.FilePointer + @in.readInt();
			while (@in.FilePointer < endOffset)
			{
				string magic = StringHelperClass.NewString(new sbyte[] {@in.readByte(), @in.readByte(), @in.readByte(), @in.readByte()});
				switch (magic)
				{
				case "KTAT":
					translation = new Track<>(typeof(Vec3F), magic);
					translation.unmarshal(@in);
					break;
				case "KTAR":
					rotation = new Track<>(typeof(QuatF), magic);
					rotation.unmarshal(@in);
					break;
				case "KTAS":
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

		public virtual UVAnimation upConvert()
		{
			UVAnimation output = new UVAnimation();
			return output;
		}
	}
}