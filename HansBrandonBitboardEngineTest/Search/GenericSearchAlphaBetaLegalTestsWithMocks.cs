﻿using System.Linq;
using System.Collections.Generic;
using HansBrandonBitboardEngine;
using HansBrandonChessEngineTest.Doubles;
using HBCommon;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace HansBrandonBitboardEngineTest
{
    /// <summary>
    /// Tests for SearchAlphaBeta with mock moves and evaluation: normal moves.
    /// </summary>
    
    [TestClass]
    public class GenericSearchAlphaBetaLegalTestsWithMocks
    {
        const int AlphaStart = int.MinValue;
        const int BetaStart = int.MaxValue;

        FakeBitMoveGeneratorMulitlevel moveGenFake;
        IHashtable hashMock;
        IBitBoard boardMock;
        BitMoveRatingFactory moveRatingFactory;
        BitMove illegalMoveWhite;
        BitMove illegalMoveBlack;

        [TestInitialize]
        public void Setup()
        {
            moveGenFake = new FakeBitMoveGeneratorMulitlevel();
            hashMock = new Mock<IHashtable>().Object;
            boardMock = new Mock<IBitBoard>().Object;
            moveRatingFactory = new BitMoveRatingFactory(moveGenFake);
            illegalMoveWhite = BitMove.CreateMove(PieceType.Pawn, Square.A1, Square.A1, PieceType.Empty, ChessColor.White, 0);
            illegalMoveBlack = BitMove.CreateMove(PieceType.Pawn, Square.A1, Square.A1, PieceType.Empty, ChessColor.Black, 0);
        }

        // ---------------------------------------------------------------------------------------------
        // Search Level Tests with Mocks
        // ---------------------------------------------------------------------------------------------

        // Depth 1

        [TestMethod]
        public void SearchAlphaBetaTest_WhenWhiteMovesFirstAnd1Level_ThenCorrectSelected()
        {
            //      start
            //     /      \
            //    1       2x    white move -> highest selected (x) 
            IEvaluator evalFake = new FakeEvaluator(new List<int>() { 100, 200 });
            
            var bestMove = BitMoveMaker.White(1, 2);
            moveGenFake.AddGetAllMoves(new List<BitMove>() { BitMoveMaker.White(1, 1), bestMove }); // first level
            moveGenFake.SetIsChecks(ChessColor.White, new List<bool>() { false, false });

            var target = new GenericSearchAlphaBeta<BitMove>(boardMock, evalFake, moveGenFake, hashMock, null, moveRatingFactory, 1);
            var bestRatingActual = target.SearchLevel(ChessColor.White, 1, AlphaStart, BetaStart);

            Assert.AreEqual(200, bestRatingActual.Score);
            Assert.AreEqual(bestMove, bestRatingActual.Move);
        }
       
        [TestMethod]
        public void SearchTest_WhenBlackMovesFirstAnd1Level_ThenCorrectSelected()
        {
            //      start
            //     /      \
            //    1x       2    black move -> lowest selected (x) 
            IEvaluator evalFake = new FakeEvaluator(new List<int>() { 100, 200 });
            
            var bestMove = BitMoveMaker.Black(1, 1);
            moveGenFake.AddGetAllMoves(new List<BitMove>() { bestMove, BitMoveMaker.Black(1, 2) }); // first level
            moveGenFake.SetIsChecks(ChessColor.Black, new List<bool>() { false, false });

            var target = new GenericSearchAlphaBeta<BitMove>(boardMock, evalFake, moveGenFake, hashMock, null, moveRatingFactory, 1);
            var bestRatingActual = target.SearchLevel(ChessColor.Black, 1, AlphaStart, BetaStart);

            Assert.AreEqual(100, bestRatingActual.Score);
            Assert.AreEqual(bestMove, bestRatingActual.Move);
        }

        // Depth 2

        [TestMethod]
        public void SearchAlphaBetaTest_WhenWhiteMovesFirstAnd2Levels_ThenCorrectSelected()
        {
            //      start
            //     /      \
            //    1       3x    white move -> highest selected (x) 
            //  /  \     /  \   
            // 1x   2   3x   4  black move -> lowest selected (x)
            IEvaluator evalFake = new FakeEvaluator(new List<int>() { 100, 200, 300, 400});
            
            var bestMove = BitMoveMaker.White(1, 2);
            moveGenFake.AddGetAllMoves(new List<BitMove>() { BitMoveMaker.White(1, 1), bestMove }); // first level
            moveGenFake.AddGetAllMoves(new List<BitMove>() { BitMoveMaker.Black(2, 1), BitMoveMaker.Black(2, 2) }); // second level 1.
            moveGenFake.AddGetAllMoves(new List<BitMove>() { BitMoveMaker.Black(2, 3), BitMoveMaker.Black(2, 4) }); // second level 2.
            moveGenFake.SetIsChecks(ChessColor.White, new List<bool>() { false, false });
            moveGenFake.SetIsChecks(ChessColor.Black, new List<bool>() { false, false, false, false });

            var target = new GenericSearchAlphaBeta<BitMove>(boardMock, evalFake, moveGenFake, hashMock, null, moveRatingFactory, 2);
            var bestRatingActual = target.SearchLevel(ChessColor.White, 2, AlphaStart, BetaStart);

            Assert.AreEqual(300, bestRatingActual.Score);
            Assert.AreEqual(bestMove, bestRatingActual.Move);
        }

        [TestMethod]
        public void SearchAlphaBetaTest_WhenBlackMovesFirstAnd2Levels_ThenCorrectSelected()
        {
            //      start
            //     /      \
            //    2x       4      black move -> lowest selected (x) 
            //  /  \     /  \   
            // 1    2x  3    4x  white move -> highest selected (x)
            IEvaluator evalFake = new FakeEvaluator(new List<int>() { 100, 200, 300, 400 });

            var bestMove = BitMoveMaker.Black(1, 1);
            moveGenFake.AddGetAllMoves(new List<BitMove>() { bestMove, BitMoveMaker.Black(1, 2) }); // first level
            moveGenFake.AddGetAllMoves(new List<BitMove>() { BitMoveMaker.White(2, 1), BitMoveMaker.White(2, 2) }); // second level 1.
            moveGenFake.AddGetAllMoves(new List<BitMove>() { BitMoveMaker.White(2, 3), BitMoveMaker.White(2, 4) }); // second level 2.
            moveGenFake.SetIsChecks(ChessColor.Black, new List<bool>() { false, false });
            moveGenFake.SetIsChecks(ChessColor.White, new List<bool>() { false, false, false, false });

            var target = new GenericSearchAlphaBeta<BitMove>(boardMock, evalFake, moveGenFake, hashMock, null, moveRatingFactory, 2);
            var bestRatingActual = target.SearchLevel(ChessColor.Black, 2, AlphaStart, BetaStart);

            Assert.AreEqual(200, bestRatingActual.Score);
            Assert.AreEqual(bestMove, bestRatingActual.Move);
        }

        // Depth 3

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
            IEvaluator evalFake = new FakeEvaluator(new List<int>() { 200, 100, 700, /*noeval800,*/500, 600, 300, 400 }); // lowest level evaluation
            // note: noeval means it is not evaluated because of cutoff

            var bestMove = BitMoveMaker.White(1, 2);
            moveGenFake.AddGetAllMoves(new List<BitMove>() { BitMoveMaker.White(1, 1), bestMove }); // 1. level (white)
            moveGenFake.AddGetAllMoves(new List<BitMove>() { BitMoveMaker.Black(2, 1), BitMoveMaker.Black(1, 2) }); // 2. level a (black)
            moveGenFake.AddGetAllMoves(new List<BitMove>() { BitMoveMaker.White(3, 1), BitMoveMaker.White(3, 2) }); // 3. level a (white)
            moveGenFake.AddGetAllMoves(new List<BitMove>() { BitMoveMaker.White(3, 3), BitMoveMaker.White(3, 3) }); // 3. level b (white)
            moveGenFake.AddGetAllMoves(new List<BitMove>() { BitMoveMaker.Black(2, 3), BitMoveMaker.Black(2, 4) }); // 2. level b (black)
            moveGenFake.AddGetAllMoves(new List<BitMove>() { BitMoveMaker.White(3, 5), BitMoveMaker.White(3, 6) }); // 3. level c (white)
            moveGenFake.AddGetAllMoves(new List<BitMove>() { BitMoveMaker.White(3, 7), BitMoveMaker.White(3, 8) }); // 3. level d (white)
            moveGenFake.SetIsChecks(ChessColor.White, new List<bool>() { false, false, false, false, false, false, false, false, false, false });
            moveGenFake.SetIsChecks(ChessColor.Black, new List<bool>() { false, false, false, false });

            var target = new GenericSearchAlphaBeta<BitMove>(boardMock, evalFake, moveGenFake, hashMock, null, moveRatingFactory, 3);
            var bestRatingActual = target.SearchLevel(ChessColor.White, 3, AlphaStart, BetaStart);

            Assert.AreEqual(400, bestRatingActual.Score);
            Assert.AreEqual(bestMove, bestRatingActual.Move);
        }

        // irregular
        
        // Depth 1

        [TestMethod]
        public void SearchAlphaBetaTest_WhenWhiteMovesFirstAnd1Level1_ThenCorrectSelected()
        {
            //      start
            //     /      
            //    1x           white move -> highest selected (x) 
            IEvaluator evalFake = new FakeEvaluator(new List<int>() { 100 });

            var bestMove = BitMoveMaker.White(1, 1);
            moveGenFake.AddGetAllMoves(new List<BitMove>() { bestMove }); // first level
            moveGenFake.SetIsChecks(ChessColor.White, new List<bool>() { false });

            var target = new GenericSearchAlphaBeta<BitMove>(boardMock, evalFake, moveGenFake, hashMock, null, moveRatingFactory, 1);
            var bestRating = target.SearchLevel(ChessColor.White, 1, AlphaStart, BetaStart);

            Assert.AreEqual(100, bestRating.Score);
            Assert.AreEqual(bestMove, bestRating.Move);
        }
        
        // Depth 2

        [TestMethod]
        public void SearchAlphaBetaTest_WhenWhiteMovesFirstAnd2LevelsIrregular_ThenCorrectSelected()
        {
            //      start
            //     /      \
            //    1       4x    white move -> highest selected (x) 
            //  /  \        \   
            // 1x   2        4x  black move -> lowest selected (x)
            IEvaluator evalFake = new FakeEvaluator(new List<int>() { 100, 200, 400 });

            var bestFakeMove = BitMoveMaker.White(1, 2);
            moveGenFake.AddGetAllMoves(new List<BitMove>() { BitMoveMaker.White(1, 1), bestFakeMove }); // first level
            moveGenFake.AddGetAllMoves(new List<BitMove>() { BitMoveMaker.Black(2, 1), BitMoveMaker.Black(1, 2) }); // second level 1.
            moveGenFake.AddGetAllMoves(new List<BitMove>() { BitMoveMaker.Black(2, 3) }); // second level 2.
            moveGenFake.SetIsChecks(ChessColor.White, new List<bool>() { false, false });
            moveGenFake.SetIsChecks(ChessColor.Black, new List<bool>() { false, false, false });

            var target = new GenericSearchAlphaBeta<BitMove>(boardMock, evalFake, moveGenFake, hashMock, null, moveRatingFactory, 2);
            var bestRatingActual = target.SearchLevel(ChessColor.White, 2, AlphaStart, BetaStart);

            Assert.AreEqual(400, bestRatingActual.Score);
            Assert.AreEqual(bestFakeMove, bestRatingActual.Move);
        }
        
        [TestMethod]
        public void SearchAlphaBetaTest_WhenWhiteMovesFirstAnd2LevelsIrregular2_ThenCorrectSelected()
        {
            //        start
            //      /      \
            //     3        5x    white move -> highest selected (x) 
            //  /  |  \      \   
            // 6   4   3x     5x  black move -> lowest selected (x)
            IEvaluator evalFake = new FakeEvaluator(new List<int>() { 600, 400, 300, 500 });

            var bestMove = BitMoveMaker.White(1, 2);
            moveGenFake.AddGetAllMoves(new List<BitMove>() { BitMoveMaker.White(1, 1), bestMove }); // level 1
            moveGenFake.AddGetAllMoves(new List<BitMove>() { BitMoveMaker.Black(2, 1), BitMoveMaker.Black(2, 2), BitMoveMaker.Black(2, 3) }); // level 2 a
            moveGenFake.AddGetAllMoves(new List<BitMove>() { BitMoveMaker.Black(2, 4) }); // level 2 b
            moveGenFake.SetIsChecks(ChessColor.White, new List<bool>() { false, false });
            moveGenFake.SetIsChecks(ChessColor.Black, new List<bool>() { false, false, false, false });

            var target = new GenericSearchAlphaBeta<BitMove>(boardMock, evalFake, moveGenFake, hashMock, null, moveRatingFactory, 2);
            var bestRatingActual = target.SearchLevel(ChessColor.White, 2, AlphaStart, BetaStart);

            Assert.AreEqual(500, bestRatingActual.Score);
            Assert.AreEqual(bestMove, bestRatingActual.Move);
        }

        // -----------------------------------------------------------------------------------
        // check mate and stall mate tests
        // -----------------------------------------------------------------------------------

        [TestMethod]
        public void SearchAlphaBetaTest_WhenWhiteHasNoMovesAndIsCheck_ThenBlackWins()
        {
            //      start
            //     /      
            //   no moves          white move -> highest selected (x) 
            IEvaluator evalFake = new FakeEvaluator(new List<int>() { });

            moveGenFake.AddGetAllMoves(new List<BitMove>() { illegalMoveWhite }); // first level
            moveGenFake.SetIsChecks(ChessColor.White, new List<bool>() { true/*1a-illegal*/, true/*white is chackmate*/ });

            var target = new GenericSearchAlphaBeta<BitMove>(boardMock, evalFake, moveGenFake, hashMock, null, moveRatingFactory, 1);
            var bestRatingActual = target.SearchLevel(ChessColor.White, 1, AlphaStart, BetaStart);

            AssertHelperBitboard.BlackWins(bestRatingActual);
            Assert.IsTrue(bestRatingActual.Move.Equals(BitMove.CreateEmptyMove()));
        }

        [TestMethod]
        public void SearchAlphaBetaTest_WhenWhiteHasNoMovesAndIsNotCheck_ThenDraw()
        {
            //      start
            //     /      
            //   no moves          white move -> highest selected (x) 
            IEvaluator evalFake = new FakeEvaluator(new List<int>() { });

            moveGenFake.AddGetAllMoves(new List<BitMove>() { illegalMoveWhite }); // first level
            moveGenFake.SetIsChecks(ChessColor.White, new List<bool>() { true/*move illegal*/, false/*stallmate*/ });

            var target = new GenericSearchAlphaBeta<BitMove>(boardMock, evalFake, moveGenFake, hashMock, null, moveRatingFactory, 1);
            var bestRatingActual = target.SearchLevel(ChessColor.White, 1, AlphaStart, BetaStart);

            Assert.AreEqual(0, bestRatingActual.Score);
            AssertHelperBitboard.StallMate(bestRatingActual);
            Assert.IsTrue(bestRatingActual.Move.Equals(BitMove.CreateEmptyMove()));
        }

        [TestMethod]
        public void SearchAlphaBetaTest_WhenWhiteChoosesBetweenCheckmateAndGoodMove_ThenWhiteSelectsCheckmate()
        {
            //      start
            //   1a /     \1b  
            //  10000x    100        white move -> highest selected (x)
            // no/moves    \2b
            //             100x       black move -> lowest selected (x)
            //              \3b
            //             100x       white move -> highest selected (x)

            IEvaluator evalFake = new FakeEvaluator(new List<int>() { 1000 });

            var bestMove = BitMoveMaker.White(1, 1);
            moveGenFake.AddGetAllMoves(new List<BitMove>() { bestMove, BitMoveMaker.White(1, 2) }); // 1a 1b
            moveGenFake.AddGetAllMoves(new List<BitMove>() { illegalMoveBlack }); // 2a
            moveGenFake.AddGetAllMoves(new List<BitMove>() { BitMoveMaker.Black(2) }); // 2b
            moveGenFake.AddGetAllMoves(new List<BitMove>() { BitMoveMaker.White(3) }); // 3b
            moveGenFake.SetIsChecks(ChessColor.White, new List<bool>() { false/*1a*/, false/*1b*/, false/*3b*/ });
            moveGenFake.SetIsChecks(ChessColor.Black, new List<bool>() { true/*2a illegal*/, true/*2a check*/, false/*2b*/ });

            var target = new GenericSearchAlphaBeta<BitMove>(boardMock, evalFake, moveGenFake, hashMock, null, moveRatingFactory, 3);
            var bestRatingActual = target.SearchLevel(ChessColor.White, 3, AlphaStart, BetaStart);

            AssertHelperBitboard.WhiteWins(bestRatingActual);
            Assert.AreEqual(bestMove, bestRatingActual.Move);
        }
        
        [TestMethod]
        public void SearchAlphaBetaTest_WhenWhiteChoosesBetweenStallmateAndBadMove_ThenWhiteSelectsStallmate()
        {
            //      start
            //   1a /     \1b  
            //    0x     -100        white move -> highest selected (x)
            // no/moves    \2b
            //             -100x       black move -> lowest selected (x)
            //              \3b
            //             -100x       white move -> highest selected (x)

            IEvaluator evalFake = new FakeEvaluator(new List<int>() { -1000 });

            var bestMove = BitMoveMaker.White(1, 1);
            moveGenFake.AddGetAllMoves(new List<BitMove>() { bestMove, BitMoveMaker.White(1, 2) }); // 1a 1b
            moveGenFake.AddGetAllMoves(new List<BitMove>() { illegalMoveBlack}); // 2a
            moveGenFake.AddGetAllMoves(new List<BitMove>() { BitMoveMaker.Black(2) }); // 2b
            moveGenFake.AddGetAllMoves(new List<BitMove>() { BitMoveMaker.White(3) }); // 3b
            moveGenFake.SetIsChecks(ChessColor.White, new List<bool>() { false/*1a*/, false/*1b*/, false/*3b*/ });
            moveGenFake.SetIsChecks(ChessColor.Black, new List<bool>() { true/*2a illegal*/, false/*2a stallmate*/, false/*2b*/ });

            var target = new GenericSearchAlphaBeta<BitMove>(boardMock, evalFake, moveGenFake, hashMock, null, moveRatingFactory, 3);
            var bestRatingActual = target.SearchLevel(ChessColor.White, 3, AlphaStart, BetaStart);

            Assert.AreEqual(0, bestRatingActual.Score);
            Assert.AreEqual(bestMove, bestRatingActual.Move);
        }
        
        [TestMethod]
        public void SearchAlphaBetaTest_WhenWhiteChoosesBetweenStallmateAndGoodMove_ThenWhiteSelectsGoodMove()
        {
            //      start
            //   1a /     \1b  
            //    0x      100        white move -> highest selected (x)
            // no/moves    \2b
            //              100x       black move -> lowest selected (x)
            //              \3b
            //              100x       white move -> highest selected (x)

            IEvaluator evalFake = new FakeEvaluator(new List<int>() { 1000 });

            var bestMove = BitMoveMaker.White(1, 2);
            moveGenFake.AddGetAllMoves(new List<BitMove>() { BitMoveMaker.White(1, 1), bestMove }); // 1a 1b
            moveGenFake.AddGetAllMoves(new List<BitMove>() { illegalMoveBlack }); // 2a
            moveGenFake.AddGetAllMoves(new List<BitMove>() { BitMoveMaker.Black(2) }); // 2b
            moveGenFake.AddGetAllMoves(new List<BitMove>() { BitMoveMaker.White(3) }); // 3b
            moveGenFake.SetIsChecks(ChessColor.White, new List<bool>() { false/*1a*/, false/*1b*/, false/*3b*/ });
            moveGenFake.SetIsChecks(ChessColor.Black, new List<bool>() { true/*2a illegal*/, false/*2a stallmate*/, false/*2b*/ });

            var target = new GenericSearchAlphaBeta<BitMove>(boardMock, evalFake, moveGenFake, hashMock, null, moveRatingFactory, 3);
            var bestRatingActual = target.SearchLevel(ChessColor.White, 3, AlphaStart, BetaStart);

            Assert.AreEqual(1000, bestRatingActual.Score);
            Assert.AreEqual(bestMove, bestRatingActual.Move);
        }

        [TestMethod]
        public void SearchAlphaBetaTest_WhenWhiteChoosesBeteweenCheckmateAndStallmate_ThenWhiteSelectsCheckmate()
        {
            //      start
            //   1a /     \1b  
            //  10000x     0        white move -> highest selected (x)
            // no/moves    no\moves 2b

            IEvaluator evalFake = new FakeEvaluator(new List<int>() { 1000 });

            var bestMove = BitMoveMaker.White(1, 1);
            moveGenFake.AddGetAllMoves(new List<BitMove>() { bestMove, BitMoveMaker.White(1, 2) }); // 1a 1b
            moveGenFake.AddGetAllMoves(new List<BitMove>() { illegalMoveBlack }); // 2a
            moveGenFake.AddGetAllMoves(new List<BitMove>() { illegalMoveBlack }); // 2b
            moveGenFake.SetIsChecks(ChessColor.White, new List<bool>() { false, false });
            moveGenFake.SetIsChecks(ChessColor.Black, new List<bool>() { true/*2a-illegalMove*/, true/*2a-black is check*/, true/*2b-illegal*/, false/*2b-black-notcheck-stallmate*/ });

            var target = new GenericSearchAlphaBeta<BitMove>(boardMock, evalFake, moveGenFake, hashMock, null, moveRatingFactory, 3);
            var bestRatingActual = target.SearchLevel(ChessColor.White, 3, AlphaStart, BetaStart);

            AssertHelperBitboard.WhiteWins(bestRatingActual);
            Assert.AreEqual(bestMove, bestRatingActual.Move);
        }
        
        [TestMethod]
        public void SearchAlphaBetaTest_WhenWhiteChoosesBeteweenCheckmateAndStallmate_ThenWhiteSelectsCheckmate_mirror()
        {
            //      start
            //   1a /     \1b  
            //     0     10000x   white move -> highest selected (x)
            //  no/moves    no\moves 2b

            IEvaluator evalFake = new FakeEvaluator(new List<int>() { 1000 });

            var bestMove = BitMoveMaker.White(1, 2);
            moveGenFake.AddGetAllMoves(new List<BitMove>() { BitMoveMaker.White(1, 1), bestMove }); // 1a 1b
            moveGenFake.AddGetAllMoves(new List<BitMove>() { illegalMoveBlack }); // 2a
            moveGenFake.AddGetAllMoves(new List<BitMove>() { illegalMoveBlack }); // 2b
            moveGenFake.SetIsChecks(ChessColor.White, new List<bool>() { false, false }); // 1a, 1b legal
            moveGenFake.SetIsChecks(ChessColor.Black, new List<bool>() { true/*2a-illegalMove*/, false/*2a-black not check-stallmate*/, true/*2b-illegal*/, true/*2b-black-check*/ });

            var target = new GenericSearchAlphaBeta<BitMove>(boardMock, evalFake, moveGenFake, hashMock, null, moveRatingFactory, 3);
            var bestRatingActual = target.SearchLevel(ChessColor.White, 3, AlphaStart, BetaStart);

            AssertHelperBitboard.WhiteWins(bestRatingActual);
            Assert.AreEqual(bestMove, bestRatingActual.Move);
        }
        
        [TestMethod]
        public void SearchAlphaBetaTest_WhenWhiteChoosesBeteweenCheckmateInLevel1AndCheckmateInLevel3_ThenWhiteSelectsCheckmateLevel1()
        {
            //      start
            //   1a /     \1b  
            //  10000x    10000        white move -> highest selected (x)
            // no/moves    \2b
            //             10000x       black move -> lowest selected (x)
            //              \3b
            //             10000x       white move -> highest selected (x)
            //              no\moves 4b
            //             10000x       black move -> lowest selected (x)

            IEvaluator evalFake = new FakeEvaluator(new List<int>() { });

            var bestMove = BitMoveMaker.White(1, 1);
            moveGenFake.AddGetAllMoves(new List<BitMove>() { bestMove, BitMoveMaker.White(1, 2) }); // 1a 1b
            moveGenFake.AddGetAllMoves(new List<BitMove>() { illegalMoveBlack }); // 2a
            moveGenFake.AddGetAllMoves(new List<BitMove>() { BitMoveMaker.Black(2) }); // 2b
            moveGenFake.AddGetAllMoves(new List<BitMove>() { BitMoveMaker.White(3) }); // 3b
            moveGenFake.AddGetAllMoves(new List<BitMove>() { illegalMoveBlack }); // 4b
            moveGenFake.SetIsChecks(ChessColor.White, new List<bool>() { false/*1a*/, false/*1b*/, false/*3a*/});
            moveGenFake.SetIsChecks(ChessColor.Black, new List<bool>() { true/*2a-illegalMove*/, true/*2a-black check*/, false/*2b*/, true/*4b-illegal*/, true/*4b check*/});

            var target = new GenericSearchAlphaBeta<BitMove>(boardMock, evalFake, moveGenFake, hashMock, null, moveRatingFactory, 4);
            var bestRatingActual = target.SearchLevel(ChessColor.White, 4, AlphaStart, BetaStart);

            AssertHelperBitboard.WhiteWins(bestRatingActual);
            Assert.AreEqual(bestMove, bestRatingActual.Move);
        }

        [TestMethod]
        public void SearchAlphaBetaTest_WhenWhiteChoosesBeteweenCheckmateInLevel1AndCheckmateInLevel3_ThenWhiteSelectsCheckmateLevel1_mirror()
        {
            //      start
            //   1a/       \1b
            //  10000x    10000           white move -> highest selected (x)
            //    /2a      no\moves 2b
            //  10000x      10000x        black move -> lowest selected (x)
            //    /3a
            //  10000x                    white move -> highest selected (x)
            // no/moves 4a
            //  10000x                    black move -> lowest selected (x)

            IEvaluator evalFake = new FakeEvaluator(new List<int>() { });

            var bestMove = BitMoveMaker.White(1, 2);
            moveGenFake.AddGetAllMoves(new List<BitMove>() { BitMoveMaker.White(1, 1), bestMove }); // 1a 1b
            moveGenFake.AddGetAllMoves(new List<BitMove>() { BitMoveMaker.Black(2) }); // 2a
            moveGenFake.AddGetAllMoves(new List<BitMove>() { BitMoveMaker.White(3) }); // 3a
            moveGenFake.AddGetAllMoves(new List<BitMove>() { illegalMoveBlack }); // 4a
            moveGenFake.AddGetAllMoves(new List<BitMove>() { illegalMoveBlack }); // 2b
            moveGenFake.SetIsChecks(ChessColor.White, new List<bool>() { false/*1a*/, false/*1b*/, false/*3a*/  });
            moveGenFake.SetIsChecks(ChessColor.Black, new List<bool>() { false/*2a*/, true/*4a illegal*/, true/*4a check*/, true/*2b illegal*/, true/*2b check*/ });

            var target = new GenericSearchAlphaBeta<BitMove>(boardMock, evalFake, moveGenFake, hashMock, null, moveRatingFactory, 4);
            var bestRatingActual = target.SearchLevel(ChessColor.White, 4, AlphaStart, BetaStart);

            AssertHelperBitboard.WhiteWins(bestRatingActual);
            Assert.AreEqual(bestMove, bestRatingActual.Move);
        }
    }
}
