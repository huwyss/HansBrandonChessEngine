using System;
using MantaChessEngine;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MantaChessEngineTest
{
    [TestClass]
    public class NodeTreeTest
    {
        [TestMethod]
        public void NodeTreeTest_WhenAddChildren_ThenCanGetChildren()
        {
            // parent
            var tree = new TreeNode<string>("root", null); // null: kein parent
            // children
            tree.AddChild("level1-first");
            tree.AddChild("level1-second");
            // children-Children
            var childchild = tree.GetChild(0);
            childchild.AddChild("level2-first");
            childchild.AddChild("level2-second");
            var childchild2 = tree.GetChild(1);
            childchild2.AddChild("level2-third");

            Assert.AreEqual("level1-first", tree.GetChild(0).Data);
            Assert.AreEqual("level1-second", tree.GetChild(1).Data);
            Assert.AreEqual("level2-first", tree.GetChild(0).GetChild(0).Data);
            Assert.AreEqual("level2-second", tree.GetChild(0).GetChild(1).Data);
            Assert.AreEqual("level2-third", tree.GetChild(1).GetChild(0).Data);

            Assert.AreEqual(2, tree.ChildrenCount);
        }

        [TestMethod]
        public void NodeTreeTest_WhenOnChild_ThenCanGetParent()
        {
            // parent
            var tree = new TreeNode<string>("root", null); // null: kein parent
            // children
            tree.AddChild("child");

            var child = tree.GetChild(0);
            var parent = child.Parent;

            Assert.AreEqual("root", parent.Data);
        }
    }
}
