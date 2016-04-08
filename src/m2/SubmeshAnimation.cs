using System.Collections.Generic;
using System.IO;
using m2lib_csharp.interfaces;
using m2lib_csharp.types;

namespace m2lib_csharp.m2
{
    public class SubmeshAnimation : IAnimated
    {
        public Track<C3Vector> Color { get; set; } = new Track<C3Vector>();
        public Track<ushort> Alpha { get; set; } = new Track<ushort>();//TODO fixed16
        public void Load(BinaryReader stream, M2.Format version)
        {
            Color.Load(stream, version);
            Alpha.Load(stream, version);
        }

        public void Save(BinaryWriter stream, M2.Format version)
        {
            Color.Save(stream, version);
            Alpha.Save(stream, version);
        }

        public void LoadContent(BinaryReader stream, M2.Format version)
        {
            Color.LoadContent(stream, version);
            Alpha.LoadContent(stream, version);
        }

        public void SaveContent(BinaryWriter stream, M2.Format version)
        {
            Color.SaveContent(stream, version);
            Alpha.SaveContent(stream, version);
        }

        public void SetSequences(IReadOnlyList<Sequence> sequences)
        {
            Color.SequenceBackRef = sequences;
            Alpha.SequenceBackRef = sequences;
        }
    }
}
