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
            var reader = new BinaryReader(new FileStream("Character/Human/Male/HumanMale_HD.m2", FileMode.Open));
            var model = new M2();
            model.Load(reader);
            reader.Close();
            Debug.WriteLine(model.Textures[0].Name);
            /*
            var point = new FixedPoint(0, 15);
            point.Bits[14] = true;
            point.Bits[15] = true;
            //Expect -0.5
            Debug.WriteLine("Created]");
            Debug.WriteLine("Bits : " + point.Bits.ToBitString());
            Debug.WriteLine("Value : " + point.Value);
            var writer = new BinaryWriter(new FileStream("Test.b", FileMode.Create));
            point.Save(writer);
            writer.Close();
            var reader = new BinaryReader(new FileStream("Test.b", FileMode.Open));
            var newPoint = new FixedPoint(0, 15);
            newPoint.Load(reader);
            reader.Close();
            Debug.WriteLine("Written&Read]");
            Debug.WriteLine("Bits : " + newPoint.Bits.ToBitString());
            Debug.WriteLine("Value : " + newPoint.Value);
            */

            /*
            var model = new M2();
            model.Name = "TestModel";
            var bone = new M2Bone();
            bone.KeyBoneId = 0;
            model.Bones.Add(bone);
            var anim = new M2Sequence();
            anim.AnimationId = 42;
            model.Sequences.Add(anim);
            var tex = new M2Texture();
            tex.Name = "ThisIsHardcoded.BLP";
            model.Textures.Add(tex);
            var writer = new BinaryWriter(new FileStream("Test.m2", FileMode.Create));
            model.Save(writer, M2.Format.LichKing);
            writer.Close();
            */
        }
    }
}