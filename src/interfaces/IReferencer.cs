using System.IO;

namespace m2lib_csharp.interfaces
{
    /// <summary>
    /// Handles the content pointed by the ArrayRef objects in the structure.
    /// </summary>
    public interface IReferencer : IMarshalable
    {
        void LoadContent(BinaryReader stream, int version = -1, BinaryReader[] animFiles = null);
        void SaveContent(BinaryWriter stream, int version = -1, BinaryWriter[] animFiles = null);
    }
}