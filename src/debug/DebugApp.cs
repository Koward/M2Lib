using System;

namespace debug
{

	using BlizzardInputStream = jm2lib.blizzard.io.BlizzardInputStream;

	/// <summary>
	/// Reads and write a BlizzardFile.
	/// 
	/// @author Koward
	/// 
	/// </summary>
	public class DebugApp
	{
		/// <summary>
		/// Test application for debugging.
		/// </summary>
		/// <param name="args"> </param>
		/// <exception cref="Exception"> </exception>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public static void main(String[] args) throws Exception
		public static void Main(string[] args)
		{
			Console.WriteLine("Debug application");
			/*
			BlizzardInputStream in = new BlizzardInputStream("./models/EarthenFury/earthenfuryCL.mdx");
			//BlizzardInputStream in = new BlizzardInputStream("./models/testModel.mdx");
			//BlizzardInputStream in = new BlizzardInputStream("/home/julien/Windows/MDLVis/Thrall, Son of Durotan/ThrallSonofDurotan.mdx");
			MDX wc3Model = (MDX) in.readObject();
			System.out.println(wc3Model.collisions);
			//System.out.println(wc3Model.bones.size());
			in.close();
	
			/*
			in = new BlizzardInputStream("./models/EarthenFury/earthenfuryCL.m2");
			Model wowModel = (Model) ((M2) in.readObject()).getModel();
			in.close();
			System.out.println(wowModel.submeshAnimations);
			*/

			/*
			BlizzardInputStream in = new BlizzardInputStream("./models/Human/Male/humanmale_HD.m2");
			M2 model = (M2) in.readObject();
			in.close();
			for(Bone bone : ((jm2lib.blizzard.wow.lichking.Model) model.getModel()).bones) {
				bone.flags |= 0x20;
			}
			BlizzardOutputStream out = new BlizzardOutputStream("./models/humanmale.m2");
			out.writeObject(model.convert(M2Format.CLASSIC));
			out.close();
			/*
			BlizzardInputStream in = new BlizzardInputStream("./models/Human/Male/humanmale_HD.m2");
			//M2 model = ((MD21) in.readObject()).getM2();
			M2 model = ((M2) in.readObject());
			in.close();
			System.out.println("Model read.");
	
			model.convert(M2Format.CLASSIC);
			System.out.println("Model converted.");
	
			BlizzardOutputStream out = new BlizzardOutputStream("./models/Human/Male/humanmale_HDCL.m2");
			out.writeObject(model);
			out.close();
			System.out.println("Model written.");
			/**/
			//readAndPrint("./models/Human/Male/HumanMaleGenuine.m2");
			//readAndPrint("./models/Skeleton/Skeleton.m3");
			//readAndPrint("./models/testModel.mdx");
			//readAndPrint("./models/Human/Male/humanmale_HDCL.m2");
			//readAndPrint("./models/UthilBears/DruidBear/DruidbearTauren.m2");
			readAndPrint("/home/julien/Legion/Creature/Imp2/Imp2CL.m2");
			//readAndPrint("./models/FandralFireScorpion/FandralFireScorpion.m2");
			//readAndPrint("/home/julien/Projets/jM2lib/models/TaurenHD/taurenmale_hd.m2");
			/*
			List<Integer> list = Arrays.asList(1, 2, 5, 10, 15);
			List<Integer> result = list.stream().filter(s -> s >= 5).collect(Collectors.toList());
			System.out.println(result);
			*/
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: private static void readAndPrint(String path) throws ClassNotFoundException, java.io.IOException
		private static void readAndPrint(string path)
		{
			BlizzardInputStream @in = new BlizzardInputStream(path);
			object obj = @in.readObject();
			Console.WriteLine("Model read. Printing...");
			Console.WriteLine(obj);
			@in.close();
		}

	}

}