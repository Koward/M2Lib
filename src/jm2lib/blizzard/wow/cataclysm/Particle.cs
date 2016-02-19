using System.Text;

namespace jm2lib.blizzard.wow.cataclysm
{


	using LERandomAccessFile = com.mindprod.ledatastream.LERandomAccessFile;

	using jm2lib.blizzard.common.types;
	using FixedPoint = jm2lib.blizzard.common.types.FixedPoint;
	using QuatF = jm2lib.blizzard.common.types.QuatF;
	using Vec2F = jm2lib.blizzard.common.types.Vec2F;
	using Vec3F = jm2lib.blizzard.common.types.Vec3F;
	using AnimFilesHandler = jm2lib.blizzard.wow.common.AnimFilesHandler;
	using jm2lib.blizzard.wow.lichking;
	using jm2lib.blizzard.wow.lichking;
	using MarshalingStream = jm2lib.io.MarshalingStream;
	using UnmarshalingStream = jm2lib.io.UnmarshalingStream;

	public class Particle : AnimFilesHandler
	{
		public int unknown;
		public int flags;
		public Vec3F position;
		public char bone;
		public char texture;
		public ArrayRef<sbyte?> modelFileName;
		public ArrayRef<sbyte?> childEmitterFileName;
		public sbyte blendingType;
		public sbyte emitterType;
		public char particleColorIndex;
		public FixedPoint[] multiTextureParamX;
		public char textureTileRotation;
		public char textureDimensionsRows;
		public char textureDimensionsColumns;
		public AnimationBlock<float?> emissionSpeed;
		public AnimationBlock<float?> speedVariation;
		public AnimationBlock<float?> verticalRange;
		public AnimationBlock<float?> horizontalRange;
		public AnimationBlock<float?> gravity;
		public AnimationBlock<float?> lifespan;
		public int unknownPadding;
		public AnimationBlock<float?> emissionRate;
		public int unknownPadding2;
		public AnimationBlock<float?> emissionAreaLength;
		public AnimationBlock<float?> emissionAreaWidth;
		public AnimationBlock<float?> gravity2;
		public FakeAnimationBlock<Vec3F> colorTrack;
		public FakeAnimationBlock<short?> alphaTrack;
		public FakeAnimationBlock<Vec2F> scaleTrack;
		public Vec2F unknownFields;
		public FakeAnimationBlock<char?> headCellTrack;
		public FakeAnimationBlock<char?> tailCellTrack;
		public float somethingParticleStyle;
		public Vec2F unknownFloats1;
		public Vec2F twinkleScale; // FIXME Check what CRange is
		public float blank; // FIXME There is a blank in the wiki... wtf ?
		public float drag;
		public Vec2F unknownFloats2;
		public float rotation;
		public Vec2F unknownFloats3;
		public Vec3F rot1;
		public Vec3F rot2;
		public Vec3F trans;
		public QuatF followParams;
		public ArrayRef<Vec3F> unknownReference;
		public AnimationBlock<sbyte?> enabledIn;
		public FixedPoint[][] multiTextureParam0;
		public FixedPoint[][] multiTextureParam1;

		public Particle()
		{
			unknown = 0;
			flags = 0;
			position = new Vec3F();
			bone = (char)0;
			texture = (char)0;
			modelFileName = new ArrayRef<sbyte?>(sbyte.TYPE);
			childEmitterFileName = new ArrayRef<sbyte?>(sbyte.TYPE);
			blendingType = 0;
			emitterType = 0;
			particleColorIndex = (char)0;
			multiTextureParamX = new FixedPoint[] {new FixedPoint(2, 5), new FixedPoint(2, 5)};
			textureTileRotation = (char)0;
			textureDimensionsRows = (char)0;
			textureDimensionsColumns = (char)0;
			emissionSpeed = new AnimationBlock<float?>(float.TYPE);
			speedVariation = new AnimationBlock<float?>(float.TYPE);
			verticalRange = new AnimationBlock<float?>(float.TYPE);
			horizontalRange = new AnimationBlock<float?>(float.TYPE);
			gravity = new AnimationBlock<float?>(float.TYPE);
			lifespan = new AnimationBlock<float?>(float.TYPE);
			unknownPadding = 0;
			emissionRate = new AnimationBlock<float?>(float.TYPE);
			unknownPadding2 = 0;
			emissionAreaLength = new AnimationBlock<float?>(float.TYPE);
			emissionAreaWidth = new AnimationBlock<float?>(float.TYPE);
			gravity2 = new AnimationBlock<float?>(float.TYPE);
			colorTrack = new FakeAnimationBlock<Vec3F>(typeof(Vec3F));
			alphaTrack = new FakeAnimationBlock<short?>(short.TYPE);
			scaleTrack = new FakeAnimationBlock<Vec2F>(typeof(Vec2F));
			unknownFields = new Vec2F();
			headCellTrack = new FakeAnimationBlock<char?>(char.TYPE);
			tailCellTrack = new FakeAnimationBlock<char?>(char.TYPE);
			somethingParticleStyle = 0;
			unknownFloats1 = new Vec2F();
			twinkleScale = new Vec2F();
			blank = 0;
			drag = 0;
			unknownFloats2 = new Vec2F();
			rotation = 0;
			unknownFloats3 = new Vec2F();
			rot1 = new Vec3F();
			rot2 = new Vec3F();
			trans = new Vec3F();
			followParams = new QuatF();
			unknownReference = new ArrayRef<Vec3F>(typeof(Vec3F));
			enabledIn = new AnimationBlock<sbyte?>(sbyte.TYPE);
			multiTextureParam0 = new FixedPoint[][] {new FixedPoint[] {new FixedPoint(6,9), new FixedPoint(6,9)}, new FixedPoint[] {new FixedPoint(6,9), new FixedPoint(6,9)}};
			multiTextureParam1 = new FixedPoint[][] {new FixedPoint[] {new FixedPoint(6,9), new FixedPoint(6,9)}, new FixedPoint[] {new FixedPoint(6,9), new FixedPoint(6,9)}};
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
			blendingType = @in.readByte();
			emitterType = @in.readByte();
			particleColorIndex = @in.readChar();
			for (sbyte i = 0; i < multiTextureParamX.Length; i++)
			{
				multiTextureParamX[i].unmarshal(@in);
			}
			textureTileRotation = @in.readChar();
			textureDimensionsRows = @in.readChar();
			textureDimensionsColumns = @in.readChar();
			emissionSpeed.unmarshal(@in);
			speedVariation.unmarshal(@in);
			verticalRange.unmarshal(@in);
			horizontalRange.unmarshal(@in);
			gravity.unmarshal(@in);
			lifespan.unmarshal(@in);
			unknownPadding = @in.readInt();
			emissionRate.unmarshal(@in);
			unknownPadding2 = @in.readInt();
			emissionAreaLength.unmarshal(@in);
			emissionAreaWidth.unmarshal(@in);
			gravity2.unmarshal(@in);
			colorTrack.unmarshal(@in);
			alphaTrack.unmarshal(@in);
			scaleTrack.unmarshal(@in);
			unknownFields.unmarshal(@in);
			headCellTrack.unmarshal(@in);
			tailCellTrack.unmarshal(@in);
			somethingParticleStyle = @in.readFloat();
			unknownFloats1.unmarshal(@in);
			twinkleScale.unmarshal(@in);
			blank = @in.readFloat();
			drag = @in.readFloat();
			unknownFloats2.unmarshal(@in);
			rotation = @in.readFloat();
			unknownFloats3.unmarshal(@in);
			rot1.unmarshal(@in);
			rot2.unmarshal(@in);
			trans.unmarshal(@in);
			followParams.unmarshal(@in);
			unknownReference.unmarshal(@in);
			enabledIn.unmarshal(@in);
			for (sbyte i = 0; i < multiTextureParam0.Length; i++)
			{
				for (sbyte j = 0; j < multiTextureParam0[i].Length; j++)
				{
					multiTextureParam0[i][j].unmarshal(@in);
				}
			}
			for (sbyte i = 0; i < multiTextureParam1.Length; i++)
			{
				for (sbyte j = 0; j < multiTextureParam1[i].Length; j++)
				{
					multiTextureParam1[i][j].unmarshal(@in);
				}
			}
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
			@out.writeByte(blendingType);
			@out.writeByte(emitterType);
			@out.writeChar(particleColorIndex);
			for (sbyte i = 0; i < multiTextureParamX.Length; i++)
			{
				multiTextureParamX[i].marshal(@out);
			}
			@out.writeChar(textureTileRotation);
			@out.writeChar(textureDimensionsRows);
			@out.writeChar(textureDimensionsColumns);
			emissionSpeed.marshal(@out);
			speedVariation.marshal(@out);
			verticalRange.marshal(@out);
			horizontalRange.marshal(@out);
			gravity.marshal(@out);
			lifespan.marshal(@out);
			@out.writeInt(unknownPadding);
			emissionRate.marshal(@out);
			@out.writeInt(unknownPadding2);
			emissionAreaLength.marshal(@out);
			emissionAreaWidth.marshal(@out);
			gravity2.marshal(@out);
			colorTrack.marshal(@out);
			alphaTrack.marshal(@out);
			scaleTrack.marshal(@out);
			unknownFields.marshal(@out);
			headCellTrack.marshal(@out);
			tailCellTrack.marshal(@out);
			@out.writeFloat(somethingParticleStyle);
			unknownFloats1.marshal(@out);
			twinkleScale.marshal(@out);
			@out.writeFloat(blank);
			@out.writeFloat(drag);
			unknownFloats2.marshal(@out);
			@out.writeFloat(rotation);
			unknownFloats3.marshal(@out);
			rot1.marshal(@out);
			rot2.marshal(@out);
			trans.marshal(@out);
			followParams.marshal(@out);
			unknownReference.marshal(@out);
			enabledIn.marshal(@out);
			for (sbyte i = 0; i < multiTextureParam0.Length; i++)
			{
				for (sbyte j = 0; j < multiTextureParam0[i].Length; j++)
				{
					multiTextureParam0[i][j].marshal(@out);
				}
			}
			for (sbyte i = 0; i < multiTextureParam1.Length; i++)
			{
				for (sbyte j = 0; j < multiTextureParam1[i].Length; j++)
				{
					multiTextureParam1[i][j].marshal(@out);
				}
			}
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
			colorTrack.writeContent(@out);
			alphaTrack.writeContent(@out);
			scaleTrack.writeContent(@out);
			headCellTrack.writeContent(@out);
			tailCellTrack.writeContent(@out);
			unknownReference.writeContent(@out);
			enabledIn.writeContent(@out);
		}

		public virtual LERandomAccessFile[] AnimFiles
		{
			set
			{
				emissionSpeed.AnimFiles = value;
				speedVariation.AnimFiles = value;
				verticalRange.AnimFiles = value;
				horizontalRange.AnimFiles = value;
				gravity.AnimFiles = value;
				lifespan.AnimFiles = value;
				emissionRate.AnimFiles = value;
				emissionAreaLength.AnimFiles = value;
				emissionAreaWidth.AnimFiles = value;
				gravity2.AnimFiles = value;
				enabledIn.AnimFiles = value;
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public jm2lib.blizzard.wow.lichking.Particle downConvert() throws Exception
		public virtual jm2lib.blizzard.wow.lichking.Particle downConvert()
		{
			jm2lib.blizzard.wow.lichking.Particle output = new jm2lib.blizzard.wow.lichking.Particle();
			output.unknown = unknown;
			output.flags = flags;
			output.position = position;
			output.bone = bone;
			output.texture = texture;
			// Textures fix @see http://www.pxr.dk/wowdev/wiki/index.php?title=M2/WotLK#Multi-textured_particles
			if ((flags & 0x10000000) != 0)
			{
				output.texture = (char)(texture & 0x1F);
				output.flags &= 0x0FFFFFFF;
			}
			output.modelFileName = modelFileName;
			output.childEmitterFileName = childEmitterFileName;
			output.blendingType = blendingType;
			// Blending type fix @see http://www.pxr.dk/wowdev/wiki/index.php?title=M2/WotLK#Particle_Blendings
			if (output.blendingType > 4)
			{
					output.blendingType = 4;
			}
			output.emitterType = emitterType;
			output.particleColorIndex = particleColorIndex;
			/*
			output.particleType = multiTextureParamX[0].toByte();
			output.headOrTail = multiTextureParamX[1].toByte();
			*/
			output.textureTileRotation = textureTileRotation;
			output.textureDimensionsRows = textureDimensionsRows;
			output.textureDimensionsColumns = textureDimensionsColumns;
			output.emissionSpeed = emissionSpeed;
			output.speedVariation = speedVariation;
			output.verticalRange = verticalRange;
			output.horizontalRange = horizontalRange;
			output.gravity = gravity;
			output.lifespan = lifespan;
			output.unknownPadding = unknownPadding;
			output.emissionRate = emissionRate;
			output.unknownPadding2 = unknownPadding2;
			output.emissionAreaLength = emissionAreaLength;
			output.emissionAreaWidth = emissionAreaWidth;
			output.gravity2 = gravity2;
			output.colorTrack = colorTrack;
			output.alphaTrack = alphaTrack;
			output.scaleTrack = scaleTrack;
			output.unknownFields = unknownFields;
			output.headCellTrack = headCellTrack;
			output.tailCellTrack = tailCellTrack;
			output.somethingParticleStyle = somethingParticleStyle;
			output.unknownFloats1 = unknownFloats1;
			output.twinkleScale = twinkleScale;
			output.blank = blank;
			output.drag = drag;
			output.unknownFloats2 = unknownFloats2;
			output.rotation = rotation;
			output.unknownFloats3 = unknownFloats3;
			output.rot1 = rot1;
			output.rot2 = rot2;
			output.trans = trans;
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
			builder.Append(this.GetType().FullName).Append(" {\n\tunknown: ").Append(unknown).Append("\n\tflags: ").Append(flags).Append("\n\tposition: ").Append(position).Append("\n\tbone: ").Append(bone).Append("\n\ttexture: ").Append(texture).Append("\n\tmodelFileName: ").Append(modelFileName.toNameString()).Append("\n\tchildEmitterFileName: ").Append(childEmitterFileName.toNameString()).Append("\n\tblendingType: ").Append(blendingType).Append("\n\temitterType: ").Append(emitterType).Append("\n\tparticleColorIndex: ").Append(particleColorIndex).Append("\n\tmultiTextureParamX: ").Append(Arrays.ToString(multiTextureParamX)).Append("\n\ttextureTileRotation: ").Append(textureTileRotation).Append("\n\ttextureDimensionsRows: ").Append(textureDimensionsRows).Append("\n\ttextureDimensionsColumns: ").Append(textureDimensionsColumns).Append("\n\temissionSpeed: ").Append(emissionSpeed).Append("\n\tspeedVariation: ").Append(speedVariation).Append("\n\tverticalRange: ").Append(verticalRange).Append("\n\thorizontalRange: ").Append(horizontalRange).Append("\n\tgravity: ").Append(gravity).Append("\n\tlifespan: ").Append(lifespan).Append("\n\tunknownPadding: ").Append(unknownPadding).Append("\n\temissionRate: ").Append(emissionRate).Append("\n\tunknownPadding2: ").Append(unknownPadding2).Append("\n\temissionAreaLength: ").Append(emissionAreaLength).Append("\n\temissionAreaWidth: ").Append(emissionAreaWidth).Append("\n\tgravity2: ").Append(gravity2).Append("\n\tcolorTrack: ").Append(colorTrack).Append("\n\talphaTrack: ").Append(alphaTrack).Append("\n\tscaleTrack: ").Append(scaleTrack).Append("\n\tunknownFields: ").Append(unknownFields).Append("\n\theadCellTrack: ").Append(headCellTrack).Append("\n\ttailCellTrack: ").Append(tailCellTrack).Append("\n\tsomethingParticleStyle: ").Append(somethingParticleStyle).Append("\n\tunknownFloats1: ").Append(unknownFloats1).Append("\n\ttwinkleScale: ").Append(twinkleScale).Append("\n\tblank: ").Append(blank).Append("\n\tdrag: ").Append(drag).Append("\n\tunknownFloats2: ").Append(unknownFloats2).Append("\n\trotation: ").Append(rotation).Append("\n\tunknownFloats3: ").Append(unknownFloats3).Append("\n\trot1: ").Append(rot1).Append("\n\trot2: ").Append(rot2).Append("\n\ttrans: ").Append(trans).Append("\n\tfollowParams: ").Append(followParams).Append("\n\tunknownReference: ").Append(unknownReference).Append("\n\tenabledIn: ").Append(enabledIn).Append("\n\tmultiTextureParam0: ").Append(Arrays.ToString(multiTextureParam0)).Append("\n\tmultiTextureParam1: ").Append(Arrays.ToString(multiTextureParam1)).Append("\n}");
			return builder.ToString();
		}
	}

}