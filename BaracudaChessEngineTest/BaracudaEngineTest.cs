using System;
using BaracudaChessEngine;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BaracudaChessEngineTest
{
    [TestClass]
    public class BaracudaEngineTest
    {
        [TestMethod]
        public void DoBestMove_DepthOneSearch_WhenQueenCanBeCaptured_ThenQueenIsCaptured_WhiteMoves()
        {
            var engine = new BaracudaEngine(EngineType.DepthOne);
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
            var engine = new BaracudaEngine(EngineType.DepthOne);
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

    }
}
