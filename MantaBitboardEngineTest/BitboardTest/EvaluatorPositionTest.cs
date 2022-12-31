using MantaBitboardEngine;
using MantaCommon;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace MantaChessEngineTest
{
    [TestClass]
    public class MantaBitboardEngineTest
    {
        private BitEvaluator _target;
        private Bitboards _board;

        [TestInitialize]
        public void Setup()
        {
            _target = new BitEvaluator(new HelperBitboards());
            _board = new Bitboards(new Mock<IHashtable>().Object);
        }

        [TestMethod]
        public void EvaluateTest_WhenPawnInCenter_Then1_2()
        {
            _board.SetPosition("........" +
                              "........" +
                              "........" +
                              "........" +
                              "....P..." +
                              "........" +
                              "........" +
                              "........");

            var score = _target.Evaluate(_board);

            Assert.AreEqual(115, score);
        }

        [TestMethod]
        public void EvaluateTest_WhenPawnIn3rdRank_Then1_1()
        {
            _board.SetPosition("........" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "....P..." +
                              "........" +
                              "........");

            var score = _target.Evaluate(_board);

            Assert.AreEqual(110, score);
        }

        [TestMethod]
        public void EvaluateTest_WhenKnightIsAtBorder_ThenItsAShame()
        {
            _board.SetPosition("....k..." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              ".......N" +
                              "....K...");

            var score = _target.Evaluate(_board);

            Assert.AreEqual(300 - 5, score);
        }
        
        [TestMethod]
        public void EvaluateTest_WhenKnightIsNotAtBorder_ThenItsOk()
        {
            _board.SetPosition("........" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "....N..." +
                              "........");

            var score = _target.Evaluate(_board);

            Assert.AreEqual(300, score);
        }

        [TestMethod]
        public void EvaluateTest_WhenWhiteHasTwoBishopAndBlackHasBishopAndKnight_ThenWhiteBetter()
        {
            _board.SetPosition("........" +
                              "........" +
                              "..n..b.." +
                              "........" +
                              "........" +
                              "..B..B.." +
                              "........" +
                              "........");

            var score = _target.Evaluate(_board);

            Assert.AreEqual(true, score > 0.1f, "Two bishops should be better than bishop and knight.");
        }

        [TestMethod]
        public void EvaluateTest_WhenBlackHasDoubleBishop_ThenBlackBetter()
        {
            _board.SetPosition("........" +
                              "........" +
                              "..b..b.." +
                              "........" +
                              "........" +
                              "..N..B.." +
                              "........" +
                              "........");

            var score = _target.Evaluate(_board);

            Assert.AreEqual(true, score < -0.1f, "Two bishops should be better than bishop and knight.");
        }

        [TestMethod]
        public void EvaluateTest_WhenWhiteDidCastle_ThenWhiteBetter()
        {
            _board.SetPosition("rnbqk..r" +
                              "pppppppp" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "PPPPPPPP" +
                              "RNBQK..R");

            // white castling
            _board.Move(BitMove.CreateCastling(ChessColor.White, CastlingType.KingSide, 0));
            var score = _target.Evaluate(_board);
            Assert.AreEqual(true, score > 0.1f, "White did castling. so white should be better.");

            // black castling
            _board.Move(BitMove.CreateCastling(ChessColor.Black, CastlingType.KingSide, 0));
            score = _target.Evaluate(_board);
            Assert.AreEqual(true, score == 0, "White and Black did castling. They are equal.");

            // take black move back
            _board.Back();
            score = _target.Evaluate(_board);
            Assert.AreEqual(true, score > 0.1f, "Black castling was taken back. so white should be better.");
        }
    }
}
