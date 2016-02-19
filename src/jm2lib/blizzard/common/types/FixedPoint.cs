using System.Collections;
using System.Text;

namespace jm2lib.blizzard.common.types
{


	using Marshalable = jm2lib.io.Marshalable;
	using MarshalingStream = jm2lib.io.MarshalingStream;
	using UnmarshalingStream = jm2lib.io.UnmarshalingStream;

	/// <summary>
	/// Fixed point number. Works internally with a {@code BitSet}.
	/// 
	/// @author Koward
	/// 
	/// </summary>
	public class FixedPoint : Marshalable
	{
		private int integerBits;
		private int decimalBits;
		private int signBits = 1;
		private BitArray bits;

		public FixedPoint(int integerBits, int decimalBits)
		{
			this.integerBits = integerBits;
			this.decimalBits = decimalBits;
			bits = new BitArray();
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void unmarshal(jm2lib.io.UnmarshalingStream in) throws java.io.IOException, ClassNotFoundException
		public virtual void unmarshal(UnmarshalingStream @in)
		{
			sbyte[] array = new sbyte[(signBits + integerBits + decimalBits) / 8];
			@in.read(array);
			bits = BitArray.valueOf(array);
		}

		public virtual BitArray Bits
		{
			get
			{
				return this.bits;
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public void marshal(jm2lib.io.MarshalingStream out) throws java.io.IOException
		public virtual void marshal(MarshalingStream @out)
		{
			sbyte[] array = new sbyte[(signBits + integerBits + decimalBits) / 8];
			for (int i = 0; i < bits.length(); i++)
			{
				if (bits.Get(i))
				{
					array[array.Length - i / 8 - 1] |= (sbyte)(1 << (i % 8));
				}
			}
			@out.write(array);
		}

		/// <summary>
		/// @author schlumpf
		/// @return
		/// </summary>
		public virtual float toFloat()
		{
			bool sign = bits.Get(signBits - 1);
			int integer = bitSetToInt(bits.get(signBits, signBits + integerBits - 1));
			int @decimal = bitSetToInt(bits.get(signBits + integerBits, signBits + integerBits + decimalBits - 1));
			return (sign ? - 1.0f : 1.0f) * (integer + @decimal / (float)(1 << decimalBits));
		}

		/*
		 * (non-Javadoc)
		 * 
		 * @see java.lang.Object#toString()
		 */
		public override string ToString()
		{
			StringBuilder builder = new StringBuilder();
			string sign = (bits.Get(signBits - 1)) ? "-" : "+";
			int integer = bitSetToInt(bits.get(signBits, signBits + integerBits - 1));
			int @decimal = bitSetToInt(bits.get(signBits + integerBits, signBits + integerBits + decimalBits - 1));
			builder.Append("FixedPoint [sign=").Append(sign).Append(", integer=").Append(integer).Append(", decimal=").Append(@decimal).Append("]");
			return builder.ToString();
		}

		private int bitSetToInt(BitArray bits)
		{
			int result = 0;
			for (int i = 0; i < bits.length(); i++)
			{
				if (bits.Get(i))
				{
					result |= (1 << i);
				}
			}
			result &= int.MaxValue;
			return result;
		}
	}

}