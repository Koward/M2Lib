using System.Collections.Generic;
using System.IO;
using System.Linq;
using m2lib_csharp.interfaces;
using m2lib_csharp.types;

namespace m2lib_csharp.m2
{
    public class M2Attachment : IAnimated
    {
        public uint Id { get; set; }
        public uint Bone { get; set; }
        public C3Vector Position { get; set; } = new C3Vector();
        public M2Track<bool> AnimateAttached { get; set; } = new M2Track<bool>();

        public void Load(BinaryReader stream, M2.Format version)
        {
            Id = stream.ReadUInt32();
            Bone = stream.ReadUInt32();
            Position.Load(stream, version);
            AnimateAttached.Load(stream, version);
        }

        public void Save(BinaryWriter stream, M2.Format version)
        {
            stream.Write(Id);
            stream.Write(Bone);
            Position.Save(stream, version);
            if (version < M2.Format.LichKing && AnimateAttached.Timestamps.Count == 0)
            {
                AnimateAttached.Timestamps.Add(new M2Array<uint> {0});
                AnimateAttached.Values.Add(new M2Array<bool> {true});
            }
            AnimateAttached.Save(stream, version);
        }

        public void LoadContent(BinaryReader stream, M2.Format version)
        {
            AnimateAttached.LoadContent(stream, version);
        }

        public void SaveContent(BinaryWriter stream, M2.Format version)
        {
            AnimateAttached.SaveContent(stream, version);
        }

        public void SetSequences(IReadOnlyList<M2Sequence> sequences)
        {
            AnimateAttached.SequenceBackRef = sequences;
        }

        public static M2Array<short> GenerateLookup(M2Array<M2Attachment> attachments)
        {
            var lookup = new M2Array<short>();
            if (attachments.Count == 0) return lookup;
            var maxId = attachments.Max(x => x.Id);
            for (short i = 0; i <= maxId; i++) lookup.Add(-1);
            for (short i = 0; i < attachments.Count; i++)
            {
                var id = (short) attachments[i].Id;
                if (lookup[id] == -1) lookup[id] = i;
            }
            return lookup;
        }
    }
}