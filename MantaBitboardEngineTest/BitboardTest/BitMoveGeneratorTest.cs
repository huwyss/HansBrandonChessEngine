using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MantaBitboardEngine;
using MantaCommon;
using MantaChessEngine; // todo: remove this dependency later...
using System.Linq;
using Moq;

namespace MantaBitboardEngineTest
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

            var moves = bitMoveGenerator.GetKnightMoves(ChessColor.White).ToList();

            Assert.AreEqual(4, moves.Count);

            Assert.IsTrue(moves.Contains(BitMove.CreateMove(BitPieceType.Knight, Square.B1, Square.A3, BitPieceType.Empty, ChessColor.White, 0)), "Nb1-a3 missing");
            Assert.IsTrue(moves.Contains(BitMove.CreateMove(BitPieceType.Knight, Square.B1, Square.C3, BitPieceType.Empty, ChessColor.White, 0)), "Nb1-c3 missing");
            Assert.IsTrue(moves.Contains(BitMove.CreateMove(BitPieceType.Knight, Square.G1, Square.F3, BitPieceType.Empty, ChessColor.White, 0)), "Ng1-f3 missing");
            Assert.IsTrue(moves.Contains(BitMove.CreateMove(BitPieceType.Knight, Square.G1, Square.H3, BitPieceType.Empty, ChessColor.White, 0)), "Ng1-h3 missing");
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

            var moves = bitMoveGenerator.GetKnightMoves(ChessColor.White).ToList();

            Assert.AreEqual(9, moves.Count);

            Assert.IsTrue(moves.Contains(BitMove.CreateCapture(BitPieceType.Knight, Square.A7, Square.C8, BitPieceType.Bishop, Square.C8, BitPieceType.Empty, ChessColor.White, 0)), "Na7-c8 missing");
            Assert.IsTrue(moves.Contains(BitMove.CreateMove(BitPieceType.Knight, Square.A7, Square.C6, BitPieceType.Empty, ChessColor.White, 0)), "Na7-c6 missing");
            Assert.IsTrue(moves.Contains(BitMove.CreateMove(BitPieceType.Knight, Square.A7, Square.B5, BitPieceType.Empty, ChessColor.White, 0)), "Na7-b5 missing");

            Assert.IsTrue(moves.Contains(BitMove.CreateCapture(BitPieceType.Knight, Square.G6, Square.H8, BitPieceType.Rook, Square.H8, BitPieceType.Empty, ChessColor.White, 0)), "Ng6xh8 missing");
            Assert.IsTrue(moves.Contains(BitMove.CreateCapture(BitPieceType.Knight, Square.G6, Square.F8, BitPieceType.Bishop, Square.F8, BitPieceType.Empty, ChessColor.White, 0)), "Ng6xf8 missing");
            Assert.IsTrue(moves.Contains(BitMove.CreateMove(BitPieceType.Knight, Square.G6, Square.E7, BitPieceType.Empty, ChessColor.White, 0)), "Ng6-e7 missing");
            Assert.IsTrue(moves.Contains(BitMove.CreateMove(BitPieceType.Knight, Square.G6, Square.E5, BitPieceType.Empty, ChessColor.White, 0)), "Ng6-e5 missing");
            Assert.IsTrue(moves.Contains(BitMove.CreateMove(BitPieceType.Knight, Square.G6, Square.F4, BitPieceType.Empty, ChessColor.White, 0)), "Ng6-f4 missing");
            Assert.IsTrue(moves.Contains(BitMove.CreateMove(BitPieceType.Knight, Square.G6, Square.H4, BitPieceType.Empty, ChessColor.White, 0)), "Ng6-h4 missing");
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

            var moves = bitMoveGenerator.GetKnightMoves(ChessColor.Black).ToList();

            Assert.AreEqual(2, moves.Count);

            Assert.IsTrue(moves.Contains(BitMove.CreateCapture(BitPieceType.Knight, Square.H1, Square.G3, BitPieceType.Rook, Square.G3, BitPieceType.Empty, ChessColor.Black, 0)), "Nh1xg3 missing");
            Assert.IsTrue(moves.Contains(BitMove.CreateCapture(BitPieceType.Knight, Square.H1, Square.F2, BitPieceType.Knight, Square.F2, BitPieceType.Empty, ChessColor.Black, 0)), "Nh1xf2 missing");
        }

        [TestMethod]
        public void GetMoves_WhenBlackKnight_ThenValidMoves()
        {
            Bitboards board = new Bitboards();
            board.Initialize();
            board.SetInitialPosition();
            var bitMoveGenerator = new BitMoveGenerator(board);

            var moves = bitMoveGenerator.GetKnightMoves(ChessColor.Black).ToList();

            Assert.AreEqual(4, moves.Count);

            Assert.IsTrue(moves.Contains(BitMove.CreateMove(BitPieceType.Knight, Square.B8, Square.A6, BitPieceType.Empty, ChessColor.Black, 0)), "Nb8-a6 missing");
            Assert.IsTrue(moves.Contains(BitMove.CreateMove(BitPieceType.Knight, Square.B8, Square.C6, BitPieceType.Empty, ChessColor.Black, 0)), "Nb8-c6 missing");
            Assert.IsTrue(moves.Contains(BitMove.CreateMove(BitPieceType.Knight, Square.G8, Square.F6, BitPieceType.Empty, ChessColor.Black, 0)), "Ng8-f6 missing");
            Assert.IsTrue(moves.Contains(BitMove.CreateMove(BitPieceType.Knight, Square.G8, Square.H6, BitPieceType.Empty, ChessColor.Black, 0)), "Ng8-h6 missing");
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

            var moves = bitMoveGenerator.GetRookMoves(ChessColor.White).ToList();

            Assert.AreEqual(11, moves.Count);
            Assert.IsTrue(moves.Contains(BitMove.CreateMove(BitPieceType.Rook, Square.E4, Square.E5, BitPieceType.Empty, ChessColor.White, 0)), "Re4-e5 missing");
            Assert.IsTrue(moves.Contains(BitMove.CreateCapture(BitPieceType.Rook, Square.E4, Square.E6, BitPieceType.Pawn, Square.E6, BitPieceType.Empty, ChessColor.White, 0)), "Re4xe6 missing"); // capture pawn
            Assert.IsTrue(moves.Contains(BitMove.CreateMove(BitPieceType.Rook, Square.E4, Square.F4, BitPieceType.Empty, ChessColor.White, 0)), "Re4-f4 missing");
            Assert.IsTrue(moves.Contains(BitMove.CreateMove(BitPieceType.Rook, Square.E4, Square.G4, BitPieceType.Empty, ChessColor.White, 0)), "Re4-g4 missing");
            Assert.IsTrue(moves.Contains(BitMove.CreateMove(BitPieceType.Rook, Square.E4, Square.E3, BitPieceType.Empty, ChessColor.White, 0)), "Re4-e3 missing");
            Assert.IsTrue(moves.Contains(BitMove.CreateMove(BitPieceType.Rook, Square.E4, Square.E2, BitPieceType.Empty, ChessColor.White, 0)), "Re4-e2 missing");
            Assert.IsTrue(moves.Contains(BitMove.CreateMove(BitPieceType.Rook, Square.E4, Square.E1, BitPieceType.Empty, ChessColor.White, 0)), "Re4-e1 missing");
            Assert.IsTrue(moves.Contains(BitMove.CreateMove(BitPieceType.Rook, Square.E4, Square.D4, BitPieceType.Empty, ChessColor.White, 0)), "Re4-d4 missing");
            Assert.IsTrue(moves.Contains(BitMove.CreateMove(BitPieceType.Rook, Square.E4, Square.C4, BitPieceType.Empty, ChessColor.White, 0)), "Re4-c4 missing");
            Assert.IsTrue(moves.Contains(BitMove.CreateMove(BitPieceType.Rook, Square.E4, Square.B4, BitPieceType.Empty, ChessColor.White, 0)), "Re4-b4 missing");
            Assert.IsTrue(moves.Contains(BitMove.CreateMove(BitPieceType.Rook, Square.E4, Square.A4, BitPieceType.Empty, ChessColor.White, 0)), "Re4-a4 missing");
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

            var moves = bitMoveGenerator.GetQueenMoves(ChessColor.White).ToList();

            Assert.AreEqual(9, moves.Count);
            Assert.IsTrue(moves.Contains(BitMove.CreateMove(BitPieceType.Queen, Square.B2, Square.B3, BitPieceType.Empty, ChessColor.White, 0)), "Qb2-b3 missing");//
            Assert.IsTrue(moves.Contains(BitMove.CreateCapture(BitPieceType.Queen, Square.B2, Square.B4, BitPieceType.Rook, Square.B4, BitPieceType.Empty, ChessColor.White, 0)), "Qb2xb4 missing");//
            Assert.IsTrue(moves.Contains(BitMove.CreateMove(BitPieceType.Queen, Square.B2, Square.C3, BitPieceType.Empty, ChessColor.White, 0)), "Qb2-c3 missing");//
            Assert.IsTrue(moves.Contains(BitMove.CreateMove(BitPieceType.Queen, Square.B2, Square.C2, BitPieceType.Empty, ChessColor.White, 0)), "Qb2-c2 missing");//
            Assert.IsTrue(moves.Contains(BitMove.CreateMove(BitPieceType.Queen, Square.B2, Square.C1, BitPieceType.Empty, ChessColor.White, 0)), "Qb2-c1 missing");//
            Assert.IsTrue(moves.Contains(BitMove.CreateMove(BitPieceType.Queen, Square.B2, Square.B1, BitPieceType.Empty, ChessColor.White, 0)), "Qb2-b1 missing");
            Assert.IsTrue(moves.Contains(BitMove.CreateMove(BitPieceType.Queen, Square.B2, Square.A1, BitPieceType.Empty, ChessColor.White, 0)), "Qb2-a1 missing");//
            Assert.IsTrue(moves.Contains(BitMove.CreateMove(BitPieceType.Queen, Square.B2, Square.A2, BitPieceType.Empty, ChessColor.White, 0)), "Qb2-a2 missing");
            Assert.IsTrue(moves.Contains(BitMove.CreateMove(BitPieceType.Queen, Square.B2, Square.A3, BitPieceType.Empty, ChessColor.White, 0)), "Qb2-a3 missing");//
        }

        [TestMethod]
        public void GetMoves_WhenQueen_ThenOnlyTwoMoves()
        {
            Bitboards board = new Bitboards();
            board.Initialize();
            board.SetPosition("........" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "BBK....." +
                              ".QP....." +
                              "R.N.....");
            var bitMoveGenerator = new BitMoveGenerator(board);

            var moves = bitMoveGenerator.GetQueenMoves(ChessColor.White).ToList();

            Assert.AreEqual(2, moves.Count);
            Assert.IsTrue(moves.Contains(BitMove.CreateMove(BitPieceType.Queen, Square.B2, Square.B1, BitPieceType.Empty, ChessColor.White, 0)), "Qb2-b1 missing");
            Assert.IsTrue(moves.Contains(BitMove.CreateMove(BitPieceType.Queen, Square.B2, Square.A2, BitPieceType.Empty, ChessColor.White, 0)), "Qb2-a2 missing");
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

            var moves = bitMoveGenerator.GetBishopMoves(ChessColor.White).ToList();

            Assert.AreEqual(5, moves.Count);
            Assert.IsTrue(moves.Contains(BitMove.CreateMove(BitPieceType.Bishop, Square.C2, Square.D3, BitPieceType.Empty, ChessColor.White, 0)), "Bc2-d3 missing");
            Assert.IsTrue(moves.Contains(BitMove.CreateMove(BitPieceType.Bishop, Square.C2, Square.D1, BitPieceType.Empty, ChessColor.White, 0)), "Bc2-d1 missing");
            Assert.IsTrue(moves.Contains(BitMove.CreateMove(BitPieceType.Bishop, Square.C2, Square.B1, BitPieceType.Empty, ChessColor.White, 0)), "Bc2-b1 missing");
            Assert.IsTrue(moves.Contains(BitMove.CreateMove(BitPieceType.Bishop, Square.C2, Square.B3, BitPieceType.Empty, ChessColor.White, 0)), "Bc2-b3 missing");
            Assert.IsTrue(moves.Contains(BitMove.CreateCapture(BitPieceType.Bishop, Square.C2, Square.A4, BitPieceType.Rook, Square.A4, BitPieceType.Empty, ChessColor.White, 0)), "Bc2xa4 missing");
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

            var moves = bitMoveGenerator.GetKingMoves(ChessColor.White).ToList();

            Assert.AreEqual(3, moves.Count);
            Assert.IsTrue(moves.Contains(BitMove.CreateCapture(BitPieceType.King, Square.B1, Square.B2, BitPieceType.Rook, Square.B2, BitPieceType.Empty, ChessColor.White, 0)), "Kb1xb2 missing");
            Assert.IsTrue(moves.Contains(BitMove.CreateMove(BitPieceType.King, Square.B1, Square.A1, BitPieceType.Empty, ChessColor.White, 0)), "Kb1-a1 missing");
            Assert.IsTrue(moves.Contains(BitMove.CreateMove(BitPieceType.King, Square.B1, Square.A2, BitPieceType.Empty, ChessColor.White, 0)), "Kb1-a2 missing");
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

            var moves = bitMoveGenerator.GetPawnMoves(ChessColor.White).ToList();

            Assert.AreEqual(4, moves.Count);

            Assert.IsTrue(moves.Contains(BitMove.CreateMove(BitPieceType.Pawn, Square.C2, Square.C3, BitPieceType.Empty, ChessColor.White, 0)), "c2-c3 missing");
            Assert.IsTrue(moves.Contains(BitMove.CreateMove(BitPieceType.Pawn, Square.C2, Square.C4, BitPieceType.Empty, ChessColor.White, 0)), "c2-c4 missing");
            Assert.IsTrue(moves.Contains(BitMove.CreateCapture(BitPieceType.Pawn, Square.C2, Square.B3, BitPieceType.Rook, Square.B3, BitPieceType.Empty, ChessColor.White, 0)), "c2xb3 missing");
            Assert.IsTrue(moves.Contains(BitMove.CreateCapture(BitPieceType.Pawn, Square.C2, Square.D3, BitPieceType.Bishop, Square.D3, BitPieceType.Empty, ChessColor.White, 0)), "c2xd3 missing");
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

            var moves = bitMoveGenerator.GetPawnMoves(ChessColor.White).ToList();

            Assert.AreEqual(2, moves.Count);
            Assert.IsTrue(moves.Contains(BitMove.CreateCapture(BitPieceType.Pawn, Square.A2, Square.B3, BitPieceType.Bishop, Square.B3, BitPieceType.Empty, ChessColor.White, 0)), "A2xB3 missing");
            Assert.IsTrue(moves.Contains(BitMove.CreateCapture(BitPieceType.Pawn, Square.H2, Square.G3, BitPieceType.Rook, Square.G3, BitPieceType.Empty, ChessColor.White, 0)), "H2xG3 missing");
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

            var moves = bitMoveGenerator.GetPawnMoves(ChessColor.White).ToList();

            Assert.AreEqual(2, moves.Count);
            Assert.IsTrue(moves.Contains(BitMove.CreateCapture(BitPieceType.Pawn, Square.A2, Square.B3, BitPieceType.Bishop, Square.B3, BitPieceType.Empty, ChessColor.White, 0)), "A2xB3 missing");
            Assert.IsTrue(moves.Contains(BitMove.CreateCapture(BitPieceType.Pawn, Square.G2, Square.H3, BitPieceType.Rook, Square.H3, BitPieceType.Empty, ChessColor.White, 0)), "G2xH3 missing");
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

            var moves = bitMoveGenerator.GetPawnMoves(ChessColor.White).ToList();

            Assert.AreEqual(2, moves.Count);
            Assert.IsTrue(moves.Contains(BitMove.CreateCapture(BitPieceType.Pawn, Square.A2, Square.B3, BitPieceType.Bishop, Square.B3, BitPieceType.Empty, ChessColor.White, 0)), "a2xb3 missing");
            Assert.IsTrue(moves.Contains(BitMove.CreateCapture(BitPieceType.Pawn, Square.B2, Square.A3, BitPieceType.Knight, Square.A3, BitPieceType.Empty, ChessColor.White, 0)), "b2xa3 missing");
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

            var moves = bitMoveGenerator.GetPawnMoves(ChessColor.White).ToList();

            Assert.AreEqual(3, moves.Count);
            Assert.IsTrue(moves.Contains(BitMove.CreateMove(BitPieceType.Pawn, Square.C2, Square.C3, BitPieceType.Empty, ChessColor.White, 0)), "c2-c3 missing");
            Assert.IsTrue(moves.Contains(BitMove.CreateCapture(BitPieceType.Pawn, Square.C2, Square.B3, BitPieceType.Rook, Square.B3, BitPieceType.Empty, ChessColor.White, 0)), "C2xB3 missing");
            Assert.IsTrue(moves.Contains(BitMove.CreateCapture(BitPieceType.Pawn, Square.C2, Square.D3, BitPieceType.Bishop, Square.D3, BitPieceType.Empty, ChessColor.White, 0)), "C2xD3 missing");
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

            var moves = bitMoveGenerator.GetPawnMoves(ChessColor.White).ToList();

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

            var moves = bitMoveGenerator.GetPawnMoves(ChessColor.White).ToList();

            Assert.AreEqual(0, moves.Count);
        }

        [TestMethod]
        public void GetMoves_WhenWhiteCanCaptureEnPassant_ThenListEnPassant()
        {
            var stateMock = new Mock<IBitBoardState>();
            stateMock.Setup(s => s.LastEnPassantSquare).Returns(Square.A6);
            Bitboards board = new Bitboards(stateMock.Object);
            board.Initialize();
            board.SetPosition(".......k" +
                              "........" +
                              "........" +
                              "pP......" +
                              "........" +
                              "........" +
                              "........" +
                              "...K....");
            var bitMoveGenerator = new BitMoveGenerator(board);

            var moves = bitMoveGenerator.GetEnPassant(ChessColor.White).ToList();

            Assert.AreEqual(1, moves.Count);
            Assert.IsTrue(moves.Contains(BitMove.CreateCapture(BitPieceType.Pawn, Square.B5, Square.A6, BitPieceType.Pawn, Square.A5, BitPieceType.Empty, ChessColor.White, 0)), "B5xA6 ep missing");
        }

        [TestMethod]
        public void GetMoves_WhenBlackCanCaptureEnPassant_ThenListEnPassant()
        {
            var stateMock = new Mock<IBitBoardState>();
            stateMock.Setup(s => s.LastEnPassantSquare).Returns(Square.D3);
            Bitboards board = new Bitboards(stateMock.Object);
            board.Initialize();
            board.SetPosition(".......k" +
                              "........" +
                              "........" +
                              "........" +
                              "..pPp..." +
                              "........" +
                              "........" +
                              "...K....");
            var bitMoveGenerator = new BitMoveGenerator(board);

            var moves = bitMoveGenerator.GetEnPassant(ChessColor.Black).ToList();
            Assert.AreEqual(2, moves.Count);
            Assert.IsTrue(moves.Contains(BitMove.CreateCapture(BitPieceType.Pawn, Square.C4, Square.D3, BitPieceType.Pawn, Square.D4, BitPieceType.Empty, ChessColor.Black, 0)), "C4xD3 ep missing");
            Assert.IsTrue(moves.Contains(BitMove.CreateCapture(BitPieceType.Pawn, Square.E4, Square.D3, BitPieceType.Pawn, Square.D4, BitPieceType.Empty, ChessColor.Black, 0)), "E4xD3 ep missing");
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

            var moves = bitMoveGenerator.GetPawnMoves(ChessColor.Black).ToList();

            Assert.AreEqual(4, moves.Count);
            Assert.IsTrue(moves.Contains(BitMove.CreateMove(BitPieceType.Pawn, Square.C7, Square.C6, BitPieceType.Empty, ChessColor.Black, 0)), "C7-C6 missing");
            Assert.IsTrue(moves.Contains(BitMove.CreateMove(BitPieceType.Pawn, Square.C7, Square.C5, BitPieceType.Empty, ChessColor.Black, 0)), "C7-C5 missing");
            Assert.IsTrue(moves.Contains(BitMove.CreateCapture(BitPieceType.Pawn, Square.C7, Square.B6, BitPieceType.Rook, Square.B6, BitPieceType.Empty, ChessColor.Black, 0)), "C7xB6 missing");
            Assert.IsTrue(moves.Contains(BitMove.CreateCapture(BitPieceType.Pawn, Square.C7, Square.D6, BitPieceType.Bishop, Square.D6, BitPieceType.Empty, ChessColor.Black, 0)), "C7xD6 missing");
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

            var moves = bitMoveGenerator.GetPawnMoves(ChessColor.Black).ToList();

            Assert.AreEqual(1, moves.Count);
            Assert.IsTrue(moves.Contains(BitMove.CreateCapture(BitPieceType.Pawn, Square.B5, Square.B4, BitPieceType.Empty, Square.NoSquare, BitPieceType.Empty, ChessColor.Black, 0)), "B5-B4 missing");
        }

        [TestMethod]
        public void GetMoves_WhenWhitePawnAt8Straight_ThenPromoted()
        {
            Bitboards board = new Bitboards();
            board.Initialize();
            board.SetPosition("........" +
                              ".P......" + // white pawn gets promoted to queen, rook, bishop or knight
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "........");
            var bitMoveGenerator = new BitMoveGenerator(board);

            var moves = bitMoveGenerator.GetPawnMoves(ChessColor.White).ToList();

            Assert.AreEqual(4, moves.Count);
            Assert.IsTrue(moves.Contains(BitMove.CreateMove(BitPieceType.Pawn, Square.B7, Square.B8, BitPieceType.Queen, ChessColor.White, 0)), "b7-b8Q missing");
            Assert.IsTrue(moves.Contains(BitMove.CreateMove(BitPieceType.Pawn, Square.B7, Square.B8, BitPieceType.Rook, ChessColor.White, 0)), "b7-b8R missing");
            Assert.IsTrue(moves.Contains(BitMove.CreateMove(BitPieceType.Pawn, Square.B7, Square.B8, BitPieceType.Bishop, ChessColor.White, 0)), "b7-b8B missing");
            Assert.IsTrue(moves.Contains(BitMove.CreateMove(BitPieceType.Pawn, Square.B7, Square.B8, BitPieceType.Knight, ChessColor.White, 0)), "b7-b8N missing");
        }

        [TestMethod]
        public void GetMoves_WhenBlackPawnAt1Straight_ThenPromoted()
        {
            Bitboards board = new Bitboards();
            board.Initialize();
            board.SetPosition("........" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              ".p......" +  // black pawn gets promoted to queen, rook, bishop or knight
                              "........");
            var bitMoveGenerator = new BitMoveGenerator(board);

            var moves = bitMoveGenerator.GetPawnMoves(ChessColor.Black).ToList();

            Assert.AreEqual(4, moves.Count);
            Assert.IsTrue(moves.Contains(BitMove.CreateMove(BitPieceType.Pawn, Square.B2, Square.B1, BitPieceType.Queen, ChessColor.Black, 0)), "b2-b1Q missing");
            Assert.IsTrue(moves.Contains(BitMove.CreateMove(BitPieceType.Pawn, Square.B2, Square.B1, BitPieceType.Rook, ChessColor.Black, 0)), "b2-b1R missing");
            Assert.IsTrue(moves.Contains(BitMove.CreateMove(BitPieceType.Pawn, Square.B2, Square.B1, BitPieceType.Bishop, ChessColor.Black, 0)), "b2-b1B missing");
            Assert.IsTrue(moves.Contains(BitMove.CreateMove(BitPieceType.Pawn, Square.B2, Square.B1, BitPieceType.Knight, ChessColor.Black, 0)), "b2-b1N missing");
        }

        [TestMethod]
        public void GetMoves_WhenWhitePawnAt8Capturing_ThenPromoted()
        {
            Bitboards board = new Bitboards();
            board.Initialize();
            board.SetPosition("rN......" +
                              ".P......" + // white pawn gets promoted to queen, rook, bishop or knight
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "........");
            var bitMoveGenerator = new BitMoveGenerator(board);

            var moves = bitMoveGenerator.GetPawnMoves(ChessColor.White).ToList();

            Assert.AreEqual(4, moves.Count);
            Assert.IsTrue(moves.Contains(BitMove.CreateCapture(BitPieceType.Pawn, Square.B7, Square.A8, BitPieceType.Rook, Square.A8, BitPieceType.Queen, ChessColor.White, 0)), "b7xa8Q missing");
            Assert.IsTrue(moves.Contains(BitMove.CreateCapture(BitPieceType.Pawn, Square.B7, Square.A8, BitPieceType.Rook, Square.A8, BitPieceType.Rook, ChessColor.White, 0)), "b7xa8R missing");
            Assert.IsTrue(moves.Contains(BitMove.CreateCapture(BitPieceType.Pawn, Square.B7, Square.A8, BitPieceType.Rook, Square.A8, BitPieceType.Bishop, ChessColor.White, 0)), "b7xa8B missing");
            Assert.IsTrue(moves.Contains(BitMove.CreateCapture(BitPieceType.Pawn, Square.B7, Square.A8, BitPieceType.Rook, Square.A8, BitPieceType.Knight, ChessColor.White, 0)), "b7xa8N missing");
        }

        [TestMethod]
        public void GetMoves_WhenBlackPawnAt1Capturing_ThenPromoted()
        {
            Bitboards board = new Bitboards();
            board.Initialize();
            board.SetPosition("........" +
                              "........" + // white pawn gets promoted to queen, rook, bishop or knight
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              ".p......" +
                              "Rn......");
            var bitMoveGenerator = new BitMoveGenerator(board);

            var moves = bitMoveGenerator.GetPawnMoves(ChessColor.Black).ToList();

            Assert.AreEqual(4, moves.Count);
            Assert.IsTrue(moves.Contains(BitMove.CreateCapture(BitPieceType.Pawn, Square.B2, Square.A1, BitPieceType.Rook, Square.A1, BitPieceType.Queen, ChessColor.Black, 0)), "b2xa1Q missing");
            Assert.IsTrue(moves.Contains(BitMove.CreateCapture(BitPieceType.Pawn, Square.B2, Square.A1, BitPieceType.Rook, Square.A1, BitPieceType.Rook, ChessColor.Black, 0)), "b2xa1R missing");
            Assert.IsTrue(moves.Contains(BitMove.CreateCapture(BitPieceType.Pawn, Square.B2, Square.A1, BitPieceType.Rook, Square.A1, BitPieceType.Bishop, ChessColor.Black, 0)), "b2xa1B missing");
            Assert.IsTrue(moves.Contains(BitMove.CreateCapture(BitPieceType.Pawn, Square.B2, Square.A1, BitPieceType.Rook, Square.A1, BitPieceType.Knight, ChessColor.Black, 0)), "b2xa1N missing");
        }

        // ----------------------------------------------------------------------------------------------------
        // Get All Moves Tests
        // ----------------------------------------------------------------------------------------------------

        [TestMethod]
        public void GetAllMoves_WhenTwoPieces_ThenShowMovesOfBothPieces()
        {
            Bitboards board = new Bitboards();
            board.Initialize();
            board.SetPosition("........" +
                              "........" +
                              "....p..." +
                              "........" +
                              "........" +
                              "........" +
                              "....P..." +
                              "....K...");
            var bitMoveGenerator = new BitMoveGenerator(board);

            var moves = bitMoveGenerator.GetAllMoves(ChessColor.White).ToList();

            Assert.AreEqual(6, moves.Count);
            Assert.IsTrue(moves.Contains(BitMove.CreateMove(BitPieceType.Pawn, Square.E2, Square.E3, BitPieceType.Empty, ChessColor.White, 0)), "e2-e3 missing");
            Assert.IsTrue(moves.Contains(BitMove.CreateMove(BitPieceType.Pawn, Square.E2, Square.E4, BitPieceType.Empty, ChessColor.White, 0)), "e2-e4 missing");
            Assert.IsTrue(moves.Contains(BitMove.CreateMove(BitPieceType.King, Square.E1, Square.D1, BitPieceType.Empty, ChessColor.White, 0)), "Ke1-d1 missing");
            Assert.IsTrue(moves.Contains(BitMove.CreateMove(BitPieceType.King, Square.E1, Square.D2, BitPieceType.Empty, ChessColor.White, 0)), "Ke1-d2 missing");
            Assert.IsTrue(moves.Contains(BitMove.CreateMove(BitPieceType.King, Square.E1, Square.F2, BitPieceType.Empty, ChessColor.White, 0)), "Ke1-f2 missing");
            Assert.IsTrue(moves.Contains(BitMove.CreateMove(BitPieceType.King, Square.E1, Square.F1, BitPieceType.Empty, ChessColor.White, 0)), "Ke1-f1 missing");
        }

        [TestMethod]
        public void GetAllMoves_CaptureMoveMustBeListedOnlyOnce()
        {
            Bitboards board = new Bitboards();
            board.Initialize();
            board.SetPosition("k......." +
                              "p......." +
                              "R......." +
                              "P......." +
                              "P......." +
                              "P......." +
                              "P......." +
                              "K.......");
            var bitMoveGenerator = new BitMoveGenerator(board);

            var moves = bitMoveGenerator.GetAllMoves(ChessColor.White).ToList();

            Assert.IsTrue(moves.Contains(BitMove.CreateMove(BitPieceType.King, Square.A1, Square.B1, BitPieceType.Empty, ChessColor.White, 0)), "Ka1-b1 missing");
            Assert.IsTrue(moves.Contains(BitMove.CreateMove(BitPieceType.King, Square.A1, Square.B2, BitPieceType.Empty, ChessColor.White, 0)), "Ka1-b2 missing");
            Assert.IsTrue(moves.Contains(BitMove.CreateMove(BitPieceType.Rook, Square.A6, Square.B6, BitPieceType.Empty, ChessColor.White, 0)), "Ra6-b6 missing");
            Assert.IsTrue(moves.Contains(BitMove.CreateMove(BitPieceType.Rook, Square.A6, Square.C6, BitPieceType.Empty, ChessColor.White, 0)), "Ra6-c6 missing");
            Assert.IsTrue(moves.Contains(BitMove.CreateMove(BitPieceType.Rook, Square.A6, Square.D6, BitPieceType.Empty, ChessColor.White, 0)), "Ra6-d6 missing");
            Assert.IsTrue(moves.Contains(BitMove.CreateMove(BitPieceType.Rook, Square.A6, Square.E6, BitPieceType.Empty, ChessColor.White, 0)), "Ra6-e6 missing");
            Assert.IsTrue(moves.Contains(BitMove.CreateMove(BitPieceType.Rook, Square.A6, Square.F6, BitPieceType.Empty, ChessColor.White, 0)), "Ra6-f6 missing");
            Assert.IsTrue(moves.Contains(BitMove.CreateMove(BitPieceType.Rook, Square.A6, Square.G6, BitPieceType.Empty, ChessColor.White, 0)), "Ra6-g6 missing");
            Assert.IsTrue(moves.Contains(BitMove.CreateMove(BitPieceType.Rook, Square.A6, Square.H6, BitPieceType.Empty, ChessColor.White, 0)), "Ra6-h6 missing");

            var rookCaptureMove = BitMove.CreateCapture(BitPieceType.Rook, Square.A6, Square.A7, BitPieceType.Pawn, Square.A7, BitPieceType.Empty, ChessColor.White, 0);
            Assert.IsTrue(moves.Contains(rookCaptureMove));

            Assert.AreEqual(1, moves.Where(m => m.Equals(rookCaptureMove)).Count());
            Assert.AreEqual(10, moves.Count);
        }

        // ----------------------------------------------------------------------------------------------------
        // Is Move Valid Tests
        // ----------------------------------------------------------------------------------------------------

        [TestMethod, Ignore]
        public void IsMoveValidTest_WhenValidMoveWhite_ThenTrue()
        {
            MoveGenerator target = new MoveGenerator();
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
            MoveGenerator target = new MoveGenerator();
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
            MoveGenerator target = new MoveGenerator();
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
            MoveGenerator target = new MoveGenerator();
            Board board = new Board();
            board.SetInitialPosition();

            bool valid = target.IsMoveValid(board, new NormalMove(Piece.MakePiece('p'), 'e', 7, 'e', 5, null)); // BLACK pawn
            Assert.AreEqual(false, valid, "This is white's move. Black must not move.");
        }

        // ----------------------------------------------------------------------------------------------------
        // Get correct move
        // ----------------------------------------------------------------------------------------------------

        [TestMethod, Ignore]
        public void GetCorrectMoveTest_Whene2e4_ThenAddDot()
        {
            var factory = new MoveFactory();
            Board board = new Board();
            board.SetInitialPosition();

            IMove actualMove = factory.MakeMove(board, "e2e4");
            Assert.AreEqual("e2e4.", actualMove.ToString());
        }

        [TestMethod, Ignore]
        public void GetCorrectMoveTest_WhenCaptureNormal_ThenAddCapturedPiece()
        {
            var factory = new MoveFactory();
            MoveGenerator target = new MoveGenerator();
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
            MoveGenerator target = new MoveGenerator();
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
            board.Move(new NormalMove(Piece.MakePiece('p'), 'c', 7, 'c', 5, null));

            IMove actualMove = factory.MakeMove(board, "d5c6");
            Assert.AreEqual("d5c6pe", actualMove.ToString());
        }

        [TestMethod, Ignore]
        public void GetCorrectMoveTest_WhenPromotion_ThenReturnPromotionMove()
        {
            var factory = new MoveFactory();
            MoveGenerator target = new MoveGenerator();
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
            var stateMock = new Mock<IBitBoardState>();
            stateMock.Setup(s => s.LastCastlingRightWhiteKingSide).Returns(true);
            stateMock.Setup(s => s.LastCastlingRightWhiteQueenSide).Returns(true);
            Bitboards board = new Bitboards(stateMock.Object);
            board.Initialize();
            board.SetPosition("r...k..r" +
                              "p......." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "P......." +
                              "R...K..R");
            var bitMoveGenerator = new BitMoveGenerator(board);

            var moves = bitMoveGenerator.GetCastling(ChessColor.White).ToList();

            Assert.AreEqual(2, moves.Count);
            Assert.IsTrue(moves.Contains(BitMove.CreateCastling(ChessColor.White, MantaBitboardEngine.CastlingType.KingSide, 0)), "Ke1-g1 0-0 castling missing");
            Assert.IsTrue(moves.Contains(BitMove.CreateCastling(ChessColor.White, MantaBitboardEngine.CastlingType.QueenSide, 0)), "Ke1-c1 0-0-0 castling missing");
        }

        [TestMethod]
        public void GetMovesTest_WhenKingMoved_ThenNoCastling()
        {
            var stateMock = new Mock<IBitBoardState>();
            stateMock.Setup(s => s.LastCastlingRightWhiteKingSide).Returns(true);
            stateMock.Setup(s => s.LastCastlingRightWhiteQueenSide).Returns(true);
            Bitboards board = new Bitboards(stateMock.Object);
            board.Initialize();
            board.SetPosition("r...k..r" +
                              "p......." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "P......." +
                              "R..K...R");
            var bitMoveGenerator = new BitMoveGenerator(board);

            var moves = bitMoveGenerator.GetCastling(ChessColor.White).ToList();

            Assert.AreEqual(0, moves.Count);
            Assert.AreEqual(false, moves.Contains(BitMove.CreateCastling(ChessColor.White, MantaBitboardEngine.CastlingType.KingSide, 0)), "Ke1-g1 0-0 castling not possible");
            Assert.AreEqual(false, moves.Contains(BitMove.CreateCastling(ChessColor.White, MantaBitboardEngine.CastlingType.QueenSide, 0)), "Ke1-c1 0-0-0 castling not possible");
        }

        [TestMethod]
        public void GetMovesTest_WhenRookMoved_ThenNoCastling()
        {
            var stateMock = new Mock<IBitBoardState>();
            stateMock.Setup(s => s.LastCastlingRightWhiteKingSide).Returns(true);
            stateMock.Setup(s => s.LastCastlingRightWhiteQueenSide).Returns(true);
            Bitboards board = new Bitboards(stateMock.Object);
            board.Initialize();
            board.SetPosition("r...k..r" +
                              "p......." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "P......R" +
                              ".R..K...");
            var bitMoveGenerator = new BitMoveGenerator(board);

            var moves = bitMoveGenerator.GetCastling(ChessColor.White).ToList();

            Assert.AreEqual(0, moves.Count);
            Assert.AreEqual(false, moves.Contains(BitMove.CreateCastling(ChessColor.White, MantaBitboardEngine.CastlingType.KingSide, 0)), "Ke1-g1 0-0 castling not possible");
            Assert.AreEqual(false, moves.Contains(BitMove.CreateCastling(ChessColor.White, MantaBitboardEngine.CastlingType.QueenSide, 0)), "Ke1-c1 0-0-0 castling not possible");
        }

        [TestMethod]
        public void GetMovesTest_WhenCastlingBlockedKingSide_ThenNoCastlingOnKingSide()
        {
            var stateMock = new Mock<IBitBoardState>();
            stateMock.Setup(s => s.LastCastlingRightWhiteKingSide).Returns(true);
            stateMock.Setup(s => s.LastCastlingRightWhiteQueenSide).Returns(true);
            Bitboards board = new Bitboards(stateMock.Object);
            board.Initialize();
            board.SetPosition("r...k..r" +
                              "p......." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "P......." +
                              "R...K.NR");
            var bitMoveGenerator = new BitMoveGenerator(board);

            var moves = bitMoveGenerator.GetCastling(ChessColor.White).ToList();

            Assert.AreEqual(1, moves.Count);
            Assert.IsFalse(moves.Contains(BitMove.CreateCastling(ChessColor.White, MantaBitboardEngine.CastlingType.KingSide, 0)), "Ke1-g1 0-0 castling not possible");
            Assert.IsTrue(moves.Contains(BitMove.CreateCastling(ChessColor.White, MantaBitboardEngine.CastlingType.QueenSide, 0)), "Ke1-c1 0-0-0 castling missing");
        }

        [TestMethod]
        public void GetMovesTest_WhenCastlingBlockedQueenSide_ThenNoCastlingOnQueenSide()
        {
            var stateMock = new Mock<IBitBoardState>();
            stateMock.Setup(s => s.LastCastlingRightWhiteKingSide).Returns(true);
            stateMock.Setup(s => s.LastCastlingRightWhiteQueenSide).Returns(true);
            Bitboards board = new Bitboards(stateMock.Object);
            board.Initialize();
            board.SetPosition("r...k..r" +
                              "p......." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "P......." +
                              "R..QK..R");
            var bitMoveGenerator = new BitMoveGenerator(board);

            var moves = bitMoveGenerator.GetCastling(ChessColor.White).ToList();

            Assert.AreEqual(1, moves.Count);
            Assert.IsTrue(moves.Contains(BitMove.CreateCastling(ChessColor.White, MantaBitboardEngine.CastlingType.KingSide, 0)), "Ke1-g1 0-0 castling missing");
            Assert.IsFalse(moves.Contains(BitMove.CreateCastling(ChessColor.White, MantaBitboardEngine.CastlingType.QueenSide, 0)), "Ke1-c1 0-0-0 castling not possible");
        }

        [TestMethod]
        public void GetMovesTest_WhenFieldNextToKingIsAttacked_ThenNoCastling()
        {
            Bitboards board = new Bitboards();
            board.Initialize();
            board.SetPosition("....k..." +
                              "p..r.r.." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "P......." +
                              "R...K..R");
            var bitMoveGenerator = new BitMoveGenerator(board);

            var moves = bitMoveGenerator.GetCastling(ChessColor.White).ToList();

            Assert.IsFalse(moves.Contains(BitMove.CreateCastling(ChessColor.White, MantaBitboardEngine.CastlingType.KingSide, 0)), "0-0 castling not possible");
            Assert.IsFalse(moves.Contains(BitMove.CreateCastling(ChessColor.White, MantaBitboardEngine.CastlingType.QueenSide, 0)), "0-0-0 castling not possible");
        }

        [TestMethod]
        public void GetMovesTest_WhenNewKingFieldIsAttacked_ThenNoCastling()
        {
            Bitboards board = new Bitboards();
            board.Initialize();
            board.SetPosition("....k..." +
                              "p.r...r." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "P......." +
                              "R...K..R");
            var bitMoveGenerator = new BitMoveGenerator(board);

            var moves = bitMoveGenerator.GetCastling(ChessColor.White).ToList();

            Assert.IsFalse(moves.Contains(BitMove.CreateCastling(ChessColor.White, MantaBitboardEngine.CastlingType.KingSide, 0)), "0-0 castling not possible. g1 is attacked.");
            Assert.IsFalse(moves.Contains(BitMove.CreateCastling(ChessColor.White, MantaBitboardEngine.CastlingType.QueenSide, 0)), "0-0-0 castling not possible. c1 is attacked.");
        }

        /// -----------------------------------------------------------------------------------------
        /// White Castling rights lost after king or rook move
        /// -----------------------------------------------------------------------------------------

        [TestMethod]
        public void GetMovesTest_WhenwhiteKingMoved_ThenCastlingRightLost()
        {
            var stateMock = new Mock<IBitBoardState>();
            stateMock.Setup(s => s.LastCastlingRightBlackKingSide).Returns(true);
            stateMock.Setup(s => s.LastCastlingRightBlackQueenSide).Returns(true);
            Bitboards board = new Bitboards(stateMock.Object);
            board.Initialize();
            board.SetPosition("r...k..r" +
                              "p......." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "P...K..." +
                              "R......R");
            var bitMoveGenerator = new BitMoveGenerator(board);



            // white king moves -> loses castling right
            board.Move(BitMove.CreateMove(BitPieceType.King, Square.E2, Square.E1, BitPieceType.Empty, ChessColor.White, 0));

            // black move
            board.Move(BitMove.CreateMove(BitPieceType.Pawn, Square.A7, Square.A6, BitPieceType.Empty, ChessColor.Black, 0));

            // get legal moves of king. should not include castling.
            var moves = bitMoveGenerator.GetCastling(ChessColor.White).ToList();

            Assert.IsFalse(moves.Contains(BitMove.CreateCastling(ChessColor.White, MantaBitboardEngine.CastlingType.KingSide, 0)), "0-0 castling not allowed");
            Assert.IsFalse(moves.Contains(BitMove.CreateCastling(ChessColor.White, MantaBitboardEngine.CastlingType.QueenSide, 0)), "0-0-0 castling not allowed");
        }

        [TestMethod]
        public void GetMovesTest_WhenWhiteKingRookMoved_ThenCastlingRightKingSideLost()
        {
            Bitboards board = new Bitboards();
            board.Initialize();
            board.SetPosition("r...k..r" +
                              "p......." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "P......." +
                              "R...K..R");
            var bitMoveGenerator = new BitMoveGenerator(board);

            // white king side rook moves -> loses castling right
            board.Move(BitMove.CreateMove(BitPieceType.Rook, Square.H1, Square.H2, BitPieceType.Empty, ChessColor.White, 0));

            // moves to bring rook back to original position
            board.Move(BitMove.CreateMove(BitPieceType.Pawn, Square.A7, Square.A6, BitPieceType.Empty, ChessColor.Black, 0));
            board.Move(BitMove.CreateMove(BitPieceType.Rook, Square.H2, Square.H1, BitPieceType.Empty, ChessColor.White, 0));
            board.Move(BitMove.CreateMove(BitPieceType.Pawn, Square.A6, Square.A5, BitPieceType.Empty, ChessColor.Black, 0));

            // get legal moves of king. should not include castling.
            var moves = bitMoveGenerator.GetCastling(ChessColor.White).ToList();

            Assert.IsFalse(moves.Contains(BitMove.CreateCastling(ChessColor.White, MantaBitboardEngine.CastlingType.KingSide, 0)), "0-0 castling not allowed");
            Assert.IsTrue(moves.Contains(BitMove.CreateCastling(ChessColor.White, MantaBitboardEngine.CastlingType.QueenSide, 0)), "0-0-0 castling missing");
        }

        [TestMethod]
        public void GetMovesTest_WhenWhiteQueenRookMoved_ThenCastlingRightQueenSideLost()
        {
            Bitboards board = new Bitboards();
            board.Initialize();
            board.SetPosition("r...k..r" +
                              "p......." +
                              "........" +
                              "........" +
                              "........" +
                              "P......." +
                              "........" +
                              "R...K..R");
            var bitMoveGenerator = new BitMoveGenerator(board);

            // white king side rook moves -> loses castling right
            board.Move(BitMove.CreateMove(BitPieceType.Rook, Square.A1, Square.A2, BitPieceType.Empty, ChessColor.White, 0));

            // moves to bring white rook back to original position
            board.Move(BitMove.CreateMove(BitPieceType.Pawn, Square.A7, Square.A6, BitPieceType.Empty, ChessColor.Black, 0));
            board.Move(BitMove.CreateMove(BitPieceType.Rook, Square.A2, Square.A1, BitPieceType.Empty, ChessColor.White, 0));
            board.Move(BitMove.CreateMove(BitPieceType.Pawn, Square.A6, Square.A5, BitPieceType.Empty, ChessColor.Black, 0));

            // get legal moves of king. should not include castling.
            var moves = bitMoveGenerator.GetCastling(ChessColor.White).ToList();

            Assert.IsTrue(moves.Contains(BitMove.CreateCastling(ChessColor.White, MantaBitboardEngine.CastlingType.KingSide, 0)), "0-0 castling missing");
            Assert.IsFalse(moves.Contains(BitMove.CreateCastling(ChessColor.White, MantaBitboardEngine.CastlingType.QueenSide, 0)), "0-0-0 castling not allowed");
        }

        /// -----------------------------------------------------------------------------------------
        /// Black Castling rights lost after king or rook move
        /// -----------------------------------------------------------------------------------------

        [TestMethod]
        public void GetMovesTest_WhenCastlingRightOk_ThenCastling_Black()
        {
            var stateMock = new Mock<IBitBoardState>();
            stateMock.Setup(s => s.LastCastlingRightBlackKingSide).Returns(true);
            stateMock.Setup(s => s.LastCastlingRightBlackQueenSide).Returns(true);
            Bitboards board = new Bitboards(stateMock.Object);
            board.Initialize();
            board.SetPosition("r...k..r" +
                              "p......." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "P......." +
                              "R...K..R");
            var bitMoveGenerator = new BitMoveGenerator(board);

            var moves = bitMoveGenerator.GetCastling(ChessColor.Black).ToList();

            Assert.AreEqual(2, moves.Count);
            Assert.IsTrue(moves.Contains(BitMove.CreateCastling(ChessColor.Black, MantaBitboardEngine.CastlingType.KingSide, 0)), "Ke8-g8 0-0 castling missing");
            Assert.IsTrue(moves.Contains(BitMove.CreateCastling(ChessColor.Black, MantaBitboardEngine.CastlingType.QueenSide, 0)), "Ke8-c8 0-0-0 castling missing");
        }

        [TestMethod]
        public void GetMovesTest_WhenKingMoved_ThenNoCastling_Black()
        {
            var stateMock = new Mock<IBitBoardState>();
            stateMock.Setup(s => s.LastCastlingRightBlackKingSide).Returns(true);
            stateMock.Setup(s => s.LastCastlingRightBlackQueenSide).Returns(true);
            Bitboards board = new Bitboards(stateMock.Object);
            board.Initialize();
            board.SetPosition("r..k...r" + // king moved!
                              "p......." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "P......R" +
                              "...QK...");
            var bitMoveGenerator = new BitMoveGenerator(board);

            var moves = bitMoveGenerator.GetCastling(ChessColor.Black).ToList();

            Assert.AreEqual(0, moves.Count);
            Assert.IsFalse(moves.Contains(BitMove.CreateCastling(ChessColor.Black, MantaBitboardEngine.CastlingType.KingSide, 0)), "Ke8-g8 0-0 castling not possible");
            Assert.IsFalse(moves.Contains(BitMove.CreateCastling(ChessColor.Black, MantaBitboardEngine.CastlingType.QueenSide, 0)), "Ke8-c8 0-0-0 castling not possible");
        }

        [TestMethod]
        public void GetMovesTest_WhenRookMoved_ThenNoCastling_Black()
        {
            var stateMock = new Mock<IBitBoardState>();
            stateMock.Setup(s => s.LastCastlingRightBlackKingSide).Returns(true);
            stateMock.Setup(s => s.LastCastlingRightBlackQueenSide).Returns(true);
            Bitboards board = new Bitboards(stateMock.Object);
            board.Initialize();
            board.SetPosition(".r..k.r." + // rooks moved!
                              "p......." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "P......R" +
                              "...QK...");
            var bitMoveGenerator = new BitMoveGenerator(board);

            var moves = bitMoveGenerator.GetCastling(ChessColor.Black).ToList();

            Assert.AreEqual(0, moves.Count);
            Assert.IsFalse(moves.Contains(BitMove.CreateCastling(ChessColor.Black, MantaBitboardEngine.CastlingType.KingSide, 0)), "Ke8-g8 0-0 castling not possible");
            Assert.IsFalse(moves.Contains(BitMove.CreateCastling(ChessColor.Black, MantaBitboardEngine.CastlingType.QueenSide, 0)), "Ke8-c8 0-0-0 castling not possible");
        }

        [TestMethod]
        public void GetMovesTest_WhenCastlingBlocked_ThenNoCastling_Black()
        {
            var stateMock = new Mock<IBitBoardState>();
            stateMock.Setup(s => s.LastCastlingRightBlackKingSide).Returns(true);
            stateMock.Setup(s => s.LastCastlingRightBlackQueenSide).Returns(true);
            Bitboards board = new Bitboards(stateMock.Object);
            board.Initialize();
            board.SetPosition("r.b.k.nr" +
                              "p......." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "P......." +
                              "R..QK.NR");
            var bitMoveGenerator = new BitMoveGenerator(board);

            var moves = bitMoveGenerator.GetCastling(ChessColor.Black).ToList();

            Assert.AreEqual(0, moves.Count);
            Assert.IsFalse(moves.Contains(BitMove.CreateCastling(ChessColor.Black, MantaBitboardEngine.CastlingType.KingSide, 0)), "Ke8-g8 0-0 castling not possible");
            Assert.IsFalse(moves.Contains(BitMove.CreateCastling(ChessColor.Black, MantaBitboardEngine.CastlingType.QueenSide, 0)), "Ke8-c8 0-0-0 castling not possible");
        }

        [TestMethod]
        public void GetMovesTest_WhenFieldNextToKingAttacked_ThenNoCastling_Black()
        {
            Bitboards board = new Bitboards();
            board.Initialize();
            board.SetPosition("r...k..r" +
                              "p......." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "P......." +
                              "...RKR..");
            var generator = new BitMoveGenerator(board);

            var moves = generator.GetCastling(ChessColor.Black).ToList();
            Assert.IsFalse(moves.Contains(BitMove.CreateCastling(ChessColor.Black, MantaBitboardEngine.CastlingType.KingSide, 0)), "0-0 castling not possible. f8 attacked");
            Assert.IsFalse(moves.Contains(BitMove.CreateCastling(ChessColor.Black, MantaBitboardEngine.CastlingType.QueenSide, 0)), "0-0-0 castling not possible. d8 attacked");
        }

        [TestMethod]
        public void GetMovesTest_WhenNewKingFieldAttacked_ThenNoCastling_Black()
        {
            Bitboards board = new Bitboards();
            board.Initialize();
            board.SetPosition("r...k..r" +
                              "p......." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "P......." +
                              "..R.K.R.");
            var generator = new BitMoveGenerator(board);

            var moves = generator.GetCastling(ChessColor.Black).ToList();
            Assert.IsFalse(moves.Contains(BitMove.CreateCastling(ChessColor.Black, MantaBitboardEngine.CastlingType.KingSide, 0)), "0-0 castling not possible. f8 attacked");
            Assert.IsFalse(moves.Contains(BitMove.CreateCastling(ChessColor.Black, MantaBitboardEngine.CastlingType.QueenSide, 0)), "0-0-0 castling not possible. d8 attacked");
        }

        /// -----------------------------------------------------------------------------------------
        /// Black Castling rights lost after king or rook move
        /// -----------------------------------------------------------------------------------------

        [TestMethod]
        public void GetMovesTest_WhenBlackKingMoved_ThenCastlingRightLost()
        {
            Bitboards board = new Bitboards();
            board.Initialize();
            board.SetPosition("r......r" +
                              "p...k..." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "P......." +
                              "R...K..R");
            var bitMoveGenerator = new BitMoveGenerator(board);

            // Black king moves -> loses castling right
            board.Move(BitMove.CreateMove(BitPieceType.King, Square.E7, Square.E8, BitPieceType.Empty, ChessColor.Black, 0));

            // white move
            board.Move(BitMove.CreateMove(BitPieceType.Pawn, Square.A2, Square.A1, BitPieceType.Empty, ChessColor.White, 0));

            // get legal moves of king. should not include castling.
            var kingMoves = bitMoveGenerator.GetCastling(ChessColor.Black).ToList();

            Assert.IsFalse(kingMoves.Contains(BitMove.CreateCastling(ChessColor.Black, MantaBitboardEngine.CastlingType.KingSide, 0)), "0-0 castling not allowed as king already moved");
            Assert.IsFalse(kingMoves.Contains(BitMove.CreateCastling(ChessColor.Black, MantaBitboardEngine.CastlingType.QueenSide, 0)), "0-0-0 castling not allowed as king already moved");
        }

        [TestMethod]
        public void GetMovesTest_WhenBlackKingRookMoved_ThenCastlingRightKingSideLost()
        {
            Bitboards board = new Bitboards();
            board.Initialize();
            board.SetPosition("r...k..r" +
                              "p......." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "P......." +
                              "R...K..R");
            var bitMoveGenerator = new BitMoveGenerator(board);

            // black king side rook moves -> loses castling right
            board.Move(BitMove.CreateMove(BitPieceType.Rook, Square.H8, Square.H7, BitPieceType.Empty, ChessColor.Black, 0));

            // some moves to bring black rook back to origin position
            board.Move(BitMove.CreateMove(BitPieceType.Pawn, Square.A2, Square.A3, BitPieceType.Empty, ChessColor.White, 0));
            board.Move(BitMove.CreateMove(BitPieceType.Rook, Square.H7, Square.H8, BitPieceType.Empty, ChessColor.Black, 0));
            board.Move(BitMove.CreateMove(BitPieceType.Pawn, Square.A3, Square.A4, BitPieceType.Empty, ChessColor.White, 0));

            // get legal moves of king. should not include kingside castling.
            var kingMoves = bitMoveGenerator.GetCastling(ChessColor.Black).ToList();

            Assert.IsFalse(kingMoves.Contains(BitMove.CreateCastling(ChessColor.Black, MantaBitboardEngine.CastlingType.KingSide, 0)), "0-0 castling not allowed as rook already moved");
            Assert.IsTrue(kingMoves.Contains(BitMove.CreateCastling(ChessColor.Black, MantaBitboardEngine.CastlingType.QueenSide, 0)), "0-0-0 castling missing");
        }

        [TestMethod]
        public void GetMovesTest_WhenBlackQueenRookMoved_ThenCastlingRightQueenSideLost()
        {
            Bitboards board = new Bitboards();
            board.Initialize();
            board.SetPosition("r...k..r" +
                              "........" +
                              "p......." +
                              "........" +
                              "........" +
                              "........" +
                              "P......." +
                              "R...K..R");
            var bitMoveGenerator = new BitMoveGenerator(board);

            // Black queen side rook moves -> loses castling right
            board.Move(BitMove.CreateMove(BitPieceType.Rook, Square.A8, Square.A7, BitPieceType.Empty, ChessColor.Black, 0));

            // some moves to bring black rook back to origin position
            board.Move(BitMove.CreateMove(BitPieceType.Pawn, Square.A2, Square.A3, BitPieceType.Empty, ChessColor.White, 0));
            board.Move(BitMove.CreateMove(BitPieceType.Rook, Square.A7, Square.A8, BitPieceType.Empty, ChessColor.Black, 0));
            board.Move(BitMove.CreateMove(BitPieceType.Pawn, Square.A3, Square.A4, BitPieceType.Empty, ChessColor.White, 0));

            // get legal moves of king. should not include queenside castling.
            var kingMoves = bitMoveGenerator.GetCastling(ChessColor.Black).ToList();

            Assert.IsTrue(kingMoves.Contains(BitMove.CreateCastling(ChessColor.Black, MantaBitboardEngine.CastlingType.KingSide, 0)), "0-0 castling missing");
            Assert.IsFalse(kingMoves.Contains(BitMove.CreateCastling(ChessColor.Black, MantaBitboardEngine.CastlingType.QueenSide, 0)), "0-0-0 castling not allowed as rook already moved");
        }

        [TestMethod]
        public void GetMovesTest_KiwipetePosition_WhenKingInCheck_ThenNoCastlingPossible()
        {
            // Kiwipete position
            // Position is after "r3k2r/p1ppqpb1/bn2pnp1/3PN3/1p2P3/2N2Q1p/PPPBBPPP/R3K2R w KQkq - 0 0"
            // and move d5 x e6     d7-d5
            //          e6 x f7

            Bitboards board = new Bitboards();
            board.Initialize();
            board.SetPosition("r...k..r" +
                              "p.ppqpb." +
                              "bn..pnp." +
                              "...PN..." +
                              ".p..P..." +
                              "..N..Q.p" +
                              "PPPBBPPP" +
                              "R...K..R");
            var bitMoveGenerator = new BitMoveGenerator(board);

            // White pawn captures d5 x e6
            board.Move(BitMove.CreateCapture(BitPieceType.Pawn, Square.D5, Square.E6, BitPieceType.Pawn, Square.E6, BitPieceType.Empty, ChessColor.White, 0));

            // Black pawn move d7-d5
            board.Move(BitMove.CreateMove(BitPieceType.Pawn, Square.D7, Square.D5, BitPieceType.Empty, ChessColor.Black, 0));

            // white pawn move e6 X f7 (results black king is in check)
            board.Move(BitMove.CreateCapture(BitPieceType.Pawn, Square.E6, Square.F7, BitPieceType.Pawn, Square.F7, BitPieceType.Empty, ChessColor.White, 0));

            // get legal moves of king. should not include castling.
            var moves = bitMoveGenerator.GetCastling(ChessColor.Black).ToList();

            Assert.IsFalse(moves.Contains(BitMove.CreateCastling(ChessColor.Black, MantaBitboardEngine.CastlingType.KingSide, 0)), "0-0 castling not allowed. king in check.");
            Assert.IsFalse(moves.Contains(BitMove.CreateCastling(ChessColor.Black, MantaBitboardEngine.CastlingType.QueenSide, 0)), "0-0-0 castling not allowed. king in check.");
        }

        [TestMethod]
        public void GetMovesTest_KiwipetePosition_PawnAttacksFieldsNextToBlackKing_ThenNoCastlingPossible()
        {
            // Kiwipete position
            // position fen r3k2r/p1ppqpb1/bn2pnp1/3PN3/1p2P3/2N2Q1p/PPPBBPPP/R3K2R w KQkq - 0 0 moves d5e6 e7c5 e6e7

            Bitboards board = new Bitboards();
            board.Initialize();
            board.SetPosition("r...k..r" +
                              "p.ppP.b." +
                              "bn...np." +
                              "..q.N..." +
                              ".p..P..." +
                              "..N..Q.p" +
                              "PPPBBPPP" +
                              "R...K..R");
            var bitMoveGenerator = new BitMoveGenerator(board);

            // get legal moves of king. should not include castling.
            var moves = bitMoveGenerator.GetCastling(ChessColor.Black).ToList();

            Assert.IsFalse(moves.Contains(BitMove.CreateCastling(ChessColor.Black, MantaBitboardEngine.CastlingType.KingSide, 0)), "0-0 castling not allowed. f8 is attacked.");
            Assert.IsFalse(moves.Contains(BitMove.CreateCastling(ChessColor.Black, MantaBitboardEngine.CastlingType.QueenSide, 0)), "0-0-0 castling not allowed. d8 is attacked.");
        }

        // ----------------------------------------------------------------
        // IsCheck Test
        // ----------------------------------------------------------------

        [TestMethod]
        public void IsCheckTest_WhenKingAttacked_ThenTrue()
        {
            var engine = new MantaBitboardEngine.MantaBitboardEngine();
            string position = "....rk.." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "....K...";
            engine.SetPosition(position);

            Assert.IsTrue(engine.IsCheck(ChessColor.White), "king is attacked by rook! IsCheck should return true.");
        }

        [TestMethod]
        public void IsCheckTest_WhenKingNotAttacked_ThenFalse()
        {
            var engine = new MantaBitboardEngine.MantaBitboardEngine();
            string position = ".....k.." +
                              ".....p.." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "....K...";
            engine.SetPosition(position);

            Assert.IsFalse(engine.IsCheck(ChessColor.White), "king is not attacked! IsCheck shoult return false.");
        }

        [TestMethod]
        public void IsCheckTest_WhenPawnInRank7CanPromote_ThenKingNotInCheck()
        {
            var engine = new MantaBitboardEngine.MantaBitboardEngine();
            string position = "r...k..." +
                              "pppp...P" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "....K...";
            engine.SetPosition(position);

            Assert.IsFalse(engine.IsCheck(ChessColor.Black), "king is not attacked! Promotion move does not count.");
        }

        // ----------------------------------------------------------------
        // IsAttacked Test
        // ----------------------------------------------------------------

        [TestMethod]
        public void IsAttackedTest_FieldDiagonalOfPawnIsAttacked()
        {
            Bitboards board = new Bitboards();
            board.Initialize();
            board.SetPosition("r...k..." +
                              "...p...." +
                              ".N......" +
                              ".......r" +
                              "........" +
                              "........" +
                              "....P..." +
                              "...QK..R");
            var bitMoveGenerator = new BitMoveGenerator(board);

            // field in front of pawn
            Assert.IsFalse(bitMoveGenerator.IsAttacked(ChessColor.Black, Square.E3), "Field in front of pawn is not attacked");

            // field diagonal in front of pawn
            Assert.IsTrue(bitMoveGenerator.IsAttacked(ChessColor.Black, Square.D3), "Field diagonal in front of pawn is attacked");
            Assert.IsTrue(bitMoveGenerator.IsAttacked(ChessColor.Black, Square.F3), "Field diagonal in front of pawn is attacked");

            // black rook is attacked
            Assert.IsTrue(bitMoveGenerator.IsAttacked(ChessColor.Black, Square.H5), "Black rook is attacked");

            // fields between the rooks is attacked by white rook
            Assert.IsTrue(bitMoveGenerator.IsAttacked(ChessColor.Black, Square.H5), "Fields between the rooks is attacked by the white rook");
            Assert.IsTrue(bitMoveGenerator.IsAttacked(ChessColor.Black, Square.H4), "Fields between the rooks is attacked by the white rook");
            Assert.IsTrue(bitMoveGenerator.IsAttacked(ChessColor.Black, Square.H3), "Fields between the rooks is attacked by the white rook");
            Assert.IsTrue(bitMoveGenerator.IsAttacked(ChessColor.Black, Square.H2), "Fields between the rooks is attacked by the white rook");

            // Knight attacks
            Assert.IsTrue(bitMoveGenerator.IsAttacked(ChessColor.Black, Square.D7), "Field D7 is attacked by knight");
            Assert.IsTrue(bitMoveGenerator.IsAttacked(ChessColor.Black, Square.A8), "Field A8 is attacked by knight");
            Assert.IsFalse(bitMoveGenerator.IsAttacked(ChessColor.Black, Square.B8), "b8 not attacked by knight");

            // Queen attacks
            Assert.IsTrue(bitMoveGenerator.IsAttacked(ChessColor.Black, Square.D6), "Field D6 is attacked by queen");
            Assert.IsFalse(bitMoveGenerator.IsAttacked(ChessColor.Black, Square.D8), "Field D8 is not attacked by queen");
            Assert.IsFalse(bitMoveGenerator.IsAttacked(ChessColor.Black, Square.A2), "Field A2 not attacked by queen");
        }

        /// <summary>
        /// I do not fix this because this is an illegal board position. There must be a king.
        /// The move generator finds king moves from a1.
        /// </summary>
        [TestMethod, Ignore]
        public void NoPiecesMustGenerateNoMovesTest()
        {
            Bitboards board = new Bitboards();
            board.Initialize();
            board.SetPosition("........" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "........");
            var bitMoveGenerator = new BitMoveGenerator(board);

            var moves = bitMoveGenerator.GetAllMoves(ChessColor.White).ToList();
            Assert.AreEqual(0, moves.Count, "Should not find any moves. Board is empty.");
        }
    }
}