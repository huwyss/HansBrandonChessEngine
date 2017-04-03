using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MantaChessEngine;

namespace MantaChessEngineTest
{
    [TestClass]
    public class CastlingMoveTest
    {
        [TestMethod]
        public void CastlingMoveTest_WhenCastlingWhiteKingSide_ThenToStringIs00()
        {
            MoveBase move = new CastlingMove(CastlingType.WhiteKingSide);
            string moveString = move.ToString();
            Assert.AreEqual("0-0", moveString, "white king castling should be 0-0");
        }

        [TestMethod]
        public void CastlingMoveTest_WhenCastlingBlackQueenSide_ThenToStringIs000()
        {
            MoveBase move = new CastlingMove(CastlingType.BlackQueenSide);
            string moveString = move.ToString();
            Assert.AreEqual("0-0-0", moveString, "white king castling should be 0-0-0");
        }
    }
}
