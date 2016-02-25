using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using m2lib_csharp.interfaces;
using m2lib_csharp.types;

namespace m2lib_csharp.m2
{

    public class Track<T> : IReferencer where T : new()
    {
        public InterpolationTypes InterpolationType { get; set; }
        public short GlobalSequence { get; set; } = -1;
        public ArrayRef<ArrayRef<uint>> Timestamps { get; set; } = new ArrayRef<ArrayRef<uint>>();
        public ArrayRef<ArrayRef<T>> Values { get; set; } = new ArrayRef<ArrayRef<T>>();

        // Used only to read 1 timeline formats
        // Legacy fields are automatically converted to standard ones in methods.
        public IReadOnlyList<Sequence> SequenceBackRef { get; set; }
        private ArrayRef<Range> LegacyRanges { get; set; }
        private ArrayRef<uint> LegacyTimestamps { get; set; }
        private ArrayRef<T> LegacyValues { get; set; }

        public enum InterpolationTypes : ushort
        {
            Instant = 0,
            Linear = 1,
            Hermite = 2,
            Bezier = 3
        } 

        public void Load(BinaryReader stream, M2.Format version = M2.Format.Unknown)
        {
            Debug.Assert(version != M2.Format.Unknown);
            InterpolationType = (InterpolationTypes) stream.ReadUInt16();
            GlobalSequence = stream.ReadInt16();
            if (version >= M2.Format.LichKing)
            {
                Timestamps.Load(stream, version);
                Values.Load(stream, version);
            }
            else
            {
                LegacyRanges = new ArrayRef<Range>();
                LegacyTimestamps = new ArrayRef<uint>();
                LegacyValues = new ArrayRef<T>();
                LegacyRanges.Load(stream);
                LegacyTimestamps.Load(stream);
                LegacyValues.Load(stream, version);
            }
        }

        public void Save(BinaryWriter stream, M2.Format version = M2.Format.Unknown)
        {
            Debug.Assert(version != M2.Format.Unknown);
            stream.Write((ushort) InterpolationType);
            stream.Write(GlobalSequence);
            if (version >= M2.Format.LichKing)
            {
                Timestamps.Save(stream, version);
                Values.Save(stream, version);
            }
            else
            {
                LegacyRanges = new ArrayRef<Range>();
                LegacyTimestamps = new ArrayRef<uint>();
                LegacyValues = new ArrayRef<T>();
                GenerateLegacyFields();
                LegacyRanges.Save(stream);
                LegacyTimestamps.Save(stream);
                LegacyValues.Save(stream, version);
            }
        }

        public void LoadContent(BinaryReader stream, M2.Format version = M2.Format.Unknown, BinaryReader[] animFiles = null)
        {
            Debug.Assert(version != M2.Format.Unknown);
            if (version >= M2.Format.LichKing)
            {
                Timestamps.LoadContent(stream, version, animFiles);
                Values.LoadContent(stream, version, animFiles);
            }
            else
            {
                Debug.Assert(SequenceBackRef != null);
                LegacyRanges.LoadContent(stream);
                LegacyTimestamps.LoadContent(stream);
                LegacyValues.LoadContent(stream, version);
                foreach (var seq in SequenceBackRef)
                {
                    var validIndexes = Enumerable.Range(0, LegacyTimestamps.Count)
                    .Where(i => LegacyTimestamps[i] >= seq.TimeStart && LegacyTimestamps[i] <= seq.TimeStart + seq.Length)
                    .ToList();
                    var animTimes = new ArrayRef<uint>();
                    var animValues = new ArrayRef<T>();
                    animTimes.AddRange(LegacyTimestamps.GetRange(validIndexes[0], validIndexes[validIndexes.Count - 1]));
                    animValues.AddRange(LegacyValues.GetRange(validIndexes[0], validIndexes[validIndexes.Count - 1]));
                    Timestamps.Add(animTimes);
                    Values.Add(animValues);
                }
            }
        }

        public void SaveContent(BinaryWriter stream, M2.Format version = M2.Format.Unknown, BinaryWriter[] animFiles = null)
        {
            Debug.Assert(version != M2.Format.Unknown);
            if (version >= M2.Format.LichKing)
            {
                Timestamps.SaveContent(stream, version, animFiles);
                Values.SaveContent(stream, version, animFiles);
            }
            else
            {
                LegacyRanges.SaveContent(stream);
                LegacyTimestamps.SaveContent(stream);
                LegacyValues.SaveContent(stream, version);
            }
        }

        private void GenerateLegacyFields()
        {
            Debug.Assert(SequenceBackRef != null);
            if (GlobalSequence >= 0)
            {
                LegacyTimestamps.AddRange(Timestamps[0]);
                LegacyValues.AddRange(Values[0]);
            }
            else if (Timestamps.Count == SequenceBackRef.Count)
            {
                for(var i = 0; i < Timestamps.Count; i++)
                {
                    if (Timestamps[i].Count == 0) continue;
                    for (var j = 0; j < Timestamps[i].Count; j++)
                    {
                        LegacyTimestamps.Add(Timestamps[i][j] + SequenceBackRef[i].TimeStart);
                        LegacyValues.Add(Values[i][j]);
                    }
                }
            }
            else if (Timestamps.Count == 1)
            {
                LegacyTimestamps.AddRange(Timestamps[0]);
                LegacyValues.AddRange(Values[0]);
            }
            GenerateLegacyRanges();
        }

        /// <summary>
        /// Pre : Sequences set with TimeStart, SequenceBackRef set, LegacyTimestamps computed
        /// </summary>
        private void GenerateLegacyRanges()
        {
            if (LegacyTimestamps.Count == 0) return;
            foreach (var seq in SequenceBackRef)
            {
                var indexesPrevious = Enumerable.Range(0, LegacyTimestamps.Count) // Indexes of times >= to the end of sequence.
                .Where(i => LegacyTimestamps[i] <= seq.TimeStart)
                .ToList();
                var indexesNext = Enumerable.Range(0, LegacyTimestamps.Count) // Indexes of times >= to the end of sequence.
                .Where(i => LegacyTimestamps[i] >= seq.TimeStart + seq.Length)
                .ToList();
                uint startIndex = 0;
                uint endIndex = 0;
                if (indexesPrevious.Count == 0) startIndex = 0;
                if (indexesNext.Count == 0) endIndex = (uint) (LegacyTimestamps.Count - 1); // We know there more than 1 element (see line 1) so it's >= 0
                LegacyRanges.Add(new Range(startIndex, endIndex));
            }
            LegacyRanges.Add(new Range());
        }
    }

    public class Range : IMarshalable
    {
        public uint StartIndex { get; set; }
        public uint EndIndex { get; set; }

        public Range(uint p1, uint p2)
        {
            StartIndex = p1;
            EndIndex = p2;
        }

        public Range() : this(0, 0) { }

        public void Load(BinaryReader stream, M2.Format version = M2.Format.Unknown)
        {
            StartIndex = stream.ReadUInt32();
            EndIndex = stream.ReadUInt32();
        }

        public void Save(BinaryWriter stream, M2.Format version = M2.Format.Unknown)
        {
            stream.Write(StartIndex);
            stream.Write(EndIndex);
        }
    }
}
