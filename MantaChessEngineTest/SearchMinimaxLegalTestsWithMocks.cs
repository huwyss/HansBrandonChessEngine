using System.Linq;
using System.Collections.Generic;
using MantaChessEngine;
using MantaCommon;
using MantaChessEngineTest.Doubles;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace MantaChessEngineTest
{
    /// <summary>
    /// Tests for SearchMinimax with mock moves and evaluation: normal moves.
    /// </summary>
    [TestClass]
    public class SearchMinimaxLegalTestsWithMocks
    {
        IMove illegalMoveWhite = MoveFactory.MakeNormalMove(Piece.MakePiece('B', ChessColor.White), 1, 1, 2, 2, null);
        IMove illegalMoveBlack = MoveFactory.MakeNormalMove(Piece.MakePiece('b', ChessColor.Black), 1, 1, 2, 2, null);

        FakeMoveGeneratorMulitlevel moveGenFake;
        IBoard boardMock;

        [TestInitialize]
        public void Setup()
        {
            moveGenFake = new FakeMoveGeneratorMulitlevel();
            boardMock = new Mock<IBoard>().Object;
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
            IEvaluator evalFake = new FakeEvaluator(new List<int>() { 100, 200 });
            
            IMove bestMove = MoveMaker.White(1, 2);
            moveGenFake.AddGetAllMoves(new List<IMove>() { MoveMaker.White(1, 1), bestMove }); // first level
            moveGenFake.SetIsChecks(ChessColor.White, new List<bool>() { false, false });

            var target = new SearchMinimax(boardMock, evalFake, moveGenFake, 1);
            var bestRatingActual = target.SearchLevel(ChessColor.White, 1).First();

            Assert.AreEqual(200, bestRatingActual.Score);
            Assert.AreEqual(bestMove, bestRatingActual.Move);
        }

        [TestMethod]
        public void SearchMinimaxTest_WhenBlackMovesFirstAnd1Level_ThenCorrectSelected()
        {
            //      start
            //     /      \
            //    1x       2    black move -> lowest selected (x) 
            IEvaluator evalFake = new FakeEvaluator(new List<int>() { 100, 200 });

            IMove bestMove = MoveMaker.Black(1, 1);
            moveGenFake.AddGetAllMoves(new List<IMove>() { bestMove, MoveMaker.Black(1, 2) }); // first level
            moveGenFake.SetIsChecks(ChessColor.Black, new List<bool>() { false, false });

            var target = new SearchMinimax(boardMock, evalFake, moveGenFake, 1);
            var bestRatingActual = target.SearchLevel(ChessColor.Black, 1).First();

            Assert.AreEqual(100, bestRatingActual.Score);
            Assert.AreEqual(bestMove, bestRatingActual.Move);
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
            IEvaluator evalFake = new FakeEvaluator(new List<int>() { 100, 200, 300, 400});
            IMove bestMove = MoveMaker.White(1, 2);
            moveGenFake.AddGetAllMoves(new List<IMove>() { MoveMaker.White(1, 1), bestMove }); // first level
            moveGenFake.AddGetAllMoves(new List<IMove>() { MoveMaker.Black(2, 1), MoveMaker.Black(2, 2) }); // second level 1.
            moveGenFake.AddGetAllMoves(new List<IMove>() { MoveMaker.Black(2, 3), MoveMaker.Black(2, 4) }); // second level 2.
            moveGenFake.SetIsChecks(ChessColor.White, new List<bool>() { false, false });
            moveGenFake.SetIsChecks(ChessColor.Black, new List<bool>() { false, false, false, false });

            var target = new SearchMinimax(boardMock, evalFake, moveGenFake, 2);
            var bestRatingActual = target.SearchLevel(ChessColor.White, 1).First();

            Assert.AreEqual(300, bestRatingActual.Score);
            Assert.AreEqual(bestMove, bestRatingActual.Move);
        }

        [TestMethod]
        public void SearchMinimaxTest_WhenBlackMovesFirstAnd2Levels_ThenCorrectSelected()
        {
            //      start
            //     /      \
            //    2x       4      black move -> lowest selected (x) 
            //  /  \     /  \   
            // 1    2x  3    4x  white move -> highest selected (x)
            IEvaluator evalFake = new FakeEvaluator(new List<int>() { 100, 200, 300, 400 });

            IMove bestMove = MoveMaker.Black(1, 1);
            moveGenFake.AddGetAllMoves(new List<IMove>() { bestMove, MoveMaker.Black(1, 2) }); // first level
            moveGenFake.AddGetAllMoves(new List<IMove>() { MoveMaker.White(2, 1), MoveMaker.White(2, 2) }); // second level 1.
            moveGenFake.AddGetAllMoves(new List<IMove>() { MoveMaker.White(2, 3), MoveMaker.White(2, 4) }); // second level 2.
            moveGenFake.SetIsChecks(ChessColor.Black, new List<bool>() { false, false });
            moveGenFake.SetIsChecks(ChessColor.White, new List<bool>() { false, false, false, false });

            var target = new SearchMinimax(boardMock, evalFake, moveGenFake, 2);
            var bestRatingActual = target.SearchLevel(ChessColor.Black, 1).First();

            Assert.AreEqual(200, bestRatingActual.Score);
            Assert.AreEqual(bestMove, bestRatingActual.Move);
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
            IEvaluator evalFake = new FakeEvaluator(new List<int>() { 200, 100, 700, 800, 500, 600, 300, 400 }); // lowest level evaluation
            
            IMove bestMove = MoveMaker.White(1, 2);
            moveGenFake.AddGetAllMoves(new List<IMove>() { MoveMaker.White(1, 1), bestMove }); // 1. level (white)
            moveGenFake.AddGetAllMoves(new List<IMove>() { MoveMaker.Black(2, 1), MoveMaker.Black(1, 2) }); // 2. level a (black)
            moveGenFake.AddGetAllMoves(new List<IMove>() { MoveMaker.White(3, 1), MoveMaker.White(3, 2) }); // 3. level a (white)
            moveGenFake.AddGetAllMoves(new List<IMove>() { MoveMaker.White(3, 3), MoveMaker.White(3, 3) }); // 3. level b (white)
            moveGenFake.AddGetAllMoves(new List<IMove>() { MoveMaker.Black(2, 3), MoveMaker.Black(2, 4) }); // 2. level b (black)
            moveGenFake.AddGetAllMoves(new List<IMove>() { MoveMaker.White(3, 5), MoveMaker.White(3, 6) }); // 3. level c (white)
            moveGenFake.AddGetAllMoves(new List<IMove>() { MoveMaker.White(3, 7), MoveMaker.White(3, 8) }); // 3. level d (white)
            moveGenFake.SetIsChecks(ChessColor.White, new List<bool>() { false, false, false, false, false, false, false, false, false, false });
            moveGenFake.SetIsChecks(ChessColor.Black, new List<bool>() { false, false, false, false });

            var target = new SearchMinimax(boardMock, evalFake, moveGenFake, 3);
            var bestRatingActual = target.SearchLevel(ChessColor.White, 1).First();

            Assert.AreEqual(400, bestRatingActual.Score);
            Assert.AreEqual(bestMove, bestRatingActual.Move);
        }

        // irregular

        // Depth 1

        [TestMethod]
        public void SearchMinimaxTest_WhenWhiteMovesFirstAnd1Level1_ThenCorrectSelected()
        {
            //      start
            //     /      
            //    1x           white move -> highest selected (x) 
            IEvaluator evalFake = new FakeEvaluator(new List<int>() { 100 });

            IMove bestMove = MoveMaker.White(1, 1);
            moveGenFake.AddGetAllMoves(new List<IMove>() { bestMove }); // first level
            moveGenFake.SetIsChecks(ChessColor.White, new List<bool>() { false });

            var target = new SearchMinimax(boardMock, evalFake, moveGenFake, 1);
            var bestRating = target.SearchLevel(ChessColor.White, 1).First();

            Assert.AreEqual(100, bestRating.Score);
            Assert.AreEqual(bestMove, bestRating.Move);
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
            IEvaluator evalFake = new FakeEvaluator(new List<int>() { 100, 200, 400 });

            IMove bestFakeMove = MoveMaker.White(1, 2);
            moveGenFake.AddGetAllMoves(new List<IMove>() { MoveMaker.White(1, 1), bestFakeMove }); // first level
            moveGenFake.AddGetAllMoves(new List<IMove>() { MoveMaker.Black(2, 1), MoveMaker.Black(1, 2) }); // second level 1.
            moveGenFake.AddGetAllMoves(new List<IMove>() { MoveMaker.Black(2, 3) }); // second level 2.
            moveGenFake.SetIsChecks(ChessColor.White, new List<bool>() { false, false });
            moveGenFake.SetIsChecks(ChessColor.Black, new List<bool>() { false, false, false });

            var target = new SearchMinimax(boardMock, evalFake, moveGenFake, 2);
            var bestRatingActual = target.SearchLevel(ChessColor.White, 1).First();

            Assert.AreEqual(400, bestRatingActual.Score);
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
            IEvaluator evalFake = new FakeEvaluator(new List<int>() { 600, 400, 300, 500 });

            IMove bestMove = MoveMaker.White(1, 2);
            moveGenFake.AddGetAllMoves(new List<IMove>() { MoveMaker.White(1, 1), bestMove }); // level 1
            moveGenFake.AddGetAllMoves(new List<IMove>() { MoveMaker.Black(2, 1), MoveMaker.Black(2, 2), MoveMaker.Black(2, 3) }); // level 2 a
            moveGenFake.AddGetAllMoves(new List<IMove>() { MoveMaker.Black(2, 4) }); // level 2 b
            moveGenFake.SetIsChecks(ChessColor.White, new List<bool>() { false, false });
            moveGenFake.SetIsChecks(ChessColor.Black, new List<bool>() { false, false, false, false });

            var target = new SearchMinimax(boardMock, evalFake, moveGenFake, 2);
            var bestRatingActual = target.SearchLevel(ChessColor.White, 1).First();

            Assert.AreEqual(500, bestRatingActual.Score);
            Assert.AreEqual(bestMove, bestRatingActual.Move);
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
            IEvaluator evalFake = new FakeEvaluator(new List<int>() { 100, 100 });

            moveGenFake.AddGetAllMoves(new List<IMove>() { MoveMaker.White(1, 1), MoveMaker.White(1, 2) }); // first level
            moveGenFake.SetIsChecks(ChessColor.White, new List<bool>() { false, false });

            var target = new SearchMinimax(boardMock, evalFake, moveGenFake, 1);
            var ratings = target.SearchLevel(ChessColor.White, 1);
            var rating0 = ratings.ElementAt(0);
            var rating1 = ratings.ElementAt(1);

            Assert.AreEqual(2, ratings.Count());

            Assert.AreEqual(100, rating0.Score);
            Assert.AreEqual(MoveMaker.White(1, 1), rating0.Move);

            Assert.AreEqual(100, rating1.Score);
            Assert.AreEqual(MoveMaker.White(1, 2), rating1.Move);
        }

        [TestMethod]
        public void SearchMinimaxTest_When2MovesHaveDifferentRating_ThenReturn1Move()
        {
            //      start
            //     /      \
            //    1       2x    white move -> highest selected (x) 
            IEvaluator evalFake = new FakeEvaluator(new List<int>() { 100, 200 });

            moveGenFake.AddGetAllMoves(new List<IMove>() { MoveMaker.White(1, 1), MoveMaker.White(1, 2) }); // first level
            moveGenFake.SetIsChecks(ChessColor.White, new List<bool>() { false, false });

            var target = new SearchMinimax(boardMock, evalFake, moveGenFake, 1);
            var ratings = target.SearchLevel(ChessColor.White, 1);
            var rating0 = ratings.ElementAt(0);

            Assert.AreEqual(1, ratings.Count());

            Assert.AreEqual(200, rating0.Score);
            Assert.AreEqual(MoveMaker.White(1, 2), rating0.Move);
        }

        [TestMethod]
        public void SearchMinimaxTest_When2MovesInTolerance_secondIsBiggest_ThenReturn2Moves()
        {
            //       start
            //     /   |    \
            //    1  1.99x   2x    white move -> highest selected (x) 
            IEvaluator evalFake = new FakeEvaluator(new List<int>() { 100, 199, 200 });

            moveGenFake.AddGetAllMoves(new List<IMove>() {
                MoveMaker.White(1, 1),
                MoveMaker.White(1, 2),
                MoveMaker.White(1, 3) });
            moveGenFake.SetIsChecks(ChessColor.White, new List<bool>() { false, false, false });

            var target = new SearchMinimax(boardMock, evalFake, moveGenFake, 1);
            var ratings = target.SearchLevel(ChessColor.White, 1);
            var rating0 = ratings.ElementAt(0);
            var rating1 = ratings.ElementAt(1);

            Assert.AreEqual(2, ratings.Count());

            Assert.AreEqual(199, rating0.Score);
            Assert.AreEqual(MoveMaker.White(1, 2), rating0.Move);

            Assert.AreEqual(200, rating1.Score);
            Assert.AreEqual(MoveMaker.White(1, 3), rating1.Move);
        }

        [TestMethod]
        public void SearchMinimaxTest_When2MovesInTolerance_FirstIsBiggest_ThenReturn2Moves()
        {
            //       start
            //     /  /    |    \
            //    1  1.01  2x  1.99x    white move -> highest selected (x) 
            IEvaluator evalFake = new FakeEvaluator(new List<int>() { 100, 101, 200, 199 });

            moveGenFake.AddGetAllMoves(new List<IMove>() {
                MoveMaker.White(1, 1),
                MoveMaker.White(1, 2),
                MoveMaker.White(1, 3),
                MoveMaker.White(1, 4) });
            moveGenFake.SetIsChecks(ChessColor.White, new List<bool>() { false, false, false, false });

            var target = new SearchMinimax(boardMock, evalFake, moveGenFake, 1);
            var ratings = target.SearchLevel(ChessColor.White, 1);
            var rating0 = ratings.ElementAt(0);
            var rating1 = ratings.ElementAt(1);

            Assert.AreEqual(2, ratings.Count());

            Assert.AreEqual(200, rating0.Score);
            Assert.AreEqual(MoveMaker.White(1, 3), rating0.Move);

            Assert.AreEqual(199, rating1.Score);
            Assert.AreEqual(MoveMaker.White(1, 4), rating1.Move);
        }

        [TestMethod]
        public void SearchMinimaxTest_When2MovesInTolerance_secondIsBiggest_ThenReturn2Moves_blackMoves()
        {
            //       start
            //     /   |    \
            //    1  -1.99   -2x    black move -> lowest selected (x) 
            IEvaluator evalFake = new FakeEvaluator(new List<int>() { 100, -199, -200 });

            moveGenFake.AddGetAllMoves(new List<IMove>() {
                MoveMaker.Black(1, 1),
                MoveMaker.Black(1, 2),
                MoveMaker.Black(1, 3) });
            moveGenFake.SetIsChecks(ChessColor.Black, new List<bool>() { false, false, false });

            var target = new SearchMinimax(boardMock, evalFake, moveGenFake, 1);
            var ratings = target.SearchLevel(ChessColor.Black, 1);
            var rating0 = ratings.ElementAt(0);
            var rating1 = ratings.ElementAt(1);

            Assert.AreEqual(2, ratings.Count());

            Assert.AreEqual(-199, rating0.Score);
            Assert.AreEqual(MoveMaker.Black(1, 2), rating0.Move);

            Assert.AreEqual(-200, rating1.Score);
            Assert.AreEqual(MoveMaker.Black(1, 3), rating1.Move);
        }

        // -----------------------------------------------------------------------------------
        // check mate and stall mate tests
        // -----------------------------------------------------------------------------------

        [TestMethod]
        public void SearchMinimaxTest_WhenWhiteHasNoMovesAndIsCheck_ThenBlackWins()
        {
            //      start
            //     /      
            //   no moves          white move -> highest selected (x) 
            IEvaluator evalFake = new FakeEvaluator(new List<int>() { });

            moveGenFake.AddGetAllMoves(new List<IMove>() { illegalMoveWhite }); // first level
            moveGenFake.SetIsChecks(ChessColor.White, new List<bool>() { true/*1a-illegal*/, true/*white is chackmate*/ });

            var target = new SearchMinimax(boardMock, evalFake, moveGenFake, 1);
            var bestRatingActual = target.SearchLevel(ChessColor.White, 1).First();

            AssertHelper.BlackWins(bestRatingActual);
            Assert.IsTrue(bestRatingActual.Move is NoLegalMove);
        }

        [TestMethod]
        public void SearchMinimaxTest_WhenWhiteHasNoMovesAndIsNotCheck_ThenDraw()
        {
            //      start
            //     /      
            //   no moves          white move -> highest selected (x) 
            IEvaluator evalFake = new FakeEvaluator(new List<int>() { });

            moveGenFake.AddGetAllMoves(new List<IMove>() { illegalMoveWhite }); // first level
            moveGenFake.SetIsChecks(ChessColor.White, new List<bool>() { true/*move illegal*/, false/*stallmate*/ });

            var target = new SearchMinimax(boardMock, evalFake, moveGenFake, 1);
            var bestRatingActual = target.SearchLevel(ChessColor.White, 1).First();

            Assert.AreEqual(0, bestRatingActual.Score);
            Assert.IsTrue(bestRatingActual.Move is NoLegalMove);
        }

        [TestMethod]
        public void SearchMinimaxTest_WhenWhiteChoosesBetweenCheckmateAndGoodMove_ThenWhiteSelectsCheckmate()
        {
            //      start
            //   1a /     \1b  
            //  10000x    100        white move -> highest selected (x)
            // no/moves    \2b
            //             100x       black move -> lowest selected (x)
            //              \3b
            //             100x       white move -> highest selected (x)

            IEvaluator evalFake = new FakeEvaluator(new List<int>() { 1000 });

            var bestMove = MoveMaker.White(1, 1);
            moveGenFake.AddGetAllMoves(new List<IMove>() { bestMove, MoveMaker.White(1, 2) }); // 1a 1b
            moveGenFake.AddGetAllMoves(new List<IMove>() { illegalMoveBlack }); // 2a
            moveGenFake.AddGetAllMoves(new List<IMove>() { MoveMaker.Black(2) }); // 2b
            moveGenFake.AddGetAllMoves(new List<IMove>() { MoveMaker.White(3) }); // 3b
            moveGenFake.SetIsChecks(ChessColor.White, new List<bool>() { false/*1a*/, false/*1b*/, false/*3b*/ });
            moveGenFake.SetIsChecks(ChessColor.Black, new List<bool>() { true/*2a illegal*/, true/*2a check*/, false/*2b*/ });

            var target = new SearchMinimax(boardMock, evalFake, moveGenFake, 3);
            var bestRatingActual = target.SearchLevel(ChessColor.White, 1).First();

            AssertHelper.WhiteWins(bestRatingActual);
            Assert.AreEqual(bestMove, bestRatingActual.Move);
        }

        [TestMethod]
        public void SearchMinimaxTest_WhenWhiteChoosesBetweenStallmateAndBadMove_ThenWhiteSelectsStallmate()
        {
            //      start
            //   1a /     \1b  
            //    0x     -100        white move -> highest selected (x)
            // no/moves    \2b
            //             -100x       black move -> lowest selected (x)
            //              \3b
            //             -100x       white move -> highest selected (x)

            IEvaluator evalFake = new FakeEvaluator(new List<int>() { -1000 });

            var bestMove = MoveMaker.White(1, 1);
            moveGenFake.AddGetAllMoves(new List<IMove>() { bestMove, MoveMaker.White(1, 2) }); // 1a 1b
            moveGenFake.AddGetAllMoves(new List<IMove>() { illegalMoveBlack }); // 2a
            moveGenFake.AddGetAllMoves(new List<IMove>() { MoveMaker.Black(2) }); // 2b
            moveGenFake.AddGetAllMoves(new List<IMove>() { MoveMaker.White(3) }); // 3b
            moveGenFake.SetIsChecks(ChessColor.White, new List<bool>() { false/*1a*/, false/*1b*/, false/*3b*/ });
            moveGenFake.SetIsChecks(ChessColor.Black, new List<bool>() { true/*2a illegal*/, false/*2a stallmate*/, false/*2b*/ });

            var target = new SearchMinimax(boardMock, evalFake, moveGenFake, 3);
            var bestRatingActual = target.SearchLevel(ChessColor.White, 1).First();

            AssertHelper.StallMate(bestRatingActual);
            Assert.AreEqual(bestMove, bestRatingActual.Move);
        }

        [TestMethod]
        public void SearchMinimaxTest_WhenWhiteChoosesBetweenStallmateAndGoodMove_ThenWhiteSelectsGoodMove()
        {
            //      start
            //   1a /     \1b  
            //    0x      100        white move -> highest selected (x)
            // no/moves    \2b
            //              100x       black move -> lowest selected (x)
            //              \3b
            //              100x       white move -> highest selected (x)

            IEvaluator evalFake = new FakeEvaluator(new List<int>() { 1000 });

            var bestMove = MoveMaker.White(1, 2);
            moveGenFake.AddGetAllMoves(new List<IMove>() { MoveMaker.White(1, 1), bestMove }); // 1a 1b
            moveGenFake.AddGetAllMoves(new List<IMove>() { illegalMoveBlack }); // 2a
            moveGenFake.AddGetAllMoves(new List<IMove>() { MoveMaker.Black(2) }); // 2b
            moveGenFake.AddGetAllMoves(new List<IMove>() { MoveMaker.White(3) }); // 3b
            moveGenFake.SetIsChecks(ChessColor.White, new List<bool>() { false/*1a*/, false/*1b*/, false/*3b*/ });
            moveGenFake.SetIsChecks(ChessColor.Black, new List<bool>() { true/*2a illegal*/, false/*2a stallmate*/, false/*2b*/ });

            var target = new SearchMinimax(boardMock, evalFake, moveGenFake, 3);
            var bestRatingActual = target.SearchLevel(ChessColor.White, 1).First();

            Assert.AreEqual(1000, bestRatingActual.Score);
            Assert.AreEqual(bestMove, bestRatingActual.Move);
        }

        [TestMethod]
        public void SearchMinimaxTest_WhenWhiteChoosesBeteweenCheckmateAndStallmate_ThenWhiteSelectsCheckmate()
        {
            //      start
            //   1a /     \1b  
            //  10000x     0        white move -> highest selected (x)
            // no/moves    no\moves 2b
            
            IEvaluator evalFake = new FakeEvaluator(new List<int>() { 1000 });

            var bestMove = MoveMaker.White(1, 1);
            moveGenFake.AddGetAllMoves(new List<IMove>() { bestMove, MoveMaker.White(1, 2) }); // 1a 1b
            moveGenFake.AddGetAllMoves(new List<IMove>() { illegalMoveBlack }); // 2a
            moveGenFake.AddGetAllMoves(new List<IMove>() { illegalMoveBlack }); // 2b
            moveGenFake.SetIsChecks(ChessColor.White, new List<bool>() { false, false });
            moveGenFake.SetIsChecks(ChessColor.Black, new List<bool>() { true/*2a-illegalMove*/, true/*2a-black is check*/, true/*2b-illegal*/, false/*2b-black-notcheck-stallmate*/ });

            var target = new SearchMinimax(boardMock, evalFake, moveGenFake, 3);
            var bestRatingActual = target.SearchLevel(ChessColor.White, 1).First();

            AssertHelper.WhiteWins(bestRatingActual);
            Assert.AreEqual(bestMove, bestRatingActual.Move);
        }

        [TestMethod]
        public void SearchMinimaxTest_WhenWhiteChoosesBeteweenCheckmateAndStallmate_ThenWhiteSelectsCheckmate_mirror()
        {
            //      start
            //   1a /     \1b  
            //     0     10000x   white move -> highest selected (x)
            //  no/moves    no\moves 2b

            IEvaluator evalFake = new FakeEvaluator(new List<int>() { 1000 });

            var bestMove = MoveMaker.White(1, 2);
            moveGenFake.AddGetAllMoves(new List<IMove>() { MoveMaker.White(1, 1), bestMove }); // 1a 1b
            moveGenFake.AddGetAllMoves(new List<IMove>() { illegalMoveBlack }); // 2a
            moveGenFake.AddGetAllMoves(new List<IMove>() { illegalMoveBlack }); // 2b
            moveGenFake.SetIsChecks(ChessColor.White, new List<bool>() { false, false }); // 1a, 1b legal
            moveGenFake.SetIsChecks(ChessColor.Black, new List<bool>() { true/*2a-illegalMove*/, false/*2a-black not check-stallmate*/, true/*2b-illegal*/, true/*2b-black-check*/ });
            
            var target = new SearchMinimax(boardMock, evalFake, moveGenFake, 3);
            var bestRatingActual = target.SearchLevel(ChessColor.White, 1).First();

            AssertHelper.WhiteWins(bestRatingActual);
            Assert.AreEqual(bestMove, bestRatingActual.Move);
        }

        [TestMethod]
        public void SearchMinimaxTest_WhenWhiteChoosesBeteweenCheckmateInLevel1AndCheckmateInLevel3_ThenWhiteSelectsCheckmateLevel1()
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

            var bestMove = MoveMaker.White(1, 1);
            moveGenFake.AddGetAllMoves(new List<IMove>() { bestMove, MoveMaker.White(1, 2) }); // 1a 1b
            moveGenFake.AddGetAllMoves(new List<IMove>() { illegalMoveBlack }); // 2a
            moveGenFake.AddGetAllMoves(new List<IMove>() { MoveMaker.Black(2) }); // 2b
            moveGenFake.AddGetAllMoves(new List<IMove>() { MoveMaker.White(3) }); // 3b
            moveGenFake.AddGetAllMoves(new List<IMove>() { illegalMoveBlack }); // 4b
            moveGenFake.SetIsChecks(ChessColor.White, new List<bool>() { false/*1a*/, false/*1b*/, false/*3a*/});
            moveGenFake.SetIsChecks(ChessColor.Black, new List<bool>() { true/*2a-illegalMove*/, true/*2a-black check*/, false/*2b*/, true/*4b-illegal*/, true/*4b check*/});
            
            var target = new SearchMinimax(boardMock, evalFake, moveGenFake, 4);
            var bestRatingActual = target.SearchLevel(ChessColor.White, 1).First();

            AssertHelper.WhiteWins(bestRatingActual);
            Assert.AreEqual(bestMove, bestRatingActual.Move);
        }

        [TestMethod]
        public void SearchMinimaxTest_WhenWhiteChoosesBeteweenCheckmateInLevel1AndCheckmateInLevel3_ThenWhiteSelectsCheckmateLevel1_mirror()
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

            var bestMove = MoveMaker.White(1, 2);
            moveGenFake.AddGetAllMoves(new List<IMove>() { MoveMaker.White(1, 1), bestMove }); // 1a 1b
            moveGenFake.AddGetAllMoves(new List<IMove>() { MoveMaker.Black(2) }); // 2a
            moveGenFake.AddGetAllMoves(new List<IMove>() { MoveMaker.White(3) }); // 3a
            moveGenFake.AddGetAllMoves(new List<IMove>() { illegalMoveBlack }); // 4a
            moveGenFake.AddGetAllMoves(new List<IMove>() { illegalMoveBlack }); // 2b
            moveGenFake.SetIsChecks(ChessColor.White, new List<bool>() { false/*1a*/, false/*1b*/, false/*3a*/  });
            moveGenFake.SetIsChecks(ChessColor.Black, new List<bool>() { false/*2a*/, true/*4a illegal*/, true/*4a check*/, true/*2b illegal*/, true/*2b check*/ });

            var target = new SearchMinimax(boardMock, evalFake, moveGenFake, 4);
            var bestRatingActual = target.SearchLevel(ChessColor.White, 1).First();

            AssertHelper.WhiteWins(bestRatingActual);
            Assert.AreEqual(bestMove, bestRatingActual.Move);
        }
    }
}
