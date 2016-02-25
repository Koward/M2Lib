using System;
using System.Diagnostics;
using System.IO;
using m2lib_csharp.interfaces;
using m2lib_csharp.types;

namespace m2lib_csharp.m2
{
    public class Bone : IReferencer
    {
        public int KeyBoneId = -1;
        public BoneFlags Flags = 0;
        public short ParentBone = -1;
        public ushort SubmeshId;
        private ushort[] _unknown = new ushort[2];
        public Track<C3Vector> Translation;
        public Track<C4Quaternion> Rotation;
        public Track<C3Vector> Scale;

        [Flags]
        public enum BoneFlags
        {
            SphericalBillboard = 0x8,
            CylindricalBillboardLockX = 0x10,
            CylindricalBillboardLockY = 0x20,
            CylindricalBillboardLockZ = 0x40,
            Transformed = 0x200,
            KinematicBone = 0x400,       // MoP+: allow physics to influence this bone
            HelmetAnimScaled = 0x1000,  // set blend_modificator to helmetAnimScalingRec.m_amount for this bone
        }

        public void Load(BinaryReader stream, M2.Format version = M2.Format.Unknown)
        {
            Debug.Assert(version != M2.Format.Unknown);
            KeyBoneId = stream.ReadInt32();
            Flags = (BoneFlags) stream.ReadUInt32();
            ParentBone = stream.ReadInt16();
            SubmeshId = stream.ReadUInt16();
            if (version > M2.Format.Classic)
            {
                _unknown[0] = stream.ReadUInt16();
                _unknown[1] = stream.ReadUInt16();
            }
            Translation.Load(stream, version);
            Rotation.Load(stream, version);
            Scale.Load(stream, version);
        }

        public void Save(BinaryWriter stream, M2.Format version = M2.Format.Unknown)
        {
            Debug.Assert(version != M2.Format.Unknown);
            throw new NotImplementedException();
        }

        public void LoadContent(BinaryReader stream, M2.Format version = M2.Format.Unknown, BinaryReader[] animFiles = null)
        {
            Debug.Assert(version != M2.Format.Unknown);
            throw new NotImplementedException();
        }

        public void SaveContent(BinaryWriter stream, M2.Format version = M2.Format.Unknown, BinaryWriter[] animFiles = null)
        {
            Debug.Assert(version != M2.Format.Unknown);
            throw new NotImplementedException();
        }

        public void ConvertTracks(ArrayRef<Sequence> sequences)
        {
            Translation.SequenceBackRef = sequences;
            Rotation.SequenceBackRef = sequences;
            Scale.SequenceBackRef = sequences;
        }
    }
}
