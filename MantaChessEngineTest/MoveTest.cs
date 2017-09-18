using System;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MantaChessEngine;

namespace MantaChessEngineTest
{
    [TestClass]
    public class MoveTest
    {
        [TestMethod]
        public void ToString_WhenMove_ThenPrintCorrect()
        {
            Assert.AreEqual("e2e4.", new NormalMove(Piece.MakePiece('p'), 5, 2, 5, 4, null).ToString());
            Assert.AreEqual("e4d5p", new NormalMove(Piece.MakePiece('p'),5, 4, 4, 5, Piece.MakePiece('p')).ToString());
        }

        [TestMethod]
        public void EqualsTest_WhenComparingTwoEqualMoves_ThenEqualsReturnsTrue()
        {
            Assert.AreEqual(new NormalMove(Piece.MakePiece('p'), 5, 2, 5, 4, Piece.MakePiece('p')), new NormalMove(Piece.MakePiece('p'), 5, 2, 5, 4, Piece.MakePiece('p')));
        }

        [TestMethod]
        public void EqualsTest_WhenComparingTwoDifferentMoves_ThenReturnsFalse()
        {
            Assert.AreNotEqual(new NormalMove(Piece.MakePiece('p'), 'e', 2, 'e', 4, Piece.MakePiece('p')), new NormalMove(Piece.MakePiece('p'), 1, 2, 5, 4, Piece.MakePiece('p')));
            Assert.AreNotEqual(new NormalMove(Piece.MakePiece('p'), 'e', 2, 'e', 4, Piece.MakePiece('p')), new NormalMove(Piece.MakePiece('p'), 5, 1, 5, 4, Piece.MakePiece('p')));
            Assert.AreNotEqual(new NormalMove(Piece.MakePiece('p'), 'e', 2, 'e', 4, Piece.MakePiece('p')), new NormalMove(Piece.MakePiece('p'), 5, 2, 1, 4, Piece.MakePiece('p')));
            Assert.AreNotEqual(new NormalMove(Piece.MakePiece('p'), 'e', 2, 'e', 4, Piece.MakePiece('p')), new NormalMove(Piece.MakePiece('p'), 5, 2, 5, 1, Piece.MakePiece('p')));
            Assert.AreNotEqual(new NormalMove(Piece.MakePiece('p'), 'e', 2, 'e', 4, Piece.MakePiece('p')), new NormalMove(Piece.MakePiece('p'), 5, 2, 5, 4, Piece.MakePiece('r')));
            Assert.AreNotEqual(new NormalMove(Piece.MakePiece('p'), 'e', 2, 'e', 4, Piece.MakePiece('p')), new NormalMove(Piece.MakePiece('q'), 5, 2, 5, 4, Piece.MakePiece('p')));
        }

        [TestMethod]
        public void ConstructorTest_WhenStringParameter_ThenCorrectObject()
        {
            MoveBase move = new NormalMove(Piece.MakePiece('q'), 5, 2, 4, 3, Piece.MakePiece('p'));
            Assert.AreEqual(Piece.MakePiece('q'), move.MovingPiece);
            Assert.AreEqual('e'-'a' + 1, move.SourceFile);
            Assert.AreEqual(2, move.SourceRank);
            Assert.AreEqual('d'-'a' + 1, move.TargetFile);
            Assert.AreEqual(3, move.TargetRank);
            Assert.AreEqual(Piece.MakePiece('p'), move.CapturedPiece);
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
            IMove normalMove = new NormalMove(Piece.MakePiece('p'), 1, 1, 1, 1, null);
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
            Assert.AreEqual(true, MoveBase.IsCorrectMove(correctMoveString));
        }

        [TestMethod]
        public void IsCorrectMoveTest_WhenBadInput_ThenFalse()
        {
            String wrongMoveString = "abcd";
            Assert.AreEqual(false, MoveBase.IsCorrectMove(wrongMoveString));

            wrongMoveString = "abcde";
            Assert.AreEqual(false, MoveBase.IsCorrectMove(wrongMoveString));

            wrongMoveString = "abcdef";
            Assert.AreEqual(false, MoveBase.IsCorrectMove(wrongMoveString));
        }

        [TestMethod]
        public void MoveTest_WhenEnPassant_ThenCheckEnPassantCorrect()
        {
            MoveBase move = new EnPassantCaptureMove(Piece.MakePiece('p'), 1, 2, 2, 3, Piece.MakePiece('p'));
            MoveBase move2 = new NormalMove(Piece.MakePiece('p'), 1, 2, 2, 3, Piece.MakePiece('p'));
            // Assert.AreEqual("a2b3pe", move.ToString());
            Assert.AreNotEqual(move2, move);
            //Assert.AreEqual(new Move("a2b3pe"), move);
        }

        [TestMethod]
        public void MoveTest_WhenNormalMove_ThenEqualsCorrect()
        {
            // equal
            MoveBase move = new NormalMove(Piece.MakePiece('p'), 'a', 2, 'b', 3, Piece.MakePiece('q'));
            MoveBase move2 = new NormalMove(Piece.MakePiece('p'), 1,  2,  2,  3, Piece.MakePiece('q'));
            Assert.AreEqual(move2, move);

            // not equal different MovingPiece
            move = new NormalMove(Piece.MakePiece('q'), 'a', 2, 'b', 3, Piece.MakePiece('q'));
            move2 = new NormalMove(Piece.MakePiece('p'), 1,  2,  2,  3, Piece.MakePiece('q'));
            Assert.AreNotEqual(move2, move);

            // equal
            move = new NormalMove(Piece.MakePiece('p'), 'a', 2, 'a', 3, null);
            move2 = new NormalMove(Piece.MakePiece('p'), 1,  2,  1,  3, null);
            Assert.AreEqual(move, move2);
        }
    }
}