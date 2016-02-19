using System.Text;

namespace jm2lib.blizzard.wow.lichking
{

	using LERandomAccessFile = com.mindprod.ledatastream.LERandomAccessFile;

	using jm2lib.blizzard.common.types;
	using Vec3F = jm2lib.blizzard.common.types.Vec3F;
	using AnimFilesHandler = jm2lib.blizzard.wow.common.AnimFilesHandler;
	using MarshalingStream = jm2lib.io.MarshalingStream;
	using UnmarshalingStream = jm2lib.io.UnmarshalingStream;

	public class SubmeshAnimation : AnimFilesHandler
	{
		public AnimationBlock<Vec3F> color;
		public AnimationBlock<short?> alpha;

		public SubmeshAnimation()
		{
			color = new AnimationBlock<Vec3F>(typeof(Vec3F));
			alpha = new AnimationBlock<short?>(short.TYPE, 1);
		}

		public override string ToString()
		{
			StringBuilder builder = new StringBuilder();
//JAVA TO C# CONVERTER WARNING: The .NET Type.FullName property will not always yield results identical to the Java Class.getName method:
			builder.Append(this.GetType().FullName).Append(" {\n\tcolor: ").Append(color).Append("\n\talpha: ").Append(alpha).Append("\n}");
			return builder.ToString();
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void unmarshal(jm2lib.io.UnmarshalingStream in) throws java.io.IOException, ClassNotFoundException
		public virtual void unmarshal(UnmarshalingStream @in)
		{
			color.unmarshal(@in);
			alpha.unmarshal(@in);
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void marshal(jm2lib.io.MarshalingStream out) throws java.io.IOException
		public virtual void marshal(MarshalingStream @out)
		{
			color.marshal(@out);
			alpha.marshal(@out);
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void writeContent(jm2lib.io.MarshalingStream out) throws InstantiationException, IllegalAccessException, java.io.IOException
		public virtual void writeContent(MarshalingStream @out)
		{
			color.writeContent(@out);
			alpha.writeContent(@out);
		}

		public virtual LERandomAccessFile[] AnimFiles
		{
			set
			{
				color.AnimFiles = value;
				alpha.AnimFiles = value;
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public jm2lib.blizzard.wow.classic.SubmeshAnimation downConvert(jm2lib.blizzard.common.types.ArrayRef<jm2lib.blizzard.wow.classic.Animation> animations) throws Exception
		public virtual jm2lib.blizzard.wow.classic.SubmeshAnimation downConvert(ArrayRef<jm2lib.blizzard.wow.classic.Animation> animations)
		{
			jm2lib.blizzard.wow.classic.SubmeshAnimation output = new jm2lib.blizzard.wow.classic.SubmeshAnimation();
			output.color = color.downConvert(animations);
			output.alpha = alpha.downConvert(animations);
			return output;
		}
	}

}