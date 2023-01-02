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

            var target = new EvaluatorSimple(board);
            var score = target.Evaluate();

            Assert.AreEqual(100, score);
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

            var target = new EvaluatorSimple(board);
            var score = target.Evaluate();

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

            var target = new EvaluatorSimple(board);
            var score = target.Evaluate();

            var expectedScore = 8 * Definitions.ValuePawn +
                                2 * Definitions.ValueKnight +
                                2 * Definitions.ValueBishop +
                                2 * Definitions.ValueRook +
                                Definitions.ValueQueen;
            Assert.AreEqual(expectedScore, score);
        }
    }
}
