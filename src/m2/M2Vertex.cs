using System.IO;
using M2Lib.interfaces;
using M2Lib.io;
using M2Lib.types;

namespace M2Lib.m2
{
    public class M2Vertex : IMarshalable
    {
        public C3Vector Position { get; set; }
        public byte[] BoneWeights { get; set; } = new byte[4];
        public byte[] BoneIndices { get; set; } = new byte[4];
        public C3Vector Normal { get; set; }

        public override string ToString()
        {
            return $"Position: {Position}, BoneWeights: [{BoneWeights[0]},{BoneWeights[1]},{BoneWeights[2]},{BoneWeights[3]}], BoneIndices: [{BoneIndices[0]},{BoneIndices[1]},{BoneIndices[2]},{BoneIndices[3]}]\n" +
                   $"Normal: {Normal}, TexCoords: {TexCoords[0]},{TexCoords[1]}";
        }

        public C2Vector[] TexCoords { get; set; } = {new C2Vector(), new C2Vector()};

        public void Load(BinaryReader stream, M2.Format version)
        {
            Position = stream.ReadC3Vector();
            for (var i = 0; i < BoneWeights.Length; i++) BoneWeights[i] = stream.ReadByte();
            for (var i = 0; i < BoneIndices.Length; i++) BoneIndices[i] = stream.ReadByte();
            Normal = stream.ReadC3Vector();
            TexCoords = new[] {stream.ReadC2Vector(), stream.ReadC2Vector()};
        }

        public void Save(BinaryWriter stream, M2.Format version)
        {
            stream.Write(Position);
            foreach (var t in BoneWeights) stream.Write(t);
            foreach (var t in BoneIndices) stream.Write(t);
            stream.Write(Normal);
            foreach (var vec in TexCoords) stream.Write(vec);
        }
    }
}