﻿using System.Linq;
using System.Collections.Generic;
using HansBrandonChessEngine;
using HBCommon;
using HansBrandonChessEngineTest.Doubles;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace HansBrandonChessEngineTest
{
    /// <summary>
    /// Tests for SearchAlphaBeta with real board.
    /// </summary>

    [TestClass]
    public class AlphaBetaPruningTest
    {
        const int AlphaStart = int.MinValue;
        const int BetaStart = int.MaxValue;

        FakeMoveGeneratorMulitlevel moveGenFake;
        IHashtable hashMock;
        IBoard boardMock;
        MoveRatingFactory moveRatingFactory;

        [TestInitialize]
        public void Setup()
        {
            moveGenFake = new FakeMoveGeneratorMulitlevel();
            hashMock = new Mock<IHashtable>().Object;
            boardMock = new Mock<IBoard>().Object;
            moveRatingFactory = new MoveRatingFactory(moveGenFake);
        }

        [TestMethod]
        public void SearchAlphaBetaTest_PruningForBlack()
        {
            //      start
            //     /      \
            //    3x       5      black move -> lowest selected (x) 
            //  /  \     /  \   
            // -1   3x  5   noeval  white move -> highest selected (x)
            var evalFake = new FakeEvaluator(new List<int>() { -100, 300, 500, 10000 /*not evaluated (pruning)*/ });

            IMove bestMove = MoveMaker.Black(1, 1);
            moveGenFake.AddGetAllMoves(new List<IMove>() { bestMove, MoveMaker.Black(1, 2) }); // first level
            moveGenFake.AddGetAllMoves(new List<IMove>() { MoveMaker.White(2, 1), MoveMaker.White(2, 2) }); // second level 1.
            moveGenFake.AddGetAllMoves(new List<IMove>() { MoveMaker.White(2, 3), MoveMaker.White(2, 4) }); // second level 2.
            moveGenFake.SetIsChecks(ChessColor.White, new List<bool>() { false, false });
            moveGenFake.SetIsChecks(ChessColor.Black, new List<bool>() { false, false, false });

            var target = new GenericSearchAlphaBeta<IMove>(boardMock, evalFake, moveGenFake, hashMock, null, moveRatingFactory, 2);
            var bestRatingActual = target.SearchLevel(ChessColor.Black, 2, AlphaStart, BetaStart);

            Assert.AreEqual(300, bestRatingActual.Score);
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
            var evalFake = new FakeEvaluator(new List<int>() { -100, 300, 200, 500 });

            IMove bestMove = MoveMaker.Black(1, 1);
            moveGenFake.AddGetAllMoves(new List<IMove>() { bestMove, MoveMaker.Black(1, 2) }); // first level
            moveGenFake.AddGetAllMoves(new List<IMove>() { MoveMaker.White(2, 1), MoveMaker.White(2, 2) }); // second level 1.
            moveGenFake.AddGetAllMoves(new List<IMove>() { MoveMaker.White(2, 3), MoveMaker.White(2, 4) }); // second level 2.
            moveGenFake.SetIsChecks(ChessColor.White, new List<bool>() { false, false });
            moveGenFake.SetIsChecks(ChessColor.Black, new List<bool>() { false, false, false, false });

            var target = new GenericSearchAlphaBeta<IMove>(boardMock, evalFake, moveGenFake, hashMock, null, moveRatingFactory, 2);
            var bestRatingActual = target.SearchLevel(ChessColor.Black, 2, AlphaStart, BetaStart);

            Assert.AreEqual(300, bestRatingActual.Score);
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
            var evalFake = new FakeEvaluator(new List<int>() { 100, -300, -500, 10000 /*noeval*/ });
            
            IMove bestMove = MoveMaker.White(1, 1);
            moveGenFake.AddGetAllMoves(new List<IMove>() { bestMove, MoveMaker.White(1, 2) }); // first level
            moveGenFake.AddGetAllMoves(new List<IMove>() { MoveMaker.Black(2, 1), MoveMaker.Black(2, 2) }); // second level 1.
            moveGenFake.AddGetAllMoves(new List<IMove>() { MoveMaker.Black(2, 3), MoveMaker.Black(2, 4) }); // second level 2.
            moveGenFake.SetIsChecks(ChessColor.White, new List<bool>() { false, false });
            moveGenFake.SetIsChecks(ChessColor.Black, new List<bool>() { false, false, false });

            var target = new GenericSearchAlphaBeta<IMove>(boardMock, evalFake, moveGenFake, hashMock, null, moveRatingFactory, 2);
            var bestRatingActual = target.SearchLevel(ChessColor.White, 2, AlphaStart, BetaStart);

            Assert.AreEqual(-300, bestRatingActual.Score);
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
            var evalFake = new FakeEvaluator(new List<int>() { 100, -300, -200, -500 });
            
            IMove bestMove = MoveMaker.White(1, 1);
            moveGenFake.AddGetAllMoves(new List<IMove>() { bestMove, MoveMaker.White(1, 2) }); // first level
            moveGenFake.AddGetAllMoves(new List<IMove>() { MoveMaker.Black(2, 1), MoveMaker.Black(2, 2) }); // second level 1.
            moveGenFake.AddGetAllMoves(new List<IMove>() { MoveMaker.Black(2, 3), MoveMaker.Black(2, 4) }); // second level 2.
            moveGenFake.SetIsChecks(ChessColor.White, new List<bool>() { false, false });
            moveGenFake.SetIsChecks(ChessColor.Black, new List<bool>() { false, false, false, false });


            var target = new GenericSearchAlphaBeta<IMove>(boardMock, evalFake, moveGenFake, hashMock, null, moveRatingFactory, 2);
            var bestRatingActual = target.SearchLevel(ChessColor.White, 2, AlphaStart, BetaStart);

            Assert.AreEqual(-300, bestRatingActual.Score);
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
            var evalFake = new FakeEvaluator(new List<int>() { 200, 100, 700, /*noe 800,*/ 500, 600, 300, 400 }); // lowest level evaluation
            
            IMove bestMove = MoveMaker.White(1, 2);
            moveGenFake.AddGetAllMoves(new List<IMove>() { MoveMaker.White(1, 1), bestMove }); // 1. level (white)
            moveGenFake.AddGetAllMoves(new List<IMove>() { MoveMaker.Black(2, 1), MoveMaker.Black(1, 2) }); // 2. level a (black)
            moveGenFake.AddGetAllMoves(new List<IMove>() { MoveMaker.White(3, 1), MoveMaker.White(3, 2) }); // 3. level a (white)
            moveGenFake.AddGetAllMoves(new List<IMove>() { MoveMaker.White(3, 3), MoveMaker.White(3, 3) }); // 3. level b (white)
            moveGenFake.AddGetAllMoves(new List<IMove>() { MoveMaker.Black(2, 3), MoveMaker.Black(2, 4) }); // 2. level b (black)
            moveGenFake.AddGetAllMoves(new List<IMove>() { MoveMaker.White(3, 5), MoveMaker.White(3, 6) }); // 3. level c (white)
            moveGenFake.AddGetAllMoves(new List<IMove>() { MoveMaker.White(3, 7), MoveMaker.White(3, 8) }); // 3. level d (white)
            moveGenFake.SetIsChecks(ChessColor.White, new List<bool>() { false, false, false, false, false, false, false, false, false });
            moveGenFake.SetIsChecks(ChessColor.Black, new List<bool>() { false, false, false, false });


            var target = new GenericSearchAlphaBeta<IMove>(boardMock, evalFake, moveGenFake, hashMock, null, moveRatingFactory, 3);
            var bestRatingActual = target.SearchLevel(ChessColor.White, 3, AlphaStart, BetaStart);

            Assert.AreEqual(400, bestRatingActual.Score);
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
            var evalFake = new FakeEvaluator(new List<int>() { -100, 300, 500, /*noe,*/ -600, -400, /*noe,*/ /*noe*/ }); // lowest level evaluation
            
            IMove bestMove = MoveMaker.White(1, 1);
            moveGenFake.AddGetAllMoves(new List<IMove>() { bestMove, MoveMaker.White(1, 2),  }); // 1. level (white)
            moveGenFake.AddGetAllMoves(new List<IMove>() { MoveMaker.Black(2, 1), MoveMaker.Black(1, 2) }); // 2. level a (black)
            moveGenFake.AddGetAllMoves(new List<IMove>() { MoveMaker.White(3, 1), MoveMaker.White(3, 2) }); // 3. level a (white)
            moveGenFake.AddGetAllMoves(new List<IMove>() { MoveMaker.White(3, 3), MoveMaker.White(3, 3) }); // 3. level b (white)
            moveGenFake.AddGetAllMoves(new List<IMove>() { MoveMaker.Black(2, 3), MoveMaker.Black(2, 4) }); // 2. level b (black)
            moveGenFake.AddGetAllMoves(new List<IMove>() { MoveMaker.White(3, 5), MoveMaker.White(3, 6) }); // 3. level c (white)
            moveGenFake.AddGetAllMoves(new List<IMove>() { MoveMaker.White(3, 7), MoveMaker.White(3, 8) }); // 3. level d (white)
            moveGenFake.SetIsChecks(ChessColor.White, new List<bool>() { false, false, false, false, false, false, false });
            moveGenFake.SetIsChecks(ChessColor.Black, new List<bool>() { false, false, false });

            var target = new GenericSearchAlphaBeta<IMove>(boardMock, evalFake, moveGenFake, hashMock, null, moveRatingFactory, 3);
            var bestRatingActual = target.SearchLevel(ChessColor.White, 3, AlphaStart, BetaStart);

            Assert.AreEqual(300, bestRatingActual.Score);
            Assert.AreEqual(bestMove, bestRatingActual.Move);
            Assert.AreEqual(5, evalFake.EvaluateCalledCounter);
        }
    }
}
