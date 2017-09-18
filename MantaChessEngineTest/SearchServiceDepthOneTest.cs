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
            MoveGenerator gen = new MoveGenerator(new MoveFactory());
            var target = new SearchServiceDepthOne(evaluator, gen);
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
            MoveBase actualMove = target.CalcScoreLevelZero(board, Definitions.ChessColor.White, out score);
            MoveBase expectedMove = new NormalMove(Piece.MakePiece('P'), 'f', 4, 'e', 5, Piece.MakePiece('q'));


            Assert.AreEqual(expectedMove, actualMove, "Queen should be captured.");
            Assert.AreEqual(9, score);
        }

        [TestMethod]
        public void SearchBestMoveOneLevelTest_WhenWhenQueenCanBeCaptured_ThenCaptureQueen_Black()
        {
            float score = 0;
            IEvaluator evaluator = new EvaluatorSimple();
            MoveGenerator gen = new MoveGenerator(new MoveFactory());
            var target = new SearchServiceDepthOne(evaluator, gen);
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

            MoveBase actualMove = target.CalcScoreLevelZero(board, Definitions.ChessColor.Black, out score);
            MoveBase expectedMove = new NormalMove(Piece.MakePiece('p'), 'e', 5, 'd', 4, Piece.MakePiece('Q'));

            Assert.AreEqual(expectedMove, actualMove, "Queen should be captured.");
            Assert.AreEqual(-9, score);
        }

        [TestMethod]
        public void SearchTest_WhenQueenCanCapturPawnButWouldGetLost_ThenQueenDoesNotCapturePawn_White()
        {
            IEvaluator evaluator = new EvaluatorSimple();
            MoveGenerator gen = new MoveGenerator(new MoveFactory());
            ISearchService target = new SearchServiceDepthOne(evaluator, gen);
            var board = new Board();
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
            MoveBase verybadMove = new NormalMove(Piece.MakePiece('Q'), 'b', 4, 'c', 5, Piece.MakePiece('p'));
            Assert.AreNotEqual(verybadMove, actualMove, "Queen must not capture pawn. Otherwise queen gets lost.");
        }

        [TestMethod]
        public void SearchTest_WhenPawnCanCaptureQueenAndPawnGetsLost_ThenPawnMustCaptureQueen_White()
        {
            IEvaluator evaluator = new EvaluatorSimple();
            MoveGenerator gen = new MoveGenerator(new MoveFactory());
            ISearchService target = new SearchServiceDepthOne(evaluator, gen);
            var board = new Board();
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
            MoveBase goodMove = new NormalMove(Piece.MakePiece('P'), 'b', 4, 'c', 5, Piece.MakePiece('q'));
            Assert.AreEqual(goodMove, actualMove, "Pawn must capture queen.");
        }

    }
}
