using System.Collections.Generic;
using System.IO;
using System.Text;
using m2lib_csharp.interfaces;
using m2lib_csharp.types;

namespace m2lib_csharp.m2
{
    public class M2Event : IAnimated
    {
        public string Identifier { get; set; }
        public int Data { get; set; }
        public int Bone { get; set; }
        public C3Vector Position { get; set; } = new C3Vector();
        public M2TrackBase Enabled { get; set; } = new M2TrackBase();

        public void Load(BinaryReader stream, M2.Format version)
        {
            Identifier = Encoding.UTF8.GetString(stream.ReadBytes(4));
            Data = stream.ReadInt32();
            Bone = stream.ReadInt32();
            Position.Load(stream, version);
            Enabled.Load(stream, version);
        }

        public void Save(BinaryWriter stream, M2.Format version)
        {
            if (Identifier.Length > 4)
                Identifier = Identifier.Substring(0, 4);
            stream.Write(Encoding.UTF8.GetBytes(Identifier));
            stream.Write(Data);
            stream.Write(Bone);
            Position.Save(stream, version);
            Enabled.Save(stream, version);
        }

        public void LoadContent(BinaryReader stream, M2.Format version)
        {
            Enabled.LoadContent(stream, version);
        }

        public void SaveContent(BinaryWriter stream, M2.Format version)
        {
            Enabled.SaveContent(stream, version);
        }

        public void SetSequences(IReadOnlyList<M2Sequence> sequences)
        {
            Enabled.SequenceBackRef = sequences;
        }
    }
}
