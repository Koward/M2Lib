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
            var model = new M2();
            model.Name = "TestModel";
            var bone = new Bone();
            bone.KeyBoneId = 0;
            model.Bones.Add(bone);
            var anim = new Sequence();
            anim.AnimationId = 42;
            model.Sequences.Add(anim);
            var tex = new Texture();
            tex.Name = "ThisIsHardcoded.BLP";
            model.Textures.Add(tex);
            var writer = new BinaryWriter(new FileStream("Test.m2", FileMode.Create));
            model.Save(writer, M2.Format.LichKing);
        }
    }
}