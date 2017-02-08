using System;
using BaracudaChessEngine;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BaracudaChessEngineTest
{
    [TestClass]
    public class SearchServiceDepthOneTest
    {
        [TestMethod]
        public void SearchTest_WhenQueenCanCapturPawnButWouldGetLost_ThenQueenDoesNotCapturePawn_White()
        {
            IEvaluator evaluator = new EvaluatorSimple();
            ISearchService target = new SearchServiceDepthOne(evaluator);
            MoveGenerator gen = new MoveGenerator();
            var board = new Board(gen);
            string boardString = ".......k" +
                                 "........" +
                                 "...p...." +
                                 "..p....." +
                                 ".Q......" +
                                 "........" +
                                 "........" +
                                 ".......K";
            board.SetPosition(boardString);

            Move actualMove = target.Search(board, Definitions.ChessColor.White);
            Move verybadMove = new Move("b4c5p");
            Assert.AreNotEqual(verybadMove, actualMove, "Queen must not capture pawn. Otherwise queen gets lost.");
        }

        [TestMethod]
        public void SearchTest_WhenPawnCanCaptureQueenAndPawnGetsLost_ThenPawnMustCaptureQueen_White()
        {
            IEvaluator evaluator = new EvaluatorSimple();
            ISearchService target = new SearchServiceDepthOne(evaluator);
            MoveGenerator gen = new MoveGenerator();
            var board = new Board(gen);
            string boardString = ".......k" +
                                 "........" +
                                 "...p...." +
                                 "..q....." +
                                 ".P......" +
                                 "........" +
                                 "........" +
                                 ".......K";
            board.SetPosition(boardString);

            Move actualMove = target.Search(board, Definitions.ChessColor.White);
            Move goodMove = new Move("b4c5q");
            Assert.AreEqual(goodMove, actualMove, "Pawn must capture queen.");
        }

    }
}
