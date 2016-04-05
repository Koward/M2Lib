using System.Diagnostics;
using System.IO;
using m2lib_csharp.m2;
using m2lib_csharp.types;

namespace m2lib_csharp
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Debug.WriteLine("Debug application for M2Lib");
            var reader = new BinaryReader(new FileStream("Test.b", FileMode.Open));
            var model = new M2();
            model.Load(reader, M2.Format.Useless);
        }
    }
}