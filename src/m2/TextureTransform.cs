using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using m2lib_csharp.interfaces;
using m2lib_csharp.types;

namespace m2lib_csharp.m2
{
    public class TextureTransform : IAnimated
    {
        public Track<C3Vector> Translation { get; set; } = new Track<C3Vector>();
        public Track<C4Quaternion> Rotation { get; set; } = new Track<C4Quaternion>();
        public Track<C3Vector> Scale { get; set; } = new Track<C3Vector>();

        private Track<CompQuat> CompressedRotation { get; set; }

        public void Load(BinaryReader stream, M2.Format version)
        {
            Translation.Load(stream, version);
            if (version >= M2.Format.LichKing)
            {
                CompressedRotation = new Track<CompQuat>();
                CompressedRotation.Load(stream, version);
            }
            else
                Rotation.Load(stream, version);
            Scale.Load(stream, version);
        }

        public void Save(BinaryWriter stream, M2.Format version)
        {
            Debug.Assert(version != M2.Format.Useless);
            Translation.Save(stream, version);
            if (version >= M2.Format.LichKing)
            {
                CompressedRotation = new Track<CompQuat>();
                CompressedRotation.InitializeCasted(Rotation);
                CompressedRotation.Save(stream, version);
            }
            else
                Rotation.Save(stream, version);
            Scale.Save(stream, version);
        }

        public void LoadContent(BinaryReader stream, M2.Format version)
        {
            Debug.Assert(version != M2.Format.Useless);
            Translation.LoadContent(stream, version);
            if (version >= M2.Format.LichKing)
            {
                CompressedRotation.LoadContent(stream, version);
                Rotation.InitializeCasted(CompressedRotation);
            }
            else
                Rotation.LoadContent(stream, version);
            Scale.LoadContent(stream, version);
        }

        public void SaveContent(BinaryWriter stream, M2.Format version)
        {
            Debug.Assert(version != M2.Format.Useless);
            Translation.SaveContent(stream, version);
            if (version >= M2.Format.LichKing)
            {
                CompressedRotation.SaveContent(stream, version);
            }
            else
                Rotation.SaveContent(stream, version);
            Scale.SaveContent(stream, version);
        }

        /// <summary>
        /// Pass the sequences reference to Tracks so they can : switch between 1 timeline & multiple timelines, open .anim files...
        /// </summary>
        /// <param name="sequences"></param>
        public void SetSequences(IReadOnlyList<Sequence> sequences)
        {
            Translation.SequenceBackRef = sequences;
            Rotation.SequenceBackRef = sequences;
            Scale.SequenceBackRef = sequences;
        }

    }
}
