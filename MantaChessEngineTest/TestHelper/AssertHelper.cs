using MantaChessEngine;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MantaChessEngineTest
{
    class AssertHelper
    {
        public static void WhiteWins(MoveRating rating)
        {
            Assert.IsTrue(rating.Score > 9900);
            Assert.IsTrue(rating.WhiteWins);
            Assert.IsFalse(rating.BlackWins);
            Assert.IsFalse(rating.Stallmate);
        }

        public static void BlackWins(MoveRating rating)
        {
            Assert.IsTrue(rating.Score < -9900);
            Assert.IsFalse(rating.WhiteWins);
            Assert.IsTrue(rating.BlackWins);
            Assert.IsFalse(rating.Stallmate);
        }

        public static void StallMate(MoveRating rating)
        {
            Assert.IsTrue(rating.Score == 0);
            Assert.IsFalse(rating.WhiteWins);
            Assert.IsFalse(rating.BlackWins);
            Assert.IsTrue(rating.Stallmate);
        }
    }
}
