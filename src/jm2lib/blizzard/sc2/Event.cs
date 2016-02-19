using System.Collections.Generic;
using System.Text;

namespace jm2lib.blizzard.sc2
{


	using QuatF = jm2lib.blizzard.common.types.QuatF;
	using MarshalingStream = jm2lib.io.MarshalingStream;
	using UnmarshalingStream = jm2lib.io.UnmarshalingStream;

	public class Event : Versioned, Indexer
	{
		internal Reference<sbyte?> name;
		internal int unknown0;
		internal int unknown1;
		internal QuatF[] matrix;
		internal int unknown2;
		internal int unknown3;
		internal int unknown4;
		internal int unknown5;
		internal int unknown6;
		internal int unknown7;
		internal int unknown8;

		public Event()
		{
			matrix = new QuatF[4];
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void unmarshal(jm2lib.io.UnmarshalingStream in) throws java.io.IOException, ClassNotFoundException
		public override void unmarshal(UnmarshalingStream @in)
		{
			name.unmarshal(@in);
			unknown0 = @in.readInt();
			unknown1 = @in.readInt();
			foreach (QuatF quat in matrix)
			{
				quat = new QuatF();
				quat.unmarshal(@in);
			}
			unknown2 = @in.readInt();
			unknown3 = @in.readInt();
			unknown4 = @in.readInt();
			unknown5 = @in.readInt();
			if (version > 0)
			{
				unknown6 = @in.readInt();
			}
			if (version > 1)
			{
				unknown7 = @in.readInt();
			}
			unknown8 = @in.readInt();
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void marshal(jm2lib.io.MarshalingStream out) throws java.io.IOException
		public override void marshal(MarshalingStream @out)
		{
			name.marshal(@out);
			@out.writeInt(unknown0);
			@out.writeInt(unknown1);
			foreach (QuatF quat in matrix)
			{
				quat.marshal(@out);
			}
			@out.writeInt(unknown2);
			@out.writeInt(unknown3);
			@out.writeInt(unknown4);
			@out.writeInt(unknown5);
			if (version > 0)
			{
				@out.writeInt(unknown6);
			}
			if (version > 1)
			{
				@out.writeInt(unknown7);
			}
			@out.writeInt(unknown8);
		}

		public virtual List<IndexEntry> Entries
		{
			set
			{
				name.Entries = value;
			}
		}

		public override string ToString()
		{
			StringBuilder builder = new StringBuilder();
//JAVA TO C# CONVERTER WARNING: The .NET Type.FullName property will not always yield results identical to the Java Class.getName method:
			builder.Append(this.GetType().FullName).Append(" {\n\tname: ").Append(name).Append("\n\tunknown0: ").Append(unknown0).Append("\n\tunknown1: ").Append(unknown1).Append("\n\tmatrix: ").Append(Arrays.ToString(matrix)).Append("\n\tunknown2: ").Append(unknown2).Append("\n\tunknown3: ").Append(unknown3).Append("\n\tunknown4: ").Append(unknown4).Append("\n\tunknown5: ").Append(unknown5).Append("\n\tunknown6: ").Append(unknown6).Append("\n\tunknown7: ").Append(unknown7).Append("\n\tunknown8: ").Append(unknown8).Append("\n}");
			return builder.ToString();
		}

	}

}