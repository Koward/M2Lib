using System;
using System.Collections.Generic;

namespace jm2lib.blizzard.wow.classic
{


	using CSVRecord = org.apache.commons.csv.CSVRecord;

	using jm2lib.blizzard.common.types;

	/// <summary>
	/// Utility static class to build a PlayableAnimationLookup from an
	/// AnimationLookup.
	/// 
	/// @author Koward
	/// 
	/// </summary>
	public sealed class LookupBuilder
	{
		// Extracted from AnimationData.dbc (Legion Beta Build 20810)
		public static short[] fallback;
		private const int ID_COLUMN = 0;
		private const int FALLBACK_COLUMN = 3;

		// Size from AnimationData.dbc (TBC 2.4.3)
		private const short NUMBER_OF_ACTIONS = 226;
		// Flags
		private static Set<short?> playThenStop;
		private static Set<short?> playBackwards;

		private const short PLAY_THEN_STOP = 3;
		private const short PLAY_BACKWARDS = 1;

		private const short DEAD = 6;
		private const short SIT_GROUND = 97;
		private const short SLEEP = 100;
		private const short KNEEL_LOOP = 115;
		private const short USE_STANDING_LOOP = 123;
		private const short DROWNED = 132;
		private const short LOOT_HOLD = 188;

		private const short WALK_BACKWARDS = 13;
		private const short SWIM_BACKWARDS = 45;
		private const short SLEEP_UP = 101;
		private const short LOOT_UP = 189;

		static LookupBuilder()
		{
			try
			{
				IList<CSVRecord> lines = M2Format.openCSV("AnimationData");
				int maxID = Convert.ToInt32(lines[lines.Count - 1].get(ID_COLUMN));
				fallback = new short[maxID + 1];
				for (int i = 1; i < lines.Count; i++)
				{
					fallback[Convert.ToInt32(lines[i].get(ID_COLUMN))] = Convert.ToInt16(lines[i].get(FALLBACK_COLUMN));
				}
				//FIXME There are loops in the fallback lookup in AnimationData.dbc. Close, FlyClose.
				fallback[146] = 0;
				fallback[375] = 0;

			}
			catch (IOException e)
			{
				Console.Error.WriteLine("Library bugged : Missing DBC file");
				Console.WriteLine(e.ToString());
				Console.Write(e.StackTrace);
				Environment.Exit(1);
			}

			playThenStop = new HashSet<short?>(Arrays.asList(DEAD, SIT_GROUND, SLEEP, KNEEL_LOOP, USE_STANDING_LOOP, DROWNED, LOOT_HOLD));
			playBackwards = new HashSet<short?>(Arrays.asList(WALK_BACKWARDS, SWIM_BACKWARDS, SLEEP_UP, LOOT_UP));
		}

		private LookupBuilder()
		{
		}

		private static short getRealID(short id, ArrayRef<short?> animationLookup)
		{
			if (id < animationLookup.Count && (animationLookup[id] > -1))
			{
				return id;
			}
			return getRealID(fallback[id], animationLookup);
		}

		/// <summary>
		/// Computes the Animations Lookup.
		/// 
		/// @return
		/// </summary>
		public static ArrayRef<short?> buildAnimLookup(ArrayRef<Animation> animations)
		{
			short?[] ids = new short?[animations.Count];
			for (int i = 0; i < animations.Count; i++)
			{
				ids[i] = animations[i].animationID;
			}
			return buildLookup(ids);
		}

		/// <summary>
		/// Builds a standard WoW lookup from the ids. </summary>
		/// <param name="ids">
		/// @return </param>
		public static ArrayRef<short?> buildLookup(short?[] ids)
		{
			ArrayRef<short?> lookup = new ArrayRef<short?>(short.TYPE);
			short maxID = Arrays.stream(ids).max(short?::compare).get();
			for (int i = 0; i < maxID + 1; i++)
			{
				lookup.Add((short) -1);
			}
			for (short i = 0; i < ids.Length; i++)
			{
				// ID says : "If there is not already a position in the lookup for my value, I set mine".
				if (lookup[ids[i]] == -1)
				{
					lookup[ids[i]] = i;
				}
			}
			return lookup;
		}

		/// <summary>
		/// Computes the Playable Animations Lookup.
		/// 
		/// @return
		/// </summary>
		public static ArrayRef<PlayableRecord> buildPlayAnimLookup(ArrayRef<short?> animationLookup)
		{
			ArrayRef<PlayableRecord> lookup = new ArrayRef<PlayableRecord>(typeof(PlayableRecord));
			for (short i = 0; i < NUMBER_OF_ACTIONS; i++)
			{
				PlayableRecord record = new PlayableRecord(getRealID(i, animationLookup), (short) 0);
				if (record.fallbackID != i)
				{
					if (playThenStop.contains(i))
					{
						record.flags = PLAY_THEN_STOP;
					}
					else if (playBackwards.contains(i))
					{
						record.flags = PLAY_BACKWARDS;
					}
				}
				lookup.Add(record);
			}
			return lookup;
		}
	}

}