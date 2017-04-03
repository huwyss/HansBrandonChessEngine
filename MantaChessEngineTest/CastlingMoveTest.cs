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
            MoveBase move = new CastlingMove('K', Helper.FileCharToFile('e'), 1, Helper.FileCharToFile('g'), 1, CastlingType.WhiteKingSide);
            string moveString = move.ToString();
            Assert.AreEqual("0-0", moveString, "white king castling should be 0-0");
        }

        [TestMethod]
        public void CastlingMoveTest_WhenCastlingBlackQueenSide_ThenToStringIs000()
        {
            MoveBase move = new CastlingMove('k', Helper.FileCharToFile('e'), 8, Helper.FileCharToFile('g'), 8, CastlingType.BlackQueenSide);
            string moveString = move.ToString();
            Assert.AreEqual("0-0-0", moveString, "white king castling should be 0-0-0");
        }
    }
}
