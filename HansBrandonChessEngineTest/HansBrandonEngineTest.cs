using System;
using HansBrandonChessEngine;
using HBCommon;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace HansBrandonChessEngineTest
{
    [TestClass]
    public class HansBrandonEngineTest
    {
        [TestMethod]
        public void DoBestMove_DepthOneSearch_WhenQueenCanBeCaptured_ThenQueenIsCaptured_WhiteMoves()
        {
            var engine = new HansBrandonEngine(EngineType.Minimax, 256);
            engine.SetMaxSearchDepth(1);
            string boardString = "rnb.kbnr" +
                                 "ppp.pppp" +
                                 "........" +
                                 "....q..." +
                                 ".....P.." +
                                 "........" +
                                 "PPPPP.PP" +
                                 "RNBQKBNR";
            engine.SetPosition(boardString);

            engine.CalculateBestMove(ChessColor.White);

            string actualBoard = engine.GetString();
            string expectedBoard = "rnb.kbnr" +
                                   "ppp.pppp" +
                                   "........" +
                                   "....P..." +
                                   "........" +
                                   "........" +
                                   "PPPPP.PP" +
                                   "RNBQKBNR";
            Assert.AreEqual(expectedBoard, actualBoard, "Black queen should be captured at e5");
        }

        [TestMethod]
        public void DoBestMove_DepthOneSearch_WhenQueenCanBeCaptured_ThenQueenIsCaptured_BlackMoves()
        {
            var engine = new HansBrandonEngine(EngineType.Minimax, 256);
            engine.SetMaxSearchDepth(1);
            string boardString = "rnbqkbnr" +
                                 "pppp.ppp" +
                                 "........" +
                                 "....p..." +
                                 "...Q...." +
                                 "........" +
                                 "PPP.PPPP" +
                                 "RNB.KBNR";
            engine.SetPosition(boardString);

            engine.CalculateBestMove(ChessColor.Black);

            string actualBoard = engine.GetString();
            string expectedBoard = "rnbqkbnr" +
                                   "pppp.ppp" +
                                   "........" +
                                   "........" +
                                   "...p...." +
                                   "........" +
                                   "PPP.PPPP" +
                                   "RNB.KBNR";
            Assert.AreEqual(expectedBoard, actualBoard, "Black queen should be captured at d4");
        }

        [TestMethod]
        public void DoBestMoveTest_WhenWhiteIsCheckMate_ThenNoLegalMove_WhiteMoves_Depth2()
        {
            DoBestMoveTest_WhenWhiteIsCheckMate_ThenNoLegalMove_WhiteMoves(2);
        }

        [TestMethod]
        public void DoBestMoveTest_WhenWhiteIsCheckMate_ThenNoLegalMove_WhiteMoves_Depth3()
        {
            DoBestMoveTest_WhenWhiteIsCheckMate_ThenNoLegalMove_WhiteMoves(3);
        }

        [TestMethod]
        public void DoBestMoveTest_WhenWhiteIsCheckMate_ThenNoLegalMove_WhiteMoves_Depth4()
        {
            DoBestMoveTest_WhenWhiteIsCheckMate_ThenNoLegalMove_WhiteMoves(4);
        }

        public void DoBestMoveTest_WhenWhiteIsCheckMate_ThenNoLegalMove_WhiteMoves(int depth)
        {
            var engine = new HansBrandonEngine(EngineType.MinimaxPosition, 256);
            engine.SetMaxSearchDepth(depth);
            string boardString = "........" +
                                 "........" +
                                 "........" +
                                 "........" +
                                 "........" +
                                 "...k...." +
                                 "...q...." +
                                 "...K....";
            engine.SetPosition(boardString);

            var actualMove = engine.CalculateBestMove(ChessColor.White);

            string actualBoard = engine.GetString();
            string expectedBoard = boardString;
            Assert.AreEqual("NoLegalMove", actualMove.Move, "White is check mate, so no legal move possible");
            Assert.AreEqual(expectedBoard, actualBoard, "White is check mate");
            Assert.IsTrue(engine.IsCheck(ChessColor.White), "white is check mate. must be check");
        }

        [TestMethod]
        public void DoBestMoveTest_WhenWhiteIsStallMate_ThenNoLegalMove_WhiteMoves_Depth2()
        {
            DoBestMoveTest_WhenWhiteIsStallMate_ThenNoLegalMove_WhiteMoves(2);
        }

        [TestMethod]
        public void DoBestMoveTest_WhenWhiteIsStallMate_ThenNoLegalMove_WhiteMoves_Depth3()
        {
            DoBestMoveTest_WhenWhiteIsStallMate_ThenNoLegalMove_WhiteMoves(3);
        }

        [TestMethod]
        public void DoBestMoveTest_WhenWhiteIsStallMate_ThenNoLegalMove_WhiteMoves_Depth4()
        {
            DoBestMoveTest_WhenWhiteIsStallMate_ThenNoLegalMove_WhiteMoves(4);
        }

        public void DoBestMoveTest_WhenWhiteIsStallMate_ThenNoLegalMove_WhiteMoves(int depth)
        {
            var engine = new HansBrandonEngine(EngineType.MinimaxPosition, 256);
            engine.SetMaxSearchDepth(depth);
            engine.SetBoard(new Board(new Mock<IHashtable>().Object));
            string boardString = "........" +
                                 "........" +
                                 "........" +
                                 "........" +
                                 "........" +
                                 "..k....." +
                                 ".r......" +
                                 "K.......";
            engine.SetPosition(boardString);

            var actualMove = engine.CalculateBestMove(ChessColor.White);
            
            string actualBoard = engine.GetString();
            string expectedBoard = boardString;
            Assert.AreEqual("NoLegalMove", actualMove.Move, "White is stall mate, so no legal move possible");
            Assert.AreEqual(expectedBoard, actualBoard, "White is stall mate");
            Assert.IsFalse(engine.IsCheck(ChessColor.White), "white is stall mate. not check");
        }

        [TestMethod]
        public void DoBestMoveTest_WhenCheck_ThenKingMustEscapeCheck()
        {
            var engine = new HansBrandonEngine(EngineType.MinimaxPosition, 256);
            string boardString = "........" +
                                 "........" +
                                 "........" +
                                 "........" +
                                 "........" +
                                 ".......k" +
                                 ".......r" +
                                 ".......K"; // king is in check and must escape. only move is Kh1g1
            engine.SetPosition(boardString);

            var actualMove = engine.CalculateBestMove(ChessColor.White);

            Assert.AreEqual("h1g1", actualMove.Move, "should be h1g1");
        }
    }
}
