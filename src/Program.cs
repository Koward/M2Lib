using System;
using System.Diagnostics;
using System.IO;
using m2lib_csharp.io;
using m2lib_csharp.types;

namespace m2lib_csharp
{
    class Program
    {
        static void Main(string[] args)
        {
            Debug.WriteLine("Debug application for M2Lib");
            BinaryWriter testWriter = new BinaryWriter(new FileStream("Test.b", FileMode.Create));
            ArrayRef<byte> array = new ArrayRef<byte>();
            array.AddRange(System.Text.Encoding.UTF8.GetBytes("IsThisTheRealLife?"));
            testWriter.Write(System.Text.Encoding.UTF8.GetBytes("TESTFILE"));
            array.Save(testWriter);
            testWriter.Write(System.Text.Encoding.UTF8.GetBytes("UMADBRO?"));
            array.SaveContent(testWriter);
        }
    }
}
