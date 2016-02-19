using System;
using System.Collections.Generic;

namespace jm2lib.blizzard.io
{


	/// <summary>
	/// Handles the maps between classes and magic identifiers.
	/// @author Dr Super Good
	/// 
	/// </summary>
	public class ObjectTypeResolver
	{
		/// <summary>
		/// The resolver singleton.
		/// </summary>
		public static readonly ObjectTypeResolver resolver = new ObjectTypeResolver();

		private readonly ServiceLoader<ObjectTypeProvider> providers;
		/// <summary>
		/// Allows one to look up a class name from the magic number.
		/// </summary>
		private readonly IDictionary<FileMagic, string> magicToClass;
		/// <summary>
		/// Allows one to look up a magic number for a class name.
		/// </summary>
		private readonly IDictionary<string, FileMagic> classToMagic;

		private ObjectTypeResolver()
		{
			providers = ServiceLoader.load(typeof(ObjectTypeProvider));
			magicToClass = new Dictionary<FileMagic, string>();
			classToMagic = new Dictionary<string, FileMagic>();
			load();
		}

		private void load()
		{
//JAVA TO C# CONVERTER TODO TASK: Java lambdas satisfy functional interfaces, while .NET lambdas satisfy delegates - change the appropriate interface to a delegate:
			providers.forEach(A => A.Mappings.forEach(B => {magicToClass.put(B.magic, B.classname); classToMagic.put(B.classname, B.magic);}));
		}

		/// <summary>
		/// Takes a file magic number and resolves its corresponding object class.
		/// </summary>
		/// <param name="magic">
		///            - the magic number of the file. </param>
		/// <returns> - the resolved object class. </returns>
		/// <exception cref="ClassNotFoundException">
		///             - if a class cannot be resolved using the magic number. </exception>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public Class resolveClass(FileMagic magic) throws ClassNotFoundException
		public virtual Type resolveClass(FileMagic magic)
		{
			string classname = magicToClass[magic];
			if (classname == null)
			{
//JAVA TO C# CONVERTER TODO TASK: The following line has a Java format specifier which cannot be directly translated to .NET:
//ORIGINAL LINE: throw new ClassNotFoundException(String.format("object class for %S is not known or not a Blizzard object", magic.toString()));
				throw new ClassNotFoundException(string.Format("object class for %S is not known or not a Blizzard object", magic.ToString()));
			}
			return Type.GetType(classname);
		}

		/// <summary>
		/// Takes an object class and resolves its corresponding file magic number.
		/// </summary>
		/// <param name="clazz">
		///            - the object class. </param>
		/// <returns> - the resolved file magic number. </returns>
		/// <exception cref="NotSerializableException">
		///             - if a file magic number cannot be resolved for the class. </exception>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public FileMagic resolveMagic(Class clazz) throws java.io.NotSerializableException
		public virtual FileMagic resolveMagic(Type clazz)
		{
//JAVA TO C# CONVERTER WARNING: The .NET Type.FullName property will not always yield results identical to the Java Class.getName method:
			string classname = clazz.FullName;
			FileMagic magic = classToMagic[classname];
			if (magic == null)
			{
//JAVA TO C# CONVERTER WARNING: The .NET Type.FullName property will not always yield results identical to the Java Class.getName method:
				throw new NotSerializableException(string.Format("file magic identifier for {0} was not found", clazz.FullName));
			}
			;
			return magic;
		}

		/// <summary>
		/// Forces all marshal providers to be reloaded. The results of all providers
		/// are cached together for efficiency.
		/// </summary>
		public virtual void reload()
		{
			providers.reload();
			magicToClass.Clear();
			classToMagic.Clear();
		}
	}

}