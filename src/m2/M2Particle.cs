using System.Collections.Generic;
using System.IO;
using m2lib_csharp.interfaces;
using m2lib_csharp.io;
using m2lib_csharp.types;

namespace m2lib_csharp.m2
{
    public class M2Particle : IAnimated
    {
        public int Unknown { get; set; } = -1;
        public uint Flags { get; set; }
        public C3Vector Position { get; set; }
        public ushort Bone { get; set; }
        public ushort Texture { get; set; }

        public string ModelFileName
        {
            get { return _modelFileName.ToNameString(); }
            set { _modelFileName.SetString(value); }
        }
        public string ChildEmitterFileName
        {
            get { return _childEmitterFileName.ToNameString(); }
            set { _childEmitterFileName.SetString(value); }
        }
        private readonly M2Array<byte> _modelFileName = new M2Array<byte>(); 
        private readonly M2Array<byte> _childEmitterFileName = new M2Array<byte>(); 

        public byte BlendingType { get; set; }
        public byte EmitterType { get; set; }
        public ushort ParticleColorIndex { get; set; }
        public byte ParticleType { get; set; }
        public byte HeadOrTail { get; set; }
        public ushort TextureTileRotation { get; set; }
        public ushort TextureDimensionsRows { get; set; }
        public ushort TextureDimensionsColumns { get; set; }
        public M2Track<float> EmissionSpeed { get; set; } = new M2Track<float>();
        public M2Track<float> SpeedVariation { get; set; } = new M2Track<float>();
        public M2Track<float> VerticalRange { get; set; } = new M2Track<float>();
        public M2Track<float> HorizontalRange { get; set; } = new M2Track<float>();
        public M2Track<float> Gravity { get; set; } = new M2Track<float>();
        public M2Track<float> Lifespan { get; set; } = new M2Track<float>();
        public float LifespanVary { get; set; }
        public M2Track<float> EmissionRate { get; set; } = new M2Track<float>();
        public float EmissionRateVary { get; set; }
        public M2Track<float> EmissionAreaLength { get; set; } = new M2Track<float>();
        public M2Track<float> EmissionAreaWidth { get; set; } = new M2Track<float>();
        public M2Track<float> ZSource { get; set; } = new M2Track<float>();
        //TODO FakeBlocks
        public C2Vector ScaleVary { get; set; }
        //TODO FakeBlocks
        public float SomethingParticleStyle { get; set; }
        public float Spread1 { get; set; }
        public float Spread2 { get; set; }
        public CRange TwinkleScale { get; set; }
        public float Blank1 { get; set; }
        public float Drag { get; set; }
        public float BaseSpin { get; set; }
        public float BaseSpinVary { get; set; }
        public float Spin { get; set; }
        public float SpinVary { get; set; }
        public float Blank2 { get; set; }
        public C3Vector Model1Rotation { get; set; }
        public C3Vector Model2Rotation { get; set; }
        public C3Vector ModelTranslation { get; set; }
        public C4Vector FollowParams { get; set; }
        public M2Track<C3Vector> UnknownReference { get; set; } = new M2Track<C3Vector>();
        public M2Track<bool> EnabledIn { get; set; } = new M2Track<bool>();
        public readonly FixedPoint_6_9[] MultiTextureParam0 = new FixedPoint_6_9[2];
        public readonly FixedPoint_6_9[] MultiTextureParam1 = new FixedPoint_6_9[2];


        public void Load(BinaryReader stream, M2.Format version)
        {
            Unknown = stream.ReadInt32();
            Flags = stream.ReadUInt32();
            Position = stream.ReadC3Vector();
            Bone = stream.ReadUInt16();
            Texture = stream.ReadUInt16();
            _modelFileName.Load(stream, version);
            _childEmitterFileName.Load(stream, version);
            BlendingType = stream.ReadByte();
            EmitterType = stream.ReadByte();
            ParticleColorIndex = stream.ReadUInt16();
            ParticleType = stream.ReadByte();
            HeadOrTail = stream.ReadByte();
            throw new System.NotImplementedException();
        }

        public void Save(BinaryWriter stream, M2.Format version)
        {
            throw new System.NotImplementedException();
        }

        public void LoadContent(BinaryReader stream, M2.Format version)
        {
            throw new System.NotImplementedException();
        }

        public void SaveContent(BinaryWriter stream, M2.Format version)
        {
            throw new System.NotImplementedException();
        }

        public void SetSequences(IReadOnlyList<M2Sequence> sequences)
        {
            throw new System.NotImplementedException();
        }
    }
}
