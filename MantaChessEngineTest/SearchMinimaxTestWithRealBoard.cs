using System.Linq;
using System.Collections.Generic;
using MantaChessEngine;
using MantaChessEngine.Doubles;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MantaChessEngineTest
{
    /// <summary>
    /// Tests for SearchMiniMax with real board.
    /// </summary>
    
    [TestClass]
    public class SearchMinimaxTestWithRealBoard
    {
        // ---------------------------------------------------------------------------------------------
        // White is first mover
        // ---------------------------------------------------------------------------------------------

        [TestMethod]
        public void SearchLevelTest_WhenLevelOne_ThenCapturePawn_White()
        {
            IEvaluator evaluator = new EvaluatorSimple();
            
            MoveGenerator gen = new MoveGenerator(new MoveFactory());
            var target = new SearchMinimax(evaluator, gen, 1);
            var board = new Board();
            string boardString = "k......." +
                                 "........" +
                                 "...p...." +
                                 "....p..." +
                                 ".....B.." +
                                 "........" +
                                 "........" +
                                 "K.......";
            board.SetPosition(boardString);

            var bestRatingActual = target.SearchLevel(board, Definitions.ChessColor.White, 1).First(); // level 1
            IMove goodMove = new NormalMove(Piece.MakePiece('B'), 'f', 4, 'e', 5, Piece.MakePiece('p'));

            Assert.AreEqual(goodMove, bestRatingActual.Move, "White bishop should capture pawn. We are on level 1");
            Assert.AreEqual(2, bestRatingActual.Score);
        }

        [TestMethod]
        public void SearchBestMoveOneLevelTest_WhenLevel2_ThenDoNotCapturePawn_White()
        {
            IEvaluator evaluator = new EvaluatorSimple();
            MoveGenerator gen = new MoveGenerator(new MoveFactory());
            var target = new SearchMinimax(evaluator, gen, 2);
            var board = new Board();
            string boardString = "k......." +
                                 "........" +
                                 "...p...." +
                                 "....p..." +
                                 ".....B.." +
                                 "........" +
                                 "........" +
                                 "K.......";
            board.SetPosition(boardString);

            var bestRatingActual = target.SearchLevel(board, Definitions.ChessColor.White, 1).First(); // level 2
            IMove badMove = new NormalMove(Piece.MakePiece('B'), 'f', 4, 'e', 5, Piece.MakePiece('p'));

            Assert.AreNotEqual(badMove, bestRatingActual.Move, "White bishop should not capture pawn.");
            Assert.AreEqual(1, bestRatingActual.Score);
        }

        [TestMethod]
        public void SearchBestMoveTest_WhenLevel3_ThenCapturePawn_White()
        {
            IEvaluator evaluator = new EvaluatorSimple();
            MoveGenerator gen = new MoveGenerator(new MoveFactory());
            var target = new SearchMinimax(evaluator, gen, 3);
            var board = new Board();
            string boardString = "k......." +
                                 "...n...." +
                                 "...b...." +
                                 "....p..." +
                                 ".....B.." +
                                 ".....N.." +
                                 "........" +
                                 "K.......";
            board.SetPosition(boardString);

                                 //"k......." +   After position
                                 //"...n...." +
                                 //"........" +
                                 //"....N..." +   can also be B
                                 //"........" +
                                 //"........" +
                                 //"........" +
                                 //"K.......";

            var bestRatingActual = target.SearchLevel(board, Definitions.ChessColor.White, 1).First(); // level 3
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
            IEvaluator evaluator = new EvaluatorSimple();
            MoveGenerator gen = new MoveGenerator(new MoveFactory());
            var target = new SearchMinimax(evaluator, gen, 4);
            var board = new Board();
            string boardString = "k......." +
                                 "...n...." +
                                 "...b...." +
                                 "....p..." +
                                 ".....B.." +
                                 ".....N.." +
                                 "........" +
                                 "K.......";
            board.SetPosition(boardString);

            var bestRatingActual = target.SearchLevel(board, Definitions.ChessColor.White, 1).First(); // level 4
            IMove badMove = new NormalMove(Piece.MakePiece('B'), 'f', 4, 'e', 5, Piece.MakePiece('p'));
            IMove badMove2 = new NormalMove(Piece.MakePiece('N'), 'f', 3, 'e', 5, Piece.MakePiece('p'));

            Assert.AreNotEqual(badMove, bestRatingActual.Move, "White bishop or knight should not capture black pawn.");
            Assert.AreNotEqual(badMove2, bestRatingActual.Move, "White bishop or knight should not capture black pawn.");
            Assert.AreEqual(-1, bestRatingActual.Score);
        }

        [TestMethod]
        public void SearchLevelTest_WhenWhiteInCheck_ThenDoNotAttackBlackKing_White()
        {
            IEvaluator evaluator = new EvaluatorSimple();
            MoveGenerator gen = new MoveGenerator(new MoveFactory());
            var target = new SearchMinimax(evaluator, gen, 3);
            var board = new Board();
            string boardString = "....q..R" +
                                 "........" +
                                 "....k..." +
                                 "...b...." +
                                 "....K..." +
                                 "........" +
                                 "........" +
                                 "........";
            board.SetPosition(boardString);

            var bestRatingActual = target.SearchLevel(board, Definitions.ChessColor.White, 1).First(); // level 3
            IMove wrongMove = new NormalMove(Piece.MakePiece('R'), 'h', 8, 'e', 8, Piece.MakePiece('q'));
            IMove wrongMove2 = new NormalMove(Piece.MakePiece('K'), 'e', 4, 'd', 5, Piece.MakePiece('b'));

            Assert.AreNotEqual(wrongMove, bestRatingActual.Move, "White must escape check.");
            Assert.AreNotEqual(wrongMove2, bestRatingActual.Move, "White must escape check.");
        }

        [TestMethod]
        public void SearchLevelTest_WhenWhiteIsCheckMate_ThenNoLegalMove_White()
        {
            IEvaluator evaluator = new EvaluatorSimple();
            MoveGenerator gen = new MoveGenerator(new MoveFactory());
            var target = new SearchMinimax(evaluator, gen, 3);
            var board = new Board();
            string boardString = "........" +
                                 "........" +
                                 "........" +
                                 "........" +
                                 "........" +
                                 "...k...." +
                                 "...q...." +
                                 "...K....";
            board.SetPosition(boardString);

            var bestRatingActual = target.SearchLevel(board, Definitions.ChessColor.White, 1).First(); // level 3
            IMove expectedMove = new NoLegalMove();

            Assert.AreEqual(expectedMove, bestRatingActual.Move, "White is check mate. no legal move possible.");
        }

        // ---------------------------------------------------------------------------------------------
        // Black is first mover
        // ---------------------------------------------------------------------------------------------

        [TestMethod]
        public void SearchLevelTest_WhenLevelOne_ThenCapturePawn_Black()
        {
            IEvaluator evaluator = new EvaluatorSimple();
            MoveGenerator gen = new MoveGenerator(new MoveFactory());
            var target = new SearchMinimax(evaluator, gen, 1);
            var board = new Board();
            string boardString = "k......." +
                                 "........" +
                                 ".....b.." +
                                 "....P..." +
                                 "...P...." +
                                 "........" +
                                 "........" +
                                 "K.......";
            board.SetPosition(boardString);

            var bestRatingActual = target.SearchLevel(board, Definitions.ChessColor.Black, 1).First(); // level 1
            IMove goodMove = new NormalMove(Piece.MakePiece('b'), 'f', 6, 'e', 5, Piece.MakePiece('P'));

            Assert.AreEqual(goodMove, bestRatingActual.Move, "Black bishop should capture pawn. We are on level 1");
            Assert.AreEqual(-2, bestRatingActual.Score);
        }

        [TestMethod]
        public void SearchBestMoveOneLevelTest_WhenLevel2_ThenDoNotCapturePawn_Black()
        {
            IEvaluator evaluator = new EvaluatorSimple();
            MoveGenerator gen = new MoveGenerator(new MoveFactory());
            var target = new SearchMinimax(evaluator, gen, 2);
            var board = new Board();
            string boardString = "k......." +
                                 "........" +
                                 ".....b.." +
                                 "....P..." +
                                 "...P...." +
                                 "........" +
                                 "........" +
                                 "K.......";
            board.SetPosition(boardString);

            var bestRatingActual = target.SearchLevel(board, Definitions.ChessColor.Black, 1).First(); // level 2
            IMove badMove = new NormalMove(Piece.MakePiece('b'), 'f', 6, 'e', 5, Piece.MakePiece('P'));

            Assert.AreNotEqual(badMove, bestRatingActual.Move, "Black bishop should not capture pawn.");
            Assert.AreEqual(-1, bestRatingActual.Score);
        }

        [TestMethod]
        public void SearchBestMoveOneLevelTest_WhenLevel3_ThenCapturePawn_Black()
        {
            IEvaluator evaluator = new EvaluatorSimple();
            MoveGenerator gen = new MoveGenerator(new MoveFactory());
            var target = new SearchMinimax(evaluator, gen, 3);
            var board = new Board();
            string boardString = "k......." +
                                 "...n...." +
                                 "...b...." +
                                 "....P..." +
                                 ".....B.." +
                                 ".....N.." +
                                 "........" +
                                 "K.......";
            board.SetPosition(boardString);

            //"k......." +   After position
            //"........" +
            //"........" +
            //"....n..." +   can also be b
            //"........" +
            //".....N.." +
            //"........" +
            //"K.......";

            var bestRatingActual = target.SearchLevel(board, Definitions.ChessColor.Black, 1).First(); // level 3
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
            IEvaluator evaluator = new EvaluatorSimple();
            MoveGenerator gen = new MoveGenerator(new MoveFactory());
            var target = new SearchMinimax(evaluator, gen, 4);
            var board = new Board();
            string boardString = "k......." +
                                 "...n...." +
                                 "...b...." +
                                 "....P..." +
                                 ".....B.." +
                                 ".....N.." +
                                 "........" +
                                 "K.......";
            board.SetPosition(boardString);

            var bestRatingActual = target.SearchLevel(board, Definitions.ChessColor.Black, 1).First(); // level 4
            IMove badMove = new NormalMove(Piece.MakePiece('b'), 'd', 6, 'e', 5, Piece.MakePiece('P'));
            IMove badMove2 = new NormalMove(Piece.MakePiece('n'), 'd', 7, 'e', 5, Piece.MakePiece('P'));

            Assert.AreNotEqual(badMove, bestRatingActual.Move, "Black bishop or knight should not capture white pawn.");
            Assert.AreNotEqual(badMove2, bestRatingActual.Move, "Black bishop or knight should not capture white pawn.");
            Assert.AreEqual(1, bestRatingActual.Score);
        }

        // ---------------------------------------------------------------------------------------------
        // White is stall mate and check mate
        // ---------------------------------------------------------------------------------------------

        [TestMethod]
        public void SearchBestMoveTest_WhenWhiteStallmate_ThenNoLegalMoveAndBestScore0()
        {
            IEvaluator evaluator = new EvaluatorSimple();
            MoveGenerator gen = new MoveGenerator(new MoveFactory());
            var target = new SearchMinimax(evaluator, gen, 2);
            var board = new Board();
            string boardString = "........" +
                                 "........" +
                                 "........" +
                                 "........" +
                                 "........" +
                                 "..k....." +
                                 ".r......" +
                                 "K.......";
            board.SetPosition(boardString);

            var bestRatingActual = target.SearchLevel(board, Definitions.ChessColor.White, 1).First();

            Assert.AreEqual(0, bestRatingActual.Score, "stalemate score should be 0");
            Assert.AreEqual(new NoLegalMove(), bestRatingActual.Move, "Should be NoLegalMove, white is stalemate");
        }

        [TestMethod]
        public void SearchBestMoveTest_WhenWhiteCheckmate_ThenNoLegalMoveAndBestScoreMinusThousand()
        {
            IEvaluator evaluator = new EvaluatorSimple();
            MoveGenerator gen = new MoveGenerator(new MoveFactory());
            var target = new SearchMinimax(evaluator, gen, 2);
            var board = new Board();
            string boardString = "........" +
                                 "........" +
                                 "........" +
                                 "........" +
                                 "........" +
                                 "..k....." +
                                 ".q......" +
                                 "K.......";
            board.SetPosition(boardString);

            var bestRatingActual = target.SearchLevel(board, Definitions.ChessColor.White, 1).First();

            Assert.AreEqual(new NoLegalMove(), bestRatingActual.Move, "Should be NoLegalMove, white is checkmate");
            Assert.AreEqual(-10000, bestRatingActual.Score, "check mate score should be -10000");
        }

        [TestMethod]
        public void SearchBestMoveTest_WhenBlackIsCheckmate_ThenNoLegalMoveAndBestScoreThousand()
        {
            IEvaluator evaluator = new EvaluatorSimple();
            MoveGenerator gen = new MoveGenerator(new MoveFactory());
            var target = new SearchMinimax(evaluator, gen, 2);
            var board = new Board();
            string boardString = ".rbqkb.r" +
                                 "pppppBpp" +
                                 "..n....." +
                                 "......N." +
                                 "........" +
                                 "........" +
                                 "PPPP.PPP" +
                                 "R.BnK..R";
            board.SetPosition(boardString);

            var bestRatingActual = target.SearchLevel(board, Definitions.ChessColor.Black, 1).First();

            Assert.AreEqual(new NoLegalMove(), bestRatingActual.Move, "Should be NoLegalMove, Black is checkmate");
            Assert.AreEqual(10000, bestRatingActual.Score, "check mate score should be 10000");
        }

        [TestMethod]
        public void SearchBestMoveTest_WhenWhiteIsCheckmateIn2_ThenWinningMove()
        {
            IEvaluator evaluator = new EvaluatorSimple();
            MoveGenerator gen = new MoveGenerator(new MoveFactory());
            var target = new SearchMinimax(evaluator, gen, 4);
            target.SetMaxDepth(4);
            var board = new Board();
            string boardString = "........" +
                                 "........" +
                                 "........" +
                                 "........" +
                                 "........" +
                                 "..k....." +
                                 ".......q" +
                                 "K.......";
            board.SetPosition(boardString);

            IMove expectedMove = new NormalMove(Piece.MakePiece('q'), 'h', 2, 'b', 2, null);

            var bestRatingActual = target.SearchLevel(board, Definitions.ChessColor.Black, 1).First();

            Assert.AreEqual(expectedMove, bestRatingActual.Move, "Should be find checkmate for black: ... Qh2b2 #");
            Assert.AreEqual(-10000, bestRatingActual.Score, "check mate score should be -10000");
        }
    }
}
