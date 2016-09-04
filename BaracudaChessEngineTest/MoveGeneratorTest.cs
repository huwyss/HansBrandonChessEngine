using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BaracudaChessEngine;

namespace BaracudaChessEngineTest
{
    [TestClass]
    public class MoveGeneratorTest
    {
        [TestMethod]
        public void GetMoves_WhenKnightB1_ThenTwoValidMoves()
        {
            MoveGenerator target = new MoveGenerator();
            Board initBoard = new Board();
            initBoard.SetInitialPosition();
            target.SetBoard(initBoard);

            var moves = target.GetMoves(Helper.FileCharToFile('b'), 1); // knight

            Assert.AreEqual(2, moves.Count);
            Assert.AreEqual("b1a3 ", moves[0].ToString());
            Assert.AreEqual("b1c3 ", moves[1].ToString());
        }

        [TestMethod]
        public void GetMoves_WhenRook_ThenAllMoves()
        {
            MoveGenerator target = new MoveGenerator();
            Board board = new Board();
            string position = "        " +
                              "        " +
                              "    p   " +
                              "        " +
                              "    R  P" +
                              "        " +
                              "        " +
                              "        ";
            board.SetPosition(position);
            target.SetBoard(board);

            var moves = target.GetMoves(Helper.FileCharToFile('e'), 4); // Rook

            Assert.AreEqual(11, moves.Count);
            Assert.AreEqual("e4e5 ", moves[0].ToString());
            Assert.AreEqual("e4e6p", moves[1].ToString());
            Assert.AreEqual("e4f4 ", moves[2].ToString());
            Assert.AreEqual("e4g4 ", moves[3].ToString());
            Assert.AreEqual("e4e3 ", moves[4].ToString());
            Assert.AreEqual("e4e2 ", moves[5].ToString());
            Assert.AreEqual("e4e1 ", moves[6].ToString());
            Assert.AreEqual("e4d4 ", moves[7].ToString());
            Assert.AreEqual("e4c4 ", moves[8].ToString());
            Assert.AreEqual("e4b4 ", moves[9].ToString());
            Assert.AreEqual("e4a4 ", moves[10].ToString());
        }

    }

}