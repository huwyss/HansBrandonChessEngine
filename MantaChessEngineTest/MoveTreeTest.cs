using System;
using System.Text;
using System.Collections.Generic;
using MantaChessEngine;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MantaChessEngineTest
{
    [TestClass]
    public class MoveTreeTest
    {
        [TestMethod]
        public void AddChildrenTest_WhenAddingChild_ThenHasNextChildTrue()
        {
            var target = new MoveTree();
            Assert.IsFalse(target.HasNextChild());

            var moves = new List<MoveBase>() {new NormalMove('p', 'e', 2, 'e', 4, '.')};
            target.AddChildren(moves);

            Assert.IsTrue(target.HasNextChild(), "should have next child.");
        }

        [TestMethod]
        public void AddChildrenTest_WhenAddingChildren_ThenCanGotoChildren()
        {
            var target = new MoveTree();
            var move1 = new NormalMove('p', 'e', 2, 'e', 4, '.');
            var move2 = new NormalMove('p', 'e', 2, 'e', 3, '.');
            var move3 = new NormalMove('p', 'e', 7, 'e', 5, '.');
            var move4 = new NormalMove('p', 'e', 7, 'e', 6, '.');

            var movesWhite = new List<MoveBase> {move1, move2};
            target.AddChildren(movesWhite);

            Assert.IsTrue(target.HasNextChild(), "should have next child.");

            target.GotoNextChild();
            Assert.AreEqual(move1, target.CurrentMove);
            target.GotoParent();
            Assert.IsTrue(target.HasNextChild());
            target.GotoNextChild();
            Assert.AreEqual(move2, target.CurrentMove);
            target.GotoParent();
            Assert.IsFalse(target.HasNextChild(), "Should not have next child.");
            Assert.IsTrue(target.IsRoot(), "We should be at the root node.");
        }

    }
}
