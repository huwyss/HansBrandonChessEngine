using System;
using MantaChessEngine;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MantaChessEngineTest
{
    [TestClass]
    public class SearchServiceTreeTest
    {
        [TestMethod]
        public void SearchTest_WhenWhenQueenCanBeCaptured_ThenCaptureQueen_White()
        {
            IEvaluator evaluator = new EvaluatorSimple();
            MoveGenerator gen = new MoveGenerator(new MoveFactory());
            ISearchService target = new SearchServiceTree(evaluator, gen);
            var board = new Board();
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
            MoveBase expectedMove = new NormalMove('P', 'f', 4, 'e', 5, 'q');
            Assert.AreEqual(expectedMove, actualMove, "Queen should be captured.");
        }

        [TestMethod]
        public void SearchTest_WhenWhenQueenCanBeCaptured_ThenCaptureQueen_Black()
        {
            IEvaluator evaluator = new EvaluatorSimple();
            MoveGenerator gen = new MoveGenerator(new MoveFactory());
            ISearchService target = new SearchServiceTree(evaluator, gen);
            var board = new Board();
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
            MoveBase expectedMove = new NormalMove('p', 'e', 5, 'd', 4, 'Q');
            Assert.AreEqual(expectedMove, actualMove, "Queen should be captured.");
        }
    }
}
