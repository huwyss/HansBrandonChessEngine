using System;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HansBrandonChessEngine;
using HBCommon;

namespace HansBrandonChessEngineTest
{
    [TestClass]
    public class MoveTest
    {
        [TestMethod]
        public void ToString_WhenMove_ThenPrintCorrect()
        {
            Assert.AreEqual("e2e4.", new NormalMove(Piece.MakePiece(PieceType.Pawn, ChessColor.Black), Square.E2, Square.E4, null).ToString());
            Assert.AreEqual("e4d5p", new NormalMove(Piece.MakePiece(PieceType.Pawn, ChessColor.Black), Square.E4, Square.D5, Piece.MakePiece(PieceType.Pawn, ChessColor.Black)).ToString());
        }

        [TestMethod]
        public void EqualsTest_WhenComparingTwoEqualMoves_ThenEqualsReturnsTrue()
        {
            Assert.AreEqual(new NormalMove(Piece.MakePiece(PieceType.Pawn, ChessColor.Black), Square.E2, Square.E4, Piece.MakePiece(PieceType.Pawn, ChessColor.Black)),
                            new NormalMove(Piece.MakePiece(PieceType.Pawn, ChessColor.Black), Square.E2, Square.E4, Piece.MakePiece(PieceType.Pawn, ChessColor.Black)));
        }

        [TestMethod]
        public void EqualsTest_WhenComparingTwoDifferentMoves_ThenReturnsFalse()
        {
            Assert.AreNotEqual(
                new NormalMove(Piece.MakePiece(PieceType.Pawn, ChessColor.Black), Square.E2, Square.E4, Piece.MakePiece(PieceType.Pawn, ChessColor.Black)),
                new NormalMove(Piece.MakePiece(PieceType.Pawn, ChessColor.Black), Square.A2, Square.E4, Piece.MakePiece(PieceType.Pawn, ChessColor.Black)));
            Assert.AreNotEqual(
                new NormalMove(Piece.MakePiece(PieceType.Pawn, ChessColor.Black), Square.E2, Square.E4, Piece.MakePiece(PieceType.Pawn, ChessColor.Black)),
                new NormalMove(Piece.MakePiece(PieceType.Pawn, ChessColor.Black), Square.E1, Square.E4, Piece.MakePiece(PieceType.Pawn, ChessColor.Black)));
            Assert.AreNotEqual(
                new NormalMove(Piece.MakePiece(PieceType.Pawn, ChessColor.Black), Square.E2, Square.E4, Piece.MakePiece(PieceType.Pawn, ChessColor.Black)),
                new NormalMove(Piece.MakePiece(PieceType.Pawn, ChessColor.Black), Square.E2, Square.A4, Piece.MakePiece(PieceType.Pawn, ChessColor.Black)));
            Assert.AreNotEqual(
                new NormalMove(Piece.MakePiece(PieceType.Pawn, ChessColor.Black), Square.E2, Square.E4, Piece.MakePiece(PieceType.Pawn, ChessColor.Black)),
                new NormalMove(Piece.MakePiece(PieceType.Pawn, ChessColor.Black), Square.E2, Square.E1, Piece.MakePiece(PieceType.Pawn, ChessColor.Black)));
            Assert.AreNotEqual(
                new NormalMove(Piece.MakePiece(PieceType.Pawn, ChessColor.Black), Square.E2, Square.E4, Piece.MakePiece(PieceType.Pawn, ChessColor.Black)),
                new NormalMove(Piece.MakePiece(PieceType.Pawn, ChessColor.Black), Square.E2, Square.E4, Piece.MakePiece(PieceType.Rook, ChessColor.Black)));
            Assert.AreNotEqual(
                new NormalMove(Piece.MakePiece(PieceType.Pawn, ChessColor.Black), Square.E2, Square.E4, Piece.MakePiece(PieceType.Pawn, ChessColor.Black)),
                new NormalMove(Piece.MakePiece(PieceType.Queen, ChessColor.Black), Square.E2, Square.E4, Piece.MakePiece(PieceType.Pawn, ChessColor.Black)));
        }

        [TestMethod]
        public void ConstructorTest_WhenStringParameter_ThenCorrectObject()
        {
            IMove move = new NormalMove(Piece.MakePiece(PieceType.Queen, ChessColor.Black), Square.E2, Square.D3, Piece.MakePiece(PieceType.Pawn, ChessColor.Black));
            Assert.AreEqual(Piece.MakePiece(PieceType.Queen, ChessColor.Black), move.MovingPiece);
            Assert.AreEqual(Square.E2, move.FromSquare);
            Assert.AreEqual(Square.D3, move.ToSquare);
            Assert.AreEqual(Piece.MakePiece(PieceType.Pawn, ChessColor.Black), move.CapturedPiece);
        }

        [TestMethod]
        public void EqualsTest_WhenNoLegalMove_ThenCorrectObject()
        {
            IMove noLegalMove = new NoLegalMove();
            IMove noLegalMove2 = new NoLegalMove();
            Assert.AreEqual(noLegalMove, noLegalMove2);
        }

        [TestMethod]
        public void EqualsTest_WhenNormalMoveAndNoLegalMove_ThenNotEqual()
        {
            IMove noLegalMove = new NoLegalMove();
            IMove normalMove = new NormalMove(Piece.MakePiece(PieceType.Pawn, ChessColor.Black), Square.A1, Square.A1, null);
            Assert.AreNotEqual(noLegalMove, normalMove);
            
            // the other way round
            Assert.AreNotEqual(normalMove, noLegalMove);
        }

        [TestMethod]
        public void ToStringTest_WhenNoLegalMove_ThenNoLegalMove()
        {
            IMove noLegal = new NoLegalMove();
            Assert.AreEqual("NoLegalMove", noLegal.ToString());
        }

        [TestMethod]
        public void IsCorrectMoveTest_WhenCorrect_ThenTrue()
        {
            String correctMoveString = "a2a3";
            Assert.AreEqual(true, CommonHelper.IsCorrectMove(correctMoveString));
        }

        [TestMethod]
        public void IsCorrectMoveTest_WhenBadInput_ThenFalse()
        {
            String wrongMoveString = "abcd";
            Assert.AreEqual(false, CommonHelper.IsCorrectMove(wrongMoveString));

            wrongMoveString = "abcde";
            Assert.AreEqual(false, CommonHelper.IsCorrectMove(wrongMoveString));

            wrongMoveString = "abcdef";
            Assert.AreEqual(false, CommonHelper.IsCorrectMove(wrongMoveString));
        }

        [TestMethod]
        public void MoveTest_WhenEnPassant_ThenCheckEnPassantCorrect()
        {
            IMove move = new EnPassantCaptureMove(Piece.MakePiece(PieceType.Pawn, ChessColor.Black), Square.A2, Square.B3, Piece.MakePiece(PieceType.Pawn, ChessColor.Black));
            IMove move2 = new NormalMove(Piece.MakePiece(PieceType.Pawn, ChessColor.Black), Square.A2, Square.B3, Piece.MakePiece(PieceType.Pawn, ChessColor.Black));
            // Assert.AreEqual("a2b3pe", move.ToString());
            Assert.AreNotEqual(move2, move);
            //Assert.AreEqual(new Move("a2b3pe"), move);
        }
    }
}