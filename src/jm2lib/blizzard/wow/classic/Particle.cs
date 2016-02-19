using System.Text;

namespace jm2lib.blizzard.wow.classic
{


	using Referencer = jm2lib.blizzard.common.interfaces.Referencer;
	using jm2lib.blizzard.common.types;
	using BGRA = jm2lib.blizzard.common.types.BGRA;
	using QuatF = jm2lib.blizzard.common.types.QuatF;
	using Vec2F = jm2lib.blizzard.common.types.Vec2F;
	using Vec3F = jm2lib.blizzard.common.types.Vec3F;
	using MarshalingStream = jm2lib.io.MarshalingStream;
	using UnmarshalingStream = jm2lib.io.UnmarshalingStream;

	public class Particle : Referencer
	{
		public int unknown;
		public int flags; //0x8000 ok, 0x20000 not ok
		public Vec3F position;
		public char bone;
		public char texture;
		public ArrayRef<sbyte?> modelFileName; //FIXME Ints ?
		public ArrayRef<sbyte?> childEmitterFileName; //FIXME Ints ?
		public char blendingType;
		public char emitterType;
		public sbyte particleType;
		public sbyte headOrTail;
		public char textureTileRotation;
		public char textureDimensionsRows;
		public char textureDimensionsColumns;
		public AnimationBlock<float?> emissionSpeed;
		public AnimationBlock<float?> speedVariation;
		public AnimationBlock<float?> verticalRange;
		public AnimationBlock<float?> horizontalRange;
		public AnimationBlock<float?> gravity;
		public AnimationBlock<float?> lifespan;
		public AnimationBlock<float?> emissionRate;
		public AnimationBlock<float?> emissionAreaLength;
		public AnimationBlock<float?> emissionAreaWidth;
		public AnimationBlock<float?> gravity2;
		public float midPoint;
		public BGRA[] colorTrack; //3*RGBA = 12
		public float[] scaleTrack; //3

		//Tries to refine the tiles
		public char[] headCellTrack1; //2
		public short between1; //Always 1 ?
		public char[] headCellTrack2; //2
		public short between2; //Always 1 ?
		public short[] tiles; //4 tailCellTrack ?


		public float somethingParticleStyle;
		public Vec2F unknownFloats1;
		public Vec2F twinkleScale; // FIXME Check what CRange is
		public float blank;
		public float drag;
		public float rotation;
		public float[] manyFloats; //10
		public QuatF followParams;
		public ArrayRef<Vec3F> unknownReference;
		public AnimationBlock<sbyte?> enabledIn; //FIXME Ints ?

		public Particle()
		{
			unknown = 0;
			flags = 0;
			position = new Vec3F();
			bone = (char)0;
			texture = (char)0;
			modelFileName = new ArrayRef<sbyte?>(sbyte.TYPE);
			childEmitterFileName = new ArrayRef<sbyte?>(sbyte.TYPE);
			blendingType = (char)0;
			emitterType = (char)0;
			particleType = 0;
			headOrTail = 0;
			textureTileRotation = (char)0;
			textureDimensionsRows = (char)0;
			textureDimensionsColumns = (char)0;
			emissionSpeed = new AnimationBlock<float?>(float.TYPE);
			speedVariation = new AnimationBlock<float?>(float.TYPE);
			verticalRange = new AnimationBlock<float?>(float.TYPE);
			horizontalRange = new AnimationBlock<float?>(float.TYPE);
			gravity = new AnimationBlock<float?>(float.TYPE);
			lifespan = new AnimationBlock<float?>(float.TYPE);
			emissionRate = new AnimationBlock<float?>(float.TYPE);
			emissionAreaLength = new AnimationBlock<float?>(float.TYPE);
			emissionAreaWidth = new AnimationBlock<float?>(float.TYPE);
			gravity2 = new AnimationBlock<float?>(float.TYPE);
			midPoint = 0;
			colorTrack = new BGRA[3];
			for (sbyte i = 0; i < 3; i++)
			{
				colorTrack[i] = new BGRA();
			}
			scaleTrack = new float[3];
			headCellTrack1 = new char[2];
			between1 = 1;
			headCellTrack2 = new char[2];
			between2 = 1;
			tiles = new short[4];
			somethingParticleStyle = 0;
			unknownFloats1 = new Vec2F();
			twinkleScale = new Vec2F();
			blank = 0;
			drag = 0;
			rotation = 0;
			manyFloats = new float[10];
			followParams = new QuatF();
			unknownReference = new ArrayRef<Vec3F>(typeof(Vec3F));
			enabledIn = new AnimationBlock<sbyte?>(sbyte.TYPE);
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void unmarshal(jm2lib.io.UnmarshalingStream in) throws java.io.IOException, ClassNotFoundException
		public virtual void unmarshal(UnmarshalingStream @in)
		{
			unknown = @in.readInt();
			flags = @in.readInt();
			position.unmarshal(@in);
			bone = @in.readChar();
			texture = @in.readChar();
			modelFileName.unmarshal(@in);
			childEmitterFileName.unmarshal(@in);
			blendingType = @in.readChar();
			emitterType = @in.readChar();
			particleType = @in.readByte();
			headOrTail = @in.readByte();
			textureTileRotation = @in.readChar();
			textureDimensionsRows = @in.readChar();
			textureDimensionsColumns = @in.readChar();
			emissionSpeed.unmarshal(@in);
			speedVariation.unmarshal(@in);
			verticalRange.unmarshal(@in);
			horizontalRange.unmarshal(@in);
			gravity.unmarshal(@in);
			lifespan.unmarshal(@in);
			emissionRate.unmarshal(@in);
			emissionAreaLength.unmarshal(@in);
			emissionAreaWidth.unmarshal(@in);
			gravity2.unmarshal(@in);
			midPoint = @in.readFloat();
			for (sbyte i = 0; i < colorTrack.Length; i++)
			{
				colorTrack[i].unmarshal(@in);
			}
			for (sbyte i = 0; i < scaleTrack.Length; i++)
			{
				scaleTrack[i] = @in.readFloat();
			}
			for (sbyte i = 0; i < headCellTrack1.Length; i++)
			{
				headCellTrack1[i] = @in.readChar();
			}
			between1 = @in.readShort();
			for (sbyte i = 0; i < headCellTrack2.Length; i++)
			{
				headCellTrack2[i] = @in.readChar();
			}
			between2 = @in.readShort();
			for (sbyte i = 0; i < tiles.Length; i++)
			{
				tiles[i] = @in.readShort();
			}
			somethingParticleStyle = @in.readFloat();
			unknownFloats1.unmarshal(@in);
			twinkleScale.unmarshal(@in);
			blank = @in.readFloat();
			drag = @in.readFloat();
			rotation = @in.readFloat();
			for (sbyte i = 0; i < 10; i++)
			{
				manyFloats[i] = @in.readFloat();
			}
			followParams.unmarshal(@in);
			unknownReference.unmarshal(@in);
			enabledIn.unmarshal(@in);
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void marshal(jm2lib.io.MarshalingStream out) throws java.io.IOException
		public virtual void marshal(MarshalingStream @out)
		{
			@out.writeInt(unknown);
			@out.writeInt(flags);
			position.marshal(@out);
			@out.writeChar(bone);
			@out.writeChar(texture);
			modelFileName.marshal(@out);
			childEmitterFileName.marshal(@out);
			@out.writeChar(blendingType);
			@out.writeChar(emitterType);
			@out.writeByte(particleType);
			@out.writeByte(headOrTail);
			@out.writeChar(textureTileRotation);
			@out.writeChar(textureDimensionsRows);
			@out.writeChar(textureDimensionsColumns);
			emissionSpeed.marshal(@out);
			speedVariation.marshal(@out);
			verticalRange.marshal(@out);
			horizontalRange.marshal(@out);
			gravity.marshal(@out);
			lifespan.marshal(@out);
			emissionRate.marshal(@out);
			emissionAreaLength.marshal(@out);
			emissionAreaWidth.marshal(@out);
			gravity2.marshal(@out);

			@out.writeFloat(midPoint);
			//for(byte i = 0; i < 12; i++) out.writeByte(colorTrack[i]);
			for (sbyte i = 0; i < colorTrack.Length; i++)
			{
				colorTrack[i].marshal(@out);
			}
			for (sbyte i = 0; i < scaleTrack.Length; i++)
			{
				@out.writeFloat(scaleTrack[i]);
			}
			for (sbyte i = 0; i < headCellTrack1.Length; i++)
			{
				@out.writeChar(headCellTrack1[i]);
			}
			@out.writeShort(between1);
			for (sbyte i = 0; i < headCellTrack2.Length; i++)
			{
				@out.writeChar(headCellTrack2[i]);
			}
			@out.writeShort(between2);
			for (sbyte i = 0; i < tiles.Length; i++)
			{
				@out.writeShort(tiles[i]);
			}

			@out.writeFloat(somethingParticleStyle);
			unknownFloats1.marshal(@out);
			twinkleScale.marshal(@out);
			@out.writeFloat(blank);
			@out.writeFloat(drag);
			@out.writeFloat(rotation);
			for (sbyte i = 0; i < 10; i++)
			{
				@out.writeFloat(manyFloats[i]);
			}
			followParams.marshal(@out);
			unknownReference.marshal(@out);
			enabledIn.marshal(@out);
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void writeContent(jm2lib.io.MarshalingStream out) throws InstantiationException, IllegalAccessException, java.io.IOException
		public virtual void writeContent(MarshalingStream @out)
		{
			modelFileName.writeContent(@out);
			childEmitterFileName.writeContent(@out);
			emissionSpeed.writeContent(@out);
			speedVariation.writeContent(@out);
			verticalRange.writeContent(@out);
			horizontalRange.writeContent(@out);
			gravity.writeContent(@out);
			lifespan.writeContent(@out);
			emissionRate.writeContent(@out);
			emissionAreaLength.writeContent(@out);
			emissionAreaWidth.writeContent(@out);
			gravity2.writeContent(@out);
			unknownReference.writeContent(@out);
			enabledIn.writeContent(@out);
		}

		public virtual jm2lib.blizzard.wow.lateburningcrusade.Particle upConvert()
		{
			jm2lib.blizzard.wow.lateburningcrusade.Particle output = new jm2lib.blizzard.wow.lateburningcrusade.Particle();
			output.unknown = unknown;
			output.flags = flags;
			output.position = position;
			output.bone = bone;
			output.texture = texture;
			output.modelFileName = modelFileName;
			output.childEmitterFileName = childEmitterFileName;
			output.blendingType = (sbyte) blendingType;
			output.emitterType = (sbyte) emitterType;
			output.particleType = particleType;
			output.headOrTail = headOrTail;
			output.textureTileRotation = textureTileRotation;
			output.textureDimensionsRows = textureDimensionsRows;
			output.textureDimensionsColumns = textureDimensionsColumns;
			output.emissionSpeed = emissionSpeed;
			output.speedVariation = speedVariation;
			output.verticalRange = verticalRange;
			output.horizontalRange = horizontalRange;
			output.gravity = gravity;
			output.lifespan = lifespan;
			output.emissionRate = emissionRate;
			output.emissionAreaLength = emissionAreaLength;
			output.emissionAreaWidth = emissionAreaWidth;
			output.gravity2 = gravity2;
			output.midPoint = midPoint;
			output.colorTrack = colorTrack;
			output.scaleTrack = scaleTrack;
			output.headCellTrack1 = headCellTrack1;
			output.between1 = between1;
			output.headCellTrack2 = headCellTrack2;
			output.between2 = between2;
			output.tiles = tiles;
			output.somethingParticleStyle = somethingParticleStyle;
			output.unknownFloats1 = unknownFloats1;
			output.twinkleScale = twinkleScale;
			output.blank = blank;
			output.drag = drag;
			output.rotation = rotation;
			output.manyFloats = manyFloats;
			output.followParams = followParams;
			output.unknownReference = unknownReference;
			output.enabledIn = enabledIn;
			return output;
		}

		/* (non-Javadoc)
		 * @see java.lang.Object#toString()
		 */
		public override string ToString()
		{
			StringBuilder builder = new StringBuilder();
//JAVA TO C# CONVERTER WARNING: The .NET Type.FullName property will not always yield results identical to the Java Class.getName method:
			builder.Append(this.GetType().FullName).Append(" {\n\tunknown: ").Append(unknown).Append("\n\tflags: ").Append(int.toBinaryString(flags)).Append("\n\tposition: ").Append(position).Append("\n\tbone: ").Append((int) bone).Append("\n\ttexture: ").Append((int) texture).Append("\n\tmodelFileName: ").Append(modelFileName.toNameString()).Append("\n\tchildEmitterFileName: ").Append(childEmitterFileName.toNameString()).Append("\n\tblendingType: ").Append((int) blendingType).Append("\n\temitterType: ").Append((int) emitterType).Append("\n\tparticleType: ").Append(particleType).Append("\n\theadOrTail: ").Append(headOrTail).Append("\n\ttextureTileRotation: ").Append((int) textureTileRotation).Append("\n\ttextureDimensionsRows: ").Append((int) textureDimensionsRows).Append("\n\ttextureDimensionsColumns: ").Append((int) textureDimensionsColumns).Append("\n\temissionSpeed: ").Append(emissionSpeed).Append("\n\tspeedVariation: ").Append(speedVariation).Append("\n\tverticalRange: ").Append(verticalRange).Append("\n\thorizontalRange: ").Append(horizontalRange).Append("\n\tgravity: ").Append(gravity).Append("\n\tlifespan: ").Append(lifespan).Append("\n\temissionRate: ").Append(emissionRate).Append("\n\temissionAreaLength: ").Append(emissionAreaLength).Append("\n\temissionAreaWidth: ").Append(emissionAreaWidth).Append("\n\tgravity2: ").Append(gravity2).Append("\n\tmidPoint: ").Append(midPoint).Append("\n\tcolorTrack: ").Append(Arrays.ToString(colorTrack)).Append("\n\tscaleTrack: ").Append(Arrays.ToString(scaleTrack)).Append("\n\theadCellTrack1: ").Append(Arrays.ToString(headCellTrack1)).Append("\n\tbetween1: ").Append(between1).Append("\n\theadCellTrack2: ").Append(Arrays.ToString(headCellTrack2)).Append("\n\tbetween2: ").Append(between2).Append("\n\ttiles: ").Append(Arrays.ToString(tiles)).Append("\n\tsomethingParticleStyle: ").Append(somethingParticleStyle).Append("\n\tunknownFloats1: ").Append(unknownFloats1).Append("\n\ttwinkleScale: ").Append(twinkleScale).Append("\n\tblank: ").Append(blank).Append("\n\tdrag: ").Append(drag).Append("\n\trotation: ").Append(rotation).Append("\n\tmanyFloats: ").Append(Arrays.ToString(manyFloats)).Append("\n\tfollowParams: ").Append(followParams).Append("\n\tunknownReference: ").Append(unknownReference).Append("\n\tenabledIn: ").Append(enabledIn).Append("\n}");
			return builder.ToString();
		}
	}

}