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

        [TestMethod]
        public void CastlingMoveTest_WhenDifferentCastlingMoves_ThenEqualsReturnsFalse()
        {
            MoveBase moveWhiteKingCastl = new CastlingMove(CastlingType.WhiteKingSide);
            MoveBase moveWhiteQueenCastl = new CastlingMove(CastlingType.WhiteQueenSide);
            Assert.AreNotEqual(moveWhiteKingCastl, moveWhiteQueenCastl, "different castling should be unequal");
        }

        [TestMethod]
        public void CastlingMoveTest_WhenTwoCastlingMoves_ThenEqualsReturnsTrue()
        {
            MoveBase moveWhiteKingCastl = new CastlingMove(CastlingType.BlackKingSide);
            MoveBase moveWhiteQueenCastl = new CastlingMove(CastlingType.BlackKingSide);
            Assert.AreEqual(moveWhiteKingCastl, moveWhiteQueenCastl, "same castling move should be equal");
        }

        [TestMethod]
        public void CastlingMoveTest_WhenCastlingAndNormalKingMove_ThenEqualsReturnsFalse()
        {
            MoveBase moveWhiteKingCastl = new CastlingMove(CastlingType.WhiteKingSide);
            MoveBase moveWhiteQueenCastl = new NormalMove('K', 'e', 1, 'g', 1, '.');
            Assert.IsFalse(moveWhiteQueenCastl.Equals(moveWhiteKingCastl), "castling and normal move should be unequal");
            Assert.IsFalse(moveWhiteKingCastl.Equals(moveWhiteQueenCastl), "normal and castling move should be unequal");
        }

    }
}
