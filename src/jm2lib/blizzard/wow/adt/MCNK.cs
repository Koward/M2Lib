namespace jm2lib.blizzard.wow.adt
{

	using jm2lib.blizzard.common.types;
	using Vec3F = jm2lib.blizzard.common.types.Vec3F;
	using Marshalable = jm2lib.io.Marshalable;
	using MarshalingStream = jm2lib.io.MarshalingStream;
	using UnmarshalingStream = jm2lib.io.UnmarshalingStream;

	public class MCNK : Marshalable
	{
		internal int flags;
		internal int indexX;
		internal int indexY;
		internal int nLayers;
		internal int nDoodadRefs;
		internal Chunk<sbyte?> height;
		internal Chunk<sbyte?> normal;
		internal Chunk<sbyte?> layer;
		internal Chunk<sbyte?> refs;
		internal Chunk<sbyte?> alpha;
		internal int sizeAlpha;
		internal Chunk<sbyte?> shadow;
		internal int sizeShadow;
		internal int areaID;
		internal int nMapObjRefs;
		internal char holesLowRes;
		internal char unknownButUsed;
		internal int[] ReallyLowQualityTexturingMap; //4
		internal int predTex;
		internal int noEffectDoodad;
		internal Chunk<sbyte?> sndEmitters;
		internal int nSndEmitters;
		internal Chunk<sbyte?> liquid;
		internal int sizeLiquid;
		internal Vec3F position;
		internal Chunk<sbyte?> mccv;
		internal Chunk<sbyte?> mclv;
		internal int unused;


//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void unmarshal(jm2lib.io.UnmarshalingStream in) throws java.io.IOException, ClassNotFoundException
		public virtual void unmarshal(UnmarshalingStream @in)
		{
			flags = @in.readInt();
			indexX = @in.readInt();
			indexY = @in.readInt();

		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void marshal(jm2lib.io.MarshalingStream out) throws java.io.IOException
		public virtual void marshal(MarshalingStream @out)
		{
			// TODO Auto-generated method stub

		}

	}

}