using MantaBitboardEngine;
using MantaCommon;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MantaChessEngineTest
{
    [TestClass]
    public class MantaBitboardEngineTest
    {
        [TestMethod]
        public void EvaluateTest_WhenPawnInCenter_Then1_2()
        {
            var target = new BitEvaluator(new HelperBitboards());
            var board = new Bitboards();
            board.SetPosition("........" +
                              "........" +
                              "........" +
                              "........" +
                              "....P..." +
                              "........" +
                              "........" +
                              "........");

            var score = target.Evaluate(board);

            Assert.AreEqual(115, score);
        }

        [TestMethod]
        public void EvaluateTest_WhenPawnIn3rdRank_Then1_1()
        {
            var target = new BitEvaluator(new HelperBitboards());
            var board = new Bitboards();
            board.SetPosition("........" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "....P..." +
                              "........" +
                              "........");

            var score = target.Evaluate(board);

            Assert.AreEqual(110, score);
        }

        [TestMethod]
        public void EvaluateTest_WhenKnightIsAtBorder_ThenItsAShame()
        {
            var target = new BitEvaluator(new HelperBitboards());
            var board = new Bitboards();
            board.SetPosition("..k..K.." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              ".......N" +
                              "........");

            var score = target.Evaluate(board);

            Assert.AreEqual(300 - 5, score);
        }
        
        [TestMethod]
        public void EvaluateTest_WhenKnightIsNotAtBorder_ThenItsOk()
        {
            var target = new BitEvaluator(new HelperBitboards());
            var board = new Bitboards();
            board.SetPosition("........" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "....N..." +
                              "........");

            var score = target.Evaluate(board);

            Assert.AreEqual(300, score);
        }

        [TestMethod]
        public void EvaluateTest_WhenWhiteHasTwoBishopAndBlackHasBishopAndKnight_ThenWhiteBetter()
        {
            var target = new BitEvaluator(new HelperBitboards());
            var board = new Bitboards();
            board.SetPosition("........" +
                              "........" +
                              "..n..b.." +
                              "........" +
                              "........" +
                              "..B..B.." +
                              "........" +
                              "........");

            var score = target.Evaluate(board);

            Assert.AreEqual(true, score > 0.1f, "Two bishops should be better than bishop and knight.");
        }

        [TestMethod]
        public void EvaluateTest_WhenBlackHasDoubleBishop_ThenBlackBetter()
        {
            var target = new BitEvaluator(new HelperBitboards());
            var board = new Bitboards();
            board.SetPosition("........" +
                              "........" +
                              "..b..b.." +
                              "........" +
                              "........" +
                              "..N..B.." +
                              "........" +
                              "........");

            var score = target.Evaluate(board);

            Assert.AreEqual(true, score < -0.1f, "Two bishops should be better than bishop and knight.");
        }

        [TestMethod]
        public void EvaluateTest_WhenWhiteDidCastle_ThenWhiteBetter()
        {
            var target = new BitEvaluator(new HelperBitboards());
            var board = new Bitboards();
            board.SetPosition("rnbqk..r" +
                              "pppppppp" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "PPPPPPPP" +
                              "RNBQK..R");

            // white castling
            board.Move(BitMove.CreateCastling(ChessColor.White, CastlingType.KingSide, 0));
            var score = target.Evaluate(board);
            Assert.AreEqual(true, score > 0.1f, "White did castling. so white should be better.");

            // black castling
            board.Move(BitMove.CreateCastling(ChessColor.Black, CastlingType.KingSide, 0));
            score = target.Evaluate(board);
            Assert.AreEqual(true, score == 0, "White and Black did castling. They are equal.");

            // take black move back
            board.Back();
            score = target.Evaluate(board);
            Assert.AreEqual(true, score > 0.1f, "Black castling was taken back. so white should be better.");
        }
    }
}
