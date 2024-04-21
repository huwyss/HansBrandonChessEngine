using HansBrandonBitboardEngine;
using HBCommon;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HansBrandonBitboardEngineTest
{
    public class AssertHelperBitboard
    {
        public static void WhiteWins(IMoveRating<BitMove> rating)
        {
            Assert.IsTrue(rating.Score > 9900);
            Assert.IsTrue(rating.WhiteWins);
            Assert.IsFalse(rating.BlackWins);
            Assert.IsFalse(rating.Stallmate);
        }

        public static void BlackWins(IMoveRating<BitMove> rating)
        {
            Assert.IsTrue(rating.Score < -9900);
            Assert.IsFalse(rating.WhiteWins);
            Assert.IsTrue(rating.BlackWins);
            Assert.IsFalse(rating.Stallmate);
        }

        public static void StallMate(IMoveRating<BitMove> rating)
        {
            Assert.IsTrue(rating.Score == 0);
            Assert.IsFalse(rating.WhiteWins);
            Assert.IsFalse(rating.BlackWins);
            Assert.IsTrue(rating.Stallmate);
        }
    }
}
