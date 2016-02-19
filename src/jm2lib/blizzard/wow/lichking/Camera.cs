using System.Text;

namespace jm2lib.blizzard.wow.lichking
{

	using LERandomAccessFile = com.mindprod.ledatastream.LERandomAccessFile;

	using jm2lib.blizzard.common.types;
	using Vec3F = jm2lib.blizzard.common.types.Vec3F;
	using Vec9F = jm2lib.blizzard.common.types.Vec9F;
	using AnimFilesHandler = jm2lib.blizzard.wow.common.AnimFilesHandler;
	using MarshalingStream = jm2lib.io.MarshalingStream;
	using UnmarshalingStream = jm2lib.io.UnmarshalingStream;

	public class Camera : AnimFilesHandler
	{
		public int type;
		public float fov;
		public float farClip;
		public float nearClip;
		public AnimationBlock<Vec9F> positions;
		public Vec3F positionBase;
		public AnimationBlock<Vec9F> targetPositions;
		public Vec3F targetPositionBase;
		public AnimationBlock<Vec3F> roll;

		public Camera()
		{
			type = 0;
			fov = 0;
			farClip = 0;
			nearClip = 0;
			positions = new AnimationBlock<Vec9F>(typeof(Vec9F));
			positionBase = new Vec3F();
			targetPositions = new AnimationBlock<Vec9F>(typeof(Vec9F));
			targetPositionBase = new Vec3F();
			roll = new AnimationBlock<Vec3F>(typeof(Vec3F));
		}

		public override string ToString()
		{
			StringBuilder builder = new StringBuilder();
//JAVA TO C# CONVERTER WARNING: The .NET Type.FullName property will not always yield results identical to the Java Class.getName method:
			builder.Append(this.GetType().FullName).Append(" {\n\ttype: ").Append(type).Append("\n\tfov: ").Append(fov).Append("\n\tfarClip: ").Append(farClip).Append("\n\tnearClip: ").Append(nearClip).Append("\n\tpositions: ").Append(positions).Append("\n\tpositionBase: ").Append(positionBase).Append("\n\ttargetPositions: ").Append(targetPositions).Append("\n\ttargetPositionBase: ").Append(targetPositionBase).Append("\n\troll: ").Append(roll).Append("\n}");
			return builder.ToString();
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void unmarshal(jm2lib.io.UnmarshalingStream in) throws java.io.IOException, ClassNotFoundException
		public virtual void unmarshal(UnmarshalingStream @in)
		{
			type = @in.readInt();
			fov = @in.readFloat();
			farClip = @in.readFloat();
			nearClip = @in.readFloat();
			positions.unmarshal(@in);
			positionBase.unmarshal(@in);
			targetPositions.unmarshal(@in);
			targetPositionBase.unmarshal(@in);
			roll.unmarshal(@in);
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void marshal(jm2lib.io.MarshalingStream out) throws java.io.IOException
		public virtual void marshal(MarshalingStream @out)
		{
			@out.writeInt(type);
			@out.writeFloat(fov);
			@out.writeFloat(farClip);
			@out.writeFloat(nearClip);
			positions.marshal(@out);
			positionBase.marshal(@out);
			targetPositions.marshal(@out);
			targetPositionBase.marshal(@out);
			roll.marshal(@out);
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void writeContent(jm2lib.io.MarshalingStream out) throws InstantiationException, IllegalAccessException, java.io.IOException
		public virtual void writeContent(MarshalingStream @out)
		{
			positions.writeContent(@out);
			targetPositions.writeContent(@out);
			roll.writeContent(@out);
		}

		public virtual LERandomAccessFile[] AnimFiles
		{
			set
			{
				positions.AnimFiles = value;
				targetPositions.AnimFiles = value;
				roll.AnimFiles = value;
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public jm2lib.blizzard.wow.cataclysm.Camera downConvert() throws Exception
		public virtual jm2lib.blizzard.wow.cataclysm.Camera downConvert()
		{
			jm2lib.blizzard.wow.cataclysm.Camera output = new jm2lib.blizzard.wow.cataclysm.Camera();
			output.type = type;
			output.farClip = farClip;
			output.nearClip = nearClip;
			output.positions = positions;
			output.positionBase = positionBase;
			output.targetPositions = targetPositions;
			output.targetPositionBase = targetPositionBase;
			output.roll = roll;
			output.FieldOfView = fov;
			return output;
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public jm2lib.blizzard.wow.classic.Camera downConvert(jm2lib.blizzard.common.types.ArrayRef<jm2lib.blizzard.wow.classic.Animation> animations) throws Exception
		public virtual jm2lib.blizzard.wow.classic.Camera downConvert(ArrayRef<jm2lib.blizzard.wow.classic.Animation> animations)
		{
			jm2lib.blizzard.wow.classic.Camera output = new jm2lib.blizzard.wow.classic.Camera();
			output.type = type;
			output.fov = fov;
			output.farClip = farClip;
			output.nearClip = nearClip;
			output.positions = positions.downConvert(animations);
			output.positionBase = positionBase;
			output.targetPositions = targetPositions.downConvert(animations);
			output.targetPositionBase = targetPositionBase;
			output.roll = roll.downConvert(animations);
			return output;
		}
	}

}