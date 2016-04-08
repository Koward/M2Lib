using System;
using System.IO;
using System.Linq;
using m2lib_csharp.interfaces;
using m2lib_csharp.types;

namespace m2lib_csharp.m2
{
    public class Texture : IReferencer
    {
        [Flags]
        public enum TextureFlags : uint
        {
            WrapX = 0x01,
            WrapY = 0x02
        }

        public enum TextureType : uint
        {
            None = 0,
            Skin = 1,
            ObjectSkin = 2,
            WeaponBlade = 3,
            WeaponHandle = 4,
            Environment = 5,
            CharacterHair = 6,
            CharacterFacialHair = 7,
            SkinExtra = 8,
            UiSkin = 9,
            TaurenMane = 10,
            MonsterSkin1 = 11,
            MonsterSkin2 = 12,
            MonsterSkin3 = 13,
            ItemIcon = 14,
            GuildBackgroundColor = 15,
            GuildEmblemColor = 16,
            GuildBorderColor = 17,
            GuildEmblem = 18
        }

        private ArrayRef<byte> _nameArrayRef = new ArrayRef<byte>();
        public TextureType Type { get; set; }
        public TextureFlags Flags { get; set; }
        public string Name { get; set; } = "";

        public void Load(BinaryReader stream, M2.Format version)
        {
            Type = (TextureType) stream.ReadUInt32();
            Flags = (TextureFlags) stream.ReadUInt32();
            _nameArrayRef.Load(stream, version);
        }

        public void Save(BinaryWriter stream, M2.Format version)
        {
            stream.Write((uint) Type);
            stream.Write((uint) Flags);
            _nameArrayRef = new ArrayRef<byte>(Name);
            _nameArrayRef.Save(stream, version);
        }

        public void LoadContent(BinaryReader stream, M2.Format version)
        {
            _nameArrayRef.LoadContent(stream, version);
            Name = _nameArrayRef.ToNameString();
        }

        public void SaveContent(BinaryWriter stream, M2.Format version)
        {
            _nameArrayRef.SaveContent(stream, version);
        }

        public static ArrayRef<short> GenerateTexReplaceLookup(ArrayRef<Texture> textures)
        {
            var lookup = new ArrayRef<short>();
            var maxId = (short) textures.Max(x => x.Type);
            for (short i = 0; i <= maxId; i++) lookup.Add(-1);
            for (short i = 0; i < textures.Count; i++)
            {
                var id = (short) textures[i].Type;
                if (lookup[id] == -1) lookup[id] = i;
            }
            return lookup;
        }
    }
}