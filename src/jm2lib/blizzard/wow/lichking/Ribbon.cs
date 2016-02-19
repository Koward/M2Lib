namespace jm2lib.blizzard.wow.lichking
{

	using LERandomAccessFile = com.mindprod.ledatastream.LERandomAccessFile;

	using jm2lib.blizzard.common.types;
	using Vec3F = jm2lib.blizzard.common.types.Vec3F;
	using AnimFilesHandler = jm2lib.blizzard.wow.common.AnimFilesHandler;
	using MarshalingStream = jm2lib.io.MarshalingStream;
	using UnmarshalingStream = jm2lib.io.UnmarshalingStream;

	public class Ribbon : AnimFilesHandler
	{
		public int unknown0;
		public int bone;
		public Vec3F position;
		public ArrayRef<char?> textures;
		public ArrayRef<char?> blendRef;
		public AnimationBlock<Vec3F> color;
		public AnimationBlock<char?> opacity;
		public AnimationBlock<float?> heightAbove;
		public AnimationBlock<float?> heightBelow;
		public float resolution;
		public float length;
		public float emissionAngle;
		public char[] renderFlags;
		public AnimationBlock<char?> unknown1;
		public AnimationBlock<sbyte?> unknown2;
		public int unknown3;

		public Ribbon()
		{
			unknown0 = 0;
			bone = 0;
			position = new Vec3F();
			textures = new ArrayRef<char?>(char.TYPE);
			blendRef = new ArrayRef<char?>(char.TYPE);
			color = new AnimationBlock<Vec3F>(typeof(Vec3F));
			opacity = new AnimationBlock<char?>(char.TYPE);
			heightAbove = new AnimationBlock<float?>(float.TYPE);
			heightBelow = new AnimationBlock<float?>(float.TYPE);
			resolution = 0;
			length = 0;
			emissionAngle = 0;
			renderFlags = new char[2];
			unknown1 = new AnimationBlock<char?>(char.TYPE);
			unknown2 = new AnimationBlock<sbyte?>(sbyte.TYPE);
			unknown3 = 0;
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void unmarshal(jm2lib.io.UnmarshalingStream in) throws java.io.IOException, ClassNotFoundException
		public virtual void unmarshal(UnmarshalingStream @in)
		{
			unknown0 = @in.readInt();
			bone = @in.readInt();
			position.unmarshal(@in);
			textures.unmarshal(@in);
			blendRef.unmarshal(@in);
			color.unmarshal(@in);
			opacity.unmarshal(@in);
			heightAbove.unmarshal(@in);
			heightBelow.unmarshal(@in);
			resolution = @in.readFloat();
			length = @in.readFloat();
			emissionAngle = @in.readFloat();
			for (sbyte i = 0;i < renderFlags.Length ;i++)
			{
				renderFlags[i] = @in.readChar();
			}
			unknown1.unmarshal(@in);
			unknown2.unmarshal(@in);
			unknown3 = @in.readInt();
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void marshal(jm2lib.io.MarshalingStream out) throws java.io.IOException
		public virtual void marshal(MarshalingStream @out)
		{
			@out.writeInt(unknown0);
			@out.writeInt(bone);
			position.marshal(@out);
			textures.marshal(@out);
			blendRef.marshal(@out);
			color.marshal(@out);
			opacity.marshal(@out);
			heightAbove.marshal(@out);
			heightBelow.marshal(@out);
			@out.writeFloat(resolution);
			@out.writeFloat(length);
			@out.writeFloat(emissionAngle);
			for (sbyte i = 0;i < 2;i++)
			{
				@out.writeChar(renderFlags[i]);
			}
			unknown1.marshal(@out);
			unknown2.marshal(@out);
			@out.writeInt(unknown3);
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void writeContent(jm2lib.io.MarshalingStream out) throws InstantiationException, IllegalAccessException, java.io.IOException
		public virtual void writeContent(MarshalingStream @out)
		{
			textures.writeContent(@out);
			blendRef.writeContent(@out);
			color.writeContent(@out);
			opacity.writeContent(@out);
			heightAbove.writeContent(@out);
			heightBelow.writeContent(@out);
			unknown1.writeContent(@out);
			unknown2.writeContent(@out);
		}

		public virtual LERandomAccessFile[] AnimFiles
		{
			set
			{
				color.AnimFiles = value;
				opacity.AnimFiles = value;
				heightAbove.AnimFiles = value;
				heightBelow.AnimFiles = value;
				unknown1.AnimFiles = value;
				unknown2.AnimFiles = value;
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public jm2lib.blizzard.wow.classic.Ribbon downConvert(jm2lib.blizzard.common.types.ArrayRef<jm2lib.blizzard.wow.classic.Animation> animations) throws Exception
		public virtual jm2lib.blizzard.wow.classic.Ribbon downConvert(ArrayRef<jm2lib.blizzard.wow.classic.Animation> animations)
		{
			jm2lib.blizzard.wow.classic.Ribbon output = new jm2lib.blizzard.wow.classic.Ribbon();
			output.unknown0 = unknown0;
			output.bone = bone;
			output.position = position;
			output.textures = textures;
			output.blendRef = blendRef;
			output.color = color.downConvert(animations);
			output.opacity = opacity.downConvert(animations);
			output.heightAbove = heightAbove.downConvert(animations);
			output.heightBelow = heightBelow.downConvert(animations);
			output.resolution = resolution;
			output.length = length;
			output.emissionAngle = emissionAngle;
			output.renderFlags = renderFlags;
			output.unknown1 = unknown1.downConvert(animations);
			output.unknown2 = unknown2.downConvert(animations);
			return output;
		}
	}

}