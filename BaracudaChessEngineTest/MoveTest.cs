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
            Assert.AreEqual("e2e4.", new Move(5, 2, 5, 4, Definitions.EmptyField).ToString());
            Assert.AreEqual("e4d5p", new Move(5, 4, 4, 5, 'p').ToString());
        }

        [TestMethod]
        public void EqualsTest_WhenComparingTwoEqualMoves_ThenEqualsReturnsTrue()
        {
            Assert.AreEqual(new Move(5, 2, 5, 4, 'p'), new Move(5, 2, 5, 4, 'p'));
        }

        [TestMethod]
        public void EqualsTest_WhenComparingTwoDifferentMoves_ThenReturnsFalse()
        {
            Assert.AreNotEqual(new Move(5, 2, 5, 4, 'p'), new Move(1, 2, 5, 4, 'p'));
            Assert.AreNotEqual(new Move(5, 2, 5, 4, 'p'), new Move(5, 1, 5, 4, 'p'));
            Assert.AreNotEqual(new Move(5, 2, 5, 4, 'p'), new Move(5, 2, 1, 4, 'p'));
            Assert.AreNotEqual(new Move(5, 2, 5, 4, 'p'), new Move(5, 2, 5, 1, 'p'));
            Assert.AreNotEqual(new Move(5, 2, 5, 4, 'p'), new Move(5, 2, 5, 4, 'r'));
        }

        [TestMethod]
        public void ConstructorTest_WhenStringParameter_ThenCorrectObject()
        {
            Assert.AreEqual(new Move(5, 2, 5, 4, 'p'), new Move("e2e4p"));
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
            IMove normalMove = new Move(1, 1, 1, 1, '.');
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
    }

}