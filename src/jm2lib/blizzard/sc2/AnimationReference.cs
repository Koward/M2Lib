using System;

namespace jm2lib.blizzard.sc2
{

	using jm2lib.blizzard.common.types;
	using jm2lib.blizzard.wow.lichking;
	using Marshalable = jm2lib.io.Marshalable;
	using MarshalingStream = jm2lib.io.MarshalingStream;
	using UnmarshalingStream = jm2lib.io.UnmarshalingStream;

	public class AnimationReference<T> : Marshalable
	{
		internal Type type;

		internal char interpolationType;
		internal int animId;
		internal T initValue;
		internal T nullValue;
		internal int unknown0;

		public AnimationReference(Type type)
		{
			this.type = type;
		}

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @SuppressWarnings("unchecked") @Override public void unmarshal(jm2lib.io.UnmarshalingStream in) throws java.io.IOException, ClassNotFoundException
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
		public virtual void unmarshal(UnmarshalingStream @in)
		{
			interpolationType = @in.readChar();
			animId = @in.readInt();
			initValue = (T) @in.readGeneric(type);
			nullValue = (T) @in.readGeneric(type);
			unknown0 = @in.readInt();
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void marshal(jm2lib.io.MarshalingStream out) throws java.io.IOException
		public virtual void marshal(MarshalingStream @out)
		{
			// TODO Auto-generated method stub

		}

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @SuppressWarnings("unchecked") public jm2lib.blizzard.wow.lichking.AnimationBlock<T> toWoW(Reference<STG> stgList, Reference<STC> stcList)
		public virtual AnimationBlock<T> toWoW(Reference<STG> stgList, Reference<STC> stcList)
		{
			AnimationBlock<T> output = new AnimationBlock<T>(type);
			foreach (STG stg in stgList)
			{
				SD<T> sd = null;
				ArrayRef<int?> timestamps = new ArrayRef<int?>(int.TYPE);
				ArrayRef<T> values = new ArrayRef<T>(type);
				for (int i = 0; i < stg.stcIndices.size(); i++)
				{
					SD<T> tmp = (SD<T>) stcList.get(i).getAnimationData(animId);
					if (tmp != null)
					{
						sd = tmp;
						break;
					}
				}
				if (sd != null)
				{
					timestamps.AddRange(sd.timestamps);
					values.AddRange(sd.values);
				}
				else
				{
					timestamps.Add(0);
					values.Add(initValue);
				}
			}
			output.interpolationType = (short) interpolationType;
			return output;
		}
	}

}