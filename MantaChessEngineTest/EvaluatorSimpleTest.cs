using System;
using MantaChessEngine;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MantaCommon;
using Moq;

namespace MantaChessEngineTest
{
    [TestClass]
    public class EvaluatorSimpleTest
    {
        private Board _board;

        [TestInitialize]
        public void Setup()
        {
            _board = new Board(new Mock<IHashtable>().Object);
        }

        [TestMethod]
        public void EvaluateTest_WhenOneWhitePawn_ThenWhite1_Black0()
        {
            string position = "........" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "P.......";
            _board.SetPosition(position);

            var target = new EvaluatorSimple(_board);
            var score = target.Evaluate();

            Assert.AreEqual(100, score);
        }

        [TestMethod]
        public void EvaluateTest_WhenBlackAndWhitePawn_ThenWhite2_Black2()
        {
            string position = "p......p" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "P......P";
            _board.SetPosition(position);

            var target = new EvaluatorSimple(_board);
            var score = target.Evaluate();

            Assert.AreEqual(0, score);
        }

        [TestMethod]
        public void EvaluateTest_WhenInitialPosition_ThenScoreCorrect()
        {
            string position = "....k..." + // black a8-h8
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "PPPPPPPP" +
                              "RNBQKBNR"; // white a1-h1
            _board.SetPosition(position);

            var target = new EvaluatorSimple(_board);
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
