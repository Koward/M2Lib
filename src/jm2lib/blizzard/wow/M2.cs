using System;
using System.Collections.Generic;

namespace jm2lib.blizzard.wow
{


	using BlizzardFile = jm2lib.blizzard.io.BlizzardFile;
	using MarshalingStream = jm2lib.io.MarshalingStream;
	using UnmarshalingStream = jm2lib.io.UnmarshalingStream;

	/// <summary>
	/// Represents a World of Warcraft M2 model. 
	/// This class handles the version detection.
	/// @author Koward
	/// 
	/// </summary>
	public class M2 : BlizzardFile
	{
		private M2Format model;
		private IDictionary<int?, string> formats;

		/// <summary>
		/// Suffix of supported files.
		/// </summary>
		public const string FILE_SUFFIX = ".m2";

		/// <summary>
		/// Initializes the mapping between versions.
		/// </summary>
		public M2()
		{
			formats = new Dictionary<int?, string>();
			formats[M2Format.CLASSIC] = "jm2lib.blizzard.wow.classic.Model";
			formats[M2Format.BURNING_CRUSADE] = "jm2lib.blizzard.wow.burningcrusade.Model";
			formats[M2Format.BURNING_CRUSADE + 1] = "jm2lib.blizzard.wow.burningcrusade.Model";
			formats[M2Format.BURNING_CRUSADE + 2] = "jm2lib.blizzard.wow.burningcrusade.Model";
			formats[M2Format.BURNING_CRUSADE + 3] = "jm2lib.blizzard.wow.lateburningcrusade.Model";
			formats[M2Format.LICH_KING] = "jm2lib.blizzard.wow.lichking.Model";
			formats[M2Format.CATACLYSM] = "jm2lib.blizzard.wow.cataclysm.Model";
			formats[M2Format.PANDARIA] = "jm2lib.blizzard.wow.cataclysm.Model";
			formats[M2Format.DRAENOR] = "jm2lib.blizzard.wow.cataclysm.Model";
			formats[M2Format.LEGION] = "jm2lib.blizzard.wow.legion.Model";
			try
			{
				object obj;
				obj = Type.GetType(formats[M2Format.LEGION]).newInstance();
				model = (M2Format) obj;
			}
//JAVA TO C# CONVERTER TODO TASK: There is no equivalent in C# to Java 'multi-catch' syntax:
			catch (InstantiationException | IllegalAccessException | ClassNotFoundException e)
			{
				e.printStackTrace();
			}
		}

		public virtual M2Format Model
		{
			get
			{
				return model;
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void unmarshal(jm2lib.io.UnmarshalingStream in) throws java.io.IOException, ClassNotFoundException
		public virtual void unmarshal(UnmarshalingStream @in)
		{
			int version = @in.readInt();
			object obj;
			try
			{
				obj = Type.GetType(formats[version]).newInstance();
				if (!(obj is M2Format))
				{
					throw new InvalidClassException("not an M2 format");
				}
				model = (M2Format) obj;
			}
//JAVA TO C# CONVERTER TODO TASK: There is no equivalent in C# to Java 'multi-catch' syntax:
			catch (InstantiationException | IllegalAccessException | ClassNotFoundException e)
			{
				Console.Error.WriteLine("Error : Could not find a matching M2 format for your version. Trying to force load.");
				e.printStackTrace();
				model = new jm2lib.blizzard.wow.cataclysm.Model();
			}
			model.version = version;
			model.unmarshal(@in);
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void marshal(jm2lib.io.MarshalingStream out) throws java.io.IOException
		public virtual void marshal(MarshalingStream @out)
		{
			@out.writeInt(model.version);
			model.marshal(@out);
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public M2 convert(int newVersion) throws Exception
		public virtual M2 convert(int newVersion)
		{
			while (formats[model.version] != formats[newVersion])
			{
				if (model.version < newVersion)
				{
					model = model.upConvert();
				}
				else if (model.version > newVersion)
				{
					model = model.downConvert();
				}
				else if (model.version == newVersion)
				{
					throw new Exception("Error : Equal versions but different class.");
				}
				if (model == null)
				{
					throw new System.NotSupportedException("Not implemented.");
				}
			}
			model.version = newVersion;
			return this;
		}

		public override string ToString()
		{
			return model.ToString();
		}

		public virtual int Version
		{
			get
			{
				return model.version;
			}
		}
	}

}