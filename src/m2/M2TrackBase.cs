using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using m2lib_csharp.interfaces;
using m2lib_csharp.types;

namespace m2lib_csharp.m2
{
    public class M2TrackBase : IReferencer
    {
        public enum InterpolationTypes : ushort
        {
            Instant = 0,
            Linear = 1,
            Hermite = 2,
            Bezier = 3
        }

        private M2Array<Range> _legacyRanges;
        private M2Array<uint> _legacyTimestamps;

        public InterpolationTypes InterpolationType { get; set; }
        public short GlobalSequence { get; set; } = -1;
        public M2Array<M2Array<uint>> Timestamps { get; } = new M2Array<M2Array<uint>>();

        // Used only to read 1 timeline formats and to open correct .anim files when needed.
        // Legacy fields are automatically converted to standard ones in methods.
        public IReadOnlyList<M2Sequence> SequenceBackRef { private get; set; }

        public void Load(BinaryReader stream, M2.Format version)
        {
            Debug.Assert(version != M2.Format.Useless);
            InterpolationType = (InterpolationTypes) stream.ReadUInt16();
            GlobalSequence = stream.ReadInt16();
            if (version >= M2.Format.LichKing)
            {
                Timestamps.Load(stream, version);
            }
            else
            {
                _legacyRanges.Load(stream, version);
                _legacyTimestamps.Load(stream, version);
            }
        }

        public void Save(BinaryWriter stream, M2.Format version)
        {
            Debug.Assert(version != M2.Format.Useless);
            stream.Write((ushort) InterpolationType);
            stream.Write(GlobalSequence);
            if (version >= M2.Format.LichKing)
            {
                Timestamps.Save(stream, version);
            }
            else
            {
                _legacyRanges = new M2Array<Range>();
                _legacyTimestamps = new M2Array<uint>();
                GenerateLegacyFields();
                _legacyRanges.Save(stream, version);
                _legacyTimestamps.Save(stream, version);
            }
        }

        public void LoadContent(BinaryReader stream, M2.Format version)
        {
            Debug.Assert(version != M2.Format.Useless);
            if (version >= M2.Format.LichKing)
            {
                Timestamps.LoadContent(stream, version);
            }
            else
            {
                Debug.Assert(SequenceBackRef != null);
                _legacyRanges.LoadContent(stream, version);
                _legacyTimestamps.LoadContent(stream, version);
                foreach (var seq in SequenceBackRef)
                {
                    var validIndexes = Enumerable.Range(0, _legacyTimestamps.Count)
                        .Where(
                            i =>
                                _legacyTimestamps[i] >= seq.TimeStart &&
                                _legacyTimestamps[i] <= seq.TimeStart + seq.Length)
                        .ToList();
                    var animTimes = new M2Array<uint>();
                    animTimes.AddRange(_legacyTimestamps.GetRange(validIndexes[0], validIndexes[validIndexes.Count - 1]));
                    Timestamps.Add(animTimes);
                }
            }
        }

        public void SaveContent(BinaryWriter stream, M2.Format version)
        {
            Debug.Assert(version != M2.Format.Useless);
            if (version >= M2.Format.LichKing)
            {
                Timestamps.SaveContent(stream, version);
            }
            else
            {
                _legacyRanges.SaveContent(stream, version);
                _legacyTimestamps.SaveContent(stream, version);
            }
        }

        private void GenerateLegacyFields()
        {
            Debug.Assert(SequenceBackRef != null);
            _legacyRanges = new M2Array<Range>();
            _legacyTimestamps = new M2Array<uint>();
            if (GlobalSequence >= 0)
            {
                _legacyTimestamps.AddRange(Timestamps[0]);
            }
            else if (Timestamps.Count == SequenceBackRef.Count)
            {
                for (var i = 0; i < Timestamps.Count; i++)
                {
                    if (Timestamps[i].Count == 0) continue;
                    for (var j = 0; j < Timestamps[i].Count; j++)
                    {
                        _legacyTimestamps.Add(Timestamps[i][j] + SequenceBackRef[i].TimeStart);
                    }
                }
            }
            else if (Timestamps.Count == 1)
            {
                _legacyTimestamps.AddRange(Timestamps[0]);
            }
            GenerateLegacyRanges();
        }

        /// <summary>
        ///     Pre : Sequences set with TimeStart, SequenceBackRef set, LegacyTimestamps computed
        /// </summary>
        private void GenerateLegacyRanges()//TODO Check if output ranges are okay
        {
            //if (_legacyTimestamps.Count == 0) return;
            foreach (var seq in SequenceBackRef)
            {
                var indexesPrevious =
                    Enumerable.Range(0, _legacyTimestamps.Count) // Indexes of times <= to the beginning of sequence.
                        .Where(i => _legacyTimestamps[i] <= seq.TimeStart)
                        .ToList();
                var indexesNext =
                    Enumerable.Range(0, _legacyTimestamps.Count) // Indexes of times >= to the end of sequence.
                        .Where(i => _legacyTimestamps[i] >= seq.TimeStart + seq.Length)
                        .ToList();

                uint startIndex;
                uint endIndex;
                if (indexesPrevious.Count == 0) startIndex = 0;
                else startIndex = (uint) indexesPrevious[indexesPrevious.Count - 1]; // Maximum

                if (indexesNext.Count == 0)
                    endIndex = (uint) (_legacyTimestamps.Count - 1);
                        // We know there more than 1 element (see line 1) so it's >= 0
                else endIndex = (uint) indexesNext[0]; // Minimum

                _legacyRanges.Add(new Range(startIndex, endIndex));
            }
            _legacyRanges.Add(new Range());
        }
    }
}