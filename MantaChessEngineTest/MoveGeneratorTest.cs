using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MantaChessEngine;
using MantaCommon;
using System.Linq;
using Moq;

namespace MantaChessEngineTest
{
    [TestClass]
    public class MoveGeneratorTest
    {
        // ----------------------------------------------------------------------------------------------------
        // Get Moves Tests
        // ----------------------------------------------------------------------------------------------------

        private Board board;

        [TestInitialize]
        public void Setup()
        {
            board = new Board(new Mock<IHashtable>().Object);
        }

        [TestMethod]
        public void GetMoves_WhenKnightB1_ThenTwoValidMoves()
        {
            board.SetInitialPosition();

            var knight = new Knight(ChessColor.White);
            var moves = knight.GetMoves(null, board, Square.B1, true); // get moves of knight

            Assert.AreEqual(2, moves.Count);
            Assert.AreEqual("b1a3.", moves[0].ToString());
            Assert.AreEqual("b1c3.", moves[1].ToString());
        }

        [TestMethod]
        public void GetMoves_WhenRook_ThenAllMoves()
        {
            string position = "........" +
                              "........" +
                              "....p..." +
                              "........" +
                              "....R..P" +
                              "........" +
                              "........" +
                              "........";
            board.SetPosition(position);

            var rook = new Rook(ChessColor.White);
            var moves = rook.GetMoves(null, board, Square.E4, true); // get moves of Rook

            Assert.AreEqual(11, moves.Count);
            Assert.AreEqual(true, moves.Contains(new NormalMove(Piece.MakePiece(PieceType.Rook, ChessColor.White), Square.E4, Square.E5, null)), "e4e5. missing");
            Assert.AreEqual(true, moves.Contains(new NormalMove(Piece.MakePiece(PieceType.Rook, ChessColor.White), Square.E4, Square.E6, Piece.MakePiece(PieceType.Pawn, ChessColor.Black))), "e4e6p missing"); // capture pawn
            Assert.AreEqual(true, moves.Contains(new NormalMove(Piece.MakePiece(PieceType.Rook, ChessColor.White), Square.E4, Square.F4, null)), "e4f4. missing");
            Assert.AreEqual(true, moves.Contains(new NormalMove(Piece.MakePiece(PieceType.Rook, ChessColor.White), Square.E4, Square.G4, null)), "e4g4. missing");
            Assert.AreEqual(true, moves.Contains(new NormalMove(Piece.MakePiece(PieceType.Rook, ChessColor.White), Square.E4, Square.E3, null)), "e4e3. missing");
            Assert.AreEqual(true, moves.Contains(new NormalMove(Piece.MakePiece(PieceType.Rook, ChessColor.White), Square.E4, Square.E2, null)), "e4e2. missing");
            Assert.AreEqual(true, moves.Contains(new NormalMove(Piece.MakePiece(PieceType.Rook, ChessColor.White), Square.E4, Square.E1, null)), "e4e1. missing");
            Assert.AreEqual(true, moves.Contains(new NormalMove(Piece.MakePiece(PieceType.Rook, ChessColor.White), Square.E4, Square.D4, null)), "e4d4. missing");
            Assert.AreEqual(true, moves.Contains(new NormalMove(Piece.MakePiece(PieceType.Rook, ChessColor.White), Square.E4, Square.C4, null)), "e4c4. missing");
            Assert.AreEqual(true, moves.Contains(new NormalMove(Piece.MakePiece(PieceType.Rook, ChessColor.White), Square.E4, Square.B4, null)), "e4b4. missing");
            Assert.AreEqual(true, moves.Contains(new NormalMove(Piece.MakePiece(PieceType.Rook, ChessColor.White), Square.E4, Square.A4, null)), "e4a4. missing");
        }

        [TestMethod]
        public void GetMoves_WhenQueen_ThenAllMoves()
        {
            string position = "........" +
                              "........" +
                              "........" +
                              "........" +
                              ".r.B...." +
                              "........" +
                              ".Q.P...." +
                              "........";
            board.SetPosition(position);

            var queen = new Queen(ChessColor.White);
            var moves = queen.GetMoves(null, board, Square.B2, true); // get moves of queen

            Assert.AreEqual(9, moves.Count);
            Assert.AreEqual(true, moves.Contains(new NormalMove(Piece.MakePiece(PieceType.Queen, ChessColor.White), Square.B2, Square.B3, null)), "b2b3. missing");
            Assert.AreEqual(true, moves.Contains(new NormalMove(Piece.MakePiece(PieceType.Queen, ChessColor.White), Square.B2, Square.B4, Piece.MakePiece(PieceType.Rook, ChessColor.Black))), "b2b4r missing");
            Assert.AreEqual(true, moves.Contains(new NormalMove(Piece.MakePiece(PieceType.Queen, ChessColor.White), Square.B2, Square.C3, null)), "b2c3. missing");
            Assert.AreEqual(true, moves.Contains(new NormalMove(Piece.MakePiece(PieceType.Queen, ChessColor.White), Square.B2, Square.C2, null)), "b2c2. missing");
            Assert.AreEqual(true, moves.Contains(new NormalMove(Piece.MakePiece(PieceType.Queen, ChessColor.White), Square.B2, Square.C1, null)), "b2c1. missing");
            Assert.AreEqual(true, moves.Contains(new NormalMove(Piece.MakePiece(PieceType.Queen, ChessColor.White), Square.B2, Square.B1, null)), "b2b1. missing");
            Assert.AreEqual(true, moves.Contains(new NormalMove(Piece.MakePiece(PieceType.Queen, ChessColor.White), Square.B2, Square.A1, null)), "b2a1. missing");
            Assert.AreEqual(true, moves.Contains(new NormalMove(Piece.MakePiece(PieceType.Queen, ChessColor.White), Square.B2, Square.A2, null)), "b2a2. missing");
            Assert.AreEqual(true, moves.Contains(new NormalMove(Piece.MakePiece(PieceType.Queen, ChessColor.White), Square.B2, Square.A3, null)), "b2a3. missing");
        }

        [TestMethod]
        public void GetMoves_WhenBishop_ThenAllMoves()
        {
            string position = "........" +
                              "........" +
                              "........" +
                              "........" +
                              "r...N..." +
                              "........" +
                              "..B....." +
                              "........";
            board.SetPosition(position);

            var bishop = new Bishop(ChessColor.White);
            var moves = bishop.GetMoves(null, board, Square.C2, true); // get moves of bishop

            Assert.AreEqual(5, moves.Count);
            Assert.AreEqual(true, moves.Contains(new NormalMove(Piece.MakePiece(PieceType.Bishop, ChessColor.White), Square.C2, Square.D3, null)), "c2d3. missing");
            Assert.AreEqual(true, moves.Contains(new NormalMove(Piece.MakePiece(PieceType.Bishop, ChessColor.White), Square.C2, Square.D1, null)), "c2d1. missing");
            Assert.AreEqual(true, moves.Contains(new NormalMove(Piece.MakePiece(PieceType.Bishop, ChessColor.White), Square.C2, Square.B1, null)), "c2b1. missing");
            Assert.AreEqual(true, moves.Contains(new NormalMove(Piece.MakePiece(PieceType.Bishop, ChessColor.White), Square.C2, Square.B3, null)), "c2b3. missing");
            Assert.AreEqual(true, moves.Contains(new NormalMove(Piece.MakePiece(PieceType.Bishop, ChessColor.White), Square.C2, Square.A4, Piece.MakePiece(PieceType.Rook, ChessColor.Black))), "c2a4r missing");
        }

        [TestMethod]
        public void GetMoves_WhenKing_ThenAllMoves()
        {
            MoveGenerator target = new MoveGenerator(board);
            string position = "........" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              ".rB....." +
                              ".KP.....";
            board.SetPosition(position);

            var king = new King(ChessColor.White);
            var moves = king.GetMoves(target, board, Square.B1, true); // king

            Assert.AreEqual(3, moves.Count);
            Assert.AreEqual(true, moves.Contains(new NormalMove(Piece.MakePiece(PieceType.King, ChessColor.White), Square.B1, Square.B2, Piece.MakePiece(PieceType.Rook, ChessColor.Black))), "b1b2r missing");
            Assert.AreEqual(true, moves.Contains(new NormalMove(Piece.MakePiece(PieceType.King, ChessColor.White), Square.B1, Square.A1, null)), "b1a1. missing");
            Assert.AreEqual(true, moves.Contains(new NormalMove(Piece.MakePiece(PieceType.King, ChessColor.White), Square.B1, Square.A2, null)), "b1a2. missing");
        }

        //
        // Pawn moves
        // 

        [TestMethod]
        public void GetMoves_WhenWhitePawn_ThenAllMoves() // en passant not included
        {
            string position = "........" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              ".r.b...." +
                              "..P....." +
                              "........";
            board.SetPosition(position);

            var pawn = new Pawn(ChessColor.White);
            var moves = pawn.GetMoves(null, board, Square.C2, true); // pawn

            Assert.AreEqual(4, moves.Count);
            Assert.AreEqual(true, moves.Contains(new NormalMove(Piece.MakePiece(PieceType.Pawn, ChessColor.White), Square.C2, Square.C3, null)), "c2c3. missing");
            Assert.AreEqual(true, moves.Contains(new NormalMove(Piece.MakePiece(PieceType.Pawn, ChessColor.White), Square.C2, Square.C4, null)), "c2c4. missing");
            Assert.AreEqual(true, moves.Contains(new NormalMove(Piece.MakePiece(PieceType.Pawn, ChessColor.White), Square.C2, Square.B3, Piece.MakePiece(PieceType.Rook, ChessColor.Black))), "c2b3r missing");
            Assert.AreEqual(true, moves.Contains(new NormalMove(Piece.MakePiece(PieceType.Pawn, ChessColor.White), Square.C2, Square.D3, Piece.MakePiece(PieceType.Bishop, ChessColor.Black))), "c2d3b missing");
        }

        [TestMethod]
        public void GetMoves_WhenWhitePawnBlocked_ThenAllMoves() // en passant not included
        {
            string position = "........" +
                              "........" +
                              "........" +
                              "........" +
                              "..n....." +
                              ".r.b...." +
                              "..P....." +
                              "........";
            board.SetPosition(position);

            var pawn = new Pawn(ChessColor.White);
            var moves = pawn.GetMoves(null, board, Square.C2, true); // pawn

            Assert.AreEqual(3, moves.Count);
            Assert.AreEqual(true, moves.Contains(new NormalMove(Piece.MakePiece(PieceType.Pawn, ChessColor.White), Square.C2, Square.C3, null)), "c2c3. missing");
            Assert.AreEqual(true, moves.Contains(new NormalMove(Piece.MakePiece(PieceType.Pawn, ChessColor.White), Square.C2, Square.B3, Piece.MakePiece(PieceType.Rook, ChessColor.Black))), "c2b3r missing");
            Assert.AreEqual(true, moves.Contains(new NormalMove(Piece.MakePiece(PieceType.Pawn, ChessColor.White), Square.C2, Square.D3, Piece.MakePiece(PieceType.Bishop, ChessColor.Black))), "c2d3b missing");
        }

        [TestMethod]
        public void GetMoves_WhenWhitePawnBlockedInMiddleOfBoard_ThenAllMoves()
        {
            string position = "........" +
                              "........" + 
                              "..p....." + // black pawn
                              "..P....." + // white pawn must not do c5-c7 !
                              "........" +
                              "........" +
                              "........" +
                              "........";
            board.SetPosition(position);

            var pawn = new Pawn(ChessColor.White);
            var moves = pawn.GetMoves(null, board, Square.C5, true); // white pawn

            Assert.AreEqual(0, moves.Count);
        }

        [TestMethod]
        public void GetMoves_WhenWhitePawnAtA5_ThenAllMoves()
        {
            string position = "........" +
                              "........" + 
                              "........" + 
                              "........" +
                              "........" +
                              "..p....." + // black pawn
                              "..P....." + // white pawn must not do c2-c4 !
                              "........";
            board.SetPosition(position);

            var pawn = new Pawn(ChessColor.White);
            var moves = pawn.GetMoves(null, board, Square.C2, true); // white pawn

            Assert.AreEqual(0, moves.Count);
        }

        [TestMethod]
        public void GetMoves_WhenWhiteCanCaptureEnPassant_ThenListEnPassant()
        {
            string position = ".......k" +
                              "p......." +
                              "........" +
                              ".P......" +
                              "........" +
                              "........" + 
                              "........" + 
                              "...K....";
            board.SetPosition(position);
            board.Move(new NormalMove(new Pawn(ChessColor.Black), Square.A7, Square.A5, null));
            Assert.AreEqual(Square.A6, board.BoardState.LastEnPassantSquare);

            var pawn = new Pawn(ChessColor.White);
            var moves = pawn.GetMoves(null, board, Square.B5, true); // white pawn

            Assert.AreEqual(2, moves.Count);
            Assert.AreEqual(true, moves.Contains(new EnPassantCaptureMove(Piece.MakePiece(PieceType.Pawn, ChessColor.White), Square.B5, Square.A6, Piece.MakePiece(PieceType.Pawn, ChessColor.Black))), "b5a6pe en passant missing");
            Assert.AreEqual(true, moves.Contains(new NormalMove(Piece.MakePiece(PieceType.Pawn, ChessColor.White), Square.B5, Square.B6, null)), "b5b6. missing");
        }

        [TestMethod]
        public void GetMoves_WhenBlackPawn_ThenAllMoves()
        {
            string position = "........" +
                              "..p....." +
                              ".R.B...." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "........";
            board.SetPosition(position);

            var pawn = new Pawn(ChessColor.Black);
            var moves = pawn.GetMoves(null, board, Square.C7, true); // black pawn

            Assert.AreEqual(4, moves.Count);
            Assert.AreEqual(true, moves.Contains(new NormalMove(Piece.MakePiece(PieceType.Pawn, ChessColor.Black), Square.C7, Square.C6, null)), "c7c6. missing");
            Assert.AreEqual(true, moves.Contains(new NormalMove(Piece.MakePiece(PieceType.Pawn, ChessColor.Black), Square.C7, Square.C5, null)), "c7c5. missing");
            Assert.AreEqual(true, moves.Contains(new NormalMove(Piece.MakePiece(PieceType.Pawn, ChessColor.Black), Square.C7, Square.B6, Piece.MakePiece(PieceType.Rook, ChessColor.White))), "c7b6R missing");
            Assert.AreEqual(true, moves.Contains(new NormalMove(Piece.MakePiece(PieceType.Pawn, ChessColor.Black), Square.C7, Square.D6, Piece.MakePiece(PieceType.Bishop, ChessColor.White))), "c7d6B missing");
        }

        [TestMethod]
        public void GetMoves_WhenBlackPawnAta4_ThenAllMoves()
        {
            string position = "........" +
                              "........" +
                              "........" +
                              ".p......" + // black pawn must not do b5-b3 !
                              "........" + 
                              "........" +
                              "........" +
                              "........";
            board.SetPosition(position);

            var pawn = new Pawn(ChessColor.Black);
            var moves = pawn.GetMoves(null, board, Square.B5, true); // black pawn

            Assert.AreEqual(1, moves.Count);
            Assert.AreEqual(true, moves.Contains(new NormalMove(Piece.MakePiece(PieceType.Pawn, ChessColor.Black), Square.B5, Square.B4, null)), "b3b4 missing");
        }

        [TestMethod]
        public void GetMoves_WhenWhitePawnAt8Straight_ThenPromoted()
        {
            string position = "........" +
                              ".P......" + // white pawn gets promoted to queen, rook, bishop or knight
                              "........" +
                              "........" + 
                              "........" +
                              "........" +
                              "........" +
                              "........";
            board.SetPosition(position);

            var pawn = new Pawn(ChessColor.White);
            var moves = pawn.GetMoves(null, board, Square.B7, true); // white pawn

            Assert.AreEqual(4, moves.Count);
            Assert.AreEqual(true, moves.Contains(new PromotionMove(Piece.MakePiece(PieceType.Pawn, ChessColor.White), Square.B7, Square.B8, null, PieceType.Queen)), "b7b8 missing (promotion to queen walking straight)");
            Assert.AreEqual(true, moves.Contains(new PromotionMove(Piece.MakePiece(PieceType.Pawn, ChessColor.White), Square.B7, Square.B8, null, PieceType.Rook)), "b7b8 missing (promotion to rook walking straight)");
            Assert.AreEqual(true, moves.Contains(new PromotionMove(Piece.MakePiece(PieceType.Pawn, ChessColor.White), Square.B7, Square.B8, null, PieceType.Bishop)), "b7b8 missing (promotion to bishop walking straight)");
            Assert.AreEqual(true, moves.Contains(new PromotionMove(Piece.MakePiece(PieceType.Pawn, ChessColor.White), Square.B7, Square.B8, null, PieceType.Knight)), "b7b8 missing (promotion to knight walking straight)");
        }

        [TestMethod]
        public void GetMoves_WhenBlackPawnAt1Straight_ThenPromoted()
        {
            string position = "........" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              ".p......" +  // black pawn gets promoted to queen, rook, bishop or knight
                              "........";
            board.SetPosition(position);

            var pawn = new Pawn(ChessColor.Black);
            var moves = pawn.GetMoves(null, board, Square.B2, true); // black pawn

            Assert.AreEqual(4, moves.Count);
            Assert.AreEqual(true, moves.Contains(new PromotionMove(Piece.MakePiece(PieceType.Pawn, ChessColor.Black), Square.B2, Square.B1, null, PieceType.Queen)), "b2b1 missing (promotion to queen walking straight)");
            Assert.AreEqual(true, moves.Contains(new PromotionMove(Piece.MakePiece(PieceType.Pawn, ChessColor.Black), Square.B2, Square.B1, null, PieceType.Rook)), "b2b1 missing (promotion to rook walking straight)");
            Assert.AreEqual(true, moves.Contains(new PromotionMove(Piece.MakePiece(PieceType.Pawn, ChessColor.Black), Square.B2, Square.B1, null, PieceType.Bishop)), "b2b1 missing (promotion to bishop walking straight)");
            Assert.AreEqual(true, moves.Contains(new PromotionMove(Piece.MakePiece(PieceType.Pawn, ChessColor.Black), Square.B2, Square.B1, null, PieceType.Knight)), "b2b1 missing (promotion to knight walking straight)");
        }

        [TestMethod]
        public void GetMoves_WhenWhitePawnAt8Capturing_ThenPromoted()
        {
            string position = "rN......" +
                              ".P......" + // white pawn gets promoted to queen, rook, bishop or knight
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "........";
            board.SetPosition(position);

            var pawn = new Pawn(ChessColor.White);
            var moves = pawn.GetMoves(null, board, Square.B7, true); // white pawn

            Assert.AreEqual(4, moves.Count);
            Assert.AreEqual(true, moves.Contains(new PromotionMove(Piece.MakePiece(PieceType.Pawn, ChessColor.White), Square.B7, Square.A8, Piece.MakePiece(PieceType.Rook, ChessColor.Black), PieceType.Queen)),"b7a8 missing (promotion to queen when capturing)");
            Assert.AreEqual(true, moves.Contains(new PromotionMove(Piece.MakePiece(PieceType.Pawn, ChessColor.White), Square.B7, Square.A8, Piece.MakePiece(PieceType.Rook, ChessColor.Black), PieceType.Rook)), "b7a8 missing (promotion to rook when capturing)");
            Assert.AreEqual(true, moves.Contains(new PromotionMove(Piece.MakePiece(PieceType.Pawn, ChessColor.White), Square.B7, Square.A8, Piece.MakePiece(PieceType.Rook, ChessColor.Black), PieceType.Bishop)), "b7a8 missing (promotion to bishop when capturing)");
            Assert.AreEqual(true, moves.Contains(new PromotionMove(Piece.MakePiece(PieceType.Pawn, ChessColor.White), Square.B7, Square.A8, Piece.MakePiece(PieceType.Rook, ChessColor.Black), PieceType.Knight)), "b7a8 missing (promotion to knight when capturing)");
        }

        [TestMethod]
        public void GetMoves_WhenBlackPawnAt1Capturing_ThenPromoted()
        {
            string position = "........" +
                              "........" + // white pawn gets promoted to queen, rook, bishop or knight
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              ".p......" +
                              "Rn......";
            board.SetPosition(position);

            var pawn = new Pawn(ChessColor.Black);
            var moves = pawn.GetMoves(null, board, Square.B2, true); // black pawn

            Assert.AreEqual(4, moves.Count);
            Assert.AreEqual(true, moves.Contains(new PromotionMove(Piece.MakePiece(PieceType.Pawn, ChessColor.Black), Square.B2, Square.A1, Piece.MakePiece(PieceType.Rook, ChessColor.White), PieceType.Queen)), "b2a1 missing (promotion to queen when capturing)");
            Assert.AreEqual(true, moves.Contains(new PromotionMove(Piece.MakePiece(PieceType.Pawn, ChessColor.Black), Square.B2, Square.A1, Piece.MakePiece(PieceType.Rook, ChessColor.White), PieceType.Rook)),  "b2a1 missing (promotion to rook when capturing)");
            Assert.AreEqual(true, moves.Contains(new PromotionMove(Piece.MakePiece(PieceType.Pawn, ChessColor.Black), Square.B2, Square.A1, Piece.MakePiece(PieceType.Rook, ChessColor.White), PieceType.Bishop)), "b2a1 missing (promotion to bishop when capturing)");
            Assert.AreEqual(true, moves.Contains(new PromotionMove(Piece.MakePiece(PieceType.Pawn, ChessColor.Black), Square.B2, Square.A1, Piece.MakePiece(PieceType.Rook, ChessColor.White), PieceType.Knight)), "b2a1 missing (promotion to knight when capturing)");
        }

        // ----------------------------------------------------------------------------------------------------
        // Get All Moves Tests
        // ----------------------------------------------------------------------------------------------------

        [TestMethod]
        public void GetAllMoves_WhenTwoPieces_ThenShowMovesOfBothPieces()
        {
            MoveGenerator target = new MoveGenerator(board);
            string position = "........" +
                              "........" +
                              "....p..." +
                              "........" +
                              "........" +
                              "........" +
                              "....P..." +
                              "....K...";
            board.SetPosition(position);
           
            var moves = target.GetAllMoves(ChessColor.White).ToList<IMove>();

            Assert.AreEqual(6, moves.Count);
            Assert.AreEqual(true, moves.Contains(new NormalMove(Piece.MakePiece(PieceType.Pawn, ChessColor.White), Square.E2, Square.E3, null)), "e2e3. missing");
            Assert.AreEqual(true, moves.Contains(new NormalMove(Piece.MakePiece(PieceType.Pawn, ChessColor.White), Square.E2, Square.E4, null)), "e2e4. missing");
            Assert.AreEqual(true, moves.Contains(new NormalMove(Piece.MakePiece(PieceType.King, ChessColor.White), Square.E1, Square.D1, null)), "e1d1. missing");
            Assert.AreEqual(true, moves.Contains(new NormalMove(Piece.MakePiece(PieceType.King, ChessColor.White), Square.E1, Square.D2, null)), "e1d2. missing");
            Assert.AreEqual(true, moves.Contains(new NormalMove(Piece.MakePiece(PieceType.King, ChessColor.White), Square.E1, Square.F2, null)), "e1f2. missing");
            Assert.AreEqual(true, moves.Contains(new NormalMove(Piece.MakePiece(PieceType.King, ChessColor.White), Square.E1, Square.F1, null)), "e1f1. missing");
        }

        // ----------------------------------------------------------------------------------------------------
        // Is Move Valid Tests
        // ----------------------------------------------------------------------------------------------------

        [TestMethod]
        public void IsMoveValidTest_WhenValidMoveWhite_ThenTrue()
        {
            MoveGenerator target = new MoveGenerator(board);
            string position = ".......k" +
                              "........" +
                              "........" +
                              "........" +
                              "..n....." +
                              ".r.b...." +
                              "..P....." +
                              ".......K";
            board.SetPosition(position);
            
            bool valid = target.IsMoveValid(new NormalMove(Piece.MakePiece(PieceType.Pawn, ChessColor.White), Square.C2, Square.C3, null)); // pawn
            Assert.AreEqual(true, valid, "Move should be valid.");
        }

        [TestMethod]
        public void IsMoveValidTest_WhenInvalidMoveWhite_ThenFalse()
        {
            MoveGenerator target = new MoveGenerator(board);
            string position = ".......k" +
                              "........" +
                              "........" +
                              "........" +
                              "..n....." +
                              ".r.b...." +
                              "..P....." +
                              ".......K";
            board.SetPosition(position);
            
            bool valid = target.IsMoveValid(new NormalMove(Piece.MakePiece(PieceType.Pawn, ChessColor.White), Square.C2, Square.C4, null)); // pawn
            Assert.AreEqual(false, valid, "Move should be invalid.");
        }

        [TestMethod]
        public void IsMoveValidTest_WhenValidMoveBlack_ThenTrue()
        {
            MoveGenerator target = new MoveGenerator(board);
            string position = ".......k" +
                              "..p....." +
                              ".R.B...." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              ".......K";
            board.SetPosition(position);
            board.BoardState.SideToMove = ChessColor.Black;
            
            bool valid = target.IsMoveValid(new NormalMove(Piece.MakePiece(PieceType.Pawn, ChessColor.Black), Square.C7, Square.C5, null)); // pawn
            Assert.AreEqual(true, valid, "Move should be valid.");

            valid = target.IsMoveValid(new NormalMove(Piece.MakePiece(PieceType.Pawn, ChessColor.Black), Square.C7, Square.C6, null)); // pawn
            Assert.AreEqual(true, valid, "Move should be valid.");
        }

        [TestMethod]
        public void IsMoveValidTest_WhenWrongSideMoves_ThenFalse()
        {
            MoveGenerator target = new MoveGenerator(board);
            board.SetInitialPosition();

            bool valid = target.IsMoveValid(new NormalMove(Piece.MakePiece(PieceType.Pawn, ChessColor.Black), Square.E7, Square.E5, null)); // BLACK pawn
            Assert.AreEqual(false, valid, "This is white's move. Black must not move.");
        }

        // ----------------------------------------------------------------------------------------------------
        // Get End Position Tests
        // ----------------------------------------------------------------------------------------------------

        [TestMethod]
        public void GetEndPosition_WhenKnightSequence_ThenCorrectEndRankFile()
        {
            string knightSequence = "uul";

            MoveGenerator target = new MoveGenerator(board);
            target.GetEndPosition(Helper.FileCharToFile('b'), 1, knightSequence, out int targetFile, out int targetRank, out bool valid);

            Assert.AreEqual(Helper.FileCharToFile('a'), targetFile);
            Assert.AreEqual(3, targetRank);
            Assert.AreEqual(true, valid);
        }

        [TestMethod]
        public void GetEndPosition_WhenOtherKnightSequence_ThenCorrectEndRankFile()
        {
            string knightSequence = "ddr";

            MoveGenerator target = new MoveGenerator(board);
            target.GetEndPosition(Helper.FileCharToFile('c'), 3, knightSequence, out int targetFile, out int targetRank, out bool valid);

            Assert.AreEqual(Helper.FileCharToFile('d'), targetFile);
            Assert.AreEqual(1, targetRank);
            Assert.AreEqual(true, valid);
        }

        [TestMethod]
        public void GetEndPosition_WhenKnightSequenceInvalid_ThenMoveInvalid()
        {
            string knightSequence = "ddl";

            MoveGenerator target = new MoveGenerator(board);
            target.GetEndPosition(Helper.FileCharToFile('a'), 1, knightSequence, out int targetFile, out int targetRank, out bool valid);

            Assert.AreEqual(false, valid);
        }

        [TestMethod]
        public void GetCorrectMoveTest_Whene2e4_ThenAddDot()
        {
            var factory = new MoveFactory(board);
            MoveGenerator target = new MoveGenerator(board);
            board.SetInitialPosition();

            IMove actualMove = factory.MakeMoveUci("e2e4");
            Assert.AreEqual("e2e4.", actualMove.ToString());
        }

        [TestMethod]
        public void GetCorrectMoveTest_WhenCaptureNormal_ThenAddCapturedPiece()
        {
            var factory = new MoveFactory(board);
            string position = "...k...." +
                              "........" +
                              "..p....." +
                              "...P...." +
                              "........" +
                              "........" +
                              "........" +
                              "..K.....";
            board.SetPosition(position);

            IMove actualMove = factory.MakeMoveUci("d5c6");
            Assert.AreEqual("d5c6p", actualMove.ToString());
        }

        [TestMethod]
        public void GetCorrectMoveTest_WhenCaptureEnPassant_ThenAddCapturedPiece()
        {
            var factory = new MoveFactory(board);
            string position = "...k...." +
                              "..p....." +
                              "........" +
                              "...P...." +
                              "........" +
                              "........" +
                              "........" +
                              "..K.....";
            board.SetPosition(position);
            board.Move(new NormalMove(Piece.MakePiece(PieceType.Pawn, ChessColor.Black), Square.C7, Square.C5, null));

            IMove actualMove = factory.MakeMoveUci("d5c6");
            Assert.AreEqual("d5c6pe", actualMove.ToString());
        }

        [TestMethod]
        public void GetCorrectMoveTest_WhenPromotion_ThenReturnPromotionMove()
        {
            var factory = new MoveFactory(board);
            string position = ".......k" +
                              "P......." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "..K.....";
            board.SetPosition(position);

            IMove actualMove = factory.MakeMoveUci("a7a8Q");

            var expectedMove = new PromotionMove(Piece.MakePiece(PieceType.Pawn, ChessColor.White), Square.A7, Square.A8, null, PieceType.Queen);
            Assert.AreEqual(expectedMove, actualMove);
        }

        // ------------------------------------------------------------------
        // Castling tests
        // ------------------------------------------------------------------

        // white

        [TestMethod]
        public void GetMovesTest_WhenCastlingRightOk_ThenCastling()
        {
            MoveGenerator generator = new MoveGenerator(board);
            string position = "r...k..r" +
                              "p......." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "P......." +
                              "R...K..R";
            board.SetPosition(position);

            var king = new King(ChessColor.White);
            var kingMoves = king.GetMoves(generator, board, Square.E1, true);

            Assert.AreEqual(true, kingMoves.Contains(new CastlingMove(MantaChessEngine.CastlingType.WhiteKingSide, new King(ChessColor.White))), "e1g1. 0-0 castling missing");
            Assert.AreEqual(true, kingMoves.Contains(new CastlingMove(MantaChessEngine.CastlingType.WhiteQueenSide, new King(ChessColor.White))), "e1c1. 0-0-0 castling missing");
        }

        [TestMethod]
        public void GetMovesTest_WhenKingMoved_ThenNoCastling()
        {
            MoveGenerator generator = new MoveGenerator(board);
            string position = "r...k..r" +
                              "p......." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "P......." +
                              "R..K...R";
            board.SetPosition(position);

            var king = new King(ChessColor.White);
            List<IMove> kingMoves = king.GetMoves(generator, board, Square.D1, true);
            Assert.AreEqual(false, kingMoves.Contains(new CastlingMove(MantaChessEngine.CastlingType.WhiteKingSide, new King(ChessColor.White))), "e1g1. 0-0 castling not possible");
            Assert.AreEqual(false, kingMoves.Contains(new CastlingMove(MantaChessEngine.CastlingType.WhiteQueenSide, new King(ChessColor.White))), "e1c1. 0-0-0 castling not possible");
        }

        [TestMethod]
        public void GetMovesTest_WhenRookMoved_ThenNoCastling()
        {
            MoveGenerator generator = new MoveGenerator(board);
            string position = "r...k..r" +
                              "p......." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "P......R" +
                              ".R..K...";
            board.SetPosition(position);

            var king = new King(ChessColor.White);
            List<IMove> kingMoves = king.GetMoves(generator, board, Square.E1, true);
            Assert.AreEqual(false, kingMoves.Contains(new CastlingMove(MantaChessEngine.CastlingType.WhiteKingSide, new King(ChessColor.White))), "e1g1. 0-0 castling not possible");
            Assert.AreEqual(false, kingMoves.Contains(new CastlingMove(MantaChessEngine.CastlingType.WhiteQueenSide, new King(ChessColor.White))), "e1c1. 0-0-0 castling not possible");
        }

        [TestMethod]
        public void GetMovesTest_WhenCastlingBlockedKingSide_ThenNoCastlingOnKingSide()
        {
            MoveGenerator target = new MoveGenerator(board);
            string position = "r...k..r" +
                              "p......." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "P......." +
                              "R...K.NR";
            board.SetPosition(position);

            var king = new King(ChessColor.White);
            List<IMove> kingMoves = king.GetMoves(target, board, Square.E1, true);
            Assert.AreEqual(false, kingMoves.Contains(new CastlingMove(MantaChessEngine.CastlingType.WhiteKingSide, new King(ChessColor.White))), "e1g1. 0-0 castling not possible");
        }

        [TestMethod]
        public void GetMovesTest_WhenCastlingBlockedQueenSide_ThenNoCastlingOnQueenSide()
        {
            MoveGenerator target = new MoveGenerator(board);
            string position = "r...k..r" +
                              "p......." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "P......." +
                              "R..QK..R";
            board.SetPosition(position);

            var king = new King(ChessColor.White);
            List<IMove> kingMoves = king.GetMoves(target, board, Square.E1, true);
            Assert.AreEqual(false, kingMoves.Contains(new CastlingMove(MantaChessEngine.CastlingType.WhiteQueenSide, new King(ChessColor.White))), "0-0-0 castling not possible");
        }

        [TestMethod]
        public void GetMovesTest_WhenFieldNextToKingIsAttacked_ThenNoCastling()
        {
            MoveGenerator target = new MoveGenerator(board);
            string position = "....k..." +
                              "p..r.r.." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "P......." +
                              "R...K..R";
            board.SetPosition(position);

            var king = new King(ChessColor.White);
            List<IMove> kingMoves = king.GetMoves(target, board, Square.E1, true);
            Assert.AreEqual(false, kingMoves.Contains(new CastlingMove(MantaChessEngine.CastlingType.WhiteKingSide, new King(ChessColor.White))), "e1g1. 0-0 castling not possible");
            Assert.AreEqual(false, kingMoves.Contains(new CastlingMove(MantaChessEngine.CastlingType.WhiteQueenSide, new King(ChessColor.White))), "e1c1. 0-0-0 castling not possible");
        }

        [TestMethod]
        public void GetMovesTest_WhenNewKingFieldIsAttacked_ThenNoCastling()
        {
            MoveGenerator target = new MoveGenerator(board);
            string position = "....k..." +
                              "p.r...r." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "P......." +
                              "R...K..R";
            board.SetPosition(position);

            var king = new King(ChessColor.White);
            List<IMove> kingMoves = king.GetMoves(target, board, Square.E1, true);
            Assert.AreEqual(false, kingMoves.Contains(new CastlingMove(MantaChessEngine.CastlingType.WhiteKingSide, new King(ChessColor.White))), "e1g1. 0-0 castling not possible. g1 is attacked.");
            Assert.AreEqual(false, kingMoves.Contains(new CastlingMove(MantaChessEngine.CastlingType.WhiteQueenSide, new King(ChessColor.White))), "e1c1. 0-0-0 castling not possible. c1 is attacked.");
        }

        /// -----------------------------------------------------------------------------------------
        /// White Castling rights lost after king or rook move
        /// -----------------------------------------------------------------------------------------

        [TestMethod]
        public void GetMovesTest_WhenwhiteKingMoved_ThenCastlingRightLost()
        {
            MoveGenerator target = new MoveGenerator(board);
            string position = "r...k..r" +
                              "p......." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "P...K..." +
                              "R......R";
            board.SetPosition(position);

            // white king moves -> loses castling right
            board.BoardState.SideToMove = ChessColor.White;
            var king = new King(ChessColor.White);
            var kingMove = new NormalMove(king, Square.E2, Square.E1, null);
            board.Move(kingMove);

            // black move
            var pawn = new Pawn(ChessColor.Black);
            var pawnMove = new NormalMove(pawn, Square.A7, Square.A6, null);
            board.Move(pawnMove);

            // get legal moves of king. should not include castling.
            var kingMoves = king.GetMoves(target, board, Square.E1, true);

            Assert.AreEqual(false, kingMoves.Contains(new CastlingMove(MantaChessEngine.CastlingType.WhiteKingSide, new King(ChessColor.White))), "e1g1. 0-0 castling missing");
            Assert.AreEqual(false, kingMoves.Contains(new CastlingMove(MantaChessEngine.CastlingType.WhiteQueenSide, new King(ChessColor.White))), "e1c1. 0-0-0 castling missing");
        }

        [TestMethod]
        public void GetMovesTest_WhenWhiteKingRookMoved_ThenCastlingRightKingSideLost()
        {
            MoveGenerator target = new MoveGenerator(board);
            string position = "r...k..r" +
                              "p......." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "P......." +
                              "R...K..R";
            board.SetPosition(position);

            // white king side rook moves -> loses castling right
            board.BoardState.SideToMove = ChessColor.White;
            var rook = new Rook(ChessColor.White);
            var pawnBlack = new Pawn(ChessColor.Black);

            board.Move(new NormalMove(rook, Square.H1, Square.H2, null));

            // moves to bring rook back to original position
            board.Move(new NormalMove(pawnBlack, Square.A7, Square.A6, null));
            board.Move(new NormalMove(rook, Square.H2, Square.H1, null));
            board.Move(new NormalMove(pawnBlack, Square.A6, Square.A5, null));


            // get legal moves of king. should not include castling.
            var king = new King(ChessColor.White);
            var kingMoves = king.GetMoves(target, board, Square.E1, true);

            Assert.AreEqual(false, kingMoves.Contains(new CastlingMove(MantaChessEngine.CastlingType.WhiteKingSide, new King(ChessColor.White))), "e1g1. 0-0 castling missing");
            Assert.AreEqual(true, kingMoves.Contains(new CastlingMove(MantaChessEngine.CastlingType.WhiteQueenSide, new King(ChessColor.White))), "e1c1. 0-0-0 castling missing");
        }

        [TestMethod]
        public void GetMovesTest_WhenWhiteQueenRookMoved_ThenCastlingRightQueenSideLost()
        {
            MoveGenerator target = new MoveGenerator(board);
            string position = "r...k..r" +
                              "p......." +
                              "........" +
                              "........" +
                              "........" +
                              "P......." +
                              "........" +
                              "R...K..R";
            board.SetPosition(position);

            // white king side rook moves -> loses castling right
            board.BoardState.SideToMove = ChessColor.White;
            var rook = new Rook(ChessColor.White);
            var pawnBlack = new Pawn(ChessColor.Black);

            board.Move(new NormalMove(rook, Square.A1, Square.A2, null));

            // moves to bring white rook back to original position
            board.Move(new NormalMove(pawnBlack, Square.A7, Square.A6, null));
            board.Move(new NormalMove(rook, Square.A2, Square.A1, null));
            board.Move(new NormalMove(pawnBlack, Square.A6, Square.A5, null));


            // get legal moves of king. should not include castling.
            var king = new King(ChessColor.White);
            var kingMoves = king.GetMoves(target, board, Square.E1, true);

            Assert.AreEqual(true, kingMoves.Contains(new CastlingMove(MantaChessEngine.CastlingType.WhiteKingSide, new King(ChessColor.White))), "e1g1. 0-0 castling missing");
            Assert.AreEqual(false, kingMoves.Contains(new CastlingMove(MantaChessEngine.CastlingType.WhiteQueenSide, new King(ChessColor.White))), "e1c1. 0-0-0 castling missing");
        }

        // black

        [TestMethod]
        public void GetMovesTest_WhenCastlingRightOk_ThenCastling_Black()
        {
            MoveGenerator generator = new MoveGenerator(board);
            string position = "r...k..r" +
                              "p......." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "P......." +
                              "R...K..R";
            board.SetPosition(position);

            var king = new King(ChessColor.Black);
            List<IMove> kingMoves = king.GetMoves(generator, board, Square.E8, true);
            Assert.AreEqual(true, kingMoves.Contains(new CastlingMove(MantaChessEngine.CastlingType.BlackKingSide, new King(ChessColor.Black))), "e8g8. 0-0 castling missing");
            Assert.AreEqual(true, kingMoves.Contains(new CastlingMove(MantaChessEngine.CastlingType.BlackQueenSide, new King(ChessColor.Black))), "e8c8. 0-0-0 castling missing");
        }

        [TestMethod]
        public void GetMovesTest_WhenKingMoved_ThenNoCastling_Black()
        {
            MoveGenerator generator = new MoveGenerator(board);
            string position = "r..k...r" + // king moved!
                              "p......." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "P......R" +
                              "...QK...";
            board.SetPosition(position);

            var king = new King(ChessColor.Black);
            List<IMove> kingMoves = king.GetMoves(generator, board, Square.D8, true);
            Assert.AreEqual(false, kingMoves.Contains(new CastlingMove(MantaChessEngine.CastlingType.BlackKingSide, new King(ChessColor.Black))), "e1g1. 0-0 castling not possible");
            Assert.AreEqual(false, kingMoves.Contains(new CastlingMove(MantaChessEngine.CastlingType.BlackQueenSide, new King(ChessColor.Black))), "e1c1. 0-0-0 castling not possible");
        }

        [TestMethod]
        public void GetMovesTest_WhenRookMoved_ThenNoCastling_Black()
        {
            MoveGenerator generator = new MoveGenerator(board);
            string position = ".r..k..r." + // rooks moved!
                              "p......." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "P......R" +
                              "...QK...";
            board.SetPosition(position);

            var king = new King(ChessColor.Black);
            List<IMove> kingMoves = king.GetMoves(generator, board, Square.D8, true);
            Assert.AreEqual(false, kingMoves.Contains(new CastlingMove(MantaChessEngine.CastlingType.BlackKingSide, new King(ChessColor.Black))), "e1g1. 0-0 castling not possible");
            Assert.AreEqual(false, kingMoves.Contains(new CastlingMove(MantaChessEngine.CastlingType.BlackQueenSide, new King(ChessColor.Black))), "e1c1. 0-0-0 castling not possible");
        }

        [TestMethod]
        public void GetMovesTest_WhenCastlingBlocked_ThenNoCastling_Black()
        {
            MoveGenerator generator = new MoveGenerator(board);
            string position = "r.b.k.nr" +
                              "p......." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "P......." +
                              "R..QK.NR";
            board.SetPosition(position);

            var king = new King(ChessColor.Black);
            List<IMove> kingMoves = king.GetMoves(generator, board, Square.E8, true);
            Assert.AreEqual(false, kingMoves.Contains(new CastlingMove(MantaChessEngine.CastlingType.BlackKingSide, new King(ChessColor.Black))), "e1g1. 0-0 castling not possible");
            Assert.AreEqual(false, kingMoves.Contains(new CastlingMove(MantaChessEngine.CastlingType.BlackQueenSide, new King(ChessColor.Black))), "e1c1. 0-0-0 castling not possible");
        }

        [TestMethod]
        public void GetMovesTest_WhenFieldNextToKingAttacked_ThenNoCastling_Black()
        {
            MoveGenerator generator = new MoveGenerator(board);
            string position = "r...k..r" +
                              "p......." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "P......." +
                              "...RKR..";
            board.SetPosition(position);

            var king = new King(ChessColor.Black);
            List<IMove> kingMoves = king.GetMoves(generator, board, Square.E8, true);
            Assert.AreEqual(false, kingMoves.Contains(new CastlingMove(MantaChessEngine.CastlingType.BlackKingSide, new King(ChessColor.Black))), "e1g1. 0-0 castling not possible. f8 attacked");
            Assert.AreEqual(false, kingMoves.Contains(new CastlingMove(MantaChessEngine.CastlingType.BlackQueenSide, new King(ChessColor.Black))), "e1c1. 0-0-0 castling not possible. d8 attacked");
        }

        [TestMethod]
        public void GetMovesTest_WhenNewKingFieldAttacked_ThenNoCastling_Black()
        {
            MoveGenerator generator = new MoveGenerator(board);
            string position = "r...k..r" +
                              "p......." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "P......." +
                              "..R.K.R.";
            board.SetPosition(position);

            var king = new King(ChessColor.Black);
            List<IMove> kingMoves = king.GetMoves(generator, board, Square.E8, true);
            Assert.AreEqual(false, kingMoves.Contains(new CastlingMove(MantaChessEngine.CastlingType.BlackKingSide, new King(ChessColor.Black))), "e1g1. 0-0 castling not possible. g8 attacked");
            Assert.AreEqual(false, kingMoves.Contains(new CastlingMove(MantaChessEngine.CastlingType.BlackQueenSide, new King(ChessColor.Black))), "e1c1. 0-0-0 castling not possible. c8 attacked");
        }

        /// -----------------------------------------------------------------------------------------
        /// Black Castling rights lost after king or rook move
        /// -----------------------------------------------------------------------------------------

        [TestMethod]
        public void GetMovesTest_WhenBlackKingMoved_ThenCastlingRightLost()
        {
            MoveGenerator generator = new MoveGenerator(board);
            string position = "r......r" +
                              "p...k..." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "P......." +
                              "R...K..R";
            board.SetPosition(position);

            // Black king moves -> loses castling right
            board.BoardState.SideToMove = ChessColor.Black;
            var king = new King(ChessColor.Black);
            var kingMove = new NormalMove(king, Square.E7, Square.E8, null);
            board.Move(kingMove);

            // white move
            var pawn = new Pawn(ChessColor.White);
            var pawnMove = new NormalMove(pawn, Square.A2, Square.A1, null);
            board.Move(pawnMove);

            // get legal moves of king. should not include castling.
            var kingMoves = king.GetMoves(generator, board, Square.E8, true);

            Assert.AreEqual(false, kingMoves.Contains(new CastlingMove(MantaChessEngine.CastlingType.BlackKingSide, new King(ChessColor.Black))), "e8g8. 0-0 castling missing");
            Assert.AreEqual(false, kingMoves.Contains(new CastlingMove(MantaChessEngine.CastlingType.BlackQueenSide, new King(ChessColor.Black))), "e8c8. 0-0-0 castling missing");
        }

        [TestMethod]
        public void GetMovesTest_WhenBlackKingRookMoved_ThenCastlingRightKingSideLost()
        {
            MoveGenerator generator = new MoveGenerator(board);
            string position = "r...k..r" +
                              "p......." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "P......." +
                              "R...K..R";
            board.SetPosition(position);

            // black king side rook moves -> loses castling right
            board.BoardState.SideToMove = ChessColor.Black;
            var rook = new Rook(ChessColor.Black);
            var pawnWhite = new Pawn(ChessColor.White);

            board.Move(new NormalMove(rook, Square.H8, Square.H7, null));

            // some moves to bring black rook back to origin position
            board.Move(new NormalMove(pawnWhite, Square.A2, Square.A3, null));
            board.Move(new NormalMove(rook, Square.H7, Square.H8, null));
            board.Move(new NormalMove(pawnWhite, Square.A3, Square.A4, null));

            // get legal moves of king. should not include castling.
            var king = new King(ChessColor.Black);
            var kingMoves = king.GetMoves(generator, board, Square.E8, true);

            Assert.AreEqual(false, kingMoves.Contains(new CastlingMove(MantaChessEngine.CastlingType.BlackKingSide, new King(ChessColor.Black))), "e8g8. 0-0 castling missing");
            Assert.AreEqual(true, kingMoves.Contains(new CastlingMove(MantaChessEngine.CastlingType.BlackQueenSide, new King(ChessColor.Black))), "e8c8. 0-0-0 castling missing");
        }

        [TestMethod]
        public void GetMovesTest_WhenBlackQueenRookMoved_ThenCastlingRightQueenSideLost()
        {
            MoveGenerator generator = new MoveGenerator(board);
            string position = "r...k..r" +
                              "........" +
                              "p......." +
                              "........" +
                              "........" +
                              "........" +
                              "P......." +
                              "R...K..R";
            board.SetPosition(position);

            // Black king side rook moves -> loses castling right
            board.BoardState.SideToMove = ChessColor.Black;
            var rook = new Rook(ChessColor.Black);
            var pawnWhite = new Pawn(ChessColor.White);

            board.Move(new NormalMove(rook, Square.A8, Square.A7, null));

            // some moves to bring black rook back to origin position
            board.Move(new NormalMove(pawnWhite, Square.A2, Square.A3, null));
            board.Move(new NormalMove(rook, Square.A7, Square.A8, null));
            board.Move(new NormalMove(pawnWhite, Square.A3, Square.A4, null));


            // get legal moves of king. should not include castling.
            var king = new King(ChessColor.Black);
            var kingMoves = king.GetMoves(generator, board, Square.E8, true);

            Assert.AreEqual(true, kingMoves.Contains(new CastlingMove(MantaChessEngine.CastlingType.BlackKingSide, new King(ChessColor.Black))), "e8g8. 0-0 castling missing");
            Assert.AreEqual(false, kingMoves.Contains(new CastlingMove(MantaChessEngine.CastlingType.BlackQueenSide, new King(ChessColor.Black))), "e8c8. 0-0-0 castling missing");
        }

        [TestMethod]
        public void GetMovesTest_KiwipetePosition_WhenKingInCheck_ThenNoCastlingPossible()
        {
            // Kiwipete position
            // Position is after "r3k2r/p1ppqpb1/bn2pnp1/3PN3/1p2P3/2N2Q1p/PPPBBPPP/R3K2R w KQkq - 0 0"
            // and move d5 x e6     d7-d5
            //          e6 x f7

            MoveGenerator generator = new MoveGenerator(board);
            string position = "r...k..r" +
                              "p.ppqpb." +
                              "bn..pnp." +
                              "...PN..." +
                              ".p..P..." +
                              "..N..Q.p" +
                              "PPPBBPPP" +
                              "R...K..R";
            board.SetPosition(position);

            // White pawn captures d5 x e6
            board.BoardState.SideToMove = ChessColor.White;
            var pawnW = new Pawn(ChessColor.White);
            var pawnWMove = new NormalMove(pawnW, Square.D5, Square.E6, new Pawn(ChessColor.Black));
            board.Move(pawnWMove);

            // Black pawn move d7-d5
            var pawnB = new Pawn(ChessColor.Black);
            var pawnBMove = new NormalMove(pawnB, Square.D7, Square.D5, null);
            board.Move(pawnBMove);

            // white pawn move e6 X f7 (results black king is in check)
            var pawnWMoveEnCap = new EnPassantCaptureMove(pawnW, Square.E6, Square.F7, pawnB);
            board.Move(pawnWMoveEnCap);

            // get legal moves of king. should not include castling.
            var king = new King(ChessColor.Black);
            var kingMoves = king.GetMoves(generator, board, Square.E8, true);

            Assert.AreEqual(false, kingMoves.Contains(new CastlingMove(MantaChessEngine.CastlingType.BlackKingSide, new King(ChessColor.Black))), "e8g8. 0-0 castling not possible, king in check.");
            Assert.AreEqual(false, kingMoves.Contains(new CastlingMove(MantaChessEngine.CastlingType.BlackQueenSide, new King(ChessColor.Black))), "e8c8. 0-0-0 castling not possible, king in check.");
        }

        [TestMethod]
        public void GetMovesTest_KiwipetePosition_PawnAttacksFieldsNextToBlackKing_ThenNoCastlingPossible()
        {
            // Kiwipete position
            // position fen r3k2r/p1ppqpb1/bn2pnp1/3PN3/1p2P3/2N2Q1p/PPPBBPPP/R3K2R w KQkq - 0 0 moves d5e6 e7c5 e6e7

            MoveGenerator generator = new MoveGenerator(board);
            string position = "r...k..r" +
                              "p.ppP.b." +
                              "bn...np." +
                              "..q.N..." +
                              ".p..P..." +
                              "..N..Q.p" +
                              "PPPBBPPP" +
                              "R...K..R";
            board.SetPosition(position);

            board.BoardState.SideToMove = ChessColor.Black;

            // get legal moves of king. should not include castling.
            var king = new King(ChessColor.Black);
            var kingMoves = king.GetMoves(generator, board, Square.E8, true);

            Assert.AreEqual(false, kingMoves.Contains(new CastlingMove(MantaChessEngine.CastlingType.BlackKingSide, new King(ChessColor.Black))), "e8g8. 0-0 castling not possible. f8 attacked.");
            Assert.AreEqual(false, kingMoves.Contains(new CastlingMove(MantaChessEngine.CastlingType.BlackQueenSide, new King(ChessColor.Black))), "e8c8. 0-0-0 castling not possible. d8 attacked.");
        }

        // ----------------------------------------------------------------
        // IsCheck Test
        // ----------------------------------------------------------------

        [TestMethod]
        public void IsCheckTest_WhenKingAttacked_ThenTrue()
        {
            MantaEngine engine = new MantaEngine(EngineType.MinimaxPosition, 256);
            string position = "....rk.." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "....K...";
            engine.SetPosition(position);

            Assert.AreEqual(true, engine.IsCheck(ChessColor.White), "king is attacked by rook! IsCheck should return true.");
        }

        [TestMethod]
        public void IsCheckTest_WhenKingNotAttacked_ThenFalse()
        {
            MantaEngine engine = new MantaEngine(EngineType.MinimaxPosition, 256);
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

            Assert.AreEqual(false, engine.IsCheck(ChessColor.White), "king is not attacked! IsCheck shoult return false.");
        }

        [TestMethod]
        public void IsCheckTest_WhenPawnInRank7CanPromote_ThenKingNotInCheck()
        {
            MantaEngine engine = new MantaEngine(EngineType.MinimaxPosition, 256);
            string position = "r...k..." +
                              "pppp...P" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "....K...";
            board.SetPosition(position);
            engine.SetBoard(board);

            Assert.AreEqual(false, engine.IsCheck(ChessColor.Black), "king is not attacked! Promotion move does not count.");
        }

        // ----------------------------------------------------------------
        // IsAttacked Test
        // ----------------------------------------------------------------

        [TestMethod]
        public void IsAttackedTest_FieldDiagonalOfPawnIsAttacked()
        {
            MoveGenerator generator = new MoveGenerator(board);
            
            string position = "r...k..." +
                              "...p...." +
                              "........" +
                              ".......r" +
                              "........" +
                              "........" +
                              "....P..." +
                              "....K..R";
            board.SetPosition(position);
           
            // field in front of pawn
            Assert.AreEqual(false, generator.IsAttacked(ChessColor.Black, Square.E3), "Field in front of pawn is not attacked");
            // field diagonal in front of pawn
            Assert.AreEqual(true, generator.IsAttacked(ChessColor.Black, Square.D3), "Field diagonal in front of pawn is attacked");
            Assert.AreEqual(true, generator.IsAttacked(ChessColor.Black, Square.F3), "Field diagonal in front of pawn is attacked");

            // black rook is attacked
            Assert.AreEqual(true, generator.IsAttacked(ChessColor.Black, Square.H5), "Black rook is attacked");
            // fields between the rooks is attacked by white rook
            Assert.AreEqual(true, generator.IsAttacked(ChessColor.Black, Square.H5), "Fields between the rooks is attacked by the white rook");
            Assert.AreEqual(true, generator.IsAttacked(ChessColor.Black, Square.H4), "Fields between the rooks is attacked by the white rook");
            Assert.AreEqual(true, generator.IsAttacked(ChessColor.Black, Square.H3), "Fields between the rooks is attacked by the white rook");
            Assert.AreEqual(true, generator.IsAttacked(ChessColor.Black, Square.H2), "Fields between the rooks is attacked by the white rook");
        }
    }
}