using System.Collections.Generic;
using System.IO;
using m2lib_csharp.interfaces;
using m2lib_csharp.types;

namespace m2lib_csharp.m2
{
    public class Light : IAnimated
    {
        public LightType Type { get; set; } = LightType.Directional;
        public short Bone { get; set; } = -1;
        public C3Vector Position { get; set; } = new C3Vector();
        public Track<C3Vector> AmbientColor { get; set; } = new Track<C3Vector>(); 
        public Track<float> AmbientIntensity { get; set; } = new Track<float>(); 
        public Track<C3Vector> DiffuseColor { get; set; } = new Track<C3Vector>(); 
        public Track<float> DiffuseIntensity { get; set; } = new Track<float>(); 
        public Track<float> AttenuationStart { get; set; } = new Track<float>(); 
        public Track<float> AttenuationEnd { get; set; } = new Track<float>(); 
        public Track<byte> Unknown { get; set; } = new Track<byte>(); 

        public enum LightType : ushort
        {
            Directional = 0,
            Point = 1
        }

        public void Load(BinaryReader stream, M2.Format version)
        {
            Type = (LightType) stream.ReadUInt16();
            Bone = stream.ReadInt16();
            Position.Load(stream, version);
            AmbientColor.Load(stream, version);
            AmbientIntensity.Load(stream, version);
            DiffuseColor.Load(stream, version);
            DiffuseIntensity.Load(stream, version);
            AttenuationStart.Load(stream, version);
            AttenuationEnd.Load(stream, version);
            Unknown.Load(stream, version);
        }

        public void Save(BinaryWriter stream, M2.Format version)
        {
            stream.Write((ushort) Type);
            stream.Write(Bone);
            Position.Save(stream, version);
            AmbientColor.Save(stream, version);
            AmbientIntensity.Save(stream, version);
            DiffuseColor.Save(stream, version);
            DiffuseIntensity.Save(stream, version);
            AttenuationStart.Save(stream, version);
            AttenuationEnd.Save(stream, version);
            Unknown.Save(stream, version);
        }

        public void LoadContent(BinaryReader stream, M2.Format version)
        {
            AmbientColor.LoadContent(stream, version);
            AmbientIntensity.LoadContent(stream, version);
            DiffuseColor.LoadContent(stream, version);
            DiffuseIntensity.LoadContent(stream, version);
            AttenuationStart.LoadContent(stream, version);
            AttenuationEnd.LoadContent(stream, version);
            Unknown.LoadContent(stream, version);
        }

        public void SaveContent(BinaryWriter stream, M2.Format version)
        {
            AmbientColor.SaveContent(stream, version);
            AmbientIntensity.SaveContent(stream, version);
            DiffuseColor.SaveContent(stream, version);
            DiffuseIntensity.SaveContent(stream, version);
            AttenuationStart.SaveContent(stream, version);
            AttenuationEnd.SaveContent(stream, version);
            Unknown.SaveContent(stream, version);
        }

        public void SetSequences(IReadOnlyList<Sequence> sequences)
        {
            AmbientColor.SequenceBackRef = sequences;
            AmbientIntensity.SequenceBackRef = sequences;
            DiffuseColor.SequenceBackRef = sequences;
            DiffuseIntensity.SequenceBackRef = sequences;
            AttenuationStart.SequenceBackRef = sequences;
            AttenuationEnd.SequenceBackRef = sequences;
            Unknown.SequenceBackRef = sequences;
        }
    }
}
