using System;
using MantaChessEngine;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MantaChessEngineTest
{
    [TestClass]
    public class MantaEngineTest
    {
        [TestMethod]
        public void DoBestMove_DepthOneSearch_WhenQueenCanBeCaptured_ThenQueenIsCaptured_WhiteMoves()
        {
            var engine = new MantaEngine(EngineType.Minimax);
            engine.SetMaxSearchDepth(1);
            engine.SetBoard(new Board());
            string boardString = "rnb.kbnr" +
                                 "ppp.pppp" +
                                 "........" +
                                 "....q..." +
                                 ".....P.." +
                                 "........" +
                                 "PPPPP.PP" +
                                 "RNBQKBNR";
            engine.SetPosition(boardString);

            engine.DoBestMove(Definitions.ChessColor.White);

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
            var engine = new MantaEngine(EngineType.Minimax);
            engine.SetMaxSearchDepth(1);
            engine.SetBoard(new Board());
            string boardString = "rnbqkbnr" +
                                 "pppp.ppp" +
                                 "........" +
                                 "....p..." +
                                 "...Q...." +
                                 "........" +
                                 "PPP.PPPP" +
                                 "RNB.KBNR";
            engine.SetPosition(boardString);

            engine.DoBestMove(Definitions.ChessColor.Black);

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
            var engine = new MantaEngine(EngineType.MinimaxPosition);
            engine.SetMaxSearchDepth(depth);
            engine.SetBoard(new Board());
            string boardString = "........" +
                                 "........" +
                                 "........" +
                                 "........" +
                                 "........" +
                                 "...k...." +
                                 "...q...." +
                                 "...K....";
            engine.SetPosition(boardString);

            IMove actualMove = engine.DoBestMove(Definitions.ChessColor.White);

            string actualBoard = engine.GetString();
            string expectedBoard = boardString;
            Assert.AreEqual(new NoLegalMove(), actualMove, "White is check mate, so no legal move possible");
            Assert.AreEqual(expectedBoard, actualBoard, "White is check mate");
            Assert.IsTrue(engine.IsCheck(Definitions.ChessColor.White), "white is check mate. must be check");
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
            var engine = new MantaEngine(EngineType.MinimaxPosition);
            engine.SetMaxSearchDepth(depth);
            engine.SetBoard(new Board());
            string boardString = "........" +
                                 "........" +
                                 "........" +
                                 "........" +
                                 "........" +
                                 "..k....." +
                                 ".r......" +
                                 "K.......";
            engine.SetPosition(boardString);

            IMove actualMove = engine.DoBestMove(Definitions.ChessColor.White);
            
            string actualBoard = engine.GetString();
            string expectedBoard = boardString;
            Assert.AreEqual(new NoLegalMove(), actualMove, "White is stall mate, so no legal move possible");
            Assert.AreEqual(expectedBoard, actualBoard, "White is stall mate");
            Assert.IsFalse(engine.IsCheck(Definitions.ChessColor.White), "white is stall mate. not check");
        }

        [TestMethod]
        public void DoBestMoveTest_WhenCheck_ThenKingMustEscapeCheck()
        {
            var engine = new MantaEngine(EngineType.MinimaxPosition);
            engine.SetBoard(new Board());
            string boardString = "........" +
                                 "........" +
                                 "........" +
                                 "........" +
                                 "........" +
                                 ".......k" +
                                 ".......r" +
                                 ".......K"; // king is in check and must escape. only move is Kh1g1
            engine.SetPosition(boardString);

            IMove actualMove = engine.DoBestMove(Definitions.ChessColor.White);

            Assert.AreEqual(new NormalMove(Piece.MakePiece('K'), 'h', 1, 'g', 1, null), actualMove, "should be h1g1");
        }
    }
}
