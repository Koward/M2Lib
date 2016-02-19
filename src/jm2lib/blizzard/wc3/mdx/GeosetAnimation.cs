namespace jm2lib.blizzard.wc3.mdx
{

	using Vec3F = jm2lib.blizzard.common.types.Vec3F;
	using SubmeshAnimation = jm2lib.blizzard.wow.classic.SubmeshAnimation;
	using Marshalable = jm2lib.io.Marshalable;
	using MarshalingStream = jm2lib.io.MarshalingStream;
	using UnmarshalingStream = jm2lib.io.UnmarshalingStream;

	public class GeosetAnimation : Marshalable
	{
		internal float alpha;
		internal int flags;
		internal Vec3F color;
		internal int geosetID;
		internal Track<float?> alphaTrack;
		internal Track<Vec3F> colorTrack;

		public GeosetAnimation()
		{
			color = new Vec3F();
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void unmarshal(jm2lib.io.UnmarshalingStream in) throws java.io.IOException, ClassNotFoundException
		public virtual void unmarshal(UnmarshalingStream @in)
		{
			long endOffset = @in.FilePointer + @in.readInt();
			alpha = @in.readFloat();
			flags = @in.readInt();
			color.unmarshal(@in);
			geosetID = @in.readInt();
			while (@in.FilePointer < endOffset)
			{
				string magic = StringHelperClass.NewString(new sbyte[] {@in.readByte(), @in.readByte(), @in.readByte(), @in.readByte()});
				switch (magic)
				{
				case "KGAO":
					alphaTrack = new Track<>(float.TYPE, magic);
					alphaTrack.unmarshal(@in);
					break;
				case "KGAC":
					colorTrack = new Track<>(typeof(Vec3F), magic);
					colorTrack.unmarshal(@in);
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
			@out.writeFloat(alpha);
			@out.writeInt(flags);
			color.marshal(@out);
			@out.writeInt(geosetID);
			if (alphaTrack != null)
			{
				alphaTrack.marshal(@out);
			}
			if (colorTrack != null)
			{
				colorTrack.marshal(@out);
			}

			int size = (int)(@out.FilePointer - sizeOffset);
			long currentOffset = @out.FilePointer;
			@out.seek(sizeOffset);
			@out.writeInt(size);
			@out.seek(currentOffset);
		}

		public virtual SubmeshAnimation upConvert()
		{
			SubmeshAnimation output = new SubmeshAnimation();
			if (colorTrack != null)
			{
				output.color = colorTrack.upConvert();
			}
			if (alphaTrack != null)
			{
				output.alpha.timestamps.AddRange(alphaTrack.Timestamps);
				foreach (float alpha in alphaTrack.Values)
				{
					output.alpha.values.Add((short)(alpha * short.MaxValue));
				}
			}
			return output;
		}

	}

}