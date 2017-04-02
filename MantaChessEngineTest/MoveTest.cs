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
            Assert.AreEqual("e2e4.", new NormalMove('p', 5, 2, 5, 4, Definitions.EmptyField).ToString());
            Assert.AreEqual("e4d5p", new NormalMove('p',5, 4, 4, 5, 'p').ToString());
        }

        [TestMethod]
        public void EqualsTest_WhenComparingTwoEqualMoves_ThenEqualsReturnsTrue()
        {
            Assert.AreEqual(new NormalMove('p', 5, 2, 5, 4, 'p'), new NormalMove('p', 5, 2, 5, 4, 'p'));
        }

        [TestMethod]
        public void EqualsTest_WhenComparingTwoDifferentMoves_ThenReturnsFalse()
        {
            Assert.AreNotEqual(new NormalMove('p', 5, 2, 5, 4, 'p'), new NormalMove('p', 1, 2, 5, 4, 'p'));
            Assert.AreNotEqual(new NormalMove('p', 5, 2, 5, 4, 'p'), new NormalMove('p', 5, 1, 5, 4, 'p'));
            Assert.AreNotEqual(new NormalMove('p', 5, 2, 5, 4, 'p'), new NormalMove('p', 5, 2, 1, 4, 'p'));
            Assert.AreNotEqual(new NormalMove('p', 5, 2, 5, 4, 'p'), new NormalMove('p', 5, 2, 5, 1, 'p'));
            Assert.AreNotEqual(new NormalMove('p', 5, 2, 5, 4, 'p'), new NormalMove('p', 5, 2, 5, 4, 'r'));
            Assert.AreNotEqual(new NormalMove('p', 5, 2, 5, 4, 'p'), new NormalMove('q', 5, 2, 5, 4, 'p'));
        }

        [TestMethod]
        public void ConstructorTest_WhenStringParameter_ThenCorrectObject()
        {
            MoveBase move = new NormalMove('q', 5, 2, 5, 4, 'p');
            move.MovingPiece = 'q';
            Assert.AreEqual(move, new NormalMove("e2e4p"));
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
            IMove normalMove = new NormalMove('p', 1, 1, 1, 1, '.');
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
            MoveBase move = new EnPassantCaptureMove('p', 1, 2, 2, 3, 'p');
            MoveBase move2 = new NormalMove('p', 1, 2, 2, 3, 'p');
            // Assert.AreEqual("a2b3pe", move.ToString());
            Assert.AreNotEqual(move2, move);
            //Assert.AreEqual(new Move("a2b3pe"), move);
        }

        [TestMethod]
        public void MoveTest_WhenMovingPiece_ThenCorrect()
        {
            // equal
            MoveBase move = new NormalMove('p', 1, 2, 2, 3, 'q');
            MoveBase move2 = new NormalMove('p', 1, 2, 2, 3, 'q');
            Assert.AreEqual(move2, move);

            // not equal different MovingPiece
            move = new NormalMove('q', 1, 2, 2, 3, 'q');
            move2 = new NormalMove('p', 1, 2, 2, 3, 'q');
            Assert.AreNotEqual(move2, move);

            // equal
            move = new NormalMove("a2a3.");
            move2 = new NormalMove('p', 1, 2, 1, 3, '.');
            Assert.AreEqual(move, move2);
        }
    }
}