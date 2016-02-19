using System.Collections.Generic;
using System.Text;

namespace jm2lib.blizzard.sc2
{


	using QuatF = jm2lib.blizzard.common.types.QuatF;
	using RGBA = jm2lib.blizzard.common.types.RGBA;
	using Vec2C = jm2lib.blizzard.common.types.Vec2C;
	using Vec2F = jm2lib.blizzard.common.types.Vec2F;
	using Vec3F = jm2lib.blizzard.common.types.Vec3F;
	using MarshalingStream = jm2lib.io.MarshalingStream;
	using UnmarshalingStream = jm2lib.io.UnmarshalingStream;

	public class STC : Indexer
	{
		internal Reference<sbyte?> name = new Reference<sbyte?>();
		internal char runsConcurrent;
		internal char priority;
		internal char stsIndex;
		internal char stsIndexCopy;
		internal Reference<int?> animIds = new Reference<int?>();
		internal Reference<Vec2C> animRefs = new Reference<Vec2C>();
		internal int unknown0;
		internal Reference<SD<Event>> sdev = new Reference<SD<Event>>();
		internal Reference<SD<Vec2F>> sd2v = new Reference<SD<Vec2F>>();
		internal Reference<SD<Vec3F>> sd3v = new Reference<SD<Vec3F>>();
		internal Reference<SD<QuatF>> sd4q = new Reference<SD<QuatF>>();
		internal Reference<SD<RGBA>> sdcc = new Reference<SD<RGBA>>();
		internal Reference<SD<float?>> sdr3 = new Reference<SD<float?>>();
//JAVA TO C# CONVERTER TODO TASK: Java wildcard generics are not converted to .NET:
//ORIGINAL LINE: Reference<SD<?>> unknown1 = new Reference<>();
		internal Reference<SD<?>> unknown1 = new Reference<SD<?>>();
		internal Reference<SD<char?>> sds6 = new Reference<SD<char?>>();
		internal Reference<SD<char?>> sdu6 = new Reference<SD<char?>>();
//JAVA TO C# CONVERTER TODO TASK: Java wildcard generics are not converted to .NET:
//ORIGINAL LINE: Reference<SD<?>> unknown2 = new Reference<>();
		internal Reference<SD<?>> unknown2 = new Reference<SD<?>>();
		internal Reference<SD<int?>> sdu3 = new Reference<SD<int?>>();
		internal Reference<SD<int?>> sdfg = new Reference<SD<int?>>();
		internal Reference<SD<BoundingSphere>> sdmb = new Reference<SD<BoundingSphere>>();

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void unmarshal(jm2lib.io.UnmarshalingStream in) throws java.io.IOException, ClassNotFoundException
		public virtual void unmarshal(UnmarshalingStream @in)
		{
			name.unmarshal(@in);
			runsConcurrent = @in.readChar();
			priority = @in.readChar();
			stsIndex = @in.readChar();
			stsIndexCopy = @in.readChar();
			animIds.unmarshal(@in);
			animRefs.unmarshal(@in);
			unknown0 = @in.readInt();
			sdev.unmarshal(@in);
			sd2v.unmarshal(@in);
			sd3v.unmarshal(@in);
			sd4q.unmarshal(@in);
			sdcc.unmarshal(@in);
			sdr3.unmarshal(@in);
			unknown1.unmarshal(@in);
			sds6.unmarshal(@in);
			sdu6.unmarshal(@in);
			unknown2.unmarshal(@in);
			sdu3.unmarshal(@in);
			sdfg.unmarshal(@in);
			sdmb.unmarshal(@in);
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void marshal(jm2lib.io.MarshalingStream out) throws java.io.IOException
		public virtual void marshal(MarshalingStream @out)
		{
			//TODO
		}

		public virtual List<IndexEntry> Entries
		{
			set
			{
				name.Entries = value;
				animIds.Entries = value;
				animRefs.Entries = value;
    
				sdev.Entries = value;
				sd2v.Entries = value;
				sd3v.Entries = value;
				sd4q.Entries = value;
				sdcc.Entries = value;
				sdr3.Entries = value;
				unknown1.Entries = value;
				sds6.Entries = value;
				sdu6.Entries = value;
				unknown2.Entries = value;
				sdu3.Entries = value;
				sdfg.Entries = value;
				sdmb.Entries = value;
			}
		}

//JAVA TO C# CONVERTER TODO TASK: Java wildcard generics are not converted to .NET:
//ORIGINAL LINE: public SD<?> getAnimationData(int animId)
		public virtual SD<?> getAnimationData(int animId)
		{
			int animRefsIndex = -1;
			for (int i = 0; i < animIds.size(); i++)
			{
				if (animIds.get(i) == animId)
				{
					animRefsIndex = i;
				}
			}
			if (animRefsIndex < 0)
			{
				return null;
			}
			Vec2C couple = animRefs.get(animRefsIndex);
			switch (couple.Y)
			{
			case 0:
				return sdev.get(couple.X);
			case 1:
				return sd2v.get(couple.X);
			case 2:
				return sd3v.get(couple.X);
			case 3:
				return sd4q.get(couple.X);
			case 4:
				return sdcc.get(couple.X);
			case 5:
				return sdr3.get(couple.X);
			case 6:
				return unknown1.get(couple.X);
			case 7:
				return sds6.get(couple.X);
			case 8:
				return sdu6.get(couple.X);
			case 9:
				return unknown2.get(couple.X);
			case 10:
				return sdu3.get(couple.X);
			case 11:
				return sdfg.get(couple.X);
			case 12:
				return sdmb.get(couple.X);
			default:
				return null;
			}
		}

		public override string ToString()
		{
			StringBuilder builder = new StringBuilder();
//JAVA TO C# CONVERTER WARNING: The .NET Type.FullName property will not always yield results identical to the Java Class.getName method:
			builder.Append(this.GetType().FullName).Append(" {\n\tname: ").Append(name.toNameString()).Append("\n\trunsConcurrent: ").Append(runsConcurrent).Append("\n\tpriority: ").Append(priority).Append("\n\tstsIndex: ").Append(stsIndex).Append("\n\tstsIndexCopy: ").Append(stsIndexCopy).Append("\n\tanimIds: ").Append(animIds).Append("\n\tanimRefs: ").Append(animRefs).Append("\n\tunknown0: ").Append(unknown0).Append("\n\tsdev: ").Append(sdev).Append("\n\tsd2v: ").Append(sd2v).Append("\n\tsd3v: ").Append(sd3v).Append("\n\tsd4q: ").Append(sd4q).Append("\n\tsdcc: ").Append(sdcc).Append("\n\tsdr3: ").Append(sdr3).Append("\n\tunknown1: ").Append(unknown1).Append("\n\tsds6: ").Append(sds6).Append("\n\tsdu6: ").Append(sdu6).Append("\n\tunknown2: ").Append(unknown2).Append("\n\tsdfg: ").Append(sdfg).Append("\n\tsdmb: ").Append(sdmb).Append("\n}");
			return builder.ToString();
		}

	}

}