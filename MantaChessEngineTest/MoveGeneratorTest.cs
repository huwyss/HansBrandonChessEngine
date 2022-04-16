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
            Board initBoard = new Board();
            initBoard.SetInitialPosition();

            var knight = new Knight(Definitions.ChessColor.White);
            var moves = knight.GetMoves(null, initBoard, Helper.FileCharToFile('b'), 1, true); // get moves of knight

            Assert.AreEqual(2, moves.Count);
            Assert.AreEqual("b1a3.", moves[0].ToString());
            Assert.AreEqual("b1c3.", moves[1].ToString());
        }

        [TestMethod]
        public void GetMoves_WhenRook_ThenAllMoves()
        {
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

            var rook = new Rook(Definitions.ChessColor.White);
            var moves = rook.GetMoves(null, board, Helper.FileCharToFile('e'), 4, true); // get moves of Rook

            Assert.AreEqual(11, moves.Count);
            Assert.AreEqual(true, moves.Contains(new NormalMove(Piece.MakePiece('R'), 'e', 4, 'e', 5, null)), "e4e5. missing");
            Assert.AreEqual(true, moves.Contains(new NormalMove(Piece.MakePiece('R'), 'e', 4, 'e', 6, Piece.MakePiece('p'))), "e4e6p missing"); // capture pawn
            Assert.AreEqual(true, moves.Contains(new NormalMove(Piece.MakePiece('R'), 'e', 4, 'f', 4, null)), "e4f4. missing");
            Assert.AreEqual(true, moves.Contains(new NormalMove(Piece.MakePiece('R'), 'e', 4, 'g', 4, null)), "e4g4. missing");
            Assert.AreEqual(true, moves.Contains(new NormalMove(Piece.MakePiece('R'), 'e', 4, 'e', 3, null)), "e4e3. missing");
            Assert.AreEqual(true, moves.Contains(new NormalMove(Piece.MakePiece('R'), 'e', 4, 'e', 2, null)), "e4e2. missing");
            Assert.AreEqual(true, moves.Contains(new NormalMove(Piece.MakePiece('R'), 'e', 4, 'e', 1, null)), "e4e1. missing");
            Assert.AreEqual(true, moves.Contains(new NormalMove(Piece.MakePiece('R'), 'e', 4, 'd', 4, null)), "e4d4. missing");
            Assert.AreEqual(true, moves.Contains(new NormalMove(Piece.MakePiece('R'), 'e', 4, 'c', 4, null)), "e4c4. missing");
            Assert.AreEqual(true, moves.Contains(new NormalMove(Piece.MakePiece('R'), 'e', 4, 'b', 4, null)), "e4b4. missing");
            Assert.AreEqual(true, moves.Contains(new NormalMove(Piece.MakePiece('R'), 'e', 4, 'a', 4, null)), "e4a4. missing");
        }

        [TestMethod]
        public void GetMoves_WhenQueen_ThenAllMoves()
        {
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

            var queen = new Queen(Definitions.ChessColor.White);
            var moves = queen.GetMoves(null, board, Helper.FileCharToFile('b'), 2, true); // get moves of queen

            Assert.AreEqual(9, moves.Count);
            Assert.AreEqual(true, moves.Contains(new NormalMove(Piece.MakePiece('Q'), 'b', 2, 'b', 3, null)), "b2b3. missing");
            Assert.AreEqual(true, moves.Contains(new NormalMove(Piece.MakePiece('Q'), 'b', 2, 'b', 4, Piece.MakePiece('r'))), "b2b4r missing");
            Assert.AreEqual(true, moves.Contains(new NormalMove(Piece.MakePiece('Q'), 'b', 2, 'c', 3, null)), "b2c3. missing");
            Assert.AreEqual(true, moves.Contains(new NormalMove(Piece.MakePiece('Q'), 'b', 2, 'c', 2, null)), "b2c2. missing");
            Assert.AreEqual(true, moves.Contains(new NormalMove(Piece.MakePiece('Q'), 'b', 2, 'c', 1, null)), "b2c1. missing");
            Assert.AreEqual(true, moves.Contains(new NormalMove(Piece.MakePiece('Q'), 'b', 2, 'b', 1, null)), "b2b1. missing");
            Assert.AreEqual(true, moves.Contains(new NormalMove(Piece.MakePiece('Q'), 'b', 2, 'a', 1, null)), "b2a1. missing");
            Assert.AreEqual(true, moves.Contains(new NormalMove(Piece.MakePiece('Q'), 'b', 2, 'a', 2, null)), "b2a2. missing");
            Assert.AreEqual(true, moves.Contains(new NormalMove(Piece.MakePiece('Q'), 'b', 2, 'a', 3, null)), "b2a3. missing");
        }

        [TestMethod]
        public void GetMoves_WhenBishop_ThenAllMoves()
        {
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

            var bishop = new Bishop(Definitions.ChessColor.White);
            var moves = bishop.GetMoves(null, board, Helper.FileCharToFile('c'), 2, true); // get moves of bishop

            Assert.AreEqual(5, moves.Count);
            Assert.AreEqual(true, moves.Contains(new NormalMove(Piece.MakePiece('B'), 'c', 2, 'd', 3, null)), "c2d3. missing");
            Assert.AreEqual(true, moves.Contains(new NormalMove(Piece.MakePiece('B'), 'c', 2, 'd', 1, null)), "c2d1. missing");
            Assert.AreEqual(true, moves.Contains(new NormalMove(Piece.MakePiece('B'), 'c', 2, 'b', 1, null)), "c2b1. missing");
            Assert.AreEqual(true, moves.Contains(new NormalMove(Piece.MakePiece('B'), 'c', 2, 'b', 3, null)), "c2b3. missing");
            Assert.AreEqual(true, moves.Contains(new NormalMove(Piece.MakePiece('B'), 'c', 2, 'a', 4, Piece.MakePiece('r'))), "c2a4r missing");
        }

        [TestMethod]
        public void GetMoves_WhenKing_ThenAllMoves()
        {
            MoveGenerator target = new MoveGenerator(new MoveFactory());
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

            var king = new King(Definitions.ChessColor.White);
            var moves = king.GetMoves(target, board, Helper.FileCharToFile('b'), 1, true); // king

            Assert.AreEqual(3, moves.Count);
            Assert.AreEqual(true, moves.Contains(new NormalMove(Piece.MakePiece('K'), 'b', 1, 'b', 2, Piece.MakePiece('r'))), "b1b2r missing");
            Assert.AreEqual(true, moves.Contains(new NormalMove(Piece.MakePiece('K'), 'b', 1, 'a', 1, null)), "b1a1. missing");
            Assert.AreEqual(true, moves.Contains(new NormalMove(Piece.MakePiece('K'), 'b', 1, 'a', 2, null)), "b1a2. missing");
        }

        //
        // Pawn moves
        // 

        [TestMethod]
        public void GetMoves_WhenWhitePawn_ThenAllMoves() // en passant not included
        {
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

            var pawn = new Pawn(Definitions.ChessColor.White);
            var moves = pawn.GetMoves(null, board, Helper.FileCharToFile('c'), 2, true); // pawn

            Assert.AreEqual(4, moves.Count);
            Assert.AreEqual(true, moves.Contains(new NormalMove(Piece.MakePiece('P'), 'c', 2, 'c', 3, null)), "c2c3. missing");
            Assert.AreEqual(true, moves.Contains(new NormalMove(Piece.MakePiece('P'), 'c', 2, 'c', 4, null)), "c2c4. missing");
            Assert.AreEqual(true, moves.Contains(new NormalMove(Piece.MakePiece('P'), 'c', 2, 'b', 3, Piece.MakePiece('r'))), "c2b3r missing");
            Assert.AreEqual(true, moves.Contains(new NormalMove(Piece.MakePiece('P'), 'c', 2, 'd', 3, Piece.MakePiece('b'))), "c2d3b missing");
        }

        [TestMethod]
        public void GetMoves_WhenWhitePawnBlocked_ThenAllMoves() // en passant not included
        {
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

            var pawn = new Pawn(Definitions.ChessColor.White);
            var moves = pawn.GetMoves(null, board, Helper.FileCharToFile('c'), 2, true); // pawn

            Assert.AreEqual(3, moves.Count);
            Assert.AreEqual(true, moves.Contains(new NormalMove(Piece.MakePiece('P'), 'c', 2, 'c', 3, null)), "c2c3. missing");
            Assert.AreEqual(true, moves.Contains(new NormalMove(Piece.MakePiece('P'), 'c', 2, 'b', 3, Piece.MakePiece('r'))), "c2b3r missing");
            Assert.AreEqual(true, moves.Contains(new NormalMove(Piece.MakePiece('P'), 'c', 2, 'd', 3, Piece.MakePiece('b'))), "c2d3b missing");
        }

        [TestMethod]
        public void GetMoves_WhenWhitePawnBlockedInMiddleOfBoard_ThenAllMoves()
        {
            Board board = new Board();
            string position = "........" +
                              "........" + 
                              "..p....." + // black pawn
                              "..P....." + // white pawn must not do c5-c7 !
                              "........" +
                              "........" +
                              "........" +
                              "........";
            board.SetPosition(position);

            var pawn = new Pawn(Definitions.ChessColor.White);
            var moves = pawn.GetMoves(null, board, Helper.FileCharToFile('c'), 5, true); // white pawn

            Assert.AreEqual(0, moves.Count);
        }

        [TestMethod]
        public void GetMoves_WhenWhitePawnAtA5_ThenAllMoves()
        {
            Board board = new Board();
            string position = "........" +
                              "........" + 
                              "........" + 
                              "........" +
                              "........" +
                              "..p....." + // black pawn
                              "..P....." + // white pawn must not do c2-c4 !
                              "........";
            board.SetPosition(position);

            var pawn = new Pawn(Definitions.ChessColor.White);
            var moves = pawn.GetMoves(null, board, Helper.FileCharToFile('c'), 2, true); // white pawn

            Assert.AreEqual(0, moves.Count);
        }

        [TestMethod]
        public void GetMoves_WhenWhiteCanCaptureEnPassant_ThenListEnPassant()
        {
            Board board = new Board();
            string position = ".......k" +
                              "p......." +
                              "........" +
                              ".P......" +
                              "........" +
                              "........" + 
                              "........" + 
                              "...K....";
            board.SetPosition(position);
            board.Move(new NormalMove(new Pawn(Definitions.ChessColor.Black),'a',7,'a',5,null));
            Assert.AreEqual(Helper.FileCharToFile('a'), board.History.LastEnPassantFile);
            Assert.AreEqual(6, board.History.LastEnPassantRank);

            var pawn = new Pawn(Definitions.ChessColor.White);
            var moves = pawn.GetMoves(null, board, Helper.FileCharToFile('b'), 5, true); // white pawn

            Assert.AreEqual(2, moves.Count);
            Assert.AreEqual(true, moves.Contains(new EnPassantCaptureMove(Piece.MakePiece('P'), 'b', 5, 'a', 6, Piece.MakePiece('p'))), "b5a6pe en passant missing");
            Assert.AreEqual(true, moves.Contains(new NormalMove(Piece.MakePiece('P'), 'b', 5, 'b', 6, null)), "b5b6. missing");
        }

        [TestMethod]
        public void GetMoves_WhenBlackPawn_ThenAllMoves()
        {
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

            var pawn = new Pawn(Definitions.ChessColor.Black);
            var moves = pawn.GetMoves(null, board, Helper.FileCharToFile('c'), 7, true); // black pawn

            Assert.AreEqual(4, moves.Count);
            Assert.AreEqual(true, moves.Contains(new NormalMove(Piece.MakePiece('p'), 'c', 7, 'c', 6, null)), "c7c6. missing");
            Assert.AreEqual(true, moves.Contains(new NormalMove(Piece.MakePiece('p'), 'c', 7, 'c', 5, null)), "c7c5. missing");
            Assert.AreEqual(true, moves.Contains(new NormalMove(Piece.MakePiece('p'), 'c', 7, 'b', 6, Piece.MakePiece('R'))), "c7b6R missing");
            Assert.AreEqual(true, moves.Contains(new NormalMove(Piece.MakePiece('p'), 'c', 7, 'd', 6, Piece.MakePiece('B'))), "c7d6B missing");
        }

        [TestMethod]
        public void GetMoves_WhenBlackPawnAta4_ThenAllMoves()
        {
            Board board = new Board();
            string position = "........" +
                              "........" +
                              "........" +
                              ".p......" + // black pawn must not do b5-b3 !
                              "........" + 
                              "........" +
                              "........" +
                              "........";
            board.SetPosition(position);

            var pawn = new Pawn(Definitions.ChessColor.Black);
            var moves = pawn.GetMoves(null, board, Helper.FileCharToFile('b'), 5, true); // black pawn

            Assert.AreEqual(1, moves.Count);
            Assert.AreEqual(true, moves.Contains(new NormalMove(Piece.MakePiece('p'), 'b', 5, 'b', 4 ,null)), "b3b4 missing");
        }

        [TestMethod]
        public void GetMoves_WhenWhitePawnAt8Straight_ThenPromoted()
        {
            Board board = new Board();
            string position = "........" +
                              ".P......" + // white pawn gets promoted to queen, rook, bishop or knight
                              "........" +
                              "........" + 
                              "........" +
                              "........" +
                              "........" +
                              "........";
            board.SetPosition(position);

            var pawn = new Pawn(Definitions.ChessColor.White);
            var moves = pawn.GetMoves(null, board, Helper.FileCharToFile('b'), 7, true); // white pawn

            Assert.AreEqual(4, moves.Count);
            Assert.AreEqual(true, moves.Contains(new PromotionMove(Piece.MakePiece('P'), 'b', 7, 'b', 8, null, Definitions.QUEEN)), "b7b8 missing (promotion to queen walking straight)");
            Assert.AreEqual(true, moves.Contains(new PromotionMove(Piece.MakePiece('P'), 'b', 7, 'b', 8, null, Definitions.ROOK)), "b7b8 missing (promotion to rook walking straight)");
            Assert.AreEqual(true, moves.Contains(new PromotionMove(Piece.MakePiece('P'), 'b', 7, 'b', 8, null, Definitions.BISHOP)), "b7b8 missing (promotion to bishop walking straight)");
            Assert.AreEqual(true, moves.Contains(new PromotionMove(Piece.MakePiece('P'), 'b', 7, 'b', 8, null, Definitions.KNIGHT)), "b7b8 missing (promotion to knight walking straight)");
        }

        [TestMethod]
        public void GetMoves_WhenBlackPawnAt1Straight_ThenPromoted()
        {
            Board board = new Board();
            string position = "........" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              ".p......" +  // black pawn gets promoted to queen, rook, bishop or knight
                              "........";
            board.SetPosition(position);

            var pawn = new Pawn(Definitions.ChessColor.Black);
            var moves = pawn.GetMoves(null, board, Helper.FileCharToFile('b'), 2, true); // black pawn

            Assert.AreEqual(4, moves.Count);
            Assert.AreEqual(true, moves.Contains(new PromotionMove(Piece.MakePiece('p'), 'b', 2, 'b', 1, null, Definitions.QUEEN)), "b2b1 missing (promotion to queen walking straight)");
            Assert.AreEqual(true, moves.Contains(new PromotionMove(Piece.MakePiece('p'), 'b', 2, 'b', 1, null, Definitions.ROOK)), "b2b1 missing (promotion to rook walking straight)");
            Assert.AreEqual(true, moves.Contains(new PromotionMove(Piece.MakePiece('p'), 'b', 2, 'b', 1, null, Definitions.BISHOP)), "b2b1 missing (promotion to bishop walking straight)");
            Assert.AreEqual(true, moves.Contains(new PromotionMove(Piece.MakePiece('p'), 'b', 2, 'b', 1, null, Definitions.KNIGHT)), "b2b1 missing (promotion to knight walking straight)");
        }

        [TestMethod]
        public void GetMoves_WhenWhitePawnAt8Capturing_ThenPromoted()
        {
            Board board = new Board();
            string position = "rN......" +
                              ".P......" + // white pawn gets promoted to queen, rook, bishop or knight
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "........";
            board.SetPosition(position);

            var pawn = new Pawn(Definitions.ChessColor.White);
            var moves = pawn.GetMoves(null, board, Helper.FileCharToFile('b'), 7, true); // white pawn

            Assert.AreEqual(4, moves.Count);
            Assert.AreEqual(true, moves.Contains(new PromotionMove(Piece.MakePiece('P'), 'b', 7, 'a', 8, Piece.MakePiece('r'), Definitions.QUEEN)), "b7a8 missing (promotion to queen when capturing)");
            Assert.AreEqual(true, moves.Contains(new PromotionMove(Piece.MakePiece('P'), 'b', 7, 'a', 8, Piece.MakePiece('r'), Definitions.ROOK)), "b7a8 missing (promotion to rook when capturing)");
            Assert.AreEqual(true, moves.Contains(new PromotionMove(Piece.MakePiece('P'), 'b', 7, 'a', 8, Piece.MakePiece('r'), Definitions.BISHOP)), "b7a8 missing (promotion to bishop when capturing)");
            Assert.AreEqual(true, moves.Contains(new PromotionMove(Piece.MakePiece('P'), 'b', 7, 'a', 8, Piece.MakePiece('r'), Definitions.KNIGHT)), "b7a8 missing (promotion to knight when capturing)");
        }

        [TestMethod]
        public void GetMoves_WhenBlackPawnAt1Capturing_ThenPromoted()
        {
            Board board = new Board();
            string position = "........" +
                              "........" + // white pawn gets promoted to queen, rook, bishop or knight
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              ".p......" +
                              "Rn......";
            board.SetPosition(position);

            var pawn = new Pawn(Definitions.ChessColor.Black);
            var moves = pawn.GetMoves(null, board, Helper.FileCharToFile('b'), 2, true); // black pawn

            Assert.AreEqual(4, moves.Count);
            Assert.AreEqual(true, moves.Contains(new PromotionMove(Piece.MakePiece('p'), 'b', 2, 'a', 1, Piece.MakePiece('R'), Definitions.QUEEN)), "b2a1 missing (promotion to queen when capturing)");
            Assert.AreEqual(true, moves.Contains(new PromotionMove(Piece.MakePiece('p'), 'b', 2, 'a', 1, Piece.MakePiece('R'), Definitions.ROOK)), "b2a1 missing (promotion to rook when capturing)");
            Assert.AreEqual(true, moves.Contains(new PromotionMove(Piece.MakePiece('p'), 'b', 2, 'a', 1, Piece.MakePiece('R'), Definitions.BISHOP)), "b2a1 missing (promotion to bishop when capturing)");
            Assert.AreEqual(true, moves.Contains(new PromotionMove(Piece.MakePiece('p'), 'b', 2, 'a', 1, Piece.MakePiece('R'), Definitions.KNIGHT)), "b2a1 missing (promotion to knight when capturing)");
        }

        // ----------------------------------------------------------------------------------------------------
        // Get All Moves Tests
        // ----------------------------------------------------------------------------------------------------

        [TestMethod]
        public void GetAllMoves_WhenTwoPieces_ThenShowMovesOfBothPieces()
        {
            MoveGenerator target = new MoveGenerator(new MoveFactory());
            Board board = new Board();
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
            Assert.AreEqual(true, moves.Contains(new NormalMove(Piece.MakePiece('P'), 'e', 2, 'e', 3, null)), "e2e3. missing");
            Assert.AreEqual(true, moves.Contains(new NormalMove(Piece.MakePiece('P'), 'e', 2, 'e', 4, null)), "e2e4. missing");
            Assert.AreEqual(true, moves.Contains(new NormalMove(Piece.MakePiece('K'), 'e', 1, 'd', 1, null)), "e1d1. missing");
            Assert.AreEqual(true, moves.Contains(new NormalMove(Piece.MakePiece('K'), 'e', 1, 'd', 2, null)), "e1d2. missing");
            Assert.AreEqual(true, moves.Contains(new NormalMove(Piece.MakePiece('K'), 'e', 1, 'f', 2, null)), "e1f2. missing");
            Assert.AreEqual(true, moves.Contains(new NormalMove(Piece.MakePiece('K'), 'e', 1, 'f', 1, null)), "e1f1. missing");
        }

        [TestMethod]
        public void GetAllMoves_WhenNoWhiteKing_ThenNoMovesReturned_White()
        {
            MoveGenerator target = new MoveGenerator(new MoveFactory());
            Board board = new Board();
            string position = "........" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              ".......k" +
                              ".......r" +
                              ".......R"; // No white King
            board.SetPosition(position);

            var moves = target.GetAllMoves(board, Definitions.ChessColor.White);

            Assert.AreEqual(0, moves.Count, "white has no king. so no white moves possible.");
        }
        
        // ----------------------------------------------------------------------------------------------------
        // Is Move Valid Tests
        // ----------------------------------------------------------------------------------------------------

        [TestMethod]
        public void IsMoveValidTest_WhenValidMoveWhite_ThenTrue()
        {
            MoveGenerator target = new MoveGenerator(new MoveFactory());
            Board board = new Board();
            string position = ".......k" +
                              "........" +
                              "........" +
                              "........" +
                              "..n....." +
                              ".r.b...." +
                              "..P....." +
                              ".......K";
            board.SetPosition(position);
            
            bool valid = target.IsMoveValid(board, new NormalMove(Piece.MakePiece('P'), 'c', 2, 'c', 3, null)); // pawn
            Assert.AreEqual(true, valid, "Move should be valid.");
        }

        [TestMethod]
        public void IsMoveValidTest_WhenInvalidMoveWhite_ThenFalse()
        {
            MoveGenerator target = new MoveGenerator(new MoveFactory());
            Board board = new Board();
            string position = ".......k" +
                              "........" +
                              "........" +
                              "........" +
                              "..n....." +
                              ".r.b...." +
                              "..P....." +
                              ".......K";
            board.SetPosition(position);
            
            bool valid = target.IsMoveValid(board, new NormalMove(Piece.MakePiece('P'), 'c', 2, 'c', 4, null)); // pawn
            Assert.AreEqual(false, valid, "Move should be invalid.");
        }

        [TestMethod]
        public void IsMoveValidTest_WhenValidMoveBlack_ThenTrue()
        {
            MoveGenerator target = new MoveGenerator(new MoveFactory());
            Board board = new Board();
            string position = ".......k" +
                              "..p....." +
                              ".R.B...." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              ".......K";
            board.SetPosition(position);
            board.SideToMove = Definitions.ChessColor.Black;
            
            bool valid = target.IsMoveValid(board, new NormalMove(Piece.MakePiece('p'), 'c', 7, 'c', 5, null)); // pawn
            Assert.AreEqual(true, valid, "Move should be valid.");

            valid = target.IsMoveValid(board, new NormalMove(Piece.MakePiece('p'), 'c', 7, 'c', 6, null)); // pawn
            Assert.AreEqual(true, valid, "Move should be valid.");
        }

        [TestMethod]
        public void IsMoveValidTest_WhenWrongSideMoves_ThenFalse()
        {
            MoveGenerator target = new MoveGenerator(new MoveFactory());
            Board board = new Board();
            board.SetInitialPosition();

            bool valid = target.IsMoveValid(board, new NormalMove(Piece.MakePiece('p'), 'e', 7, 'e', 5, null)); // BLACK pawn
            Assert.AreEqual(false, valid, "This is white's move. Black must not move.");
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
            Board board = new Board();
            board.SetInitialPosition();

            IMove actualMove = factory.MakeMove(board, "e2e4");
            Assert.AreEqual("e2e4.", actualMove.ToString());
        }

        [TestMethod]
        public void GetCorrectMoveTest_WhenCaptureNormal_ThenAddCapturedPiece()
        {
            var factory = new MoveFactory();
            MoveGenerator target = new MoveGenerator(factory);
            Board board = new Board();
            string position = "...k...." +
                              "........" +
                              "..p....." +
                              "...P...." +
                              "........" +
                              "........" +
                              "........" +
                              "..K.....";
            board.SetPosition(position);

            IMove actualMove = factory.MakeMove(board, "d5c6");
            Assert.AreEqual("d5c6p", actualMove.ToString());
        }

        [TestMethod]
        public void GetCorrectMoveTest_WhenCaptureEnPassant_ThenAddCapturedPiece()
        {
            var factory = new MoveFactory();
            MoveGenerator target = new MoveGenerator(factory);
            Board board = new Board();
            string position = "...k...." +
                              "..p....." +
                              "........" +
                              "...P...." +
                              "........" +
                              "........" +
                              "........" +
                              "..K.....";
            board.SetPosition(position);
            board.Move(new NormalMove(Piece.MakePiece('p'),'c',7,'c',5,null));

            IMove actualMove = factory.MakeMove(board, "d5c6");
            Assert.AreEqual("d5c6pe", actualMove.ToString());
        }

        [TestMethod]
        public void GetCorrectMoveTest_WhenPromotion_ThenReturnPromotionMove()
        {
            var factory = new MoveFactory();
            MoveGenerator target = new MoveGenerator(factory);
            Board board = new Board();
            string position = ".......k" +
                              "P......." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "..K.....";
            board.SetPosition(position);

            IMove actualMove = factory.MakeMove(board, "a7a8");

            var expectedMove = new PromotionMove(Piece.MakePiece('P'), 'a', 7, 'a', 8, null, Definitions.QUEEN);
            Assert.AreEqual("a7a8.", actualMove.ToString());
            Assert.AreEqual(expectedMove, actualMove);
        }

        // ------------------------------------------------------------------
        // Castling tests
        // ------------------------------------------------------------------

        // white

        [TestMethod]
        public void GetMovesTest_WhenCastlingRightOk_ThenCastling()
        {
            MoveGenerator generator = new MoveGenerator(new MoveFactory());
            Board board = new Board();
            string position = "r...k..r" +
                              "p......." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "P......." +
                              "R...K..R";
            board.SetPosition(position);

            var king = new King(Definitions.ChessColor.White);
            var kingMoves = king.GetMoves(generator, board, Helper.FileCharToFile('e'), 1, true);

            Assert.AreEqual(true, kingMoves.Contains(new CastlingMove(CastlingType.WhiteKingSide, new King(Definitions.ChessColor.White))), "e1g1. 0-0 castling missing");
            Assert.AreEqual(true, kingMoves.Contains(new CastlingMove(CastlingType.WhiteQueenSide, new King(Definitions.ChessColor.White))), "e1c1. 0-0-0 castling missing");
        }

        [TestMethod]
        public void GetMovesTest_WhenKingMoved_ThenNoCastling()
        {
            MoveGenerator generator = new MoveGenerator(new MoveFactory());
            Board board = new Board();
            string position = "r...k..r" +
                              "p......." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "P......." +
                              "R..K...R";
            board.SetPosition(position);

            var king = new King(Definitions.ChessColor.White);
            List<IMove> kingMoves = king.GetMoves(generator, board, Helper.FileCharToFile('d'), 1, true);
            Assert.AreEqual(false, kingMoves.Contains(new CastlingMove(CastlingType.WhiteKingSide, new King(Definitions.ChessColor.White))), "e1g1. 0-0 castling not possible");
            Assert.AreEqual(false, kingMoves.Contains(new CastlingMove(CastlingType.WhiteQueenSide, new King(Definitions.ChessColor.White))), "e1c1. 0-0-0 castling not possible");
        }

        [TestMethod]
        public void GetMovesTest_WhenRookMoved_ThenNoCastling()
        {
            MoveGenerator generator = new MoveGenerator(new MoveFactory());
            Board board = new Board();
            string position = "r...k..r" +
                              "p......." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "P......R" +
                              ".R..K...";
            board.SetPosition(position);

            var king = new King(Definitions.ChessColor.White);
            List<IMove> kingMoves = king.GetMoves(generator, board, Helper.FileCharToFile('e'), 1, true);
            Assert.AreEqual(false, kingMoves.Contains(new CastlingMove(CastlingType.WhiteKingSide, new King(Definitions.ChessColor.White))), "e1g1. 0-0 castling not possible");
            Assert.AreEqual(false, kingMoves.Contains(new CastlingMove(CastlingType.WhiteQueenSide, new King(Definitions.ChessColor.White))), "e1c1. 0-0-0 castling not possible");
        }

        [TestMethod]
        public void GetMovesTest_WhenCastlingBlockedKingSide_ThenNoCastlingOnKingSide()
        {
            MoveGenerator generator = new MoveGenerator(new MoveFactory());
            Board board = new Board();
            string position = "r...k..r" +
                              "p......." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "P......." +
                              "R...K.NR";
            board.SetPosition(position);

            var king = new King(Definitions.ChessColor.White);
            List<IMove> kingMoves = king.GetMoves(generator, board, Helper.FileCharToFile('e'), 1, true);
            Assert.AreEqual(false, kingMoves.Contains(new CastlingMove(CastlingType.WhiteKingSide, new King(Definitions.ChessColor.White))), "e1g1. 0-0 castling not possible");
        }

        [TestMethod]
        public void GetMovesTest_WhenCastlingBlockedQueenSide_ThenNoCastlingOnQueenSide()
        {
            MoveGenerator generator = new MoveGenerator(new MoveFactory());
            Board board = new Board();
            string position = "r...k..r" +
                              "p......." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "P......." +
                              "R..QK..R";
            board.SetPosition(position);

            var king = new King(Definitions.ChessColor.White);
            List<IMove> kingMoves = king.GetMoves(generator, board, Helper.FileCharToFile('e'), 1, true);
            Assert.AreEqual(false, kingMoves.Contains(new CastlingMove(CastlingType.WhiteQueenSide, new King(Definitions.ChessColor.White))), "0-0-0 castling not possible");
        }

        [TestMethod]
        public void GetMovesTest_WhenFieldNextToKingIsAttacked_ThenNoCastling()
        {
            MoveGenerator generator = new MoveGenerator(new MoveFactory());
            Board board = new Board();
            string position = "....k..." +
                              "p..r.r.." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "P......." +
                              "R...K..R";
            board.SetPosition(position);

            var king = new King(Definitions.ChessColor.White);
            List<IMove> kingMoves = king.GetMoves(generator, board, Helper.FileCharToFile('e'), 1, true);
            Assert.AreEqual(false, kingMoves.Contains(new CastlingMove(CastlingType.WhiteKingSide, new King(Definitions.ChessColor.White))), "e1g1. 0-0 castling not possible");
            Assert.AreEqual(false, kingMoves.Contains(new CastlingMove(CastlingType.WhiteQueenSide, new King(Definitions.ChessColor.White))), "e1c1. 0-0-0 castling not possible");
        }

        [TestMethod]
        public void GetMovesTest_WhenNewKingFieldIsAttacked_ThenNoCastling()
        {
            MoveGenerator generator = new MoveGenerator(new MoveFactory());
            Board board = new Board();
            string position = "....k..." +
                              "p.r...r." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "P......." +
                              "R...K..R";
            board.SetPosition(position);

            var king = new King(Definitions.ChessColor.White);
            List<IMove> kingMoves = king.GetMoves(generator, board, Helper.FileCharToFile('e'), 1, true);
            Assert.AreEqual(false, kingMoves.Contains(new CastlingMove(CastlingType.WhiteKingSide, new King(Definitions.ChessColor.White))), "e1g1. 0-0 castling not possible. g1 is attacked.");
            Assert.AreEqual(false, kingMoves.Contains(new CastlingMove(CastlingType.WhiteQueenSide, new King(Definitions.ChessColor.White))), "e1c1. 0-0-0 castling not possible. c1 is attacked.");
        }

        // black

        [TestMethod]
        public void GetMovesTest_WhenCastlingRightOk_ThenCastling_Black()
        {
            MoveGenerator generator = new MoveGenerator(new MoveFactory());
            Board board = new Board();
            string position = "r...k..r" +
                              "p......." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "P......." +
                              "R...K..R";
            board.SetPosition(position);

            var king = new King(Definitions.ChessColor.Black);
            List<IMove> kingMoves = king.GetMoves(generator, board, Helper.FileCharToFile('e'), 8, true);
            Assert.AreEqual(true, kingMoves.Contains(new CastlingMove(CastlingType.BlackKingSide, new King(Definitions.ChessColor.Black))), "e8g8. 0-0 castling missing");
            Assert.AreEqual(true, kingMoves.Contains(new CastlingMove(CastlingType.BlackQueenSide, new King(Definitions.ChessColor.Black))), "e8c8. 0-0-0 castling missing");
        }

        [TestMethod]
        public void GetMovesTest_WhenKingMoved_ThenNoCastling_Black()
        {
            MoveGenerator generator = new MoveGenerator(new MoveFactory());
            Board board = new Board();
            string position = "r..k...r" + // king moved!
                              "p......." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "P......R" +
                              "...QK...";
            board.SetPosition(position);

            var king = new King(Definitions.ChessColor.Black);
            List<IMove> kingMoves = king.GetMoves(generator, board, Helper.FileCharToFile('d'), 8, true);
            Assert.AreEqual(false, kingMoves.Contains(new CastlingMove(CastlingType.BlackKingSide, new King(Definitions.ChessColor.Black))), "e1g1. 0-0 castling not possible");
            Assert.AreEqual(false, kingMoves.Contains(new CastlingMove(CastlingType.BlackQueenSide, new King(Definitions.ChessColor.Black))), "e1c1. 0-0-0 castling not possible");
        }

        [TestMethod]
        public void GetMovesTest_WhenRookMoved_ThenNoCastling_Black()
        {
            MoveGenerator generator = new MoveGenerator(new MoveFactory());
            Board board = new Board();
            string position = ".r..k..r." + // rooks moved!
                              "p......." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "P......R" +
                              "...QK...";
            board.SetPosition(position);

            var king = new King(Definitions.ChessColor.Black);
            List<IMove> kingMoves = king.GetMoves(generator, board, Helper.FileCharToFile('d'), 8, true);
            Assert.AreEqual(false, kingMoves.Contains(new CastlingMove(CastlingType.BlackKingSide, new King(Definitions.ChessColor.Black))), "e1g1. 0-0 castling not possible");
            Assert.AreEqual(false, kingMoves.Contains(new CastlingMove(CastlingType.BlackQueenSide, new King(Definitions.ChessColor.Black))), "e1c1. 0-0-0 castling not possible");
        }

        [TestMethod]
        public void GetMovesTest_WhenCastlingBlocked_ThenNoCastling_Black()
        {
            MoveGenerator generator = new MoveGenerator(new MoveFactory());
            Board board = new Board();
            string position = "r.b.k.nr" +
                              "p......." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "P......." +
                              "R..QK.NR";
            board.SetPosition(position);

            var king = new King(Definitions.ChessColor.Black);
            List<IMove> kingMoves = king.GetMoves(generator, board, Helper.FileCharToFile('e'), 8, true);
            Assert.AreEqual(false, kingMoves.Contains(new CastlingMove(CastlingType.BlackKingSide, new King(Definitions.ChessColor.Black))), "e1g1. 0-0 castling not possible");
            Assert.AreEqual(false, kingMoves.Contains(new CastlingMove(CastlingType.BlackQueenSide, new King(Definitions.ChessColor.Black))), "e1c1. 0-0-0 castling not possible");
        }

        [TestMethod]
        public void GetMovesTest_WhenFieldNextToKingAttacked_ThenNoCastling_Black()
        {
            MoveGenerator generator = new MoveGenerator(new MoveFactory());
            Board board = new Board();
            string position = "r...k..r" +
                              "p......." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "P......." +
                              "...RKR..";
            board.SetPosition(position);

            var king = new King(Definitions.ChessColor.Black);
            List<IMove> kingMoves = king.GetMoves(generator, board, Helper.FileCharToFile('e'), 8, true);
            Assert.AreEqual(false, kingMoves.Contains(new CastlingMove(CastlingType.BlackKingSide, new King(Definitions.ChessColor.Black))), "e1g1. 0-0 castling not possible. f8 attacked");
            Assert.AreEqual(false, kingMoves.Contains(new CastlingMove(CastlingType.BlackQueenSide, new King(Definitions.ChessColor.Black))), "e1c1. 0-0-0 castling not possible. d8 attacked");
        }

        [TestMethod]
        public void GetMovesTest_WhenNewKingFieldAttacked_ThenNoCastling_Black()
        {
            MoveGenerator generator = new MoveGenerator(new MoveFactory());
            Board board = new Board();
            string position = "r...k..r" +
                              "p......." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "P......." +
                              "..R.K.R.";
            board.SetPosition(position);

            var king = new King(Definitions.ChessColor.Black);
            List<IMove> kingMoves = king.GetMoves(generator, board, Helper.FileCharToFile('e'), 8, true);
            Assert.AreEqual(false, kingMoves.Contains(new CastlingMove(CastlingType.BlackKingSide, new King(Definitions.ChessColor.Black))), "e1g1. 0-0 castling not possible. g8 attacked");
            Assert.AreEqual(false, kingMoves.Contains(new CastlingMove(CastlingType.BlackQueenSide, new King(Definitions.ChessColor.Black))), "e1c1. 0-0-0 castling not possible. c8 attacked");
        }


        // ----------------------------------------------------------------
        // IsCheck Test
        // ----------------------------------------------------------------

        [TestMethod]
        public void IsCheckTest_WhenKingAttacked_ThenTrue()
        {
            MantaEngine engine = new MantaEngine(EngineType.MinimaxPosition);
            Board board = new Board();
            string position = "....rk.." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "....K...";
            board.SetPosition(position);
            engine.SetBoard(board);

            Assert.AreEqual(true, engine.IsCheck(Definitions.ChessColor.White), "king is attacked by rook! IsCheck should return true.");
        }

        [TestMethod]
        public void IsCheckTest_WhenKingNotAttacked_ThenFalse()
        {
            MantaEngine engine = new MantaEngine(EngineType.MinimaxPosition);
            Board board = new Board();
            string position = ".....k.." +
                              ".....p.." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "....K...";
            board.SetPosition(position);
            engine.SetBoard(board);

            Assert.AreEqual(false, engine.IsCheck(Definitions.ChessColor.White), "king is not attacked! IsCheck shoult return false.");
        }


    }
}