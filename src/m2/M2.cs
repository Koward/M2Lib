using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using m2lib_csharp.interfaces;
using m2lib_csharp.io;
using m2lib_csharp.types;

namespace m2lib_csharp.m2
{
    /// <summary>
    /// World of Warcraft model format.
    /// </summary>
    public class M2 : IMarshalable
    {
        public Format Version { get; set; } = Format.Draenor;
        public string Name { get; set; } = "NullModel";
        public GlobalFlags GlobalModelFlags { get; set; } = 0;
        public ArrayRef<int> GlobalSequences { get; set; } = new ArrayRef<int>();
        public ArrayRef<Sequence> Sequences { get; set; } = new ArrayRef<Sequence>();
        public ArrayRef<Bone> Bones { get; set; } = new ArrayRef<Bone>();
        public ArrayRef<Vertex> Vertices { get; set; } = new ArrayRef<Vertex>();
        public List<View> Views { get; set; } = new List<View>();

        public void Load(BinaryReader stream, Format version = Format.Useless)
        {
            // LOAD MAGIC
            var magic = Encoding.UTF8.GetString(stream.ReadBytes(4));
            if (magic == "MD21") {
                stream.ReadBytes(4); // Ignore chunked structure of Legion
                stream = new BinaryReader(new Substream(stream.BaseStream));
                magic = Encoding.UTF8.GetString(stream.ReadBytes(4));
            }
            Debug.Assert(magic == "MD20");

            // LOAD HEADER
            if (version == Format.Useless) version = (Format) stream.ReadUInt32();
            else stream.ReadUInt32();
            Version = version;
            Debug.Assert(version != Format.Useless);
            var nameArrayRef = new ArrayRef<byte>();
            nameArrayRef.Load(stream, version);
            GlobalModelFlags = (GlobalFlags) stream.ReadUInt32();
            GlobalSequences.Load(stream, version);
            Sequences.Load(stream, version);
            new ArrayRef<short>().Load(stream, version);
            if(version < Format.LichKing) new ArrayRef<short>().Load(stream, version);
            Bones.Load(stream, version);
            new ArrayRef<short>().Load(stream, version);
            Vertices.Load(stream, version);
            var nViews = stream.ReadUInt32();
            uint ofsView = 0;
            if (version < Format.LichKing)
            {
                ofsView = stream.ReadUInt32();
            }

            // LOAD REFERENCED CONTENT
            nameArrayRef.LoadContent(stream);
            Name = nameArrayRef.ToNameString();
            GlobalSequences.LoadContent(stream);
            Sequences.LoadContent(stream, version);
            SetSequences();
            Bones.LoadContent(stream, version);
            Vertices.LoadContent(stream, version);

            if (version < Format.LichKing)
                stream.BaseStream.Seek(ofsView, SeekOrigin.Begin);
            for (var i = 0; i < nViews; i++)
            {
                var view = new View();
                if (version >= Format.LichKing)
                {
                    var skinFile = new BinaryReader(
                        new FileStream(View.SkinFileName(((FileStream) stream.BaseStream).Name, i), FileMode.Open));
                    view.Load(skinFile, version);
                    view.LoadContent(skinFile, version);
                }
                else
                {
                    view.Load(stream, version);
                    view.LoadContent(stream, version);
                }
                Views.Add(view);
            }
        }

        public void Save(BinaryWriter stream, Format version = Format.Useless)
        {
            SetSequences();

            // SAVE MAGIC
            stream.Write(Encoding.UTF8.GetBytes("MD20"));
            if(version == Format.Useless) version = Version;

            // SAVE HEADER
            Debug.Assert(version != Format.Useless);
            stream.Write((uint) version);
            var nameArrayRef = new ArrayRef<byte>(Name);
            nameArrayRef.Save(stream, version);
            stream.Write((uint) GlobalModelFlags);
            GlobalSequences.Save(stream, version);
            Sequences.Save(stream, version);
            var sequenceLookup = Sequence.GenerateAnimationLookup(Sequences);
            sequenceLookup.Save(stream, version);
            ArrayRef<short> playableLookup = null;
            if(version < Format.LichKing) playableLookup = Sequence.GenerateAnimationLookup(Sequences);
            Bones.Save(stream, version);
            var keyBoneLookup = Bone.GenerateKeyBoneLookup(Bones);
            keyBoneLookup.Save(stream, version);
            Vertices.Save(stream, version);
            stream.Write(Views.Count);
            var ofsViewFieldAddress = 0;//To rewrite ofsView
            if (version < Format.LichKing)
            {
                ofsViewFieldAddress = (int) stream.BaseStream.Position;
                stream.Write((uint) 0);
            }

            // SAVE REFERENCED CONTENT
            nameArrayRef.SaveContent(stream);
            GlobalSequences.SaveContent(stream);
            if(version < Format.LichKing)
            {
                uint time = 0;
                foreach(var seq in Sequences)
                {
                    time += 3333;
                    seq.TimeStart = time;
                    time += seq.Length;
                }
            }
            Sequences.SaveContent(stream, version);
            sequenceLookup.SaveContent(stream);
            playableLookup?.SaveContent(stream);
            Bones.SaveContent(stream, version);
            keyBoneLookup.SaveContent(stream);
            Vertices.SaveContent(stream, version);
            if (version < Format.LichKing)
            {
                Debug.Assert(ofsViewFieldAddress != 0);
                var ofsViews = (int) stream.BaseStream.Position;
                stream.Seek(ofsViewFieldAddress, SeekOrigin.Begin);
                stream.Write(ofsViews);
                stream.Seek(ofsViews, SeekOrigin.Begin);
            }
            foreach (var view in Views)
            for(var i = 0; i < Views.Count; i++)
            {
                if (version < Format.LichKing)
                {
                    view.Save(stream, version);
                }
                else
                {
                    var skinFile = new BinaryWriter(
                        new FileStream(View.SkinFileName(((FileStream) stream.BaseStream).Name, i), FileMode.Create));
                    view.Save(skinFile, version);
                    view.SaveContent(skinFile, version);
                }
            }
            if (version < Format.LichKing)
            {
                foreach (var view in Views)
                    view.SaveContent(stream, version);
            }
        }

        private void SetSequences()
        {
            Bones.SetSequences(Sequences);
        }

        /// <summary>
        /// Versions of M2 encountered so far.
        /// </summary>
        public enum Format
        {
            Useless = -1,
            Classic = 256,
            BurningCrusade = 260,
            LateBurningCrusade = 263,
            LichKing = 264,
            Cataclysm = 272,
            Pandaria = 272,
            Draenor = 272,
            Legion = 274
        }

        [Flags]
        public enum GlobalFlags
        {
            TiltX = 0x0001,
            TiltY = 0x0002,
            Add2Fields = 0x0008,
            LoadPhys = 0x0020,
            HasLod = 0x0080,
            CameraRelated = 0x0100 
        }
    }
}