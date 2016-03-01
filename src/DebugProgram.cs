using System.Diagnostics;
using System.IO;

namespace m2lib_csharp
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Debug.WriteLine("Debug application for M2Lib");
            const string path = "K:\\Potato\\Potato.m2";
            const ushort animId = 666;
            const ushort subAnimId = 42;
            var pathDirectory = Path.GetDirectoryName(path);
            var pathWithoutExt = Path.GetFileNameWithoutExtension(path);
            string animFileName = $"{pathWithoutExt}{animId:0000}-{subAnimId:00}.anim";
            Debug.Assert(pathDirectory != null, "pathDirectory != null");
            var animFilePath = Path.Combine(pathDirectory, animFileName);
            Debug.WriteLine(animFilePath);
        }
    }
}