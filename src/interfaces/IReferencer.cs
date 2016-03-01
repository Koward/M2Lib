using System.IO;
using m2lib_csharp.m2;

namespace m2lib_csharp.interfaces
{
    /// <summary>
    /// Handles the content pointed by the ArrayRef objects in the structure.
    /// </summary>
    public interface IReferencer : IMarshalable
    {
        void LoadContent(BinaryReader stream, M2.Format version = M2.Format.Unknown);
        void SaveContent(BinaryWriter stream, M2.Format version = M2.Format.Unknown);
    }
}