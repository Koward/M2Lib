using System.Text;

namespace jm2lib.blizzard.wc3.mdx
{


	using Marshalable = jm2lib.io.Marshalable;
	using MarshalingStream = jm2lib.io.MarshalingStream;
	using UnmarshalingStream = jm2lib.io.UnmarshalingStream;

	public class SoundTrack : Marshalable
	{
		internal sbyte[] fileName;
		internal float volume;
		internal float pitch;
		internal int flags;

		public SoundTrack()
		{
			fileName = new sbyte[260];
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void unmarshal(jm2lib.io.UnmarshalingStream in) throws java.io.IOException, ClassNotFoundException
		public virtual void unmarshal(UnmarshalingStream @in)
		{
			@in.read(fileName);
			volume = @in.readFloat();
			pitch = @in.readFloat();
			flags = @in.readInt();
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void marshal(jm2lib.io.MarshalingStream out) throws java.io.IOException
		public virtual void marshal(MarshalingStream @out)
		{
			@out.write(fileName);
			@out.writeFloat(volume);
			@out.writeFloat(pitch);
			@out.writeInt(flags);
		}

		public override string ToString()
		{
			StringBuilder builder = new StringBuilder();
//JAVA TO C# CONVERTER WARNING: The .NET Type.FullName property will not always yield results identical to the Java Class.getName method:
			builder.Append(this.GetType().FullName).Append(" {\n\tfileName: ").Append(Arrays.ToString(fileName)).Append("\n\tvolume: ").Append(volume).Append("\n\tpitch: ").Append(pitch).Append("\n\tflags: ").Append(flags).Append("\n}");
			return builder.ToString();
		}

	}
}