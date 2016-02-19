using System;

namespace jm2lib.lang
{

	/// 
	/// <summary>
	/// Class to represent a 32 bit magic number.
	/// <para>
	/// Such numbers are often produced by combining four human readable 8 bit ASCII
	/// characters. It is often desired to treat them as a {@code String} when
	/// interacting with users to preserve the literal meaning while processing them
	/// as {@code int} internally.
	/// </para>
	/// <para>
	/// Due to the variety of magic number usages this class should not be used directly. A
	/// delegating constructor only child class should be used to define a specific
	/// type of magic number. This adds type safety to the magic numbers preventing
	/// them from accidently being used for the wrong purpose.
	/// </para>
	/// <para>
	/// The {@code int} value of the magic number is constructed from the bytes of
	/// the string in little-endian order.
	/// 
	/// @author Dr Super Good
	/// </para>
	/// </summary>
	public abstract class MagicInt : IComparable<MagicInt>
	{
		private readonly int value;

		/// <summary>
		/// Constructs a magic number from an internal {@code int} value.
		/// </summary>
		/// <param name="value">
		///            - the identity number </param>
		protected internal MagicInt(int value)
		{
			this.value = value;
		}

		/// <summary>
		/// Constructs a magic number from a human readable magic string.
		/// <para>
		/// A magic string must be exactly 4 characters long.
		/// 
		/// </para>
		/// </summary>
		/// <param name="value">
		///            - the type string </param>
		/// <exception cref="StringIndexOutOfBoundsException">
		///             - if the type string is not exactly 4 characters long </exception>
		protected internal MagicInt(string value)
		{
			this.value = stringToMagic(value);
		}

		/// <summary>
		/// Converts a human readable magic number into an internal magic number.
		/// <para>
		/// A type string must be exactly 4 characters long.
		/// 
		/// </para>
		/// </summary>
		/// <param name="value">
		///            - the human readable magic number </param>
		/// <returns> - the internal magic number </returns>
		/// <exception cref="StringIndexOutOfBoundsException">
		///             - if the type string is not exactly 4 characters long </exception>
		public static int stringToMagic(string value)
		{
			if (value.Length != 4)
			{
				throw new StringIndexOutOfBoundsException(string.Format("'{0}' is not a valid type string (must be exactly 4 characters long)", value));
			}
			return (value[0] & 0xFF) | ((value[1] & 0xFF) << 8) | ((value[2] & 0xFF) << 16) | ((value[3] & 0xFF) << 24);
		}

		/// <summary>
		/// Converts an internal magic number into a human readable magic number.
		/// </summary>
		/// <param name="value">
		///            - the internal magic number </param>
		/// <returns> - the human readable magic number </returns>
		public static string magicToString(int value)
		{
			char[] chars = new char[] {(char)(value & 0xFF), (char)(value >> 8 & 0xFF), (char)(value >> 16 & 0xFF), (char)(value >> 24 & 0xFF)};
			return new string(chars);
		}

		/// <seealso cref= Object#hashCode() </seealso>
		public override int GetHashCode()
		{
			return value;
		}

		/// <seealso cref= Object#equals() </seealso>
		public override bool Equals(object obj)
		{
			if (this == obj)
			{
				return true;
			}
			if (obj == null)
			{
				return false;
			}
			if (this.GetType() != obj.GetType())
			{
				return false;
			}
			MagicInt other = (MagicInt) obj;
			if (value != other.value)
			{
				return false;
			}
			return true;
		}

		/// <summary>
		/// Special equal method for comparing with primitive {@code int} type.
		/// </summary>
		/// <param name="value"> - the internal magic number
		/// @return </param>
		public virtual bool Equals(int value)
		{
			return this.value == value;
		}

		/// <summary>
		/// Gets the magic number as an {@code String} value.
		/// </summary>
		/// <returns> - the human readable magic number </returns>
		public override string ToString()
		{
			return magicToString(value);
		}

		/// <summary>
		/// Gets the magic number as an {@code int} value.
		/// </summary>
		/// <returns> - the internal magic number </returns>
		public virtual int toInt()
		{
			return value;
		}

		public virtual int CompareTo(MagicInt o)
		{
		/// <seealso cref= java.lang.Comparable#compareTo(java.lang.Object) </seealso>
			return value - o.value;
		}
	}

}