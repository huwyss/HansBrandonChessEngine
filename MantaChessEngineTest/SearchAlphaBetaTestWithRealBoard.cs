using System.Linq;
using System.Collections.Generic;
using MantaChessEngine;
using MantaCommon;
using MantaChessEngineTest.Doubles;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MantaChessEngineTest
{
    /// <summary>
    /// Tests for SearchAlphaBeta with real board.
    /// </summary>

    [TestClass]
    public class SearchAlphaBetaTestWithRealBoard
    {
        const int AlphaStart = int.MinValue;
        const int BetaStart = int.MaxValue;

        Board _board;
        IEvaluator _evaluator;
        MoveGenerator _gen;

        // ---------------------------------------------------------------------------------------------
        // White is first mover
        // ---------------------------------------------------------------------------------------------

        [TestInitialize]
        public void Setup()
        {
            _board = new Board();
            _evaluator = new EvaluatorSimple(_board);
            _gen = new MoveGenerator();
        }
        [TestMethod]
        public void SearchLevelTest_WhenLevelOne_ThenCapturePawn_White()
        {
            var target = new SearchAlphaBeta(_board, _evaluator, _gen, 1, null, null);
            string boardString = "k......." +
                                 "........" +
                                 "...p...." +
                                 "....p..." +
                                 ".....B.." +
                                 "........" +
                                 "........" +
                                 "K.......";
            _board.SetPosition(boardString);

            var bestRatingActual = target.SearchLevel(ChessColor.White, 1, AlphaStart, BetaStart); // level 1
            IMove goodMove = new NormalMove(Piece.MakePiece('B'), 'f', 4, 'e', 5, Piece.MakePiece('p'));

            Assert.AreEqual(goodMove, bestRatingActual.Move, "White bishop should capture pawn. We are on level 1");
            Assert.AreEqual(200, bestRatingActual.Score);
        }

        [TestMethod]
        public void SearchBestMoveOneLevelTest_WhenLevel2_ThenDoNotCapturePawn_White()
        {
            var target = new SearchAlphaBeta(_board, _evaluator, _gen, 2, null, null);
            string boardString = "k......." +
                                 "........" +
                                 "...p...." +
                                 "....p..." +
                                 ".....B.." +
                                 "........" +
                                 "........" +
                                 "K.......";
            _board.SetPosition(boardString);

            var bestRatingActual = target.SearchLevel(ChessColor.White, 1, AlphaStart, BetaStart); // level 2
            IMove badMove = new NormalMove(Piece.MakePiece('B'), 'f', 4, 'e', 5, Piece.MakePiece('p'));

            Assert.AreNotEqual(badMove, bestRatingActual.Move, "White bishop should not capture pawn.");
            Assert.AreEqual(100, bestRatingActual.Score);
        }

        [TestMethod]
        public void SearchBestMoveTest_WhenLevel3_ThenCapturePawn_White()
        {
            var target = new SearchAlphaBeta(_board, _evaluator, _gen, 3, null, null);
            string boardString = "k......." +
                                 "...n...." +
                                 "...b...." +
                                 "....p..." +
                                 ".....B.." +
                                 ".....N.." +
                                 "........" +
                                 "K.......";
            _board.SetPosition(boardString);

                                 //"k......." +   After position
                                 //"...n...." +
                                 //"........" +
                                 //"....N..." +   can also be B
                                 //"........" +
                                 //"........" +
                                 //"........" +
                                 //"K.......";

            var bestRatingActual = target.SearchLevel(ChessColor.White, 1, AlphaStart, BetaStart); // level 3
            IMove expectedMove = new NormalMove(Piece.MakePiece('B'), 'f', 4, 'e', 5, Piece.MakePiece('p'));
            IMove expectedMove2 = new NormalMove(Piece.MakePiece('N'), 'f', 3, 'e', 5, Piece.MakePiece('p'));

            bool passed = bestRatingActual.Move.Equals(expectedMove) ||
                          bestRatingActual.Move.Equals(expectedMove2);

            Assert.AreEqual(true, passed, "White bishop or knight should capture black pawn. It is actually bad but we test level 3.");
            Assert.AreEqual(0, bestRatingActual.Score);
        }

        [TestMethod]
        public void SearchBestMoveOneLevelTest_WhenLevel4_ThenDoNotCapturePawn_White()
        {
            var target = new SearchAlphaBeta(_board, _evaluator, _gen, 4, null, null);
            string boardString = "k......." +
                                 "...n...." +
                                 "...b...." +
                                 "....p..." +
                                 ".....B.." +
                                 ".....N.." +
                                 "........" +
                                 "K.......";
            _board.SetPosition(boardString);

            var bestRatingActual = target.SearchLevel(ChessColor.White, 1, AlphaStart, BetaStart); // level 4
            IMove badMove = new NormalMove(Piece.MakePiece('B'), 'f', 4, 'e', 5, Piece.MakePiece('p'));
            IMove badMove2 = new NormalMove(Piece.MakePiece('N'), 'f', 3, 'e', 5, Piece.MakePiece('p'));

            Assert.AreNotEqual(badMove, bestRatingActual.Move, "White bishop or knight should not capture black pawn.");
            Assert.AreNotEqual(badMove2, bestRatingActual.Move, "White bishop or knight should not capture black pawn.");
            Assert.AreEqual(-100, bestRatingActual.Score);
        }

        [TestMethod]
        public void SearchLevelTest_WhenWhiteInCheck_ThenDoNotAttackBlackKing_White()
        {
            var target = new SearchAlphaBeta(_board, _evaluator, _gen, 3, null, null);
            string boardString = "....q..R" +
                                 "........" +
                                 "....k..." +
                                 "...b...." +
                                 "....K..." +
                                 "........" +
                                 "........" +
                                 "........";
            _board.SetPosition(boardString);

            var bestRatingActual = target.SearchLevel(ChessColor.White, 1, AlphaStart, BetaStart); // level 3
            IMove wrongMove = new NormalMove(Piece.MakePiece('R'), 'h', 8, 'e', 8, Piece.MakePiece('q'));
            IMove wrongMove2 = new NormalMove(Piece.MakePiece('K'), 'e', 4, 'd', 5, Piece.MakePiece('b'));

            Assert.AreNotEqual(wrongMove, bestRatingActual.Move, "White must escape check.");
            Assert.AreNotEqual(wrongMove2, bestRatingActual.Move, "White must escape check.");
        }

        [TestMethod]
        public void SearchLevelTest_WhenWhiteIsCheckMate_ThenNoLegalMove_White()
        {
            var target = new SearchAlphaBeta(_board, _evaluator, _gen, 3, null, null);
            string boardString = "........" +
                                 "........" +
                                 "........" +
                                 "........" +
                                 "........" +
                                 "...k...." +
                                 "...q...." +
                                 "...K....";
            _board.SetPosition(boardString);

            var bestRatingActual = target.SearchLevel(ChessColor.White, 1, AlphaStart, BetaStart); // level 3
            IMove expectedMove = new NoLegalMove();

            Assert.AreEqual(expectedMove, bestRatingActual.Move, "White is check mate. no legal move possible.");
        }

        // ---------------------------------------------------------------------------------------------
        // Black is first mover
        // ---------------------------------------------------------------------------------------------

        [TestMethod]
        public void SearchLevelTest_WhenLevelOne_ThenCapturePawn_Black()
        {
            var target = new SearchAlphaBeta(_board, _evaluator, _gen, 1, null, null);
            string boardString = "k......." +
                                 "........" +
                                 ".....b.." +
                                 "....P..." +
                                 "...P...." +
                                 "........" +
                                 "........" +
                                 "K.......";
            _board.SetPosition(boardString);

            var bestRatingActual = target.SearchLevel(ChessColor.Black, 1, AlphaStart, BetaStart); // level 1
            IMove goodMove = new NormalMove(Piece.MakePiece('b'), 'f', 6, 'e', 5, Piece.MakePiece('P'));

            Assert.AreEqual(goodMove, bestRatingActual.Move, "Black bishop should capture pawn. We are on level 1");
            Assert.AreEqual(-200, bestRatingActual.Score);
        }

        [TestMethod]
        public void SearchBestMoveOneLevelTest_WhenLevel2_ThenDoNotCapturePawn_Black()
        {
            var target = new SearchAlphaBeta(_board, _evaluator, _gen, 2, null, null);
            string boardString = "k......." +
                                 "........" +
                                 ".....b.." +
                                 "....P..." +
                                 "...P...." +
                                 "........" +
                                 "........" +
                                 "K.......";
            _board.SetPosition(boardString);

            var bestRatingActual = target.SearchLevel(ChessColor.Black, 1, AlphaStart, BetaStart); // level 2
            IMove badMove = new NormalMove(Piece.MakePiece('b'), 'f', 6, 'e', 5, Piece.MakePiece('P'));

            Assert.AreNotEqual(badMove, bestRatingActual.Move, "Black bishop should not capture pawn.");
            Assert.AreEqual(-100, bestRatingActual.Score);
        }

        [TestMethod]
        public void SearchBestMoveOneLevelTest_WhenLevel3_ThenCapturePawn_Black()
        {
            var target = new SearchAlphaBeta(_board, _evaluator, _gen, 3, null, null);
            string boardString = "k......." +
                                 "...n...." +
                                 "...b...." +
                                 "....P..." +
                                 ".....B.." +
                                 ".....N.." +
                                 "........" +
                                 "K.......";
            _board.SetPosition(boardString);

            //"k......." +   After position
            //"........" +
            //"........" +
            //"....n..." +   can also be b
            //"........" +
            //".....N.." +
            //"........" +
            //"K.......";

            var bestRatingActual = target.SearchLevel(ChessColor.Black, 1, AlphaStart, BetaStart); // level 3
            IMove expectedMove = new NormalMove(Piece.MakePiece('b'), 'd', 6, 'e', 5, Piece.MakePiece('P'));
            IMove expectedMove2 = new NormalMove(Piece.MakePiece('n'), 'd', 7, 'e', 5, Piece.MakePiece('P'));

            bool passed = bestRatingActual.Move.Equals(expectedMove) ||
                          bestRatingActual.Move.Equals(expectedMove2);

            Assert.AreEqual(true, passed, "Black bishop or knight should capture white pawn. It is actually bad but we test level 3.");
            Assert.AreEqual(0, bestRatingActual.Score);
        }

        [TestMethod]
        public void SearchBestMoveOneLevelTest_WhenLevel4_ThenDoNotCapturePawn_Black()
        {
            var target = new SearchAlphaBeta(_board, _evaluator, _gen, 4, null, null);
            string boardString = "k......." +
                                 "...n...." +
                                 "...b...." +
                                 "....P..." +
                                 ".....B.." +
                                 ".....N.." +
                                 "........" +
                                 "K.......";
            _board.SetPosition(boardString);

            var bestRatingActual = target.SearchLevel(ChessColor.Black, 1, AlphaStart, BetaStart); // level 4
            IMove badMove = new NormalMove(Piece.MakePiece('b'), 'd', 6, 'e', 5, Piece.MakePiece('P'));
            IMove badMove2 = new NormalMove(Piece.MakePiece('n'), 'd', 7, 'e', 5, Piece.MakePiece('P'));

            Assert.AreNotEqual(badMove, bestRatingActual.Move, "Black bishop or knight should not capture white pawn.");
            Assert.AreNotEqual(badMove2, bestRatingActual.Move, "Black bishop or knight should not capture white pawn.");
            Assert.AreEqual(100, bestRatingActual.Score);
        }

        // ---------------------------------------------------------------------------------------------
        // White is stall mate and check mate
        // ---------------------------------------------------------------------------------------------

        [TestMethod]
        public void SearchBestMoveTest_WhenWhiteStallmate_ThenNoLegalMoveAndBestScore0()
        {
            var target = new SearchAlphaBeta(_board, _evaluator, _gen, 2, null, null);
            string boardString = "........" +
                                 "........" +
                                 "........" +
                                 "........" +
                                 "........" +
                                 "..k....." +
                                 ".r......" +
                                 "K.......";
            _board.SetPosition(boardString);

            var bestRatingActual = target.SearchLevel(ChessColor.White, 1, AlphaStart, BetaStart);

            AssertHelper.StallMate(bestRatingActual);
            Assert.AreEqual(new NoLegalMove(), bestRatingActual.Move, "Should be NoLegalMove, white is stalemate");
        }

        [TestMethod]
        public void SearchBestMoveTest_WhenWhiteCheckmate_ThenNoLegalMoveAndBestScoreMinusThousand()
        {
            var target = new SearchAlphaBeta(_board, _evaluator, _gen, 2, null, null);
            string boardString = "........" +
                                 "........" +
                                 "........" +
                                 "........" +
                                 "........" +
                                 "..k....." +
                                 ".q......" +
                                 "K.......";
            _board.SetPosition(boardString);

            var bestRatingActual = target.SearchLevel(ChessColor.White, 1, AlphaStart, BetaStart);

            Assert.AreEqual(new NoLegalMove(), bestRatingActual.Move, "Should be NoLegalMove, white is checkmate");
            AssertHelper.BlackWins(bestRatingActual);
        }

        [TestMethod]
        public void SearchBestMoveTest_WhenBlackIsCheckmate_ThenNoLegalMoveAndBestScoreThousand()
        {
            var target = new SearchAlphaBeta(_board, _evaluator, _gen, 2, null, null);
            string boardString = ".rbqkb.r" +
                                 "pppppBpp" +
                                 "..n....." +
                                 "......N." +
                                 "........" +
                                 "........" +
                                 "PPPP.PPP" +
                                 "R.BnK..R";
            _board.SetPosition(boardString);

            var bestRatingActual = target.SearchLevel(ChessColor.Black, 1, AlphaStart, BetaStart);

            Assert.AreEqual(new NoLegalMove(), bestRatingActual.Move, "Should be NoLegalMove, Black is checkmate");
            AssertHelper.WhiteWins(bestRatingActual);
        }

        [TestMethod]
        public void SearchBestMoveTest_WhenWhiteIsCheckmateIn2_ThenWinningMove()
        {
            var target = new SearchAlphaBeta(_board, _evaluator, _gen, 4, null, null);
            target.SetMaxDepth(4);
            string boardString = "........" +
                                 "........" +
                                 "........" +
                                 "........" +
                                 "........" +
                                 "..k....." +
                                 ".......q" +
                                 "K.......";
            _board.SetPosition(boardString);

            IMove expectedMove = new NormalMove(Piece.MakePiece('q'), 'h', 2, 'b', 2, null);

            var bestRatingActual = target.SearchLevel(ChessColor.Black, 1, AlphaStart, BetaStart);

            Assert.AreEqual(expectedMove, bestRatingActual.Move, "Should be find checkmate for black: ... Qh2b2 #");
            AssertHelper.BlackWins(bestRatingActual);
        }
    }
}
