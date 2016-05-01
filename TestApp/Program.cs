using System.Diagnostics;
using System.IO;
using M2Lib.m2;

namespace TestProject
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            /*
            Console.WriteLine("Koward M2 converter prealpha");
            Console.WriteLine("YOU ARE ABOUT TO HACK TIME. ARE YOU SURE? >YES");
            if (args.Length < 1)
            {
                Console.WriteLine("No model was specified.");
                return;
            }
            var model = new M2();
            using (var reader = new BinaryReader(new FileStream(args[0], FileMode.Open)))
                model.Load(reader);
            Debug.WriteLine(model.Name + " loaded.");
            using (var writer = new BinaryWriter(new FileStream(args[0], FileMode.Create)))
                model.Save(writer, M2.Format.Classic);
            Debug.WriteLine(model.Name + " converted.");
            */
            Debug.WriteLine("Debug application for M2Lib");
            const string fileName = "draenor/world/arttest/boxtest/xyz.m2";
            //const string fileName = "draenor/world/generic/activedoodads/chest01/chest01.m2";
            //const string fileName = "lichking/world/arttest/boxtest/xyz.m2";
            //const string fileName = "Draenor/Character/Human/Male/HumanMale_HD.m2";
            //const string fileName = "Draenor/Creature/Frog/Frog.m2";
            //const string fileName = "Legion/Character/Naga_/Male/Naga_Male.m2";
            //const string fileName2 = "Output/Naga_Male.m2";
            //const string fileName2 = "Output/Frog.m2";
            //const string fileName = "BurningCrusade/FrogGenuine.m2";
            //const string fileName = "Legion/Creature/Bear2/Bear2.m2";
            //const string fileName2 = "Output/Bear2CSharp.m2";
            //const string fileName = "Classic/BogBeast.m2";
            //const string fileName = "Legion/Interface/Glues/Models/ui_orc/ui_orc.m2";
            //const string fileName2 = "Output/ui_orc.m2";
            var model = new M2();
            using (var reader = new BinaryReader(new FileStream(fileName, FileMode.Open)))
                model.Load(reader);
            Debug.WriteLine(model.Name + " loaded");
            Debug.WriteLine(model.BoneLookup);
            Debug.WriteLine(model.Views);
            /*
            foreach (var view in model.Views)
            {
                var goodSubmeshes = new List<M2SkinSection>();
                foreach (var geoset in view.Submeshes)
                {
                    if(geoset.SubmeshId == 0 || geoset.SubmeshId == 401) goodSubmeshes.Add(geoset);
                }
                view.Submeshes.Clear();
                view.Submeshes.AddRange(goodSubmeshes);
            }
            */
            /*
            using (var writer = new BinaryWriter(new FileStream(fileName2, FileMode.Create)))
                model.Save(writer, M2.Format.Classic);
            Debug.WriteLine(model.Name + " written.");

            /*
            Debug.WriteLine("Submeshes number : " + model.Views[0].Submeshes.Count);
            Debug.WriteLine("BoneInfluences : " + model.Views[0].Submeshes[0].BoneInfluences);
            //KOWARD method
            var max = 0;
            const int submeshNumber = 1;
            for (var i = model.Views[0].Submeshes[submeshNumber].StartVertex;
                i < model.Views[0].Submeshes[submeshNumber].StartVertex + model.Views[0].Submeshes[submeshNumber].NVertices;
                i++)
            {
                var vertex = model.GlobalVertexList[model.Views[0].Indices[i]];
                var localMax = 0;
                for(var j = 0; j < vertex.BoneWeights.Length; j++)
                {
                    var weight = vertex.BoneWeights[j];
                    Debug.Write(" "+weight+" ");
                    var relatedBone = model.Bones[model.BoneLookup[vertex.BoneIndices[j]]];
                    if (weight > 0 && relatedBone.Flags.HasFlag(M2Bone.BoneFlags.Transformed)) localMax++;
                }
                if (localMax > max) max = localMax;
                Debug.WriteLine("");
            }
            Debug.WriteLine("\tComputed number : " + max);
            /*
            //DEAMON method
            Debug.WriteLine("Method 2 (Deamon)");
                var boneSet = new HashSet<M2Bone>();
                for (var i = model.Views[0].Submeshes[0].StartVertex;
                    i < model.Views[0].Submeshes[0].StartVertex + model.Views[0].Submeshes[0].NVertices;
                    i++)
                {
                    var vertex = model.GlobalVertexList[model.Views[0].Indices[i]];
                    foreach (var index in vertex.BoneIndices)
                    {
                        Debug.Assert(model.Bones.Count > index);
                        if ((!boneSet.Contains(model.Bones[index])) && (model.Bones[index].KeyBoneId != (M2Bone.KeyBone) (-1))) boneSet.Add(model.Bones[index]);
                    }
                }
                Debug.WriteLine("\tNumber : " + boneSet.Count);
            /*
            /*
            BinaryReader stream = null;
            Func<BinaryReader, C3Vector> testFunc = i => i.ReadC3Vector();
            var result = testFunc(stream);
            */
            /*
            using (var writer = new StreamWriter("Test.iqe"))
            {
                writer.WriteLine("# Inter-Quake Export");
                writer.WriteLine("");
                foreach (var bone in model.Bones)
                {
                    if(bone.KeyBoneId != M2Bone.KeyBone.Other) writer.WriteLine("joint \""+ bone.KeyBoneId + "\" " + bone.ParentBone);
                    else writer.WriteLine("joint \"\" " + bone.ParentBone);
                }
                writer.WriteLine("");
                foreach (var bone in model.Bones)
                {
                    writer.WriteLine("pq "+bone.Pivot.X+" "+bone.Pivot.Y+" "+bone.Pivot.Z);
                }
                writer.WriteLine("");
                writer.WriteLine("vertexarray position float 3");
                writer.WriteLine("vertexarray texcoord float 2");
                writer.WriteLine("vertexarray normal float 3");
                //writer.WriteLine("vertexarray tangent float 4");//TODO Tangents ?
                writer.WriteLine("vertexarray normal float 3");
                writer.WriteLine("");
                writer.WriteLine("mesh \""+"Test"+"\"");
                writer.WriteLine("material \""+"Texture.tga"+"\"");
                writer.WriteLine("");
                writer.WriteLine("");
                writer.WriteLine("");
            }
            */
        }
    }
}