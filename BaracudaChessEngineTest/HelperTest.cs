using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BaracudaChessEngine;

namespace BaracudaChessEngineTest
{
    [TestClass]
    public class HelperTest
    {
        [TestMethod]
        public void FileToFileCharTest_When1_ThenA()
        {
            Assert.AreEqual('a', Helper.FileToFileChar(1));
            Assert.AreEqual('b', Helper.FileToFileChar(2));
            Assert.AreEqual('c', Helper.FileToFileChar(3));
            Assert.AreEqual('h', Helper.FileToFileChar(8));
        }

        [TestMethod]
        public void FileCharToFileTest_WhenA_Then1()
        {
            Assert.AreEqual(1, Helper.FileCharToFile('a'));
            Assert.AreEqual(6, Helper.FileCharToFile('f'));
            Assert.AreEqual(7, Helper.FileCharToFile('g'));
            Assert.AreEqual(8, Helper.FileCharToFile('h'));
        }
    }
}