using System;
using HansBrandonChessEngine;
using HBCommon;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace HansBrandonChessEngineTest
{
    [TestClass]
    public class EvaluatorPositionTest
    {
        private Board _board;

        [TestInitialize]
        public void Setup()
        {
            _board = new Board(new Mock<IHashtable>().Object);
        }

        [TestMethod]
        public void EvaluateTest_WhenPawnInCenter_Then1_2()
        {
            string position = "........" +
                              "........" +
                              "........" +
                              "........" +
                              "....P..." +
                              "........" +
                              "........" +
                              "........";
            _board.SetPosition(position);

            var target = new EvaluatorPosition(_board);
            var score = target.Evaluate();

            Assert.AreEqual(115, score);
        }

        [TestMethod]
        public void EvaluateTest_WhenPawnIn3rdRank_Then1_1()
        {
            string position = "........" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "....P..." +
                              "........" +
                              "........";
            _board.SetPosition(position);

            var target = new EvaluatorPosition(_board);
            var score = target.Evaluate();

            Assert.AreEqual(110, score);
        }

        [TestMethod]
        public void EvaluateTest_WhenKnightIsAtBorder_ThenItsAShame()
        {
            string position = "..k..K.." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              ".......N" +
                              "........";
            _board.SetPosition(position);

            var target = new EvaluatorPosition(_board);
            var score = target.Evaluate();

            Assert.AreEqual(300 - 5, score);
        }
        
        [TestMethod]
        public void EvaluateTest_WhenKnightIsNotAtBorder_ThenItsOk()
        {
            string position = "........" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "....N..." +
                              "........";
            _board.SetPosition(position);

            var target = new EvaluatorPosition(_board);
            var score = target.Evaluate();

            Assert.AreEqual(300, score);
        }

        [TestMethod]
        public void EvaluateTest_WhenWhiteHasTwoBishopAndBlackHasBishopAndKnight_ThenWhiteBetter()
        {
            string position = "........" +
                              "....bn.." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "....BB.." +
                              "........";
            _board.SetPosition(position);

            var target = new EvaluatorPosition(_board);
            var score = target.Evaluate();

            Assert.AreEqual(true, score > 0.1f, "Two bishops should be better than bishop and knight.");
        }

        [TestMethod]
        public void EvaluateTest_WhenBlackHasDoubleBishop_ThenBlackBetter()
        {
            string position = "........" +
                              "....bb.." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "....NB.." +
                              "........";
            _board.SetPosition(position);

            var target = new EvaluatorPosition(_board);
            var score = target.Evaluate();

            Assert.AreEqual(true, score < -0.1f, "Two bishops should be better than bishop and knight.");
        }

        [TestMethod]
        public void EvaluateTest_WhenWhiteDidCastle_ThenWhiteBetter()
        {
            string position = "rnbqk..r" +
                              "ppppppbp" +
                              ".....np." +
                              "........" +
                              "........" +
                              ".....NP." +
                              "PPPPPPBP" +
                              "RNBQK..R";
            _board.SetPosition(position);

            // white castling
            _board.Move(new CastlingMove(HansBrandonChessEngine.CastlingType.WhiteKingSide, new King(ChessColor.White)));
            var target = new EvaluatorPosition(_board);
            var score = target.Evaluate();
            Assert.AreEqual(true, score > 0.1f, "White did castling. so white should be better.");

            // black castling
            _board.Move(new CastlingMove(HansBrandonChessEngine.CastlingType.BlackKingSide, new King(ChessColor.Black)));
            score = target.Evaluate();
            Assert.AreEqual(true, score == 0, "White and Black did castling. They are equal.");

            // take black move back
            _board.Back();
            score = target.Evaluate();
            Assert.AreEqual(true, score > 0.1f, "Black castling was taken back. so white should be better.");
        }
    }
}
