namespace jm2lib.blizzard.sc2
{

	using Marshalable = jm2lib.io.Marshalable;

	/// <summary>
	/// Many Starcraft 2 M3 structures are parsed depending on {@code IndexEntry#version}
	/// Maybe I should extend this system to other formats ?
	/// 
	/// @author Koward
	/// 
	/// </summary>
	public abstract class Versioned : Marshalable
	{
		public abstract void marshal(jm2lib.io.MarshalingStream @out);
		public abstract void unmarshal(jm2lib.io.UnmarshalingStream @in);
		protected internal int version = 0;
		public virtual int Version
		{
			set
			{
				version = value;
			}
		}
	}

}