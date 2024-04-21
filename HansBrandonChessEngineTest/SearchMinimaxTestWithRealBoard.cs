using System.Linq;
using System.Collections.Generic;
using HansBrandonChessEngine;
using HansBrandonChessEngineTest.Doubles;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HBCommon;
using Moq;

namespace HansBrandonChessEngineTest
{
    /// <summary>
    /// Tests for SearchMiniMax with real board.
    /// </summary>
    
    [TestClass]
    public class SearchMinimaxTestWithRealBoard
    {
        Board _board;
        IEvaluator _evaluator;
        MoveGenerator _gen;

        // ---------------------------------------------------------------------------------------------
        // White is first mover
        // ---------------------------------------------------------------------------------------------

        [TestInitialize]
        public void Setup()
        {
            _board = new Board(new Mock<IHashtable>().Object);
            _evaluator = new EvaluatorSimple(_board);
            _gen = new MoveGenerator(_board);
        }

        [TestMethod]
        public void SearchLevelTest_WhenLevelOne_ThenCapturePawn_White()
        {
            var target = new SearchMinimax(_board, _evaluator, _gen, 1);
            string boardString = "k......." +
                                 "........" +
                                 "...p...." +
                                 "....p..." +
                                 ".....B.." +
                                 "........" +
                                 "........" +
                                 "K.......";
            _board.SetPosition(boardString);

            var bestRatingActual = target.SearchLevel(ChessColor.White, 1).First(); // level 1
            IMove goodMove = new NormalMove(Piece.MakePiece(PieceType.Bishop, ChessColor.White), Square.F4,Square.E5, Piece.MakePiece(PieceType.Pawn, ChessColor.Black));

            Assert.AreEqual(goodMove, bestRatingActual.Move, "White bishop should capture pawn. We are on level 1");
            Assert.AreEqual(200, bestRatingActual.Score);
        }

        [TestMethod]
        public void SearchBestMoveOneLevelTest_WhenLevel2_ThenDoNotCapturePawn_White()
        {
            var target = new SearchMinimax(_board, _evaluator, _gen, 2);
            string boardString = "k......." +
                                 "........" +
                                 "...p...." +
                                 "....p..." +
                                 ".....B.." +
                                 "........" +
                                 "........" +
                                 "K.......";
            _board.SetPosition(boardString);

            var bestRatingActual = target.SearchLevel(ChessColor.White, 1).First(); // level 2
            IMove badMove = new NormalMove(Piece.MakePiece(PieceType.Bishop, ChessColor.White), Square.F4, Square.E5, Piece.MakePiece(PieceType.Pawn, ChessColor.Black));

            Assert.AreNotEqual(badMove, bestRatingActual.Move, "White bishop should not capture pawn.");
            Assert.AreEqual(100, bestRatingActual.Score);
        }

        [TestMethod]
        public void SearchBestMoveTest_WhenLevel3_ThenCapturePawn_White()
        {
            var target = new SearchMinimax(_board, _evaluator, _gen, 3);
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

            var bestRatingActual = target.SearchLevel(ChessColor.White, 1).First(); // level 3
            IMove expectedMove = new NormalMove(Piece.MakePiece(PieceType.Bishop, ChessColor.White), Square.F4, Square.E5, Piece.MakePiece(PieceType.Pawn, ChessColor.Black));
            IMove expectedMove2 = new NormalMove(Piece.MakePiece(PieceType.Knight, ChessColor.White), Square.F3, Square.E5, Piece.MakePiece(PieceType.Pawn, ChessColor.Black));

            bool passed = bestRatingActual.Move.Equals(expectedMove) ||
                          bestRatingActual.Move.Equals(expectedMove2);

            Assert.AreEqual(true, passed, "White bishop or knight should capture black pawn. It is actually bad but we test level 3.");
            Assert.AreEqual(0, bestRatingActual.Score);
        }

        [TestMethod]
        public void SearchBestMoveOneLevelTest_WhenLevel4_ThenDoNotCapturePawn_White()
        {
            var target = new SearchMinimax(_board, _evaluator, _gen, 4);
            string boardString = "k......." +
                                 "...n...." +
                                 "...b...." +
                                 "....p..." +
                                 ".....B.." +
                                 ".....N.." +
                                 "........" +
                                 "K.......";
            _board.SetPosition(boardString);

            var bestRatingActual = target.SearchLevel(ChessColor.White, 1).First(); // level 4
            IMove badMove = new NormalMove(Piece.MakePiece(PieceType.Bishop, ChessColor.White), Square.F4, Square.E5, Piece.MakePiece(PieceType.Pawn, ChessColor.Black));
            IMove badMove2 = new NormalMove(Piece.MakePiece(PieceType.Knight, ChessColor.White), Square.F3, Square.E5, Piece.MakePiece(PieceType.Pawn, ChessColor.Black));

            Assert.AreNotEqual(badMove, bestRatingActual.Move, "White bishop or knight should not capture black pawn.");
            Assert.AreNotEqual(badMove2, bestRatingActual.Move, "White bishop or knight should not capture black pawn.");
            Assert.AreEqual(-100, bestRatingActual.Score);
        }

        [TestMethod]
        public void SearchLevelTest_WhenWhiteInCheck_ThenDoNotAttackBlackKing_White()
        {
            var target = new SearchMinimax(_board, _evaluator, _gen, 3);
            string boardString = "....q..R" +
                                 "........" +
                                 "....k..." +
                                 "...b...." +
                                 "....K..." +
                                 "........" +
                                 "........" +
                                 "........";
            _board.SetPosition(boardString);

            var bestRatingActual = target.SearchLevel(ChessColor.White, 1).First(); // level 3
            IMove wrongMove = new NormalMove(Piece.MakePiece(PieceType.Rook, ChessColor.White), Square.H8, Square.E8, Piece.MakePiece(PieceType.Queen, ChessColor.Black));
            IMove wrongMove2 = new NormalMove(Piece.MakePiece(PieceType.King, ChessColor.White), Square.E4, Square.D5, Piece.MakePiece(PieceType.Bishop, ChessColor.Black));

            Assert.AreNotEqual(wrongMove, bestRatingActual.Move, "White must escape check.");
            Assert.AreNotEqual(wrongMove2, bestRatingActual.Move, "White must escape check.");
        }

        [TestMethod]
        public void SearchLevelTest_WhenWhiteIsCheckMate_ThenNoLegalMove_White()
        {
            var target = new SearchMinimax(_board, _evaluator, _gen, 3);
            string boardString = "........" +
                                 "........" +
                                 "........" +
                                 "........" +
                                 "........" +
                                 "...k...." +
                                 "...q...." +
                                 "...K....";
            _board.SetPosition(boardString);

            var bestRatingActual = target.SearchLevel(ChessColor.White, 1).First(); // level 3
            IMove expectedMove = new NoLegalMove();

            Assert.AreEqual(expectedMove, bestRatingActual.Move, "White is check mate. no legal move possible.");
        }

        // ---------------------------------------------------------------------------------------------
        // Black is first mover
        // ---------------------------------------------------------------------------------------------

        [TestMethod]
        public void SearchLevelTest_WhenLevelOne_ThenCapturePawn_Black()
        {
            var target = new SearchMinimax(_board, _evaluator, _gen, 1);
            string boardString = "k......." +
                                 "........" +
                                 ".....b.." +
                                 "....P..." +
                                 "...P...." +
                                 "........" +
                                 "........" +
                                 "K.......";
            _board.SetPosition(boardString);

            var bestRatingActual = target.SearchLevel(ChessColor.Black, 1).First(); // level 1
            IMove goodMove = new NormalMove(Piece.MakePiece(PieceType.Bishop, ChessColor.Black), Square.F6, Square.E5, Piece.MakePiece(PieceType.Pawn, ChessColor.White));

            Assert.AreEqual(goodMove, bestRatingActual.Move, "Black bishop should capture pawn. We are on level 1");
            Assert.AreEqual(-200, bestRatingActual.Score);
        }

        [TestMethod]
        public void SearchBestMoveOneLevelTest_WhenLevel2_ThenDoNotCapturePawn_Black()
        {
            var target = new SearchMinimax(_board, _evaluator, _gen, 2);
            string boardString = "k......." +
                                 "........" +
                                 ".....b.." +
                                 "....P..." +
                                 "...P...." +
                                 "........" +
                                 "........" +
                                 "K.......";
            _board.SetPosition(boardString);

            var bestRatingActual = target.SearchLevel(ChessColor.Black, 1).First(); // level 2
            IMove badMove = new NormalMove(Piece.MakePiece(PieceType.Bishop, ChessColor.Black), Square.F6, Square.E5, Piece.MakePiece(PieceType.Pawn, ChessColor.White));

            Assert.AreNotEqual(badMove, bestRatingActual.Move, "Black bishop should not capture pawn.");
            Assert.AreEqual(-100, bestRatingActual.Score);
        }

        [TestMethod]
        public void SearchBestMoveOneLevelTest_WhenLevel3_ThenCapturePawn_Black()
        {
            var target = new SearchMinimax(_board, _evaluator, _gen, 3);
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

            var bestRatingActual = target.SearchLevel(ChessColor.Black, 1).First(); // level 3
            IMove expectedMove = new NormalMove(Piece.MakePiece(PieceType.Bishop, ChessColor.Black), Square.D6, Square.E5, Piece.MakePiece(PieceType.Pawn, ChessColor.White));
            IMove expectedMove2 = new NormalMove(Piece.MakePiece(PieceType.Knight, ChessColor.Black), Square.D7, Square.E5, Piece.MakePiece(PieceType.Pawn, ChessColor.White));

            bool passed = bestRatingActual.Move.Equals(expectedMove) ||
                          bestRatingActual.Move.Equals(expectedMove2);

            Assert.AreEqual(true, passed, "Black bishop or knight should capture white pawn. It is actually bad but we test level 3.");
            Assert.AreEqual(0, bestRatingActual.Score);
        }

        [TestMethod]
        public void SearchBestMoveOneLevelTest_WhenLevel4_ThenDoNotCapturePawn_Black()
        {
            var target = new SearchMinimax(_board, _evaluator, _gen, 4);
            string boardString = "k......." +
                                 "...n...." +
                                 "...b...." +
                                 "....P..." +
                                 ".....B.." +
                                 ".....N.." +
                                 "........" +
                                 "K.......";
            _board.SetPosition(boardString);

            var bestRatingActual = target.SearchLevel(ChessColor.Black, 1).First(); // level 4
            IMove badMove = new NormalMove(Piece.MakePiece(PieceType.Bishop, ChessColor.Black), Square.D6, Square.E5, Piece.MakePiece(PieceType.Pawn, ChessColor.White));
            IMove badMove2 = new NormalMove(Piece.MakePiece(PieceType.Knight, ChessColor.Black), Square.D7, Square.E5, Piece.MakePiece(PieceType.Pawn, ChessColor.White));

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
            var target = new SearchMinimax(_board, _evaluator, _gen, 2);
            string boardString = "........" +
                                 "........" +
                                 "........" +
                                 "........" +
                                 "........" +
                                 "..k....." +
                                 ".r......" +
                                 "K.......";
            _board.SetPosition(boardString);

            var bestRatingActual = target.SearchLevel(ChessColor.White, 1).First();

            AssertHelper.StallMate(bestRatingActual);
            Assert.AreEqual(new NoLegalMove(), bestRatingActual.Move, "Should be NoLegalMove, white is stalemate");
        }

        [TestMethod]
        public void SearchBestMoveTest_WhenWhiteCheckmate_ThenNoLegalMoveAndBestScoreMinusThousand()
        {
            var target = new SearchMinimax(_board, _evaluator, _gen, 2);
            string boardString = "........" +
                                 "........" +
                                 "........" +
                                 "........" +
                                 "........" +
                                 "..k....." +
                                 ".q......" +
                                 "K.......";
            _board.SetPosition(boardString);

            var bestRatingActual = target.SearchLevel(ChessColor.White, 1).First();

            Assert.AreEqual(new NoLegalMove(), bestRatingActual.Move, "Should be NoLegalMove, white is checkmate");
            AssertHelper.BlackWins(bestRatingActual);
        }

        [TestMethod]
        public void SearchBestMoveTest_WhenBlackIsCheckmate_ThenNoLegalMoveAndBestScoreThousand()
        {
            var target = new SearchMinimax(_board, _evaluator, _gen, 2);
            string boardString = ".rbqkb.r" +
                                 "pppppBpp" +
                                 "..n....." +
                                 "......N." +
                                 "........" +
                                 "........" +
                                 "PPPP.PPP" +
                                 "R.BnK..R";
            _board.SetPosition(boardString);

            var bestRatingActual = target.SearchLevel(ChessColor.Black, 1).First();

            Assert.AreEqual(new NoLegalMove(), bestRatingActual.Move, "Should be NoLegalMove, Black is checkmate");
            AssertHelper.WhiteWins(bestRatingActual);
        }

        [TestMethod]
        public void SearchBestMoveTest_WhenWhiteIsCheckmateIn2_ThenWinningMove()
        {
            var target = new SearchMinimax(_board, _evaluator, _gen, 4);
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

            IMove expectedMove = new NormalMove(Piece.MakePiece(PieceType.Queen, ChessColor.Black), Square.H2, Square.B2, null);

            var bestRatingActual = target.SearchLevel(ChessColor.Black, 1).First();

            Assert.AreEqual(expectedMove, bestRatingActual.Move, "Should be find checkmate for black: ... Qh2b2 #");
            AssertHelper.BlackWins(bestRatingActual);
        }
    }
}
