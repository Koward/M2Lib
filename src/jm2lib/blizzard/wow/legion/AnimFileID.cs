using System.Text;

namespace jm2lib.blizzard.wow.legion
{

	using Marshalable = jm2lib.io.Marshalable;
	using MarshalingStream = jm2lib.io.MarshalingStream;
	using UnmarshalingStream = jm2lib.io.UnmarshalingStream;

	public class AnimFileID : Marshalable
	{
		public char animID;
		public char subAnimID;
		public int fileID;

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void unmarshal(jm2lib.io.UnmarshalingStream in) throws java.io.IOException, ClassNotFoundException
		public virtual void unmarshal(UnmarshalingStream @in)
		{
			animID = @in.readChar();
			subAnimID = @in.readChar();
			fileID = @in.readInt();
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void marshal(jm2lib.io.MarshalingStream out) throws java.io.IOException
		public virtual void marshal(MarshalingStream @out)
		{
			@out.writeChar(animID);
			@out.writeChar(subAnimID);
			@out.writeInt(fileID);
		}

		public override string ToString()
		{
			StringBuilder builder = new StringBuilder();
			builder.Append("AnimFileID [animID=").Append((int) animID).Append(", subAnimID=").Append((int) subAnimID).Append(", fileID=").Append(fileID).Append("]");
			return builder.ToString();
		}
	}
}