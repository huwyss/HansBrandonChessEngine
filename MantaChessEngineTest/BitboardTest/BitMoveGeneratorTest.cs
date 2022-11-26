using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MantaChessEngine;
using static MantaChessEngine.Definitions;
using System.Linq;

namespace MantaChessEngineTest
{
    [TestClass]
    public class BitMoveGeneratorTest
    {
        // ----------------------------------------------------------------------------------------------------
        // Get Moves Tests
        // ----------------------------------------------------------------------------------------------------

        [TestMethod]
        public void GetMoves_WhenWhiteKnight_ThenValidMoves()
        {
            Bitboards board = new Bitboards();
            board.Initialize();
            board.SetInitialPosition();
            var bitMoveGenerator = new BitMoveGenerator(board);

            var moves = bitMoveGenerator.GetKnightMoves(BitColor.White).ToList();

            Assert.AreEqual(4, moves.Count);

            Assert.IsTrue(moves.Contains(new BitMove(BitPieceType.Knight, Square.B1, Square.A3, 0)), "Nb1-a3 missing");
            Assert.IsTrue(moves.Contains(new BitMove(BitPieceType.Knight, Square.B1, Square.C3, 0)), "Nb1-c3 missing");
            Assert.IsTrue(moves.Contains(new BitMove(BitPieceType.Knight, Square.G1, Square.F3, 0)), "Ng1-f3 missing");
            Assert.IsTrue(moves.Contains(new BitMove(BitPieceType.Knight, Square.G1, Square.H3, 0)), "Ng1-h3 missing");
        }

        [TestMethod]
        public void GetMoves_WhenWhiteKnight_ThenValidCaptures()
        {
            Bitboards board = new Bitboards();
            board.Initialize();
            board.SetPosition("rnbqkbnr" +
                              "N......." +
                              "......N." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "........");
            var bitMoveGenerator = new BitMoveGenerator(board);

            var moves = bitMoveGenerator.GetKnightMoves(BitColor.White).ToList();

            Assert.AreEqual(9, moves.Count);

            Assert.IsTrue(moves.Contains(new BitMove(BitPieceType.Knight, Square.A7, Square.C8, BitPieceType.Bishop, Square.C8, BitPieceType.Empty, 0)), "Na7-c8 missing");
            Assert.IsTrue(moves.Contains(new BitMove(BitPieceType.Knight, Square.A7, Square.C6, 0)), "Na7-c6 missing");
            Assert.IsTrue(moves.Contains(new BitMove(BitPieceType.Knight, Square.A7, Square.B5, 0)), "Na7-b5 missing");

            Assert.IsTrue(moves.Contains(new BitMove(BitPieceType.Knight, Square.G6, Square.H8, BitPieceType.Rook, Square.H8, BitPieceType.Empty, 0)), "Ng6xh8 missing");
            Assert.IsTrue(moves.Contains(new BitMove(BitPieceType.Knight, Square.G6, Square.F8, BitPieceType.Bishop, Square.F8, BitPieceType.Empty, 0)), "Ng6xf8 missing");
            Assert.IsTrue(moves.Contains(new BitMove(BitPieceType.Knight, Square.G6, Square.E7, 0)), "Ng6-e7 missing");
            Assert.IsTrue(moves.Contains(new BitMove(BitPieceType.Knight, Square.G6, Square.E5, 0)), "Ng6-e5 missing");
            Assert.IsTrue(moves.Contains(new BitMove(BitPieceType.Knight, Square.G6, Square.F4, 0)), "Ng6-f4 missing");
            Assert.IsTrue(moves.Contains(new BitMove(BitPieceType.Knight, Square.G6, Square.H4, 0)), "Ng6-h4 missing");
        }

        [TestMethod]
        public void GetMoves_WhenBlackKnight_ThenValidCaptures()
        {
            Bitboards board = new Bitboards();
            board.Initialize();
            board.SetPosition("r.bqkb.r" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "......R." +
                              ".....N.." +
                              "..KQ...n");
            var bitMoveGenerator = new BitMoveGenerator(board);

            var moves = bitMoveGenerator.GetKnightMoves(BitColor.Black).ToList();

            Assert.AreEqual(2, moves.Count);

            Assert.IsTrue(moves.Contains(new BitMove(BitPieceType.Knight, Square.H1, Square.G3, BitPieceType.Rook, Square.G3, BitPieceType.Empty, 0)), "Nh1xg3 missing");
            Assert.IsTrue(moves.Contains(new BitMove(BitPieceType.Knight, Square.H1, Square.F2, BitPieceType.Knight, Square.F2, BitPieceType.Empty, 0)), "Nh1xf2 missing");
        }

        [TestMethod]
        public void GetMoves_WhenBlackKnight_ThenValidMoves()
        {
            Bitboards board = new Bitboards();
            board.Initialize();
            board.SetInitialPosition();
            var bitMoveGenerator = new BitMoveGenerator(board);

            var moves = bitMoveGenerator.GetKnightMoves(BitColor.Black).ToList();

            Assert.AreEqual(4, moves.Count);

            Assert.IsTrue(moves.Contains(new BitMove(BitPieceType.Knight, Square.B8, Square.A6, 0)), "Nb8-a6 missing");
            Assert.IsTrue(moves.Contains(new BitMove(BitPieceType.Knight, Square.B8, Square.C6, 0)), "Nb8-c6 missing");
            Assert.IsTrue(moves.Contains(new BitMove(BitPieceType.Knight, Square.G8, Square.F6, 0)), "Ng8-f6 missing");
            Assert.IsTrue(moves.Contains(new BitMove(BitPieceType.Knight, Square.G8, Square.H6, 0)), "Ng8-h6 missing");
        }

        [TestMethod]
        public void GetMoves_WhenRook_ThenAllMoves()
        {
            Bitboards board = new Bitboards();
            board.Initialize();
            board.SetPosition("........" +
                              "........" +
                              "....p..." +
                              "........" +
                              "....R..P" +
                              "........" +
                              "........" +
                              "........");
            var bitMoveGenerator = new BitMoveGenerator(board);

            var moves = bitMoveGenerator.GetRookMoves(BitColor.White).ToList();

            Assert.AreEqual(11, moves.Count);
            Assert.IsTrue(moves.Contains(new BitMove(BitPieceType.Rook, Square.E4, Square.E5, 0)), "Re4-e5 missing");
            Assert.IsTrue(moves.Contains(new BitMove(BitPieceType.Rook, Square.E4, Square.E6, BitPieceType.Pawn, Square.NoSquare, BitPieceType.Empty, 0)), "Re4xe6 missing"); // capture pawn
            Assert.IsTrue(moves.Contains(new BitMove(BitPieceType.Rook, Square.E4, Square.F4, 0)), "Re4-f4 missing");
            Assert.IsTrue(moves.Contains(new BitMove(BitPieceType.Rook, Square.E4, Square.G4, 0)), "Re4-g4 missing");
            Assert.IsTrue(moves.Contains(new BitMove(BitPieceType.Rook, Square.E4, Square.E3, 0)), "Re4-e3 missing");
            Assert.IsTrue(moves.Contains(new BitMove(BitPieceType.Rook, Square.E4, Square.E2, 0)), "Re4-e2 missing");
            Assert.IsTrue(moves.Contains(new BitMove(BitPieceType.Rook, Square.E4, Square.E1, 0)), "Re4-e1 missing");
            Assert.IsTrue(moves.Contains(new BitMove(BitPieceType.Rook, Square.E4, Square.D4, 0)), "Re4-d4 missing");
            Assert.IsTrue(moves.Contains(new BitMove(BitPieceType.Rook, Square.E4, Square.C4, 0)), "Re4-c4 missing");
            Assert.IsTrue(moves.Contains(new BitMove(BitPieceType.Rook, Square.E4, Square.B4, 0)), "Re4-b4 missing");
            Assert.IsTrue(moves.Contains(new BitMove(BitPieceType.Rook, Square.E4, Square.A4, 0)), "Re4-a4 missing");
        }

        [TestMethod]
        public void GetMoves_WhenQueen_ThenAllMoves()
        {
            Bitboards board = new Bitboards();
            board.Initialize();
            board.SetPosition("........" +
                              "........" +
                              "........" +
                              "........" +
                              ".r.B...." +
                              "........" +
                              ".Q.P...." +
                              "........");
            var bitMoveGenerator = new BitMoveGenerator(board);

            var moves = bitMoveGenerator.GetQueenMoves(BitColor.White).ToList();

            Assert.AreEqual(9, moves.Count);
            Assert.IsTrue(moves.Contains(new BitMove(BitPieceType.Queen, Square.B2, Square.B3, 0)), "Qb2-b3 missing");
            Assert.IsTrue(moves.Contains(new BitMove(BitPieceType.Queen, Square.B2, Square.B4, BitPieceType.Rook, Square.B4, BitPieceType.Empty, 0)), "Qb2xb4 missing");
            Assert.IsTrue(moves.Contains(new BitMove(BitPieceType.Queen, Square.B2, Square.C3, 0)), "Qb2-c3 missing");
            Assert.IsTrue(moves.Contains(new BitMove(BitPieceType.Queen, Square.B2, Square.C2, 0)), "Qb2-c2 missing");
            Assert.IsTrue(moves.Contains(new BitMove(BitPieceType.Queen, Square.B2, Square.C1, 0)), "Qb2-c1 missing");
            Assert.IsTrue(moves.Contains(new BitMove(BitPieceType.Queen, Square.B2, Square.B1, 0)), "Qb2-b1 missing");
            Assert.IsTrue(moves.Contains(new BitMove(BitPieceType.Queen, Square.B2, Square.A1, 0)), "Qb2-a1 missing");
            Assert.IsTrue(moves.Contains(new BitMove(BitPieceType.Queen, Square.B2, Square.A2, 0)), "Qb2-a2 missing");
            Assert.IsTrue(moves.Contains(new BitMove(BitPieceType.Queen, Square.B2, Square.A3, 0)), "Qb2-a3 missing");
        }

        [TestMethod]
        public void GetMoves_WhenBishop_ThenAllMoves()
        {
            Bitboards board = new Bitboards();
            board.Initialize();
            board.SetPosition("........" +
                              "........" +
                              "........" +
                              "........" +
                              "r...N..." +
                              "........" +
                              "..B....." +
                              "........");
            var bitMoveGenerator = new BitMoveGenerator(board);

            var moves = bitMoveGenerator.GetBishopMoves(BitColor.White).ToList();

            Assert.AreEqual(5, moves.Count);
            Assert.IsTrue(moves.Contains(new BitMove(BitPieceType.Bishop, Square.C2, Square.D3, 0)), "Bc2-d3 missing");
            Assert.IsTrue(moves.Contains(new BitMove(BitPieceType.Bishop, Square.C2, Square.D1, 0)), "Bc2-d1 missing");
            Assert.IsTrue(moves.Contains(new BitMove(BitPieceType.Bishop, Square.C2, Square.B1, 0)), "Bc2-b1 missing");
            Assert.IsTrue(moves.Contains(new BitMove(BitPieceType.Bishop, Square.C2, Square.B3, 0)), "Bc2-b3 missing");
            Assert.IsTrue(moves.Contains(new BitMove(BitPieceType.Bishop, Square.C2, Square.A4, BitPieceType.Rook, Square.A4, BitPieceType.Empty, 0)), "Bc2xa4 missing");
        }

        [TestMethod]
        public void GetMoves_WhenKing_ThenAllMoves()
        {
            Bitboards board = new Bitboards();
            board.Initialize();
            board.SetPosition("........" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              ".rB....." +
                              ".KP.....");
            var bitMoveGenerator = new BitMoveGenerator(board);

            var moves = bitMoveGenerator.GetKingMoves(BitColor.White).ToList();

            Assert.AreEqual(3, moves.Count);
            Assert.IsTrue(moves.Contains(new BitMove(BitPieceType.King, Square.B2, Square.B3, BitPieceType.Rook, Square.B3, BitPieceType.Empty, 0)), "Kb1xb2 missing");
            Assert.IsTrue(moves.Contains(new BitMove(BitPieceType.King, Square.B2, Square.A1, 0)), "Kb1-a1 missing");
            Assert.IsTrue(moves.Contains(new BitMove(BitPieceType.King, Square.B2, Square.A2, 0)), "Kb1-a2 missing");
        }

        //
        // Pawn moves
        // 

        [TestMethod]
        public void GetMoves_WhenWhitePawn_ThenAllMoves() // en passant not included
        {
            Bitboards board = new Bitboards();
            board.Initialize();
            board.SetPosition("........" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              ".r.b...." +
                              "..P....." +
                              "........");
            var bitMoveGenerator = new BitMoveGenerator(board);

            var moves = bitMoveGenerator.GetPawnMoves(BitColor.White).ToList();

            Assert.AreEqual(4, moves.Count);
            
            Assert.IsTrue(moves.Contains(new BitMove(BitPieceType.Pawn, Square.C2, Square.C3, 0)), "c2-c3 missing");
            Assert.IsTrue(moves.Contains(new BitMove(BitPieceType.Pawn, Square.C2, Square.C4, 0)), "c2-c4 missing");
            Assert.IsTrue(moves.Contains(new BitMove(BitPieceType.Pawn, Square.C2, Square.B3, BitPieceType.Rook, Square.B3, BitPieceType.Empty, 0)), "c2xb3 missing");
            Assert.IsTrue(moves.Contains(new BitMove(BitPieceType.Pawn, Square.C2, Square.D3, BitPieceType.Bishop, Square.D3, BitPieceType.Empty, 0)), "c2xd3 missing");
        }

        [TestMethod]
        public void GetMoves_WhenWhitePawnCaptureLeft_ThenAllMoves()
        {
            Bitboards board = new Bitboards();
            board.Initialize();
            board.SetPosition("........" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "nb....rR" +
                              "P.....PP" +
                              "........");
            var bitMoveGenerator = new BitMoveGenerator(board);

            var moves = bitMoveGenerator.GetPawnMoves(BitColor.White).ToList();

            Assert.AreEqual(2, moves.Count);
            Assert.IsTrue(moves.Contains(new BitMove(BitPieceType.Pawn, Square.A2, Square.B3, BitPieceType.Bishop, Square.B3, BitPieceType.Empty, 0)), "A2xB3 missing");
            Assert.IsTrue(moves.Contains(new BitMove(BitPieceType.Pawn, Square.H2, Square.G3, BitPieceType.Rook, Square.G3, BitPieceType.Empty, 0)), "H2xG3 missing");
        }

        [TestMethod]
        public void GetMoves_WhenWhitePawnCaptureRight_ThenAllMoves()
        {
            Bitboards board = new Bitboards();
            board.Initialize();
            board.SetPosition("........" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "nb....Rr" +
                              "P.....PP" +
                              "........");
            var bitMoveGenerator = new BitMoveGenerator(board);

            var moves = bitMoveGenerator.GetPawnMoves(BitColor.White).ToList();

            Assert.AreEqual(2, moves.Count);
            Assert.IsTrue(moves.Contains(new BitMove(BitPieceType.Pawn, Square.A2, Square.B3, BitPieceType.Bishop, Square.B3, BitPieceType.Empty, 0)), "A2xB3 missing");
            Assert.IsTrue(moves.Contains(new BitMove(BitPieceType.Pawn, Square.G2, Square.H3, BitPieceType.Rook, Square.H3, BitPieceType.Empty, 0)), "G2xH3 missing");
        }

        [TestMethod]
        public void GetMoves_WhenWhitePawnCaptureLeftAndRight_ThenAllMoves()
        {
            Bitboards board = new Bitboards();
            board.Initialize();
            board.SetPosition("........" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "nb.....r" +
                              "PP.....P" +
                              "........");
            var bitMoveGenerator = new BitMoveGenerator(board);

            var moves = bitMoveGenerator.GetPawnMoves(BitColor.White).ToList();

            Assert.AreEqual(2, moves.Count);
            Assert.IsTrue(moves.Contains(new BitMove(BitPieceType.Pawn, Square.A2, Square.B3, BitPieceType.Bishop, Square.B3, BitPieceType.Empty, 0)), "a2xb3 missing");
            Assert.IsTrue(moves.Contains(new BitMove(BitPieceType.Pawn, Square.B2, Square.A3, BitPieceType.Knight, Square.A3, BitPieceType.Empty, 0)), "b2xa3 missing");
        }

        [TestMethod]
        public void GetMoves_WhenWhitePawnBlocked_ThenAllMoves() // en passant not included
        {
            Bitboards board = new Bitboards();
            board.Initialize();
            board.SetPosition("........" +
                              "........" +
                              "........" +
                              "........" +
                              "..n....." +
                              ".r.b...." +
                              "..P....." +
                              "........");
            var bitMoveGenerator = new BitMoveGenerator(board);

            var moves = bitMoveGenerator.GetPawnMoves(BitColor.White).ToList();

            Assert.AreEqual(3, moves.Count);
            Assert.IsTrue(moves.Contains(new BitMove(BitPieceType.Pawn, Square.C2, Square.C3, 0)), "c2-c3 missing");
            Assert.IsTrue(moves.Contains(new BitMove(BitPieceType.Pawn, Square.C2, Square.B3, BitPieceType.Rook, Square.B3, BitPieceType.Empty, 0)), "C2xB3 missing");
            Assert.IsTrue(moves.Contains(new BitMove(BitPieceType.Pawn, Square.C2, Square.D3, BitPieceType.Bishop, Square.D3, BitPieceType.Empty, 0)), "C2xD3 missing");
        }

        [TestMethod]
        public void GetMoves_WhenWhitePawnBlockedInMiddleOfBoard_ThenAllMoves()
        {
            Bitboards board = new Bitboards();
            board.Initialize();
            board.SetPosition("........" +
                              "........" + 
                              "..p....." + // black pawn
                              "..P....." + // white pawn must not do c5-c7 !
                              "........" +
                              "........" +
                              "........" +
                              "........");
            var bitMoveGenerator = new BitMoveGenerator(board);

            var moves = bitMoveGenerator.GetPawnMoves(BitColor.White).ToList();

            Assert.AreEqual(0, moves.Count);
        }

        [TestMethod]
        public void GetMoves_WhenWhitePawnAtA5_ThenAllMoves()
        {
            Bitboards board = new Bitboards();
            board.Initialize();
            board.SetPosition("........" +
                              "........" + 
                              "........" + 
                              "........" +
                              "........" +
                              "..p....." + // black pawn
                              "..P....." + // white pawn must not do c2-c4 !
                              "........");
            var bitMoveGenerator = new BitMoveGenerator(board);

            var moves = bitMoveGenerator.GetPawnMoves(BitColor.White).ToList();

            Assert.AreEqual(0, moves.Count);
        }

        [TestMethod, Ignore]
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
            board.Move(new NormalMove(new Pawn(ChessColor.Black),'a',7,'a',5,null));
            Assert.AreEqual(Helper.FileCharToFile('a'), board.BoardState.LastEnPassantFile);
            Assert.AreEqual(6, board.BoardState.LastEnPassantRank);

            var pawn = new Pawn(ChessColor.White);
            var moves = pawn.GetMoves(null, board, Helper.FileCharToFile('b'), 5, true); // white pawn

            Assert.AreEqual(2, moves.Count);
            Assert.AreEqual(true, moves.Contains(new EnPassantCaptureMove(Piece.MakePiece('P'), 'b', 5, 'a', 6, Piece.MakePiece('p'))), "b5a6pe en passant missing");
            Assert.AreEqual(true, moves.Contains(new NormalMove(Piece.MakePiece('P'), 'b', 5, 'b', 6, null)), "b5b6. missing");
        }

        [TestMethod]
        public void GetMoves_WhenBlackPawn_ThenAllMoves()
        {
            Bitboards board = new Bitboards();
            board.Initialize();
            board.SetPosition("........" +
                              "..p....." +
                              ".R.B...." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "........");
            var bitMoveGenerator = new BitMoveGenerator(board);

            var moves = bitMoveGenerator.GetPawnMoves(BitColor.Black).ToList();

            Assert.AreEqual(4, moves.Count);
            Assert.IsTrue(moves.Contains(new BitMove(BitPieceType.Pawn, Square.C7, Square.C6, 0)), "C7-C6 missing");
            Assert.IsTrue(moves.Contains(new BitMove(BitPieceType.Pawn, Square.C7, Square.C5, 0)), "C7-C5 missing");
            Assert.IsTrue(moves.Contains(new BitMove(BitPieceType.Pawn, Square.C7, Square.B6, BitPieceType.Rook, Square.B6, BitPieceType.Empty, 0)), "C7xB6 missing");
            Assert.IsTrue(moves.Contains(new BitMove(BitPieceType.Pawn, Square.C7, Square.D6, BitPieceType.Bishop, Square.D6, BitPieceType.Empty, 0)), "C7xD6 missing");
        }

        [TestMethod]
        public void GetMoves_WhenBlackPawnAta4_ThenAllMoves()
        {
            Bitboards board = new Bitboards();
            board.Initialize();
            board.SetPosition("........" +
                              "........" +
                              "........" +
                              ".p......" + // black pawn must not do b5-b3 !
                              "........" + 
                              "........" +
                              "........" +
                              "........");
            var bitMoveGenerator = new BitMoveGenerator(board);

            var moves = bitMoveGenerator.GetPawnMoves(BitColor.Black).ToList();

            Assert.AreEqual(1, moves.Count);
            Assert.IsTrue(moves.Contains(new BitMove(BitPieceType.Pawn, Square.B5, Square.B4, BitPieceType.Empty, Square.NoSquare, BitPieceType.Empty, 0)), "B5-B4 missing");
        }

        [TestMethod, Ignore]
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

            var pawn = new Pawn(ChessColor.White);
            var moves = pawn.GetMoves(null, board, Helper.FileCharToFile('b'), 7, true); // white pawn

            Assert.AreEqual(4, moves.Count);
            Assert.AreEqual(true, moves.Contains(new PromotionMove(Piece.MakePiece('P'), 'b', 7, 'b', 8, null, Definitions.QUEEN)), "b7b8 missing (promotion to queen walking straight)");
            Assert.AreEqual(true, moves.Contains(new PromotionMove(Piece.MakePiece('P'), 'b', 7, 'b', 8, null, Definitions.ROOK)), "b7b8 missing (promotion to rook walking straight)");
            Assert.AreEqual(true, moves.Contains(new PromotionMove(Piece.MakePiece('P'), 'b', 7, 'b', 8, null, Definitions.BISHOP)), "b7b8 missing (promotion to bishop walking straight)");
            Assert.AreEqual(true, moves.Contains(new PromotionMove(Piece.MakePiece('P'), 'b', 7, 'b', 8, null, Definitions.KNIGHT)), "b7b8 missing (promotion to knight walking straight)");
        }

        [TestMethod, Ignore]
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

            var pawn = new Pawn(ChessColor.Black);
            var moves = pawn.GetMoves(null, board, Helper.FileCharToFile('b'), 2, true); // black pawn

            Assert.AreEqual(4, moves.Count);
            Assert.AreEqual(true, moves.Contains(new PromotionMove(Piece.MakePiece('p'), 'b', 2, 'b', 1, null, Definitions.QUEEN)), "b2b1 missing (promotion to queen walking straight)");
            Assert.AreEqual(true, moves.Contains(new PromotionMove(Piece.MakePiece('p'), 'b', 2, 'b', 1, null, Definitions.ROOK)), "b2b1 missing (promotion to rook walking straight)");
            Assert.AreEqual(true, moves.Contains(new PromotionMove(Piece.MakePiece('p'), 'b', 2, 'b', 1, null, Definitions.BISHOP)), "b2b1 missing (promotion to bishop walking straight)");
            Assert.AreEqual(true, moves.Contains(new PromotionMove(Piece.MakePiece('p'), 'b', 2, 'b', 1, null, Definitions.KNIGHT)), "b2b1 missing (promotion to knight walking straight)");
        }

        [TestMethod, Ignore]
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

            var pawn = new Pawn(ChessColor.White);
            var moves = pawn.GetMoves(null, board, Helper.FileCharToFile('b'), 7, true); // white pawn

            Assert.AreEqual(4, moves.Count);
            Assert.AreEqual(true, moves.Contains(new PromotionMove(Piece.MakePiece('P'), 'b', 7, 'a', 8, Piece.MakePiece('r'), Definitions.QUEEN)), "b7a8 missing (promotion to queen when capturing)");
            Assert.AreEqual(true, moves.Contains(new PromotionMove(Piece.MakePiece('P'), 'b', 7, 'a', 8, Piece.MakePiece('r'), Definitions.ROOK)), "b7a8 missing (promotion to rook when capturing)");
            Assert.AreEqual(true, moves.Contains(new PromotionMove(Piece.MakePiece('P'), 'b', 7, 'a', 8, Piece.MakePiece('r'), Definitions.BISHOP)), "b7a8 missing (promotion to bishop when capturing)");
            Assert.AreEqual(true, moves.Contains(new PromotionMove(Piece.MakePiece('P'), 'b', 7, 'a', 8, Piece.MakePiece('r'), Definitions.KNIGHT)), "b7a8 missing (promotion to knight when capturing)");
        }

        [TestMethod, Ignore]
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

            var pawn = new Pawn(ChessColor.Black);
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

        [TestMethod, Ignore]
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
           
            var moves = target.GetAllMoves(board, Definitions.ChessColor.White).ToList<IMove>();

            Assert.AreEqual(6, moves.Count);
            Assert.AreEqual(true, moves.Contains(new NormalMove(Piece.MakePiece('P'), 'e', 2, 'e', 3, null)), "e2e3. missing");
            Assert.AreEqual(true, moves.Contains(new NormalMove(Piece.MakePiece('P'), 'e', 2, 'e', 4, null)), "e2e4. missing");
            Assert.AreEqual(true, moves.Contains(new NormalMove(Piece.MakePiece('K'), 'e', 1, 'd', 1, null)), "e1d1. missing");
            Assert.AreEqual(true, moves.Contains(new NormalMove(Piece.MakePiece('K'), 'e', 1, 'd', 2, null)), "e1d2. missing");
            Assert.AreEqual(true, moves.Contains(new NormalMove(Piece.MakePiece('K'), 'e', 1, 'f', 2, null)), "e1f2. missing");
            Assert.AreEqual(true, moves.Contains(new NormalMove(Piece.MakePiece('K'), 'e', 1, 'f', 1, null)), "e1f1. missing");
        }

        [TestMethod, Ignore]
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

            var moves = target.GetAllMoves(board, ChessColor.White).ToList<IMove>();

            Assert.AreEqual(0, moves.Count, "white has no king. so no white moves possible.");
        }
        
        // ----------------------------------------------------------------------------------------------------
        // Is Move Valid Tests
        // ----------------------------------------------------------------------------------------------------

        [TestMethod, Ignore]
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

        [TestMethod, Ignore]
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

        [TestMethod, Ignore]
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
            board.BoardState.SideToMove = ChessColor.Black;
            
            bool valid = target.IsMoveValid(board, new NormalMove(Piece.MakePiece('p'), 'c', 7, 'c', 5, null)); // pawn
            Assert.AreEqual(true, valid, "Move should be valid.");

            valid = target.IsMoveValid(board, new NormalMove(Piece.MakePiece('p'), 'c', 7, 'c', 6, null)); // pawn
            Assert.AreEqual(true, valid, "Move should be valid.");
        }

        [TestMethod, Ignore]
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

        [TestMethod, Ignore]
        public void GetEndPosition_WhenKnightSequence_ThenCorrectEndRankFile()
        {
            string knightSequence = "uul";

            var target = new MoveGenerator(new MoveFactory());
            target.GetEndPosition(Helper.FileCharToFile('b'), 1, knightSequence, out int targetFile, out int targetRank, out bool valid);

            Assert.AreEqual(Helper.FileCharToFile('a'), targetFile);
            Assert.AreEqual(3, targetRank);
            Assert.AreEqual(true, valid);
        }

        [TestMethod, Ignore]
        public void GetEndPosition_WhenOtherKnightSequence_ThenCorrectEndRankFile()
        {
            string knightSequence = "ddr";

            var target = new MoveGenerator(new MoveFactory());
            target.GetEndPosition(Helper.FileCharToFile('c'), 3, knightSequence, out int targetFile, out int targetRank, out bool valid);

            Assert.AreEqual(Helper.FileCharToFile('d'), targetFile);
            Assert.AreEqual(1, targetRank);
            Assert.AreEqual(true, valid);
        }

        [TestMethod, Ignore]
        public void GetEndPosition_WhenKnightSequenceInvalid_ThenMoveInvalid()
        {
            string knightSequence = "ddl";

            var target = new MoveGenerator(new MoveFactory());
            target.GetEndPosition(Helper.FileCharToFile('a'), 1, knightSequence, out int targetFile, out int targetRank, out bool valid);

            Assert.AreEqual(false, valid);
        }

        [TestMethod, Ignore]
        public void GetCorrectMoveTest_Whene2e4_ThenAddDot()
        {
            var factory = new MoveFactory();
            MoveGenerator target = new MoveGenerator(factory);
            Board board = new Board();
            board.SetInitialPosition();

            IMove actualMove = factory.MakeMove(board, "e2e4");
            Assert.AreEqual("e2e4.", actualMove.ToString());
        }

        [TestMethod, Ignore]
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

        [TestMethod, Ignore]
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

        [TestMethod, Ignore]
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

        [TestMethod, Ignore]
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

            var king = new King(ChessColor.White);
            var kingMoves = king.GetMoves(generator, board, Helper.FileCharToFile('e'), 1, true);

            Assert.AreEqual(true, kingMoves.Contains(new CastlingMove(CastlingType.WhiteKingSide, new King(ChessColor.White))), "e1g1. 0-0 castling missing");
            Assert.AreEqual(true, kingMoves.Contains(new CastlingMove(CastlingType.WhiteQueenSide, new King(ChessColor.White))), "e1c1. 0-0-0 castling missing");
        }

        [TestMethod, Ignore]
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

            var king = new King(ChessColor.White);
            List<IMove> kingMoves = king.GetMoves(generator, board, Helper.FileCharToFile('d'), 1, true);
            Assert.AreEqual(false, kingMoves.Contains(new CastlingMove(CastlingType.WhiteKingSide, new King(ChessColor.White))), "e1g1. 0-0 castling not possible");
            Assert.AreEqual(false, kingMoves.Contains(new CastlingMove(CastlingType.WhiteQueenSide, new King(ChessColor.White))), "e1c1. 0-0-0 castling not possible");
        }

        [TestMethod, Ignore]
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

            var king = new King(ChessColor.White);
            List<IMove> kingMoves = king.GetMoves(generator, board, Helper.FileCharToFile('e'), 1, true);
            Assert.AreEqual(false, kingMoves.Contains(new CastlingMove(CastlingType.WhiteKingSide, new King(ChessColor.White))), "e1g1. 0-0 castling not possible");
            Assert.AreEqual(false, kingMoves.Contains(new CastlingMove(CastlingType.WhiteQueenSide, new King(ChessColor.White))), "e1c1. 0-0-0 castling not possible");
        }

        [TestMethod, Ignore]
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

            var king = new King(ChessColor.White);
            List<IMove> kingMoves = king.GetMoves(generator, board, Helper.FileCharToFile('e'), 1, true);
            Assert.AreEqual(false, kingMoves.Contains(new CastlingMove(CastlingType.WhiteKingSide, new King(ChessColor.White))), "e1g1. 0-0 castling not possible");
        }

        [TestMethod, Ignore]
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

            var king = new King(ChessColor.White);
            List<IMove> kingMoves = king.GetMoves(generator, board, Helper.FileCharToFile('e'), 1, true);
            Assert.AreEqual(false, kingMoves.Contains(new CastlingMove(CastlingType.WhiteQueenSide, new King(ChessColor.White))), "0-0-0 castling not possible");
        }

        [TestMethod, Ignore]
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

            var king = new King(ChessColor.White);
            List<IMove> kingMoves = king.GetMoves(generator, board, Helper.FileCharToFile('e'), 1, true);
            Assert.AreEqual(false, kingMoves.Contains(new CastlingMove(CastlingType.WhiteKingSide, new King(ChessColor.White))), "e1g1. 0-0 castling not possible");
            Assert.AreEqual(false, kingMoves.Contains(new CastlingMove(CastlingType.WhiteQueenSide, new King(ChessColor.White))), "e1c1. 0-0-0 castling not possible");
        }

        [TestMethod, Ignore]
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

            var king = new King(ChessColor.White);
            List<IMove> kingMoves = king.GetMoves(generator, board, Helper.FileCharToFile('e'), 1, true);
            Assert.AreEqual(false, kingMoves.Contains(new CastlingMove(CastlingType.WhiteKingSide, new King(ChessColor.White))), "e1g1. 0-0 castling not possible. g1 is attacked.");
            Assert.AreEqual(false, kingMoves.Contains(new CastlingMove(CastlingType.WhiteQueenSide, new King(ChessColor.White))), "e1c1. 0-0-0 castling not possible. c1 is attacked.");
        }

        /// -----------------------------------------------------------------------------------------
        /// White Castling rights lost after king or rook move
        /// -----------------------------------------------------------------------------------------

        [TestMethod, Ignore]
        public void GetMovesTest_WhenwhiteKingMoved_ThenCastlingRightLost()
        {
            MoveGenerator generator = new MoveGenerator(new MoveFactory());
            Board board = new Board();
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
            var kingMove = new NormalMove(king, 'e', 2, 'e', 1, null);
            board.Move(kingMove);

            // black move
            var pawn = new Pawn(ChessColor.Black);
            var pawnMove = new NormalMove(pawn, 'a', 7, 'a', 6, null);
            board.Move(pawnMove);

            // get legal moves of king. should not include castling.
            var kingMoves = king.GetMoves(generator, board, Helper.FileCharToFile('e'), 1, true);

            Assert.AreEqual(false, kingMoves.Contains(new CastlingMove(CastlingType.WhiteKingSide, new King(ChessColor.White))), "e1g1. 0-0 castling missing");
            Assert.AreEqual(false, kingMoves.Contains(new CastlingMove(CastlingType.WhiteQueenSide, new King(ChessColor.White))), "e1c1. 0-0-0 castling missing");
        }

        [TestMethod, Ignore]
        public void GetMovesTest_WhenWhiteKingRookMoved_ThenCastlingRightKingSideLost()
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
                              "R...K...";
            board.SetPosition(position);

            // white king side rook moves -> loses castling right
            board.BoardState.SideToMove = ChessColor.White;
            var rook = new Rook(ChessColor.White);
            var rookMove = new NormalMove(rook, 'h', 2, 'h', 1, null);
            board.Move(rookMove);

            // black move
            var pawn = new Pawn(ChessColor.Black);
            var pawnMove = new NormalMove(pawn, 'a', 7, 'a', 6, null);
            board.Move(pawnMove);

            // get legal moves of king. should not include castling.
            var king = new King(ChessColor.White);
            var kingMoves = king.GetMoves(generator, board, Helper.FileCharToFile('e'), 1, true);

            Assert.AreEqual(false, kingMoves.Contains(new CastlingMove(CastlingType.WhiteKingSide, new King(ChessColor.White))), "e1g1. 0-0 castling missing");
            Assert.AreEqual(true, kingMoves.Contains(new CastlingMove(CastlingType.WhiteQueenSide, new King(ChessColor.White))), "e1c1. 0-0-0 castling missing");
        }

        [TestMethod, Ignore]
        public void GetMovesTest_WhenWhiteQueenRookMoved_ThenCastlingRightQueenSideLost()
        {
            MoveGenerator generator = new MoveGenerator(new MoveFactory());
            Board board = new Board();
            string position = "r...k..r" +
                              "p......." +
                              "........" +
                              "........" +
                              "........" +
                              "P......." +
                              "R......." +
                              "....K..R";
            board.SetPosition(position);

            // white king side rook moves -> loses castling right
            board.BoardState.SideToMove = ChessColor.White;
            var rook = new Rook(ChessColor.White);
            var rookMove = new NormalMove(rook, 'a', 2, 'a', 1, null);
            board.Move(rookMove);

            // black move
            var pawn = new Pawn(ChessColor.Black);
            var pawnMove = new NormalMove(pawn, 'a', 7, 'a', 6, null);
            board.Move(pawnMove);

            // get legal moves of king. should not include castling.
            var king = new King(ChessColor.White);
            var kingMoves = king.GetMoves(generator, board, Helper.FileCharToFile('e'), 1, true);

            Assert.AreEqual(true, kingMoves.Contains(new CastlingMove(CastlingType.WhiteKingSide, new King(ChessColor.White))), "e1g1. 0-0 castling missing");
            Assert.AreEqual(false, kingMoves.Contains(new CastlingMove(CastlingType.WhiteQueenSide, new King(ChessColor.White))), "e1c1. 0-0-0 castling missing");
        }

        // black

        [TestMethod, Ignore]
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

            var king = new King(ChessColor.Black);
            List<IMove> kingMoves = king.GetMoves(generator, board, Helper.FileCharToFile('e'), 8, true);
            Assert.AreEqual(true, kingMoves.Contains(new CastlingMove(CastlingType.BlackKingSide, new King(ChessColor.Black))), "e8g8. 0-0 castling missing");
            Assert.AreEqual(true, kingMoves.Contains(new CastlingMove(CastlingType.BlackQueenSide, new King(ChessColor.Black))), "e8c8. 0-0-0 castling missing");
        }

        [TestMethod, Ignore]
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

            var king = new King(ChessColor.Black);
            List<IMove> kingMoves = king.GetMoves(generator, board, Helper.FileCharToFile('d'), 8, true);
            Assert.AreEqual(false, kingMoves.Contains(new CastlingMove(CastlingType.BlackKingSide, new King(ChessColor.Black))), "e1g1. 0-0 castling not possible");
            Assert.AreEqual(false, kingMoves.Contains(new CastlingMove(CastlingType.BlackQueenSide, new King(ChessColor.Black))), "e1c1. 0-0-0 castling not possible");
        }

        [TestMethod, Ignore]
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

            var king = new King(ChessColor.Black);
            List<IMove> kingMoves = king.GetMoves(generator, board, Helper.FileCharToFile('d'), 8, true);
            Assert.AreEqual(false, kingMoves.Contains(new CastlingMove(CastlingType.BlackKingSide, new King(ChessColor.Black))), "e1g1. 0-0 castling not possible");
            Assert.AreEqual(false, kingMoves.Contains(new CastlingMove(CastlingType.BlackQueenSide, new King(ChessColor.Black))), "e1c1. 0-0-0 castling not possible");
        }

        [TestMethod, Ignore]
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

            var king = new King(ChessColor.Black);
            List<IMove> kingMoves = king.GetMoves(generator, board, Helper.FileCharToFile('e'), 8, true);
            Assert.AreEqual(false, kingMoves.Contains(new CastlingMove(CastlingType.BlackKingSide, new King(ChessColor.Black))), "e1g1. 0-0 castling not possible");
            Assert.AreEqual(false, kingMoves.Contains(new CastlingMove(CastlingType.BlackQueenSide, new King(ChessColor.Black))), "e1c1. 0-0-0 castling not possible");
        }

        [TestMethod, Ignore]
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

            var king = new King(ChessColor.Black);
            List<IMove> kingMoves = king.GetMoves(generator, board, Helper.FileCharToFile('e'), 8, true);
            Assert.AreEqual(false, kingMoves.Contains(new CastlingMove(CastlingType.BlackKingSide, new King(ChessColor.Black))), "e1g1. 0-0 castling not possible. f8 attacked");
            Assert.AreEqual(false, kingMoves.Contains(new CastlingMove(CastlingType.BlackQueenSide, new King(ChessColor.Black))), "e1c1. 0-0-0 castling not possible. d8 attacked");
        }

        [TestMethod, Ignore]
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

            var king = new King(ChessColor.Black);
            List<IMove> kingMoves = king.GetMoves(generator, board, Helper.FileCharToFile('e'), 8, true);
            Assert.AreEqual(false, kingMoves.Contains(new CastlingMove(CastlingType.BlackKingSide, new King(ChessColor.Black))), "e1g1. 0-0 castling not possible. g8 attacked");
            Assert.AreEqual(false, kingMoves.Contains(new CastlingMove(CastlingType.BlackQueenSide, new King(ChessColor.Black))), "e1c1. 0-0-0 castling not possible. c8 attacked");
        }

        /// -----------------------------------------------------------------------------------------
        /// Black Castling rights lost after king or rook move
        /// -----------------------------------------------------------------------------------------

        [TestMethod, Ignore]
        public void GetMovesTest_WhenBlackKingMoved_ThenCastlingRightLost()
        {
            MoveGenerator generator = new MoveGenerator(new MoveFactory());
            Board board = new Board();
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
            var kingMove = new NormalMove(king, 'e', 7, 'e', 8, null);
            board.Move(kingMove);

            // white move
            var pawn = new Pawn(ChessColor.White);
            var pawnMove = new NormalMove(pawn, 'a', 2, 'a', 1, null);
            board.Move(pawnMove);

            // get legal moves of king. should not include castling.
            var kingMoves = king.GetMoves(generator, board, Helper.FileCharToFile('e'), 8, true);

            Assert.AreEqual(false, kingMoves.Contains(new CastlingMove(CastlingType.BlackKingSide, new King(ChessColor.Black))), "e8g8. 0-0 castling missing");
            Assert.AreEqual(false, kingMoves.Contains(new CastlingMove(CastlingType.BlackQueenSide, new King(ChessColor.Black))), "e8c8. 0-0-0 castling missing");
        }

        [TestMethod, Ignore]
        public void GetMovesTest_WhenBlackKingRookMoved_ThenCastlingRightKingSideLost()
        {
            MoveGenerator generator = new MoveGenerator(new MoveFactory());
            Board board = new Board();
            string position = "r...k..." +
                              "p......r" +
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
            var rookMove = new NormalMove(rook, 'h', 7, 'h', 8, null);
            board.Move(rookMove);

            // black move
            var pawn = new Pawn(ChessColor.White);
            var pawnMove = new NormalMove(pawn, 'a', 2, 'a', 3, null);
            board.Move(pawnMove);

            // get legal moves of king. should not include castling.
            var king = new King(ChessColor.Black);
            var kingMoves = king.GetMoves(generator, board, Helper.FileCharToFile('e'), 8, true);

            Assert.AreEqual(false, kingMoves.Contains(new CastlingMove(CastlingType.BlackKingSide, new King(ChessColor.Black))), "e8g8. 0-0 castling missing");
            Assert.AreEqual(true, kingMoves.Contains(new CastlingMove(CastlingType.BlackQueenSide, new King(ChessColor.Black))), "e8c8. 0-0-0 castling missing");
        }

        [TestMethod, Ignore]
        public void GetMovesTest_WhenBlackQueenRookMoved_ThenCastlingRightQueenSideLost()
        {
            MoveGenerator generator = new MoveGenerator(new MoveFactory());
            Board board = new Board();
            string position = "....k..r" +
                              "r......." +
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
            var rookMove = new NormalMove(rook, 'a', 7, 'a', 8, null);
            board.Move(rookMove);

            // White move
            var pawn = new Pawn(ChessColor.White);
            var pawnMove = new NormalMove(pawn, 'a', 2, 'a', 3, null);
            board.Move(pawnMove);

            // get legal moves of king. should not include castling.
            var king = new King(ChessColor.Black);
            var kingMoves = king.GetMoves(generator, board, Helper.FileCharToFile('e'), 8, true);

            Assert.AreEqual(true, kingMoves.Contains(new CastlingMove(CastlingType.BlackKingSide, new King(ChessColor.Black))), "e8g8. 0-0 castling missing");
            Assert.AreEqual(false, kingMoves.Contains(new CastlingMove(CastlingType.BlackQueenSide, new King(ChessColor.Black))), "e8c8. 0-0-0 castling missing");
        }

        [TestMethod, Ignore]
        public void GetMovesTest_WhenEnPassantCaptureAndKingInCheck_ThenNoCastlingPossible()
        {
            // Kiwipete position
            // Position is after "r3k2r/p1ppqpb1/bn2pnp1/3PN3/1p2P3/2N2Q1p/PPPBBPPP/R3K2R w KQkq - 0 0"
            // and move d5 x e6     d7-d5
            //          e6 x d7 ep

            MoveGenerator generator = new MoveGenerator(new MoveFactory());
            Board board = new Board();
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
            var pawnWMove = new NormalMove(pawnW, 'd', 5, 'e', 6, new Pawn(ChessColor.Black));
            board.Move(pawnWMove);

            // Black pawn move d7-d5
            var pawnB = new Pawn(ChessColor.Black);
            var pawnBMove = new NormalMove(pawnB, 'd', 7, 'd', 5, null);
            board.Move(pawnBMove);

            // white pawn (en passant) move e6 X d7 ep (results black king is in check)
            var pawnWMoveEnCap = new EnPassantCaptureMove(pawnW, 'e', 6, 'd', 7, pawnB);
            board.Move(pawnWMoveEnCap);

            // get legal moves of king. should not include castling.
            var king = new King(ChessColor.Black);
            var kingMoves = king.GetMoves(generator, board, Helper.FileCharToFile('e'), 8, true);

            Assert.AreEqual(false, kingMoves.Contains(new CastlingMove(CastlingType.BlackKingSide, new King(ChessColor.Black))), "e8g8. 0-0 castling missing");
            Assert.AreEqual(false, kingMoves.Contains(new CastlingMove(CastlingType.BlackQueenSide, new King(ChessColor.Black))), "e8c8. 0-0-0 castling missing");
        }

        [TestMethod, Ignore]
        public void GetMovesTest_KiwipetePosition_PawnAttacksFieldsNextToBlackKing_ThenNoCastlingPossible()
        {
            // Kiwipete position
            // position fen r3k2r/p1ppqpb1/bn2pnp1/3PN3/1p2P3/2N2Q1p/PPPBBPPP/R3K2R w KQkq - 0 0 moves d5e6 e7c5 e6e7
            
            MoveGenerator generator = new MoveGenerator(new MoveFactory());
            Board board = new Board();
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
            var kingMoves = king.GetMoves(generator, board, Helper.FileCharToFile('e'), 8, true);

            Assert.AreEqual(false, kingMoves.Contains(new CastlingMove(CastlingType.BlackKingSide, new King(ChessColor.Black))), "e8g8. 0-0 castling missing");
            Assert.AreEqual(false, kingMoves.Contains(new CastlingMove(CastlingType.BlackQueenSide, new King(ChessColor.Black))), "e8c8. 0-0-0 castling missing");
        }

        // ----------------------------------------------------------------
        // IsCheck Test
        // ----------------------------------------------------------------

        [TestMethod, Ignore]
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

            Assert.AreEqual(true, engine.IsCheck(ChessColor.White), "king is attacked by rook! IsCheck should return true.");
        }

        [TestMethod, Ignore]
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

            Assert.AreEqual(false, engine.IsCheck(ChessColor.White), "king is not attacked! IsCheck shoult return false.");
        }

        [TestMethod, Ignore]
        public void IsCheckTest_WhenPawnInRank7CanPromote_ThenKingNotInCheck()
        {
            MantaEngine engine = new MantaEngine(EngineType.MinimaxPosition);
            Board board = new Board();
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

        [TestMethod, Ignore]
        public void IsAttackedTest_FieldDiagonalOfPawnIsAttacked()
        {
            MoveGenerator generator = new MoveGenerator(new MoveFactory());
            Board board = new Board();
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
            Assert.AreEqual(false, generator.IsAttacked(board, ChessColor.Black, 5, 3), "Field in front of pawn is not attacked");
            // field diagonal in front of pawn
            Assert.AreEqual(true, generator.IsAttacked(board, ChessColor.Black, 4, 3), "Field diagonal in front of pawn is attacked");
            Assert.AreEqual(true, generator.IsAttacked(board, ChessColor.Black, 6, 3), "Field diagonal in front of pawn is attacked");

            // black rook is attacked
            Assert.AreEqual(true, generator.IsAttacked(board, ChessColor.Black, 8, 5), "Black rook is attacked");
            // fields between the rooks is attacked by white rook
            Assert.AreEqual(true, generator.IsAttacked(board, ChessColor.Black, 8, 5), "Fields between the rooks is attacked by the white rook");
            Assert.AreEqual(true, generator.IsAttacked(board, ChessColor.Black, 8, 4), "Fields between the rooks is attacked by the white rook");
            Assert.AreEqual(true, generator.IsAttacked(board, ChessColor.Black, 8, 3), "Fields between the rooks is attacked by the white rook");
            Assert.AreEqual(true, generator.IsAttacked(board, ChessColor.Black, 8, 2), "Fields between the rooks is attacked by the white rook");
        }
    }
}