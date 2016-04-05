using System;
using System.IO;
using m2lib_csharp.interfaces;

namespace m2lib_csharp.m2
{
    /// <summary>
    /// A playable record for a sequence.
    /// </summary>
    public class PlayableRecord : IMarshalable
    {
        public ushort FallbackId;
        public PlayFlags Flags;

        public PlayableRecord(ushort p1, PlayFlags p2)
        {
            FallbackId = p1;
            Flags = p2;
        }

        public PlayableRecord() : this(0,0)
        {
        }

        public void Load(BinaryReader stream, M2.Format version)
        {
            FallbackId = stream.ReadUInt16();
            Flags = (PlayFlags) stream.ReadUInt16();
        }

        public void Save(BinaryWriter stream, M2.Format version)
        {
            stream.Write(FallbackId);
            stream.Write((ushort) Flags);
        }

        [Flags]
        public enum PlayFlags : ushort 
        {
            Loop = 0,
            Backwards = 1,
            Freeze = 3
        }
    }
}