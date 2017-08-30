using System;
using MantaChessEngine;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MantaChessEngineTest
{
    [TestClass]
    public class NodeTreeTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            var tree = new NodeTree<string>("root");
            tree.AddChild("level1-first");
            tree.AddChild("level1-second");

            Assert.AreEqual("level1-first", tree.GetChild(0).Data);
            Assert.AreEqual("level1-second", tree.GetChild(1).Data);
        }
    }
}
