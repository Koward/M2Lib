using System;
using System.Collections.Generic;
using System.Text;

namespace jm2lib.blizzard.wc3.mdx
{


	using jm2lib.blizzard.wow.classic;
	using Marshalable = jm2lib.io.Marshalable;
	using MarshalingStream = jm2lib.io.MarshalingStream;
	using UnmarshalingStream = jm2lib.io.UnmarshalingStream;

	/// <summary>
	/// Represent key frame pairs for animations. Equivalent to WoW AnimationBlocks structures.
	/// @author Koward
	/// 
	/// </summary>
	public class Track<T> : Marshalable
	{
		/// <summary>
		/// The type of the content </summary>
		private readonly Type type;
		private readonly string magic;

		public int interpolationType;
		public int globalSequence;
		public List<KeyFrame> keyFrames;
		public Track(Type type, string magic)
		{
			this.type = type;
			this.magic = magic;
			interpolationType = 0;
			globalSequence = -1;
			keyFrames = new List<>();
		}

		public virtual List<int?> Timestamps
		{
			get
			{
				List<int?> timestamps = new List<int?>();
				foreach (KeyFrame keyFrame in keyFrames)
				{
					timestamps.Add(keyFrame.timestamp);
				}
				return timestamps;
			}
		}

		public virtual List<T> Values
		{
			get
			{
				List<T> values = new List<T>();
				foreach (KeyFrame keyFrame in keyFrames)
				{
					values.Add(keyFrame.value);
				}
				return values;
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public final void unmarshal(jm2lib.io.UnmarshalingStream in) throws java.io.IOException, ClassNotFoundException
		public void unmarshal(UnmarshalingStream @in)
		{
			int tracksCount = @in.readInt();
			interpolationType = @in.readInt();
			globalSequence = @in.readInt();
			for (int i = 0; i < tracksCount; i++)
			{
				keyFrames.Add(new KeyFrame(this));
				keyFrames[i].unmarshal(@in);
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public final void marshal(jm2lib.io.MarshalingStream out) throws java.io.IOException
		public void marshal(MarshalingStream @out)
		{
			@out.write(magic.GetBytes(StandardCharsets.UTF_8));
			@out.writeInt(keyFrames.Count);
			@out.writeInt(interpolationType);
			@out.writeInt(globalSequence);
			for (int i = 0; i < keyFrames.Count; i++)
			{
				keyFrames[i].marshal(@out);
			}
		}

		public virtual AnimationBlock<T> upConvert()
		{
			AnimationBlock<T> output = new AnimationBlock<T>(type);
			output.interpolationType = (short) interpolationType;
			output.globalSequence = (short) globalSequence;
			//TODO Ranges
			output.timestamps.AddRange(Timestamps);
			output.values.AddRange(Values);
			return output;
		}

		public override string ToString()
		{
			StringBuilder builder = new StringBuilder();
//JAVA TO C# CONVERTER WARNING: The .NET Type.FullName property will not always yield results identical to the Java Class.getName method:
			builder.Append(this.GetType().FullName).Append(" {\n\ttype: ").Append(type).Append("\n\tinterpolationType: ").Append(interpolationType).Append("\n\tglobalSequence: ").Append(globalSequence).Append("\n\tkeyFrames: ").Append(keyFrames).Append("\n}");
			return builder.ToString();
		}

		private class KeyFrame : Marshalable
		{
			private readonly Track<T> outerInstance;

			internal int timestamp;
			internal T value;
			internal T inTan;
			internal T outTan;

			internal KeyFrame(Track<T> outerInstance)
			{
				this.outerInstance = outerInstance;
			}

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @SuppressWarnings("unchecked") public void unmarshal(jm2lib.io.UnmarshalingStream in) throws java.io.IOException, ClassNotFoundException
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
			public virtual void unmarshal(UnmarshalingStream @in)
			{
				timestamp = @in.readInt();
				value = (T) @in.readGeneric(outerInstance.type);
				if (outerInstance.interpolationType > 1)
				{
					inTan = (T) @in.readGeneric(outerInstance.type);
					outTan = (T) @in.readGeneric(outerInstance.type);
				}
			}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void marshal(jm2lib.io.MarshalingStream out) throws java.io.IOException
			public virtual void marshal(MarshalingStream @out)
			{
				@out.writeInt(timestamp);
				@out.writeGeneric(outerInstance.type, value);
				if (outerInstance.interpolationType > 1)
				{
					@out.writeGeneric(outerInstance.type, inTan);
					@out.writeGeneric(outerInstance.type, outTan);
				}
			}

			public override string ToString()
			{
				StringBuilder builder = new StringBuilder();
				builder.Append(" \tkf(").Append(timestamp).Append(":").Append(value + ") ");
				if (inTan != null)
				{
					builder.Append("\tinTan: ").Append(inTan);
				}
				if (outTan != null)
				{
					builder.Append("\toutTan: ").Append(outTan);
				}
				builder.Append("\n");
				return builder.ToString();
			}
		}
	}

}