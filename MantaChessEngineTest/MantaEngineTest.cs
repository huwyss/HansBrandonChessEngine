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
            var engine = new MantaEngine(EngineType.DepthHalf);
            engine.SetBoard(new Board(new MoveGenerator(new MoveFactory())));
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
            var engine = new MantaEngine(EngineType.DepthHalf);
            engine.SetBoard(new Board(new MoveGenerator(new MoveFactory())));
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
        public void DoBestMoveTest_WhenWhiteIsCheckMate_ThenNoLegalMove_WhiteMoves()
        {
            var engine = new MantaEngine(EngineType.MinmaxPosition);
            engine.SetBoard(new Board(new MoveGenerator(new MoveFactory())));
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
            Assert.AreEqual(expectedBoard, actualBoard, "White is check mate");
            Assert.AreEqual(new NoLegalMove(), actualMove, "White is check mate, so no legal move possible");
        }



    }
}
