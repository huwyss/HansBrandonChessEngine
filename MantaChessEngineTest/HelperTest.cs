using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MantaChessEngine;

namespace MantaChessEngineTest
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

        [TestMethod]
        public void GetOpositColorTest_WhenWhite_ThenOpositeIsBlack_AndViceVersa()
        {
            Assert.AreEqual(Definitions.ChessColor.Black, Helper.GetOpositeColor(Definitions.ChessColor.White));
            Assert.AreEqual(Definitions.ChessColor.White, Helper.GetOpositeColor(Definitions.ChessColor.Black));
            Assert.AreEqual(Definitions.ChessColor.Empty, Helper.GetOpositeColor(Definitions.ChessColor.Empty));
        }
    }
}