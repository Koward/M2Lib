namespace jm2lib.blizzard.wc3.mdx
{

	using Vec3F = jm2lib.blizzard.common.types.Vec3F;
	using Marshalable = jm2lib.io.Marshalable;
	using MarshalingStream = jm2lib.io.MarshalingStream;
	using UnmarshalingStream = jm2lib.io.UnmarshalingStream;

	public class Light : Marshalable
	{
		public Node node;
		public int type;
		public int attenuationStart;
		public int attenuationEnd;
		public Vec3F color;
		public float intensity;
		public Vec3F ambientColor;
		public float ambientIntensity;

		public Track<int?> attenuationStartTrack;
		public Track<int?> attenuationEndTrack;
		public Track<Vec3F> colorTrack;
		public Track<float?> intensityTrack;
		public Track<float?> ambientIntensityTrack;
		public Track<Vec3F> ambientColorTrack;
		public Track<float?> visibility;

		public Light()
		{
			node = new Node();
			color = new Vec3F();
			ambientColor = new Vec3F();
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void unmarshal(jm2lib.io.UnmarshalingStream in) throws java.io.IOException, ClassNotFoundException
		public virtual void unmarshal(UnmarshalingStream @in)
		{
			long endOffset = @in.FilePointer + @in.readInt();
			node.unmarshal(@in);
			type = @in.readInt();
			attenuationStart = @in.readInt();
			attenuationEnd = @in.readInt();
			color.unmarshal(@in);
			intensity = @in.readFloat();
			ambientColor.unmarshal(@in);
			ambientIntensity = @in.readFloat();

			while (@in.FilePointer < endOffset)
			{
				string magic = StringHelperClass.NewString(new sbyte[] {@in.readByte(), @in.readByte(), @in.readByte(), @in.readByte()});
				switch (magic)
				{
				case "KLAS":
					attenuationStartTrack = new Track<>(int.TYPE, magic);
					attenuationStartTrack.unmarshal(@in);
					break;
				case "KLAE":
					attenuationEndTrack = new Track<>(int.TYPE, magic);
					attenuationEndTrack.unmarshal(@in);
					break;
				case "KLAC":
					colorTrack = new Track<>(typeof(Vec3F), magic);
					colorTrack.unmarshal(@in);
					break;
				case "KLAI":
					intensityTrack = new Track<>(float.TYPE, magic);
					intensityTrack.unmarshal(@in);
					break;
				case "KLBI":
					ambientIntensityTrack = new Track<>(float.TYPE, magic);
					ambientIntensityTrack.unmarshal(@in);
					break;
				case "KLBC":
					ambientColorTrack = new Track<>(typeof(Vec3F), magic);
					ambientColorTrack.unmarshal(@in);
					break;
				case "KLAV":
					visibility = new Track<>(float.TYPE, magic);
					visibility.unmarshal(@in);
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
			node.marshal(@out);
			@out.writeInt(type);
			@out.writeInt(attenuationStart);
			@out.writeInt(attenuationEnd);
			color.marshal(@out);
			@out.writeFloat(intensity);
			ambientColor.marshal(@out);
			@out.writeFloat(ambientIntensity);

			if (attenuationStartTrack != null)
			{
				attenuationStartTrack.marshal(@out);
			}
			if (attenuationEndTrack != null)
			{
				attenuationEndTrack.marshal(@out);
			}
			if (colorTrack != null)
			{
				colorTrack.marshal(@out);
			}
			if (intensityTrack != null)
			{
				intensityTrack.marshal(@out);
			}
			if (ambientIntensityTrack != null)
			{
				ambientIntensityTrack.marshal(@out);
			}
			if (ambientColorTrack != null)
			{
				ambientColorTrack.marshal(@out);
			}
			if (visibility != null)
			{
				visibility.marshal(@out);
			}

			int size = (int)(@out.FilePointer - sizeOffset);
			long currentOffset = @out.FilePointer;
			@out.seek(sizeOffset);
			@out.writeInt(size);
			@out.seek(currentOffset);
		}
	}

}