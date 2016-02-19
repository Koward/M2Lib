using System.Collections.Generic;

namespace jm2lib.blizzard.sc2
{


	using jm2lib.blizzard.common.types;
	using BlizzardFile = jm2lib.blizzard.io.BlizzardFile;
	using Model = jm2lib.blizzard.wow.cataclysm.Model;
	using LookupBuilder = jm2lib.blizzard.wow.classic.LookupBuilder;
	using MarshalingStream = jm2lib.io.MarshalingStream;
	using UnmarshalingStream = jm2lib.io.UnmarshalingStream;

	/// <summary>
	/// M3 model format
	/// 
	/// </summary>
	public class M3 : BlizzardFile
	{
		internal List<IndexEntry> indexEntries = new List<IndexEntry>();
		internal Header model;

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void unmarshal(jm2lib.io.UnmarshalingStream in) throws java.io.IOException, ClassNotFoundException
		public virtual void unmarshal(UnmarshalingStream @in)
		{
			int indexOffset = @in.readInt();
			int nIndexEntries = @in.readInt();
			Reference<Header> headers = new Reference<Header>();
			headers.unmarshal(@in);

			long currentOffset = @in.FilePointer;
			@in.seek(indexOffset);
			for (int i = 0; i < nIndexEntries; i++)
			{
				indexEntries.Add(new IndexEntry());
				indexEntries[i].unmarshal(@in);
			}
			@in.seek(currentOffset);

			headers.Entries = indexEntries;

			model = headers.get(0); // So far a model with more than 1 MODL chunk has never been seen.
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void marshal(jm2lib.io.MarshalingStream out) throws java.io.IOException
		public virtual void marshal(MarshalingStream @out)
		{
			long beginOffset = @out.FilePointer;
			@out.writeInt(0);
			@out.writeInt(indexEntries.Count);

			long currentOffset = @out.FilePointer;
			@out.seek(beginOffset);
			@out.writeInt((int) currentOffset);
			@out.seek(currentOffset);
			for (int i = 0; i < indexEntries.Count; i++)
			{
				indexEntries[i].marshal(@out);
			}
		}

		public virtual Model toWoW()
		{
			Model output = new Model();
			output.name = new ArrayRef<sbyte?>(model.name.toNameString());

			foreach (Sequence seq in model.sequences)
			{
				output.animations.add(seq.toWoW());
			}
			short?[] ids = new short?[output.animations.size()];
			for (int i = 0; i < output.animations.size(); i++)
			{
				ids[i] = output.animations.get(i).animationID;
			}
			output.animationLookup = LookupBuilder.buildLookup(ids);

			return output;
		}

		public override string ToString()
		{
			return model.ToString();
		}
	}

}