using System.Linq;
using System.Collections.Generic;
using MantaChessEngine;
using MantaChessEngine.Doubles;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MantaChessEngineTest
{
    /// <summary>
    /// Tests for SearchMinimax with mock moves and evaluation: check mate and stall mate moves.
    /// </summary>
    
    [TestClass]
    public class SearchMinimaxTestsWithMocks_CheckAndStallMate
    {
        // -------------------------------------------------
        // checkmate and stall mate
        // -------------------------------------------------

        // SearchLevel Tests

        [TestMethod]
        public void SearchMinimaxTest_WhenBlackIsCheckMate_Now_AndBlackMoves_MaxDepth2_Then_WhiteWins_NoLegalMove()
        {
            //      start ***
            //     /              black move -> lowest selected (x), the only move black can do is an illegal move.    
            //  10000x
            //   /                white move -> highest selected (x), whites move captures the black king, which is also an illegal move. ==> this should be KingCaptureMove
            // 10000x

            FakeMoveGeneratorMulitlevel moveGenFake = new FakeMoveGeneratorMulitlevel();
            moveGenFake.AddGetAllMoves(new List<IMove>() { MakeBlackMove(1) }); // level 1
            moveGenFake.AddGetAllMoves(new List<IMove>() { MakeWhiteMove(2) }); // level 2
            moveGenFake.SetIsChecks(new List<bool>() { true }); // in Position *** black is in check
            IEvaluator evalFake = new FakeEvaluator(new List<float>() { 10000 });
            IBoard boardFake = new FakeBoard();

            var target = new SearchMinimax(evalFake, moveGenFake);
            target.SetMaxDepth(2);
            var bestRatingActual = target.SearchLevel(boardFake, Definitions.ChessColor.Black, 2).First();

            Assert.AreEqual(10000, bestRatingActual.Score); // Note: this means white wins.
            Assert.AreEqual(new NoLegalMove(), bestRatingActual.Move); // note: this means the game is over in its original position.
        }

        [TestMethod]
        public void SearchMinimaxTest_WhenBlackIsStallMate_Now_AndBlackMoves_MaxDepth2_Then_Draw_NoLegalMove()
        {
            //      start ***
            //     /              black move -> lowest selected (x), the only move black can do is an illegal move.
            //  10000x
            //   /                white move -> highest selected (x), whites move captures the black king, which is also an illegal move. ==> this should be KingCaptureMove
            // 10000x

            FakeMoveGeneratorMulitlevel moveGenFake = new FakeMoveGeneratorMulitlevel();
            moveGenFake.AddGetAllMoves(new List<IMove>() { MakeBlackMove(1) }); // level 1
            moveGenFake.AddGetAllMoves(new List<IMove>() { MakeWhiteMove(2) }); // level 2
            moveGenFake.SetIsChecks(new List<bool>() { false }); // in Position *** black is not in check
            IEvaluator evalFake = new FakeEvaluator(new List<float>() { 10000 });
            IBoard boardFake = new FakeBoard();

            var target = new SearchMinimax(evalFake, moveGenFake);
            target.SetMaxDepth(2);
            var bestRatingActual = target.SearchLevel(boardFake, Definitions.ChessColor.Black, 2).First();

            Assert.AreEqual(0, bestRatingActual.Score); // means draw
            Assert.AreEqual(new NoLegalMove(), bestRatingActual.Move); // game is over at startposition (***)
        }

        [TestMethod]
        public void SearchMinimaxTest_WhenBlackIsCheckMate_Now_AndBlackMoves_MaxDepth3_Then_WhiteWins_NoLegalMove()
        {
            //      start ***
            //     /      
            //  10000x             black move -> lowest selected (x), the only move black can do is an illegal move.
            //   /             
            // 10000x             white move -> highest selected (x), whites move captures the black king, which is also an illegal move. ==> this should be KingCaptureMove
            //  /
            // no more moves

            FakeMoveGeneratorMulitlevel moveGenFake = new FakeMoveGeneratorMulitlevel();
            moveGenFake.AddGetAllMoves(new List<IMove>() { MakeBlackMove(1) }); // level 1
            moveGenFake.AddGetAllMoves(new List<IMove>() { MakeWhiteMove(2) }); // level 2
            moveGenFake.AddGetAllMoves(new List<IMove>() { }); // level 3 no moves
            moveGenFake.SetIsChecks(new List<bool>() { true }); // in Position *** black is in check
            IEvaluator evalFake = new FakeEvaluator(new List<float>() { }); // eval not called!
            IBoard boardFake = new FakeBoard();

            var target = new SearchMinimax(evalFake, moveGenFake);
            target.SetMaxDepth(3);
            var bestRatingActual = target.SearchLevel(boardFake, Definitions.ChessColor.Black, 3).First();

            Assert.AreEqual(10000, bestRatingActual.Score);
            Assert.AreEqual(new NoLegalMove(), bestRatingActual.Move);
        }

        [TestMethod]
        public void SearchMinimaxTest_WhenBlackIsStallMate_Now_AndBlackMoves_MaxDepth3_Then_Draw_NoLegalMove()
        {
            //      start ***
            //     /      
            //  10000x             black move -> lowest selected (x), the only move black can do is an illegal move.
            //   /             
            // 10000x             white move -> highest selected (x), whites move captures the black king, which is also an illegal move. ==> this should be KingCaptureMove
            //  /
            // no more moves

            FakeMoveGeneratorMulitlevel moveGenFake = new FakeMoveGeneratorMulitlevel();
            moveGenFake.AddGetAllMoves(new List<IMove>() { MakeBlackMove(1) }); // level 1
            moveGenFake.AddGetAllMoves(new List<IMove>() { MakeWhiteMove(2) }); // level 2
            moveGenFake.AddGetAllMoves(new List<IMove>() { }); // level 3 no moves
            moveGenFake.SetIsChecks(new List<bool>() { false }); // in Position *** black is not in check
            IEvaluator evalFake = new FakeEvaluator(new List<float>() { }); // eval not called!
            IBoard boardFake = new FakeBoard();

            var target = new SearchMinimax(evalFake, moveGenFake);
            target.SetMaxDepth(3);
            var bestRatingActual = target.SearchLevel(boardFake, Definitions.ChessColor.Black, 3).First();

            Assert.AreEqual(0, bestRatingActual.Score); // draw
            Assert.AreEqual(new NoLegalMove(), bestRatingActual.Move); // game is over in position ***
        }

        [TestMethod]
        public void SearchMinimaxTest_WhenBlackIsCheckMate_Now_AndBlackMoves_MaxDepth4_Then_WhiteWins_NoLegalMove()
        {
            //      start ***
            //     /      
            //  10000x             black move -> lowest selected (x), the only move black can do is an illegal move.
            //   /             
            // 10000x             white move -> highest selected (x), whites move captures the black king, which is also an illegal move. ==> this should be KingCaptureMove
            //  /
            // no more moves

            FakeMoveGeneratorMulitlevel moveGenFake = new FakeMoveGeneratorMulitlevel();
            moveGenFake.AddGetAllMoves(new List<IMove>() { MakeBlackMove(1) }); // level 1
            moveGenFake.AddGetAllMoves(new List<IMove>() { MakeWhiteMove(2) }); // level 2
            moveGenFake.AddGetAllMoves(new List<IMove>() { }); // level 3 no moves
            ////moveGenFake.AddGetAllMoves(new List<IMove>() { }); // level 4 no moves
            moveGenFake.SetIsChecks(new List<bool>() { true }); // in Position *** black is in check
            IEvaluator evalFake = new FakeEvaluator(new List<float>() { }); // eval not called!
            IBoard boardFake = new FakeBoard();

            var target = new SearchMinimax(evalFake, moveGenFake);
            target.SetMaxDepth(4);
            var bestRatingActual = target.SearchLevel(boardFake, Definitions.ChessColor.Black, 4).First();

            Assert.AreEqual(10000, bestRatingActual.Score);
            Assert.AreEqual(new NoLegalMove(), bestRatingActual.Move);
        }

        [TestMethod]
        public void SearchMinimaxTest_WhenBlackIsStallMate_Now_AndBlackMoves_MaxDepth4_Then_Draw_NoLegalMove()
        {
            //      start ***
            //     /      
            //  10000x             black move -> lowest selected (x), the only move black can do is an illegal move.
            //   /             
            // 10000x             white move -> highest selected (x), whites move captures the black king, which is also an illegal move. ==> this should be KingCaptureMove
            //  /
            // no more moves

            FakeMoveGeneratorMulitlevel moveGenFake = new FakeMoveGeneratorMulitlevel();
            moveGenFake.AddGetAllMoves(new List<IMove>() { MakeBlackMove(1) }); // level 1
            moveGenFake.AddGetAllMoves(new List<IMove>() { MakeWhiteMove(2) }); // level 2
            moveGenFake.AddGetAllMoves(new List<IMove>() { }); // level 3 no moves
            moveGenFake.AddGetAllMoves(new List<IMove>() { }); // level 4 no moves
            moveGenFake.SetIsChecks(new List<bool>() { false }); // in Position *** black is not in check
            IEvaluator evalFake = new FakeEvaluator(new List<float>() { }); // eval not called!
            IBoard boardFake = new FakeBoard();

            var target = new SearchMinimax(evalFake, moveGenFake);
            target.SetMaxDepth(4);
            var bestRatingActual = target.SearchLevel(boardFake, Definitions.ChessColor.Black, 4).First();

            Assert.AreEqual(0, bestRatingActual.Score);
            Assert.AreEqual(new NoLegalMove(), bestRatingActual.Move);
        }

        // -------------------------------------------------
        // Search chooses between normal move and winning/loosing move
        // -------------------------------------------------

        [TestMethod]
        public void SearchMinimaxTest_WhenWhiteChoosesBetweenGoodMoveAndLosing_ThenWhiteSelectsNormalGoodMove()
        {
            //       start
            //    1a /     \1b
            //   1010x   -10000 ***    white move -> highest selected (x), 1b is check mate for black, white must not select this.
            //   2a/        2b\   
            //  1010x     -10000x     black move -> lowest selected (x)
            //   3a/         3b\   
            //  1010x       -10000x     white move -> highest selected (x)

            FakeMoveGeneratorMulitlevel moveGenFake = new FakeMoveGeneratorMulitlevel();
            var bestMove = MakeWhiteMove(1, 1);
            moveGenFake.AddGetAllMoves(new List<IMove>() { bestMove, MakeWhiteMove(1, 2) }); // level 1
            moveGenFake.AddGetAllMoves(new List<IMove>() { MakeBlackMove(2) }); // level 2 a
            moveGenFake.AddGetAllMoves(new List<IMove>() { MakeWhiteMove(3) }); // level 3 a
            moveGenFake.AddGetAllMoves(new List<IMove>() { MakeBlackMove(2, 2) }); // level 2 b
            moveGenFake.AddGetAllMoves(new List<IMove>() { MakeWhiteMove(3, 2) }); // level 3 b
            moveGenFake.SetIsChecks(new List<bool>() { true });
            IEvaluator evalFake = new FakeEvaluator(new List<float>() { 1010, -10000 });
            IBoard boardFake = new FakeBoard();

            var target = new SearchMinimax(evalFake, moveGenFake);
            target.SetMaxDepth(3);
            var bestRatingActual = target.SearchLevel(boardFake, Definitions.ChessColor.White, 3).First();

            Assert.AreEqual(1010, bestRatingActual.Score);
            Assert.AreEqual(bestMove, bestRatingActual.Move);
        }

        [TestMethod]
        public void SearchMinimaxTest_WhenWhiteChoosesBetweenNormalMoveAndWinning_ThenWhiteSelectsWinningMove()
        {
            //       start
            //    1a /     \1b
            //   1010     10000x     white move -> highest selected (x), but here white has lost king-> no move returned
            //   2a/        2b\   
            //  1010x      10000x     black move -> lowest selected (x)
            //   3a/         3b\   
            //  1010x        10000x     white move -> highest selected (x)

            FakeMoveGeneratorMulitlevel moveGenFake = new FakeMoveGeneratorMulitlevel();
            var bestMove = MakeWhiteMove(1, 2);
            moveGenFake.AddGetAllMoves(new List<IMove>() { MakeWhiteMove(1, 1), bestMove  } ); // level 1
            moveGenFake.AddGetAllMoves(new List<IMove>() { MakeBlackMove(2) }); // level 2 a
            moveGenFake.AddGetAllMoves(new List<IMove>() { MakeWhiteMove(3) }); // level 3 a
            moveGenFake.AddGetAllMoves(new List<IMove>() { MakeBlackMove(2, 2) }); // level 2 b
            moveGenFake.AddGetAllMoves(new List<IMove>() { MakeWhiteMove(3, 2) }); // level 3 b
            moveGenFake.SetIsChecks(new List<bool>() { true });
            IEvaluator evalFake = new FakeEvaluator(new List<float>() { 1010, 10000 });
            IBoard boardFake = new FakeBoard();

            var target = new SearchMinimax(evalFake, moveGenFake);
            target.SetMaxDepth(3);
            var bestRatingActual = target.SearchLevel(boardFake, Definitions.ChessColor.White, 3).First();

            Assert.AreEqual(10000, bestRatingActual.Score);
            Assert.AreEqual(bestMove, bestRatingActual.Move);
        }

        [TestMethod]
        public void SearchMinimaxTest_WhenBlackChoosesBetweenGoodMoveAndLoosing_ThenBlackSelectsNormalGoodMove()
        {
            //       start
            //    1a /     \1b
            //   -1010x    10000 ***   black move -> lowest selected (x), 1b is check mate for white, black must not select this.
            //   2a/        2b\   
            //  -1010x      10000x     white move -> highest selected (x)
            //   3a/         3b\   
            // -1010x        10000x    black move -> lowest selected (x)

            FakeMoveGeneratorMulitlevel moveGenFake = new FakeMoveGeneratorMulitlevel();
            var bestMove = MakeBlackMove(1, 1);
            moveGenFake.AddGetAllMoves(new List<IMove>() { bestMove, MakeBlackMove(1, 2) }); // level 1
            moveGenFake.AddGetAllMoves(new List<IMove>() { MakeWhiteMove(2) }); // level 2 a
            moveGenFake.AddGetAllMoves(new List<IMove>() { MakeBlackMove(3) }); // level 3 a
            moveGenFake.AddGetAllMoves(new List<IMove>() { MakeWhiteMove(2, 2) }); // level 2 b
            moveGenFake.AddGetAllMoves(new List<IMove>() { MakeBlackMove(3, 2) }); // level 3 b
            moveGenFake.SetIsChecks(new List<bool>() { true });
            IEvaluator evalFake = new FakeEvaluator(new List<float>() { -1010, 10000 });
            IBoard boardFake = new FakeBoard();

            var target = new SearchMinimax(evalFake, moveGenFake);
            target.SetMaxDepth(3);
            var bestRatingActual = target.SearchLevel(boardFake, Definitions.ChessColor.Black, 3).First();

            Assert.AreEqual(-1010, bestRatingActual.Score);
            Assert.AreEqual(bestMove, bestRatingActual.Move);
        }

        [TestMethod]
        public void SearchMinimaxTest_WhenBlackChoosesBetweenNormalMoveAndWinning_ThenBlackSelectsWinningMove()
        {
            //        start
            //    1a /     \1b
            //   1010     -10000x     black move -> lowest selected (x), 1b white is checkmate, black must select this.
            //   2a/        2b\   
            //  1010x      -10000x     white move -> highest selected (x)
            //   3a/         3b\   
            //  1010x        -10000x     black move -> lowest selected (x)

            FakeMoveGeneratorMulitlevel moveGenFake = new FakeMoveGeneratorMulitlevel();
            var bestMove = MakeBlackMove(1, 2);
            moveGenFake.AddGetAllMoves(new List<IMove>() { MakeBlackMove(1, 1), bestMove }); // level 1
            moveGenFake.AddGetAllMoves(new List<IMove>() { MakeWhiteMove(2) }); // level 2 a
            moveGenFake.AddGetAllMoves(new List<IMove>() { MakeBlackMove(3) }); // level 3 a
            moveGenFake.AddGetAllMoves(new List<IMove>() { MakeWhiteMove(2, 2) }); // level 2 b
            moveGenFake.AddGetAllMoves(new List<IMove>() { MakeBlackMove(3, 2) }); // level 3 b
            moveGenFake.SetIsChecks(new List<bool>() { true });
            IEvaluator evalFake = new FakeEvaluator(new List<float>() { 1010, -10000 });
            IBoard boardFake = new FakeBoard();

            var target = new SearchMinimax(evalFake, moveGenFake);
            target.SetMaxDepth(3);
            var bestRatingActual = target.SearchLevel(boardFake, Definitions.ChessColor.Black, 3).First();

            Assert.AreEqual(-10000, bestRatingActual.Score);
            Assert.AreEqual(bestMove, bestRatingActual.Move);
        }

        // -------------------------------------------------
        // Search chooses between normal move and winning/loosing move.
        // Capture happens before deepest level. Move generator delivers no more move because king is lost.
        // -------------------------------------------------

        [TestMethod]
        public void SearchMinimaxTest_WhenWhiteChoosesBetweenNormalMoveAndWinning_ThenWhiteSelectsWinningMove_Depth4()
        {
            //       start
            //    1a /     \1b
            //   1010     10000x ***    white move -> highest selected (x), 1b is winning move for white.
            //   2a/        2b\   
            //  1010x      10000x       black move -> lowest selected (x). 2b is illegal move for black
            //   3a/         3b\   
            //  1010x        10000x     white move -> highest selected (x). 3b white captures king (illegal)
            //   4a/         4b\   
            //  1010x       no moves    black move -> lowest selected (x). 4b no black moves returned as black has no king.

            FakeMoveGeneratorMulitlevel moveGenFake = new FakeMoveGeneratorMulitlevel();
            var bestMove = MakeWhiteMove(1, 2);
            moveGenFake.AddGetAllMoves(new List<IMove>() { MakeWhiteMove(1, 1), bestMove }); // level 1
            moveGenFake.AddGetAllMoves(new List<IMove>() { MakeBlackMove(2) }); // level 2 a
            moveGenFake.AddGetAllMoves(new List<IMove>() { MakeWhiteMove(3) }); // level 3 a
            moveGenFake.AddGetAllMoves(new List<IMove>() { MakeBlackMove(4) }); // level 4 a
            moveGenFake.AddGetAllMoves(new List<IMove>() { MakeBlackMove(2, 2) }); // level 2 b
            moveGenFake.AddGetAllMoves(new List<IMove>() { MakeWhiteMove(3, 2) }); // level 3 b
            moveGenFake.AddGetAllMoves(new List<IMove>()); // no move generated for 4b
            moveGenFake.SetIsChecks(new List<bool>() { true });
            IEvaluator evalFake = new FakeEvaluator(new List<float>() { 1010 }); // eval not called for right branch
            IBoard boardFake = new FakeBoard();

            var target = new SearchMinimax(evalFake, moveGenFake);
            target.SetMaxDepth(4);
            var bestRatingActual = target.SearchLevel(boardFake, Definitions.ChessColor.White, 4).First();

            Assert.AreEqual(10000, bestRatingActual.Score);
            Assert.AreEqual(bestMove, bestRatingActual.Move);
        }

        [TestMethod]
        public void SearchMinimaxTest_WhenWhiteChoosesBetweenNormalMoveAndLoosing_ThenWhiteSelectsNormalMove_Depth5()
        {
            //        start
            //    1a /     \1b
            //   1010x     -10000      white move -> highest selected (x), 1b is loosing move for white.
            //   2a/        2b\   
            //  1010x      -10000x       black move -> lowest selected (x). 2b is check mate move for black
            //   3a/         3b\   
            //  1010x       -10000x     white move -> highest selected (x). 3b white moves illegal
            //   4a/         4b\   
            //  1010x       -10000x    black move -> lowest selected (x). 4b black captures white king (illegal)
            //   5a/         5b\   
            //  1010x       no moves    white move -> highest selected (x). 5b no white moves returned as white has no king.

            FakeMoveGeneratorMulitlevel moveGenFake = new FakeMoveGeneratorMulitlevel();
            var bestMove = MakeWhiteMove(1, 1);
            moveGenFake.AddGetAllMoves(new List<IMove>() { bestMove, MakeWhiteMove(1, 2) }); // level 1
            moveGenFake.AddGetAllMoves(new List<IMove>() { MakeBlackMove(2) }); // level 2 a
            moveGenFake.AddGetAllMoves(new List<IMove>() { MakeWhiteMove(3) }); // level 3 a
            moveGenFake.AddGetAllMoves(new List<IMove>() { MakeBlackMove(4) }); // level 4 a
            moveGenFake.AddGetAllMoves(new List<IMove>() { MakeWhiteMove(5) }); // level 5 a
            moveGenFake.AddGetAllMoves(new List<IMove>() { MakeBlackMove(2, 2) }); // level 2 b
            moveGenFake.AddGetAllMoves(new List<IMove>() { MakeWhiteMove(3, 2) }); // level 3 b
            moveGenFake.AddGetAllMoves(new List<IMove>() { MakeBlackMove(4, 2) }); // level 4 b
            moveGenFake.AddGetAllMoves(new List<IMove>()); // no move generated for 5b
            moveGenFake.SetIsChecks(new List<bool>() { true });
            IEvaluator evalFake = new FakeEvaluator(new List<float>() { 1010 }); // eval not called for right branch
            IBoard boardFake = new FakeBoard();

            var target = new SearchMinimax(evalFake, moveGenFake);
            target.SetMaxDepth(5);
            var bestRatingActual = target.SearchLevel(boardFake, Definitions.ChessColor.White, 5).First();

            Assert.AreEqual(1010, bestRatingActual.Score);
            Assert.AreEqual(bestMove, bestRatingActual.Move);
        }

        // -------------------------------------------------
        // Search chooses between good/bad move and stall mate (draw)
        // -------------------------------------------------

        [TestMethod]
        public void SearchMinimaxTest_WhenWhiteChoosesBetweenGoodMoveAndStallMate_ThenWhiteSelectsGoodMove()
        {
            //       start
            //    1a /     \1b
            //   1010x      0 ***    white move -> highest selected (x), 1b is stall mate, white must not select this.
            //   2a/        2b\   
            //  1010x       10000x     black move -> lowest selected (x)
            //   3a/         3b\   
            //  1010x        10000x    white move -> highest selected (x)

            FakeMoveGeneratorMulitlevel moveGenFake = new FakeMoveGeneratorMulitlevel();
            var bestMove = MakeWhiteMove(1, 1);
            moveGenFake.AddGetAllMoves(new List<IMove>() { bestMove, MakeWhiteMove(1, 2) }); // level 1
            moveGenFake.AddGetAllMoves(new List<IMove>() { MakeBlackMove(2) }); // level 2 a
            moveGenFake.AddGetAllMoves(new List<IMove>() { MakeWhiteMove(3) }); // level 3 a
            moveGenFake.AddGetAllMoves(new List<IMove>() { MakeBlackMove(2, 2) }); // level 2 b
            moveGenFake.AddGetAllMoves(new List<IMove>() { MakeWhiteMove(3, 2) }); // level 3 b
            moveGenFake.SetIsChecks(new List<bool>() { false }); // stall mate at ***
            IEvaluator evalFake = new FakeEvaluator(new List<float>() { 1010, 10000 });
            IBoard boardFake = new FakeBoard();

            var target = new SearchMinimax(evalFake, moveGenFake);
            target.SetMaxDepth(3);
            var bestRatingActual = target.SearchLevel(boardFake, Definitions.ChessColor.White, 3).First();

            Assert.AreEqual(1010, bestRatingActual.Score);
            Assert.AreEqual(bestMove, bestRatingActual.Move);
        }

        [TestMethod]
        public void SearchMinimaxTest_WhenWhiteChoosesBetweenBadMoveAndStallMate_ThenWhiteSelectsStallMate()
        {
            //       start
            //    1a /     \1b
            //  -1010       0x ***    white move -> highest selected (x), 1b is stall mate, white must select this.
            //   2a/        2b\   
            // -1010x       10000x     black move -> lowest selected (x)
            //   3a/         3b\   
            // -1010x        10000x    white move -> highest selected (x)

            FakeMoveGeneratorMulitlevel moveGenFake = new FakeMoveGeneratorMulitlevel();
            var bestMove = MakeWhiteMove(1, 2);
            moveGenFake.AddGetAllMoves(new List<IMove>() { MakeWhiteMove(1, 1), bestMove  }); // level 1
            moveGenFake.AddGetAllMoves(new List<IMove>() { MakeBlackMove(2) }); // level 2 a
            moveGenFake.AddGetAllMoves(new List<IMove>() { MakeWhiteMove(3) }); // level 3 a
            moveGenFake.AddGetAllMoves(new List<IMove>() { MakeBlackMove(2, 2) }); // level 2 b
            moveGenFake.AddGetAllMoves(new List<IMove>() { MakeWhiteMove(3, 2) }); // level 3 b
            moveGenFake.SetIsChecks(new List<bool>() { false }); // stall mate at ***
            IEvaluator evalFake = new FakeEvaluator(new List<float>() { -1010, -10000 });
            IBoard boardFake = new FakeBoard();

            var target = new SearchMinimax(evalFake, moveGenFake);
            target.SetMaxDepth(3);
            var bestRatingActual = target.SearchLevel(boardFake, Definitions.ChessColor.White, 3).First();

            Assert.AreEqual(0, bestRatingActual.Score);
            Assert.AreEqual(bestMove, bestRatingActual.Move);
        }

        [TestMethod]
        public void SearchMinimaxTest_WhenBlackChoosesBetweenGoodMoveAndStallMate_ThenBlackSelectsGoodMove()
        {
            //         start
            //      1a/     \1b
            //   - 1010x      0 ***    black move -> lowest selected (x), 1b is stall mate, black must not select this.
            //    2a/        2b\   
            //  -1010x       -10000x     white move -> highest selected (x)
            //   3a/         3b\   
            // -1010x        -10000x     black move -> lowest selected (x)

            FakeMoveGeneratorMulitlevel moveGenFake = new FakeMoveGeneratorMulitlevel();
            var bestMove = MakeBlackMove(1, 1);
            moveGenFake.AddGetAllMoves(new List<IMove>() { bestMove, MakeBlackMove(1, 2) }); // level 1
            moveGenFake.AddGetAllMoves(new List<IMove>() { MakeWhiteMove(2) }); // level 2 a
            moveGenFake.AddGetAllMoves(new List<IMove>() { MakeBlackMove(3) }); // level 3 a
            moveGenFake.AddGetAllMoves(new List<IMove>() { MakeWhiteMove(2, 2) }); // level 2 b
            moveGenFake.AddGetAllMoves(new List<IMove>() { MakeBlackMove(3, 2) }); // level 3 b
            moveGenFake.SetIsChecks(new List<bool>() { false }); // stall mate at ***
            IEvaluator evalFake = new FakeEvaluator(new List<float>() { -1010, -10000 });
            IBoard boardFake = new FakeBoard();

            var target = new SearchMinimax(evalFake, moveGenFake);
            target.SetMaxDepth(3);
            var bestRatingActual = target.SearchLevel(boardFake, Definitions.ChessColor.Black, 3).First();

            Assert.AreEqual(-1010, bestRatingActual.Score);
            Assert.AreEqual(bestMove, bestRatingActual.Move);
        }

        [TestMethod]
        public void SearchMinimaxTest_WhenBlackChoosesBetweenBadMoveAndStallMate_ThenBlackSelectsStallMate()
        {
            //       start
            //    1a /     \1b
            //   1010       0x ***    black move -> lowest selected (x), 1b is stall mate, black must select this.
            //   2a/        2b\   
            //  1010x       -10000x     white move -> highest selected (x)
            //   3a/         3b\   
            //  1010x        -10000x    black move -> lowest selected (x)

            FakeMoveGeneratorMulitlevel moveGenFake = new FakeMoveGeneratorMulitlevel();
            var bestMove = MakeBlackMove(1, 2);
            moveGenFake.AddGetAllMoves(new List<IMove>() { MakeBlackMove(1, 1), bestMove }); // level 1
            moveGenFake.AddGetAllMoves(new List<IMove>() { MakeWhiteMove(2) }); // level 2 a
            moveGenFake.AddGetAllMoves(new List<IMove>() { MakeBlackMove(3) }); // level 3 a
            moveGenFake.AddGetAllMoves(new List<IMove>() { MakeWhiteMove(2, 2) }); // level 2 b
            moveGenFake.AddGetAllMoves(new List<IMove>() { MakeBlackMove(3, 2) }); // level 3 b
            moveGenFake.SetIsChecks(new List<bool>() { false }); // stall mate at ***
            IEvaluator evalFake = new FakeEvaluator(new List<float>() { 1010, -10000 });
            IBoard boardFake = new FakeBoard();

            var target = new SearchMinimax(evalFake, moveGenFake);
            target.SetMaxDepth(3);
            var bestRatingActual = target.SearchLevel(boardFake, Definitions.ChessColor.Black, 3).First();

            Assert.AreEqual(0, bestRatingActual.Score);
            Assert.AreEqual(bestMove, bestRatingActual.Move);
        }

        // -------------------------------------------------
        // Search chooses between good/bad move and stall mate (draw)
        // King lost before deepest level.
        // -------------------------------------------------

        [TestMethod]
        public void SearchMinimaxTest_WhenWhiteChoosesBetweenGoodMoveAndStallMate_ThenWhiteSelectsGoodMove_Depth4()
        {
            //       start
            //    1a /     \1b
            //   1010x       0          white move -> highest selected (x), 1b is stall mate.
            //   2a/        2b\   
            //  1010x      10000x       black move -> lowest selected (x). 2b is illegal move for black
            //   3a/         3b\   
            //  1010x        10000x     white move -> highest selected (x). 3b white captures king (illegal)
            //   4a/         4b\   
            //  1010x       no moves    black move -> lowest selected (x). 4b no black moves returned as black has no king.

            FakeMoveGeneratorMulitlevel moveGenFake = new FakeMoveGeneratorMulitlevel();
            var bestMove = MakeWhiteMove(1, 1);
            moveGenFake.AddGetAllMoves(new List<IMove>() { bestMove, MakeWhiteMove(1, 2) }); // level 1
            moveGenFake.AddGetAllMoves(new List<IMove>() { MakeBlackMove(2) }); // level 2 a
            moveGenFake.AddGetAllMoves(new List<IMove>() { MakeWhiteMove(3) }); // level 3 a
            moveGenFake.AddGetAllMoves(new List<IMove>() { MakeBlackMove(4) }); // level 4 a
            moveGenFake.AddGetAllMoves(new List<IMove>() { MakeBlackMove(2, 2) }); // level 2 b
            moveGenFake.AddGetAllMoves(new List<IMove>() { MakeWhiteMove(3, 2) }); // level 3 b
            moveGenFake.AddGetAllMoves(new List<IMove>()); // no move generated for 4b
            moveGenFake.SetIsChecks(new List<bool>() { false });
            IEvaluator evalFake = new FakeEvaluator(new List<float>() { 1010 }); // eval not called for right branch
            IBoard boardFake = new FakeBoard();

            var target = new SearchMinimax(evalFake, moveGenFake);
            target.SetMaxDepth(4);
            var bestRatingActual = target.SearchLevel(boardFake, Definitions.ChessColor.White, 4).First();

            Assert.AreEqual(1010, bestRatingActual.Score);
            Assert.AreEqual(bestMove, bestRatingActual.Move);
        }

        [TestMethod]
        public void SearchMinimaxTest_WhenWhiteChoosesBetweenBadMoveAndStallMate_ThenWhiteSelectsStallMate_Depth5()
        {
            //        start
            //    1a /     \1b
            //  -1010       0x          white move -> highest selected (x), 1b normal move that leads to stall mate.
            //   2a/        2b\   
            // -1010x         0x        black move -> lowest selected (x). 2b is stall mate move for black
            //   3a/         3b\   
            // -1010x        -10000x    white move -> highest selected (x). 3b white moves illegal
            //   4a/         4b\   
            // -1010x        -10000x    black move -> lowest selected (x). 4b black captures white king (illegal)
            //   5a/         5b\   
            // -1010x       no moves    white move -> highest selected (x). 5b no moves returned as white has no king.

            FakeMoveGeneratorMulitlevel moveGenFake = new FakeMoveGeneratorMulitlevel();
            var bestMove = MakeWhiteMove(1, 2);
            moveGenFake.AddGetAllMoves(new List<IMove>() { MakeWhiteMove(1, 1), bestMove }); // level 1
            moveGenFake.AddGetAllMoves(new List<IMove>() { MakeBlackMove(2) }); // level 2 a
            moveGenFake.AddGetAllMoves(new List<IMove>() { MakeWhiteMove(3) }); // level 3 a
            moveGenFake.AddGetAllMoves(new List<IMove>() { MakeBlackMove(4) }); // level 4 a
            moveGenFake.AddGetAllMoves(new List<IMove>() { MakeWhiteMove(5) }); // level 5 a
            moveGenFake.AddGetAllMoves(new List<IMove>() { MakeBlackMove(2, 2) }); // level 2 b
            moveGenFake.AddGetAllMoves(new List<IMove>() { MakeWhiteMove(3, 2) }); // level 3 b
            moveGenFake.AddGetAllMoves(new List<IMove>() { MakeBlackMove(4, 2) }); // level 4 b
            moveGenFake.AddGetAllMoves(new List<IMove>()); // no move generated for 5b
            moveGenFake.SetIsChecks(new List<bool>() { false });
            IEvaluator evalFake = new FakeEvaluator(new List<float>() { -1010 }); // eval not called for right branch
            IBoard boardFake = new FakeBoard();

            var target = new SearchMinimax(evalFake, moveGenFake);
            target.SetMaxDepth(5);
            var bestRatingActual = target.SearchLevel(boardFake, Definitions.ChessColor.White, 5).First();

            Assert.AreEqual(0, bestRatingActual.Score);
            Assert.AreEqual(bestMove, bestRatingActual.Move);
        }

        // -------------------------------------------------
        // Search chooses between check mate and stall mate
        // -------------------------------------------------

        [TestMethod]
        public void SearchMinimaxTest_WhenWhiteChoosesBetweenCheckMateAndStallMate_ThenWhiteSelectsCheckMate()
        {
            //       start
            //    1a /     \1b
            //   10000x     0        white move -> highest selected (x), 1a is check mate, 1b is stall mate.
            //   2a/        2b\   
            //  10000x       10000x     black move -> lowest selected (x)
            //   3a/         3b\   
            //  10000x       10000x    white move -> highest selected (x)

            FakeMoveGeneratorMulitlevel moveGenFake = new FakeMoveGeneratorMulitlevel();
            var bestMove = MakeWhiteMove(1, 1);
            moveGenFake.AddGetAllMoves(new List<IMove>() { bestMove, MakeWhiteMove(1, 2) }); // level 1
            moveGenFake.AddGetAllMoves(new List<IMove>() { MakeBlackMove(2) }); // level 2 a
            moveGenFake.AddGetAllMoves(new List<IMove>() { MakeWhiteMove(3) }); // level 3 a
            moveGenFake.AddGetAllMoves(new List<IMove>() { MakeBlackMove(2, 2) }); // level 2 b
            moveGenFake.AddGetAllMoves(new List<IMove>() { MakeWhiteMove(3, 2) }); // level 3 b
            moveGenFake.SetIsChecks(new List<bool>() { true, false });
            IEvaluator evalFake = new FakeEvaluator(new List<float>() { 10000, 10000 });
            IBoard boardFake = new FakeBoard();

            var target = new SearchMinimax(evalFake, moveGenFake);
            target.SetMaxDepth(3);
            var bestRatingActual = target.SearchLevel(boardFake, Definitions.ChessColor.White, 3).First();

            Assert.AreEqual(10000, bestRatingActual.Score);
            Assert.AreEqual(bestMove, bestRatingActual.Move);
        }

        [TestMethod]
        public void SearchMinimaxTest_WhenWhiteChoosesBetweenCheckMateAndStallMate_ThenWhiteSelectsCheckMate_MovesSwitched()
        {
            //       start
            //    1a /     \1b
            //      0    10000x        white move -> highest selected (x), 1a is stall mate, 1b is check mate.
            //   2a/        2b\   
            //  10000x       10000x    black move -> lowest selected (x)
            //   3a/         3b\   
            //  10000x       10000x    white move -> highest selected (x)

            FakeMoveGeneratorMulitlevel moveGenFake = new FakeMoveGeneratorMulitlevel();
            var bestMove = MakeWhiteMove(1, 2);
            moveGenFake.AddGetAllMoves(new List<IMove>() { MakeWhiteMove(1, 1), bestMove }); // level 1
            moveGenFake.AddGetAllMoves(new List<IMove>() { MakeBlackMove(2) }); // level 2 a
            moveGenFake.AddGetAllMoves(new List<IMove>() { MakeWhiteMove(3) }); // level 3 a
            moveGenFake.AddGetAllMoves(new List<IMove>() { MakeBlackMove(2, 2) }); // level 2 b
            moveGenFake.AddGetAllMoves(new List<IMove>() { MakeWhiteMove(3, 2) }); // level 3 b
            moveGenFake.SetIsChecks(new List<bool>() { false, true });
            IEvaluator evalFake = new FakeEvaluator(new List<float>() { 10000, 10000 });
            IBoard boardFake = new FakeBoard();

            var target = new SearchMinimax(evalFake, moveGenFake);
            target.SetMaxDepth(3);
            var bestRatingActual = target.SearchLevel(boardFake, Definitions.ChessColor.White, 3).First();

            Assert.AreEqual(10000, bestRatingActual.Score);
            Assert.AreEqual(bestMove, bestRatingActual.Move);
        }

        [TestMethod]
        public void SearchMinimaxTest_WhenBlackChoosesBetweenCheckMateAndStallMate_ThenBlackSelectsCheckMate()
        {
            //       start
            //    1a /     \1b
            //   -10000x     0        black move -> lowest selected (x), 1a is check mate, 1b is stall mate.
            //   2a/        2b\   
            //  -10000x     -10000x   white move -> highest selected (x)
            //   3a/         3b\   
            //  -10000x     -10000x   black move -> lowest selected (x)

            FakeMoveGeneratorMulitlevel moveGenFake = new FakeMoveGeneratorMulitlevel();
            var bestMove = MakeBlackMove(1, 1);
            moveGenFake.AddGetAllMoves(new List<IMove>() { bestMove, MakeBlackMove(1, 2) }); // level 1
            moveGenFake.AddGetAllMoves(new List<IMove>() { MakeWhiteMove(2) }); // level 2 a
            moveGenFake.AddGetAllMoves(new List<IMove>() { MakeBlackMove(3) }); // level 3 a
            moveGenFake.AddGetAllMoves(new List<IMove>() { MakeWhiteMove(2, 2) }); // level 2 b
            moveGenFake.AddGetAllMoves(new List<IMove>() { MakeBlackMove(3, 2) }); // level 3 b
            moveGenFake.SetIsChecks(new List<bool>() { true, false });
            IEvaluator evalFake = new FakeEvaluator(new List<float>() { -10000, -10000 });
            IBoard boardFake = new FakeBoard();

            var target = new SearchMinimax(evalFake, moveGenFake);
            target.SetMaxDepth(3);
            var bestRatingActual = target.SearchLevel(boardFake, Definitions.ChessColor.Black, 3).First();

            Assert.AreEqual(-10000, bestRatingActual.Score);
            Assert.AreEqual(bestMove, bestRatingActual.Move);
        }

        [TestMethod]
        public void SearchMinimaxTest_WhenBlackChoosesBetweenCheckMateAndStallMate_ThenBlackSelectsCheckMate_MovesSwitched()
        {
            //       start
            //    1a /     \1b
            //      0     -10000x     black move -> lowest selected (x), 1a is stall mate, 1b is check mate.
            //   2a/        2b\   
            //  -10000x     -10000x   white move -> highest selected (x)
            //   3a/         3b\   
            //  -10000x     -10000x   black move -> lowest selected (x)

            FakeMoveGeneratorMulitlevel moveGenFake = new FakeMoveGeneratorMulitlevel();
            var bestMove = MakeBlackMove(1, 2);
            moveGenFake.AddGetAllMoves(new List<IMove>() { MakeBlackMove(1, 1), bestMove }); // level 1
            moveGenFake.AddGetAllMoves(new List<IMove>() { MakeWhiteMove(2) }); // level 2 a
            moveGenFake.AddGetAllMoves(new List<IMove>() { MakeBlackMove(3) }); // level 3 a
            moveGenFake.AddGetAllMoves(new List<IMove>() { MakeWhiteMove(2, 2) }); // level 2 b
            moveGenFake.AddGetAllMoves(new List<IMove>() { MakeBlackMove(3, 2) }); // level 3 b
            moveGenFake.SetIsChecks(new List<bool>() { false, true });
            IEvaluator evalFake = new FakeEvaluator(new List<float>() { -10000, -10000 });
            IBoard boardFake = new FakeBoard();

            var target = new SearchMinimax(evalFake, moveGenFake);
            target.SetMaxDepth(3);
            var bestRatingActual = target.SearchLevel(boardFake, Definitions.ChessColor.Black, 3).First();

            Assert.AreEqual(-10000, bestRatingActual.Score);
            Assert.AreEqual(bestMove, bestRatingActual.Move);
        }

        /////////////////////////////////////////////////////////////////////////////////////
        // todo...
        /////////////////////////////////////////////////////////////////////////////////////

        // -------------------------------------------------
        // Search chooses between check mate and stall mate
        // King lost before deepest level.
        // -------------------------------------------------

        // -------------------------------------------------
        // Search chooses between check mate and stall mate
        // King lost before deepest level. Different levels...
        // -------------------------------------------------


        /////////////////////////////////////////////////////////////////////////////////////
        // older tests
        // to be checked thoroughly...
        /////////////////////////////////////////////////////////////////////////////////////

        [TestMethod]
        public void SearchMinimaxTest_WhenWhiteIsCheckMateIn2_AndMaxDepth2_ThenNoLegalMove()
        {
            //      start
            //     /      \
            //  -10000    -10000     white move -> highest selected (x), but here white has lost king-> no move returned
            //   /           \   
            // -10000x       -10000x     black move -> lowest selected (x)
            
            FakeMoveGeneratorMulitlevel moveGenFake = new FakeMoveGeneratorMulitlevel();
            moveGenFake.AddGetAllMoves(new List<IMove>() { MakeWhiteMove(1, 1), MakeWhiteMove(1, 2) }); // level 1
            moveGenFake.AddGetAllMoves(new List<IMove>() { MakeBlackMove(2, 1) }); // level 2 a
            moveGenFake.AddGetAllMoves(new List<IMove>() { MakeBlackMove(2, 2) }); // level 2 b
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
            moveGenFake.AddGetAllMoves(new List<IMove>() { MakeWhiteMove(1, 1), MakeWhiteMove(1, 2) }); // level 1
            moveGenFake.AddGetAllMoves(new List<IMove>() { MakeBlackMove(2, 1) }); // level 2 a
            moveGenFake.AddGetAllMoves(new List<IMove>() { MakeWhiteMove(2, 1) }); // level 3 a
            moveGenFake.AddGetAllMoves(new List<IMove>() { MakeBlackMove(2, 2) }); // level 2 b
            moveGenFake.AddGetAllMoves(new List<IMove>() { MakeWhiteMove(2, 2) }); // level 3 b
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
        public void SearchMinimaxTest_WhenWhiteIsCheckMateIn4_AndMaxDepth4_ThenNormalMove()
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

        [TestMethod] // Does this make sense ???
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

        [TestMethod] // Does this make sense ???
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

        [TestMethod] // does that make sense ???
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

        [TestMethod] // does that make sense ???
        public void SearchMinimaxTest_WhenCheckMateInDifferentDepths_ThenSelectTheOneIn_LESS_Depth()
        {
            //       start        // white is check mate -> nolegal move. it must select the left move!!
            //       -10000
            //      /      \
            //  -10000x   -10000x       white move -> highest selected (x)  white is/goes in check -> no legal
            //    /           \   
            // -10000x      -10000x       black move -> lowest selected (x)  black captures white king -> no legal
            // no / moves      \    
            //                10000x        white move -> highest selected (x)  no white king -> no moves returned from move generator
            //                  \
            //                 -10000x        black move -> lowest selected (x)
            FakeMoveGeneratorMulitlevel moveGenFake = new FakeMoveGeneratorMulitlevel();
            moveGenFake.AddGetAllMoves(new List<IMove>() { new NormalMove(Piece.MakePiece('P'), 0, 0, 0, 0, null), new NormalMove(Piece.MakePiece('Q'), 0, 0, 0, 0, null) }); // level 1
            moveGenFake.AddGetAllMoves(new List<IMove>() { new NormalMove(Piece.MakePiece('b'), 0, 0, 0, 0, null) }); // level 2 a
            moveGenFake.AddGetAllMoves(new List<IMove>() { }); // level 3 a  no white king -> no moves
            moveGenFake.AddGetAllMoves(new List<IMove>() { new NormalMove(Piece.MakePiece('n'), 0, 0, 0, 0, null) }); // level 2 b
            moveGenFake.AddGetAllMoves(new List<IMove>() { new NormalMove(Piece.MakePiece('K'), 0, 0, 0, 0, null) }); // level 3 b  
            moveGenFake.AddGetAllMoves(new List<IMove>() { new NormalMove(Piece.MakePiece('q'), 0, 0, 0, 0, null) }); // level 4 b

            moveGenFake.SetIsChecks(new List<bool>() { true, true });
            IEvaluator evalFake = new FakeEvaluator(new List<float>() { -10000 }); // eval called for 4b 
            IBoard boardFake = new FakeBoard();

            var target = new SearchMinimax(evalFake, moveGenFake);
            target.SetMaxDepth(4);
            var bestRatingsActual = target.SearchLevel(boardFake, Definitions.ChessColor.White, 4);

            Assert.AreEqual(1, bestRatingsActual.Count(), "Check mate, can only have nolegal move");

            Assert.AreEqual(-10000, bestRatingsActual.First().Score, "check mate should be score -10000");
            Assert.IsTrue(bestRatingsActual.First().Move is NoLegalMove);
        }

        [TestMethod] // Does this make sense ???
        public void SearchMinimaxTest_WhenCheckMateInDifferentDepths_ThenSelectTheOneIn_LESS_Depth2()
        {
            //       start        // white is check mate -> nolegal move. it must select the left move!!
            //      -10000
            //      /      \
            //  -10000x   -10000x       white move -> highest selected (x)  white is/goes in check -> no legal
            //    /           \   
            // -10000x      -10000x       black move -> lowest selected (x)  black captures white king -> no legal
            //   /           no \ moves 
            // -10000x                       white move -> highest selected (x)  no white king -> no moves returned from move generator
            //  /
            // -10000x                         black move -> lowest selected (x)
            FakeMoveGeneratorMulitlevel moveGenFake = new FakeMoveGeneratorMulitlevel();
            moveGenFake.AddGetAllMoves(new List<IMove>() { new NormalMove(Piece.MakePiece('P'), 0, 0, 0, 0, null), new NormalMove(Piece.MakePiece('Q'), 0, 0, 0, 0, null) }); // level 1
            moveGenFake.AddGetAllMoves(new List<IMove>() { new NormalMove(Piece.MakePiece('n'), 0, 0, 0, 0, null) }); // level 2 a
            moveGenFake.AddGetAllMoves(new List<IMove>() { new NormalMove(Piece.MakePiece('K'), 0, 0, 0, 0, null) }); // level 3 a  
            moveGenFake.AddGetAllMoves(new List<IMove>() { new NormalMove(Piece.MakePiece('q'), 0, 0, 0, 0, null) }); // level 4 a
            moveGenFake.AddGetAllMoves(new List<IMove>() { new NormalMove(Piece.MakePiece('b'), 0, 0, 0, 0, null) }); // level 2 b
            moveGenFake.AddGetAllMoves(new List<IMove>() { }); // level 3 b  no white king -> no moves

            moveGenFake.SetIsChecks(new List<bool>() { true, true });
            IEvaluator evalFake = new FakeEvaluator(new List<float>() { -10000 }); // eval called for 4b 
            IBoard boardFake = new FakeBoard();

            var target = new SearchMinimax(evalFake, moveGenFake);
            target.SetMaxDepth(4);
            var bestRatingsActual = target.SearchLevel(boardFake, Definitions.ChessColor.White, 4);

            Assert.AreEqual(1, bestRatingsActual.Count(), "Check mate, can only have nolegal move");

            Assert.AreEqual(-10000, bestRatingsActual.First().Score, "check mate should be score -10000");
            Assert.IsTrue(bestRatingsActual.First().Move is NoLegalMove, "must be NoLegalMove");
        }

        [TestMethod] // It does make sense! The engine must notice that it is check mate now and must say "NoLegalMove".
        public void SearchMinimaxTest_WhenCheckMateInDifferentDepths_ThenSelectTheOneIn_LESS_Depth_WinnerSelects()
        {
            //       start        // white is check mate -> nolegal move. it must select the left move!!
            //       -10000
            //            \
            //          -10000x       white move -> highest selected (x)  white is/goes in check -> no legal
            //           /    \   
            //      -10000x  -10000       black move -> lowest selected (x)  black captures white king -> no legal
            //   no / moves     \    
            //               -10000x        white move -> highest selected (x)  no white king -> no moves returned from move generator
            //                  \
            //               -10000x        black move -> lowest selected (x)
            FakeMoveGeneratorMulitlevel moveGenFake = new FakeMoveGeneratorMulitlevel();
            moveGenFake.AddGetAllMoves(new List<IMove>() { new NormalMove(Piece.MakePiece('P'), 0, 0, 0, 0, null) }); // level 1
            moveGenFake.AddGetAllMoves(new List<IMove>() { new NormalMove(Piece.MakePiece('p'), 0, 0, 0, 0, null), new NormalMove(Piece.MakePiece('q'), 0, 0, 0, 0, null) }); // level 2
            moveGenFake.AddGetAllMoves(new List<IMove>() { }); // level 3 a  no white king -> no moves
            moveGenFake.AddGetAllMoves(new List<IMove>() { new NormalMove(Piece.MakePiece('N'), 0, 0, 0, 0, null) }); // level 3 b
            moveGenFake.AddGetAllMoves(new List<IMove>() { new NormalMove(Piece.MakePiece('k'), 0, 0, 0, 0, null) }); // level 4 b  

            moveGenFake.SetIsChecks(new List<bool>() { true, true });
            IEvaluator evalFake = new FakeEvaluator(new List<float>() { -10000 }); // eval called for 4b 
            IBoard boardFake = new FakeBoard();

            var target = new SearchMinimax(evalFake, moveGenFake);
            target.SetMaxDepth(4);
            var bestRatingsActual = target.SearchLevel(boardFake, Definitions.ChessColor.White, 4);

            Assert.AreEqual(1, bestRatingsActual.Count(), "Check mate, can only have nolegal move");

            Assert.AreEqual(-10000, bestRatingsActual.First().Score, "check mate should be score -10000");
            Assert.IsTrue(bestRatingsActual.First().Move is NoLegalMove, "should be NoLegalMove");
        }

        [TestMethod] // It does make sense! The engine must notice that it is check mate now and must say "NoLegalMove".
        // todo check if the test is correct. The idea is fine.
        public void SearchMinimaxTest_WhenCheckMateInDifferentDepths_ThenSelectTheOneIn_LESS_Depth_WinnerSelects2()
        {
            //       start        // white is check mate -> nolegal move. it must select the left move!!
            //       -10000
            //            \
            //          -10000x       white move -> highest selected (x)  white is/goes in check -> no legal
            //           /    \   
            //      -10000   -10000x       black move -> lowest selected (x)  black captures white king -> no legal
            //       /         no \ moves    
            //    10000x                      white move -> highest selected (x)  no white king -> no moves returned from move generator
            //      /
            //   -10000x                        black move -> lowest selected (x)
            FakeMoveGeneratorMulitlevel moveGenFake = new FakeMoveGeneratorMulitlevel();
            moveGenFake.AddGetAllMoves(new List<IMove>() { new NormalMove(Piece.MakePiece('P'), 1, 1, 1, 1, null) }); // level 1
            moveGenFake.AddGetAllMoves(new List<IMove>() { new NormalMove(Piece.MakePiece('k'), 2, 1, 1, 1, null), new NormalMove(Piece.MakePiece('q'), 2, 2, 1, 1, null) }); // level 2
            moveGenFake.AddGetAllMoves(new List<IMove>() { new NormalMove(Piece.MakePiece('B'), 3, 1, 1, 1, null) }); // level 3 a
            moveGenFake.AddGetAllMoves(new List<IMove>() { new NormalMove(Piece.MakePiece('n'), 4, 1, 1, 1, null) }); // level 4 a
            moveGenFake.AddGetAllMoves(new List<IMove>() { }); // level 3 b  no white king -> no moves
            

            moveGenFake.SetIsChecks(new List<bool>() { true, true });
            IEvaluator evalFake = new FakeEvaluator(new List<float>() { -10000 }); // eval called for 4a 
            IBoard boardFake = new FakeBoard();

            var target = new SearchMinimax(evalFake, moveGenFake);
            target.SetMaxDepth(4);
            var bestRatingsActual = target.SearchLevel(boardFake, Definitions.ChessColor.White, 4);

            Assert.AreEqual(1, bestRatingsActual.Count(), "Check mate, can only have nolegal move");

            Assert.AreEqual(-10000, bestRatingsActual.First().Score, "check mate should be score -10000");
            Assert.IsTrue(bestRatingsActual.First().Move is NoLegalMove, "should be NoLegalMove");
        }

        // Helpers

        private IMove MakeWhiteMove(int file)
        {
            return new NormalMove(Piece.MakePiece('Q'), file, 0, 0, 0, null);
        }

        private IMove MakeWhiteMove(int file, int rank)
        {
            return new NormalMove(Piece.MakePiece('Q'), file, rank, 0, 0, null);
        }

        private IMove MakeBlackMove(int file)
        {
            return new NormalMove(Piece.MakePiece('q'), file, 0, 0, 0, null);
        }

        private IMove MakeBlackMove(int file, int rank)
        {
            return new NormalMove(Piece.MakePiece('q'), file, rank, 0, 0, null);
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
