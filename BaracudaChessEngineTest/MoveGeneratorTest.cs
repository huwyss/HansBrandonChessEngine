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

            var moves = target.GetMoves(Helper.FileCharToFile('b'), 1); // get moves of knight

            Assert.AreEqual(2, moves.Count);
            Assert.AreEqual("b1a3.", moves[0].ToString());
            Assert.AreEqual("b1c3.", moves[1].ToString());
        }

        [TestMethod]
        public void GetMoves_WhenRook_ThenAllMoves()
        {
            MoveGenerator target = new MoveGenerator();
            Board board = new Board();
            string position = "........" +
                              "........" +
                              "....p..." +
                              "........" +
                              "....R..P" +
                              "........" +
                              "........" +
                              "........";
            board.SetPosition(position);
            target.SetBoard(board);

            var moves = target.GetMoves(Helper.FileCharToFile('e'), 4); // get moves of Rook

            Assert.AreEqual(11, moves.Count);
            Assert.AreEqual(true, moves.Contains(new Move("e4e5.")), "e4e5. missing");
            Assert.AreEqual(true, moves.Contains(new Move("e4e6p")), "e4e6p missing");
            Assert.AreEqual(true, moves.Contains(new Move("e4f4.")), "e4f4. missing");
            Assert.AreEqual(true, moves.Contains(new Move("e4g4.")), "e4g4. missing");
            Assert.AreEqual(true, moves.Contains(new Move("e4e3.")), "e4e3. missing");
            Assert.AreEqual(true, moves.Contains(new Move("e4e2.")), "e4e2. missing");
            Assert.AreEqual(true, moves.Contains(new Move("e4e1.")), "e4e1. missing");
            Assert.AreEqual(true, moves.Contains(new Move("e4d4.")), "e4d4. missing");
            Assert.AreEqual(true, moves.Contains(new Move("e4c4.")), "e4c4. missing");
            Assert.AreEqual(true, moves.Contains(new Move("e4b4.")), "e4b4. missing");
            Assert.AreEqual(true, moves.Contains(new Move("e4a4.")), "e4a4. missing");
        }

        [TestMethod]
        public void GetMoves_WhenQueen_ThenAllMoves()
        {
            MoveGenerator target = new MoveGenerator();
            Board board = new Board();
            string position = "........" +
                              "........" +
                              "........" +
                              "........" +
                              ".r.B...." +
                              "........" +
                              ".Q.P...." +
                              "........";
            board.SetPosition(position);
            target.SetBoard(board);

            var moves = target.GetMoves(Helper.FileCharToFile('b'), 2); // get moves of queen

            Assert.AreEqual(9, moves.Count);
            Assert.AreEqual(true, moves.Contains(new Move("b2b3.")), "b2b3. missing");
            Assert.AreEqual(true, moves.Contains(new Move("b2b4r")), "b2b4r missing");
            Assert.AreEqual(true, moves.Contains(new Move("b2c3.")), "b2c3. missing");
            Assert.AreEqual(true, moves.Contains(new Move("b2c2.")), "b2c2. missing");
            Assert.AreEqual(true, moves.Contains(new Move("b2c1.")), "b2c1. missing");
            Assert.AreEqual(true, moves.Contains(new Move("b2b1.")), "b2b1. missing");
            Assert.AreEqual(true, moves.Contains(new Move("b2a1.")), "b2a1. missing");
            Assert.AreEqual(true, moves.Contains(new Move("b2a2.")), "b2a2. missing");
            Assert.AreEqual(true, moves.Contains(new Move("b2a3.")), "b2a3. missing");
        }

        [TestMethod]
        public void GetMoves_WhenBishop_ThenAllMoves()
        {
            MoveGenerator target = new MoveGenerator();
            Board board = new Board();
            string position = "........" +
                              "........" +
                              "........" +
                              "........" +
                              "r...N..." +
                              "........" +
                              "..B....." +
                              "........";
            board.SetPosition(position);
            target.SetBoard(board);

            var moves = target.GetMoves(Helper.FileCharToFile('c'), 2); // get moves of bishop

            Assert.AreEqual(5, moves.Count);
            Assert.AreEqual(true, moves.Contains(new Move("c2d3.")), "c2d3. missing");
            Assert.AreEqual(true, moves.Contains(new Move("c2d1.")), "c2d1. missing");
            Assert.AreEqual(true, moves.Contains(new Move("c2b1.")), "c2b1. missing");
            Assert.AreEqual(true, moves.Contains(new Move("c2b3.")), "c2b3. missing");
            Assert.AreEqual(true, moves.Contains(new Move("c2a4r")), "c2a4r missing");
        }

        [TestMethod]
        public void GetMoves_WhenKing_ThenAllMoves() // castling not included ! todo!
        {
            MoveGenerator target = new MoveGenerator();
            Board board = new Board();
            string position = "........" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              ".rB....." +
                              ".KP.....";
            board.SetPosition(position);
            target.SetBoard(board);

            var moves = target.GetMoves(Helper.FileCharToFile('b'), 1); // king

            Assert.AreEqual(3, moves.Count);
            Assert.AreEqual(true, moves.Contains(new Move("b1b2r")), "b1b2r missing");
            Assert.AreEqual(true, moves.Contains(new Move("b1a1.")), "b1a1. missing");
            Assert.AreEqual(true, moves.Contains(new Move("b1a2.")), "b1a2. missing");
        }

        [TestMethod]
        public void GetMoves_WhenWhitePawn_ThenAllMoves() // en passant not included
        {
            MoveGenerator target = new MoveGenerator();
            Board board = new Board();
            string position = "........" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              ".r.b...." +
                              "..P....." +
                              "........";
            board.SetPosition(position);
            target.SetBoard(board);

            var moves = target.GetMoves(Helper.FileCharToFile('c'), 2); // pawn

            Assert.AreEqual(4, moves.Count);
            Assert.AreEqual(true, moves.Contains(new Move("c2c3.")), "c2c3. missing");
            Assert.AreEqual(true, moves.Contains(new Move("c2c4.")), "c2c4. missing");
            Assert.AreEqual(true, moves.Contains(new Move("c2b3r")), "c2b3r missing");
            Assert.AreEqual(true, moves.Contains(new Move("c2d3b")), "c2d3b missing");
        }

        [TestMethod]
        public void GetMoves_WhenWhitePawnBlocked_ThenAllMoves() // en passant not included
        {
            MoveGenerator target = new MoveGenerator();
            Board board = new Board();
            string position = "........" +
                              "........" +
                              "........" +
                              "........" +
                              "..n....." +
                              ".r.b...." +
                              "..P....." +
                              "........";
            board.SetPosition(position);
            target.SetBoard(board);

            var moves = target.GetMoves(Helper.FileCharToFile('c'), 2); // pawn

            Assert.AreEqual(3, moves.Count);
            Assert.AreEqual(true, moves.Contains(new Move("c2c3.")), "c2c3. missing");
            Assert.AreEqual(true, moves.Contains(new Move("c2b3r")), "c2b3r missing");
            Assert.AreEqual(true, moves.Contains(new Move("c2d3b")), "c2d3b missing");
        }

        [TestMethod]
        public void GetMoves_WhenBlackPawn_ThenAllMoves()
        {
            MoveGenerator target = new MoveGenerator();
            Board board = new Board();
            string position = "........" +
                              "..p....." +
                              ".R.B...." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "........";
            board.SetPosition(position);
            target.SetBoard(board);

            var moves = target.GetMoves(Helper.FileCharToFile('c'), 7); // black pawn

            Assert.AreEqual(4, moves.Count);
            Assert.AreEqual(true, moves.Contains(new Move("c7c6.")), "c7c6. missing");
            Assert.AreEqual(true, moves.Contains(new Move("c7c5.")), "c7c5. missing");
            Assert.AreEqual(true, moves.Contains(new Move("c7b6R")), "c7b6R missing");
            Assert.AreEqual(true, moves.Contains(new Move("c7d6B")), "c7d6B missing");
        }

        [TestMethod]
        public void IsMoveValidTest_WhenValidMoveWhite_ThenTrue()
        {
            MoveGenerator target = new MoveGenerator();
            Board board = new Board();
            string position = "........" +
                              "........" +
                              "........" +
                              "........" +
                              "..n....." +
                              ".r.b...." +
                              "..P....." +
                              "........";
            board.SetPosition(position);
            target.SetBoard(board);

            bool valid = target.IsMoveValid(new Move("c2c3.")); // pawn
            Assert.AreEqual(true, valid, "Move should be valid.");
        }

        [TestMethod]
        public void IsMoveValidTest_WhenInvalidMoveWhite_ThenFalse()
        {
            MoveGenerator target = new MoveGenerator();
            Board board = new Board();
            string position = "........" +
                              "........" +
                              "........" +
                              "........" +
                              "..n....." +
                              ".r.b...." +
                              "..P....." +
                              "........";
            board.SetPosition(position);
            target.SetBoard(board);

            bool valid = target.IsMoveValid(new Move("c2c4.")); // pawn
            Assert.AreEqual(false, valid, "Move should be invalid.");
        }

        [TestMethod]
        public void IsMoveValidTest_WhenValidMoveBlack_ThenTrue()
        {
            MoveGenerator target = new MoveGenerator();
            Board board = new Board();
            string position = "........" +
                              "..p....." +
                              ".R.B...." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "........";
            board.SetPosition(position);
            target.SetBoard(board);

            bool valid = target.IsMoveValid(new Move("c7c5.")); // pawn
            Assert.AreEqual(true, valid, "Move should be valid.");

            valid = target.IsMoveValid(new Move("c7c6.")); // pawn
            Assert.AreEqual(true, valid, "Move should be valid.");
        }
    }
}