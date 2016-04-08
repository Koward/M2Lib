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
    ///     World of Warcraft model format.
    /// </summary>
    public class M2 : IMarshalable
    {
        /// <summary>
        ///     Versions of M2 encountered so far.
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

        private readonly ArrayRef<short> _boneLookup = new ArrayRef<short>();
        private readonly ArrayRef<Bone> _bones = new ArrayRef<Bone>();
        private readonly ArrayRef<C3Vector> _boundingNormals = new ArrayRef<C3Vector>();
        private readonly ArrayRef<ushort> _boundingTriangles = new ArrayRef<ushort>();
        private readonly ArrayRef<C3Vector> _boundingVertices = new ArrayRef<C3Vector>();
        private readonly ArrayRef<int> _globalSequences = new ArrayRef<int>();
        private readonly ArrayRef<Material> _materials = new ArrayRef<Material>();
        private readonly ArrayRef<Sequence> _sequences = new ArrayRef<Sequence>();
        private readonly ArrayRef<SubmeshAnimation> _submeshAnimations = new ArrayRef<SubmeshAnimation>();
        private readonly ArrayRef<short> _texLookup = new ArrayRef<short>();
        private readonly ArrayRef<Texture> _textures = new ArrayRef<Texture>();
        private readonly ArrayRef<TextureTransform> _textureTransforms = new ArrayRef<TextureTransform>();
        private readonly ArrayRef<short> _texUnitLookup = new ArrayRef<short>();
        private readonly ArrayRef<short> _transLookup = new ArrayRef<short>();
        private readonly ArrayRef<Transparency> _transparencies = new ArrayRef<Transparency>();
        private readonly ArrayRef<short> _uvAnimLookup = new ArrayRef<short>();
        private readonly ArrayRef<Vertex> _vertices = new ArrayRef<Vertex>();
        private readonly ArrayRef<View> _views = new ArrayRef<View>();
        private readonly ArrayRef<Attachment> _attachments = new ArrayRef<Attachment>();
        private readonly ArrayRef<Event> _events = new ArrayRef<Event>();
        private readonly ArrayRef<Light> _lights = new ArrayRef<Light>();
        private readonly ArrayRef<Camera> _cameras = new ArrayRef<Camera>();
        private ArrayRef<byte> _name = new ArrayRef<byte>();

        public Format Version { get; set; } = Format.Draenor;

        public string Name
        {
            get { return _name.ToNameString(); }
            set { _name = new ArrayRef<byte>(value); }
        }

        public GlobalFlags GlobalModelFlags { get; set; } = 0;
        public List<int> GlobalSequences => _globalSequences;
        public List<Sequence> Sequences => _sequences;
        public List<Bone> Bones => _bones;
        public List<Vertex> Vertices => _vertices;
        public List<View> Views => _views;
        public List<SubmeshAnimation> SubmeshAnimations => _submeshAnimations;
        public List<Texture> Textures => _textures;
        public List<Transparency> Transparencies => _transparencies;
        public List<TextureTransform> TextureTransforms => _textureTransforms;
        public List<Material> Materials => _materials;
        public List<Attachment> Attachments => _attachments;
        public List<Event> Events => _events;
        public List<Light> Lights => _lights;
        public List<Camera> Cameras => _cameras;

        //Data referenced by Views. TODO See if can be generated on the fly.
        public List<short> BoneLookup => _boneLookup;
        public List<short> TexLookup => _texLookup;
        public List<short> TexUnitLookup => _texUnitLookup;
        public List<short> TransLookup => _transLookup;
        public List<short> UvAnimLookup => _uvAnimLookup;

        public CAaBox BoundingBox { get; set; } = new CAaBox();
        public float BoundingSphereRadius { get; set; }
        public CAaBox CollisionBox { get; set; } = new CAaBox();
        public float CollisionSphereRadius { get; set; }
        public List<ushort> BoundingTriangles => _boundingTriangles;
        public List<C3Vector> BoundingVertices => _boundingVertices;
        public List<C3Vector> BoundingNormals => _boundingNormals;

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
            _globalSequences.Load(stream, version);
            _sequences.Load(stream, version);
            SkipLookupParsing(stream, version);
            if (version < Format.LichKing) SkipLookupParsing(stream, version);
            _bones.Load(stream, version);
            SkipLookupParsing(stream, version);
            _vertices.Load(stream, version);
            uint nViews = 0; //For Lich King external views system.
            if (version < Format.LichKing) _views.Load(stream, version);
            else nViews = stream.ReadUInt32();
            _submeshAnimations.Load(stream, version);
            _textures.Load(stream, version);
            _transparencies.Load(stream, version);
            _textureTransforms.Load(stream, version);
            SkipLookupParsing(stream, version);
            _materials.Load(stream, version);
            _boneLookup.Load(stream, version);
            _texLookup.Load(stream, version);
            _texUnitLookup.Load(stream, version);
            _transLookup.Load(stream, version);
            _uvAnimLookup.Load(stream, version);
            BoundingBox.Load(stream, version);
            BoundingSphereRadius = stream.ReadSingle();
            CollisionBox.Load(stream, version);
            CollisionSphereRadius = stream.ReadSingle();
            _boundingTriangles.Load(stream, version);
            _boundingVertices.Load(stream, version);
            _boundingNormals.Load(stream, version);
            _attachments.Load(stream, version);
            SkipLookupParsing(stream, version);
            _events.Load(stream, version);
            _lights.Load(stream, version);
            _cameras.Load(stream, version);

            // LOAD REFERENCED CONTENT
            _name.LoadContent(stream);
            _globalSequences.LoadContent(stream);
            _sequences.LoadContent(stream, version);
            SetSequences();
            _bones.LoadContent(stream, version);
            _vertices.LoadContent(stream, version);
            //VIEWS
            if (version < Format.LichKing) _views.LoadContent(stream, version);
            else
            {
                for (var i = 0; i < nViews; i++)
                {
                    var view = new View();
                    var skinFile = new BinaryReader(
                        new FileStream(View.SkinFileName(((FileStream) stream.BaseStream).Name, i), FileMode.Open));
                    view.Load(skinFile, version);
                    view.LoadContent(skinFile, version);
                    _views.Add(view);
                }
            }
            //VIEWS END
            _submeshAnimations.LoadContent(stream, version);
            _textures.LoadContent(stream, version);
            _transparencies.LoadContent(stream, version);
            _textureTransforms.LoadContent(stream, version);
            _materials.LoadContent(stream, version);
            _boneLookup.LoadContent(stream, version);
            _texLookup.LoadContent(stream, version);
            _texUnitLookup.LoadContent(stream, version);
            _transLookup.LoadContent(stream, version);
            _uvAnimLookup.LoadContent(stream, version);
            _boundingTriangles.LoadContent(stream, version);
            _boundingVertices.LoadContent(stream, version);
            _boundingNormals.LoadContent(stream, version);
            _attachments.LoadContent(stream, version);
            _events.LoadContent(stream, version);
            _lights.LoadContent(stream, version);
            _cameras.LoadContent(stream, version);
        }

        public void Save(BinaryWriter stream, Format version = Format.Useless)
        {
            SetSequences();

            // SAVE MAGIC
            stream.Write(Encoding.UTF8.GetBytes("MD20"));
            if (version == Format.Useless) version = Version;

            // SAVE HEADER
            Debug.Assert(version != Format.Useless);
            stream.Write((uint) version);
            _name.Save(stream, version);
            stream.Write((uint) GlobalModelFlags);
            _globalSequences.Save(stream, version);
            _sequences.Save(stream, version);
            var sequenceLookup = Sequence.GenerateAnimationLookup(_sequences);
            sequenceLookup.Save(stream, version);
            ArrayRef<short> playableLookup = null;
            if (version < Format.LichKing) playableLookup = Sequence.GenerateAnimationLookup(_sequences);
            _bones.Save(stream, version);
            var keyBoneLookup = Bone.GenerateKeyBoneLookup(_bones);
            keyBoneLookup.Save(stream, version);
            _vertices.Save(stream, version);
            if (version < Format.LichKing) _views.Save(stream, version);
            else stream.Write(_views.Count);
            _submeshAnimations.Save(stream, version);
            _textures.Save(stream, version);
            _transparencies.Save(stream, version);
            _textureTransforms.Save(stream, version);
            var texReplaceLookup = Texture.GenerateTexReplaceLookup(_textures);
            texReplaceLookup.Save(stream, version);
            _materials.Save(stream, version);
            _boneLookup.Save(stream, version);
            _texLookup.Save(stream, version);
            _texUnitLookup.Save(stream, version);
            _transLookup.Save(stream, version);
            _uvAnimLookup.Save(stream, version);
            BoundingBox.Save(stream, version);
            stream.Write(BoundingSphereRadius);
            CollisionBox.Save(stream, version);
            stream.Write(CollisionSphereRadius);
            _boundingTriangles.Save(stream, version);
            _boundingVertices.Save(stream, version);
            _boundingNormals.Save(stream, version);
            _attachments.Save(stream, version);
            var attachmentLookup = Attachment.GenerateAttachmentLookup(_attachments);
            attachmentLookup.Save(stream, version);
            _events.Save(stream, version);
            _lights.Save(stream, version);
            _cameras.Save(stream, version);

            // SAVE REFERENCED CONTENT
            _name.SaveContent(stream);
            _globalSequences.SaveContent(stream);
            if (version < Format.LichKing)
            {
                uint time = 0;
                foreach (var seq in _sequences)
                {
                    time += 3333;
                    seq.TimeStart = time;
                    time += seq.Length;
                }
            }
            _sequences.SaveContent(stream, version);
            sequenceLookup.SaveContent(stream);
            playableLookup?.SaveContent(stream);
            _bones.SaveContent(stream, version);
            keyBoneLookup.SaveContent(stream);
            _vertices.SaveContent(stream, version);
            //VIEWS
            if (version < Format.LichKing) _views.SaveContent(stream, version);
            else
            {
                for (var i = 0; i < _views.Count; i++)
                {
                    var skinFile = new BinaryWriter(
                        new FileStream(View.SkinFileName(((FileStream) stream.BaseStream).Name, i), FileMode.Create));
                    _views[i].Save(skinFile, version);
                    _views[i].SaveContent(skinFile, version);
                }
            }
            //VIEWS END
            _submeshAnimations.SaveContent(stream, version);
            _textures.SaveContent(stream, version);
            _transparencies.SaveContent(stream, version);
            _textureTransforms.SaveContent(stream, version);
            texReplaceLookup.SaveContent(stream, version);
            _materials.SaveContent(stream, version);
            _boneLookup.SaveContent(stream, version);
            _texLookup.SaveContent(stream, version);
            _texUnitLookup.SaveContent(stream, version);
            _transLookup.SaveContent(stream, version);
            _uvAnimLookup.SaveContent(stream, version);
            _boundingTriangles.SaveContent(stream, version);
            _boundingVertices.SaveContent(stream, version);
            _boundingNormals.SaveContent(stream, version);
            _attachments.SaveContent(stream, version);
            attachmentLookup.SaveContent(stream, version);
            _events.SaveContent(stream, version);
            _lights.SaveContent(stream, version);
            _cameras.SaveContent(stream, version);
        }

        private void SetSequences()
        {
            _bones.SetSequences(_sequences);
            _submeshAnimations.SetSequences(_sequences);
            _transparencies.SetSequences(_sequences);
            _textureTransforms.SetSequences(_sequences);
            _attachments.SetSequences(_sequences);
            _events.SetSequences(_sequences);
            _lights.SetSequences(_sequences);
            _cameras.SetSequences(_sequences);
        }

        /// <summary>
        ///     Skip the parsing of useless lookups, since lookups are generated at writing.
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="version"></param>
        private void SkipLookupParsing(BinaryReader stream, Format version)
        {
            new ArrayRef<short>().Load(stream, version);
        }
    }
}