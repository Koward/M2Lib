using System.IO;
using m2lib_csharp.interfaces;
using m2lib_csharp.io;

namespace m2lib_csharp.m2
{
    /// <summary>
    /// World of Warcraft model format.
    /// </summary>
    class M2 : IMarshalable
    {
        public int Version { get; set; }
        // TODO Everything.

        public M2()
        {
            Version = (int) Format.Draenor;
        }

        public void Load(BinaryReader stream, int version = -1)
        {
            string magic = System.Text.Encoding.UTF8.GetString(stream.ReadBytes(4));
            if (magic == "MD21") {
                stream.ReadBytes(4); // Ignore chunked structure of Legion
                stream = new BinaryReader(new Substream(stream.BaseStream));
                magic = System.Text.Encoding.UTF8.GetString(stream.ReadBytes(4));
            }
            if (magic != "MD20") throw new System.NotImplementedException("[Invalid Magic] This file is not recognized as M2");
        }

        public void Save(BinaryWriter stream, int version = -1)
        {
            throw new System.NotImplementedException();
        }
    }

    /// <summary>
    /// Versions of M2 encountered so far.
    /// </summary>
    enum Format
    {
        Classic = 256,
        BurningCrusade = 260,
        LichKing = 264,
        Cataclysm = 272,
        Pandaria = 272,
        Draenor = 272,
        Legion = 274
    }
}
