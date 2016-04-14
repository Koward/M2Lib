using System.Diagnostics;
using System.IO;
using m2lib_csharp.m2;

namespace m2lib_csharp
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Debug.WriteLine("Debug application for M2Lib");

            const string fileName = "Character/Human/Male/HumanMale_HD.m2";
            //const string fileName = "world/arttest/boxtest/xyz.m2";
            //const string fileName2 = "Classic/HumanMale_HD.m2";
            //const string fileName = "LichKing/Frog.m2";
            var model = new M2();
            using (var reader = new BinaryReader(new FileStream(fileName, FileMode.Open)))
                model.Load(reader);
            Debug.WriteLine(model.Name);
            /*
            using (var writer = new BinaryWriter(new FileStream(fileName2, FileMode.Create)))
                model.Save(writer, M2.Format.Classic);
                */
        }
    }
}