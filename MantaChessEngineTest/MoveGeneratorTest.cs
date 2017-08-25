using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MantaChessEngine;

namespace MantaChessEngineTest
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
            MoveGenerator target = new MoveGenerator(new MoveFactory());
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
            MoveGenerator target = new MoveGenerator(new MoveFactory());
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
            Assert.AreEqual(true, moves.Contains(new NormalMove('R', 'e', 4, 'e', 5, '.')), "e4e5. missing");
            Assert.AreEqual(true, moves.Contains(new NormalMove('R', 'e', 4, 'e', 6, 'p')), "e4e6p missing");
            Assert.AreEqual(true, moves.Contains(new NormalMove('R', 'e', 4, 'f', 4, '.')), "e4f4. missing");
            Assert.AreEqual(true, moves.Contains(new NormalMove('R', 'e', 4, 'g', 4, '.')), "e4g4. missing");
            Assert.AreEqual(true, moves.Contains(new NormalMove('R', 'e', 4, 'e', 3, '.')), "e4e3. missing");
            Assert.AreEqual(true, moves.Contains(new NormalMove('R', 'e', 4, 'e', 2, '.')), "e4e2. missing");
            Assert.AreEqual(true, moves.Contains(new NormalMove('R', 'e', 4, 'e', 1, '.')), "e4e1. missing");
            Assert.AreEqual(true, moves.Contains(new NormalMove('R', 'e', 4, 'd', 4, '.')), "e4d4. missing");
            Assert.AreEqual(true, moves.Contains(new NormalMove('R', 'e', 4, 'c', 4, '.')), "e4c4. missing");
            Assert.AreEqual(true, moves.Contains(new NormalMove('R', 'e', 4, 'b', 4, '.')), "e4b4. missing");
            Assert.AreEqual(true, moves.Contains(new NormalMove('R', 'e', 4, 'a', 4, '.')), "e4a4. missing");
        }

        [TestMethod]
        public void GetMoves_WhenQueen_ThenAllMoves()
        {
            MoveGenerator target = new MoveGenerator(new MoveFactory());
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
            Assert.AreEqual(true, moves.Contains(new NormalMove('Q', 'b', 2, 'b', 3, '.')), "b2b3. missing");
            Assert.AreEqual(true, moves.Contains(new NormalMove('Q', 'b', 2, 'b', 4, 'r')), "b2b4r missing");
            Assert.AreEqual(true, moves.Contains(new NormalMove('Q', 'b', 2, 'c', 3, '.')), "b2c3. missing");
            Assert.AreEqual(true, moves.Contains(new NormalMove('Q', 'b', 2, 'c', 2, '.')), "b2c2. missing");
            Assert.AreEqual(true, moves.Contains(new NormalMove('Q', 'b', 2, 'c', 1, '.')), "b2c1. missing");
            Assert.AreEqual(true, moves.Contains(new NormalMove('Q', 'b', 2, 'b', 1, '.')), "b2b1. missing");
            Assert.AreEqual(true, moves.Contains(new NormalMove('Q', 'b', 2, 'a', 1, '.')), "b2a1. missing");
            Assert.AreEqual(true, moves.Contains(new NormalMove('Q', 'b', 2, 'a', 2, '.')), "b2a2. missing");
            Assert.AreEqual(true, moves.Contains(new NormalMove('Q', 'b', 2, 'a', 3, '.')), "b2a3. missing");
        }

        [TestMethod]
        public void GetMoves_WhenBishop_ThenAllMoves()
        {
            MoveGenerator target = new MoveGenerator(new MoveFactory());
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
            Assert.AreEqual(true, moves.Contains(new NormalMove('B', 'c', 2, 'd', 3, '.')), "c2d3. missing");
            Assert.AreEqual(true, moves.Contains(new NormalMove('B', 'c', 2, 'd', 1, '.')), "c2d1. missing");
            Assert.AreEqual(true, moves.Contains(new NormalMove('B', 'c', 2, 'b', 1, '.')), "c2b1. missing");
            Assert.AreEqual(true, moves.Contains(new NormalMove('B', 'c', 2, 'b', 3, '.')), "c2b3. missing");
            Assert.AreEqual(true, moves.Contains(new NormalMove('B', 'c', 2, 'a', 4, 'r')), "c2a4r missing");
        }

        [TestMethod]
        public void GetMoves_WhenKing_ThenAllMoves() // castling not included ! todo!
        {
            MoveGenerator target = new MoveGenerator(new MoveFactory());
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
            Assert.AreEqual(true, moves.Contains(new NormalMove('K', 'b', 1, 'b', 2, 'r')), "b1b2r missing");
            Assert.AreEqual(true, moves.Contains(new NormalMove('K', 'b', 1, 'a', 1, '.')), "b1a1. missing");
            Assert.AreEqual(true, moves.Contains(new NormalMove('K', 'b', 1, 'a', 2, '.')), "b1a2. missing");
        }

        //
        // Pawn moves
        // 

        [TestMethod]
        public void GetMoves_WhenWhitePawn_ThenAllMoves() // en passant not included
        {
            MoveGenerator target = new MoveGenerator(new MoveFactory());
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
            Assert.AreEqual(true, moves.Contains(new NormalMove('P', 'c', 2, 'c', 3, '.')), "c2c3. missing");
            Assert.AreEqual(true, moves.Contains(new NormalMove('P', 'c', 2, 'c', 4, '.')), "c2c4. missing");
            Assert.AreEqual(true, moves.Contains(new NormalMove('P', 'c', 2, 'b', 3, 'r')), "c2b3r missing");
            Assert.AreEqual(true, moves.Contains(new NormalMove('P', 'c', 2, 'd', 3, 'b')), "c2d3b missing");
        }

        [TestMethod]
        public void GetMoves_WhenWhitePawnBlocked_ThenAllMoves() // en passant not included
        {
            MoveGenerator target = new MoveGenerator(new MoveFactory());
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
            Assert.AreEqual(true, moves.Contains(new NormalMove('P', 'c', 2, 'c', 3, '.')), "c2c3. missing");
            Assert.AreEqual(true, moves.Contains(new NormalMove('P', 'c', 2, 'b', 3, 'r')), "c2b3r missing");
            Assert.AreEqual(true, moves.Contains(new NormalMove('P', 'c', 2, 'd', 3, 'b')), "c2d3b missing");
        }

        [TestMethod]
        public void GetMoves_WhenWhitePawnBlockedInMiddleOfBoard_ThenAllMoves()
        {
            MoveGenerator target = new MoveGenerator(new MoveFactory());
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
            MoveGenerator target = new MoveGenerator(new MoveFactory());
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
            MoveGenerator target = new MoveGenerator(new MoveFactory());
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
            board.Move("a7a5");
            Assert.AreEqual(Helper.FileCharToFile('a'), board.History.LastEnPassantFile);
            Assert.AreEqual(6, board.History.LastEnPassantRank);

            var moves = target.GetMoves(board, Helper.FileCharToFile('b'), 5); // white pawn

            Assert.AreEqual(2, moves.Count);
            Assert.AreEqual(true, moves.Contains(new EnPassantCaptureMove('P', 'b', 5, 'a', 6, 'p')), "b5a6pe en passant missing");
            Assert.AreEqual(true, moves.Contains(new NormalMove('P', 'b', 5, 'b', 6, '.')), "b5b6. missing");
        }

        [TestMethod]
        public void GetMoves_WhenBlackPawn_ThenAllMoves()
        {
            MoveGenerator target = new MoveGenerator(new MoveFactory());
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
            Assert.AreEqual(true, moves.Contains(new NormalMove('p', 'c', 7, 'c', 6, '.')), "c7c6. missing");
            Assert.AreEqual(true, moves.Contains(new NormalMove('p', 'c', 7, 'c', 5, '.')), "c7c5. missing");
            Assert.AreEqual(true, moves.Contains(new NormalMove('p', 'c', 7, 'b', 6, 'R')), "c7b6R missing");
            Assert.AreEqual(true, moves.Contains(new NormalMove('p', 'c', 7, 'd', 6, 'B')), "c7d6B missing");
        }

        [TestMethod]
        public void GetMoves_WhenBlackPawnAta4_ThenAllMoves()
        {
            MoveGenerator target = new MoveGenerator(new MoveFactory());
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
            Assert.AreEqual(true, moves.Contains(new NormalMove('p', 'b', 5, 'b', 4 ,'.')), "b3b4 missing");
        }

        // ----------------------------------------------------------------------------------------------------
        // Get All Moves Tests
        // ----------------------------------------------------------------------------------------------------

        [TestMethod]
        public void GetAllMoves_WhenTwoPieces_ThenShowMovesOfBothPieces()
        {
            MoveGenerator target = new MoveGenerator(new MoveFactory());
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
            Assert.AreEqual(true, moves.Contains(new NormalMove('P', 'e', 2, 'e', 3, '.')), "e2e3. missing");
            Assert.AreEqual(true, moves.Contains(new NormalMove('P', 'e', 2, 'e', 4, '.')), "e2e4. missing");
            Assert.AreEqual(true, moves.Contains(new NormalMove('K', 'e', 1, 'd', 1, '.')), "e1d1. missing");
            Assert.AreEqual(true, moves.Contains(new NormalMove('K', 'e', 1, 'd', 2, '.')), "e1d2. missing");
            Assert.AreEqual(true, moves.Contains(new NormalMove('K', 'e', 1, 'f', 2, '.')), "e1f2. missing");
            Assert.AreEqual(true, moves.Contains(new NormalMove('K', 'e', 1, 'f', 1, '.')), "e1f1. missing");
        }

        [TestMethod]
        [Ignore]
        public void GetAllMoves_WhenCheck_ThenKingMustEscapeCheck()
        {
            MoveGenerator target = new MoveGenerator(new MoveFactory());
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
            Assert.AreEqual(true, moves.Contains(new NormalMove('K', 'h', 1, 'g', 1, '.')), "h1g1. missing");
        }

        // ----------------------------------------------------------------------------------------------------
        // Is Move Valid Tests
        // ----------------------------------------------------------------------------------------------------

        [TestMethod]
        public void IsMoveValidTest_WhenValidMoveWhite_ThenTrue()
        {
            MoveGenerator target = new MoveGenerator(new MoveFactory());
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
            
            bool valid = target.IsMoveValid(board, new NormalMove('P', 'c', 2, 'c', 3, '.')); // pawn
            Assert.AreEqual(true, valid, "Move should be valid.");
        }

        [TestMethod]
        public void IsMoveValidTest_WhenInvalidMoveWhite_ThenFalse()
        {
            MoveGenerator target = new MoveGenerator(new MoveFactory());
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
            
            bool valid = target.IsMoveValid(board, new NormalMove('P', 'c', 2, 'c', 4, '.')); // pawn
            Assert.AreEqual(false, valid, "Move should be invalid.");
        }

        [TestMethod]
        public void IsMoveValidTest_WhenValidMoveBlack_ThenTrue()
        {
            MoveGenerator target = new MoveGenerator(new MoveFactory());
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
            
            bool valid = target.IsMoveValid(board, new NormalMove('p', 'c', 7, 'c', 5, '.')); // pawn
            Assert.AreEqual(true, valid, "Move should be valid.");

            valid = target.IsMoveValid(board, new NormalMove('p', 'c', 7, 'c', 6, '.')); // pawn
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

            var target = new MoveGenerator(new MoveFactory());
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

            var target = new MoveGenerator(new MoveFactory());
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

            var target = new MoveGenerator(new MoveFactory());
            target.GetEndPosition(Helper.FileCharToFile('a'), 1, knightSequence, out targetFile, out targetRank, out valid);

            Assert.AreEqual(false, valid);
        }

        [TestMethod]
        public void GetCorrectMoveTest_Whene2e4_ThenAddDot()
        {
            var factory = new MoveFactory();
            MoveGenerator target = new MoveGenerator(factory);
            Board board = new Board(target);
            board.SetInitialPosition();
            
            MoveBase actualMove = factory.GetCorrectMove(board, "e2e4");
            Assert.AreEqual("e2e4.", actualMove.ToString());
        }

        [TestMethod]
        public void GetCorrectMoveTest_WhenCaptureNormal_ThenAddCapturedPiece()
        {
            var factory = new MoveFactory();
            MoveGenerator target = new MoveGenerator(factory);
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

            MoveBase actualMove = factory.GetCorrectMove(board, "d5c6");
            Assert.AreEqual("d5c6p", actualMove.ToString());
        }

        [TestMethod]
        public void GetCorrectMoveTest_WhenCaptureEnPassant_ThenAddCapturedPiece()
        {
            var factory = new MoveFactory();
            MoveGenerator target = new MoveGenerator(factory);
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
            board.Move("c7c5");

            MoveBase actualMove = factory.GetCorrectMove(board, "d5c6");
            Assert.AreEqual("d5c6pe", actualMove.ToString());
        }

        // ------------------------------------------------------------------
        // Castling tests
        // ------------------------------------------------------------------

        // white

        [TestMethod]
        public void GetMovesTest_WhenCastlingRightOk_ThenCastling()
        {
            MoveGenerator generator = new MoveGenerator(new MoveFactory());
            Board board = new Board(generator);
            string position = "r...k..r" +
                              "p......." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "P......." +
                              "R...K..R";
            board.SetPosition(position);

            List<MoveBase> kingMoves = generator.GetMoves(board, Helper.FileCharToFile('e'), 1);
            Assert.AreEqual(true, kingMoves.Contains(new NormalMove('K', 'e', 1, 'g', 1, '.')), "e1g1. 0-0 castling missing");
            Assert.AreEqual(true, kingMoves.Contains(new NormalMove('K', 'e', 1, 'c', 1, '.')), "e1c1. 0-0-0 castling missing");
        }

        [TestMethod]
        public void GetMovesTest_WhenCastlingBlocked_ThenNoCastling()
        {
            MoveGenerator generator = new MoveGenerator(new MoveFactory());
            Board board = new Board(generator);
            string position = "r...k..r" +
                              "p......." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "P......R" +
                              "...QK...";
            board.SetPosition(position);

            List<MoveBase> kingMoves = generator.GetMoves(board, Helper.FileCharToFile('e'), 1);
            Assert.AreEqual(false, kingMoves.Contains(new NormalMove('K', 'e', 1, 'g', 1, '.')), "e1g1. 0-0 castling not possible");
            Assert.AreEqual(false, kingMoves.Contains(new NormalMove('K', 'e', 1, 'c', 1, '.')), "e1c1. 0-0-0 castling not possible");
        }

        [TestMethod]
        public void GetMovesTest_WhenCastlingBlockedKingSide_ThenNoCastling()
        {
            MoveGenerator generator = new MoveGenerator(new MoveFactory());
            Board board = new Board(generator);
            string position = "r...k..r" +
                              "p......." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "P......." +
                              "R..QK.NR";
            board.SetPosition(position);

            List<MoveBase> kingMoves = generator.GetMoves(board, Helper.FileCharToFile('e'), 1);
            Assert.AreEqual(false, kingMoves.Contains(new NormalMove('K', 'e', 1, 'g', 1, '.')), "e1g1. 0-0 castling not possible");
            Assert.AreEqual(false, kingMoves.Contains(new NormalMove('K', 'e', 1, 'c', 1, '.')), "e1c1. 0-0-0 castling not possible");
        }

        [TestMethod]
        public void GetMovesTest_WhenFieldNextToKingIsAttacked_ThenNoCastling()
        {
            MoveGenerator generator = new MoveGenerator(new MoveFactory());
            Board board = new Board(generator);
            string position = "....k..." +
                              "p..r.r.." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "P......." +
                              "R...K..R";
            board.SetPosition(position);
            board.Move("e8e7");

            List<MoveBase> kingMoves = generator.GetMoves(board, Helper.FileCharToFile('e'), 1);
            Assert.AreEqual(false, kingMoves.Contains(new NormalMove('K', 'e', 1, 'g', 1, '.')), "e1g1. 0-0 castling not possible. f1 is attacked.");
            Assert.AreEqual(false, kingMoves.Contains(new NormalMove('K', 'e', 1, 'c', 1, '.')), "e1c1. 0-0-0 castling not possible. d1 is attacked.");
        }

        // black

        [TestMethod]
        public void GetMovesTest_WhenCastlingRightOk_ThenCastling_Black()
        {
            MoveGenerator generator = new MoveGenerator(new MoveFactory());
            Board board = new Board(generator);
            string position = "r...k..r" +
                              "p......." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "P......." +
                              "R...K..R";
            board.SetPosition(position);

            List<MoveBase> kingMoves = generator.GetMoves(board, Helper.FileCharToFile('e'), 8);
            Assert.AreEqual(true, kingMoves.Contains(new NormalMove('k', 'e', 8, 'g', 8, '.')), "e8g8. 0-0 castling missing");
            Assert.AreEqual(true, kingMoves.Contains(new NormalMove('k', 'e', 8, 'c', 8, '.')), "e8c8. 0-0-0 castling missing");
        }

        [TestMethod]
        public void GetMovesTest_WhenCastlingBlocked_ThenNoCastling_Black()
        {
            MoveGenerator generator = new MoveGenerator(new MoveFactory());
            Board board = new Board(generator);
            string position = "r..k...r" + // king moved!
                              "p......." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "P......R" +
                              "...QK...";
            board.SetPosition(position);

            List<MoveBase> kingMoves = generator.GetMoves(board, Helper.FileCharToFile('e'), 8);
            Assert.AreEqual(false, kingMoves.Contains(new NormalMove('k', 'e', 8, 'g', 8, '.')), "e8g8. 0-0 castling not possible");
            Assert.AreEqual(false, kingMoves.Contains(new NormalMove('k', 'e', 8, 'c', 8, '.')), "e8c8. 0-0-0 castling not possible");
        }

        [TestMethod]
        public void GetMovesTest_WhenCastlingBlockedKingSide_ThenNoCastling_Black()
        {
            MoveGenerator generator = new MoveGenerator(new MoveFactory());
            Board board = new Board(generator);
            string position = "r.b.k.nr" +
                              "p......." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "P......." +
                              "R..QK.NR";
            board.SetPosition(position);

            List<MoveBase> kingMoves = generator.GetMoves(board, Helper.FileCharToFile('e'), 8);
            Assert.AreEqual(false, kingMoves.Contains(new NormalMove('k', 'e', 8, 'g', 8, '.')), "e8g8. 0-0 castling not possible");
            Assert.AreEqual(false, kingMoves.Contains(new NormalMove('k', 'e', 8, 'c', 8, '.')), "e8c8. 0-0-0 castling not possible");
        }

        [TestMethod]
        public void GetMovesTest_WhenFieldNextToKingAttacked_ThenNoCastling_Black()
        {
            MoveGenerator generator = new MoveGenerator(new MoveFactory());
            Board board = new Board(generator);
            string position = "r...k..r" +
                              "p......." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "P......." +
                              "...RKR..";
            board.SetPosition(position);

            List<MoveBase> kingMoves = generator.GetMoves(board, Helper.FileCharToFile('e'), 8);
            Assert.AreEqual(false, kingMoves.Contains(new NormalMove('k', 'e', 8, 'g', 8, '.')), "e8g8. 0-0 castling not possible. F8 attacked");
            Assert.AreEqual(false, kingMoves.Contains(new NormalMove('k', 'e', 8, 'c', 8, '.')), "e8c8. 0-0-0 castling not possible. D8 attacked");
        }
    }
}