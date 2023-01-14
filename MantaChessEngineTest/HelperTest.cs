using Microsoft.VisualStudio.TestTools.UnitTesting;
using MantaChessEngine;
using MantaCommon;

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
            Assert.AreEqual(ChessColor.Black, Helper.GetOppositeColor(ChessColor.White));
            Assert.AreEqual(ChessColor.White, Helper.GetOppositeColor(ChessColor.Black));
            Assert.AreEqual(ChessColor.Empty, Helper.GetOppositeColor(ChessColor.Empty));
        }

        [TestMethod]
        public void GetRankTest()
        {
            Assert.AreEqual(1, Helper.GetRank(Square.H1));
            Assert.AreEqual(2, Helper.GetRank(Square.G2));
            Assert.AreEqual(3, Helper.GetRank(Square.F3));
            Assert.AreEqual(4, Helper.GetRank(Square.E4));
            Assert.AreEqual(5, Helper.GetRank(Square.D5));
            Assert.AreEqual(6, Helper.GetRank(Square.C6));
            Assert.AreEqual(7, Helper.GetRank(Square.B7));
            Assert.AreEqual(8, Helper.GetRank(Square.A8));
        }

        [TestMethod]
        public void GetFileTest()
        {
            Assert.AreEqual(1, Helper.GetFile(Square.A1));
            Assert.AreEqual(2, Helper.GetFile(Square.B1));
            Assert.AreEqual(3, Helper.GetFile(Square.C2));
            Assert.AreEqual(4, Helper.GetFile(Square.D8));
            Assert.AreEqual(5, Helper.GetFile(Square.E4));
            Assert.AreEqual(6, Helper.GetFile(Square.F8));
            Assert.AreEqual(7, Helper.GetFile(Square.G2));
            Assert.AreEqual(8, Helper.GetFile(Square.H5));
        }
    }
}