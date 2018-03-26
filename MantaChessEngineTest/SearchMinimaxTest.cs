using System.Linq;
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

            var bestRatingActual = target.SearchLevel(board, Definitions.ChessColor.White, 2).First(); // level 2
            IMove badMove = new NormalMove(Piece.MakePiece('B'), 'f', 4, 'e', 5, Piece.MakePiece('p'));

            Assert.AreNotEqual(badMove, bestRatingActual.Move, "White bishop should not capture pawn.");
            Assert.AreEqual(1, bestRatingActual.Score);
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

            var bestRatingActual = target.SearchLevel(board, Definitions.ChessColor.White, 3).First(); // level 3
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

            var bestRatingActual = target.SearchLevel(board, Definitions.ChessColor.White, 4).First(); // level 4
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

            var bestRatingActual = target.SearchLevel(board, Definitions.ChessColor.White, 3).First(); // level 3
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

            var bestRatingActual = target.SearchLevel(board, Definitions.ChessColor.White, 3).First(); // level 3
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

            var bestRatingActual = target.SearchLevel(board, Definitions.ChessColor.Black, 2).First(); // level 2
            IMove badMove = new NormalMove(Piece.MakePiece('b'), 'f', 6, 'e', 5, Piece.MakePiece('P'));

            Assert.AreNotEqual(badMove, bestRatingActual.Move, "Black bishop should not capture pawn.");
            Assert.AreEqual(-1, bestRatingActual.Score);
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

            var bestRatingActual = target.SearchLevel(board, Definitions.ChessColor.Black, 3).First(); // level 3
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

            var bestRatingActual = target.SearchLevel(board, Definitions.ChessColor.Black, 4).First(); // level 4
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

            var bestRatingActual = target.SearchLevel(board, Definitions.ChessColor.White, 2).First();

            Assert.AreEqual(0, bestRatingActual.Score, "stalemate score should be 0");
            Assert.AreEqual(new NoLegalMove(), bestRatingActual.Move, "Should be NoLegalMove, white is stalemate");
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

            var bestRatingActual = target.SearchLevel(board, Definitions.ChessColor.White, 2).First();

            Assert.AreEqual(new NoLegalMove(), bestRatingActual.Move, "Should be NoLegalMove, white is stalemate");
            Assert.AreEqual(-10000, bestRatingActual.Score, "check mate score should be -10000");
        }

        // ---------------------------------------------------------------------------------------------
        // Search Level Tests with Mocks
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
            var bestRatingActual = target.SearchLevel(boardFake, Definitions.ChessColor.White, 1).First();

            Assert.AreEqual(2, bestRatingActual.Score);
            Assert.AreEqual(bestFakeMove, bestRatingActual.Move);
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
            var bestRatingActual = target.SearchLevel(boardFake, Definitions.ChessColor.Black, 1).First();

            Assert.AreEqual(1, bestRatingActual.Score);
            Assert.AreEqual(bestFakeMove, bestRatingActual.Move);
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
            var bestRatingActual = target.SearchLevel(boardFake, Definitions.ChessColor.White, 2).First();

            Assert.AreEqual(3, bestRatingActual.Score);
            Assert.AreEqual(bestFakeMove, bestRatingActual.Move);
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
            var bestRatingActual = target.SearchLevel(boardFake, Definitions.ChessColor.Black, 2).First();

            Assert.AreEqual(2, bestRatingActual.Score);
            Assert.AreEqual(bestFakeMove, bestRatingActual.Move);
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
            var bestRatingActual = target.SearchLevel(boardFake, Definitions.ChessColor.White, 3).First();

            Assert.AreEqual(4, bestRatingActual.Score);
            Assert.AreEqual(bestFakeMove, bestRatingActual.Move);
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
            target.SetMaxDepth(1);
            var bestRating = target.SearchLevel(boardFake, Definitions.ChessColor.White, 1).First();

            Assert.AreEqual(1, bestRating.Score);
            Assert.AreEqual(bestFakeMove, bestRating.Move);
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
            target.SetMaxDepth(2);
            var bestRatingActual = target.SearchLevel(boardFake, Definitions.ChessColor.White, 2).First();

            Assert.AreEqual(4, bestRatingActual.Score);
            Assert.AreEqual(bestFakeMove, bestRatingActual.Move);
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
            target.SetMaxDepth(2);
            var bestRatingActual = target.SearchLevel(boardFake, Definitions.ChessColor.White, 2).First();

            Assert.AreEqual(5, bestRatingActual.Score);
            Assert.AreEqual(bestFakeMove, bestRatingActual.Move);
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
            //  -10000    -1000     white move -> highest selected (x), but here white has lost king-> no move returned
            //   /           \   
            // -10000x       -10000x     black move -> lowest selected (x)
            
            FakeMoveGeneratorMulitlevel moveGenFake = new FakeMoveGeneratorMulitlevel();
            moveGenFake.AddGetAllMoves(new List<IMove>() { new NormalMove(Piece.MakePiece('P'), 0, 0, 0, 0, null), new NormalMove(Piece.MakePiece('Q'), 0, 0, 0, 0, null) }); // level 1
            moveGenFake.AddGetAllMoves(new List<IMove>() { new NormalMove(Piece.MakePiece('b'), 0, 0, 0, 0, null) }); // level 2 a
            moveGenFake.AddGetAllMoves(new List<IMove>() { new NormalMove(Piece.MakePiece('b'), 0, 0, 0, 0, null) }); // level 2 b
            moveGenFake.SetIsChecks(new List<bool>() { true, true });
            IEvaluator evalFake = new FakeEvaluator(new List<float>() { -10000, -10000 });
            IBoard boardFake = new FakeBoard();

            var target = new SearchMinimax(evalFake, moveGenFake);
            target.SetMaxDepth(2);
            var bestRatingActual = target.SearchLevel(boardFake, Definitions.ChessColor.White, 2).First();

            Assert.AreEqual(-10000, bestRatingActual.Score);
            Assert.AreEqual(new NoLegalMove(), bestRatingActual.Move);
        }

        [TestMethod]
        public void SearchMinimaxTest_WhenBlackIsCheckMateIn3_AndMaxDepth3_ThenNormalMove()
        {
            //       start
            //     /       \
            //   10000x    10000x     white move -> highest selected (x) (legal move)
            //    /           \   
            //  10000x      10000x     black move -> lowest selected (x) (black king is/goes in check -> not legal)
            //   /            \   
            // 10000x        10000x     white move -> highest selected (x), black king lost (white captures king -> not legal)
            // check         check (2 ply earlier)

            FakeMoveGeneratorMulitlevel moveGenFake = new FakeMoveGeneratorMulitlevel();
            moveGenFake.AddGetAllMoves(new List<IMove>() { new NormalMove(Piece.MakePiece('P'), 0, 0, 0, 0, null), new NormalMove(Piece.MakePiece('Q'), 0, 0, 0, 0, null) }); // level 1
            moveGenFake.AddGetAllMoves(new List<IMove>() { new NormalMove(Piece.MakePiece('b'), 0, 0, 0, 0, null) }); // level 2 a
            moveGenFake.AddGetAllMoves(new List<IMove>() { new NormalMove(Piece.MakePiece('B'), 0, 0, 0, 0, null) }); // level 3 a
            moveGenFake.AddGetAllMoves(new List<IMove>() { new NormalMove(Piece.MakePiece('n'), 0, 0, 0, 0, null) }); // level 2 b
            moveGenFake.AddGetAllMoves(new List<IMove>() { new NormalMove(Piece.MakePiece('N'), 0, 0, 0, 0, null) }); // level 3 b
            moveGenFake.SetIsChecks(new List<bool>() { true, true });
            IEvaluator evalFake = new FakeEvaluator(new List<float>() { 10000, 10000 });
            IBoard boardFake = new FakeBoard();

            var target = new SearchMinimax(evalFake, moveGenFake);
            target.SetMaxDepth(3);
            var bestRatingActual = target.SearchLevel(boardFake, Definitions.ChessColor.White, 3).First();

            Assert.AreEqual(10000, bestRatingActual.Score);
            Assert.IsTrue(bestRatingActual.Move is NormalMove); // the first move of white is legal
        }

        [TestMethod]
        public void SearchMinimaxTest_WhenWhiteIsCheckMateIn4_AndMaxDepth4_ThenNoLegalMove()
        {
            //      start
            //     /      \
            //   -1000    -1010     white move -> highest selected (x), legal move
            //   /           \   
            // -10000x       -10010x     black move -> lowest selected (x), legal move
            //   /           \   
            // -10000x       -10010x     white move -> highest selected (x), king is/goes in check -> not legal
            //   /           \   
            // -10000x       -10010x     black move -> lowest selected (x), king is captured -> not legal

            FakeMoveGeneratorMulitlevel moveGenFake = new FakeMoveGeneratorMulitlevel();
            moveGenFake.AddGetAllMoves(new List<IMove>() { new NormalMove(Piece.MakePiece('P'), 0, 0, 0, 0, null), new NormalMove(Piece.MakePiece('Q'), 0, 0, 0, 0, null) }); // level 1
            moveGenFake.AddGetAllMoves(new List<IMove>() { new NormalMove(Piece.MakePiece('b'), 0, 0, 0, 0, null) }); // level 2 a
            moveGenFake.AddGetAllMoves(new List<IMove>() { new NormalMove(Piece.MakePiece('B'), 0, 0, 0, 0, null) }); // level 3 a
            moveGenFake.AddGetAllMoves(new List<IMove>() { new NormalMove(Piece.MakePiece('b'), 0, 0, 0, 0, null) }); // level 4 a
            moveGenFake.AddGetAllMoves(new List<IMove>() { new NormalMove(Piece.MakePiece('n'), 0, 0, 0, 0, null) }); // level 2 b
            moveGenFake.AddGetAllMoves(new List<IMove>() { new NormalMove(Piece.MakePiece('N'), 0, 0, 0, 0, null) }); // level 3 b
            moveGenFake.AddGetAllMoves(new List<IMove>() { new NormalMove(Piece.MakePiece('n'), 0, 0, 0, 0, null) }); // level 4 b
            moveGenFake.SetIsChecks(new List<bool>() { true, true });
            IEvaluator evalFake = new FakeEvaluator(new List<float>() { -10000, -10000 });
            IBoard boardFake = new FakeBoard();

            var target = new SearchMinimax(evalFake, moveGenFake);
            target.SetMaxDepth(4);
            var bestRatingActual = target.SearchLevel(boardFake, Definitions.ChessColor.White, 4).First();

            Assert.AreEqual(-10000, bestRatingActual.Score);
            Assert.IsTrue(bestRatingActual.Move is NormalMove, "should be normal move, check mate is only in 2nd move");
        }

        [TestMethod]
        public void SearchMinimaxTest_WhenWhiteIsStallMateIn2_ThenNoLegalMoveScore0()
        {
            //      start
            //        0
            //     /      \
            // -10000x   -10000x       white move -> highest selected (x)
            //   /           \   
            // -10000x     -10000x     black move -> lowest selected (x)  stall mate here. eval returns -10000, ischeck returns false
            // not check    not check
            FakeMoveGeneratorMulitlevel moveGenFake = new FakeMoveGeneratorMulitlevel();
            moveGenFake.AddGetAllMoves(new List<IMove>() { new NormalMove(Piece.MakePiece('P'), 0, 0, 0, 0, null), new NormalMove(Piece.MakePiece('Q'), 0, 0, 0, 0, null) }); // level 1
            moveGenFake.AddGetAllMoves(new List<IMove>() { new NormalMove(Piece.MakePiece('b'), 0, 0, 0, 0, null) }); // level 2 a
            moveGenFake.AddGetAllMoves(new List<IMove>() { new NormalMove(Piece.MakePiece('n'), 0, 0, 0, 0, null) }); // level 2 b
            moveGenFake.SetIsChecks(new List<bool>() { false, false });
            IEvaluator evalFake = new FakeEvaluator(new List<float>() { -10000, -10000 });
            IBoard boardFake = new FakeBoard();

            var target = new SearchMinimax(evalFake, moveGenFake);
            target.SetMaxDepth(2);
            var bestRatingActual = target.SearchLevel(boardFake, Definitions.ChessColor.White, 2).First();

            Assert.AreEqual(0, bestRatingActual.Score, "stall mate should be score 0");
            Assert.AreEqual(new NoLegalMove(), bestRatingActual.Move);
        }

        [TestMethod]
        public void SearchMinimaxTest_WhenWhiteIsStallMateIn2MoreOptions_ThenNoLegalMoveScore0()
        {
            //              start
            //                0  (not check here) (white is stall mate)
            //          /            \
            //     -10000x          -10000x       white move -> highest selected (x) (white king goes in check -> not legal)
            //     a/    \b         c/    \d   
            // -10000x  -10        10  -10000x     black move -> lowest selected (x), white king lost here (black captures white king -> not legal)
            // not check    not check
            FakeMoveGeneratorMulitlevel moveGenFake = new FakeMoveGeneratorMulitlevel();
            moveGenFake.AddGetAllMoves(new List<IMove>() { new NormalMove(Piece.MakePiece('P'), 0, 0, 0, 0, null), new NormalMove(Piece.MakePiece('Q'), 0, 0, 0, 0, null) }); // level 1
            moveGenFake.AddGetAllMoves(new List<IMove>() { new NormalMove(Piece.MakePiece('b'), 0, 0, 0, 0, null), new NormalMove(Piece.MakePiece('n'), 0, 0, 0, 0, null) }); // level 2 ab
            moveGenFake.AddGetAllMoves(new List<IMove>() { new NormalMove(Piece.MakePiece('q'), 0, 0, 0, 0, null), new NormalMove(Piece.MakePiece('k'), 0, 0, 0, 0, null) }); // level 2 cd
            moveGenFake.SetIsChecks(new List<bool>() { false, false, false, false });
            IEvaluator evalFake = new FakeEvaluator(new List<float>() { -10000, -10, 10, -10000 });
            IBoard boardFake = new FakeBoard();

            var target = new SearchMinimax(evalFake, moveGenFake);
            target.SetMaxDepth(2);
            var bestRatingActual = target.SearchLevel(boardFake, Definitions.ChessColor.White, 2).First();

            Assert.AreEqual(0, bestRatingActual.Score, "stall mate should be score 0");
            Assert.IsTrue(bestRatingActual.Move is NoLegalMove);
        }

        [TestMethod]
        public void SearchMinimaxTest_WhenWhiteIsNotStallMate_ThenNormalMove()
        {
            //              start
            //               -10  (not check here) (white is not stall mate)
            //          /            \
            //     -10000            -10x       white move -> highest selected (x) (white king goes in check -> not legal)
            //     a/    \b         c/    \d   
            // -10000x  -10        -10x   -1     black move -> lowest selected (x), white king lost here (black captures white king -> not legal)
            // not check    not check
            FakeMoveGeneratorMulitlevel moveGenFake = new FakeMoveGeneratorMulitlevel();
            moveGenFake.AddGetAllMoves(new List<IMove>() { new NormalMove(Piece.MakePiece('P'), 0, 0, 0, 0, null), new NormalMove(Piece.MakePiece('Q'), 0, 0, 0, 0, null) }); // level 1
            moveGenFake.AddGetAllMoves(new List<IMove>() { new NormalMove(Piece.MakePiece('b'), 0, 0, 0, 0, null), new NormalMove(Piece.MakePiece('n'), 0, 0, 0, 0, null) }); // level 2 ab
            moveGenFake.AddGetAllMoves(new List<IMove>() { new NormalMove(Piece.MakePiece('q'), 0, 0, 0, 0, null), new NormalMove(Piece.MakePiece('k'), 0, 0, 0, 0, null) }); // level 2 cd
            moveGenFake.SetIsChecks(new List<bool>() { false, false, false, false });
            IEvaluator evalFake = new FakeEvaluator(new List<float>() { -10000, -10, -10, -1 });
            IBoard boardFake = new FakeBoard();

            var target = new SearchMinimax(evalFake, moveGenFake);
            target.SetMaxDepth(2);
            var bestRatingActual = target.SearchLevel(boardFake, Definitions.ChessColor.White, 2).First();

            Assert.AreEqual(-10, bestRatingActual.Score, "white is not stall mate. score should be -10");
            Assert.IsTrue(bestRatingActual.Move is NormalMove);
        }

        [TestMethod]
        public void SearchMinimaxTest_WhenWhiteIsNotStallMate2_ThenNormalMove()
        {
            //              start
            //               -10  (not check here) (white is not stall mate)
            //          /            \
            //        -10          -10000         white move -> highest selected (x) (white king goes in check -> not legal)
            //      a/    \b       c/    \d   
            //   -10x    -1     -10000x  -10       black move -> lowest selected (x), white king lost here (black captures white king -> not legal)
            // not check    not check
            FakeMoveGeneratorMulitlevel moveGenFake = new FakeMoveGeneratorMulitlevel();
            moveGenFake.AddGetAllMoves(new List<IMove>() { new NormalMove(Piece.MakePiece('P'), 0, 0, 0, 0, null), new NormalMove(Piece.MakePiece('Q'), 0, 0, 0, 0, null) }); // level 1
            moveGenFake.AddGetAllMoves(new List<IMove>() { new NormalMove(Piece.MakePiece('b'), 0, 0, 0, 0, null), new NormalMove(Piece.MakePiece('n'), 0, 0, 0, 0, null) }); // level 2 ab
            moveGenFake.AddGetAllMoves(new List<IMove>() { new NormalMove(Piece.MakePiece('q'), 0, 0, 0, 0, null), new NormalMove(Piece.MakePiece('k'), 0, 0, 0, 0, null) }); // level 2 cd
            moveGenFake.SetIsChecks(new List<bool>() { false, false, false, false });
            IEvaluator evalFake = new FakeEvaluator(new List<float>() { -10, -1, -10000, -10 });
            IBoard boardFake = new FakeBoard();

            var target = new SearchMinimax(evalFake, moveGenFake);
            target.SetMaxDepth(2);
            var bestRatingActual = target.SearchLevel(boardFake, Definitions.ChessColor.White, 2).First();

            Assert.AreEqual(-10, bestRatingActual.Score, "white is not stall mate. score should be -10");
            Assert.IsTrue(bestRatingActual.Move is NormalMove);
        }

        [TestMethod]
        public void SearchMinimaxTest_WhenBlackIsStallMateIn3_Depth3_ThenNoLegalMoveScore0()
        {
            //      start
            //        0
            //     /      \
            //  10000x    10000x       black move -> lowest selected (x)  (not check) legal move
            //   /           \   
            //  10000x    10000x       white move -> highest selected (x) white king is in check or goes in check -> not legal
            //   /           \   
            // 10000x     10000x      black move -> lowest selected (x)  black captures white king -> not legal move
            // not check    not  (2 plys earlier)
            FakeMoveGeneratorMulitlevel moveGenFake = new FakeMoveGeneratorMulitlevel();
            moveGenFake.AddGetAllMoves(new List<IMove>() { new NormalMove(Piece.MakePiece('P'), 0, 0, 0, 0, null), new NormalMove(Piece.MakePiece('Q'), 0, 0, 0, 0, null) }); // level 1
            moveGenFake.AddGetAllMoves(new List<IMove>() { new NormalMove(Piece.MakePiece('b'), 0, 0, 0, 0, null) }); // level 2 a
            moveGenFake.AddGetAllMoves(new List<IMove>() { new NormalMove(Piece.MakePiece('B'), 0, 0, 0, 0, null) }); // level 3 a
            moveGenFake.AddGetAllMoves(new List<IMove>() { new NormalMove(Piece.MakePiece('n'), 0, 0, 0, 0, null) }); // level 2 b
            moveGenFake.AddGetAllMoves(new List<IMove>() { new NormalMove(Piece.MakePiece('N'), 0, 0, 0, 0, null) }); // level 3 b
            moveGenFake.SetIsChecks(new List<bool>() { false, false });
            IEvaluator evalFake = new FakeEvaluator(new List<float>() { 10000, 10000 });
            IBoard boardFake = new FakeBoard();

            var target = new SearchMinimax(evalFake, moveGenFake);
            target.SetMaxDepth(3);
            var bestRatingActual = target.SearchLevel(boardFake, Definitions.ChessColor.White, 3).First();

            Assert.AreEqual(0, bestRatingActual.Score, "stall mate should be score 0");
            Assert.IsTrue(bestRatingActual.Move is NormalMove);
        }

        [TestMethod]
        public void SearchMinimaxTest_WhenWhiteIsStallMateIn4_Depth4_ThenNoLegalMoveScore0()
        {
            //      start
            //        0
            //     /      \
            // -10000x   -10000x       white move -> highest selected (x) legal move
            //   /2a        \2b   
            // -10000x   -10000x       black move -> lowest selected (x) (not check) legal move
            //   /3a        \3b   
            // -10000x   -10000x       white move -> highest selected (x) white king goes to check -> not legal
            //   /4a        \4b   
            // -10000x     -10000x     black move -> lowest selected (x)  black captures white king -> not legal
            // not check    not  (2 plys earlier)
            FakeMoveGeneratorMulitlevel moveGenFake = new FakeMoveGeneratorMulitlevel();
            moveGenFake.AddGetAllMoves(new List<IMove>() { new NormalMove(Piece.MakePiece('P'), 0, 0, 0, 0, null), new NormalMove(Piece.MakePiece('Q'), 0, 0, 0, 0, null) }); // level 1
            moveGenFake.AddGetAllMoves(new List<IMove>() { new NormalMove(Piece.MakePiece('b'), 0, 0, 0, 0, null) }); // level 2 a
            moveGenFake.AddGetAllMoves(new List<IMove>() { new NormalMove(Piece.MakePiece('B'), 0, 0, 0, 0, null) }); // level 3 a
            moveGenFake.AddGetAllMoves(new List<IMove>() { new NormalMove(Piece.MakePiece('b'), 0, 0, 0, 0, null) }); // level 4 a
            moveGenFake.AddGetAllMoves(new List<IMove>() { new NormalMove(Piece.MakePiece('n'), 0, 0, 0, 0, null) }); // level 2 b
            moveGenFake.AddGetAllMoves(new List<IMove>() { new NormalMove(Piece.MakePiece('N'), 0, 0, 0, 0, null) }); // level 3 b
            moveGenFake.AddGetAllMoves(new List<IMove>() { new NormalMove(Piece.MakePiece('n'), 0, 0, 0, 0, null) }); // level 4 b
            moveGenFake.SetIsChecks(new List<bool>() { false, false });
            IEvaluator evalFake = new FakeEvaluator(new List<float>() { -10000, -10000 });
            IBoard boardFake = new FakeBoard();

            var target = new SearchMinimax(evalFake, moveGenFake);
            target.SetMaxDepth(4);
            var bestRatingActual = target.SearchLevel(boardFake, Definitions.ChessColor.White, 4).First();

            Assert.AreEqual(0, bestRatingActual.Score, "stall mate should be score 0");
            Assert.IsTrue(bestRatingActual.Move is NormalMove);
        }

        // ---------------------------------------------------------
        // Stall mate and Check mate not in max depth tests
        // ---------------------------------------------------------

        [TestMethod]
        public void SearchMinimaxTest_WhenWhiteCanEscapeCheck_Depth3_ThenEscape()
        {
            //      start
            //     /a     \
            // -10000x    10x       white move -> highest selected (x)  white is/goes in check -  no legal move (a)
            //   /a         \   
            // -10000       10x       black move -> lowest selected (x)  black captures white king - no legal move (a) eval is not called here!
            // no / moves     \   
            //                10x         white move -> lowest selected (x)  no white king -> no white move possible (a)

            var bestMove = new NormalMove(Piece.MakePiece('Q'), 0, 0, 0, 0, null);
            FakeMoveGeneratorMulitlevel moveGenFake = new FakeMoveGeneratorMulitlevel();
            moveGenFake.AddGetAllMoves(new List<IMove>() { new NormalMove(Piece.MakePiece('P'), 0, 0, 0, 0, null), bestMove }); // level 1
            moveGenFake.AddGetAllMoves(new List<IMove>() { new NormalMove(Piece.MakePiece('b'), 0, 0, 0, 0, null) }); // level 2 a
            moveGenFake.AddGetAllMoves(new List<IMove>() { }); // level 3 a // no white moves possible because there is no white king
            moveGenFake.AddGetAllMoves(new List<IMove>() { new NormalMove(Piece.MakePiece('n'), 0, 0, 0, 0, null) }); // level 2 b
            moveGenFake.AddGetAllMoves(new List<IMove>() { new NormalMove(Piece.MakePiece('N'), 0, 0, 0, 0, null) }); // level 3 b
            moveGenFake.SetIsChecks(new List<bool>() { false, false });
            IEvaluator evalFake = new FakeEvaluator(new List<float>() { 10 }); // eval is not called for a, only b
            IBoard boardFake = new FakeBoard();

            var target = new SearchMinimax(evalFake, moveGenFake);
            target.SetMaxDepth(3);
            var bestRatingActual = target.SearchLevel(boardFake, Definitions.ChessColor.White, 3).First();

            Assert.AreEqual(10, bestRatingActual.Score, "white should escape check mate");
            Assert.AreEqual(bestMove, bestRatingActual.Move, "white should escape check mate");
        }

        [TestMethod]
        public void SearchMinimaxTest_WhenWhiteCheckmateNow_Depth3_ThenNoLegalMove()
        {
            //       start        // white is check mate
            //       -10000
            //      /      \
            //  -10000x   -10000x       white move -> highest selected (x)  white is/goes in check -> no legal
            //    /           \   
            // -10000x      -10000x       black move -> lowest selected (x)  black captures white king -> no legal
            // no / moves   no \ moves   
            //                            black move -> lowest selected (x)  no white king -> no moves returned from move generator

            FakeMoveGeneratorMulitlevel moveGenFake = new FakeMoveGeneratorMulitlevel();
            moveGenFake.AddGetAllMoves(new List<IMove>() { new NormalMove(Piece.MakePiece('P'), 0, 0, 0, 0, null), new NormalMove(Piece.MakePiece('Q'), 0, 0, 0, 0, null) }); // level 1
            moveGenFake.AddGetAllMoves(new List<IMove>() { new NormalMove(Piece.MakePiece('b'), 0, 0, 0, 0, null) }); // level 2 a
            moveGenFake.AddGetAllMoves(new List<IMove>() { }); // level 3 a  no white king -> no moves
            moveGenFake.AddGetAllMoves(new List<IMove>() { new NormalMove(Piece.MakePiece('n'), 0, 0, 0, 0, null) }); // level 2 b
            moveGenFake.AddGetAllMoves(new List<IMove>() { }); // level 3 b  no white king -> no moves
            moveGenFake.SetIsChecks(new List<bool>() { true, true });
            IEvaluator evalFake = new FakeEvaluator(new List<float>()); // eval not called 
            IBoard boardFake = new FakeBoard();

            var target = new SearchMinimax(evalFake, moveGenFake);
            target.SetMaxDepth(3);
            var bestRatingActual = target.SearchLevel(boardFake, Definitions.ChessColor.White, 3).First();

            Assert.AreEqual(-10000, bestRatingActual.Score, "check mate should be score -10000");
            Assert.IsTrue(bestRatingActual.Move is NoLegalMove);
        }

        [TestMethod]
        public void SearchMinimaxTest_WhenWhiteStallMateNow_Depth3_ThenNoLegalMove()
        {
            //       start        // white is stall mate
            //         0
            //      /      \
            //  -10000x   -10000x       white move -> highest selected (x)  white goes in check -> no legal
            //    /           \   
            // -10000x      -10000x       black move -> lowest selected (x)  black captures white king -> no legal
            // no / moves   no \ moves   
            //                            black move -> lowest selected (x)  no white king -> no moves returned from move generator

            FakeMoveGeneratorMulitlevel moveGenFake = new FakeMoveGeneratorMulitlevel();
            moveGenFake.AddGetAllMoves(new List<IMove>() { new NormalMove(Piece.MakePiece('P'), 0, 0, 0, 0, null), new NormalMove(Piece.MakePiece('Q'), 0, 0, 0, 0, null) }); // level 1
            moveGenFake.AddGetAllMoves(new List<IMove>() { new NormalMove(Piece.MakePiece('b'), 0, 0, 0, 0, null) }); // level 2 a
            moveGenFake.AddGetAllMoves(new List<IMove>() { }); // level 3 a  no white king -> no moves
            moveGenFake.AddGetAllMoves(new List<IMove>() { new NormalMove(Piece.MakePiece('n'), 0, 0, 0, 0, null) }); // level 2 b
            moveGenFake.AddGetAllMoves(new List<IMove>() { }); // level 3 b  no white king -> no moves
            moveGenFake.SetIsChecks(new List<bool>() { false, false }); // in start position white is not check
            IEvaluator evalFake = new FakeEvaluator(new List<float>()); // eval not called 
            IBoard boardFake = new FakeBoard();

            var target = new SearchMinimax(evalFake, moveGenFake);
            target.SetMaxDepth(3);
            var bestRatingActual = target.SearchLevel(boardFake, Definitions.ChessColor.White, 3).First();

            Assert.AreEqual(0, bestRatingActual.Score, "stall mate should be score 0");
            Assert.IsTrue(bestRatingActual.Move is NoLegalMove);
        }

        // ---------------------------------------------------------
        // check return value is a Collection
        // ---------------------------------------------------------

        [TestMethod]
        public void SearchMinimaxTest_When2MovesHaveSameRating_ThenReturn2Moves()
        {
            //      start
            //     /      \
            //    1x       1x    white move -> highest selected (x) 
            IEvaluator evalFake = new FakeEvaluator(new List<float>() { 1, 1 });
            FakeMoveGeneratorMulitlevel moveGenFake = new FakeMoveGeneratorMulitlevel();
            moveGenFake.AddGetAllMoves(new List<IMove>() { new NormalMove(Piece.MakePiece('P'), 0, 0, 0, 0, null), new NormalMove(Piece.MakePiece('Q'), 0, 0, 0, 0, null) }); // first level

            IBoard boardFake = new FakeBoard();

            var target = new SearchMinimax(evalFake, moveGenFake);
            var ratings = target.SearchLevel(boardFake, Definitions.ChessColor.White, 1);
            var rating0 = ratings.ElementAt(0);
            var rating1 = ratings.ElementAt(1);

            Assert.AreEqual(2, ratings.Count());

            Assert.AreEqual(1, rating0.Score);
            Assert.AreEqual(new NormalMove(Piece.MakePiece('P'), 0, 0, 0, 0, null), rating0.Move);

            Assert.AreEqual(1, rating1.Score);
            Assert.AreEqual(new NormalMove(Piece.MakePiece('Q'), 0, 0, 0, 0, null), rating1.Move);
        }

        [TestMethod]
        public void SearchMinimaxTest_When2MovesHaveDifferentRating_ThenReturn1Move()
        {
            //      start
            //     /      \
            //    1       2x    white move -> highest selected (x) 
            IEvaluator evalFake = new FakeEvaluator(new List<float>() { 1, 2 });
            FakeMoveGeneratorMulitlevel moveGenFake = new FakeMoveGeneratorMulitlevel();
            moveGenFake.AddGetAllMoves(new List<IMove>() { new NormalMove(Piece.MakePiece('P'), 0, 0, 0, 0, null), new NormalMove(Piece.MakePiece('Q'), 0, 0, 0, 0, null) }); // first level

            IBoard boardFake = new FakeBoard();

            var target = new SearchMinimax(evalFake, moveGenFake);
            var ratings = target.SearchLevel(boardFake, Definitions.ChessColor.White, 1);
            var rating0 = ratings.ElementAt(0);

            Assert.AreEqual(1, ratings.Count());

            Assert.AreEqual(2, rating0.Score);
            Assert.AreEqual(new NormalMove(Piece.MakePiece('Q'), 0, 0, 0, 0, null), rating0.Move);
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

        //[TestMethod]
        //public void SearchTest_WhenCheckmateNow_ThenReturnNoLegalMove()
        //{
        //    // checkmate now --> Search(level 2) -> nolegal move

        //    var scoresMoves = new List<MoveAndScore>()
        //    {
        //        null,
        //        null,
        //        new MoveAndScore() {Move = new NoLegalMove(), Score = -10000}, // level 2: king lost
        //        null,
        //        new MoveAndScore() {Move = new NoLegalMove(), Score = -10000}, // level 4 king lost

        //    };
        //    var target = new SearchMinimaxDouble_SearchLevelOverwritten(null, null, scoresMoves.ToArray());
        //    target.SetMaxDepth(4);

        //    float actualScore;
        //    var actualMove = target.Search(null, Definitions.ChessColor.Empty, out actualScore);

        //    Assert.AreEqual(new NoLegalMove(), actualMove);
        //    Assert.AreEqual(-10000, actualScore);
        //}

        //[TestMethod]
        //public void SearchTest_WhenCheckmateIn2_ThenReturnNormalMove()
        //{
        //    // checkmate in 2  --> SearchLevel(2) --> normal move
        //    //                 --> SearchLevel(4) --> NoLegalMove

        //    var scoresMoves = new List<MoveAndScore>()
        //    {
        //        null,
        //        null,
        //        new MoveAndScore() {Move = new NormalMove(Piece.MakePiece('Q'), 0, 0, 0, 0, null), Score = 0}, // level 2: checkmate
        //        null,
        //        new MoveAndScore() {Move = new NoLegalMove(), Score = -10000}, // level 4: king lost

        //    };
        //    var target = new SearchMinimaxDouble_SearchLevelOverwritten(null, null, scoresMoves.ToArray());
        //    target.SetMaxDepth(4);

        //    float actualScore;
        //    var actualMove = target.Search(null, Definitions.ChessColor.Empty, out actualScore);

        //    Assert.AreEqual(new NormalMove(Piece.MakePiece('Q'), 0, 0, 0, 0, null), actualMove);
        //    Assert.AreEqual(0, actualScore);
        //}

        //[TestMethod]
        //public void SearchTest_WhenCheckmateIn4MaxDepth6_ThenReturnNormalMove()
        //{
        //    // checkmate in 4  --> SearchLevel(2) --> normal move
        //    //                 --> SearchLevel(4) --> normal Move  <-- select this
        //    //                 --> SearchLevel(6) --> NoLegalMove

        //    var scoresMoves = new List<MoveAndScore>()
        //    {
        //        null,
        //        null,
        //        new MoveAndScore() {Move = new NormalMove(Piece.MakePiece('P'), 0, 0, 0, 0, null), Score = 1}, // level 2: checkmate in 2
        //        null,
        //        new MoveAndScore() {Move = new NormalMove(Piece.MakePiece('Q'), 0, 0, 0, 0, null), Score = 2}, // level 4: check mate
        //        null,
        //        new MoveAndScore() {Move = new NoLegalMove(), Score = -10000}, // level 6: lost king

        //    };
        //    var target = new SearchMinimaxDouble_SearchLevelOverwritten(null, null, scoresMoves.ToArray());
        //    target.SetMaxDepth(6);

        //    float actualScore;
        //    var actualMove = target.Search(null, Definitions.ChessColor.Empty, out actualScore);

        //    Assert.AreEqual(new NormalMove(Piece.MakePiece('Q'), 0, 0, 0, 0, null), actualMove);
        //    Assert.AreEqual(2, actualScore);
        //}

        //[TestMethod]
        //public void SearchTest_WhenCheckmateIn2MaxDepth6_ThenReturnNormalMove()
        //{
        //    // checkmate in 2  --> SearchLevel(2) --> normal move  <-- select this
        //    //                 --> SearchLevel(4) --> NoLegalMove
        //    //                 --> SearchLevel(6) --> NoLegalMove

        //    var scoresMoves = new List<MoveAndScore>()
        //    {
        //        null,
        //        null,
        //        new MoveAndScore() {Move = new NormalMove(Piece.MakePiece('P'), 0, 0, 0, 0, null), Score = 1}, // level 2: checkmate
        //        null,
        //        new MoveAndScore() {Move = new NoLegalMove(), Score = -10000}, // level 4: lost king
        //        null,
        //        new MoveAndScore() {Move = new NoLegalMove(), Score = -10000}, // level 6: lost king

        //    };
        //    var target = new SearchMinimaxDouble_SearchLevelOverwritten(null, null, scoresMoves.ToArray());
        //    target.SetMaxDepth(6);

        //    float actualScore;
        //    var actualMove = target.Search(null, Definitions.ChessColor.Empty, out actualScore);

        //    Assert.AreEqual(new NormalMove(Piece.MakePiece('P'), 0, 0, 0, 0, null), actualMove);
        //    Assert.AreEqual(1, actualScore);
        //}

        //[TestMethod]
        //public void SearchTest_WhenCheckmateIn2MaxDepth3_ThenReturnNormalMove()
        //{
        //    // checkmate in 2  --> SearchLevel(2) --> normal move  <-- select this
        //    //                 --> SearchLevel(3) --> NoLegalMove
        //    //                 --> SearchLevel(4) --> NoLegalMove

        //    var scoresMoves = new List<MoveAndScore>()
        //    {
        //        null,
        //        null,
        //        new MoveAndScore() {Move = new NoLegalMove(), Score = -10000}, // level 2: lost king
        //        new MoveAndScore() {Move = new NoLegalMove(), Score = -10000}, // level 3: lost king

        //    };
        //    var target = new SearchMinimaxDouble_SearchLevelOverwritten(null, null, scoresMoves.ToArray());
        //    target.SetMaxDepth(3);

        //    float actualScore;
        //    var actualMove = target.Search(null, Definitions.ChessColor.Empty, out actualScore);

        //    Assert.AreEqual(new NoLegalMove(), actualMove);
        //    Assert.AreEqual(-10000, actualScore);
        //}

        //[TestMethod]
        //public void SearchTest_WhenCheckmateNow_Level3_ThenReturnNormalMove()
        //{
        //    // check mate in 2  --> SearchLevel(3) --> NoLegalMove
        //    //                  --> SearchLevel(2) --> NoLegalMove

        //    var scoresMoves = new List<MoveAndScore>()
        //    {
        //        null,
        //        null,
        //        new MoveAndScore() {Move = new NoLegalMove(), Score = -10001}, // level 2: lost king
        //        new MoveAndScore() {Move = new NoLegalMove(), Score = -10000}, // level 3: lost king

        //    };
        //    var target = new SearchMinimaxDouble_SearchLevelOverwritten(null, null, scoresMoves.ToArray());
        //    target.SetMaxDepth(3);

        //    float actualScore;
        //    var actualMove = target.Search(null, Definitions.ChessColor.Empty, out actualScore);

        //    Assert.AreEqual(new NoLegalMove(), actualMove);
        //    Assert.AreEqual(-10000, actualScore);
        //}

        //[TestMethod]
        //public void SearchTest_WhenCheckmatIn2_Level5_ThenReturnNormalMove()
        //{
        //    // check mate in 2  --> SearchLevel(5) --> NoLegalMove
        //    //                  --> SearchLevel(2) --> Normal move

        //    var scoresMoves = new List<MoveAndScore>()
        //    {
        //        null,
        //        null,
        //        new MoveAndScore() {Move = new NormalMove(Piece.MakePiece('Q'),0,0,0,0,null), Score = 1}, // level 2: lost king
        //        null,
        //        null,
        //        new MoveAndScore() {Move = new NoLegalMove(), Score = -10000}, // level 5: lost king
        //    };
        //    var target = new SearchMinimaxDouble_SearchLevelOverwritten(null, null, scoresMoves.ToArray());
        //    target.SetMaxDepth(5);

        //    float actualScore;
        //    var actualMove = target.Search(null, Definitions.ChessColor.Empty, out actualScore);

        //    Assert.AreEqual(new NormalMove(Piece.MakePiece('Q'),0,0,0,0,null), actualMove);
        //    Assert.AreEqual(1, actualScore);
        //}

        //[TestMethod]
        //public void SearchTest_WhenStallmateIn2_Level2_ThenReturnNoLegalMove()
        //{
        //    // stall mate in 2 --> SearchLevel(2) -> nolegal move, score 0

        //    var scoresMoves = new List<MoveAndScore>()
        //    {
        //        null,
        //        null,
        //        new MoveAndScore() {Move = new NoLegalMove(), Score = 0}, // level 2: lost king
        //    };

        //    var target = new SearchMinimaxDouble_SearchLevelOverwritten(null, null, scoresMoves.ToArray());
        //    target.SetMaxDepth(2);

        //    float actualScore;
        //    var actualMove = target.Search(null, Definitions.ChessColor.White, out actualScore);

        //    Assert.AreEqual(new NoLegalMove(),  actualMove);
        //    Assert.AreEqual(0, actualScore);
        //}

        //[TestMethod]
        //public void SearchTest_WhenStallmateIn4_Level6_ThenReturnNoLegalMove()
        //{
        //    // stall mate in 2 --> SearchLevel(2) -> Normal move, score x
        //    // stall mate in 2 --> SearchLevel(4) -> nolegal move, score 0
        //    // stall mate in 2 --> SearchLevel(6) -> nolegal move, score 0

        //    var scoresMoves = new List<MoveAndScore>()
        //    {
        //        null,
        //        null,
        //        new MoveAndScore() {Move = new NormalMove(Piece.MakePiece('Q'),0,0,0,0,null), Score = 1}, // level 2: lost king
        //        null,
        //        new MoveAndScore() {Move = new NoLegalMove(), Score = 0}, // level 4: lost king
        //        null,
        //        new MoveAndScore() {Move = new NoLegalMove(), Score = 0}, // level 6: lost king
        //    };

        //    var target = new SearchMinimaxDouble_SearchLevelOverwritten(null, null, scoresMoves.ToArray());
        //    target.SetMaxDepth(6);

        //    float actualScore;
        //    var actualMove = target.Search(null, Definitions.ChessColor.White, out actualScore);

        //    Assert.AreEqual(new NormalMove(Piece.MakePiece('Q'), 0, 0, 0, 0, null), actualMove);
        //    Assert.AreEqual(1, actualScore);
        //}

        //[TestMethod]
        //public void SearchTest_WhenCheckmateIn1_Depth3_ThenNormalMove()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void SearchTest_WhenStallmateIn1_Depth3_ThenNormalMove()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void SearchLevelTest_WhenKingLostAndNotMaxDepthReached_then()
        //{
        //    Assert.Fail();
        //}






    }


}
