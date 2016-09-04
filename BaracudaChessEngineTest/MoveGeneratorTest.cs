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
            Assert.AreEqual("b1a3 ", moves[0]);
            Assert.AreEqual("b1c3 ", moves[1]);
        }

    }

}