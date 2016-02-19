using System.Text;

namespace jm2lib.blizzard.wow.cataclysm
{

	using LERandomAccessFile = com.mindprod.ledatastream.LERandomAccessFile;

	using jm2lib.blizzard.common.types;
	using Vec3F = jm2lib.blizzard.common.types.Vec3F;
	using Vec9F = jm2lib.blizzard.common.types.Vec9F;
	using AnimFilesHandler = jm2lib.blizzard.wow.common.AnimFilesHandler;
	using jm2lib.blizzard.wow.lichking;
	using MarshalingStream = jm2lib.io.MarshalingStream;
	using UnmarshalingStream = jm2lib.io.UnmarshalingStream;

	public class Camera : AnimFilesHandler
	{
		public int type;
		public float farClip;
		public float nearClip;
		public AnimationBlock<Vec9F> positions;
		public Vec3F positionBase;
		public AnimationBlock<Vec9F> targetPositions;
		public Vec3F targetPositionBase;
		public AnimationBlock<Vec3F> roll;
		public AnimationBlock<Vec3F> fov;

		public Camera()
		{
			type = 0;
			farClip = 0;
			nearClip = 0;
			positions = new AnimationBlock<Vec9F>(typeof(Vec9F));
			positionBase = new Vec3F();
			targetPositions = new AnimationBlock<Vec9F>(typeof(Vec9F));
			targetPositionBase = new Vec3F();
			roll = new AnimationBlock<Vec3F>(typeof(Vec3F));
			fov = new AnimationBlock<Vec3F>(typeof(Vec3F));
		}

		/* (non-Javadoc)
		 * @see java.lang.Object#toString()
		 */
		public override string ToString()
		{
			StringBuilder builder = new StringBuilder();
//JAVA TO C# CONVERTER WARNING: The .NET Type.FullName property will not always yield results identical to the Java Class.getName method:
			builder.Append(this.GetType().FullName).Append(" {\n\ttype: ").Append(type).Append("\n\tfarClip: ").Append(farClip).Append("\n\tnearClip: ").Append(nearClip).Append("\n\tpositions: ").Append(positions).Append("\n\tpositionBase: ").Append(positionBase).Append("\n\ttargetPositions: ").Append(targetPositions).Append("\n\ttargetPositionBase: ").Append(targetPositionBase).Append("\n\troll: ").Append(roll).Append("\n\tfov: ").Append(fov).Append("\n}");
			return builder.ToString();
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void unmarshal(jm2lib.io.UnmarshalingStream in) throws java.io.IOException, ClassNotFoundException
		public virtual void unmarshal(UnmarshalingStream @in)
		{
			type = @in.readInt();
			farClip = @in.readFloat();
			nearClip = @in.readFloat();
			positions.unmarshal(@in);
			positionBase.unmarshal(@in);
			targetPositions.unmarshal(@in);
			targetPositionBase.unmarshal(@in);
			roll.unmarshal(@in);
			fov.unmarshal(@in);
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void marshal(jm2lib.io.MarshalingStream out) throws java.io.IOException
		public virtual void marshal(MarshalingStream @out)
		{
			@out.writeInt(type);
			@out.writeFloat(farClip);
			@out.writeFloat(nearClip);
			positions.marshal(@out);
			positionBase.marshal(@out);
			targetPositions.marshal(@out);
			targetPositionBase.marshal(@out);
			roll.marshal(@out);
			fov.marshal(@out);
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void writeContent(jm2lib.io.MarshalingStream out) throws InstantiationException, IllegalAccessException, java.io.IOException
		public virtual void writeContent(MarshalingStream @out)
		{
			positions.writeContent(@out);
			targetPositions.writeContent(@out);
			roll.writeContent(@out);
			fov.writeContent(@out);
		}

		public virtual LERandomAccessFile[] AnimFiles
		{
			set
			{
				positions.AnimFiles = value;
				targetPositions.AnimFiles = value;
				roll.AnimFiles = value;
				fov.AnimFiles = value;
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public jm2lib.blizzard.wow.lichking.Camera downConvert() throws Exception
		public virtual jm2lib.blizzard.wow.lichking.Camera downConvert()
		{
			jm2lib.blizzard.wow.lichking.Camera output = new jm2lib.blizzard.wow.lichking.Camera();
			output.type = type;
			output.fov = type == 0 ? 0.7F : 0.97F; // Original FoV conversion, generally overridden by next line.
			if (fov.values.Count == 1)
			{
				output.fov = fov.values[0].get(0).X;
			}
			output.farClip = farClip;
			output.nearClip = nearClip;
			output.positions = positions;
			output.positionBase = positionBase;
			output.targetPositions = targetPositions;
			output.targetPositionBase = targetPositionBase;
			output.roll = roll;
			return output;
		}

		/// <summary>
		/// Set the animation block field of view from an old simple float value. </summary>
		/// <param name="oldFov"> field of view. </param>
		public virtual float FieldOfView
		{
			set
			{
				ArrayRef<int?> times = new ArrayRef<int?>(int.TYPE);
				ArrayRef<Vec3F> values = new ArrayRef<Vec3F>(typeof(Vec3F));
				times.Add(0);
				values.Add(new Vec3F(value, 0, 0));
				fov.timestamps.Add(times);
				fov.values.Add(values);
			}
		}
	}

}