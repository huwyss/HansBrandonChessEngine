using System;
using MantaChessEngine;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MantaChessEngineTest
{
    [TestClass]
    public class SearchServiceDepthOneTest
    {
        [TestMethod]
        public void SearchBestMoveOneLevelTest_WhenWhenQueenCanBeCaptured_ThenCaptureQueen_White()
        {
            IEvaluator evaluator = new EvaluatorSimple();
            var target = new SearchServiceDepthOne(evaluator);
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
            Move actualMove = target.CalcScoreLevelZero(board, Definitions.ChessColor.White, out score);
            Move expectedMove = new Move("f4e5q");
            
            Assert.AreEqual(expectedMove, actualMove, "Queen should be captured.");
            Assert.AreEqual(9, score);
        }

        [TestMethod]
        public void SearchBestMoveOneLevelTest_WhenWhenQueenCanBeCaptured_ThenCaptureQueen_Black()
        {
            float score = 0;
            IEvaluator evaluator = new EvaluatorSimple();
            var target = new SearchServiceDepthOne(evaluator);
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

            Move actualMove = target.CalcScoreLevelZero(board, Definitions.ChessColor.Black, out score);
            Move expectedMove = new Move("e5d4Q");

            Assert.AreEqual(expectedMove, actualMove, "Queen should be captured.");
            Assert.AreEqual(-9, score);
        }

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

            float score = 0;
            IMove actualMove = target.Search(board, Definitions.ChessColor.White, out score);
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

            float score = 0;
            IMove actualMove = target.Search(board, Definitions.ChessColor.White, out score);
            Move goodMove = new Move("b4c5q");
            Assert.AreEqual(goodMove, actualMove, "Pawn must capture queen.");
        }

    }
}
