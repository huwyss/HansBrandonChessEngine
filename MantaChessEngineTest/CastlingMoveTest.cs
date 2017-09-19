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
            MoveBase move = new CastlingMove(CastlingType.WhiteKingSide, new King(Definitions.ChessColor.White));
            string moveString = move.ToString();
            Assert.AreEqual("0-0", moveString, "white king castling should be 0-0");
        }

        [TestMethod]
        public void CastlingMoveTest_WhenCastlingBlackQueenSide_ThenToStringIs000()
        {
            MoveBase move = new CastlingMove(CastlingType.BlackQueenSide, new King(Definitions.ChessColor.Black));
            string moveString = move.ToString();
            Assert.AreEqual("0-0-0", moveString, "white king castling should be 0-0-0");
        }

        [TestMethod]
        public void CastlingMoveTest_WhenDifferentCastlingMoves_ThenEqualsReturnsFalse()
        {
            MoveBase moveWhiteKingCastl = new CastlingMove(CastlingType.WhiteKingSide, new King(Definitions.ChessColor.White));
            MoveBase moveWhiteQueenCastl = new CastlingMove(CastlingType.WhiteQueenSide, new King(Definitions.ChessColor.White));
            Assert.AreNotEqual(moveWhiteKingCastl, moveWhiteQueenCastl, "different castling should be unequal");
        }

        [TestMethod]
        public void CastlingMoveTest_WhenTwoCastlingMoves_ThenEqualsReturnsTrue()
        {
            MoveBase moveBlackKingCastl = new CastlingMove(CastlingType.BlackKingSide, new King(Definitions.ChessColor.Black));
            MoveBase moveBlackKingCastl1 = new CastlingMove(CastlingType.BlackKingSide, new King(Definitions.ChessColor.Black));
            Assert.AreEqual(moveBlackKingCastl, moveBlackKingCastl1, "same castling move should be equal");
        }

        [TestMethod]
        public void CastlingMoveTest_WhenCastlingAndNormalKingMove_ThenEqualsReturnsFalse()
        {
            MoveBase moveWhiteKingCastl = new CastlingMove(CastlingType.WhiteKingSide, new King(Definitions.ChessColor.White));
            MoveBase moveWhiteQueenCastl = new NormalMove(Piece.MakePiece('K'), 'e', 1, 'g', 1, null);
            Assert.IsFalse(moveWhiteQueenCastl.Equals(moveWhiteKingCastl), "castling and normal move should be unequal");
            Assert.IsFalse(moveWhiteKingCastl.Equals(moveWhiteQueenCastl), "normal and castling move should be unequal");
        }

    }
}
