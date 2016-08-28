using System;
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
            Assert.AreEqual("e2e4 ", new Move(5, 2, 5, 4, ' ').ToString());
            Assert.AreEqual("e4d5p", new Move(5, 4, 4, 5, 'p').ToString());
        }
    }

}