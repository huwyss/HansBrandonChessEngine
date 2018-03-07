using System;
using System.Collections.Generic;
using MantaChessEngine;
using MantaChessEngine.Doubles;
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
        // White is stall mate and check mate
        // ---------------------------------------------------------------------------------------------

        

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

            float score = 0 ;
            IMove actualMove = target.SearchLevel(board, Definitions.ChessColor.White, 2, out score);

            Assert.AreEqual(0, score, "stalemate score should be 0");
            Assert.AreEqual(new NoLegalMove(), actualMove, "Should be NoLegalMove, white is stalemate");
        }

        [TestMethod]
        public void SearchBestMoveTest_WhenWhiteCheckmate_ThenNoLegalMoveAndBestScoreMinusThousand()
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
                                 ".q......" +
                                 "K.......";
            board.SetPosition(boardString);

            float score;
            IMove actualMove = target.SearchLevel(board, Definitions.ChessColor.White, 2, out score);

            Assert.AreEqual(new NoLegalMove(), actualMove, "Should be NoLegalMove, white is stalemate");
            Assert.AreEqual(-10000, score, "check mate score should be -10000");
        }

        // ---------------------------------------------------------------------------------------------
        // Search Test with Mocks
        // ---------------------------------------------------------------------------------------------

        // Depth 1

        [TestMethod]
        public void SearchMinimaxTest_WhenWhiteMovesFirstAnd1Level_ThenCorrectSelected()
        {
            //      start
            //     /      \
            //    1       2x    white move -> highest selected (x) 
            IEvaluator evalFake = new FakeEvaluator(new List<float>() { 1, 2 });
            FakeMoveGeneratorMulitlevel moveGenFake = new FakeMoveGeneratorMulitlevel();
            IMove bestFakeMove = new NormalMove(Piece.MakePiece('Q'), 0, 0, 0, 0, null);
            moveGenFake.AddGetAllMoves(new List<IMove>() { new NormalMove(Piece.MakePiece('P'), 0, 0, 0, 0, null), bestFakeMove }); // first level

            IBoard boardFake = new FakeBoard();

            var target = new SearchMinimax(evalFake, moveGenFake);
            float scoreActual;
            IMove bestMoveActual = target.SearchLevel(boardFake, Definitions.ChessColor.White, 1, out scoreActual);

            Assert.AreEqual(2, scoreActual);
            Assert.AreEqual(bestFakeMove, bestMoveActual);
        }

        [TestMethod]
        public void SearchMinimaxTest_WhenBlackMovesFirstAnd1Level_ThenCorrectSelected()
        {
            //      start
            //     /      \
            //    1x       2    black move -> lowest selected (x) 
            IEvaluator evalFake = new FakeEvaluator(new List<float>() { 1, 2 });
            FakeMoveGeneratorMulitlevel moveGenFake = new FakeMoveGeneratorMulitlevel();
            IMove bestFakeMove = new NormalMove(Piece.MakePiece('q'), 0, 0, 0, 0, null);
            moveGenFake.AddGetAllMoves(new List<IMove>() { bestFakeMove, new NormalMove(Piece.MakePiece('p'), 0, 0, 0, 0, null) }); // first level

            IBoard boardFake = new FakeBoard();

            var target = new SearchMinimax(evalFake, moveGenFake);
            float scoreActual;
            IMove bestMoveActual = target.SearchLevel(boardFake, Definitions.ChessColor.Black, 1, out scoreActual);

            Assert.AreEqual(1, scoreActual);
            Assert.AreEqual(bestFakeMove, bestMoveActual);
        }

        // Depth 2

        [TestMethod]
        public void SearchMinimaxTest_WhenWhiteMovesFirstAnd2Levels_ThenCorrectSelected()
        {
            //      start
            //     /      \
            //    1       3x    white move -> highest selected (x) 
            //  /  \     /  \   
            // 1x   2   3x   4  black move -> lowest selected (x)
            IEvaluator evalFake = new FakeEvaluator(new List<float>() { 1, 2, 3, 4});
            FakeMoveGeneratorMulitlevel moveGenFake = new FakeMoveGeneratorMulitlevel();
            IMove bestFakeMove = new NormalMove(Piece.MakePiece('Q'), 0, 0, 0, 0, null);
            moveGenFake.AddGetAllMoves(new List<IMove>() {new NormalMove(Piece.MakePiece('P'), 0, 0, 0, 0, null), bestFakeMove }); // first level
            moveGenFake.AddGetAllMoves(new List<IMove>() {new NormalMove(Piece.MakePiece('b'), 0, 0, 0, 0, null), new NormalMove(Piece.MakePiece('b'), 0,0,0,0,null)}); // second level 1.
            moveGenFake.AddGetAllMoves(new List<IMove>() {new NormalMove(Piece.MakePiece('b'), 0, 0, 0, 0, null), new NormalMove(Piece.MakePiece('b'), 0,0,0,0,null)}); // second level 2.

            IBoard boardFake = new FakeBoard();

            var target = new SearchMinimax(evalFake, moveGenFake);
            float scoreActual;
            IMove bestMoveActual = target.SearchLevel(boardFake, Definitions.ChessColor.White, 2, out scoreActual);

            Assert.AreEqual(3, scoreActual);
            Assert.AreEqual(bestFakeMove, bestMoveActual);
        }

        [TestMethod]
        public void SearchMinimaxTest_WhenBlackMovesFirstAnd2Levels_ThenCorrectSelected()
        {
            //      start
            //     /      \
            //    2x       4      black move -> lowest selected (x) 
            //  /  \     /  \   
            // 1    2x  3    4x  white move -> highest selected (x)
            IEvaluator evalFake = new FakeEvaluator(new List<float>() { 1, 2, 3, 4 });
            FakeMoveGeneratorMulitlevel moveGenFake = new FakeMoveGeneratorMulitlevel();
            IMove bestFakeMove = new NormalMove(Piece.MakePiece('q'), 0, 0, 0, 0, null);
            moveGenFake.AddGetAllMoves(new List<IMove>() { bestFakeMove, new NormalMove(Piece.MakePiece('b'), 0, 0, 0, 0, null) }); // first level
            moveGenFake.AddGetAllMoves(new List<IMove>() { new NormalMove(Piece.MakePiece('P'), 0, 0, 0, 0, null), new NormalMove(Piece.MakePiece('P'), 0, 0, 0, 0, null) }); // second level 1.
            moveGenFake.AddGetAllMoves(new List<IMove>() { new NormalMove(Piece.MakePiece('P'), 0, 0, 0, 0, null), new NormalMove(Piece.MakePiece('P'), 0, 0, 0, 0, null) }); // second level 2.

            IBoard boardFake = new FakeBoard();

            var target = new SearchMinimax(evalFake, moveGenFake);
            float scoreActual;
            IMove bestMoveActual = target.SearchLevel(boardFake, Definitions.ChessColor.Black, 2, out scoreActual);

            Assert.AreEqual(2, scoreActual);
            Assert.AreEqual(bestFakeMove, bestMoveActual);
        }

        // Depth 3

        [TestMethod]
        public void SearchMinimaxTest_WhenWhiteMovesFirstAnd3Levels_ThenCorrectSelected()
        {
            //           start
            //        /          \
            //       2            4x         white move -> highest selected (x) 
            //    /    \        /    \   
            //   2x     8      6      4x     black move -> lowest selected (x)
            //  / \    / \    / \    / \
            // 2x  1  7  8x  5  6x  3  4x    white move -> highest selected (x) 
            IEvaluator evalFake = new FakeEvaluator(new List<float>() { 2, 1, 7, 8, 5, 6, 3, 4 }); // lowest level evaluation
            FakeMoveGeneratorMulitlevel moveGenFake = new FakeMoveGeneratorMulitlevel();
            IMove bestFakeMove = new NormalMove(Piece.MakePiece('Q'), 0, 0, 0, 0, null);
            moveGenFake.AddGetAllMoves(new List<IMove>() { new NormalMove(Piece.MakePiece('P'), 0, 0, 0, 0, null), bestFakeMove }); // 1. level (white)
            moveGenFake.AddGetAllMoves(new List<IMove>() { new NormalMove(Piece.MakePiece('b'), 0, 0, 0, 0, null), new NormalMove(Piece.MakePiece('b'), 0, 0, 0, 0, null) }); // 2. level a (black)
            moveGenFake.AddGetAllMoves(new List<IMove>() { new NormalMove(Piece.MakePiece('B'), 0, 0, 0, 0, null), new NormalMove(Piece.MakePiece('B'), 0, 0, 0, 0, null) }); // 3. level a (white)
            moveGenFake.AddGetAllMoves(new List<IMove>() { new NormalMove(Piece.MakePiece('B'), 0, 0, 0, 0, null), new NormalMove(Piece.MakePiece('B'), 0, 0, 0, 0, null) }); // 3. level b (white)
            moveGenFake.AddGetAllMoves(new List<IMove>() { new NormalMove(Piece.MakePiece('b'), 0, 0, 0, 0, null), new NormalMove(Piece.MakePiece('b'), 0, 0, 0, 0, null) }); // 2. level b (black)
            moveGenFake.AddGetAllMoves(new List<IMove>() { new NormalMove(Piece.MakePiece('B'), 0, 0, 0, 0, null), new NormalMove(Piece.MakePiece('B'), 0, 0, 0, 0, null) }); // 3. level c (white)
            moveGenFake.AddGetAllMoves(new List<IMove>() { new NormalMove(Piece.MakePiece('B'), 0, 0, 0, 0, null), new NormalMove(Piece.MakePiece('B'), 0, 0, 0, 0, null) }); // 3. level d (white)

            IBoard boardFake = new FakeBoard();

            var target = new SearchMinimax(evalFake, moveGenFake);
            float scoreActual;
            IMove bestMoveActual = target.SearchLevel(boardFake, Definitions.ChessColor.White, 3, out scoreActual);

            Assert.AreEqual(4, scoreActual);
            Assert.AreEqual(bestFakeMove, bestMoveActual);
        }

        // irregular

        // Depth 1

        [TestMethod]
        public void SearchMinimaxTest_WhenWhiteMovesFirstAnd1Level1_ThenCorrectSelected()
        {
            //      start
            //     /      
            //    1x           white move -> highest selected (x) 
            IEvaluator evalFake = new FakeEvaluator(new List<float>() { 1 });
            FakeMoveGeneratorMulitlevel moveGenFake = new FakeMoveGeneratorMulitlevel();
            IMove bestFakeMove = new NormalMove(Piece.MakePiece('Q'), 0, 0, 0, 0, null);
            moveGenFake.AddGetAllMoves(new List<IMove>() { bestFakeMove }); // first level

            IBoard boardFake = new FakeBoard();

            var target = new SearchMinimax(evalFake, moveGenFake);
            float scoreActual;
            IMove bestMoveActual = target.SearchLevel(boardFake, Definitions.ChessColor.White, 1, out scoreActual);

            Assert.AreEqual(1, scoreActual);
            Assert.AreEqual(bestFakeMove, bestMoveActual);
        }

        // Depth 2

        [TestMethod]
        public void SearchMinimaxTest_WhenWhiteMovesFirstAnd2LevelsIrregular_ThenCorrectSelected()
        {
            //      start
            //     /      \
            //    1       4x    white move -> highest selected (x) 
            //  /  \        \   
            // 1x   2        4x  black move -> lowest selected (x)
            IEvaluator evalFake = new FakeEvaluator(new List<float>() { 1, 2, 4 });
            FakeMoveGeneratorMulitlevel moveGenFake = new FakeMoveGeneratorMulitlevel();
            IMove bestFakeMove = new NormalMove(Piece.MakePiece('Q'), 0, 0, 0, 0, null);
            moveGenFake.AddGetAllMoves(new List<IMove>() { new NormalMove(Piece.MakePiece('P'), 0, 0, 0, 0, null), bestFakeMove }); // first level
            moveGenFake.AddGetAllMoves(new List<IMove>() { new NormalMove(Piece.MakePiece('b'), 0, 0, 0, 0, null), new NormalMove(Piece.MakePiece('b'), 0, 0, 0, 0, null) }); // second level 1.
            moveGenFake.AddGetAllMoves(new List<IMove>() { new NormalMove(Piece.MakePiece('b'), 0, 0, 0, 0, null) }); // second level 2.

            IBoard boardFake = new FakeBoard();

            var target = new SearchMinimax(evalFake, moveGenFake);
            float scoreActual;
            IMove bestMoveActual = target.SearchLevel(boardFake, Definitions.ChessColor.White, 2, out scoreActual);

            Assert.AreEqual(4, scoreActual);
            Assert.AreEqual(bestFakeMove, bestMoveActual);
        }

        [TestMethod]
        public void SearchMinimaxTest_WhenWhiteMovesFirstAnd2LevelsIrregular2_ThenCorrectSelected()
        {
            //        start
            //      /      \
            //     3        5x    white move -> highest selected (x) 
            //  /  |  \      \   
            // 6   4   3x     5x  black move -> lowest selected (x)
            IEvaluator evalFake = new FakeEvaluator(new List<float>() { 6, 4, 3, 5 });
            FakeMoveGeneratorMulitlevel moveGenFake = new FakeMoveGeneratorMulitlevel();
            IMove bestFakeMove = new NormalMove(Piece.MakePiece('Q'), 0, 0, 0, 0, null);
            moveGenFake.AddGetAllMoves(new List<IMove>() { new NormalMove(Piece.MakePiece('P'), 0, 0, 0, 0, null), bestFakeMove }); // level 1
            moveGenFake.AddGetAllMoves(new List<IMove>() { new NormalMove(Piece.MakePiece('b'), 0, 0, 0, 0, null), new NormalMove(Piece.MakePiece('b'), 0, 0, 0, 0, null), new NormalMove(Piece.MakePiece('b'), 0, 0, 0, 0, null) }); // level 2 a
            moveGenFake.AddGetAllMoves(new List<IMove>() { new NormalMove(Piece.MakePiece('b'), 0, 0, 0, 0, null) }); // level 2 b

            IBoard boardFake = new FakeBoard();

            var target = new SearchMinimax(evalFake, moveGenFake);
            float scoreActual;
            IMove bestMoveActual = target.SearchLevel(boardFake, Definitions.ChessColor.White, 2, out scoreActual);

            Assert.AreEqual(5, scoreActual);
            Assert.AreEqual(bestFakeMove, bestMoveActual);
        }

        // -------------------------------------------------
        // checkmate and stall mate
        // -------------------------------------------------

        // SearchLevel Tests

        [TestMethod]
        public void SearchMinimaxTest_WhenWhiteIsCheckMateIn2_AndMaxDepth2_ThenNoLegalMove()
        {
            //      start
            //     /      \
            //   -1000    -1010     white move -> highest selected (x), but here white has lost king-> no move returned
            //   /           \   
            // -10000x       -10010x     black move -> lowest selected (x)
            
            FakeMoveGeneratorMulitlevel moveGenFake = new FakeMoveGeneratorMulitlevel();
            moveGenFake.AddGetAllMoves(new List<IMove>() { new NormalMove(Piece.MakePiece('P'), 0, 0, 0, 0, null), new NormalMove(Piece.MakePiece('Q'), 0, 0, 0, 0, null) }); // level 1
            moveGenFake.AddGetAllMoves(new List<IMove>() { new NormalMove(Piece.MakePiece('b'), 0, 0, 0, 0, null) }); // level 2 a
            moveGenFake.AddGetAllMoves(new List<IMove>() { new NormalMove(Piece.MakePiece('b'), 0, 0, 0, 0, null) }); // level 2 b
            moveGenFake.SetIsChecks(new List<bool>() { true, true });
            IEvaluator evalFake = new FakeEvaluator(new List<float>() { -10000, -10010 });
            IBoard boardFake = new FakeBoard();

            var target = new SearchMinimax(evalFake, moveGenFake);
            float scoreActual;
            IMove bestMoveActual = target.SearchLevel(boardFake, Definitions.ChessColor.White, 2, out scoreActual);

            Assert.IsTrue(-1000 > scoreActual);
            Assert.AreEqual(new NoLegalMove(), bestMoveActual);
        }

        [TestMethod]
        public void SearchMinimaxTest_WhenWhiteIsStllMateIn2_ThenNoLegalMoveScore0()
        {
            //      start
            //     /      \
            //    0x      -10       white move -> highest selected (x)
            //   /           \   
            // -10000x       -10x     black move -> lowest selected (x)  stall mate here. eval returns -10000, ischeck returns false
            
            FakeMoveGeneratorMulitlevel moveGenFake = new FakeMoveGeneratorMulitlevel();
            moveGenFake.AddGetAllMoves(new List<IMove>() { new NormalMove(Piece.MakePiece('P'), 0, 0, 0, 0, null), new NormalMove(Piece.MakePiece('Q'), 0, 0, 0, 0, null) }); // level 1
            moveGenFake.AddGetAllMoves(new List<IMove>() { new NormalMove(Piece.MakePiece('b'), 0, 0, 0, 0, null) }); // level 2 a
            moveGenFake.AddGetAllMoves(new List<IMove>() { new NormalMove(Piece.MakePiece('q'), 0, 0, 0, 0, null) }); // level 2 b
            moveGenFake.SetIsChecks(new List<bool>() { false, false });
            IEvaluator evalFake = new FakeEvaluator(new List<float>() { -10000, -10 });
            IBoard boardFake = new FakeBoard();

            var target = new SearchMinimax(evalFake, moveGenFake);
            float scoreActual;
            IMove bestMoveActual = target.SearchLevel(boardFake, Definitions.ChessColor.White, 2, out scoreActual);

            Assert.AreEqual(0, scoreActual, "stall mate should be score 0");
            Assert.AreEqual(new NoLegalMove(), bestMoveActual);
        }


        // ---------------------------------------------------------
        // Search Tests
        // ---------------------------------------------------------

        // Remark:

        // If now checkmate   --> SearchLevel(2) --> NoLegalMove

        // if checkmate in 2  --> SearchLevel(2) --> normal move
        //                    --> SearchLevel(4) --> NoLegalMove

        // if checkmate in 4  --> SearchLevel(2) --> normal move
        //                    --> SearchLevel(4) --> normal Move
        //                    --> SearchLevel(6) --> NoLegalMove

        [TestMethod]
        public void SearchTest_WhenCheckmateNow_ThenReturnNoLegalMove()
        {
            // checkmate now --> Search(level 2) -> nolegal move

            var scoresMoves = new List<MoveAndScore>()
            {
                null,
                null,
                new MoveAndScore() {Move = new NoLegalMove(), Score = -10000}, // level 2: king lost
                null,
                new MoveAndScore() {Move = new NoLegalMove(), Score = -10000}, // level 4 king lost

            };
            var target = new SearchMinimaxDouble_SearchLevelOverwritten(null, null, scoresMoves.ToArray());
            target.SetMaxDepth(4);

            float actualScore;
            var actualMove = target.Search(null, Definitions.ChessColor.Empty, out actualScore);

            Assert.AreEqual(new NoLegalMove(), actualMove);
            Assert.AreEqual(-10000, actualScore);
        }
        
        [TestMethod]
        public void SearchTest_WhenCheckmateIn2_ThenReturnNormalMove()
        {
            // checkmate in 2  --> SearchLevel(2) --> normal move
            //                 --> SearchLevel(4) --> NoLegalMove

            var scoresMoves = new List<MoveAndScore>()
            {
                null,
                null,
                new MoveAndScore() {Move = new NormalMove(Piece.MakePiece('Q'), 0, 0, 0, 0, null), Score = 0}, // level 2: checkmate
                null,
                new MoveAndScore() {Move = new NoLegalMove(), Score = -10000}, // level 4: king lost

            };
            var target = new SearchMinimaxDouble_SearchLevelOverwritten(null, null, scoresMoves.ToArray());
            target.SetMaxDepth(4);

            float actualScore;
            var actualMove = target.Search(null, Definitions.ChessColor.Empty, out actualScore);

            Assert.AreEqual(new NormalMove(Piece.MakePiece('Q'), 0, 0, 0, 0, null), actualMove);
            Assert.AreEqual(0, actualScore);
        }

        [TestMethod]
        public void SearchTest_WhenCheckmateIn4MaxDepth6_ThenReturnNormalMove()
        {
            // checkmate in 4  --> SearchLevel(2) --> normal move
            //                 --> SearchLevel(4) --> normal Move  <-- select this
            //                 --> SearchLevel(6) --> NoLegalMove

            var scoresMoves = new List<MoveAndScore>()
            {
                null,
                null,
                new MoveAndScore() {Move = new NormalMove(Piece.MakePiece('P'), 0, 0, 0, 0, null), Score = 1}, // level 2: checkmate in 2
                null,
                new MoveAndScore() {Move = new NormalMove(Piece.MakePiece('Q'), 0, 0, 0, 0, null), Score = 2}, // level 4: check mate
                null,
                new MoveAndScore() {Move = new NoLegalMove(), Score = -10000}, // level 6: lost king

            };
            var target = new SearchMinimaxDouble_SearchLevelOverwritten(null, null, scoresMoves.ToArray());
            target.SetMaxDepth(6);

            float actualScore;
            var actualMove = target.Search(null, Definitions.ChessColor.Empty, out actualScore);

            Assert.AreEqual(new NormalMove(Piece.MakePiece('Q'), 0, 0, 0, 0, null), actualMove);
            Assert.AreEqual(2, actualScore);
        }

        [TestMethod]
        public void SearchTest_WhenCheckmateIn2MaxDepth6_ThenReturnNormalMove()
        {
            // checkmate in 2  --> SearchLevel(2) --> normal move  <-- select this
            //                 --> SearchLevel(4) --> NoLegalMove
            //                 --> SearchLevel(6) --> NoLegalMove

            var scoresMoves = new List<MoveAndScore>()
            {
                null,
                null,
                new MoveAndScore() {Move = new NormalMove(Piece.MakePiece('P'), 0, 0, 0, 0, null), Score = 1}, // level 2: checkmate
                null,
                new MoveAndScore() {Move = new NoLegalMove(), Score = -10000}, // level 4: lost king
                null,
                new MoveAndScore() {Move = new NoLegalMove(), Score = -10000}, // level 6: lost king

            };
            var target = new SearchMinimaxDouble_SearchLevelOverwritten(null, null, scoresMoves.ToArray());
            target.SetMaxDepth(6);

            float actualScore;
            var actualMove = target.Search(null, Definitions.ChessColor.Empty, out actualScore);

            Assert.AreEqual(new NormalMove(Piece.MakePiece('P'), 0, 0, 0, 0, null), actualMove);
            Assert.AreEqual(1, actualScore);
        }

        [TestMethod]
        public void SearchTest_WhenCheckmateIn2MaxDepth3_ThenReturnNormalMove()
        {
            // checkmate in 2  --> SearchLevel(2) --> normal move  <-- select this
            //                 --> SearchLevel(3) --> NoLegalMove
            //                 --> SearchLevel(4) --> NoLegalMove

            var scoresMoves = new List<MoveAndScore>()
            {
                null,
                null,
                new MoveAndScore() {Move = new NoLegalMove(), Score = -10000}, // level 2: lost king
                new MoveAndScore() {Move = new NoLegalMove(), Score = -10000}, // level 3: lost king

            };
            var target = new SearchMinimaxDouble_SearchLevelOverwritten(null, null, scoresMoves.ToArray());
            target.SetMaxDepth(3);

            float actualScore;
            var actualMove = target.Search(null, Definitions.ChessColor.Empty, out actualScore);

            Assert.AreEqual(new NoLegalMove(), actualMove);
            Assert.AreEqual(-10000, actualScore);
        }

        [TestMethod]
        public void SearchTest_WhenCheckmateNow_Level3_ThenReturnNormalMove()
        {
            // check mate in 2  --> SearchLevel(3) --> NoLegalMove
            //                  --> SearchLevel(2) --> NoLegalMove

            var scoresMoves = new List<MoveAndScore>()
            {
                null,
                null,
                new MoveAndScore() {Move = new NoLegalMove(), Score = -10001}, // level 2: lost king
                new MoveAndScore() {Move = new NoLegalMove(), Score = -10000}, // level 3: lost king

            };
            var target = new SearchMinimaxDouble_SearchLevelOverwritten(null, null, scoresMoves.ToArray());
            target.SetMaxDepth(3);

            float actualScore;
            var actualMove = target.Search(null, Definitions.ChessColor.Empty, out actualScore);

            Assert.AreEqual(new NoLegalMove(), actualMove);
            Assert.AreEqual(-10000, actualScore);
        }

        [TestMethod]
        public void SearchTest_WhenCheckmatIn2_Level5_ThenReturnNormalMove()
        {
            // check mate in 2  --> SearchLevel(5) --> NoLegalMove
            //                  --> SearchLevel(2) --> Normal move

            var scoresMoves = new List<MoveAndScore>()
            {
                null,
                null,
                new MoveAndScore() {Move = new NormalMove(Piece.MakePiece('Q'),0,0,0,0,null), Score = 1}, // level 2: lost king
                null,
                null,
                new MoveAndScore() {Move = new NoLegalMove(), Score = -10000}, // level 5: lost king
            };
            var target = new SearchMinimaxDouble_SearchLevelOverwritten(null, null, scoresMoves.ToArray());
            target.SetMaxDepth(5);

            float actualScore;
            var actualMove = target.Search(null, Definitions.ChessColor.Empty, out actualScore);

            Assert.AreEqual(new NormalMove(Piece.MakePiece('Q'),0,0,0,0,null), actualMove);
            Assert.AreEqual(1, actualScore);
        }

        [TestMethod]
        public void SearchTest_WhenStallmateIn2_Level2_ThenReturnNoLegalMove()
        {
            // stall mate in 2 --> SearchLevel(2) -> nolegal move, score 0

            var scoresMoves = new List<MoveAndScore>()
            {
                null,
                null,
                new MoveAndScore() {Move = new NoLegalMove(), Score = 0}, // level 2: lost king
            };

            var target = new SearchMinimaxDouble_SearchLevelOverwritten(null, null, scoresMoves.ToArray());
            target.SetMaxDepth(2);

            float actualScore;
            var actualMove = target.Search(null, Definitions.ChessColor.White, out actualScore);

            Assert.AreEqual(new NoLegalMove(),  actualMove);
            Assert.AreEqual(0, actualScore);
        }

        [TestMethod]
        public void SearchTest_WhenStallmateIn4_Level6_ThenReturnNoLegalMove()
        {
            // stall mate in 2 --> SearchLevel(2) -> Normal move, score x
            // stall mate in 2 --> SearchLevel(4) -> nolegal move, score 0
            // stall mate in 2 --> SearchLevel(6) -> nolegal move, score 0

            var scoresMoves = new List<MoveAndScore>()
            {
                null,
                null,
                new MoveAndScore() {Move = new NormalMove(Piece.MakePiece('Q'),0,0,0,0,null), Score = 1}, // level 2: lost king
                null,
                new MoveAndScore() {Move = new NoLegalMove(), Score = 0}, // level 4: lost king
                null,
                new MoveAndScore() {Move = new NoLegalMove(), Score = 0}, // level 6: lost king
            };

            var target = new SearchMinimaxDouble_SearchLevelOverwritten(null, null, scoresMoves.ToArray());
            target.SetMaxDepth(6);

            float actualScore;
            var actualMove = target.Search(null, Definitions.ChessColor.White, out actualScore);

            Assert.AreEqual(new NormalMove(Piece.MakePiece('Q'), 0, 0, 0, 0, null), actualMove);
            Assert.AreEqual(1, actualScore);
        }




    }

   
}
