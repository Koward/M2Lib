using System.Text;

namespace jm2lib.blizzard.sc2
{


	using Marshalable = jm2lib.io.Marshalable;
	using MarshalingStream = jm2lib.io.MarshalingStream;
	using UnmarshalingStream = jm2lib.io.UnmarshalingStream;

	public class SSGS : Marshalable
	{
		internal int shape;
		internal short bone;
		internal char unknown0;
		internal float[] matrix = new float[16];
		internal int[] unknown1To6 = new int[6];
		internal float size0;
		internal float size1;
		internal float size2;

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void unmarshal(jm2lib.io.UnmarshalingStream in) throws java.io.IOException, ClassNotFoundException
		public virtual void unmarshal(UnmarshalingStream @in)
		{
			shape = @in.readInt();
			bone = @in.readShort();
			unknown0 = @in.readChar();
			for (int i = 0; i < matrix.Length; i++)
			{
				matrix[i] = @in.readFloat();
			}
			for (int i = 0; i < unknown1To6.Length; i++)
			{
				unknown1To6[i] = @in.readInt();
			}
			size0 = @in.readFloat();
			size1 = @in.readFloat();
			size2 = @in.readFloat();

		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void marshal(jm2lib.io.MarshalingStream out) throws java.io.IOException
		public virtual void marshal(MarshalingStream @out)
		{
			// TODO Auto-generated method stub

		}

		public override string ToString()
		{
			StringBuilder builder = new StringBuilder();
//JAVA TO C# CONVERTER WARNING: The .NET Type.FullName property will not always yield results identical to the Java Class.getName method:
			builder.Append(this.GetType().FullName).Append(" {\n\tshape: ").Append(shape).Append("\n\tbone: ").Append(bone).Append("\n\tunknown0: ").Append((int) unknown0).Append("\n\tmatrix: ").Append(Arrays.ToString(matrix)).Append("\n\tunknown1To6: ").Append(Arrays.ToString(unknown1To6)).Append("\n\tsize0: ").Append(size0).Append("\n\tsize1: ").Append(size1).Append("\n\tsize2: ").Append(size2).Append("\n}");
			return builder.ToString();
		}

	}

}