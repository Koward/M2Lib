namespace jm2lib.blizzard.wow.classic
{

	using Referencer = jm2lib.blizzard.common.interfaces.Referencer;
	using Vec3F = jm2lib.blizzard.common.types.Vec3F;
	using MarshalingStream = jm2lib.io.MarshalingStream;
	using UnmarshalingStream = jm2lib.io.UnmarshalingStream;

	public class Light : Referencer
	{
		public short type;
		public short bone;
		public Vec3F position;
		public AnimationBlock<Vec3F> ambientColor;
		public AnimationBlock<float?> ambientIntensity;
		public AnimationBlock<Vec3F> diffuseColor;
		public AnimationBlock<float?> diffuseIntensity;
		public AnimationBlock<float?> attenuationStart;
		public AnimationBlock<float?> attenuationEnd;
		public AnimationBlock<sbyte?> unknown;

		public Light()
		{
			type = 0;
			bone = 0;
			position = new Vec3F();
			ambientColor = new AnimationBlock<Vec3F>(typeof(Vec3F));
			ambientIntensity = new AnimationBlock<float?>(float.TYPE);
			diffuseColor = new AnimationBlock<Vec3F>(typeof(Vec3F));
			diffuseIntensity = new AnimationBlock<float?>(float.TYPE);
			attenuationStart = new AnimationBlock<float?>(float.TYPE);
			attenuationEnd = new AnimationBlock<float?>(float.TYPE);
			unknown = new AnimationBlock<sbyte?>(sbyte.TYPE);
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void unmarshal(jm2lib.io.UnmarshalingStream in) throws java.io.IOException, ClassNotFoundException
		public virtual void unmarshal(UnmarshalingStream @in)
		{
			type = @in.readShort();
			bone = @in.readShort();
			position.unmarshal(@in);
			ambientColor.unmarshal(@in);
			ambientIntensity.unmarshal(@in);
			diffuseColor.unmarshal(@in);
			diffuseIntensity.unmarshal(@in);
			attenuationStart.unmarshal(@in);
			attenuationEnd.unmarshal(@in);
			unknown.unmarshal(@in);
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void marshal(jm2lib.io.MarshalingStream out) throws java.io.IOException
		public virtual void marshal(MarshalingStream @out)
		{
			@out.writeShort(type);
			@out.writeShort(bone);
			position.marshal(@out);
			ambientColor.marshal(@out);
			ambientIntensity.marshal(@out);
			diffuseColor.marshal(@out);
			diffuseIntensity.marshal(@out);
			attenuationStart.marshal(@out);
			attenuationEnd.marshal(@out);
			unknown.marshal(@out);
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void writeContent(jm2lib.io.MarshalingStream out) throws InstantiationException, IllegalAccessException, java.io.IOException
		public virtual void writeContent(MarshalingStream @out)
		{
			ambientColor.writeContent(@out);
			ambientIntensity.writeContent(@out);
			diffuseColor.writeContent(@out);
			diffuseIntensity.writeContent(@out);
			attenuationStart.writeContent(@out);
			attenuationEnd.writeContent(@out);
			unknown.writeContent(@out);
		}
	}

}