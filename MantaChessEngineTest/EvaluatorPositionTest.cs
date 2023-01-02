using System;
using MantaChessEngine;
using MantaCommon;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MantaChessEngineTest
{
    [TestClass]
    public class EvaluatorPositionTest
    {
        [TestMethod]
        public void EvaluateTest_WhenPawnInCenter_Then1_2()
        {
            Board board = new Board();
            string position = "........" +
                              "........" +
                              "........" +
                              "........" +
                              "....P..." +
                              "........" +
                              "........" +
                              "........";
            board.SetPosition(position);

            var target = new EvaluatorPosition(board);
            var score = target.Evaluate();

            Assert.AreEqual(115, score);
        }

        [TestMethod]
        public void EvaluateTest_WhenPawnIn3rdRank_Then1_1()
        {
            Board board = new Board();
            string position = "........" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "....P..." +
                              "........" +
                              "........";
            board.SetPosition(position);

            var target = new EvaluatorPosition(board);
            var score = target.Evaluate();

            Assert.AreEqual(110, score);
        }

        [TestMethod]
        public void EvaluateTest_WhenKnightIsAtBorder_ThenItsAShame()
        {
            Board board = new Board();
            string position = "..k..K.." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              ".......N" +
                              "........";
            board.SetPosition(position);

            var target = new EvaluatorPosition(board);
            var score = target.Evaluate();

            Assert.AreEqual(300 - 5, score);
        }
        
        [TestMethod]
        public void EvaluateTest_WhenKnightIsNotAtBorder_ThenItsOk()
        {
            Board board = new Board();
            string position = "........" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "....N..." +
                              "........";
            board.SetPosition(position);

            var target = new EvaluatorPosition(board);
            var score = target.Evaluate();

            Assert.AreEqual(300, score);
        }

        [TestMethod]
        public void EvaluateTest_WhenWhiteHasTwoBishopAndBlackHasBishopAndKnight_ThenWhiteBetter()
        {
            Board board = new Board();
            string position = "........" +
                              "....bn.." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "....BB.." +
                              "........";
            board.SetPosition(position);

            var target = new EvaluatorPosition(board);
            var score = target.Evaluate();

            Assert.AreEqual(true, score > 0.1f, "Two bishops should be better than bishop and knight.");
        }

        [TestMethod]
        public void EvaluateTest_WhenBlackHasDoubleBishop_ThenBlackBetter()
        {
            Board board = new Board();
            string position = "........" +
                              "....bb.." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "....NB.." +
                              "........";
            board.SetPosition(position);

            var target = new EvaluatorPosition(board);
            var score = target.Evaluate();

            Assert.AreEqual(true, score < -0.1f, "Two bishops should be better than bishop and knight.");
        }

        [TestMethod]
        public void EvaluateTest_WhenWhiteDidCastle_ThenWhiteBetter()
        {
            Board board = new Board();
            string position = "rnbqk..r" +
                              "ppppppbp" +
                              ".....np." +
                              "........" +
                              "........" +
                              ".....NP." +
                              "PPPPPPBP" +
                              "RNBQK..R";
            board.SetPosition(position);

            // white castling
            board.Move(new CastlingMove(CastlingType.WhiteKingSide, new King(ChessColor.White)));
            var target = new EvaluatorPosition(board);
            var score = target.Evaluate();
            Assert.AreEqual(true, score > 0.1f, "White did castling. so white should be better.");

            // black castling
            board.Move(new CastlingMove(CastlingType.BlackKingSide, new King(ChessColor.Black)));
            score = target.Evaluate();
            Assert.AreEqual(true, score == 0, "White and Black did castling. They are equal.");

            // take black move back
            board.Back();
            score = target.Evaluate();
            Assert.AreEqual(true, score > 0.1f, "Black castling was taken back. so white should be better.");
        }
    }
}
