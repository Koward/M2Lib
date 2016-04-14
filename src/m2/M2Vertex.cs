using System.IO;
using m2lib_csharp.interfaces;
using m2lib_csharp.io;
using m2lib_csharp.types;

namespace m2lib_csharp.m2
{
    public class M2Vertex : IMarshalable
    {
        public C3Vector Position { get; set; }
        public byte[] BoneWeights { get; set; } = new byte[4];
        public byte[] BoneIndices { get; set; } = new byte[4];
        public C3Vector Normal { get; set; }
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