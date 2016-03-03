using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using m2lib_csharp.interfaces;
using m2lib_csharp.types;

namespace m2lib_csharp.m2
{
    public class Bone : IAnimated
    {
        public int KeyBoneId { get; set; } = -1;
        public BoneFlags Flags { get; set; } = 0;
        public short ParentBone { get; set; } = -1;
        public ushort SubmeshId { get; set; }
        private readonly ushort[] _unknown = new ushort[2];
        public Track<C3Vector> Translation { get; set; } = new Track<C3Vector>();
        public Track<C4Quaternion> Rotation { get; set; } = new Track<C4Quaternion>();
        public Track<C3Vector> Scale { get; set; } = new Track<C3Vector>();

        private Track<CompQuat> CompressedRotation { get; set; }

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
            if (version >= M2.Format.LichKing)
            {
                CompressedRotation = new Track<CompQuat>();
                CompressedRotation.Load(stream, version);
            }
            else
                Rotation.Load(stream, version);
            Scale.Load(stream, version);
        }

        public void Save(BinaryWriter stream, M2.Format version = M2.Format.Unknown)
        {
            Debug.Assert(version != M2.Format.Unknown);
            stream.Write(KeyBoneId);
            stream.Write((uint) Flags);
            stream.Write(ParentBone);
            stream.Write(SubmeshId);
            if (version > M2.Format.Classic)
            {
                stream.Write(_unknown[0]);
                stream.Write(_unknown[1]);
            }
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

        public void LoadContent(BinaryReader stream, M2.Format version = M2.Format.Unknown)
        {
            Debug.Assert(version != M2.Format.Unknown);
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

        public void SaveContent(BinaryWriter stream, M2.Format version = M2.Format.Unknown)
        {
            Debug.Assert(version != M2.Format.Unknown);
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

        public static ArrayRef<short> GenerateKeyBoneLookup(ArrayRef<Bone> bones)
        {
            var lookup = new ArrayRef<short>();
            var maxId = bones.Max(x => x.KeyBoneId);
            for(short i = 0; i <= maxId; i++) lookup.Add(-1);
            for(short i = 0; i < bones.Count; i++)
            {
                var id = bones[i].KeyBoneId;
                if (lookup[id] == -1) lookup[id] = i;
            }
            return lookup;
        }
    }
}
