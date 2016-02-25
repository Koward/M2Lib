using System.Diagnostics;
using System.IO;
using m2lib_csharp.types;

namespace m2lib_csharp
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Debug.WriteLine("Debug application for M2Lib");
            var testWriter = new BinaryWriter(new FileStream("Test.b", FileMode.Create));
            var array = new ArrayRef<byte>("Four");
            testWriter.Write(System.Text.Encoding.UTF8.GetBytes("TEST"));
            array.Save(testWriter);
            array.SaveContent(testWriter);
            testWriter.Close();
            var testReader = new BinaryReader(new FileStream("Test.b", FileMode.Open));
            Debug.WriteLine(System.Text.Encoding.UTF8.GetString(testReader.ReadBytes(4)));
            array = new ArrayRef<byte>();
            array.Load(testReader);
            array.LoadContent(testReader);
            Debug.WriteLine(array.ToNameString());
            Debug.WriteLine("Length : " + array.ToNameString().Length);
            Debug.WriteLine("Done.");
            testReader.Close();
        }
    }
}
