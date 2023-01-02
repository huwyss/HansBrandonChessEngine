using System.Linq;
using System.Collections.Generic;
using MantaChessEngine;
using MantaChessEngineTest.Doubles;
using MantaCommon;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MantaChessEngineTest
{
    [TestClass]
    public class SelectiveDepthSearchTest
    {
        [TestMethod]
        public void SearchAlphaBetaTest_NoPruningForWhite_PrincipalVariationMovesInRating()
        {
            //      start
            //     /      \
            //   -3x      -5      white move -> highest selected (x) 
            //  /  \     /  \   
            // 1   -3x  -2  -5    black move -> lowest selected (x)
            var evalFake = new FakeEvaluator(new List<int>() { 100, -300, -200, -500 });
            var moveGenFake = new FakeMoveGeneratorMulitlevel();
            var bestMoveWhite = MoveMaker.White(1, 1);
            var bestMoveBlack = MoveMaker.Black(2, 2);
            moveGenFake.AddGetAllMoves(new List<IMove>() { bestMoveWhite, MoveMaker.White(1, 2) }); // first level
            moveGenFake.AddGetAllMoves(new List<IMove>() { MoveMaker.Black(2, 1), bestMoveBlack }); // second level 1.
            moveGenFake.AddGetAllMoves(new List<IMove>() { MoveMaker.Black(2, 3), MoveMaker.Black(2, 4) }); // second level 2.
            moveGenFake.SetIsChecks(new List<bool>() { false, false, false, false, false, false });

            IBoard boardFake = new FakeBoard();

            var target = new SearchAlphaBeta(boardFake, evalFake, moveGenFake, 2, null, null);
            var bestRatingActual = target.SearchLevel(ChessColor.White, 1, -100000, 100000);

            Assert.AreEqual(-300, bestRatingActual.Score);
            Assert.AreEqual(bestMoveWhite, bestRatingActual.Move);
            
            Assert.AreEqual(2, bestRatingActual.PrincipalVariation.Count);
            Assert.AreEqual(bestMoveWhite, bestRatingActual.PrincipalVariation[0]);
            Assert.AreEqual(bestMoveBlack, bestRatingActual.PrincipalVariation[1]);

            Assert.AreEqual(4, evalFake.EvaluateCalledCounter);
        }


        [TestMethod]
        public void SelectivSearchTest_PrincipalVariationOfDeeperSearchDeleted()
        {
            //           start
            //        /          \
            //       2            4x         white move -> highest selected (x) 
            //    c/   \        /    \   
            //   2x     3      4x     5      black move -> lowest selected (x), c = capture move    --> maxDepth
            // c/  \c                       
            // 1    2x                       white capture move -> highest selected (x)             --> selectiveDepth
            // |    |
            // 0    0                        (no capture move, will not be evaluated)
            var evalFake = new FakeEvaluator(new List<int>() { 100, 200, 300, 400, 500 }); // lowest level evaluation
            var moveGenFake = new FakeMoveGeneratorMulitlevel();
            var bestMoveWhite = MoveMaker.White(1, 2);
            var bestMoveBlack = MoveMaker.Black(2, 3);
            moveGenFake.AddGetAllMoves(new List<IMove>() { MoveMaker.White(1, 1), bestMoveWhite }); // 1. level (white)
            moveGenFake.AddGetAllMoves(new List<IMove>() { MoveMaker.BlackCapture(2, 1), MoveMaker.Black(2, 2) }); // 2. level a (black)
            moveGenFake.AddGetAllMoves(new List<IMove>() { MoveMaker.WhiteCapture(3, 1), MoveMaker.WhiteCapture(3, 2) }); // 3. level a (white)
            moveGenFake.AddGetAllMoves(new List<IMove>() { MoveMaker.Black(4, 1) }); // (0)
            moveGenFake.AddGetAllMoves(new List<IMove>() { MoveMaker.Black(4, 2) }); // (0)
            moveGenFake.AddGetAllMoves(new List<IMove>() { bestMoveBlack, MoveMaker.Black(2, 4) }); // 2. level b (black)
            moveGenFake.SetIsChecks(new List<bool>() { false, false, false, false, false, false, false, false });

            IBoard boardFake = new FakeBoard();

            var target = new SearchAlphaBeta(boardFake, evalFake, moveGenFake, 2, null, new FilterCapturesOnly()); // maxDepth = 2
            target.SetAdditionalSelectiveDepth(1); // selectiveDepth = 4
            var bestRatingActual = target.SearchLevel(ChessColor.White, 1, -100000, 100000);

            Assert.AreEqual(400, bestRatingActual.Score);
            Assert.AreEqual(bestMoveWhite, bestRatingActual.Move);
            Assert.AreEqual(2, bestRatingActual.PrincipalVariation.Count);
            Assert.AreEqual(bestMoveWhite, bestRatingActual.PrincipalVariation[0]);
            Assert.AreEqual(bestMoveBlack, bestRatingActual.PrincipalVariation[1]);

            Assert.AreEqual(5, evalFake.EvaluateCalledCounter);
        }

        [TestMethod]
        public void SelectivSearchTest_PrincipalVariationOfDeeperSearchDeleted2()
        {
            //           start
            //        /          \
            //       2            4x         white move -> highest selected (x) 
            //     /   \c       /    \   
            //   1x     3      4x     5      black move -> lowest selected (x), c = capture move    --> maxDepth
            //       c/  \c                       
            //      -2   -3x                 white capture move -> highest selected (x)             --> selectiveDepth
            //       |    |
            //       0    0                  (no capture move, will not be evaluated)
            var evalFake = new FakeEvaluator(new List<int>() { 100, -200, -300, 400, 500 }); // lowest level evaluation
            var moveGenFake = new FakeMoveGeneratorMulitlevel();
            var bestMoveWhite = MoveMaker.White(1, 2);
            var bestMoveBlack = MoveMaker.Black(2, 3);
            moveGenFake.AddGetAllMoves(new List<IMove>() { MoveMaker.White(1, 1), bestMoveWhite }); // 1. level (white)
            moveGenFake.AddGetAllMoves(new List<IMove>() { MoveMaker.Black(2, 1), MoveMaker.BlackCapture(2, 2) }); // 2. level a (black)
            moveGenFake.AddGetAllMoves(new List<IMove>() { MoveMaker.WhiteCapture(3, 1), MoveMaker.WhiteCapture(3, 2) }); // 3. level a (white)
            moveGenFake.AddGetAllMoves(new List<IMove>() { MoveMaker.Black(4, 1) }); // (0)
            moveGenFake.AddGetAllMoves(new List<IMove>() { MoveMaker.Black(4, 2) }); // (0)
            moveGenFake.AddGetAllMoves(new List<IMove>() { bestMoveBlack, MoveMaker.Black(2, 4) }); // 2. level b (black)
            moveGenFake.SetIsChecks(new List<bool>() { false, false, false, false, false, false, false, false });

            IBoard boardFake = new FakeBoard();

            var target = new SearchAlphaBeta(boardFake, evalFake, moveGenFake, 2, null, new FilterCapturesOnly()); // maxDepth = 2
            target.SetAdditionalSelectiveDepth(1); // selectiveDepth = 4
            var bestRatingActual = target.SearchLevel(ChessColor.White, 1, -100000, 100000);

            Assert.AreEqual(400, bestRatingActual.Score);
            Assert.AreEqual(bestMoveWhite, bestRatingActual.Move);
            Assert.AreEqual(2, bestRatingActual.PrincipalVariation.Count);
            Assert.AreEqual(bestMoveWhite, bestRatingActual.PrincipalVariation[0]);
            Assert.AreEqual(bestMoveBlack, bestRatingActual.PrincipalVariation[1]);

            Assert.AreEqual(5, evalFake.EvaluateCalledCounter);
        }

        [TestMethod]
        public void SelectivSearchTest_PrincipalVariationOfDeeperSearchDeleted3()
        {
            //           start
            //        /          \
            //       1            4x           white move -> highest selected (x) 
            //     /   \        /   \c   
            //   1x     2      4x    5        black move -> lowest selected (x), c = capture move    --> maxDepth
            //                     c/  \c                       
            //                     2    5x    white capture move -> highest selected (x)             --> selectiveDepth
            //                     |    |
            //                     0    0     (no capture move, will not be evaluated)
            var evalFake = new FakeEvaluator(new List<int>() { 100, 200, 400, 200, 500 }); // lowest level evaluation
            var moveGenFake = new FakeMoveGeneratorMulitlevel();
            var bestMoveWhite = MoveMaker.White(1, 2);
            var bestMoveBlack = MoveMaker.Black(2, 3);
            moveGenFake.AddGetAllMoves(new List<IMove>() { MoveMaker.White(1, 1), bestMoveWhite }); // 1. level (white)
            moveGenFake.AddGetAllMoves(new List<IMove>() { MoveMaker.Black(2, 1), MoveMaker.Black(2, 2) }); // 2. level a (black)
            moveGenFake.AddGetAllMoves(new List<IMove>() { bestMoveBlack, MoveMaker.BlackCapture(2, 4) }); // 3. level a (white)
            moveGenFake.AddGetAllMoves(new List<IMove>() { MoveMaker.WhiteCapture(3, 1), MoveMaker.WhiteCapture(3, 2) }); // 3. level a (white)
            moveGenFake.AddGetAllMoves(new List<IMove>() { MoveMaker.Black(4, 1) }); // (0)
            moveGenFake.AddGetAllMoves(new List<IMove>() { MoveMaker.Black(4, 2) }); // (0)
            moveGenFake.SetIsChecks(new List<bool>() { false, false, false, false, false, false, false, false });

            IBoard boardFake = new FakeBoard();

            var target = new SearchAlphaBeta(boardFake, evalFake, moveGenFake, 2, null, new FilterCapturesOnly()); // maxDepth = 2
            target.SetAdditionalSelectiveDepth(1); // selectiveDepth = 4
            var bestRatingActual = target.SearchLevel(ChessColor.White, 1, -100000, 100000);

            Assert.AreEqual(5, evalFake.EvaluateCalledCounter);

            Assert.AreEqual(400, bestRatingActual.Score);
            Assert.AreEqual(bestMoveWhite, bestRatingActual.Move);
            Assert.AreEqual(2, bestRatingActual.PrincipalVariation.Count);
            Assert.AreEqual(bestMoveWhite, bestRatingActual.PrincipalVariation[0]);
            Assert.AreEqual(bestMoveBlack, bestRatingActual.PrincipalVariation[1]);
        }

        [TestMethod]
        public void SelectivSearchTest_PrincipalVariationOfDeeperSearchSelected()
        {
            //           start
            //        /          \
            //       1            4x       white move -> highest selected (x) 
            //     /   \        /   \c   
            //   1x     2      4x    5     black move -> lowest selected (x), c = capture move    --> maxDepth
            //               c/  \c                       
            //               3    4x       white capture move -> highest selected (x)             --> selectiveDepth
            //               |    |
            //               0    0        (no capture move, will not be evaluated)
            var evalFake = new FakeEvaluator(new List<int>() { 100, 200, 300, 400, 500 }); // lowest level evaluation
            var moveGenFake = new FakeMoveGeneratorMulitlevel();
            var bestMoveWhite = MoveMaker.White(1, 2);
            var bestMoveBlack = MoveMaker.BlackCapture(2, 3);
            var bestMoveWhite2 = MoveMaker.WhiteCapture(3, 2);
            moveGenFake.AddGetAllMoves(new List<IMove>() { MoveMaker.White(1, 1), bestMoveWhite }); // 1. level (white)
            moveGenFake.AddGetAllMoves(new List<IMove>() { MoveMaker.Black(2, 1), MoveMaker.Black(2, 2) }); // 2. level a (black)
            moveGenFake.AddGetAllMoves(new List<IMove>() { bestMoveBlack, MoveMaker.Black(2, 4) }); // 3. level a (white)
            moveGenFake.AddGetAllMoves(new List<IMove>() { MoveMaker.WhiteCapture(3, 1), bestMoveWhite2 }); // 3. level a (white)
            moveGenFake.AddGetAllMoves(new List<IMove>() { MoveMaker.Black(4, 1) }); // (0)
            moveGenFake.AddGetAllMoves(new List<IMove>() { MoveMaker.Black(4, 2) }); // (0)
            moveGenFake.SetIsChecks(new List<bool>() { false, false, false, false, false, false, false, false });

            IBoard boardFake = new FakeBoard();

            var target = new SearchAlphaBeta(boardFake, evalFake, moveGenFake, 2, null, new FilterCapturesOnly()); // maxDepth = 2
            target.SetAdditionalSelectiveDepth(1); // selectiveDepth = 4
            var bestRatingActual = target.SearchLevel(ChessColor.White, 1, -100000, 100000);

            Assert.AreEqual(5, evalFake.EvaluateCalledCounter);

            Assert.AreEqual(400, bestRatingActual.Score);
            Assert.AreEqual(bestMoveWhite, bestRatingActual.Move);
            Assert.AreEqual(3, bestRatingActual.PrincipalVariation.Count);
            Assert.AreEqual(bestMoveWhite, bestRatingActual.PrincipalVariation[0]);
            Assert.AreEqual(bestMoveBlack, bestRatingActual.PrincipalVariation[1]);
            Assert.AreEqual(bestMoveWhite2, bestRatingActual.PrincipalVariation[2]);
        }

        [TestMethod]
        public void SelectivSearchTest_PrincipalVariationOfDeeperSearchOverwritten()
        {
            //           start
            //        /          \
            //       1            4x       white move -> highest selected (x) 
            //    c/   \        /   \c   
            //   1x     2      4x    5     black move -> lowest selected (x), c = capture move    --> maxDepth
            //  c/           c/  \c                       
            //  1            3    4x       white capture move -> highest selected (x)             --> selectiveDepth
            //  |            |    |
            //  0            0    0        (no capture move, will not be evaluated)
            var evalFake = new FakeEvaluator(new List<int>() { 100, 200, 300, 400, 500 }); // lowest level evaluation
            var moveGenFake = new FakeMoveGeneratorMulitlevel();
            var bestMoveWhite = MoveMaker.White(1, 2);
            var bestMoveBlack = MoveMaker.BlackCapture(2, 3);
            var bestMoveWhite2 = MoveMaker.WhiteCapture(3, 3);
            moveGenFake.AddGetAllMoves(new List<IMove>() { MoveMaker.White(1, 1), bestMoveWhite }); // 1. level (white)
            moveGenFake.AddGetAllMoves(new List<IMove>() { MoveMaker.Black(2, 1), MoveMaker.BlackCapture(2, 2) }); // 2. level a (black)
            moveGenFake.AddGetAllMoves(new List<IMove>() { MoveMaker.WhiteCapture(3, 1) });
            moveGenFake.AddGetAllMoves(new List<IMove>() { MoveMaker.Black(4, 1) }); // (0)
            moveGenFake.AddGetAllMoves(new List<IMove>() { bestMoveBlack, MoveMaker.Black(2, 4) }); // 3. level a (white)
            moveGenFake.AddGetAllMoves(new List<IMove>() { MoveMaker.WhiteCapture(3, 2), bestMoveWhite2 }); // 3. level a (white)
            moveGenFake.AddGetAllMoves(new List<IMove>() { MoveMaker.Black(4, 2) }); // (0)
            moveGenFake.AddGetAllMoves(new List<IMove>() { MoveMaker.Black(4, 3) }); // (0)
            moveGenFake.SetIsChecks(new List<bool>() { false, false, false, false, false, false, false, false });

            IBoard boardFake = new FakeBoard();

            var target = new SearchAlphaBeta(boardFake, evalFake, moveGenFake, 2, null, new FilterCapturesOnly()); // maxDepth = 2
            target.SetAdditionalSelectiveDepth(1); // selectiveDepth = 4
            var bestRatingActual = target.SearchLevel(ChessColor.White, 1, -100000, 100000);

            Assert.AreEqual(5, evalFake.EvaluateCalledCounter);

            Assert.AreEqual(400, bestRatingActual.Score);
            Assert.AreEqual(bestMoveWhite, bestRatingActual.Move);
            Assert.AreEqual(3, bestRatingActual.PrincipalVariation.Count);
            Assert.AreEqual(bestMoveWhite, bestRatingActual.PrincipalVariation[0]);
            Assert.AreEqual(bestMoveBlack, bestRatingActual.PrincipalVariation[1]);
            Assert.AreEqual(bestMoveWhite2, bestRatingActual.PrincipalVariation[2]);
        }

        [TestMethod]
        public void SelectivSearchTest_PrincipalVariationOfDeeperThanPVSearchOverwritten()
        {
            //           start
            //        /          \
            //       1            4x       white move -> highest selected (x) 
            //    c/   \        /   \c   
            //   1x     2      4x    5     black move -> lowest selected (x), c = capture move    --> maxDepth
            //  c/           c/  \c                       
            //  1            3    4x       white capture move -> highest selected (x)             --> selectiveDepth
            // c|            |    |
            //  1            0    0        (no capture move, will not be evaluated)
            var evalFake = new FakeEvaluator(new List<int>() { 100, 200, 300, 400, 500 }); // lowest level evaluation
            var moveGenFake = new FakeMoveGeneratorMulitlevel();
            var bestMoveWhite = MoveMaker.White(1, 2);
            var bestMoveBlack = MoveMaker.BlackCapture(2, 3);
            var bestMoveWhite2 = MoveMaker.WhiteCapture(3, 3);
            moveGenFake.AddGetAllMoves(new List<IMove>() { MoveMaker.White(1, 1), bestMoveWhite }); // 1. level (white)
            moveGenFake.AddGetAllMoves(new List<IMove>() { MoveMaker.Black(2, 1), MoveMaker.BlackCapture(2, 2) }); // 2. level a (black)
            moveGenFake.AddGetAllMoves(new List<IMove>() { MoveMaker.WhiteCapture(3, 1) });
            moveGenFake.AddGetAllMoves(new List<IMove>() { MoveMaker.BlackCapture(4, 1) }); // (0)
            moveGenFake.AddGetAllMoves(new List<IMove>() { bestMoveBlack, MoveMaker.Black(2, 4) }); // 3. level a (white)
            moveGenFake.AddGetAllMoves(new List<IMove>() { MoveMaker.WhiteCapture(3, 2), bestMoveWhite2 }); // 3. level a (white)
            moveGenFake.AddGetAllMoves(new List<IMove>() { MoveMaker.Black(4, 2) }); // (0)
            moveGenFake.AddGetAllMoves(new List<IMove>() { MoveMaker.Black(4, 3) }); // (0)
            moveGenFake.SetIsChecks(new List<bool>() { false, false, false, false, false, false, false, false });

            IBoard boardFake = new FakeBoard();

            var target = new SearchAlphaBeta(boardFake, evalFake, moveGenFake, 2, null, new FilterCapturesOnly()); // maxDepth = 2
            target.SetAdditionalSelectiveDepth(1); // selectiveDepth = 4
            var bestRatingActual = target.SearchLevel(ChessColor.White, 1, -100000, 100000);

            Assert.AreEqual(5, evalFake.EvaluateCalledCounter);

            Assert.AreEqual(400, bestRatingActual.Score);
            Assert.AreEqual(bestMoveWhite, bestRatingActual.Move);
            Assert.AreEqual(3, bestRatingActual.PrincipalVariation.Count);
            Assert.AreEqual(bestMoveWhite, bestRatingActual.PrincipalVariation[0]);
            Assert.AreEqual(bestMoveBlack, bestRatingActual.PrincipalVariation[1]);
            Assert.AreEqual(bestMoveWhite2, bestRatingActual.PrincipalVariation[2]);
        }
    }
}
