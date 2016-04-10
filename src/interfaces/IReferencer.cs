using System.IO;
using m2lib_csharp.m2;

namespace m2lib_csharp.interfaces
{
    /// <summary>
    ///     Handles the content pointed by the M2Array objects in the structure.
    /// </summary>
    public interface IReferencer : IMarshalable
    {
        void LoadContent(BinaryReader stream, M2.Format version);
        void SaveContent(BinaryWriter stream, M2.Format version);
    }
}