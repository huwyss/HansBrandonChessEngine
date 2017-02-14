using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BaracudaChessEngine;

namespace BaracudaChessEngineTest
{
    [TestClass]
    public class MoveGeneratorTest
    {
        // ----------------------------------------------------------------------------------------------------
        // Get Moves Tests
        // ----------------------------------------------------------------------------------------------------

        [TestMethod]
        public void GetMoves_WhenKnightB1_ThenTwoValidMoves()
        {
            MoveGenerator target = new MoveGenerator();
            Board initBoard = new Board(target);
            initBoard.SetInitialPosition();
            
            var moves = target.GetMoves(initBoard, Helper.FileCharToFile('b'), 1); // get moves of knight

            Assert.AreEqual(2, moves.Count);
            Assert.AreEqual("b1a3.", moves[0].ToString());
            Assert.AreEqual("b1c3.", moves[1].ToString());
        }

        [TestMethod]
        public void GetMoves_WhenRook_ThenAllMoves()
        {
            MoveGenerator target = new MoveGenerator();
            Board board = new Board(target);
            string position = "........" +
                              "........" +
                              "....p..." +
                              "........" +
                              "....R..P" +
                              "........" +
                              "........" +
                              "........";
            board.SetPosition(position);
            
            var moves = target.GetMoves(board, Helper.FileCharToFile('e'), 4); // get moves of Rook

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
            Board board = new Board(target);
            string position = "........" +
                              "........" +
                              "........" +
                              "........" +
                              ".r.B...." +
                              "........" +
                              ".Q.P...." +
                              "........";
            board.SetPosition(position);
           
            var moves = target.GetMoves(board, Helper.FileCharToFile('b'), 2); // get moves of queen

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
            Board board = new Board(target);
            string position = "........" +
                              "........" +
                              "........" +
                              "........" +
                              "r...N..." +
                              "........" +
                              "..B....." +
                              "........";
            board.SetPosition(position);
            
            var moves = target.GetMoves(board, Helper.FileCharToFile('c'), 2); // get moves of bishop

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
            Board board = new Board(target);
            string position = "........" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              ".rB....." +
                              ".KP.....";
            board.SetPosition(position);
            
            var moves = target.GetMoves(board, Helper.FileCharToFile('b'), 1); // king

            Assert.AreEqual(3, moves.Count);
            Assert.AreEqual(true, moves.Contains(new Move("b1b2r")), "b1b2r missing");
            Assert.AreEqual(true, moves.Contains(new Move("b1a1.")), "b1a1. missing");
            Assert.AreEqual(true, moves.Contains(new Move("b1a2.")), "b1a2. missing");
        }

        //
        // Pawn moves
        // 

        [TestMethod]
        public void GetMoves_WhenWhitePawn_ThenAllMoves() // en passant not included
        {
            MoveGenerator target = new MoveGenerator();
            Board board = new Board(target);
            string position = "........" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              ".r.b...." +
                              "..P....." +
                              "........";
            board.SetPosition(position);
            
            var moves = target.GetMoves(board, Helper.FileCharToFile('c'), 2); // pawn

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
            Board board = new Board(target);
            string position = "........" +
                              "........" +
                              "........" +
                              "........" +
                              "..n....." +
                              ".r.b...." +
                              "..P....." +
                              "........";
            board.SetPosition(position);

            var moves = target.GetMoves(board, Helper.FileCharToFile('c'), 2); // pawn

            Assert.AreEqual(3, moves.Count);
            Assert.AreEqual(true, moves.Contains(new Move("c2c3.")), "c2c3. missing");
            Assert.AreEqual(true, moves.Contains(new Move("c2b3r")), "c2b3r missing");
            Assert.AreEqual(true, moves.Contains(new Move("c2d3b")), "c2d3b missing");
        }

        [TestMethod]
        public void GetMoves_WhenWhitePawnBlockedInMiddleOfBoard_ThenAllMoves()
        {
            MoveGenerator target = new MoveGenerator();
            Board board = new Board(target);
            string position = "........" +
                              "........" + 
                              "..p....." + // black pawn
                              "..P....." + // white pawn must not do c5-c7 !
                              "........" +
                              "........" +
                              "........" +
                              "........";
            board.SetPosition(position);

            var moves = target.GetMoves(board, Helper.FileCharToFile('c'), 5); // white pawn

            Assert.AreEqual(0, moves.Count);
        }

        [TestMethod]
        public void GetMoves_WhenWhitePawnAtA5_ThenAllMoves()
        {
            MoveGenerator target = new MoveGenerator();
            Board board = new Board(target);
            string position = "........" +
                              "........" + 
                              "........" + 
                              "........" +
                              "........" +
                              "..p....." + // black pawn
                              "..P....." + // white pawn must not do c2-c4 !
                              "........";
            board.SetPosition(position);

            var moves = target.GetMoves(board, Helper.FileCharToFile('c'), 2); // white pawn

            Assert.AreEqual(0, moves.Count);
        }

        [TestMethod]
        public void GetMoves_WhenWhiteCanCaptureEnPassant_ThenListEnPassant()
        {
            MoveGenerator target = new MoveGenerator();
            Board board = new Board(target);
            string position = ".......k" +
                              "p......." +
                              "........" +
                              ".P......" +
                              "........" +
                              "........" + 
                              "........" + 
                              "...K....";
            board.SetPosition(position);
            board.Move(new Move("a7a5"));
            Assert.AreEqual(Helper.FileCharToFile('a'), board.History.LastEnPassantFile);
            Assert.AreEqual(6, board.History.LastEnPassantRank);

            var moves = target.GetMoves(board, Helper.FileCharToFile('b'), 5); // white pawn

            Assert.AreEqual(2, moves.Count);
            Assert.AreEqual(true, moves.Contains(new Move("b5a6pe")), "b5a6pe en passant missing");
            Assert.AreEqual(true, moves.Contains(new Move("b5b6.")), "b5b6. missing");
        }

        [TestMethod]
        public void GetMoves_WhenBlackPawn_ThenAllMoves()
        {
            MoveGenerator target = new MoveGenerator();
            Board board = new Board(target);
            string position = "........" +
                              "..p....." +
                              ".R.B...." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "........";
            board.SetPosition(position);
            
            var moves = target.GetMoves(board, Helper.FileCharToFile('c'), 7); // black pawn

            Assert.AreEqual(4, moves.Count);
            Assert.AreEqual(true, moves.Contains(new Move("c7c6.")), "c7c6. missing");
            Assert.AreEqual(true, moves.Contains(new Move("c7c5.")), "c7c5. missing");
            Assert.AreEqual(true, moves.Contains(new Move("c7b6R")), "c7b6R missing");
            Assert.AreEqual(true, moves.Contains(new Move("c7d6B")), "c7d6B missing");
        }

        [TestMethod]
        public void GetMoves_WhenBlackPawnAta4_ThenAllMoves()
        {
            MoveGenerator target = new MoveGenerator();
            Board board = new Board(target);
            string position = "........" +
                              "........" +
                              "........" +
                              ".p......" + // black pawn must not do b5-b3 !
                              "........" + 
                              "........" +
                              "........" +
                              "........";
            board.SetPosition(position);

            var moves = target.GetMoves(board, Helper.FileCharToFile('b'), 5); // black pawn

            Assert.AreEqual(1, moves.Count);
            Assert.AreEqual(true, moves.Contains(new Move("b5b4.")), "b3b4 missing");
        }

        // ----------------------------------------------------------------------------------------------------
        // Get All Moves Tests
        // ----------------------------------------------------------------------------------------------------

        [TestMethod]
        public void GetAllMoves_WhenTwoPieces_ThenShowMovesOfBothPieces()
        {
            MoveGenerator target = new MoveGenerator();
            Board board = new Board(target);
            string position = "........" +
                              "........" +
                              "....p..." +
                              "........" +
                              "........" +
                              "........" +
                              "....P..." +
                              "....K...";
            board.SetPosition(position);
           
            var moves = target.GetAllMoves(board, Definitions.ChessColor.White);

            Assert.AreEqual(6, moves.Count);
            Assert.AreEqual(true, moves.Contains(new Move("e2e3.")), "e2e3. missing");
            Assert.AreEqual(true, moves.Contains(new Move("e2e4.")), "e2e4. missing");
            Assert.AreEqual(true, moves.Contains(new Move("e1d1.")), "e1d1. missing");
            Assert.AreEqual(true, moves.Contains(new Move("e1d2.")), "e1d2. missing");
            Assert.AreEqual(true, moves.Contains(new Move("e1f2.")), "e1f2. missing");
            Assert.AreEqual(true, moves.Contains(new Move("e1f1.")), "e1f1. missing");
        }

        [TestMethod]
        [Ignore]
        public void GetAllMoves_WhenCheck_ThenKingMustEscapeCheck()
        {
            MoveGenerator target = new MoveGenerator();
            Board board = new Board(target);
            string position = "........" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              ".......k" +
                              ".......r" +
                              ".......K"; // king is in check and must escape. only move is Kh1g1
            board.SetPosition(position);
            
            var moves = target.GetAllMoves(board, Definitions.ChessColor.White);

            Assert.AreEqual(1, moves.Count, "Only one move is possible. The other moves for white are in check.");
            Assert.AreEqual(true, moves.Contains(new Move("h1g1.")), "h1g1. missing");
        }

        // ----------------------------------------------------------------------------------------------------
        // Is Move Valid Tests
        // ----------------------------------------------------------------------------------------------------

        [TestMethod]
        public void IsMoveValidTest_WhenValidMoveWhite_ThenTrue()
        {
            MoveGenerator target = new MoveGenerator();
            Board board = new Board(target);
            string position = "........" +
                              "........" +
                              "........" +
                              "........" +
                              "..n....." +
                              ".r.b...." +
                              "..P....." +
                              "........";
            board.SetPosition(position);
            
            bool valid = target.IsMoveValid(board, new Move("c2c3.")); // pawn
            Assert.AreEqual(true, valid, "Move should be valid.");
        }

        [TestMethod]
        public void IsMoveValidTest_WhenInvalidMoveWhite_ThenFalse()
        {
            MoveGenerator target = new MoveGenerator();
            Board board = new Board(target);
            string position = "........" +
                              "........" +
                              "........" +
                              "........" +
                              "..n....." +
                              ".r.b...." +
                              "..P....." +
                              "........";
            board.SetPosition(position);
            
            bool valid = target.IsMoveValid(board, new Move("c2c4.")); // pawn
            Assert.AreEqual(false, valid, "Move should be invalid.");
        }

        [TestMethod]
        public void IsMoveValidTest_WhenValidMoveBlack_ThenTrue()
        {
            MoveGenerator target = new MoveGenerator();
            Board board = new Board(target);
            string position = "........" +
                              "..p....." +
                              ".R.B...." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "........";
            board.SetPosition(position);
            
            bool valid = target.IsMoveValid(board, new Move("c7c5.")); // pawn
            Assert.AreEqual(true, valid, "Move should be valid.");

            valid = target.IsMoveValid(board, new Move("c7c6.")); // pawn
            Assert.AreEqual(true, valid, "Move should be valid.");
        }
        
        // ----------------------------------------------------------------------------------------------------
        // Get End Position Tests
        // ----------------------------------------------------------------------------------------------------

        [TestMethod]
        public void GetEndPosition_WhenKnightSequence_ThenCorrectEndRankFile()
        {
            string knightSequence = "uul";
            int targetRank;
            int targetFile;
            bool valid;

            var target = new MoveGenerator();
            target.GetEndPosition(Helper.FileCharToFile('b'), 1, knightSequence, out targetFile, out targetRank, out valid);

            Assert.AreEqual(Helper.FileCharToFile('a'), targetFile);
            Assert.AreEqual(3, targetRank);
            Assert.AreEqual(true, valid);
        }

        [TestMethod]
        public void GetEndPosition_WhenOtherKnightSequence_ThenCorrectEndRankFile()
        {
            string knightSequence = "ddr";
            int targetRank;
            int targetFile;
            bool valid;

            var target = new MoveGenerator();
            target.GetEndPosition(Helper.FileCharToFile('c'), 3, knightSequence, out targetFile, out targetRank, out valid);

            Assert.AreEqual(Helper.FileCharToFile('d'), targetFile);
            Assert.AreEqual(1, targetRank);
            Assert.AreEqual(true, valid);
        }

        [TestMethod]
        public void GetEndPosition_WhenKnightSequenceInvalid_ThenMoveInvalid()
        {
            string knightSequence = "ddl";
            int targetRank;
            int targetFile;
            bool valid;

            var target = new MoveGenerator();
            target.GetEndPosition(Helper.FileCharToFile('a'), 1, knightSequence, out targetFile, out targetRank, out valid);

            Assert.AreEqual(false, valid);
        }

        [TestMethod]
        public void GetCorrectMoveTest_Whene2e4_ThenAddDot()
        {
            MoveGenerator target = new MoveGenerator();
            Board board = new Board(target);
            board.SetInitialPosition();
            
            Move actualMove = target.GetCorrectMove(board, "e2e4");
            Assert.AreEqual("e2e4.", actualMove.ToString());
        }

        [TestMethod]
        public void GetCorrectMoveTest_WhenCaptureNormal_ThenAddCapturedPiece()
        {
            MoveGenerator target = new MoveGenerator();
            Board board = new Board(target);
            string position = "...k...." +
                              "........" +
                              "..p....." +
                              "...P...." +
                              "........" +
                              "........" +
                              "........" +
                              "..K.....";
            board.SetPosition(position);

            Move actualMove = target.GetCorrectMove(board, "d5c6");
            Assert.AreEqual("d5c6p", actualMove.ToString());
        }

        [TestMethod]
        public void GetCorrectMoveTest_WhenCaptureEnPassant_ThenAddCapturedPiece()
        {
            MoveGenerator target = new MoveGenerator();
            Board board = new Board(target);
            string position = "...k...." +
                              "..p....." +
                              "........" +
                              "...P...." +
                              "........" +
                              "........" +
                              "........" +
                              "..K.....";
            board.SetPosition(position);
            board.Move(new Move("c7c5"));

            Move actualMove = target.GetCorrectMove(board, "d5c6");
            Assert.AreEqual("d5c6pe", actualMove.ToString());
        }
    }
}