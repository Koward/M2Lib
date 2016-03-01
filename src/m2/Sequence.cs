using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using m2lib_csharp.interfaces;
using m2lib_csharp.types;
using Microsoft.VisualBasic.FileIO;

namespace m2lib_csharp.m2
{
    public class Sequence : IMarshalable
    {
        public ushort AnimationId { get; set; }
        public ushort SubAnimationId { get; set; }
        public uint Length { get; set; }
        public float MovingSpeed { get; set; }
        public SequenceFlags Flags { get; set; } = 0;
        public short Probability { get; set; } = short.MaxValue;
        private ushort _padding;
        public uint MinimumRepetitions { get; set; }
        public uint MaximumRepetitions { get; set; }
        public ushort BlendTimeStart { get; set; } = 150;
        public ushort BlendTimeEnd { get; set; } = 150;
        public CAaBox Bounds { get; set; } = new CAaBox();
        public float BoundRadius { get; set; }
        public short NextAnimation { get; set; } = -1;
        public ushort AliasNext { get; set; }

        /// <summary>
        /// Used to convert to one-timeline animation style
        /// </summary>
        public uint TimeStart { get; set; }

        public void Load(BinaryReader stream, M2.Format version = M2.Format.Unknown)
        {
            Debug.Assert(version != M2.Format.Unknown);
            AnimationId = stream.ReadUInt16();
            SubAnimationId = stream.ReadUInt16();
            if (version >= M2.Format.LichKing)
            {
                Length = stream.ReadUInt32();
            }
            else
            {
                var timeStart = stream.ReadUInt32();
                var timeEnd = stream.ReadUInt32();
                Length = timeEnd - timeStart;
            }
            MovingSpeed = stream.ReadSingle();
            Flags = (SequenceFlags) stream.ReadUInt32();
            Probability = stream.ReadInt16();
            _padding = stream.ReadUInt16();
            MinimumRepetitions = stream.ReadUInt32();
            MaximumRepetitions = stream.ReadUInt32();
            if (version >= M2.Format.Legion)
            {
                BlendTimeStart = stream.ReadUInt16();
                BlendTimeEnd = stream.ReadUInt16();
            }
            else
            {
                var blendTime = stream.ReadUInt32();
                BlendTimeStart = (ushort) blendTime;
                BlendTimeEnd = (ushort) blendTime;
            }
            Bounds.Load(stream);
            BoundRadius = stream.ReadSingle();
            NextAnimation = stream.ReadInt16();
            AliasNext = stream.ReadUInt16();
        }

        public void Save(BinaryWriter stream, M2.Format version = M2.Format.Unknown)
        {
            Debug.Assert(version != M2.Format.Unknown);
            stream.Write(AnimationId);
            stream.Write(SubAnimationId);
            if (version >= M2.Format.LichKing)
            { 
                stream.Write(Length);
            }
            else
            {
                stream.Write(TimeStart);
                stream.Write(TimeStart + Length);
            }
            stream.Write(MovingSpeed);
            stream.Write((uint) Flags);
            stream.Write(Probability);
            stream.Write(_padding);
            stream.Write(MinimumRepetitions);
            stream.Write(MaximumRepetitions);
            if (version >= M2.Format.Legion)
            {
                stream.Write(BlendTimeStart);
                stream.Write(BlendTimeEnd);
            }
            else
            {
                stream.Write((BlendTimeStart + BlendTimeEnd) / 2);
            }
            Bounds.Save(stream);
            stream.Write(BoundRadius);
            stream.Write(NextAnimation);
            stream.Write(AliasNext);
        }

        [Flags]
        public enum SequenceFlags
        {
            RuntimeBlended = 0x01,
            LowPriority = 0x10,
            Looped = 0x20,
            HasNext = 0x40,
            Blended = 0x80,
            Stored = 0x100,
            AnimFile = LowPriority + Looped + Stored
        }

        public string GetAnimFilePath(string path)
        {
            var pathDirectory = Path.GetDirectoryName(path);
            var pathWithoutExt = Path.GetFileNameWithoutExtension(path);
            string animFileName = $"{pathWithoutExt}{AnimationId:0000}-{SubAnimationId:00}.anim";
            Debug.Assert(pathDirectory != null, "pathDirectory != null");
            return Path.Combine(pathDirectory, animFileName);
        }

        /// <summary>
        /// Return the animation that's really implemented (handle aliases).
        /// </summary>
        /// <param name="sequences">List of sequences to navigate in.</param>
        /// <returns></returns>
        public Sequence GetRealSequence(IReadOnlyList<Sequence> sequences)
        {
            return Flags.HasFlag(SequenceFlags.HasNext) ? sequences[AliasNext].GetRealSequence(sequences) : this;
        }

        // ANIMATION LOOKUP

        public static ArrayRef<short> GenerateAnimationLookup(ArrayRef<Sequence> sequences)
        {
            var lookup = new ArrayRef<short>();
            var maxId = sequences.Max(x => x.AnimationId);
            for(short i = 0; i <= maxId; i++) lookup.Add(-1);
            for(short i = 0; i < sequences.Count; i++)
            {
                var id = sequences[i].AnimationId;
                if (lookup[id] == -1) lookup[id] = i;
            }
            return lookup;
        }

        private static class AnimationData
        {
            public static IDictionary Fallback { get; } = new Dictionary<ushort, ushort>();
            public static IDictionary Ids { get; } = new Dictionary<string, ushort>();

            static AnimationData()
            {
                var embeddedStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("AnimationData.csv");
                if(embeddedStream == null) throw new IOException("Could not open embedded ressource AnimationData");
                var csvParser = new TextFieldParser(embeddedStream) {CommentTokens = new[] {"#"}};
                csvParser.SetDelimiters(",");
                csvParser.HasFieldsEnclosedInQuotes = true;
                csvParser.ReadLine(); // Skip first line
                while (!csvParser.EndOfData)
                {
                    var fields = csvParser.ReadFields();
                    if (fields == null) return;
                    Fallback[fields[0]] = fields[3];
                    Ids[fields[1]] = fields[0];
                }
            }
        }

        private static ushort NameToId(string name)
        {
            return (ushort) AnimationData.Ids[name];
        }

        private static ushort GetFallback(ushort id)
        {
            return (ushort) AnimationData.Fallback[id];
        }

        // PLAYABLE ANIMATION LOOKUP

        private static readonly ushort[] PlayThenStop = {NameToId("Dead"), NameToId("SitGround"), NameToId("Sleep"), NameToId("KneelLoop"), NameToId("UseStandingLoop"), NameToId("Drowned"), NameToId("LootHold") };
        private static readonly ushort[] PlayBackwards = {NameToId("WalkBackwards"), NameToId("SwimBackwards"), NameToId("SleepUp"), NameToId("LootUp") };

        private static ushort GetRealId(ushort id, IReadOnlyList<short> animLookup)
        {
            while (true)
            {
                if (id < animLookup.Count && (animLookup[id] > -1))
                {
                    return id;
                }
                id = GetFallback(id);
            }
        }

        public static ArrayRef<PlayableRecord> GeneratePlayableLookup(IReadOnlyList<short> animLookup)
        {
            const int numberOfActions = 226;// From 2.4.3 DB/AnimationData
            var lookup = new ArrayRef<PlayableRecord>();
            for (ushort i = 0; i <= numberOfActions; i++)
            {
                var record = new PlayableRecord(GetRealId(i, animLookup), 0);
                if (record.FallbackId != i)
                {
                    if (PlayThenStop.Contains(i)) record.Flags = PlayableRecord.PlayFlags.Freeze;
                    else if (PlayBackwards.Contains(i)) record.Flags = PlayableRecord.PlayFlags.Backwards;
                }
                lookup.Add(record);
            }
            return lookup;
        }
    }
}
