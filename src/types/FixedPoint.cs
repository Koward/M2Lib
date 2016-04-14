using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Text;
using m2lib_csharp.interfaces;
using m2lib_csharp.m2;

namespace m2lib_csharp.types
{
    /// <summary>
    ///     Fixed point number. Little endian.
    ///     Exponents : -9 -8 -7 -6 -5 -4 -3 -2 -1 | 0  1  2  3  4  5
    ///     Indices   :  0  1  2  3  4  5  6  7  8 | 9  10 11 12 13 14
    /// </summary>
    public class FixedPoint : IMarshalable
    {
        private readonly int _decimalBits;
        private readonly int _integerBits;
        // ReSharper disable once ConvertToConstant.Local
        private readonly int _signBits = 1;

        public FixedPoint(int integerBits, int decimalBits)
        {
            _decimalBits = decimalBits;
            _integerBits = integerBits;
            Bits = new BitArray(_decimalBits + _integerBits + _signBits);
        }

        public BitArray Bits { get; private set; }

        public float Value
        {
            get
            {
                var decimalPart = Bits.GetRange(0, _decimalBits).ToInt();
                var integerPart = Bits.GetRange(_decimalBits, _decimalBits + _integerBits).ToInt();
                var sign = Bits[_decimalBits + _integerBits + _signBits - 1];
                return (sign ? -1.0f : 1.0f)*(integerPart + decimalPart/(float) (1 << _decimalBits));
            }
        }

        public void Load(BinaryReader stream, M2.Format version = M2.Format.Useless)
        {
            Bits = new BitArray(stream.ReadBytes((_decimalBits + _integerBits + _signBits)/8));
        }

        public void Save(BinaryWriter stream, M2.Format version = M2.Format.Useless)
        {
            var content = new byte[(_decimalBits + _integerBits + _signBits)/8];
            Bits.CopyTo(content, 0);
            stream.Write(content);
        }

        public override string ToString()
        {
            return Value.ToString(CultureInfo.CurrentCulture);
        }
    }

    public static class BitArrayExtensions
    {
        public static int ToInt(this BitArray bits)
        {
            var result = 0;
            for (var i = 0; i < bits.Count; i++)
            {
                if (bits[i])
                {
                    result |= 1 << i;
                }
            }
            result &= int.MaxValue;
            return result;
        }

        /// <summary>
        ///     Returns a new BitArray composed of bits from this BitArray from fromIndex (inclusive) to toIndex (exclusive).
        /// </summary>
        /// <param name="bits">this</param>
        /// <param name="fromIndex">index of the first bit to include</param>
        /// <param name="toIndex">index after the last bit to include</param>
        /// <returns>a new BitArray from a range of this BitArray</returns>
        public static BitArray GetRange(this BitArray bits, int fromIndex, int toIndex)
        {
            CheckRange(bits, fromIndex, toIndex);
            var len = bits.Count;
            // If no set bits in range return empty bitarray
            if (len <= fromIndex || fromIndex == toIndex)
                return new BitArray(0);
            // An optimization
            if (toIndex > len)
                toIndex = len;
            var result = new BitArray(toIndex - fromIndex);
            var j = 0;
            for (var i = fromIndex; i < toIndex; i++)
            {
                result[j] = bits[i];
                j++;
            }
            return result;
        }

        /// <summary>
        ///     Checks that fromIndex ... toIndex is a valid range of bit indices.
        /// </summary>
        /// <param name="bits">this</param>
        /// <param name="fromIndex">index of the first bit to include</param>
        /// <param name="toIndex">index after the last bit to include</param>
        public static void CheckRange(this BitArray bits, int fromIndex, int toIndex)
        {
            if (fromIndex < 0)
                throw new IndexOutOfRangeException("fromIndex < 0: " + fromIndex);
            if (toIndex < 0)
                throw new IndexOutOfRangeException("toIndex < 0: " + toIndex);
            if (fromIndex > toIndex)
                throw new IndexOutOfRangeException("fromIndex: " + fromIndex +
                                                   " > toIndex: " + toIndex);
        }

        public static string ToBitString(this BitArray bits)
        {
            var sb = new StringBuilder();

            for (var i = 0; i < bits.Count; i++)
            {
                var c = bits[i] ? '1' : '0';
                sb.Append(c);
            }

            return sb.ToString();
        }
    }

    public class Fixed16 : FixedPoint
    {
        public Fixed16() : base(0, 15)
        {
        }
    }
}