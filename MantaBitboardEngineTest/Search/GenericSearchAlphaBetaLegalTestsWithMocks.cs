using System.Linq;
using System.Collections.Generic;
using MantaBitboardEngine;
using MantaChessEngineTest.Doubles;
using MantaCommon;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace MantaBitboardEngineTest
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

        [TestInitialize]
        public void Setup()
        {
            moveGenFake = new FakeBitMoveGeneratorMulitlevel();
            hashMock = new Mock<IHashtable>().Object;
            boardMock = new Mock<IBitBoard>().Object;
            moveRatingFactory = new BitMoveRatingFactory(moveGenFake);
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
            moveGenFake.SetIsChecks(new List<bool>() { false, false });

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
            moveGenFake.SetIsChecks(new List<bool>() { false, false });

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
            moveGenFake.SetIsChecks(new List<bool>() { false, false, false, false, false, false, });

            var target = new GenericSearchAlphaBeta<BitMove>(boardMock, evalFake, moveGenFake, hashMock, null, moveRatingFactory, 2);
            var bestRatingActual = target.SearchLevel(ChessColor.White, 1, AlphaStart, BetaStart);

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
            moveGenFake.SetIsChecks(new List<bool>() { false, false, false, false, false, false, });

            var target = new GenericSearchAlphaBeta<BitMove>(boardMock, evalFake, moveGenFake, hashMock, null, moveRatingFactory, 2);
            var bestRatingActual = target.SearchLevel(ChessColor.Black, 1, AlphaStart, BetaStart);

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
            
            var bestMove = BitMoveMaker.White(1, 2);
            moveGenFake.AddGetAllMoves(new List<BitMove>() { BitMoveMaker.White(1, 1), bestMove }); // 1. level (white)
            moveGenFake.AddGetAllMoves(new List<BitMove>() { BitMoveMaker.Black(2, 1), BitMoveMaker.Black(1, 2) }); // 2. level a (black)
            moveGenFake.AddGetAllMoves(new List<BitMove>() { BitMoveMaker.White(3, 1), BitMoveMaker.White(3, 2) }); // 3. level a (white)
            moveGenFake.AddGetAllMoves(new List<BitMove>() { BitMoveMaker.White(3, 3), BitMoveMaker.White(3, 3) }); // 3. level b (white)
            moveGenFake.AddGetAllMoves(new List<BitMove>() { BitMoveMaker.Black(2, 3), BitMoveMaker.Black(2, 4) }); // 2. level b (black)
            moveGenFake.AddGetAllMoves(new List<BitMove>() { BitMoveMaker.White(3, 5), BitMoveMaker.White(3, 6) }); // 3. level c (white)
            moveGenFake.AddGetAllMoves(new List<BitMove>() { BitMoveMaker.White(3, 7), BitMoveMaker.White(3, 8) }); // 3. level d (white)
            moveGenFake.SetIsChecks(new List<bool>() { false, false, false, false, false, false, false, false, false, false, false, false, false, false });

            var target = new GenericSearchAlphaBeta<BitMove>(boardMock, evalFake, moveGenFake, hashMock, null, moveRatingFactory, 3);
            var bestRatingActual = target.SearchLevel(ChessColor.White, 1, AlphaStart, BetaStart);

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
            moveGenFake.SetIsChecks(new List<bool>() { false });

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
            moveGenFake.SetIsChecks(new List<bool>() { false, false, false, false, false });

            var target = new GenericSearchAlphaBeta<BitMove>(boardMock, evalFake, moveGenFake, hashMock, null, moveRatingFactory, 2);
            var bestRatingActual = target.SearchLevel(ChessColor.White, 1, AlphaStart, BetaStart);

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
            moveGenFake.SetIsChecks(new List<bool>() { false, false, false, false, false, false });

            var target = new GenericSearchAlphaBeta<BitMove>(boardMock, evalFake, moveGenFake, hashMock, null, moveRatingFactory, 2);
            var bestRatingActual = target.SearchLevel(ChessColor.White, 1, AlphaStart, BetaStart);

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

            moveGenFake.AddGetAllMoves(new List<BitMove>() { }); // first level
            moveGenFake.SetIsChecks(new List<bool>() { true });

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

            moveGenFake.AddGetAllMoves(new List<BitMove>() { }); // first level
            moveGenFake.SetIsChecks(new List<bool>() { false });
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
            moveGenFake.AddGetAllMoves(new List<BitMove>() { }); // 2a
            moveGenFake.AddGetAllMoves(new List<BitMove>() { BitMoveMaker.Black(2) }); // 2b
            moveGenFake.AddGetAllMoves(new List<BitMove>() { BitMoveMaker.White(3) }); // 3b
            moveGenFake.SetIsChecks(new List<bool>() { false, true/*2a*/, false, false, false }); // 2a

            var target = new GenericSearchAlphaBeta<BitMove>(boardMock, evalFake, moveGenFake, hashMock, null, moveRatingFactory, 3);
            var bestRatingActual = target.SearchLevel(ChessColor.White, 1, AlphaStart, BetaStart);

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
            moveGenFake.AddGetAllMoves(new List<BitMove>() { }); // 2a
            moveGenFake.AddGetAllMoves(new List<BitMove>() { BitMoveMaker.Black(2) }); // 2b
            moveGenFake.AddGetAllMoves(new List<BitMove>() { BitMoveMaker.White(3) }); // 3b
            moveGenFake.SetIsChecks(new List<bool>() { false }); // 2a

            var target = new GenericSearchAlphaBeta<BitMove>(boardMock, evalFake, moveGenFake, hashMock, null, moveRatingFactory, 3);
            var bestRatingActual = target.SearchLevel(ChessColor.White, 1, AlphaStart, BetaStart);

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
            moveGenFake.AddGetAllMoves(new List<BitMove>() { }); // 2a
            moveGenFake.AddGetAllMoves(new List<BitMove>() { BitMoveMaker.Black(2) }); // 2b
            moveGenFake.AddGetAllMoves(new List<BitMove>() { BitMoveMaker.White(3) }); // 3b
            moveGenFake.SetIsChecks(new List<bool>() { false, false, false, false, false }); // 2a

            var target = new GenericSearchAlphaBeta<BitMove>(boardMock, evalFake, moveGenFake, hashMock, null, moveRatingFactory, 3);
            var bestRatingActual = target.SearchLevel(ChessColor.White, 1, AlphaStart, BetaStart);

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
            moveGenFake.AddGetAllMoves(new List<BitMove>() { }); // 2a
            moveGenFake.AddGetAllMoves(new List<BitMove>() { }); // 2b
            moveGenFake.SetIsChecks(new List<bool>() { false, true/*2a*/, false, false/*2b*/ }); // 2a 2b

            var target = new GenericSearchAlphaBeta<BitMove>(boardMock, evalFake, moveGenFake, hashMock, null, moveRatingFactory, 3);
            var bestRatingActual = target.SearchLevel(ChessColor.White, 1, AlphaStart, BetaStart);

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
            moveGenFake.AddGetAllMoves(new List<BitMove>() { }); // 2a
            moveGenFake.AddGetAllMoves(new List<BitMove>() { }); // 2b
            moveGenFake.SetIsChecks(new List<bool>() { false, false/*2a*/, false, true/*2b*/ }); // 2a 2b

            var target = new GenericSearchAlphaBeta<BitMove>(boardMock, evalFake, moveGenFake, hashMock, null, moveRatingFactory, 3);
            var bestRatingActual = target.SearchLevel(ChessColor.White, 1, AlphaStart, BetaStart);

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
            moveGenFake.AddGetAllMoves(new List<BitMove>() { }); // 2a
            moveGenFake.AddGetAllMoves(new List<BitMove>() { BitMoveMaker.Black(2) }); // 2b
            moveGenFake.AddGetAllMoves(new List<BitMove>() { BitMoveMaker.White(3) }); // 3b
            moveGenFake.AddGetAllMoves(new List<BitMove>() { }); // 4b
            moveGenFake.SetIsChecks(new List<bool>() { false, true/*2a*/, false, false, false, true/*4b*/ });

            var target = new GenericSearchAlphaBeta<BitMove>(boardMock, evalFake, moveGenFake, hashMock, null, moveRatingFactory, 4);
            var bestRatingActual = target.SearchLevel(ChessColor.White, 1, AlphaStart, BetaStart);

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
            moveGenFake.AddGetAllMoves(new List<BitMove>() { }); // 4a
            moveGenFake.AddGetAllMoves(new List<BitMove>() { }); // 2b
            moveGenFake.SetIsChecks(new List<bool>() { false, false, false, true/*4a*/, false, true/*2b*/ });

            var target = new GenericSearchAlphaBeta<BitMove>(boardMock, evalFake, moveGenFake, hashMock, null, moveRatingFactory, 4);
            var bestRatingActual = target.SearchLevel(ChessColor.White, 1, AlphaStart, BetaStart);

            AssertHelperBitboard.WhiteWins(bestRatingActual);
            Assert.AreEqual(bestMove, bestRatingActual.Move);
        }
    }
}
