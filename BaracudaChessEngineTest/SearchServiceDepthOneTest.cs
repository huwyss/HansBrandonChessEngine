using System;
using BaracudaChessEngine;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BaracudaChessEngineTest
{
    [TestClass]
    public class SearchServiceDepthOneTest
    {
        [TestMethod]
        public void SearchTest_WhenWhenQueenCanBeCaptured_ThenCaptureQueen_White()
        {
            IEvaluator evaluator = new EvaluatorSimple();
            ISearchService target = new SearchServiceDepthOne(evaluator);
            MoveGenerator gen = new MoveGenerator();
            var board = new Board(gen);
            string boardString = "rnb.kbnr" +
                                 "ppp.pppp" +
                                 "........" +
                                 "....q..." +
                                 ".....P.." +
                                 "........" +
                                 "PPPPP.PP" +
                                 "RNBQKBNR";
            board.SetPosition(boardString);

            Move actualMove = target.Search(board, Definitions.ChessColor.White);
            Move expectedMove = new Move("f4e5q");
            Assert.AreEqual(expectedMove, actualMove, "Queen should be captured.");
        }
    }
}
