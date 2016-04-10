using System.Collections.Generic;
using System.IO;
using m2lib_csharp.interfaces;
using m2lib_csharp.types;

namespace m2lib_csharp.m2
{
    public class M2TextureWeight : IAnimated
    {
        public M2Track<Fixed16> Weight { get; set; } = new M2Track<Fixed16>();

        public void Load(BinaryReader stream, M2.Format version)
        {
            Weight.Load(stream, version);
        }

        public void Save(BinaryWriter stream, M2.Format version)
        {
            Weight.Save(stream, version);
        }

        public void LoadContent(BinaryReader stream, M2.Format version)
        {
            Weight.LoadContent(stream, version);
        }

        public void SaveContent(BinaryWriter stream, M2.Format version)
        {
            Weight.SaveContent(stream, version);
        }

        public void SetSequences(IReadOnlyList<M2Sequence> sequences)
        {
            Weight.SequenceBackRef = sequences;
        }
    }
}