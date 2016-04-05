using System.Diagnostics;
using System.IO;
using System.Text;
using m2lib_csharp.interfaces;
using m2lib_csharp.types;

namespace m2lib_csharp.m2
{
    public class View : IReferencer
    {
        //TODO change types (ushort, placeholder) to real ones
        public ArrayRef<ushort> Indices { get; set; }= new ArrayRef<ushort>();
        public ArrayRef<ushort> Triangles { get; set; }= new ArrayRef<ushort>();
        public ArrayRef<uint> Properties { get; set; }= new ArrayRef<uint>();
        public ArrayRef<Submesh> Submeshes { get; set; }= new ArrayRef<Submesh>();
        public ArrayRef<TextureUnit> TextureUnits { get; set; }= new ArrayRef<TextureUnit>();
        public uint Bones { get; set; }
        public ArrayRef<ShadowBatch> ShadowBatches { get; set; }= new ArrayRef<ShadowBatch>();

        public void Load(BinaryReader stream, M2.Format version)
        {
            if (version >= M2.Format.LichKing)
            {
                var magic = Encoding.UTF8.GetString(stream.ReadBytes(4));
                Debug.Assert(magic == "SKIN");
            }
            Indices.Load(stream, version);
            Triangles.Load(stream, version);
            Properties.Load(stream, version);
            Submeshes.Load(stream, version);
            TextureUnits.Load(stream, version);
            Bones = stream.ReadUInt32();
            if(version >= M2.Format.Cataclysm) ShadowBatches.Load(stream, version);
        }

        public void Save(BinaryWriter stream, M2.Format version)
        {
            if (version >= M2.Format.LichKing)
                stream.Write(Encoding.UTF8.GetBytes("SKIN"));
            Indices.Save(stream, version);
            Triangles.Save(stream, version);
            Properties.Save(stream, version);
            Submeshes.Save(stream, version);
            TextureUnits.Save(stream, version);
            stream.Write(Bones);
            if(version >= M2.Format.Cataclysm) ShadowBatches.Save(stream, version);
        }

        public void LoadContent(BinaryReader stream, M2.Format version)
        {
            Indices.LoadContent(stream, version);
            Triangles.LoadContent(stream, version);
            Properties.LoadContent(stream, version);
            Submeshes.LoadContent(stream, version);
            TextureUnits.LoadContent(stream, version);
            if(version >= M2.Format.Cataclysm) ShadowBatches.LoadContent(stream, version);
        }

        public void SaveContent(BinaryWriter stream, M2.Format version)
        {
            Indices.SaveContent(stream, version);
            Triangles.SaveContent(stream, version);
            Properties.SaveContent(stream, version);
            Submeshes.SaveContent(stream, version);
            TextureUnits.SaveContent(stream, version);
            if(version >= M2.Format.Cataclysm) ShadowBatches.SaveContent(stream, version);
        }

        public static string SkinFileName(string path, int number)
        {
            var pathDirectory = Path.GetDirectoryName(path);
            Debug.Assert(pathDirectory != null, "pathDirectory != null");
            var pathWithoutExt = Path.GetFileNameWithoutExtension(path);
            string animFileName = $"{pathWithoutExt}{number:00}.skin";
            return Path.Combine(pathDirectory, animFileName);
        }
    }
}
