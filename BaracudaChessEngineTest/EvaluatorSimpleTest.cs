using System;
using BaracudaChessEngine;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BaracudaChessEngineTest
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

            Assert.AreEqual(1, score.ScoreWhite);
            Assert.AreEqual(0, score.ScoreBlack);
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

            Assert.AreEqual(2, score.ScoreWhite);
            Assert.AreEqual(2, score.ScoreBlack);
        }

        [TestMethod]
        public void EvaluateTest_WhenInitialPosition_ThenScoreCorrect()
        {
            Board board = new Board();
            string position = "rnbqkbnr" + // black a8-h8
                              "pppppppp" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "PPPPPPPP" +
                              "RNBQKBNR"; // white a1-h1
            board.SetPosition(position);

            var target = new EvaluatorSimple();
            var score = target.Evaluate(board);

            float expectedScoreWhite = 8*target.ValuePawn +
                                       2*target.ValueKnight +
                                       2*target.ValueBishop +
                                       2*target.ValueRook +
                                       target.ValueQueen +
                                       target.ValueKing;
            float expectedScoreBlack = expectedScoreWhite;
            Assert.AreEqual(expectedScoreWhite, score.ScoreWhite);
            Assert.AreEqual(expectedScoreBlack, score.ScoreBlack);
        }
    }
}
