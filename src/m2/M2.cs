using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using m2lib_csharp.interfaces;
using m2lib_csharp.io;
using m2lib_csharp.types;

namespace m2lib_csharp.m2
{
    /// <summary>
    ///     World of Warcraft model format.
    /// </summary>
    public class M2 : IMarshalable
    {
        /// <summary>
        ///     Versions of M2 encountered so far.
        /// </summary>
        public enum Format
        {
            Useless = 0xCAFE,
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

        private readonly M2Array<byte> _name = new M2Array<byte>();

        public Format Version { get; set; } = Format.Draenor;

        public string Name
        {
            get { return _name.ToNameString(); }
            set { _name.SetString(value); }
        }

        public GlobalFlags GlobalModelFlags { get; set; } = 0;
        public M2Array<int> GlobalSequences { get; } = new M2Array<int>();
        public M2Array<M2Sequence> Sequences { get; } = new M2Array<M2Sequence>();
        public M2Array<M2Bone> Bones { get; } = new M2Array<M2Bone>();
        public M2Array<M2Vertex> Vertices { get; } = new M2Array<M2Vertex>();
        public M2Array<M2SkinProfile> Views { get; } = new M2Array<M2SkinProfile>();
        public M2Array<M2Color> Colors { get; } = new M2Array<M2Color>();
        public M2Array<M2Texture> Textures { get; } = new M2Array<M2Texture>();
        public M2Array<M2TextureWeight> Transparencies { get; } = new M2Array<M2TextureWeight>();
        public M2Array<M2TextureTransform> TextureTransforms { get; } = new M2Array<M2TextureTransform>();
        public M2Array<M2Material> Materials { get; } = new M2Array<M2Material>();
        public M2Array<M2Attachment> Attachments { get; } = new M2Array<M2Attachment>();
        public M2Array<M2Event> Events { get; } = new M2Array<M2Event>();
        public M2Array<M2Light> Lights { get; } = new M2Array<M2Light>();
        public M2Array<M2Camera> Cameras { get; } = new M2Array<M2Camera>();

        //Data referenced by Views. TODO See if can be generated on the fly.
        public M2Array<short> BoneLookup { get; } = new M2Array<short>();
        public M2Array<short> TexLookup { get; } = new M2Array<short>();
        public M2Array<short> TexUnitLookup { get; } = new M2Array<short>();
        public M2Array<short> TransLookup { get; } = new M2Array<short>();
        public M2Array<short> UvAnimLookup { get; } = new M2Array<short>();

        public CAaBox BoundingBox { get; set; } = new CAaBox();
        public float BoundingSphereRadius { get; set; }
        public CAaBox CollisionBox { get; set; } = new CAaBox();
        public float CollisionSphereRadius { get; set; }
        public M2Array<ushort> BoundingTriangles { get; } = new M2Array<ushort>();
        public M2Array<C3Vector> BoundingVertices { get; } = new M2Array<C3Vector>();
        public M2Array<C3Vector> BoundingNormals { get; } = new M2Array<C3Vector>();
        public M2Array<M2Ribbon> Ribbons { get; } = new M2Array<M2Ribbon>();
        public M2Array<ushort> Particles { get; } = new M2Array<ushort>();//TODO Replace by real struct
        public M2Array<ushort> BlendingMaps { get; } = new M2Array<ushort>();

        public void Load(BinaryReader stream, Format version = Format.Useless)
        {
            // LOAD MAGIC
            var magic = Encoding.UTF8.GetString(stream.ReadBytes(4));
            if (magic == "MD21")
            {
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
            _name.Load(stream, version);
            GlobalModelFlags = (GlobalFlags) stream.ReadUInt32();
            GlobalSequences.Load(stream, version);
            Sequences.Load(stream, version);
            SkipArrayParsing(stream, version);
            if (version < Format.LichKing) SkipArrayParsing(stream, version);
            Bones.Load(stream, version);
            SkipArrayParsing(stream, version);
            Vertices.Load(stream, version);
            uint nViews = 0; //For Lich King external views system.
            if (version < Format.LichKing) Views.Load(stream, version);
            else nViews = stream.ReadUInt32();
            Colors.Load(stream, version);
            Textures.Load(stream, version);
            Transparencies.Load(stream, version);
            if(version < Format.LichKing) SkipArrayParsing(stream, version);//Unknown Ref
            TextureTransforms.Load(stream, version);
            SkipArrayParsing(stream, version);
            Materials.Load(stream, version);
            BoneLookup.Load(stream, version);
            TexLookup.Load(stream, version);
            TexUnitLookup.Load(stream, version);
            TransLookup.Load(stream, version);
            UvAnimLookup.Load(stream, version);
            BoundingBox.Load(stream, version);
            BoundingSphereRadius = stream.ReadSingle();
            CollisionBox.Load(stream, version);
            CollisionSphereRadius = stream.ReadSingle();
            BoundingTriangles.Load(stream, version);
            BoundingVertices.Load(stream, version);
            BoundingNormals.Load(stream, version);
            Attachments.Load(stream, version);
            SkipArrayParsing(stream, version);
            Events.Load(stream, version);
            Lights.Load(stream, version);
            Cameras.Load(stream, version);
            SkipArrayParsing(stream, version);
            Ribbons.Load(stream, version);
            Particles.Load(stream, version);
            if(GlobalModelFlags.HasFlag(GlobalFlags.Add2Fields)) BlendingMaps.Load(stream, version);

            // LOAD REFERENCED CONTENT
            _name.LoadContent(stream);
            GlobalSequences.LoadContent(stream);
            Sequences.LoadContent(stream, version);
            if (version >= Format.LichKing)
            {
                foreach (var seq in Sequences.Where(seq => (!seq.IsAlias) &&
                                                           seq.IsExtern))
                {
                    seq.ReadingAnimFile =
                        new BinaryReader(
                            new FileStream(seq.GetAnimFilePath(((FileStream) stream.BaseStream).Name), FileMode.Open));
                }
            }
            SetSequences();
            Bones.LoadContent(stream, version);
            Vertices.LoadContent(stream, version);
            //VIEWS
            if (version < Format.LichKing) Views.LoadContent(stream, version);
            else
            {
                for (var i = 0; i < nViews; i++)
                {
                    var view = new M2SkinProfile();
                    using (var skinFile = new BinaryReader(
                        new FileStream(M2SkinProfile.SkinFileName(((FileStream) stream.BaseStream).Name, i),
                            FileMode.Open)))
                    {
                        view.Load(skinFile, version);
                        view.LoadContent(skinFile, version);
                    }
                    Views.Add(view);
                }
            }
            //VIEWS END
            Colors.LoadContent(stream, version);
            Textures.LoadContent(stream, version);
            Transparencies.LoadContent(stream, version);
            TextureTransforms.LoadContent(stream, version);
            Materials.LoadContent(stream, version);
            BoneLookup.LoadContent(stream, version);
            TexLookup.LoadContent(stream, version);
            TexUnitLookup.LoadContent(stream, version);
            TransLookup.LoadContent(stream, version);
            UvAnimLookup.LoadContent(stream, version);
            BoundingTriangles.LoadContent(stream, version);
            BoundingVertices.LoadContent(stream, version);
            BoundingNormals.LoadContent(stream, version);
            Attachments.LoadContent(stream, version);
            Events.LoadContent(stream, version);
            Lights.LoadContent(stream, version);
            Cameras.LoadContent(stream, version);
            Ribbons.LoadContent(stream, version);
            Particles.LoadContent(stream, version);
            if(GlobalModelFlags.HasFlag(GlobalFlags.Add2Fields)) BlendingMaps.LoadContent(stream, version);
            foreach (var seq in Sequences)
                seq.ReadingAnimFile?.Close();
        }

        public void Save(BinaryWriter stream, Format version = Format.Useless)
        {
            SetSequences();

            // SAVE MAGIC
            stream.Write(Encoding.UTF8.GetBytes("MD20"));
            if (version == Format.Useless) version = Version;

            // SAVE HEADER
            stream.Write((uint) version);
            _name.Save(stream, version);
            stream.Write((uint) GlobalModelFlags);
            GlobalSequences.Save(stream, version);
            Sequences.Save(stream, version);
            var sequenceLookup = M2Sequence.GenerateAnimationLookup(Sequences);
            sequenceLookup.Save(stream, version);
            M2Array<short> playableLookup = null;
            if (version < Format.LichKing)
            {
                playableLookup = M2Sequence.GenerateAnimationLookup(Sequences);
                playableLookup.Save(stream, version);
            }
            Bones.Save(stream, version);
            var keyBoneLookup = M2Bone.GenerateKeyBoneLookup(Bones);
            keyBoneLookup.Save(stream, version);
            Vertices.Save(stream, version);
            if (version < Format.LichKing) Views.Save(stream, version);
            else stream.Write(Views.Count);
            Colors.Save(stream, version);
            Textures.Save(stream, version);
            Transparencies.Save(stream, version);
            if(version < Format.LichKing) stream.Write((long) 0);//Unknown Ref
            TextureTransforms.Save(stream, version);
            var texReplaceLookup = M2Texture.GenerateTexReplaceLookup(Textures);
            texReplaceLookup.Save(stream, version);
            Materials.Save(stream, version);
            BoneLookup.Save(stream, version);
            TexLookup.Save(stream, version);
            TexUnitLookup.Save(stream, version);
            TransLookup.Save(stream, version);
            UvAnimLookup.Save(stream, version);
            BoundingBox.Save(stream, version);
            stream.Write(BoundingSphereRadius);
            CollisionBox.Save(stream, version);
            stream.Write(CollisionSphereRadius);
            BoundingTriangles.Save(stream, version);
            BoundingVertices.Save(stream, version);
            BoundingNormals.Save(stream, version);
            Attachments.Save(stream, version);
            var attachmentLookup = M2Attachment.GenerateLookup(Attachments);
            attachmentLookup.Save(stream, version);
            Events.Save(stream, version);
            Lights.Save(stream, version);
            Cameras.Save(stream, version);
            var cameraLookup = M2Camera.GenerateLookup(Cameras);
            cameraLookup.Save(stream, version);
            Ribbons.Save(stream, version);
            Particles.Save(stream, version);
            if(version >= Format.LichKing && GlobalModelFlags.HasFlag(GlobalFlags.Add2Fields)) BlendingMaps.Save(stream, version);

            // SAVE REFERENCED CONTENT
            _name.SaveContent(stream);
            GlobalSequences.SaveContent(stream);
            if (version < Format.LichKing)
            {
                uint time = 0;
                //Alias system. Alias should be skipped in the timing
                foreach (var seq in Sequences.Where(seq => !seq.IsAlias))
                {
                    time += 3333;
                    seq.TimeStart = time;
                    time += seq.Length;
                }
                //set the timeStart of Alias to their real counterpart
                foreach (var seq in Sequences.Where(seq => seq.IsAlias))
                    seq.TimeStart = seq.GetRealSequence(Sequences).TimeStart;
            }
            Sequences.SaveContent(stream, version);
            if (version >= Format.LichKing)
            {
                foreach (var seq in Sequences.Where(seq => (!seq.IsAlias) &&
                                                           seq.IsExtern))
                {
                    seq.WritingAnimFile =
                        new BinaryWriter(
                            new FileStream(seq.GetAnimFilePath(((FileStream) stream.BaseStream).Name), FileMode.Create));
                }
            }
            sequenceLookup.SaveContent(stream);
            playableLookup?.SaveContent(stream);
            Bones.SaveContent(stream, version);
            keyBoneLookup.SaveContent(stream);
            Vertices.SaveContent(stream, version);
            //VIEWS
            if (version < Format.LichKing) Views.SaveContent(stream, version);
            else
            {
                for (var i = 0; i < Views.Count; i++)
                {
                    using (var skinFile = new BinaryWriter(
                        new FileStream(M2SkinProfile.SkinFileName(((FileStream) stream.BaseStream).Name, i),
                            FileMode.Create)))
                    {
                        Views[i].Save(skinFile, version);
                        Views[i].SaveContent(skinFile, version);
                    }
                }
            }
            //VIEWS END
            Colors.SaveContent(stream, version);
            Textures.SaveContent(stream, version);
            Transparencies.SaveContent(stream, version);
            TextureTransforms.SaveContent(stream, version);
            texReplaceLookup.SaveContent(stream, version);
            Materials.SaveContent(stream, version);
            BoneLookup.SaveContent(stream, version);
            TexLookup.SaveContent(stream, version);
            TexUnitLookup.SaveContent(stream, version);
            TransLookup.SaveContent(stream, version);
            UvAnimLookup.SaveContent(stream, version);
            BoundingTriangles.SaveContent(stream, version);
            BoundingVertices.SaveContent(stream, version);
            BoundingNormals.SaveContent(stream, version);
            Attachments.SaveContent(stream, version);
            attachmentLookup.SaveContent(stream, version);
            Events.SaveContent(stream, version);
            Lights.SaveContent(stream, version);
            Cameras.SaveContent(stream, version);
            cameraLookup.SaveContent(stream, version);
            Ribbons.SaveContent(stream, version);
            Particles.Save(stream, version);
            if(GlobalModelFlags.HasFlag(GlobalFlags.Add2Fields)) BlendingMaps.SaveContent(stream, version);
            foreach (var seq in Sequences)
                seq.WritingAnimFile?.Close();
        }

        private void SetSequences()
        {
            Bones.PassSequences(Sequences);
            Colors.PassSequences(Sequences);
            Transparencies.PassSequences(Sequences);
            TextureTransforms.PassSequences(Sequences);
            Attachments.PassSequences(Sequences);
            Events.PassSequences(Sequences);
            Lights.PassSequences(Sequences);
            Cameras.PassSequences(Sequences);
            Ribbons.PassSequences(Sequences);
            //Particles.PassSequences(Sequences);//TODO Once Particles are done.
        }

        /// <summary>
        ///     Skip the parsing of useless M2Array (like lookups, since lookups are generated at writing).
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="version"></param>
        private void SkipArrayParsing(BinaryReader stream, Format version)
        {
            new M2Array<short>().Load(stream, version);
        }
    }
}