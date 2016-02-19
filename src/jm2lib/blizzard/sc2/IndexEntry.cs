using System.Collections.Generic;
using System.Text;

namespace jm2lib.blizzard.sc2
{


	using QuatF = jm2lib.blizzard.common.types.QuatF;
	using RGBA = jm2lib.blizzard.common.types.RGBA;
	using Vec2F = jm2lib.blizzard.common.types.Vec2F;
	using Vec3F = jm2lib.blizzard.common.types.Vec3F;
	using Marshalable = jm2lib.io.Marshalable;
	using MarshalingStream = jm2lib.io.MarshalingStream;
	using UnmarshalingStream = jm2lib.io.UnmarshalingStream;

	public class IndexEntry : List<object>, Marshalable
	{
		private const long serialVersionUID = 6217022338565486583L;
		public bool implemented = true;
		internal string tag;
		internal int offset;
		internal int entries;
		internal int version;

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void unmarshal(jm2lib.io.UnmarshalingStream in) throws java.io.IOException, ClassNotFoundException
		public virtual void unmarshal(UnmarshalingStream @in)
		{
			tag = @in.readString(4);
			offset = @in.readInt();
			entries = @in.readInt();
			this.Capacity = entries;
			version = @in.readInt();

			long currentOffset = @in.FilePointer;
			@in.seek(offset);
			for (int i = 0; i < entries; i++)
			{
				object obj;
//JAVA TO C# CONVERTER TODO TASK: There is no .NET StringBuilder equivalent to the Java 'reverse' method:
				switch ((new StringBuilder(tag)).reverse().ToString())
				{
				case "VEC2":
					obj = new Vec2F();
					break;
				case "VEC3":
					obj = new Vec3F();
					break;
				case "VEC4":
				case "QUAT":
					obj = new QuatF();
					break;
				case "CHAR":
				case "U8__":
					obj = @in.readByte();
					break;
				case "U16_":
					obj = @in.readShort();
					break;
				case "U32_":
				case "I32_":
				case "FLAG":
					obj = @in.readInt();
					break;
				case "MODL":
					obj = new Header(version);
					break;
				case "SEQS":
					obj = new Sequence(version);
					break;
				case "STC_":
					obj = new STC();
					break;
				case "SDEV":
					obj = new SD<Event>();
					break;
				case "SD2V":
					obj = new SD<Vec2F>();
					break;
				case "SD3V":
					obj = new SD<Vec3F>();
					break;
				case "SD4Q":
					obj = new SD<QuatF>();
					break;
				case "SDCC":
					obj = new SD<RGBA>();
					break;
				case "SDR3":
					obj = new SD<float?>();
					break;
				case "SDS6":
					obj = new SD<short?>();
					break;
				case "SDU6":
					obj = new SD<char?>();
					break;
				case "SDFG":
					obj = new SD<int?>();
					break;
				case "SDMB":
					obj = new SD<BoundingSphere>();
					break;
				case "STG_":
					obj = new STG();
					break;
				case "BONE":
					obj = new Bone();
					break;
				default:
					implemented = false;
					obj = new object();
				break;
				}
				if (obj is Marshalable)
				{
					((Marshalable) obj).unmarshal(@in);
				}
				this.Add(obj);
			}
			@in.seek(currentOffset);
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void marshal(jm2lib.io.MarshalingStream out) throws java.io.IOException
		public virtual void marshal(MarshalingStream @out)
		{
			//TODO
			@out.writeString(tag, 4);
			@out.writeInt(offset);
			@out.writeInt(entries);
			@out.writeInt(version);
		}

		public override string ToString()
		{
			if (!implemented)
			{
				return "?";
			}

			StringBuilder result = new StringBuilder();
			string NEW_LINE = System.getProperty("line.separator");
			result.Append("[number: " + this.Count + "]");
			if (this.Count != 0)
			{
				for (int i = 0; i < this.Count; i++)
				{
					result.Append("[" + i + "] ");
					result.Append(this[i]);
				}
				result.Append(NEW_LINE);
			}
			return result.ToString();
		}
	}

}