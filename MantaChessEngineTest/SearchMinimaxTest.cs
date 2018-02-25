using System;
using MantaChessEngine;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MantaChessEngineTest
{
    /// <summary>
    /// class is a copy of SearchMinimaxTest
    /// </summary>
    
    [TestClass]
    public class SearchMinimaxTest
    {
        // ---------------------------------------------------------------------------------------------
        // White is first mover
        // ---------------------------------------------------------------------------------------------

        [TestMethod]
        public void SearchLevelTest_WhenLevelOne_ThenCapturePawn_White()
        {
            IEvaluator evaluator = new EvaluatorSimple();
            
            MoveGenerator gen = new MoveGenerator(new MoveFactory());
            var target = new SearchMinimax(evaluator, gen);
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

            float score = 0;
            IMove actualMove = target.SearchLevel(board, Definitions.ChessColor.White, 1, out score); // level 1
            IMove goodMove = new NormalMove(Piece.MakePiece('B'), 'f', 4, 'e', 5, Piece.MakePiece('p'));

            Assert.AreEqual(goodMove, actualMove, "White bishop should capture pawn. We are on level 1");
            Assert.AreEqual(2, score);
        }

        [TestMethod]
        public void SearchBestMoveOneLevelTest_WhenLevel2_ThenDoNotCapturePawn_White()
        {
            IEvaluator evaluator = new EvaluatorSimple();
            MoveGenerator gen = new MoveGenerator(new MoveFactory());
            var target = new SearchMinimax(evaluator, gen);
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

            float score = 0;
            IMove actualMove = target.SearchLevel(board, Definitions.ChessColor.White, 2, out score); // level 2
            IMove badMove = new NormalMove(Piece.MakePiece('B'), 'f', 4, 'e', 5, Piece.MakePiece('p'));

            Assert.AreNotEqual(badMove, actualMove, "White bishop should not capture pawn.");
            Assert.AreEqual(1, score);
        }

        [TestMethod]
        public void SearchBestMoveOneLevelTest_WhenLevel3_ThenCapturePawn_White()
        {
            IEvaluator evaluator = new EvaluatorSimple();
            MoveGenerator gen = new MoveGenerator(new MoveFactory());
            var target = new SearchMinimax(evaluator, gen);
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

            float score = 0;
            IMove actualMove = target.SearchLevel(board, Definitions.ChessColor.White, 3, out score); // level 3
            IMove expectedMove = new NormalMove(Piece.MakePiece('B'), 'f', 4, 'e', 5, Piece.MakePiece('p'));
            IMove expectedMove2 = new NormalMove(Piece.MakePiece('N'), 'f', 3, 'e', 5, Piece.MakePiece('p'));

            bool passed = actualMove.Equals(expectedMove) ||
                          actualMove.Equals(expectedMove2);

            Assert.AreEqual(true, passed, "White bishop or knight should capture black pawn. It is actually bad but we test level 3.");
            Assert.AreEqual(0, score);
        }

        [TestMethod]
        public void SearchBestMoveOneLevelTest_WhenLevel4_ThenDoNotCapturePawn_White()
        {
            IEvaluator evaluator = new EvaluatorSimple();
            MoveGenerator gen = new MoveGenerator(new MoveFactory());
            var target = new SearchMinimax(evaluator, gen);
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

            float score = 0;
            IMove actualMove = target.SearchLevel(board, Definitions.ChessColor.White, 4, out score); // level 4
            IMove badMove = new NormalMove(Piece.MakePiece('B'), 'f', 4, 'e', 5, Piece.MakePiece('p'));
            IMove badMove2 = new NormalMove(Piece.MakePiece('N'), 'f', 3, 'e', 5, Piece.MakePiece('p'));

            Assert.AreNotEqual(badMove, actualMove, "White bishop or knight should not capture black pawn.");
            Assert.AreNotEqual(badMove2, actualMove, "White bishop or knight should not capture black pawn.");
            Assert.AreEqual(-1, score);
        }

        [TestMethod]
        public void SearchLevelTest_WhenWhiteInCheck_ThenDoNotAttackBlackKing_White()
        {
            IEvaluator evaluator = new EvaluatorSimple();
            MoveGenerator gen = new MoveGenerator(new MoveFactory());
            var target = new SearchMinimax(evaluator, gen);
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

            float score = 0;
            IMove actualMove = target.SearchLevel(board, Definitions.ChessColor.White, 3, out score); // level 3
            IMove wrongMove = new NormalMove(Piece.MakePiece('R'), 'h', 8, 'e', 8, Piece.MakePiece('q'));
            IMove wrongMove2 = new NormalMove(Piece.MakePiece('K'), 'e', 4, 'd', 5, Piece.MakePiece('b'));

            Assert.AreNotEqual(wrongMove, actualMove, "White must escape check.");
            Assert.AreNotEqual(wrongMove2, actualMove, "White must escape check.");
        }

        [TestMethod]
        public void SearchLevelTest_WhenWhiteIsCheckMate_ThenNoLegalMove_White()
        {
            IEvaluator evaluator = new EvaluatorSimple();
            MoveGenerator gen = new MoveGenerator(new MoveFactory());
            var target = new SearchMinimax(evaluator, gen);
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

            float score = 0;
            IMove actualMove = target.SearchLevel(board, Definitions.ChessColor.White, 3, out score); // level 3
            IMove expectedMove = new NoLegalMove();

            Assert.AreEqual(expectedMove, actualMove, "White is check mate. no legal move possible.");
        }

        // ---------------------------------------------------------------------------------------------
        // Black is first mover
        // ---------------------------------------------------------------------------------------------

        [TestMethod]
        public void SearchLevelTest_WhenLevelOne_ThenCapturePawn_Black()
        {
            IEvaluator evaluator = new EvaluatorSimple();
            MoveGenerator gen = new MoveGenerator(new MoveFactory());
            var target = new SearchMinimax(evaluator, gen);
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

            float score = 0;
            IMove actualMove = target.SearchLevel(board, Definitions.ChessColor.Black, 1, out score); // level 1
            IMove goodMove = new NormalMove(Piece.MakePiece('b'), 'f', 6, 'e', 5, Piece.MakePiece('P'));

            Assert.AreEqual(goodMove, actualMove, "Black bishop should capture pawn. We are on level 1");
            Assert.AreEqual(-2, score);
        }

        [TestMethod]
        public void SearchBestMoveOneLevelTest_WhenLevel2_ThenDoNotCapturePawn_Black()
        {
            IEvaluator evaluator = new EvaluatorSimple();
            MoveGenerator gen = new MoveGenerator(new MoveFactory());
            var target = new SearchMinimax(evaluator, gen);
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

            float score = 0;
            IMove actualMove = target.SearchLevel(board, Definitions.ChessColor.Black, 2, out score); // level 2
            IMove badMove = new NormalMove(Piece.MakePiece('b'), 'f', 6, 'e', 5, Piece.MakePiece('P'));

            Assert.AreNotEqual(badMove, actualMove, "Black bishop should not capture pawn.");
            Assert.AreEqual(-1, score);
        }

        [TestMethod]
        public void SearchBestMoveOneLevelTest_WhenLevel3_ThenCapturePawn_Black()
        {
            IEvaluator evaluator = new EvaluatorSimple();
            MoveGenerator gen = new MoveGenerator(new MoveFactory());
            var target = new SearchMinimax(evaluator, gen);
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

            float score = 0;
            IMove actualMove = target.SearchLevel(board, Definitions.ChessColor.Black, 3, out score); // level 3
            IMove expectedMove = new NormalMove(Piece.MakePiece('b'), 'd', 6, 'e', 5, Piece.MakePiece('P'));
            IMove expectedMove2 = new NormalMove(Piece.MakePiece('n'), 'd', 7, 'e', 5, Piece.MakePiece('P'));

            bool passed = actualMove.Equals(expectedMove) ||
                          actualMove.Equals(expectedMove2);

            Assert.AreEqual(true, passed, "Black bishop or knight should capture white pawn. It is actually bad but we test level 3.");
            Assert.AreEqual(0, score);
        }

        [TestMethod]
        public void SearchBestMoveOneLevelTest_WhenLevel4_ThenDoNotCapturePawn_Black()
        {
            IEvaluator evaluator = new EvaluatorSimple();
            MoveGenerator gen = new MoveGenerator(new MoveFactory());
            var target = new SearchMinimax(evaluator, gen);
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

            float score = 0;
            IMove actualMove = target.SearchLevel(board, Definitions.ChessColor.Black, 4, out score); // level 4
            IMove badMove = new NormalMove(Piece.MakePiece('b'), 'd', 6, 'e', 5, Piece.MakePiece('P'));
            IMove badMove2 = new NormalMove(Piece.MakePiece('n'), 'd', 7, 'e', 5, Piece.MakePiece('P'));

            Assert.AreNotEqual(badMove, actualMove, "Black bishop or knight should not capture white pawn.");
            Assert.AreNotEqual(badMove2, actualMove, "Black bishop or knight should not capture white pawn.");
            Assert.AreEqual(1, score);
        }

        // ---------------------------------------------------------------------------------------------
        // White is stall mate
        // ---------------------------------------------------------------------------------------------

        [Ignore]
        [TestMethod]
        public void SearchBestMoveTest_WhenWhiteStallmate_ThenNoLegalMoveAndBestScore0()
        {
            IEvaluator evaluator = new EvaluatorSimple();
            MoveGenerator gen = new MoveGenerator(new MoveFactory());
            var target = new SearchMinimax(evaluator, gen);
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

            float score = 0;
            IMove actualMove = target.SearchLevel(board, Definitions.ChessColor.White, 1, out score);

            Assert.AreEqual(0, score, "stalemate score should be 0");
            Assert.AreEqual(new NoLegalMove(), actualMove, "Should be NoLegalMove, white is stalemate");
        }
    }
}
