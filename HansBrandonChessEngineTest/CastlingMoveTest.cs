﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HansBrandonChessEngine;
using HBCommon;

namespace HansBrandonChessEngineTest
{
    [TestClass]
    public class CastlingMoveTest
    {
        [TestMethod]
        public void CastlingMoveTest_WhenCastlingWhiteKingSide_ThenToStringIs00()
        {
            IMove move = new CastlingMove(HansBrandonChessEngine.CastlingType.WhiteKingSide, new King(ChessColor.White));
            string moveString = move.ToString();
            Assert.AreEqual("0-0", moveString, "white king castling should be 0-0");
        }

        [TestMethod]
        public void CastlingMoveTest_WhenCastlingBlackQueenSide_ThenToStringIs000()
        {
            IMove move = new CastlingMove(HansBrandonChessEngine.CastlingType.BlackQueenSide, new King(ChessColor.Black));
            string moveString = move.ToString();
            Assert.AreEqual("0-0-0", moveString, "white king castling should be 0-0-0");
        }

        [TestMethod]
        public void CastlingMoveTest_WhenDifferentCastlingMoves_ThenEqualsReturnsFalse()
        {
            IMove moveWhiteKingCastl = new CastlingMove(HansBrandonChessEngine.CastlingType.WhiteKingSide, new King(ChessColor.White));
            IMove moveWhiteQueenCastl = new CastlingMove(HansBrandonChessEngine.CastlingType.WhiteQueenSide, new King(ChessColor.White));
            Assert.AreNotEqual(moveWhiteKingCastl, moveWhiteQueenCastl, "different castling should be unequal");
        }

        [TestMethod]
        public void CastlingMoveTest_WhenTwoCastlingMoves_ThenEqualsReturnsTrue()
        {
            IMove moveBlackKingCastl = new CastlingMove(HansBrandonChessEngine.CastlingType.BlackKingSide, new King(ChessColor.Black));
            IMove moveBlackKingCastl1 = new CastlingMove(HansBrandonChessEngine.CastlingType.BlackKingSide, new King(ChessColor.Black));
            Assert.AreEqual(moveBlackKingCastl, moveBlackKingCastl1, "same castling move should be equal");
        }

        [TestMethod]
        public void CastlingMoveTest_WhenCastlingAndNormalKingMove_ThenEqualsReturnsFalse()
        {
            IMove moveWhiteKingCastl = new CastlingMove(HansBrandonChessEngine.CastlingType.WhiteKingSide, new King(ChessColor.White));
            IMove moveWhiteQueenCastl = new NormalMove(Piece.MakePiece(PieceType.King, ChessColor.White ), Square.E1, Square.G1, null);
            Assert.IsFalse(moveWhiteQueenCastl.Equals(moveWhiteKingCastl), "castling and normal move should be unequal");
            Assert.IsFalse(moveWhiteKingCastl.Equals(moveWhiteQueenCastl), "normal and castling move should be unequal");
        }

    }
}
