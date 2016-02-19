using System.Collections.Generic;

namespace jm2lib.blizzard.io
{


	/// <summary>
	/// Provides a collection of Blizzard Marshalable object classes with their 4
	/// character file magic number.
	/// <para>
	/// Classes are represented by a fully qualified class name string rather than a
	/// class object to allow for lazy loading.
	/// 
	/// @author Dr Super Good
	/// 
	/// </para>
	/// </summary>
	public abstract class ObjectTypeProvider
	{
		private readonly ICollection<MagicClassMapping> sources;

		protected internal ObjectTypeProvider()
		{
			sources = new LinkedList<MagicClassMapping>();
		}

		/// <summary>
		/// Adds a new object source with the appropriate magic identifier.
		/// </summary>
		/// <param name="magic">
		///            - four character magic file identifier </param>
		/// <param name="classname">
		///            - the path of the source class </param>
		protected internal virtual void addMapping(FileMagic magic, string classname)
		{
			sources.Add(new MagicClassMapping(magic, classname));
		}

		/// <summary>
		/// Return the mappings provided by this class.
		/// </summary>
		/// <returns> - A collection of class mappings. </returns>
		public virtual ICollection<MagicClassMapping> Mappings
		{
			get
			{
				return sources;
			}
		}

		/// <summary>
		/// Represents a mapping between a FileMagic and a class full qualified name.
		/// 
		/// </summary>
		public class MagicClassMapping
		{
			public readonly FileMagic magic;
			public readonly string classname;

			internal MagicClassMapping(FileMagic magic, string classname)
			{
				this.magic = magic;
				this.classname = classname;
			}
		}
	}

}