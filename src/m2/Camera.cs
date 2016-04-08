using System.Collections.Generic;
using System.IO;
using m2lib_csharp.interfaces;
using m2lib_csharp.types;

namespace m2lib_csharp.m2
{
    public class Camera : IAnimated
    {
        public CameraType Type { get; set; } = CameraType.UserInterface;
        public float FarClip { get; set; }
        public float NearClip { get; set; }
        public Track<C33Matrix> Positions { get; set; } = new Track<C33Matrix>();
        public C3Vector PositionBase { get; set; } = new C3Vector();
        public Track<C33Matrix> TargetPositions { get; set; } = new Track<C33Matrix>(); 
        public C3Vector TargetPositionBase { get; set; } = new C3Vector();
        public Track<C3Vector> Roll { get; set; } = new Track<C3Vector>(); 
        public Track<C3Vector> FieldOfView { get; set; } = new Track<C3Vector>(); 

        public enum CameraType
        {
            Portrait = 0,
            CharacterInfo = 1,
            UserInterface = -1
        }
        public void Load(BinaryReader stream, M2.Format version)
        {
            Type = (CameraType) stream.ReadInt32();
            if(version < M2.Format.Cataclysm)
            {
                FieldOfView.Timestamps.Add(new ArrayRef<uint> {0});
                FieldOfView.Values.Add(new ArrayRef<C3Vector> {new C3Vector(stream.ReadSingle(), 0, 0)});
            }
            FarClip = stream.ReadSingle();
            NearClip = stream.ReadSingle();
            Positions.Load(stream, version);
            PositionBase.Load(stream, version);
            TargetPositions.Load(stream, version);
            TargetPositionBase.Load(stream, version);
            Roll.Load(stream, version);
            if(version >= M2.Format.Cataclysm) FieldOfView.Load(stream, version);
        }

        public void Save(BinaryWriter stream, M2.Format version)
        {
            stream.Write((int) Type);
            if(version < M2.Format.Cataclysm) throw new System.NotImplementedException();//TODO
            {
                if (FieldOfView.Values.Count == 1) stream.Write(FieldOfView.Values[0][0].X);
                else stream.Write(Type == CameraType.Portrait ? 0.7F : 0.97F);
            }
            stream.Write(FarClip);
            stream.Write(NearClip);
            Positions.Save(stream, version);
            PositionBase.Save(stream, version);
            TargetPositions.Save(stream, version);
            TargetPositionBase.Save(stream, version);
            Roll.Save(stream, version);
            if(version >= M2.Format.Cataclysm) FieldOfView.Save(stream, version);
        }

        public void LoadContent(BinaryReader stream, M2.Format version)
        {
            Positions.LoadContent(stream, version);
            TargetPositions.LoadContent(stream, version);
            Roll.LoadContent(stream, version);
            if(version >= M2.Format.Cataclysm) FieldOfView.LoadContent(stream, version);
        }

        public void SaveContent(BinaryWriter stream, M2.Format version)
        {
            Positions.SaveContent(stream, version);
            TargetPositions.SaveContent(stream, version);
            Roll.SaveContent(stream, version);
            if(version >= M2.Format.Cataclysm) FieldOfView.SaveContent(stream, version);
        }

        public void SetSequences(IReadOnlyList<Sequence> sequences)
        {
            Positions.SequenceBackRef = sequences;
            TargetPositions.SequenceBackRef = sequences;
            Roll.SequenceBackRef = sequences;
            FieldOfView.SequenceBackRef = sequences;
        }
    }
}
