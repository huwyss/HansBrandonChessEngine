using System.Linq;
using System.Collections.Generic;
using MantaChessEngine;
using MantaChessEngine.Doubles;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MantaChessEngineTest
{
    /// <summary>
    /// Tests for SearchMinimax with mock moves and evaluation: normal moves.
    /// </summary>
    
    [TestClass]
    public class SearchMinimaxTestsWithMocks_NormalMoves
    {
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
            moveGenFake.AddGetAllMoves(new List<IMove>() {new NormalMove(Piece.MakePiece('b'), 0, 0, 0, 0, null), new NormalMove(Piece.MakePiece('n'), 0,0,0,0,null)}); // second level 1.
            moveGenFake.AddGetAllMoves(new List<IMove>() {new NormalMove(Piece.MakePiece('k'), 0, 0, 0, 0, null), new NormalMove(Piece.MakePiece('r'), 0,0,0,0,null)}); // second level 2.

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

        [TestMethod]
        public void SearchMinimaxTest_When2MovesInTolerance_secondIsBiggest_ThenReturn2Moves()
        {
            //       start
            //     /   |    \
            //    1  1.99x   2x    white move -> highest selected (x) 
            IEvaluator evalFake = new FakeEvaluator(new List<float>() { 1, 1.99f, 2 });
            FakeMoveGeneratorMulitlevel moveGenFake = new FakeMoveGeneratorMulitlevel();
            moveGenFake.AddGetAllMoves(new List<IMove>() {
                new NormalMove(Piece.MakePiece('P'), 0, 0, 0, 0, null),
                new NormalMove(Piece.MakePiece('R'), 0, 0, 0, 0, null),
                new NormalMove(Piece.MakePiece('Q'), 0, 0, 0, 0, null) });

            IBoard boardFake = new FakeBoard();

            var target = new SearchMinimax(evalFake, moveGenFake);
            var ratings = target.SearchLevel(boardFake, Definitions.ChessColor.White, 1);
            var rating0 = ratings.ElementAt(0);
            var rating1 = ratings.ElementAt(1);

            Assert.AreEqual(2, ratings.Count());

            Assert.AreEqual(1.99f, rating0.Score);
            Assert.AreEqual(new NormalMove(Piece.MakePiece('R'), 0, 0, 0, 0, null), rating0.Move);

            Assert.AreEqual(2, rating1.Score);
            Assert.AreEqual(new NormalMove(Piece.MakePiece('Q'), 0, 0, 0, 0, null), rating1.Move);
        }

        [TestMethod]
        public void SearchMinimaxTest_When2MovesInTolerance_FirstIsBiggest_ThenReturn2Moves()
        {
            //       start
            //     /  /    |    \
            //    1  1.01  2x  1.99x    white move -> highest selected (x) 
            IEvaluator evalFake = new FakeEvaluator(new List<float>() { 1, 1.01f, 2, 1.99f });
            FakeMoveGeneratorMulitlevel moveGenFake = new FakeMoveGeneratorMulitlevel();
            moveGenFake.AddGetAllMoves(new List<IMove>() {
                new NormalMove(Piece.MakePiece('P'), 0, 0, 0, 0, null),
                new NormalMove(Piece.MakePiece('P'), 0, 0, 0, 0, null),
                new NormalMove(Piece.MakePiece('R'), 0, 0, 0, 0, null),
                new NormalMove(Piece.MakePiece('Q'), 0, 0, 0, 0, null) });

            IBoard boardFake = new FakeBoard();

            var target = new SearchMinimax(evalFake, moveGenFake);
            var ratings = target.SearchLevel(boardFake, Definitions.ChessColor.White, 1);
            var rating0 = ratings.ElementAt(0);
            var rating1 = ratings.ElementAt(1);

            Assert.AreEqual(2, ratings.Count());

            Assert.AreEqual(2, rating0.Score);
            Assert.AreEqual(new NormalMove(Piece.MakePiece('R'), 0, 0, 0, 0, null), rating0.Move);

            Assert.AreEqual(1.99f, rating1.Score);
            Assert.AreEqual(new NormalMove(Piece.MakePiece('Q'), 0, 0, 0, 0, null), rating1.Move);
        }

        [TestMethod]
        public void SearchMinimaxTest_When2MovesInTolerance_secondIsBiggest_ThenReturn2Moves_blackMoves()
        {
            //       start
            //     /   |    \
            //    1  -1.99   -2x    black move -> lowest selected (x) 
            IEvaluator evalFake = new FakeEvaluator(new List<float>() { 1, -1.99f, -2 });
            FakeMoveGeneratorMulitlevel moveGenFake = new FakeMoveGeneratorMulitlevel();
            moveGenFake.AddGetAllMoves(new List<IMove>() {
                new NormalMove(Piece.MakePiece('p'), 0, 0, 0, 0, null),
                new NormalMove(Piece.MakePiece('r'), 0, 0, 0, 0, null),
                new NormalMove(Piece.MakePiece('q'), 0, 0, 0, 0, null) });

            IBoard boardFake = new FakeBoard();

            var target = new SearchMinimax(evalFake, moveGenFake);
            var ratings = target.SearchLevel(boardFake, Definitions.ChessColor.Black, 1);
            var rating0 = ratings.ElementAt(0);
            var rating1 = ratings.ElementAt(1);

            Assert.AreEqual(2, ratings.Count());

            Assert.AreEqual(-1.99f, rating0.Score);
            Assert.AreEqual(new NormalMove(Piece.MakePiece('r'), 0, 0, 0, 0, null), rating0.Move);

            Assert.AreEqual(-2, rating1.Score);
            Assert.AreEqual(new NormalMove(Piece.MakePiece('q'), 0, 0, 0, 0, null), rating1.Move);
        }
    }
}
