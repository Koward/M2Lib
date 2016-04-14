using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using m2lib_csharp.interfaces;
using m2lib_csharp.types;

namespace m2lib_csharp.m2
{
    public class M2Track<T> : IReferencer where T : new()
    {
        public enum InterpolationTypes : ushort
        {
            Instant = 0,
            Linear = 1,
            Hermite = 2,
            Bezier = 3
        }

        private readonly M2Array<Range> _legacyRanges = new M2Array<Range>();
        private readonly M2Array<uint> _legacyTimestamps = new M2Array<uint>();
        private readonly M2Array<T> _legacyValues = new M2Array<T>();

        public InterpolationTypes InterpolationType { get; set; }
        public short GlobalSequence { get; set; } = -1;
        public M2Array<M2Array<uint>> Timestamps { get; } = new M2Array<M2Array<uint>>();

        public M2Array<M2Array<T>> Values { get; } = new M2Array<M2Array<T>>();

        // Used only to read 1 timeline formats and to open correct .anim files when needed.
        // Legacy fields are automatically converted to standard ones in methods.
        public IReadOnlyList<M2Sequence> SequenceBackRef { set; get; }

        // Used to add new values
        public T DefaultValue
        {
            set { _defaultValue = value;
                _defaultValueSet = true;}
        }
        private bool _defaultValueSet;
        private T _defaultValue;


        public void Load(BinaryReader stream, M2.Format version)
        {
            Debug.Assert(version != M2.Format.Useless);
            InterpolationType = (InterpolationTypes) stream.ReadUInt16();
            GlobalSequence = stream.ReadInt16();
            if (version >= M2.Format.LichKing)
            {
                Timestamps.Load(stream, version);
                Values.Load(stream, version);
            }
            else
            {
                LegacyLoad(stream, version);
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
                Values.Save(stream, version);
            }
            else
            {
                LegacySave(stream, version);
            }
        }

        public void LoadContent(BinaryReader stream, M2.Format version)
        {
            Debug.Assert(version != M2.Format.Useless);
            if (version >= M2.Format.LichKing)
            {
                Timestamps.LoadContent(stream, version);
                Values.LoadContent(stream, version);
                for (var i = 0; i < Timestamps.Count; i++)
                {
                    //TODO Should we check if GlobalSequence before accessing sequence flags ?
                    if (SequenceBackRef[i].IsAlias)
                    {
                        var realIndex = i;
                        while (SequenceBackRef[realIndex].IsAlias)
                            realIndex = SequenceBackRef[realIndex].AliasNext;
                        Timestamps[i] = Timestamps[realIndex];
                        Values[i] = Values[realIndex];
                        continue;
                    }
                    if (SequenceBackRef[i].IsExtern)
                    {
                        Timestamps[i].LoadContent(SequenceBackRef[i].ReadingAnimFile, version);
                        Values[i].LoadContent(SequenceBackRef[i].ReadingAnimFile, version);
                    }
                    else
                    {
                        Timestamps[i].LoadContent(stream, version);
                        Values[i].LoadContent(stream, version);
                    }
                }
            }
            else
            {
                LegacyLoadContent(stream, version);
            }
        }

        public void SaveContent(BinaryWriter stream, M2.Format version)
        {
            Debug.Assert(version != M2.Format.Useless);
            if (version >= M2.Format.LichKing)
            {
                Timestamps.SaveContent(stream, version);
                Values.SaveContent(stream, version);
                for (var i = 0; i < Timestamps.Count; i++)
                {
                    //TODO Should we check if GlobalSequence before accessing sequence flags ?
                    if (SequenceBackRef[i].IsAlias)
                    {
                        var realIndex = i;
                        while (SequenceBackRef[realIndex].IsAlias)
                            realIndex = SequenceBackRef[realIndex].AliasNext;
                        Timestamps[i] = Timestamps[realIndex];
                        Values[i] = Values[realIndex];
                        continue;
                    }
                    if (SequenceBackRef[i].IsExtern)
                    {
                        Timestamps[i].SaveContent(SequenceBackRef[i].WritingAnimFile, version);
                        Values[i].SaveContent(SequenceBackRef[i].WritingAnimFile, version);
                    }
                    else
                    {
                        Timestamps[i].SaveContent(stream, version);
                        Values[i].SaveContent(stream, version);
                    }
                }
            }
            else
            {
                LegacySaveContent(stream, version);
            }
        }

        public override string ToString()
        {
            return $"InterpolationType: {InterpolationType}, GlobalSequence: {GlobalSequence}, " +
                   $"\nTimestamps: {Timestamps}, " +
                   $"\nValues: {Values}";
        }

        /// <summary>
        /// Pre : SequenceBackRef != null
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="version"></param>
        private void LegacySave(BinaryWriter stream, M2.Format version)
        {
            if (GlobalSequence >= 0)
            {
                _legacyTimestamps.AddRange(Timestamps[0]);
                _legacyValues.AddRange(Values[0]);
            }
            else if (Timestamps.Count == SequenceBackRef.Count)
            {
                for (var i = 0; i < Timestamps.Count; i++)
                {
                    if (Timestamps[i].Count == 0) {
                        _legacyTimestamps.Add(SequenceBackRef[i].TimeStart);
                        _legacyValues.Add(!_defaultValueSet ? new T() : _defaultValue);
                        continue;
                    }
                    for (var j = 0; j < Timestamps[i].Count; j++)
                    {
                        _legacyTimestamps.Add(Timestamps[i][j] + SequenceBackRef[i].TimeStart);
                        _legacyValues.Add(Values[i][j]);
                    }
                }
            }
            else if (Timestamps.Count == 1)
            {
                _legacyTimestamps.AddRange(Timestamps[0]);
                _legacyValues.AddRange(Values[0]);
            }
            GenerateLegacyRanges();
            _legacyRanges.Save(stream, version);
            _legacyTimestamps.Save(stream, version);
            _legacyValues.Save(stream, version);
        }

        private void LegacySaveContent(BinaryWriter stream, M2.Format version)
        {

            _legacyRanges.SaveContent(stream, version);
                _legacyTimestamps.SaveContent(stream, version);
                _legacyValues.SaveContent(stream, version);
        } 

        /// <summary>
        ///     Pre : Sequences set with TimeStart, SequenceBackRef set, LegacyTimestamps computed
        /// </summary>
        private void GenerateLegacyRanges()
        {
            if (_legacyTimestamps.Count < 2) return;
            // ReSharper disable once ForCanBeConvertedToForeach
            for(var index = 0; index < SequenceBackRef.Count; index++)
            {
                var seq = SequenceBackRef[index];
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

        private void LegacyLoad(BinaryReader stream, M2.Format version)
        {
            Debug.Assert(SequenceBackRef != null, "SequenceBackRef is null in M2Track<"+typeof(T)+">");
            _legacyRanges.Load(stream, version);
            _legacyTimestamps.Load(stream, version);
            _legacyValues.Load(stream, version);
        }

        private void LegacyLoadContent(BinaryReader stream, M2.Format version)
        {
            _legacyRanges.LoadContent(stream, version);
            _legacyTimestamps.LoadContent(stream, version);
            _legacyValues.LoadContent(stream, version);
            if (_legacyTimestamps.Count == 0) return;
            if (GlobalSequence >= 0)
            {
                Timestamps.Add(_legacyTimestamps);
                Values.Add(_legacyValues);
            }
            else
            {
                // ReSharper disable once ForCanBeConvertedToForeach
                for(var index = 0; index < SequenceBackRef.Count; index++)
                {
                    var seq = SequenceBackRef[index];
                    var validIndexes = Enumerable.Range(0, _legacyTimestamps.Count)
                        .Where(
                            i =>
                                _legacyTimestamps[i] >= seq.TimeStart &&
                                _legacyTimestamps[i] <= seq.TimeStart + seq.Length)
                        .ToList();

                    var animTimes = new M2Array<uint>();
                    var animValues = new M2Array<T>();
                    if (validIndexes.Count > 0)
                    {
                        var firstIndex = validIndexes[0];
                        var lastIndex = validIndexes[validIndexes.Count - 1];
                        animTimes.AddRange(_legacyTimestamps.GetRange(firstIndex, lastIndex - firstIndex + 1));
                        animValues.AddRange(_legacyValues.GetRange(firstIndex, lastIndex - firstIndex + 1));
                    }
                    Timestamps.Add(animTimes);
                    Values.Add(animValues);
                }
            }
        }
    }

    public class Range : IMarshalable
    {
        public Range(uint p1, uint p2)
        {
            StartIndex = p1;
            EndIndex = p2;
        }

        public Range() : this(0, 0)
        {
        }

        public uint StartIndex { get; set; }
        public uint EndIndex { get; set; }

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

    public static class M2TrackExtensions
    {
        public static void Compress(this M2Track<C4Quaternion> track, M2Track<CompQuat> target)
        {
            target.Timestamps.Clear();
            target.Values.Clear();
            target.InterpolationType = (M2Track<CompQuat>.InterpolationTypes) track.InterpolationType;
            target.GlobalSequence = track.GlobalSequence;
            target.SequenceBackRef = track.SequenceBackRef;
            for (var i = 0; i < track.Timestamps.Count; i++) target.Timestamps.Add(track.Timestamps[i]);
            for (var i = 0; i < track.Values.Count; i++)
            {
                var newArray = new M2Array<CompQuat>();
                newArray.AddRange(track.Values[i].Select(value => (CompQuat) value));
                target.Values.Add(newArray);
            }
        } 

        public static void Decompress(this M2Track<CompQuat> track, M2Track<C4Quaternion> target)
        {
            target.Timestamps.Clear();
            target.Values.Clear();
            target.InterpolationType = (M2Track<C4Quaternion>.InterpolationTypes) track.InterpolationType;
            target.GlobalSequence = track.GlobalSequence;
            target.SequenceBackRef = track.SequenceBackRef;
            for (var i = 0; i < track.Timestamps.Count; i++) target.Timestamps.Add(track.Timestamps[i]);
            for (var i = 0; i < track.Values.Count; i++)
            {
                var newArray = new M2Array<C4Quaternion>();
                newArray.AddRange(track.Values[i].Select(value => (C4Quaternion) value));
                target.Values.Add(newArray);
            }
        } 
    }
}