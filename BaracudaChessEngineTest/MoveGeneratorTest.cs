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

        [TestMethod]
        public void GetMoves_WhenQueen_ThenAllMoves()
        {
            MoveGenerator target = new MoveGenerator();
            Board board = new Board();
            string position = "        " +
                              "        " +
                              "        " +
                              "        " +
                              " r B    " +
                              "        " +
                              " Q P    " +
                              "        ";
            board.SetPosition(position);
            target.SetBoard(board);

            var moves = target.GetMoves(Helper.FileCharToFile('b'), 2); // queen

            Assert.AreEqual(9, moves.Count);
            Assert.AreEqual("b2b3 ", moves[0].ToString());
            Assert.AreEqual("b2b4r", moves[1].ToString());
            Assert.AreEqual("b2c3 ", moves[2].ToString());
            Assert.AreEqual("b2c2 ", moves[3].ToString());
            Assert.AreEqual("b2c1 ", moves[4].ToString());
            Assert.AreEqual("b2b1 ", moves[5].ToString());
            Assert.AreEqual("b2a1 ", moves[6].ToString());
            Assert.AreEqual("b2a2 ", moves[7].ToString());
            Assert.AreEqual("b2a3 ", moves[8].ToString());
        }

        [TestMethod]
        public void GetMoves_WhenBishop_ThenAllMoves()
        {
            MoveGenerator target = new MoveGenerator();
            Board board = new Board();
            string position = "        " +
                              "        " +
                              "        " +
                              "        " +
                              "r   N   " +
                              "        " +
                              "  B     " +
                              "        ";
            board.SetPosition(position);
            target.SetBoard(board);

            var moves = target.GetMoves(Helper.FileCharToFile('c'), 2); // bishop

            Assert.AreEqual(5, moves.Count);
            Assert.AreEqual("c2d3 ", moves[0].ToString());
            Assert.AreEqual("c2d1 ", moves[1].ToString());
            Assert.AreEqual("c2b1 ", moves[2].ToString());
            Assert.AreEqual("c2b3 ", moves[3].ToString());
            Assert.AreEqual("c2a4r", moves[4].ToString());
        }

        [TestMethod]
        public void GetMoves_WhenKing_ThenAllMoves() // castling not included ! todo!
        {
            MoveGenerator target = new MoveGenerator();
            Board board = new Board();
            string position = "        " +
                              "        " +
                              "        " +
                              "        " +
                              "        " +
                              "        " +
                              " rB     " +
                              " KP     ";
            board.SetPosition(position);
            target.SetBoard(board);

            var moves = target.GetMoves(Helper.FileCharToFile('b'), 1); // king

            Assert.AreEqual(3, moves.Count);
            Assert.AreEqual("b1b2r", moves[0].ToString());
            Assert.AreEqual("b1a1 ", moves[1].ToString());
            Assert.AreEqual("b1a2 ", moves[2].ToString());
        }

        [TestMethod]
        public void GetMoves_WhenPawn_ThenAllMoves() // en passant not included
        {
            MoveGenerator target = new MoveGenerator();
            Board board = new Board();
            string position = "        " +
                              "        " +
                              "        " +
                              "        " +
                              "        " +
                              " r b    " +
                              "  P     " +
                              "        ";
            board.SetPosition(position);
            target.SetBoard(board);

            var moves = target.GetMoves(Helper.FileCharToFile('c'), 2); // pawn

            Assert.AreEqual(4, moves.Count);
            Assert.AreEqual("c2c3 ", moves[0].ToString());
            Assert.AreEqual("c2c4 ", moves[1].ToString());
            Assert.AreEqual("c2b3r", moves[2].ToString());
            Assert.AreEqual("c2d3b", moves[3].ToString());
        }

        [TestMethod]
        public void GetMoves_WhenPawnBlocked_ThenAllMoves() // en passant not included
        {
            MoveGenerator target = new MoveGenerator();
            Board board = new Board();
            string position = "        " +
                              "        " +
                              "        " +
                              "        " +
                              "  n     " +
                              " r b    " +
                              "  P     " +
                              "        ";
            board.SetPosition(position);
            target.SetBoard(board);

            var moves = target.GetMoves(Helper.FileCharToFile('c'), 2); // pawn

            Assert.AreEqual(3, moves.Count);
            Assert.AreEqual("c2c3 ", moves[0].ToString());
            Assert.AreEqual("c2b3r", moves[1].ToString());
            Assert.AreEqual("c2d3b", moves[2].ToString());
        }

        [TestMethod]
        public void GetMoves_WhenBlackPawn_ThenAllMoves()
        {
            MoveGenerator target = new MoveGenerator();
            Board board = new Board();
            string position = "        " +
                              "  p     " +
                              " R B    " +
                              "        " +
                              "        " +
                              "        " +
                              "        " +
                              "        ";
            board.SetPosition(position);
            target.SetBoard(board);

            var moves = target.GetMoves(Helper.FileCharToFile('c'), 7); // black pawn

            Assert.AreEqual(4, moves.Count);
            Assert.AreEqual("c7c6 ", moves[0].ToString());
            Assert.AreEqual("c7c5 ", moves[1].ToString());
            Assert.AreEqual("c7b6R", moves[2].ToString());
            Assert.AreEqual("c7d6B", moves[3].ToString());
        }

    }
}