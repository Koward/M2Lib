using System.Diagnostics.CodeAnalysis;
using M2Lib.types;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    [TestClass]
    public class FixedPointTests
    {
        [TestMethod]
        [SuppressMessage("ReSharper", "InconsistentNaming")]
        public void Conversions()
        {
            var fp0_15 = new FixedPoint_0_15(42);
            var fp6_9 = new FixedPoint_6_9(42);
            var fp2_5 = new FixedPoint_2_5(42);
            Assert.AreEqual(42, fp0_15.ToShort());
            Assert.AreEqual(42, fp6_9.ToShort());
            Assert.AreEqual(42, fp2_5.ToByte());
        }
        /*
        [TestMethod]
        public void NoIntegerPart()
        {
            var point = new FixedPoint(0, 15)
            {
                _bits =
                {
                    [14] = true,
                    [15] = true
                }
            };
            Assert.AreEqual(-0.5, point.Value);
        }
        */
    }
}
