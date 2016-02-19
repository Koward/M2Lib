namespace jm2lib.blizzard.wow.classic
{

	using Referencer = jm2lib.blizzard.common.interfaces.Referencer;
	using jm2lib.blizzard.common.types;
	using Vec3F = jm2lib.blizzard.common.types.Vec3F;
	using MarshalingStream = jm2lib.io.MarshalingStream;
	using UnmarshalingStream = jm2lib.io.UnmarshalingStream;

	public class Ribbon : Referencer
	{
		public int unknown0;
		public int bone;
		public Vec3F position;
		public ArrayRef<char?> textures; //FIXME Integers ?
		public ArrayRef<char?> blendRef; //FIXME Ints ?
		public AnimationBlock<Vec3F> color;
		public AnimationBlock<char?> opacity;
		public AnimationBlock<float?> heightAbove;
		public AnimationBlock<float?> heightBelow;
		public float resolution;
		public float length;
		public float emissionAngle;
		public char[] renderFlags;
		public AnimationBlock<char?> unknown1; //FIXME Ints ?
		public AnimationBlock<sbyte?> unknown2; //FIXME Ints ?

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
			for (sbyte i = 0;i < renderFlags.Length;i++)
			{
				renderFlags[i] = @in.readChar();
			}
			unknown1.unmarshal(@in);
			unknown2.unmarshal(@in);
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
			for (sbyte i = 0;i < renderFlags.Length;i++)
			{
				@out.writeChar(renderFlags[i]);
			}
			unknown1.marshal(@out);
			unknown2.marshal(@out);
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
	}

}