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
        public ArrayRef<ArrayRef<uint>> Timestamps => _timestamps;
        public ArrayRef<ArrayRef<T>> Values => _values; 

        private readonly ArrayRef<ArrayRef<uint>> _timestamps = new ArrayRef<ArrayRef<uint>>();
        private readonly ArrayRef<ArrayRef<T>> _values = new ArrayRef<ArrayRef<T>>();

        // Used only to read 1 timeline formats
        // Legacy fields are automatically converted to standard ones in methods.
        public IReadOnlyList<Sequence> SequenceBackRef { private get; set; }
        private ArrayRef<Range> _legacyRanges;
        private ArrayRef<uint> _legacyTimestamps;
        private ArrayRef<T> _legacyValues;

        public enum InterpolationTypes : ushort
        {
            Instant = 0,
            Linear = 1,
            Hermite = 2,
            Bezier = 3
        } 

        public void Load(BinaryReader stream, M2.Format version)
        {
            Debug.Assert(version != M2.Format.Useless);
            InterpolationType = (InterpolationTypes) stream.ReadUInt16();
            GlobalSequence = stream.ReadInt16();
            if (version >= M2.Format.LichKing)
            {
                _timestamps.Load(stream, version);
                _values.Load(stream, version);
            }
            else
            {
                _legacyRanges = new ArrayRef<Range>();
                _legacyTimestamps = new ArrayRef<uint>();
                _legacyValues = new ArrayRef<T>();
                _legacyRanges.Load(stream, version);
                _legacyTimestamps.Load(stream, version);
                _legacyValues.Load(stream, version);
            }
        }

        public void Save(BinaryWriter stream, M2.Format version)
        {
            Debug.Assert(version != M2.Format.Useless);
            stream.Write((ushort) InterpolationType);
            stream.Write(GlobalSequence);
            if (version >= M2.Format.LichKing)
            {
                _timestamps.Save(stream, version);
                _values.Save(stream, version);
            }
            else
            {
                _legacyRanges = new ArrayRef<Range>();
                _legacyTimestamps = new ArrayRef<uint>();
                _legacyValues = new ArrayRef<T>();
                GenerateLegacyFields();
                _legacyRanges.Save(stream, version);
                _legacyTimestamps.Save(stream, version);
                _legacyValues.Save(stream, version);
            }
        }

        public void LoadContent(BinaryReader stream, M2.Format version)
        {
            Debug.Assert(version != M2.Format.Useless);
            if (version >= M2.Format.LichKing)
            {
                _timestamps.LoadContent(stream, version);
                _values.LoadContent(stream, version);
            }
            else
            {
                Debug.Assert(SequenceBackRef != null);
                _legacyRanges.LoadContent(stream, version);
                _legacyTimestamps.LoadContent(stream, version);
                _legacyValues.LoadContent(stream, version);
                foreach (var seq in SequenceBackRef)
                {
                    var validIndexes = Enumerable.Range(0, _legacyTimestamps.Count)
                    .Where(i => _legacyTimestamps[i] >= seq.TimeStart && _legacyTimestamps[i] <= seq.TimeStart + seq.Length)
                    .ToList();
                    var animTimes = new ArrayRef<uint>();
                    var animValues = new ArrayRef<T>();
                    animTimes.AddRange(_legacyTimestamps.GetRange(validIndexes[0], validIndexes[validIndexes.Count - 1]));
                    animValues.AddRange(_legacyValues.GetRange(validIndexes[0], validIndexes[validIndexes.Count - 1]));
                    _timestamps.Add(animTimes);
                    _values.Add(animValues);
                }
            }
        }

        public void SaveContent(BinaryWriter stream, M2.Format version)
        {
            Debug.Assert(version != M2.Format.Useless);
            if (version >= M2.Format.LichKing)
            {
                _timestamps.SaveContent(stream, version);
                _values.SaveContent(stream, version);
            }
            else
            {
                _legacyRanges.SaveContent(stream, version);
                _legacyTimestamps.SaveContent(stream, version);
                _legacyValues.SaveContent(stream, version);
            }
        }

        private void GenerateLegacyFields()
        {
            Debug.Assert(SequenceBackRef != null);
            if (GlobalSequence >= 0)
            {
                _legacyTimestamps.AddRange(_timestamps[0]);
                _legacyValues.AddRange(_values[0]);
            }
            else if (_timestamps.Count == SequenceBackRef.Count)
            {
                for(var i = 0; i < _timestamps.Count; i++)
                {
                    if (_timestamps[i].Count == 0) continue;
                    for (var j = 0; j < _timestamps[i].Count; j++)
                    {
                        _legacyTimestamps.Add(_timestamps[i][j] + SequenceBackRef[i].TimeStart);
                        _legacyValues.Add(_values[i][j]);
                    }
                }
            }
            else if (_timestamps.Count == 1)
            {
                _legacyTimestamps.AddRange(_timestamps[0]);
                _legacyValues.AddRange(_values[0]);
            }
            GenerateLegacyRanges();
        }

        /// <summary>
        /// Pre : Sequences set with TimeStart, SequenceBackRef set, LegacyTimestamps computed
        /// </summary>
        private void GenerateLegacyRanges()
        {
            if (_legacyTimestamps.Count == 0) return;
            foreach (var seq in SequenceBackRef)
            {
                var indexesPrevious = Enumerable.Range(0, _legacyTimestamps.Count) // Indexes of times >= to the end of sequence.
                .Where(i => _legacyTimestamps[i] <= seq.TimeStart)
                .ToList();
                var indexesNext = Enumerable.Range(0, _legacyTimestamps.Count) // Indexes of times >= to the end of sequence.
                .Where(i => _legacyTimestamps[i] >= seq.TimeStart + seq.Length)
                .ToList();

                uint startIndex;
                uint endIndex;
                if (indexesPrevious.Count == 0) startIndex = 0;
                else startIndex = (uint) indexesPrevious[indexesPrevious.Count - 1]; // Maximum

                if (indexesNext.Count == 0) endIndex = (uint) (_legacyTimestamps.Count - 1); // We know there more than 1 element (see line 1) so it's >= 0
                else endIndex = (uint) indexesNext[0]; // Minimum

                _legacyRanges.Add(new Range(startIndex, endIndex));
            }
            _legacyRanges.Add(new Range());
        }

        public void InitializeCasted<TG>(Track<TG> track) where TG : new()
        {
            InterpolationType = (InterpolationTypes) track.InterpolationType;
            GlobalSequence = track.GlobalSequence;
            foreach (var timestamp in track._timestamps) _timestamps.Add(timestamp);
            foreach (var array in track._values)
            {
                var newArray = new ArrayRef<T>();
                newArray.AddRange(array.Select(value => (T) (object) value));
                _values.Add(newArray);
            }
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

        public void Load(BinaryReader stream, M2.Format version)
        {
            StartIndex = stream.ReadUInt32();
            EndIndex = stream.ReadUInt32();
        }

        public void Save(BinaryWriter stream, M2.Format version)
        {
            stream.Write(StartIndex);
            stream.Write(EndIndex);
        }

    }
}
