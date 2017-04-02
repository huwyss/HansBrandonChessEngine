using System;
using MantaChessEngine;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MantaChessEngineTest
{
    [TestClass]
    public class SearchServiceDepthHalfTest
    {
        [TestMethod]
        public void SearchTest_WhenWhenQueenCanBeCaptured_ThenCaptureQueen_White()
        {
            IEvaluator evaluator = new EvaluatorSimple();
            ISearchService target = new SearchServiceDepthHalfMove(evaluator);
            MoveGenerator gen = new MoveGenerator();
            var board = new Board(gen);
            string boardString = "rnb.kbnr" +
                                 "ppp.pppp" +
                                 "........" +
                                 "....q..." +
                                 "...p.P.." +
                                 "........" +
                                 "PPPPP.PP" +
                                 "RNBQKBNR";
            board.SetPosition(boardString);

            float score = 0;
            IMove actualMove = target.Search(board, Definitions.ChessColor.White, out score);
            MoveBase expectedMove = new NormalMove("f4e5q");
            Assert.AreEqual(expectedMove, actualMove, "Queen should be captured.");
        }

        [TestMethod]
        public void SearchTest_WhenWhenQueenCanBeCaptured_ThenCaptureQueen_Black()
        {
            IEvaluator evaluator = new EvaluatorSimple();
            ISearchService target = new SearchServiceDepthHalfMove(evaluator);
            MoveGenerator gen = new MoveGenerator();
            var board = new Board(gen);
            string boardString = "rnbqkbnr" +
                                 "pppp.ppp" +
                                 "........" +
                                 "...Pp..." +
                                 "...Q...." +
                                 "........" +
                                 "PPP.PPPP" +
                                 "RNB.KBNR";
            board.SetPosition(boardString);

            float score = 0;
            IMove actualMove = target.Search(board, Definitions.ChessColor.Black, out score);
            MoveBase expectedMove = new NormalMove("e5d4Q");
            Assert.AreEqual(expectedMove, actualMove, "Queen should be captured.");
        }
    }
}
