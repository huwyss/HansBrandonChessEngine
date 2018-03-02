using System;
using MantaChessEngine;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MantaChessEngineTest
{
    [TestClass]
    public class EvaluatorSimpleTest
    {
        [TestMethod]
        public void EvaluateTest_WhenOneWhitePawn_ThenWhite1_Black0()
        {
            Board board = new Board();
            string position = "........" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "P.......";
            board.SetPosition(position);

            var target = new EvaluatorSimple();
            var score = target.Evaluate(board);

            Assert.AreEqual(1, score);
        }

        [TestMethod]
        public void EvaluateTest_WhenBlackAndWhitePawn_ThenWhite2_Black2()
        {
            Board board = new Board();
            string position = "p......p" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "P......P";
            board.SetPosition(position);

            var target = new EvaluatorSimple();
            var score = target.Evaluate(board);

            Assert.AreEqual(0, score);
        }

        [TestMethod]
        public void EvaluateTest_WhenInitialPosition_ThenScoreCorrect()
        {
            Board board = new Board();
            string position = "....k..." + // black a8-h8
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "PPPPPPPP" +
                              "RNBQKBNR"; // white a1-h1
            board.SetPosition(position);

            var target = new EvaluatorSimple();
            var score = target.Evaluate(board);

            var expectedScore = 8 * target.ValuePawn +
                                       2 * target.ValueKnight +
                                       2 * target.ValueBishop +
                                       2 * target.ValueRook +
                                       target.ValueQueen;
            Assert.AreEqual(expectedScore, score);
        }

        [TestMethod]
        public void EvaluateTest_WhenKingMissing_ThenScoreBlackWins()
        {
            Board board = new Board();
            string position = "....k..." + // black a8-h8
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "........"; // white a1-h1
            board.SetPosition(position);

            var target = new EvaluatorSimple();
            var score = target.Evaluate(board);

            var expectedScore = Definitions.ScoreBlackWins;
            Assert.AreEqual(expectedScore, score);
        }
    }
}
