using System;
using MantaChessEngine;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MantaChessEngineTest
{
    [TestClass]
    public class EvaluatorPositionTest
    {
        [TestMethod]
        public void EvaluateTest_WhenPawnInCenter_Then1_2()
        {
            Board board = new Board(null);
            string position = "........" +
                              "........" +
                              "........" +
                              "........" +
                              "....P..." +
                              "........" +
                              "........" +
                              "........";
            board.SetPosition(position);

            var target = new EvaluatorPosition();
            var score = target.Evaluate(board);

            Assert.AreEqual(1.2f, score);
        }

        [TestMethod]
        public void EvaluateTest_WhenPawnIn3rdRank_Then1_1()
        {
            Board board = new Board(null);
            string position = "........" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "....P..." +
                              "........" +
                              "........";
            board.SetPosition(position);

            var target = new EvaluatorPosition();
            var score = target.Evaluate(board);

            Assert.AreEqual(1.1f, score);
        }

        [TestMethod]
        public void EvaluateTest_WhenKnightIsAtBorder_ThenItsAShame()
        {
            Board board = new Board(null);
            string position = "........" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              ".......N" +
                              "........";
            board.SetPosition(position);

            var target = new EvaluatorPosition();
            var score = target.Evaluate(board);

            Assert.AreEqual(0.95f * 3f, score);
        }
        
        [TestMethod]
        public void EvaluateTest_WhenKnightIsNotAtBorder_ThenItsOk()
        {
            Board board = new Board(null);
            string position = "........" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "....N..." +
                              "........";
            board.SetPosition(position);

            var target = new EvaluatorPosition();
            var score = target.Evaluate(board);

            Assert.AreEqual(3f, score);
        }

        [TestMethod]
        public void EvaluateTest_WhenWhiteHasTwoBishopAndBlackHasBishopAndKnight_ThenWhiteBetter()
        {
            Board board = new Board(null);
            string position = "........" +
                              "....bn.." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "....BB.." +
                              "........";
            board.SetPosition(position);

            var target = new EvaluatorPosition();
            var score = target.Evaluate(board);

            Assert.AreEqual(true, score > 0.1f, "Two bishops should be better than bishop and knight.");
        }

        [TestMethod]
        public void EvaluateTest_WhenBlackHasDoubleBishop_ThenBlackBetter()
        {
            Board board = new Board(null);
            string position = "........" +
                              "....bb.." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "....NB.." +
                              "........";
            board.SetPosition(position);

            var target = new EvaluatorPosition();
            var score = target.Evaluate(board);

            Assert.AreEqual(true, score < -0.1f, "Two bishops should be better than bishop and knight.");
        }
    }
}
