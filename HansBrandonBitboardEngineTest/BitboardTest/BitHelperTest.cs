using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HansBrandonBitboardEngine;
using HBCommon;

namespace HansBrandonBitboardEngineTest
{
    [TestClass]
    public class BitHelperTest
    {
        [TestMethod]
        public void BitScanForwardTest_FindingTheLSB()
        {
            var result = BitHelper.BitScanForward(0x000001);
            Assert.AreEqual(0, result);

            result = BitHelper.BitScanForward(0x00000100);
            Assert.AreEqual(8, result);

            result = BitHelper.BitScanForward(0x20000000000);
            Assert.AreEqual(41, result);

            result = BitHelper.BitScanForward(0x800000000000000);
            Assert.AreEqual(59, result);

            result = BitHelper.BitScanForward(0x0018); // binary: 11000
            Assert.AreEqual(3, result); // 3 zeros at the end
        }
    }
}
