using System;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BaracudaChessEngine;

namespace BaracudaChessEngineTest
{
    [TestClass]
    public class MoveTest
    {
        [TestMethod]
        public void ToString_WhenMove_ThenPrintCorrect()
        {
            Assert.AreEqual("e2e4.", new Move('p', 5, 2, 5, 4, Definitions.EmptyField).ToString());
            Assert.AreEqual("e4d5p", new Move('p',5, 4, 4, 5, 'p').ToString());
        }

        [TestMethod]
        public void EqualsTest_WhenComparingTwoEqualMoves_ThenEqualsReturnsTrue()
        {
            Assert.AreEqual(new Move('p', 5, 2, 5, 4, 'p'), new Move('p', 5, 2, 5, 4, 'p'));
        }

        [TestMethod]
        public void EqualsTest_WhenComparingTwoDifferentMoves_ThenReturnsFalse()
        {
            Assert.AreNotEqual(new Move('p', 5, 2, 5, 4, 'p'), new Move('p', 1, 2, 5, 4, 'p'));
            Assert.AreNotEqual(new Move('p', 5, 2, 5, 4, 'p'), new Move('p', 5, 1, 5, 4, 'p'));
            Assert.AreNotEqual(new Move('p', 5, 2, 5, 4, 'p'), new Move('p', 5, 2, 1, 4, 'p'));
            Assert.AreNotEqual(new Move('p', 5, 2, 5, 4, 'p'), new Move('p', 5, 2, 5, 1, 'p'));
            Assert.AreNotEqual(new Move('p', 5, 2, 5, 4, 'p'), new Move('p', 5, 2, 5, 4, 'r'));
            Assert.AreNotEqual(new Move('p', 5, 2, 5, 4, 'p'), new Move('q', 5, 2, 5, 4, 'r'));
        }

        [TestMethod]
        public void ConstructorTest_WhenStringParameter_ThenCorrectObject()
        {
            Move move = new Move('q', 5, 2, 5, 4, 'p');
            move.MovingPiece = 'q';
            Assert.AreEqual(move, new Move("e2e4p"));
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
            IMove normalMove = new Move('p', 1, 1, 1, 1, '.');
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
            Assert.AreEqual(true, Move.IsCorrectMove(correctMoveString));
        }

        [TestMethod]
        public void IsCorrectMoveTest_WhenBadInput_ThenFalse()
        {
            String wrongMoveString = "abcd";
            Assert.AreEqual(false, Move.IsCorrectMove(wrongMoveString));

            wrongMoveString = "abcde";
            Assert.AreEqual(false, Move.IsCorrectMove(wrongMoveString));

            wrongMoveString = "abcdef";
            Assert.AreEqual(false, Move.IsCorrectMove(wrongMoveString));
        }

        [TestMethod]
        public void MoveTest_WhenEnPassant_ThenCheckEnPassantCorrect()
        {
            Move move = new Move('p', 1, 2, 2, 3, 'p', true);
            Move move2 = new Move('p', 1, 2, 2, 3, 'p', false);
            Assert.AreEqual("a2b3pe", move.ToString());
            Assert.AreNotEqual(move2, move);
            Assert.AreEqual(new Move("a2b3pe"), move);
        }
    }
}