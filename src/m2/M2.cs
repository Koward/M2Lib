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

        private readonly M2Array<short> _boneLookup = new M2Array<short>();
        private readonly M2Array<M2Bone> _bones = new M2Array<M2Bone>();
        private readonly M2Array<C3Vector> _boundingNormals = new M2Array<C3Vector>();
        private readonly M2Array<ushort> _boundingTriangles = new M2Array<ushort>();
        private readonly M2Array<C3Vector> _boundingVertices = new M2Array<C3Vector>();
        private readonly M2Array<int> _globalSequences = new M2Array<int>();
        private readonly M2Array<M2Material> _materials = new M2Array<M2Material>();
        private readonly M2Array<M2Sequence> _sequences = new M2Array<M2Sequence>();
        private readonly M2Array<M2Color> _colors = new M2Array<M2Color>();
        private readonly M2Array<short> _texLookup = new M2Array<short>();
        private readonly M2Array<M2Texture> _textures = new M2Array<M2Texture>();
        private readonly M2Array<M2TextureTransform> _textureTransforms = new M2Array<M2TextureTransform>();
        private readonly M2Array<short> _texUnitLookup = new M2Array<short>();
        private readonly M2Array<short> _transLookup = new M2Array<short>();
        private readonly M2Array<M2TextureWeight> _transparencies = new M2Array<M2TextureWeight>();
        private readonly M2Array<short> _uvAnimLookup = new M2Array<short>();
        private readonly M2Array<M2Vertex> _vertices = new M2Array<M2Vertex>();
        private readonly M2Array<M2SkinProfile> _views = new M2Array<M2SkinProfile>();
        private readonly M2Array<M2Attachment> _attachments = new M2Array<M2Attachment>();
        private readonly M2Array<M2Event> _events = new M2Array<M2Event>();
        private readonly M2Array<M2Light> _lights = new M2Array<M2Light>();
        private readonly M2Array<M2Camera> _cameras = new M2Array<M2Camera>();
        private M2Array<byte> _name = new M2Array<byte>();

        public Format Version { get; set; } = Format.Draenor;

        public string Name
        {
            get { return _name.ToNameString(); }
            set { _name = new M2Array<byte>(value); }
        }

        public GlobalFlags GlobalModelFlags { get; set; } = 0;
        public List<int> GlobalSequences => _globalSequences;
        public List<M2Sequence> Sequences => _sequences;
        public List<M2Bone> Bones => _bones;
        public List<M2Vertex> Vertices => _vertices;
        public List<M2SkinProfile> Views => _views;
        public List<M2Color> Colors => _colors;
        public List<M2Texture> Textures => _textures;
        public List<M2TextureWeight> Transparencies => _transparencies;
        public List<M2TextureTransform> TextureTransforms => _textureTransforms;
        public List<M2Material> Materials => _materials;
        public List<M2Attachment> Attachments => _attachments;
        public List<M2Event> Events => _events;
        public List<M2Light> Lights => _lights;
        public List<M2Camera> Cameras => _cameras;

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
            _colors.Load(stream, version);
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
            SkipLookupParsing(stream, version);

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
                    var view = new M2SkinProfile();
                    var skinFile = new BinaryReader(
                        new FileStream(M2SkinProfile.SkinFileName(((FileStream) stream.BaseStream).Name, i), FileMode.Open));
                    view.Load(skinFile, version);
                    view.LoadContent(skinFile, version);
                    _views.Add(view);
                }
            }
            //VIEWS END
            _colors.LoadContent(stream, version);
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
            var sequenceLookup = M2Sequence.GenerateAnimationLookup(_sequences);
            sequenceLookup.Save(stream, version);
            M2Array<short> playableLookup = null;
            if (version < Format.LichKing) playableLookup = M2Sequence.GenerateAnimationLookup(_sequences);
            _bones.Save(stream, version);
            var keyBoneLookup = M2Bone.GenerateKeyBoneLookup(_bones);
            keyBoneLookup.Save(stream, version);
            _vertices.Save(stream, version);
            if (version < Format.LichKing) _views.Save(stream, version);
            else stream.Write(_views.Count);
            _colors.Save(stream, version);
            _textures.Save(stream, version);
            _transparencies.Save(stream, version);
            _textureTransforms.Save(stream, version);
            var texReplaceLookup = M2Texture.GenerateTexReplaceLookup(_textures);
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
            var attachmentLookup = M2Attachment.GenerateLookup(_attachments);
            attachmentLookup.Save(stream, version);
            _events.Save(stream, version);
            _lights.Save(stream, version);
            _cameras.Save(stream, version);
            var cameraLookup = M2Camera.GenerateLookup(_cameras);
            cameraLookup.Save(stream, version);

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
                        new FileStream(M2SkinProfile.SkinFileName(((FileStream) stream.BaseStream).Name, i), FileMode.Create));
                    _views[i].Save(skinFile, version);
                    _views[i].SaveContent(skinFile, version);
                }
            }
            //VIEWS END
            _colors.SaveContent(stream, version);
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
            cameraLookup.SaveContent(stream, version);
        }

        private void SetSequences()
        {
            _bones.SetSequences(_sequences);
            _colors.SetSequences(_sequences);
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
            new M2Array<short>().Load(stream, version);
        }
    }
}