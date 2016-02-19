using System;
using System.Collections.Generic;
using System.Text;

namespace jm2lib.blizzard.wc3.mdx
{


	using Animation = jm2lib.blizzard.wow.classic.Animation;
	using Marshalable = jm2lib.io.Marshalable;
	using MarshalingStream = jm2lib.io.MarshalingStream;
	using UnmarshalingStream = jm2lib.io.UnmarshalingStream;

	public class Sequence : Marshalable
	{
		public string name;
		public int timeStart;
		public int timeEnd;
		public float moveSpeed;
		public int flags;
		public float rarity;
		public int syncPoint;
		public Extent extent;

		public Sequence()
		{
			name = "Stand";
			extent = new Extent();
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void unmarshal(jm2lib.io.UnmarshalingStream in) throws java.io.IOException, ClassNotFoundException
		public virtual void unmarshal(UnmarshalingStream @in)
		{
			name = @in.readString(80);
			timeStart = @in.readInt();
			timeEnd = @in.readInt();
			moveSpeed = @in.readFloat();
			flags = @in.readInt();
			rarity = @in.readFloat();
			syncPoint = @in.readInt();
			extent.unmarshal(@in);
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void marshal(jm2lib.io.MarshalingStream out) throws java.io.IOException
		public virtual void marshal(MarshalingStream @out)
		{
			@out.writeString(name, 80);
			@out.writeInt(timeStart);
			@out.writeInt(timeEnd);
			@out.writeFloat(moveSpeed);
			@out.writeInt(flags);
			@out.writeFloat(rarity);
			@out.writeInt(syncPoint);
			extent.marshal(@out);
		}

		internal static readonly Dictionary<string, int?> NAME_TO_ID;
		static Sequence()
		{
			NAME_TO_ID = new Dictionary<>();
			// Generated from MDLVis
			NAME_TO_ID["Stand"] = 0;
			NAME_TO_ID["Death"] = 1;
			NAME_TO_ID["Spell"] = 2;
			NAME_TO_ID["Cinematic Stop"] = 3;
			NAME_TO_ID["Walk"] = 4;
			NAME_TO_ID["Walk Fast"] = 5;
			NAME_TO_ID["Cinematic Rise"] = 7;
			NAME_TO_ID["Stand Hit"] = 8;
			NAME_TO_ID["Stand Hit Large"] = 9;
			NAME_TO_ID["Stand Hit Critical"] = 10;
			NAME_TO_ID["Cinematic ShuffleLeft"] = 11;
			NAME_TO_ID["Cinematic ShuffleRight"] = 12;
			NAME_TO_ID["Cinematic WalkBackward"] = 13;
			NAME_TO_ID["Stand Wounded"] = 14;
			NAME_TO_ID["Cinematic HandsClosed"] = 15;
			NAME_TO_ID["Attack"] = 16;
			NAME_TO_ID["Attack Second"] = 18;
			NAME_TO_ID["Attack Third"] = 19;
			NAME_TO_ID["Stand Hit Medium"] = 20;
			NAME_TO_ID["Stand Hit Medium First"] = 21;
			NAME_TO_ID["Stand Hit Medium Second"] = 22;
			NAME_TO_ID["Stand Hit Medium Third"] = 23;
			NAME_TO_ID["Stand Hit Defend"] = 24;
			NAME_TO_ID["Stand Ready"] = 25;
			NAME_TO_ID["Stand Ready First"] = 26;
			NAME_TO_ID["Stand Ready Second"] = 27;
			NAME_TO_ID["Stand Ready Third"] = 28;
			NAME_TO_ID["Stand Gold Fourth"] = 29;
			NAME_TO_ID["Stand Hit Small"] = 30;
			NAME_TO_ID["Spell Second"] = 31;
			NAME_TO_ID["Spell Channel Second"] = 32;
			NAME_TO_ID["Spell Channel Second 2"] = 33;
			NAME_TO_ID["Cinematic Welcome"] = 34;
			NAME_TO_ID["Stand Hit 2"] = 36;
			NAME_TO_ID["Cinematic JumpStart"] = 37;
			NAME_TO_ID["Cinematic Jump"] = 38;
			NAME_TO_ID["Cinematic JumpEnd"] = 39;
			NAME_TO_ID["Cinematic Fall"] = 40;
			NAME_TO_ID["Stand Swim"] = 41;
			NAME_TO_ID["Walk Swim"] = 42;
			NAME_TO_ID["Walk Right Swim"] = 43;
			NAME_TO_ID["Walk Left Swim"] = 44;
			NAME_TO_ID["Cinematic SwimBackward"] = 45;
			NAME_TO_ID["Attack Fourth"] = 46;
			NAME_TO_ID["Attack Fourth 2"] = 47;
			NAME_TO_ID["Stand Gold Fifth"] = 48;
			NAME_TO_ID["Attack Fifth"] = 49;
			NAME_TO_ID["Stand Victory"] = 50;
			NAME_TO_ID["Spell Ready Throw"] = 51;
			NAME_TO_ID["Spell Ready"] = 52;
			NAME_TO_ID["Spell Throw"] = 53;
			NAME_TO_ID["Spell 2"] = 54;
			NAME_TO_ID["Spell Slam"] = 55;
			NAME_TO_ID["Stand Ready Second 2"] = 56;
			NAME_TO_ID["Attack Slam First"] = 57;
			NAME_TO_ID["Attack Slam Second"] = 58;
			NAME_TO_ID["Attack Off Two"] = 59;
			NAME_TO_ID["Portrait Talk"] = 60;
			NAME_TO_ID["Cinematic Eat"] = 61;
			NAME_TO_ID["Stand Work"] = 62;
			NAME_TO_ID["Portrait"] = 63;
			NAME_TO_ID["Portrait Talk 2"] = 64;
			NAME_TO_ID["Portrait Talk 3"] = 65;
			NAME_TO_ID["Cinematic EmoteBow"] = 66;
			NAME_TO_ID["Cinematic EmoteWave"] = 67;
			NAME_TO_ID["Cinematic EmoteCheer"] = 68;
			NAME_TO_ID["Cinematic EmoteDance"] = 69;
			NAME_TO_ID["Cinematic EmoteLaugh"] = 70;
			NAME_TO_ID["Cinematic EmoteSleep"] = 71;
			NAME_TO_ID["Attack First 2"] = 85;
			NAME_TO_ID["Attack Off"] = 87;
			NAME_TO_ID["Attack Off 2"] = 88;
			NAME_TO_ID["Attack Upgrade"] = 95;
			NAME_TO_ID["Stand Ready Fourth"] = 105;
			NAME_TO_ID["Stand Ready Fifth"] = 106;
			NAME_TO_ID["Attack Puke"] = 107;
			NAME_TO_ID["Stand Gold Alternate"] = 108;
			NAME_TO_ID["Stand Lumber Fourth"] = 109;
			NAME_TO_ID["Stand Lumber Fifth"] = 110;
			NAME_TO_ID["Stand Lumber Alternate"] = 111;
			NAME_TO_ID["Stand Ready Alternate"] = 112;
			NAME_TO_ID["Stand Victory 2"] = 113;
			NAME_TO_ID["Attack Off Alternate"] = 117;
			NAME_TO_ID["Attack Slam"] = 118;
			NAME_TO_ID["Walk Moderate"] = 119;
			NAME_TO_ID["Cinematic 13119235"] = 120;
			NAME_TO_ID["Stand Hit Slam"] = 121;
			NAME_TO_ID["Stand Channel Throw"] = 124;
			NAME_TO_ID["Spell Ready 2"] = 125;
			NAME_TO_ID["Attack Walk Stand Spin"] = 126;
			NAME_TO_ID["Birth"] = 127;
			NAME_TO_ID["Stand 2"] = 135;
			NAME_TO_ID["Walk Fast 2"] = 143;
			NAME_TO_ID["Walk 2"] = 144;
			NAME_TO_ID["Cinematic Close"] = 146;
			NAME_TO_ID["Cinematic Closed"] = 147;
			NAME_TO_ID["Stand Lumber"] = 158;
			NAME_TO_ID["Decay"] = 159;
			NAME_TO_ID["Cinematic BowPull"] = 160;
			NAME_TO_ID["Cinematic BowRelease"] = 161;
			NAME_TO_ID["Attack Slam Large"] = 181;
			NAME_TO_ID["Stand 3"] = 190;
			NAME_TO_ID["Stand Victory 3"] = 194;

			// Linked by hand
			NAME_TO_ID["Cinematic Scared"] = 225;
			NAME_TO_ID["Spell Channel"] = 125;
			NAME_TO_ID["Cinematic Sit"] = 97;
			NAME_TO_ID["Cinematic Sit Start"] = 96;
			NAME_TO_ID["Cinematic Sit End"] = 98;
			NAME_TO_ID["Cinematic Roar"] = 55;
			NAME_TO_ID["Cinematic Kneel"] = 115;
			NAME_TO_ID["Cinematic Kneel Start"] = 114;
			NAME_TO_ID["Cinematic Kneel End"] = 116;
			NAME_TO_ID["Cinematic Hover"] = 193;
			NAME_TO_ID["Stand Channel"] = 124;
			NAME_TO_ID["Cinematic Mount Stand"] = 91;
		}

		/// <summary>
		/// Converts a WC3 sequence to a WoW animations.
		/// Other fields depends on the global list of animations and are computed by {@code Animation#linkAnimations(java.util.ArrayList)} </summary>
		/// <param name="blendTime">
		/// @return </param>
		public virtual Animation upConvert(int blendTime)
		{
			Animation output = new Animation();
			string sequenceToken = name.Split(" - ", true)[0];
			if (NAME_TO_ID.ContainsKey(sequenceToken))
			{
				output.animationID = (short)NAME_TO_ID[sequenceToken];
			}
			else
			{
				Console.Error.WriteLine("\"" + sequenceToken + "\" has not yet been linked to a World of Warcraft animation.");
				output.animationID = 213;
			}
			output.timeStart = timeStart;
			output.timeEnd = timeEnd;
			output.movingSpeed = moveSpeed;
			output.flags = flags;
			output.probability = (short)(rarity * short.MaxValue);
			output.blendTime = blendTime;
			output.minimumExtent = extent.minimum;
			output.maximumExtent = extent.maximum;
			output.boundRadius = extent.boundsRadius;
			return output;

		}

		public override string ToString()
		{
			StringBuilder builder = new StringBuilder();
//JAVA TO C# CONVERTER WARNING: The .NET Type.FullName property will not always yield results identical to the Java Class.getName method:
			builder.Append(this.GetType().FullName).Append(" {\n\tname: ").Append(name).Append("\n\ttimeStart: ").Append(timeStart).Append("\n\ttimeEnd: ").Append(timeEnd).Append("\n\tmoveSpeed: ").Append(moveSpeed).Append("\n\tflags: ").Append(flags).Append("\n\trarity: ").Append(rarity).Append("\n\tsyncPoint: ").Append(syncPoint).Append("\n\textent: ").Append(extent).Append("\n}");
			return builder.ToString();
		}
	}
}