using static MantaChessEngine.Definitions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MantaChessEngine;
using MantaCommon;

namespace MantaChessEngineTest
{
    [TestClass]
    public class MoveRatingTest
    {
        private int _smallDifference = 1;

        //// White, Test for IsBetter

        [TestMethod]
        public void RatingWithHigherScoreIsBetterForWhiteTest()
        {
            var badRatingForWhite = new MoveRating() { Score = -10 };
            var goodRatingForWhite = new MoveRating() { Score = 10 };

            var isBetterActual = badRatingForWhite.IsBetter(ChessColor.White, goodRatingForWhite);

            Assert.IsFalse(isBetterActual, "white should pick move with higher score.");
        }

        [TestMethod]
        public void RatingWithHigherScoreIsBetterForWhiteTest_mirror()
        {
            var badRatingForWhite = new MoveRating() { Score = -10 };
            var goodRatingForWhite = new MoveRating() { Score = 10 };

            var isBetterActual = goodRatingForWhite.IsBetter(ChessColor.White, badRatingForWhite);

            Assert.IsTrue(isBetterActual, "white should pick move with higher score.");
        }

        [TestMethod]
        public void RatingWithEarlierCheckmateIsBetterForWhiteTest()
        {
            var checkmateIn1 = new MoveRating() { Score = ScoreWhiteWins - 10 }; // checkmate in 1 move
            var checkmateIn3 = new MoveRating() { Score = ScoreWhiteWins - 30 }; // checkmate in 3 moves

            var isBetterActual = checkmateIn1.IsBetter(ChessColor.White, checkmateIn3);

            Assert.IsTrue(isBetterActual, "white should pick move with earlier checkmate.");
        }

        [TestMethod]
        public void RatingWithEarlierCheckmateIsBetterForWhiteTest_mirror()
        {
            var checkmateIn1 = new MoveRating() { Score = ScoreWhiteWins - 10 }; // checkmate in 1 move
            var checkmateIn3 = new MoveRating() { Score = ScoreWhiteWins - 30 }; // checkmate in 3 moves

            var isBetterActual = checkmateIn3.IsBetter(ChessColor.White, checkmateIn1);

            Assert.IsFalse(isBetterActual, "white should pick move with earlier checkmate.");
        }

        [TestMethod]
        public void RatingWithLaterLoosingIsBetterForWhiteTest()
        {
            var checkmateIn1 = new MoveRating() { Score = ScoreBlackWins + 10 }; // checkmate in 1 move
            var checkmateIn3 = new MoveRating() { Score = ScoreBlackWins + 30 }; // checkmate in 3 moves

            var isBetterActual = checkmateIn1.IsBetter(ChessColor.White, checkmateIn3);

            Assert.IsFalse(isBetterActual, "white should pick move with loosing later.");
        }

        [TestMethod]
        public void RatingWithLaterLoosingIsBetterForWhiteTest_mirror()
        {
            var checkmateIn1 = new MoveRating() { Score = ScoreBlackWins + 10 }; // checkmate in 1 move
            var checkmateIn3 = new MoveRating() { Score = ScoreBlackWins + 30 }; // checkmate in 3 moves

            var isBetterActual = checkmateIn3.IsBetter(ChessColor.White, checkmateIn1);

            Assert.IsTrue(isBetterActual, "white should pick move with loosing later.");
        }

        /// Black, Test for IsBetter

        [TestMethod]
        public void RatingWithLowerScoreIsBetterForBlaclTest()
        {
            var badRatingForBlack = new MoveRating() { Score = 100 };
            var goodRatingForBlack = new MoveRating() { Score = -100 };

            var isBetterActual = badRatingForBlack.IsBetter(ChessColor.Black, goodRatingForBlack);

            Assert.IsFalse(isBetterActual, "Black should pick move with lower score.");
        }

        [TestMethod]
        public void RatingWithLowerScoreIsBetterForBlaclTest_mirror()
        {
            var badRatingForBlack = new MoveRating() { Score = 100 };
            var goodRatingForBlack = new MoveRating() { Score = -10 };

            var isBetterActual = goodRatingForBlack.IsBetter(ChessColor.Black, badRatingForBlack);

            Assert.IsTrue(isBetterActual, "Black should pick move with lower score.");
        }

        [TestMethod]
        public void RatingWithEarlierCheckmateIsBetterForBlackTest()
        {
            var checkmateIn1 = new MoveRating() { Score = ScoreBlackWins + 10 }; // checkmate in 1 move
            var checkmateIn3 = new MoveRating() { Score = ScoreBlackWins + 30 }; // checkmate in 3 moves

            var isBetterActual = checkmateIn1.IsBetter(ChessColor.Black, checkmateIn3);

            Assert.IsTrue(isBetterActual, "Black should pick move with earlier checkmate.");
        }

        [TestMethod]
        public void RatingWithEarlierCheckmateIsBetterForBlackTest_mirror()
        {
            var checkmateIn1 = new MoveRating() { Score = ScoreBlackWins + 10 }; // checkmate in 1 move
            var checkmateIn3 = new MoveRating() { Score = ScoreBlackWins + 30 }; // checkmate in 3 moves

            var isBetterActual = checkmateIn3.IsBetter(ChessColor.Black, checkmateIn1);

            Assert.IsFalse(isBetterActual, "Black should pick move with earlier checkmate.");
        }

        [TestMethod]
        public void RatingWithLaterLoosingIsBetterForBlackTest()
        {
            var checkmateIn1 = new MoveRating() { Score = ScoreWhiteWins - 10 }; // checkmate in 1 move
            var checkmateIn3 = new MoveRating() { Score = ScoreWhiteWins - 30 }; // checkmate in 3 moves

            var isBetterActual = checkmateIn1.IsBetter(ChessColor.Black, checkmateIn3);

            Assert.IsFalse(isBetterActual, "Black should pick move with loosing later.");
        }

        [TestMethod]
        public void RatingWithLaterLoosingIsBetterForBlackTest_mirror()
        {
            var checkmateIn1 = new MoveRating() { Score = ScoreWhiteWins - 10 }; // checkmate in 1 move
            var checkmateIn3 = new MoveRating() { Score = ScoreWhiteWins - 30 }; // checkmate in 3 moves

            var isBetterActual = checkmateIn3.IsBetter(ChessColor.Black, checkmateIn1);

            Assert.IsTrue(isBetterActual, "Black should pick move with loosing later.");
        }

        //// White, Test for IsEquallyGood

        [TestMethod]
        public void RatingWithSameScoreIsEqualTest()
        {
            var rating = new MoveRating() { Score = 100 };
            var littleBetterRating = new MoveRating() { Score = 100 + _smallDifference };

            var isEquallyGood = rating.IsEquallyGood(littleBetterRating);

            Assert.IsTrue(isEquallyGood, "Small difference in rating should be equally good.");
        }

        [TestMethod]
        public void RatingWithSameScoreIsEqualTest_mirror()
        {
            var rating = new MoveRating() { Score = 100 };
            var littleBetterRating = new MoveRating() { Score = 100 + _smallDifference };

            var isEquallyGood = littleBetterRating.IsEquallyGood(rating);

            Assert.IsTrue(isEquallyGood, "Small difference in rating should be equally good.");
        }

        [TestMethod]
        public void RatingWithdifferentScoreIsNotEqualTest()
        {
            var rating = new MoveRating() { Score = 100 };
            var betterRating = new MoveRating() { Score = 200 };

            var isEquallyGood = rating.IsEquallyGood(betterRating);

            Assert.IsFalse(isEquallyGood, "difference in rating should be not equally good.");
        }

        [TestMethod]
        public void RatingWithdifferentScoreIsNotEqualTest_mirror()
        {
            var rating = new MoveRating() { Score = 100 };
            var betterRating = new MoveRating() { Score = 200 };

            var isEquallyGood = betterRating.IsEquallyGood(rating);

            Assert.IsFalse(isEquallyGood, "difference in rating should be not equally good.");
        }

        [TestMethod]
        public void RatingWithEarlierCheckmateDifferentTest()
        {
            var checkmateIn1 = new MoveRating() { Score = ScoreWhiteWins - 10 }; // checkmate in 1 move
            var checkmateIn3 = new MoveRating() { Score = ScoreWhiteWins - 30 }; // checkmate in 3 moves

            var isEquallyGood = checkmateIn1.IsEquallyGood(checkmateIn3);

            Assert.IsFalse(isEquallyGood, "Checkmate happens in different levels so ratings are not equally good.");
        }

        [TestMethod]
        public void RatingWithSameCheckmateLevelTest()
        {
            var checkmateIn1 = new MoveRating() { Score = ScoreWhiteWins - 10 }; // checkmate in 1 move
            var checkmateIn3 = new MoveRating() { Score = ScoreWhiteWins - 10 }; // checkmate in 1 move

            var isEquallyGood = checkmateIn1.IsEquallyGood(checkmateIn3);

            Assert.IsTrue(isEquallyGood, "Checkmate happens in same level so ratings are equally good.");
        }
    }
}