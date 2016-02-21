using System.IO;

namespace m2lib_csharp.interfaces
{
    public interface IMarshalable
    {
        void Load(BinaryReader stream, int version = -1);
        void Save(BinaryWriter stream, int version = -1);
    }
}