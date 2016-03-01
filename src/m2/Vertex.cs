using System.IO;
using m2lib_csharp.interfaces;
using m2lib_csharp.types;

namespace m2lib_csharp.m2 {

    public class Vertex : IMarshalable
    {
        public C3Vector Position { get; set; } = new C3Vector();
        public byte[] BoneWeights { get; set; } = new byte[4];
        public byte[] BoneIndices { get; set; } = new byte[4];
        public C3Vector Normal { get; set; } = new C3Vector();
        public C2Vector[] TexCoords { get; set; } = {new C2Vector(),new C2Vector()};

        public void Load(BinaryReader stream, M2.Format version = M2.Format.Unknown)
        {
            Position.Load(stream, version);
            for (var i = 0; i < BoneWeights.Length; i++) BoneWeights[i] = stream.ReadByte();
            for (var i = 0; i < BoneIndices.Length; i++) BoneIndices[i] = stream.ReadByte();
            Normal.Load(stream, version);
            foreach (var vec in TexCoords) vec.Load(stream, version);
        }

        public void Save(BinaryWriter stream, M2.Format version = M2.Format.Unknown)
        {
            Position.Save(stream, version);
            foreach (var t in BoneWeights) stream.Write(t);
            foreach (var t in BoneIndices) stream.Write(t);
            Normal.Save(stream, version);
            foreach (var vec in TexCoords) vec.Save(stream, version);
        }
    }
}
