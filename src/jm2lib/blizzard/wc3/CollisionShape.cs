using System.Text;

namespace jm2lib.blizzard.wc3
{


	using Node = jm2lib.blizzard.wc3.mdx.Node;
	using Marshalable = jm2lib.io.Marshalable;
	using MarshalingStream = jm2lib.io.MarshalingStream;
	using UnmarshalingStream = jm2lib.io.UnmarshalingStream;

	public class CollisionShape : Marshalable
	{
		internal Node node = new Node();
		internal int type;
		internal float[][] vertices = new float[3][];
		internal float radius; //if t == 3 || t == 2

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void unmarshal(jm2lib.io.UnmarshalingStream in) throws java.io.IOException, ClassNotFoundException
		public virtual void unmarshal(UnmarshalingStream @in)
		{
			node.unmarshal(@in);
			type = @in.readInt();
			for (int i = 0; i < vertices.Length; i++)
			{
				if (type == 0 || type == 1 || type == 3)
				{
					vertices[i] = new float[2];
				}
				else if (type == 2)
				{
					vertices[i] = new float[1];
				}
				for (int j = 0; j < vertices[i].Length; j++)
				{
					vertices[i][j] = @in.readFloat();
				}
			}
			if (type == 2 || type == 3)
			{
				radius = @in.readFloat();
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void marshal(jm2lib.io.MarshalingStream out) throws java.io.IOException
		public virtual void marshal(MarshalingStream @out)
		{
			node.marshal(@out);
			@out.writeInt(type);
			for (int i = 0; i < vertices.Length; i++)
			{
				for (int j = 0; j < vertices[i].Length; j++)
				{
					@out.writeFloat(vertices[i][j]);
				}
			}
		}

		public override string ToString()
		{
			StringBuilder builder = new StringBuilder();
//JAVA TO C# CONVERTER WARNING: The .NET Type.FullName property will not always yield results identical to the Java Class.getName method:
			builder.Append(this.GetType().FullName).Append(" {\n\tnode: ").Append(node).Append("\n\ttype: ").Append(type).Append("\n\tvertices: ").Append(Arrays.ToString(vertices)).Append("\n\tradius: ").Append(radius).Append("\n}");
			return builder.ToString();
		}
	}

}