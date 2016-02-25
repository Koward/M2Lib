using System;
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

        public void Load(BinaryReader stream, Format version = Format.Unknown)
        {
            var magic = Encoding.UTF8.GetString(stream.ReadBytes(4));
            if (magic == "MD21") {
                stream.ReadBytes(4); // Ignore chunked structure of Legion
                stream = new BinaryReader(new Substream(stream.BaseStream));
                magic = Encoding.UTF8.GetString(stream.ReadBytes(4));
            }
            if (magic != "MD20") throw new NotImplementedException("Invalid Magic: "+ magic +" is not recognized as M2");

            if (version == Format.Unknown) version = (Format) stream.ReadUInt32();
            else stream.ReadUInt32();
            Version = version;
            var nameArrayRef = new ArrayRef<byte>();
            nameArrayRef.Load(stream);
            GlobalModelFlags = (GlobalFlags) stream.ReadUInt32();
            GlobalSequences.Load(stream);
            Sequences.Load(stream, version);
            new ArrayRef<short>().Load(stream);
            if(version <= Format.BurningCrusade) new ArrayRef<short>().Load(stream);
            //TODO Everything

            nameArrayRef.LoadContent(stream);
            Name = nameArrayRef.ToNameString();
            GlobalSequences.LoadContent(stream);
            Sequences.LoadContent(stream, version);
        }

        public void Save(BinaryWriter stream, Format version = Format.Unknown)
        {
            stream.Write(Encoding.UTF8.GetBytes("MD20"));
            if(version == Format.Unknown) version = Version;
            stream.Write((uint) version);
            var nameArrayRef = new ArrayRef<byte>(Name);
            nameArrayRef.Save(stream);
            stream.Write((uint) GlobalModelFlags);
            GlobalSequences.Save(stream);
            Sequences.Save(stream, version);
            var sequenceLookup = Sequence.BuildLookup(Sequences);
            sequenceLookup.Save(stream);
            ArrayRef<short> playableLookup = null;
            if(version <= Format.BurningCrusade) playableLookup = Sequence.BuildLookup(Sequences);
            //TODO Everything

            nameArrayRef.SaveContent(stream);
            GlobalSequences.SaveContent(stream);
            if(version <= Format.BurningCrusade)
            {
                uint time = 0;
                foreach(var seq in Sequences)
                {
                    time += 3333;
                    seq.TimeStart = time;
                }
            }
            Sequences.SaveContent(stream, version);
            sequenceLookup.SaveContent(stream);
            playableLookup?.SaveContent(stream);
        }

        /// <summary>
        /// Versions of M2 encountered so far.
        /// </summary>
        public enum Format
        {
            Unknown = -1,
            Classic = 256,
            BurningCrusade = 260,
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
