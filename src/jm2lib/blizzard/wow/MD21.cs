namespace jm2lib.blizzard.wow
{

	using jm2lib.blizzard.common.types;
	using BlizzardFile = jm2lib.blizzard.io.BlizzardFile;
	using BlizzardInputStream = jm2lib.blizzard.io.BlizzardInputStream;
	using BlizzardOutputStream = jm2lib.blizzard.io.BlizzardOutputStream;
	using AnimFileID = jm2lib.blizzard.wow.legion.AnimFileID;
	using MarshalingStream = jm2lib.io.MarshalingStream;
	using UnmarshalingStream = jm2lib.io.UnmarshalingStream;

	/// <summary>
	/// Represents Legion chunked model. 
	/// Note that MD21 don't really have "other chunks" inside. It's just a hack to identify the file.
	/// Let's pray the first chunk is indeed MD21.
	/// @author Koward
	/// 
	/// </summary>
	public class MD21 : BlizzardFile
	{
		private M2 model;
		public Chunk<int?> physFileID;
		public Chunk<int?> skinFileID;
		public Chunk<AnimFileID> animFileID;
		public Chunk<int?> boneFileID;

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void unmarshal(jm2lib.io.UnmarshalingStream in) throws java.io.IOException, ClassNotFoundException
		public virtual void unmarshal(UnmarshalingStream @in)
		{
			int size = @in.readInt();
			long endOffset = @in.FilePointer + size;

			model = (M2)((BlizzardInputStream) @in).readTared();
			@in.seek(endOffset);
			while (@in.FilePointer < @in.length())
			{
				string magic = StringHelperClass.NewString(new sbyte[] {@in.readByte(), @in.readByte(), @in.readByte(), @in.readByte()});
				switch (magic)
				{
				case "PFID":
					physFileID = new Chunk<>(int.TYPE, magic);
					physFileID.unmarshal(@in);
					break;
				case "SFID":
					skinFileID = new Chunk<>(int.TYPE, magic);
					skinFileID.unmarshal(@in);
					break;
				case "AFID":
					animFileID = new Chunk<>(typeof(AnimFileID), magic);
					animFileID.unmarshal(@in);
					break;
				case "BFID":
					boneFileID = new Chunk<>(int.TYPE, magic);
					boneFileID.unmarshal(@in);
					break;
				default:
					@in.seek(@in.FilePointer + @in.readInt());
				break;
				}
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void marshal(jm2lib.io.MarshalingStream out) throws java.io.IOException
		public virtual void marshal(MarshalingStream @out)
		{
			if (model != null)
			{
			long sizeOffset = @out.FilePointer;
			@out.writeInt(0);
			((BlizzardOutputStream) @out).writeTared(model);

			int size = (int)(@out.FilePointer - sizeOffset);
			long currentOffset = @out.FilePointer;
			@out.seek(sizeOffset);
			@out.writeInt(size);
			@out.seek(currentOffset);
			}
			if (physFileID != null)
			{
				physFileID.marshal(@out);
			}

		}

		public virtual M2 M2
		{
			get
			{
				return model;
			}
			set
			{
				this.model = value;
				//TODO Generate other chunks
			}
		}

	}

}