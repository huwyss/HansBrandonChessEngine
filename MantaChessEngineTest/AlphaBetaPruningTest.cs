using System.Linq;
using System.Collections.Generic;
using MantaChessEngine;
using MantaChessEngine.Doubles;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MantaChessEngineTest
{
    /// <summary>
    /// Tests for SearchAlphaBeta with real board.
    /// </summary>

    [TestClass]
    public class AlphaBetaPruningTest
    {
        [TestMethod]
        public void SearchAlphaBetaTest_PruningForBlack()
        {
            //      start
            //     /      \
            //    3x       5      black move -> lowest selected (x) 
            //  /  \     /  \   
            // -1   3x  5   noeval  white move -> highest selected (x)
            var evalFake = new FakeEvaluator(new List<float>() { -1, 3, 5, 100 /*noeval*/ });
            FakeMoveGeneratorMulitlevel moveGenFake = new FakeMoveGeneratorMulitlevel();
            IMove bestMove = MoveMaker.Black(1, 1);
            moveGenFake.AddGetAllMoves(new List<IMove>() { bestMove, MoveMaker.Black(1, 2) }); // first level
            moveGenFake.AddGetAllMoves(new List<IMove>() { MoveMaker.White(2, 1), MoveMaker.White(2, 2) }); // second level 1.
            moveGenFake.AddGetAllMoves(new List<IMove>() { MoveMaker.White(2, 3), MoveMaker.White(2, 4) }); // second level 2.

            IBoard boardFake = new FakeBoard();

            var target = new SearchAlphaBeta(evalFake, moveGenFake, 2, null);
            var bestRatingActual = target.SearchLevel(boardFake, Definitions.ChessColor.Black, 1, -100000, 100000);

            Assert.AreEqual(3, bestRatingActual.Score);
            Assert.AreEqual(bestMove, bestRatingActual.Move);
            Assert.AreEqual(3, evalFake.EvaluateCalledCounter);
        }

        [TestMethod]
        public void SearchAlphaBetaTest_NoPruningForBlack()
        {
            //      start
            //     /      \
            //    3x       5      black move -> lowest selected (x) 
            //  /  \     /  \   
            // -1   3x  2    5    white move -> highest selected (x)
            var evalFake = new FakeEvaluator(new List<float>() { -1, 3, 2, 5 });
            FakeMoveGeneratorMulitlevel moveGenFake = new FakeMoveGeneratorMulitlevel();
            IMove bestMove = MoveMaker.Black(1, 1);
            moveGenFake.AddGetAllMoves(new List<IMove>() { bestMove, MoveMaker.Black(1, 2) }); // first level
            moveGenFake.AddGetAllMoves(new List<IMove>() { MoveMaker.White(2, 1), MoveMaker.White(2, 2) }); // second level 1.
            moveGenFake.AddGetAllMoves(new List<IMove>() { MoveMaker.White(2, 3), MoveMaker.White(2, 4) }); // second level 2.

            IBoard boardFake = new FakeBoard();

            var target = new SearchAlphaBeta(evalFake, moveGenFake, 2, null);
            var bestRatingActual = target.SearchLevel(boardFake, Definitions.ChessColor.Black, 1, -100000, 100000);

            Assert.AreEqual(3, bestRatingActual.Score);
            Assert.AreEqual(bestMove, bestRatingActual.Move);
            Assert.AreEqual(4, evalFake.EvaluateCalledCounter);
        }

        [TestMethod]
        public void SearchAlphaBetaTest_PruningForWhite()
        {
            //      start
            //     /      \
            //   -3x      -5         white move -> highest selected (x) 
            //  /  \     /  \   
            //  1  -3x  -5x  noeval  black move -> lowest selected (x)
            var evalFake = new FakeEvaluator(new List<float>() { 1, -3, -5, 100 /*noeval*/ });
            FakeMoveGeneratorMulitlevel moveGenFake = new FakeMoveGeneratorMulitlevel();
            IMove bestMove = MoveMaker.White(1, 1);
            moveGenFake.AddGetAllMoves(new List<IMove>() { bestMove, MoveMaker.White(1, 2) }); // first level
            moveGenFake.AddGetAllMoves(new List<IMove>() { MoveMaker.Black(2, 1), MoveMaker.Black(2, 2) }); // second level 1.
            moveGenFake.AddGetAllMoves(new List<IMove>() { MoveMaker.Black(2, 3), MoveMaker.Black(2, 4) }); // second level 2.

            IBoard boardFake = new FakeBoard();

            var target = new SearchAlphaBeta(evalFake, moveGenFake, 2, null);
            var bestRatingActual = target.SearchLevel(boardFake, Definitions.ChessColor.White, 1, -100000, 100000);

            Assert.AreEqual(-3, bestRatingActual.Score);
            Assert.AreEqual(bestMove, bestRatingActual.Move);
            Assert.AreEqual(3, evalFake.EvaluateCalledCounter);
        }

        [TestMethod]
        public void SearchAlphaBetaTest_NoPruningForWhite()
        {
            //      start
            //     /      \
            //   -3x      -5      white move -> highest selected (x) 
            //  /  \     /  \   
            // 1   -3x  -2  -5    black move -> lowest selected (x)
            var evalFake = new FakeEvaluator(new List<float>() { 1, -3, -2, -5 });
            FakeMoveGeneratorMulitlevel moveGenFake = new FakeMoveGeneratorMulitlevel();
            IMove bestMove = MoveMaker.White(1, 1);
            moveGenFake.AddGetAllMoves(new List<IMove>() { bestMove, MoveMaker.White(1, 2) }); // first level
            moveGenFake.AddGetAllMoves(new List<IMove>() { MoveMaker.Black(2, 1), MoveMaker.Black(2, 2) }); // second level 1.
            moveGenFake.AddGetAllMoves(new List<IMove>() { MoveMaker.Black(2, 3), MoveMaker.Black(2, 4) }); // second level 2.

            IBoard boardFake = new FakeBoard();

            var target = new SearchAlphaBeta(evalFake, moveGenFake, 2, null);
            var bestRatingActual = target.SearchLevel(boardFake, Definitions.ChessColor.White, 1, -100000, 100000);

            Assert.AreEqual(-3, bestRatingActual.Score);
            Assert.AreEqual(bestMove, bestRatingActual.Move);
            Assert.AreEqual(4, evalFake.EvaluateCalledCounter);
        }

        [TestMethod]
        public void SearchAlphaBetaTest_WhenWhiteMovesFirstAnd3Levels_ThenCorrectSelected()
        {
            //           start
            //        /          \
            //       2            4x         white move -> highest selected (x) 
            //    /    \        /    \   
            //   2x     8      6      4x     black move -> lowest selected (x)
            //  / \    / \    / \    / \
            // 2x  1  7  8x  5  6x  3  4x    white move -> highest selected (x)
            //          noe
            var evalFake = new FakeEvaluator(new List<float>() { 2, 1, 7, /*noe 8,*/ 5, 6, 3, 4 }); // lowest level evaluation
            FakeMoveGeneratorMulitlevel moveGenFake = new FakeMoveGeneratorMulitlevel();
            IMove bestMove = MoveMaker.White(1, 2);
            moveGenFake.AddGetAllMoves(new List<IMove>() { MoveMaker.White(1, 1), bestMove }); // 1. level (white)
            moveGenFake.AddGetAllMoves(new List<IMove>() { MoveMaker.Black(2, 1), MoveMaker.Black(1, 2) }); // 2. level a (black)
            moveGenFake.AddGetAllMoves(new List<IMove>() { MoveMaker.White(3, 1), MoveMaker.White(3, 2) }); // 3. level a (white)
            moveGenFake.AddGetAllMoves(new List<IMove>() { MoveMaker.White(3, 3), MoveMaker.White(3, 3) }); // 3. level b (white)
            moveGenFake.AddGetAllMoves(new List<IMove>() { MoveMaker.Black(2, 3), MoveMaker.Black(2, 4) }); // 2. level b (black)
            moveGenFake.AddGetAllMoves(new List<IMove>() { MoveMaker.White(3, 5), MoveMaker.White(3, 6) }); // 3. level c (white)
            moveGenFake.AddGetAllMoves(new List<IMove>() { MoveMaker.White(3, 7), MoveMaker.White(3, 8) }); // 3. level d (white)

            IBoard boardFake = new FakeBoard();

            var target = new SearchAlphaBeta(evalFake, moveGenFake, 3, null);
            var bestRatingActual = target.SearchLevel(boardFake, Definitions.ChessColor.White, 1, -100000, 100000);

            Assert.AreEqual(4, bestRatingActual.Score);
            Assert.AreEqual(bestMove, bestRatingActual.Move);
            Assert.AreEqual(7, evalFake.EvaluateCalledCounter);
        }

        [TestMethod]
        public void SearchAlphaBetaTest_PruningForLevel3Tree_White()
        {
            //              start
            //         /              \
            //       3x               -4           white move -> highest selected (x) 
            //     /    \           /     \   
            //    3x     5        -4      noe      black move -> lowest selected (x)
            //   / \    / \       / \     / \
            // -1  3x  5x  noe  -6 -4x  noe  noe   white move -> highest selected (x) 
            var evalFake = new FakeEvaluator(new List<float>() { -1, 3, 5, /*noe,*/ -6, -4, /*noe,*/ /*noe*/ }); // lowest level evaluation
            FakeMoveGeneratorMulitlevel moveGenFake = new FakeMoveGeneratorMulitlevel();
            IMove bestMove = MoveMaker.White(1, 1);
            moveGenFake.AddGetAllMoves(new List<IMove>() { bestMove, MoveMaker.White(1, 2),  }); // 1. level (white)
            moveGenFake.AddGetAllMoves(new List<IMove>() { MoveMaker.Black(2, 1), MoveMaker.Black(1, 2) }); // 2. level a (black)
            moveGenFake.AddGetAllMoves(new List<IMove>() { MoveMaker.White(3, 1), MoveMaker.White(3, 2) }); // 3. level a (white)
            moveGenFake.AddGetAllMoves(new List<IMove>() { MoveMaker.White(3, 3), MoveMaker.White(3, 3) }); // 3. level b (white)
            moveGenFake.AddGetAllMoves(new List<IMove>() { MoveMaker.Black(2, 3), MoveMaker.Black(2, 4) }); // 2. level b (black)
            moveGenFake.AddGetAllMoves(new List<IMove>() { MoveMaker.White(3, 5), MoveMaker.White(3, 6) }); // 3. level c (white)
            moveGenFake.AddGetAllMoves(new List<IMove>() { MoveMaker.White(3, 7), MoveMaker.White(3, 8) }); // 3. level d (white)

            IBoard boardFake = new FakeBoard();

            var target = new SearchAlphaBeta(evalFake, moveGenFake, 3, null);
            var bestRatingActual = target.SearchLevel(boardFake, Definitions.ChessColor.White, 1, -100000, 100000);

            Assert.AreEqual(3, bestRatingActual.Score);
            Assert.AreEqual(bestMove, bestRatingActual.Move);
            Assert.AreEqual(5, evalFake.EvaluateCalledCounter);
        }
    }
}
