using System.Linq;
using System.Collections.Generic;
using MantaBitboardEngine;
using MantaCommon;
using MantaChessEngineTest.Doubles;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace MantaBitboardEngineTest
{
    /// <summary>
    /// Tests for SearchAlphaBeta with real board.
    /// </summary>
    
    [TestClass]
    public class GenericSearchAlphaBetaTestWithRealBoard
    {
        const int AlphaStart = int.MinValue;
        const int BetaStart = int.MaxValue;

        IHashtable _hashMock;
        Bitboards _board;
        IEvaluator _evaluator;
        BitMoveGenerator _gen;
        BitMoveRatingFactory _ratingFactory;
        
        [TestInitialize]
        public void Setup()
        {
            var helperBits = new HelperBitboards();
            _hashMock = new Mock<IHashtable>().Object;
            _board = new Bitboards(_hashMock);
            _evaluator = new BitEvaluatorSimple(_board, helperBits);
            _gen = new BitMoveGenerator(_board, helperBits);
            _ratingFactory = new BitMoveRatingFactory(_gen);
        }

        // ---------------------------------------------------------------------------------------------
        // White is first mover
        // ---------------------------------------------------------------------------------------------

        [TestMethod]
        public void SearchLevelTest_WhenLevelOne_ThenCapturePawn_White()
        {
            var target = new GenericSearchAlphaBeta<BitMove>(_board, _evaluator, _gen, _hashMock, null, _ratingFactory, 1);
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
            var goodMove = BitMove.CreateCapture(BitPieceType.Bishop, Square.F4, Square.E5, BitPieceType.Pawn, Square.E5, BitPieceType.Empty, ChessColor.White, 0);

            Assert.AreEqual(goodMove, bestRatingActual.Move, "White bishop should capture pawn. We are on level 1");
            Assert.AreEqual(200, bestRatingActual.Score);
        }
        
        [TestMethod]
        public void SearchBestMoveOneLevelTest_WhenLevel2_ThenDoNotCapturePawn_White()
        {
            var target = new GenericSearchAlphaBeta<BitMove>(_board, _evaluator, _gen, _hashMock, null, _ratingFactory, 2);
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
            var badMove = BitMove.CreateCapture(BitPieceType.Bishop, Square.F4, Square.E5, BitPieceType.Pawn, Square.E5, BitPieceType.Empty, ChessColor.White, 0);

            Assert.AreNotEqual(badMove, bestRatingActual.Move, "White bishop should not capture pawn.");
            Assert.AreEqual(100, bestRatingActual.Score);
        }
        
        [TestMethod]
        public void SearchBestMoveTest_WhenLevel3_ThenCapturePawn_White()
        {
            var target = new GenericSearchAlphaBeta<BitMove>(_board, _evaluator, _gen, _hashMock, null, _ratingFactory, 3);
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

            var expectedMove = BitMove.CreateCapture(BitPieceType.Bishop, Square.F4, Square.E5, BitPieceType.Pawn, Square.E5, BitPieceType.Empty, ChessColor.White, 0);
            var expectedMove2 = BitMove.CreateCapture(BitPieceType.Knight, Square.F3, Square.E5, BitPieceType.Pawn, Square.E5, BitPieceType.Empty, ChessColor.White, 0);

            bool passed = bestRatingActual.Move.Equals(expectedMove) ||
                          bestRatingActual.Move.Equals(expectedMove2);

            Assert.AreEqual(true, passed, "White bishop or knight should capture black pawn. It is actually bad but we test level 3.");
            Assert.AreEqual(0, bestRatingActual.Score);
        }
        
        [TestMethod]
        public void SearchBestMoveOneLevelTest_WhenLevel4_ThenDoNotCapturePawn_White()
        {
            var target = new GenericSearchAlphaBeta<BitMove>(_board, _evaluator, _gen, _hashMock, null, _ratingFactory, 4);
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
            var badMove = BitMove.CreateCapture(BitPieceType.Bishop, Square.F4, Square.E5, BitPieceType.Pawn, Square.E5, BitPieceType.Empty, ChessColor.White, 0);
            var badMove2 = BitMove.CreateCapture(BitPieceType.Knight, Square.F3, Square.E5, BitPieceType.Pawn, Square.E5, BitPieceType.Empty, ChessColor.White, 0);

            Assert.AreNotEqual(badMove, bestRatingActual.Move, "White bishop or knight should not capture black pawn.");
            Assert.AreNotEqual(badMove2, bestRatingActual.Move, "White bishop or knight should not capture black pawn.");
            Assert.AreEqual(-100, bestRatingActual.Score);
        }
        
        [TestMethod]
        public void SearchLevelTest_WhenWhiteInCheck_ThenDoNotAttackBlackKing_White()
        {
            var target = new GenericSearchAlphaBeta<BitMove>(_board, _evaluator, _gen, _hashMock, null, _ratingFactory, 3);
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

            var wrongMove = BitMove.CreateCapture(BitPieceType.Rook, Square.H8, Square.E8, BitPieceType.Queen, Square.E8, BitPieceType.Empty, ChessColor.White, 0);
            var wrongMove2 = BitMove.CreateCapture(BitPieceType.King, Square.E4, Square.D5, BitPieceType.Bishop, Square.E8, BitPieceType.Empty, ChessColor.White, 0);

            Assert.AreNotEqual(wrongMove, bestRatingActual.Move, "White must escape check.");
            Assert.AreNotEqual(wrongMove2, bestRatingActual.Move, "White must escape check.");
        }
        
        [TestMethod]
        public void SearchLevelTest_WhenWhiteIsCheckMate_ThenNoLegalMove_White()
        {
            var target = new GenericSearchAlphaBeta<BitMove>(_board, _evaluator, _gen, _hashMock, null, _ratingFactory, 3);
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
            var expectedMove = BitMove.CreateEmptyMove();

            Assert.AreEqual(expectedMove, bestRatingActual.Move, "White is check mate. no legal move possible.");
        }

        // ---------------------------------------------------------------------------------------------
        // Black is first mover
        // ---------------------------------------------------------------------------------------------

        [TestMethod]
        public void SearchLevelTest_WhenLevelOne_ThenCapturePawn_Black()
        {
            var target = new GenericSearchAlphaBeta<BitMove>(_board, _evaluator, _gen, _hashMock, null, _ratingFactory, 1);
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
            var goodMove = BitMove.CreateCapture(BitPieceType.Bishop, Square.F6, Square.E5, BitPieceType.Pawn, Square.E5, BitPieceType.Empty, ChessColor.Black, 0);

            Assert.AreEqual(goodMove, bestRatingActual.Move, "Black bishop should capture pawn. We are on level 1");
            Assert.AreEqual(-200, bestRatingActual.Score);
        }

        [TestMethod]
        public void SearchBestMoveOneLevelTest_WhenLevel2_ThenDoNotCapturePawn_Black()
        {
            var target = new GenericSearchAlphaBeta<BitMove>(_board, _evaluator, _gen, _hashMock, null, _ratingFactory, 2);
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
            var badMove = BitMove.CreateCapture(BitPieceType.Bishop, Square.F6, Square.E5, BitPieceType.Pawn, Square.E5, BitPieceType.Empty, ChessColor.Black, 0);

            Assert.AreNotEqual(badMove, bestRatingActual.Move, "Black bishop should not capture pawn.");
            Assert.AreEqual(-100, bestRatingActual.Score);
        }

        [TestMethod]
        public void SearchBestMoveOneLevelTest_WhenLevel3_ThenCapturePawn_Black()
        {
            var target = new GenericSearchAlphaBeta<BitMove>(_board, _evaluator, _gen, _hashMock, null, _ratingFactory, 3);
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
            var expectedMove = BitMove.CreateCapture(BitPieceType.Bishop, Square.D6, Square.E5, BitPieceType.Pawn, Square.E5, BitPieceType.Empty, ChessColor.Black, 0);
            var expectedMove2 = BitMove.CreateCapture(BitPieceType.Knight, Square.D7, Square.E5, BitPieceType.Pawn, Square.E5, BitPieceType.Empty, ChessColor.Black, 0);

            bool passed = bestRatingActual.Move.Equals(expectedMove) ||
                          bestRatingActual.Move.Equals(expectedMove2);

            Assert.AreEqual(true, passed, "Black bishop or knight should capture white pawn. It is actually bad but we test level 3.");
            Assert.AreEqual(0, bestRatingActual.Score);
        }

        [TestMethod]
        public void SearchBestMoveOneLevelTest_WhenLevel4_ThenDoNotCapturePawn_Black()
        {
            var target = new GenericSearchAlphaBeta<BitMove>(_board, _evaluator, _gen, _hashMock, null, _ratingFactory, 4);
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
            var badMove = BitMove.CreateCapture(BitPieceType.Bishop, Square.D6, Square.E5, BitPieceType.Pawn, Square.E5, BitPieceType.Empty, ChessColor.Black, 0);
            var badMove2 = BitMove.CreateCapture(BitPieceType.Knight, Square.D7, Square.E5, BitPieceType.Pawn, Square.E5, BitPieceType.Empty, ChessColor.Black, 0);

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
            var target = new GenericSearchAlphaBeta<BitMove>(_board, _evaluator, _gen, _hashMock, null, _ratingFactory, 2);
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

            AssertHelperBitboard.StallMate(bestRatingActual);
            Assert.AreEqual(BitMove.CreateEmptyMove(), bestRatingActual.Move, "Should be NoLegalMove, white is stalemate");
        }

        [TestMethod]
        public void SearchBestMoveTest_WhenWhiteCheckmate_ThenNoLegalMoveAndBestScoreMinusThousand()
        {
            var target = new GenericSearchAlphaBeta<BitMove>(_board, _evaluator, _gen, _hashMock, null, _ratingFactory, 2);
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

            Assert.AreEqual(BitMove.CreateEmptyMove(), bestRatingActual.Move, "Should be NoLegalMove, white is checkmate");
            AssertHelperBitboard.BlackWins(bestRatingActual);
        }

        [TestMethod]
        public void SearchBestMoveTest_WhenBlackIsCheckmate_ThenNoLegalMoveAndBestScoreThousand()
        {
            var target = new GenericSearchAlphaBeta<BitMove>(_board, _evaluator, _gen, _hashMock, null, _ratingFactory, 2);
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

            Assert.AreEqual(BitMove.CreateEmptyMove(), bestRatingActual.Move, "Should be NoLegalMove, Black is checkmate");
            AssertHelperBitboard.WhiteWins(bestRatingActual);
        }

        [TestMethod]
        public void SearchBestMoveTest_WhenWhiteIsCheckmateIn2_ThenWinningMove()
        {
            var target = new GenericSearchAlphaBeta<BitMove>(_board, _evaluator, _gen, _hashMock, null, _ratingFactory, 4);
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

            var bestRatingActual = target.SearchLevel(ChessColor.Black, 1, AlphaStart, BetaStart);

            var expectedMove = BitMove.CreateMove(BitPieceType.Queen, Square.H2, Square.B2, BitPieceType.Empty, ChessColor.Black, 0);

            Assert.AreEqual(expectedMove, bestRatingActual.Move, "Should be find checkmate for black: ... Qh2b2 #");
            AssertHelperBitboard.BlackWins(bestRatingActual);
        }
    }
}
