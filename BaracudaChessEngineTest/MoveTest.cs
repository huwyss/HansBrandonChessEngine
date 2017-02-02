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
            Assert.AreNotEqual(new Move(5, 2, 5, 4, 'p'), new Move(5, 2, 5, 4, 'x'));
        }

        [TestMethod]
        public void ConstructorTest_WhenStringParameter_ThenCorrectObject()
        {
            Assert.AreEqual(new Move(5, 2, 5, 4, 'p'), new Move("e2e4p"));
        }
    }

}