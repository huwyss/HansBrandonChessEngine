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
        private Bitboards _board;
        private HelperBitboards _helperBits;
        private BitMoveGenerator _bitMoveGenerator; // test target

        [TestInitialize]
        public void Setup()
        {
            _board = new Bitboards(new Mock<IHashtable>().Object);
            _helperBits = new HelperBitboards();
            _bitMoveGenerator = new BitMoveGenerator(_board, _helperBits);
        }

        // ----------------------------------------------------------------------------------------------------
        // Get Moves Tests
        // ----------------------------------------------------------------------------------------------------

        [TestMethod]
        public void GetMoves_WhenWhiteKnight_ThenValidMoves()
        {
            _board.SetInitialPosition();

            var moves = _bitMoveGenerator.GetKnightMoves(ChessColor.White).ToList();

            Assert.AreEqual(4, moves.Count);

            Assert.IsTrue(moves.Contains(BitMove.CreateMove(PieceType.Knight, Square.B1, Square.A3, PieceType.Empty, ChessColor.White, 0)), "Nb1-a3 missing");
            Assert.IsTrue(moves.Contains(BitMove.CreateMove(PieceType.Knight, Square.B1, Square.C3, PieceType.Empty, ChessColor.White, 0)), "Nb1-c3 missing");
            Assert.IsTrue(moves.Contains(BitMove.CreateMove(PieceType.Knight, Square.G1, Square.F3, PieceType.Empty, ChessColor.White, 0)), "Ng1-f3 missing");
            Assert.IsTrue(moves.Contains(BitMove.CreateMove(PieceType.Knight, Square.G1, Square.H3, PieceType.Empty, ChessColor.White, 0)), "Ng1-h3 missing");
        }

        [TestMethod]
        public void GetMoves_WhenWhiteKnight_ThenValidCaptures()
        {
            _board.SetPosition("rnbqkbnr" +
                              "N......." +
                              "......N." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "........");
            
            var moves = _bitMoveGenerator.GetKnightMoves(ChessColor.White).ToList();

            Assert.AreEqual(9, moves.Count);

            Assert.IsTrue(moves.Contains(BitMove.CreateCapture(PieceType.Knight, Square.A7, Square.C8, PieceType.Bishop, Square.C8, PieceType.Empty, ChessColor.White, 0)), "Na7-c8 missing");
            Assert.IsTrue(moves.Contains(BitMove.CreateMove(PieceType.Knight, Square.A7, Square.C6, PieceType.Empty, ChessColor.White, 0)), "Na7-c6 missing");
            Assert.IsTrue(moves.Contains(BitMove.CreateMove(PieceType.Knight, Square.A7, Square.B5, PieceType.Empty, ChessColor.White, 0)), "Na7-b5 missing");

            Assert.IsTrue(moves.Contains(BitMove.CreateCapture(PieceType.Knight, Square.G6, Square.H8, PieceType.Rook, Square.H8, PieceType.Empty, ChessColor.White, 0)), "Ng6xh8 missing");
            Assert.IsTrue(moves.Contains(BitMove.CreateCapture(PieceType.Knight, Square.G6, Square.F8, PieceType.Bishop, Square.F8, PieceType.Empty, ChessColor.White, 0)), "Ng6xf8 missing");
            Assert.IsTrue(moves.Contains(BitMove.CreateMove(PieceType.Knight, Square.G6, Square.E7, PieceType.Empty, ChessColor.White, 0)), "Ng6-e7 missing");
            Assert.IsTrue(moves.Contains(BitMove.CreateMove(PieceType.Knight, Square.G6, Square.E5, PieceType.Empty, ChessColor.White, 0)), "Ng6-e5 missing");
            Assert.IsTrue(moves.Contains(BitMove.CreateMove(PieceType.Knight, Square.G6, Square.F4, PieceType.Empty, ChessColor.White, 0)), "Ng6-f4 missing");
            Assert.IsTrue(moves.Contains(BitMove.CreateMove(PieceType.Knight, Square.G6, Square.H4, PieceType.Empty, ChessColor.White, 0)), "Ng6-h4 missing");
        }

        [TestMethod]
        public void GetMoves_WhenBlackKnight_ThenValidCaptures()
        {
            _board.SetPosition("r.bqkb.r" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "......R." +
                              ".....N.." +
                              "..KQ...n");

            var moves = _bitMoveGenerator.GetKnightMoves(ChessColor.Black).ToList();

            Assert.AreEqual(2, moves.Count);

            Assert.IsTrue(moves.Contains(BitMove.CreateCapture(PieceType.Knight, Square.H1, Square.G3, PieceType.Rook, Square.G3, PieceType.Empty, ChessColor.Black, 0)), "Nh1xg3 missing");
            Assert.IsTrue(moves.Contains(BitMove.CreateCapture(PieceType.Knight, Square.H1, Square.F2, PieceType.Knight, Square.F2, PieceType.Empty, ChessColor.Black, 0)), "Nh1xf2 missing");
        }

        [TestMethod]
        public void GetMoves_WhenBlackKnight_ThenValidMoves()
        {
            _board.SetInitialPosition();

            var moves = _bitMoveGenerator.GetKnightMoves(ChessColor.Black).ToList();

            Assert.AreEqual(4, moves.Count);

            Assert.IsTrue(moves.Contains(BitMove.CreateMove(PieceType.Knight, Square.B8, Square.A6, PieceType.Empty, ChessColor.Black, 0)), "Nb8-a6 missing");
            Assert.IsTrue(moves.Contains(BitMove.CreateMove(PieceType.Knight, Square.B8, Square.C6, PieceType.Empty, ChessColor.Black, 0)), "Nb8-c6 missing");
            Assert.IsTrue(moves.Contains(BitMove.CreateMove(PieceType.Knight, Square.G8, Square.F6, PieceType.Empty, ChessColor.Black, 0)), "Ng8-f6 missing");
            Assert.IsTrue(moves.Contains(BitMove.CreateMove(PieceType.Knight, Square.G8, Square.H6, PieceType.Empty, ChessColor.Black, 0)), "Ng8-h6 missing");
        }

        [TestMethod]
        public void GetMoves_WhenRook_ThenAllMoves()
        {
            _board.SetPosition("........" +
                              "........" +
                              "....p..." +
                              "........" +
                              "....R..P" +
                              "........" +
                              "........" +
                              "........");

            var moves = _bitMoveGenerator.GetRookMoves(ChessColor.White).ToList();

            Assert.AreEqual(11, moves.Count);
            Assert.IsTrue(moves.Contains(BitMove.CreateMove(PieceType.Rook, Square.E4, Square.E5, PieceType.Empty, ChessColor.White, 0)), "Re4-e5 missing");
            Assert.IsTrue(moves.Contains(BitMove.CreateCapture(PieceType.Rook, Square.E4, Square.E6, PieceType.Pawn, Square.E6, PieceType.Empty, ChessColor.White, 0)), "Re4xe6 missing"); // capture pawn
            Assert.IsTrue(moves.Contains(BitMove.CreateMove(PieceType.Rook, Square.E4, Square.F4, PieceType.Empty, ChessColor.White, 0)), "Re4-f4 missing");
            Assert.IsTrue(moves.Contains(BitMove.CreateMove(PieceType.Rook, Square.E4, Square.G4, PieceType.Empty, ChessColor.White, 0)), "Re4-g4 missing");
            Assert.IsTrue(moves.Contains(BitMove.CreateMove(PieceType.Rook, Square.E4, Square.E3, PieceType.Empty, ChessColor.White, 0)), "Re4-e3 missing");
            Assert.IsTrue(moves.Contains(BitMove.CreateMove(PieceType.Rook, Square.E4, Square.E2, PieceType.Empty, ChessColor.White, 0)), "Re4-e2 missing");
            Assert.IsTrue(moves.Contains(BitMove.CreateMove(PieceType.Rook, Square.E4, Square.E1, PieceType.Empty, ChessColor.White, 0)), "Re4-e1 missing");
            Assert.IsTrue(moves.Contains(BitMove.CreateMove(PieceType.Rook, Square.E4, Square.D4, PieceType.Empty, ChessColor.White, 0)), "Re4-d4 missing");
            Assert.IsTrue(moves.Contains(BitMove.CreateMove(PieceType.Rook, Square.E4, Square.C4, PieceType.Empty, ChessColor.White, 0)), "Re4-c4 missing");
            Assert.IsTrue(moves.Contains(BitMove.CreateMove(PieceType.Rook, Square.E4, Square.B4, PieceType.Empty, ChessColor.White, 0)), "Re4-b4 missing");
            Assert.IsTrue(moves.Contains(BitMove.CreateMove(PieceType.Rook, Square.E4, Square.A4, PieceType.Empty, ChessColor.White, 0)), "Re4-a4 missing");
        }

        [TestMethod]
        public void GetMoves_WhenQueen_ThenAllMoves()
        {
            _board.SetPosition("........" +
                              "........" +
                              "........" +
                              "........" +
                              ".r.B...." +
                              "........" +
                              ".Q.P...." +
                              "........");

            var moves = _bitMoveGenerator.GetQueenMoves(ChessColor.White).ToList();

            Assert.AreEqual(9, moves.Count);
            Assert.IsTrue(moves.Contains(BitMove.CreateMove(PieceType.Queen, Square.B2, Square.B3, PieceType.Empty, ChessColor.White, 0)), "Qb2-b3 missing");//
            Assert.IsTrue(moves.Contains(BitMove.CreateCapture(PieceType.Queen, Square.B2, Square.B4, PieceType.Rook, Square.B4, PieceType.Empty, ChessColor.White, 0)), "Qb2xb4 missing");//
            Assert.IsTrue(moves.Contains(BitMove.CreateMove(PieceType.Queen, Square.B2, Square.C3, PieceType.Empty, ChessColor.White, 0)), "Qb2-c3 missing");//
            Assert.IsTrue(moves.Contains(BitMove.CreateMove(PieceType.Queen, Square.B2, Square.C2, PieceType.Empty, ChessColor.White, 0)), "Qb2-c2 missing");//
            Assert.IsTrue(moves.Contains(BitMove.CreateMove(PieceType.Queen, Square.B2, Square.C1, PieceType.Empty, ChessColor.White, 0)), "Qb2-c1 missing");//
            Assert.IsTrue(moves.Contains(BitMove.CreateMove(PieceType.Queen, Square.B2, Square.B1, PieceType.Empty, ChessColor.White, 0)), "Qb2-b1 missing");
            Assert.IsTrue(moves.Contains(BitMove.CreateMove(PieceType.Queen, Square.B2, Square.A1, PieceType.Empty, ChessColor.White, 0)), "Qb2-a1 missing");//
            Assert.IsTrue(moves.Contains(BitMove.CreateMove(PieceType.Queen, Square.B2, Square.A2, PieceType.Empty, ChessColor.White, 0)), "Qb2-a2 missing");
            Assert.IsTrue(moves.Contains(BitMove.CreateMove(PieceType.Queen, Square.B2, Square.A3, PieceType.Empty, ChessColor.White, 0)), "Qb2-a3 missing");//
        }

        [TestMethod]
        public void GetMoves_WhenQueen_ThenOnlyTwoMoves()
        {
            _board.SetPosition("........" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "BBK....." +
                              ".QP....." +
                              "R.N.....");

            var moves = _bitMoveGenerator.GetQueenMoves(ChessColor.White).ToList();

            Assert.AreEqual(2, moves.Count);
            Assert.IsTrue(moves.Contains(BitMove.CreateMove(PieceType.Queen, Square.B2, Square.B1, PieceType.Empty, ChessColor.White, 0)), "Qb2-b1 missing");
            Assert.IsTrue(moves.Contains(BitMove.CreateMove(PieceType.Queen, Square.B2, Square.A2, PieceType.Empty, ChessColor.White, 0)), "Qb2-a2 missing");
        }

        [TestMethod]
        public void GetMoves_WhenBishop_ThenAllMoves()
        {
            _board.SetPosition("........" +
                              "........" +
                              "........" +
                              "........" +
                              "r...N..." +
                              "........" +
                              "..B....." +
                              "........");

            var moves = _bitMoveGenerator.GetBishopMoves(ChessColor.White).ToList();

            Assert.AreEqual(5, moves.Count);
            Assert.IsTrue(moves.Contains(BitMove.CreateMove(PieceType.Bishop, Square.C2, Square.D3, PieceType.Empty, ChessColor.White, 0)), "Bc2-d3 missing");
            Assert.IsTrue(moves.Contains(BitMove.CreateMove(PieceType.Bishop, Square.C2, Square.D1, PieceType.Empty, ChessColor.White, 0)), "Bc2-d1 missing");
            Assert.IsTrue(moves.Contains(BitMove.CreateMove(PieceType.Bishop, Square.C2, Square.B1, PieceType.Empty, ChessColor.White, 0)), "Bc2-b1 missing");
            Assert.IsTrue(moves.Contains(BitMove.CreateMove(PieceType.Bishop, Square.C2, Square.B3, PieceType.Empty, ChessColor.White, 0)), "Bc2-b3 missing");
            Assert.IsTrue(moves.Contains(BitMove.CreateCapture(PieceType.Bishop, Square.C2, Square.A4, PieceType.Rook, Square.A4, PieceType.Empty, ChessColor.White, 0)), "Bc2xa4 missing");
        }

        [TestMethod]
        public void GetMoves_WhenKing_ThenAllMoves()
        {
            _board.SetPosition("........" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              ".rB....." +
                              ".KP.....");

            var moves = _bitMoveGenerator.GetKingMoves(ChessColor.White).ToList();

            Assert.AreEqual(3, moves.Count);
            Assert.IsTrue(moves.Contains(BitMove.CreateCapture(PieceType.King, Square.B1, Square.B2, PieceType.Rook, Square.B2, PieceType.Empty, ChessColor.White, 0)), "Kb1xb2 missing");
            Assert.IsTrue(moves.Contains(BitMove.CreateMove(PieceType.King, Square.B1, Square.A1, PieceType.Empty, ChessColor.White, 0)), "Kb1-a1 missing");
            Assert.IsTrue(moves.Contains(BitMove.CreateMove(PieceType.King, Square.B1, Square.A2, PieceType.Empty, ChessColor.White, 0)), "Kb1-a2 missing");
        }

        //
        // Pawn moves
        // 

        [TestMethod]
        public void GetMoves_WhenWhitePawn_ThenAllMoves() // en passant not included
        {
            _board.SetPosition("........" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              ".r.b...." +
                              "..P....." +
                              "........");

            var moves = _bitMoveGenerator.GetPawnMoves(ChessColor.White).ToList();

            Assert.AreEqual(4, moves.Count);

            Assert.IsTrue(moves.Contains(BitMove.CreateMove(PieceType.Pawn, Square.C2, Square.C3, PieceType.Empty, ChessColor.White, 0)), "c2-c3 missing");
            Assert.IsTrue(moves.Contains(BitMove.CreateMove(PieceType.Pawn, Square.C2, Square.C4, PieceType.Empty, ChessColor.White, 0)), "c2-c4 missing");
            Assert.IsTrue(moves.Contains(BitMove.CreateCapture(PieceType.Pawn, Square.C2, Square.B3, PieceType.Rook, Square.B3, PieceType.Empty, ChessColor.White, 0)), "c2xb3 missing");
            Assert.IsTrue(moves.Contains(BitMove.CreateCapture(PieceType.Pawn, Square.C2, Square.D3, PieceType.Bishop, Square.D3, PieceType.Empty, ChessColor.White, 0)), "c2xd3 missing");
        }

        [TestMethod]
        public void GetMoves_WhenWhitePawnCaptureLeft_ThenAllMoves()
        {
            _board.SetPosition("........" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "nb....rR" +
                              "P.....PP" +
                              "........");

            var moves = _bitMoveGenerator.GetPawnMoves(ChessColor.White).ToList();

            Assert.AreEqual(2, moves.Count);
            Assert.IsTrue(moves.Contains(BitMove.CreateCapture(PieceType.Pawn, Square.A2, Square.B3, PieceType.Bishop, Square.B3, PieceType.Empty, ChessColor.White, 0)), "A2xB3 missing");
            Assert.IsTrue(moves.Contains(BitMove.CreateCapture(PieceType.Pawn, Square.H2, Square.G3, PieceType.Rook, Square.G3, PieceType.Empty, ChessColor.White, 0)), "H2xG3 missing");
        }

        [TestMethod]
        public void GetMoves_WhenWhitePawnCaptureRight_ThenAllMoves()
        {
            _board.SetPosition("........" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "nb....Rr" +
                              "P.....PP" +
                              "........");

            var moves = _bitMoveGenerator.GetPawnMoves(ChessColor.White).ToList();

            Assert.AreEqual(2, moves.Count);
            Assert.IsTrue(moves.Contains(BitMove.CreateCapture(PieceType.Pawn, Square.A2, Square.B3, PieceType.Bishop, Square.B3, PieceType.Empty, ChessColor.White, 0)), "A2xB3 missing");
            Assert.IsTrue(moves.Contains(BitMove.CreateCapture(PieceType.Pawn, Square.G2, Square.H3, PieceType.Rook, Square.H3, PieceType.Empty, ChessColor.White, 0)), "G2xH3 missing");
        }

        [TestMethod]
        public void GetMoves_WhenWhitePawnCaptureLeftAndRight_ThenAllMoves()
        {
            _board.SetPosition("........" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "nb.....r" +
                              "PP.....P" +
                              "........");

            var moves = _bitMoveGenerator.GetPawnMoves(ChessColor.White).ToList();

            Assert.AreEqual(2, moves.Count);
            Assert.IsTrue(moves.Contains(BitMove.CreateCapture(PieceType.Pawn, Square.A2, Square.B3, PieceType.Bishop, Square.B3, PieceType.Empty, ChessColor.White, 0)), "a2xb3 missing");
            Assert.IsTrue(moves.Contains(BitMove.CreateCapture(PieceType.Pawn, Square.B2, Square.A3, PieceType.Knight, Square.A3, PieceType.Empty, ChessColor.White, 0)), "b2xa3 missing");
        }

        [TestMethod]
        public void GetMoves_WhenWhitePawnBlocked_ThenAllMoves() // en passant not included
        {
            _board.SetPosition("........" +
                              "........" +
                              "........" +
                              "........" +
                              "..n....." +
                              ".r.b...." +
                              "..P....." +
                              "........");

            var moves = _bitMoveGenerator.GetPawnMoves(ChessColor.White).ToList();

            Assert.AreEqual(3, moves.Count);
            Assert.IsTrue(moves.Contains(BitMove.CreateMove(PieceType.Pawn, Square.C2, Square.C3, PieceType.Empty, ChessColor.White, 0)), "c2-c3 missing");
            Assert.IsTrue(moves.Contains(BitMove.CreateCapture(PieceType.Pawn, Square.C2, Square.B3, PieceType.Rook, Square.B3, PieceType.Empty, ChessColor.White, 0)), "C2xB3 missing");
            Assert.IsTrue(moves.Contains(BitMove.CreateCapture(PieceType.Pawn, Square.C2, Square.D3, PieceType.Bishop, Square.D3, PieceType.Empty, ChessColor.White, 0)), "C2xD3 missing");
        }

        [TestMethod]
        public void GetMoves_WhenWhitePawnBlockedInMiddleOfBoard_ThenAllMoves()
        {
            _board.SetPosition("........" +
                              "........" +
                              "..p....." + // black pawn
                              "..P....." + // white pawn must not do c5-c7 !
                              "........" +
                              "........" +
                              "........" +
                              "........");

            var moves = _bitMoveGenerator.GetPawnMoves(ChessColor.White).ToList();

            Assert.AreEqual(0, moves.Count);
        }

        [TestMethod]
        public void GetMoves_WhenWhitePawnAtA5_ThenAllMoves()
        {
            _board.SetPosition("........" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "..p....." + // black pawn
                              "..P....." + // white pawn must not do c2-c4 !
                              "........");

            var moves = _bitMoveGenerator.GetPawnMoves(ChessColor.White).ToList();

            Assert.AreEqual(0, moves.Count);
        }

        [TestMethod]
        public void GetMoves_WhenWhiteCanCaptureEnPassant_ThenListEnPassant()
        {
            var stateMock = new Mock<IBitBoardState>();
            stateMock.Setup(s => s.LastEnPassantSquare).Returns(Square.A6);
            _board = new Bitboards(new Mock<IHashtable>().Object, stateMock.Object);
            _bitMoveGenerator = new BitMoveGenerator(_board, _helperBits);
            _board.SetPosition(".......k" +
                              "........" +
                              "........" +
                              "pP......" +
                              "........" +
                              "........" +
                              "........" +
                              "...K....");

            var moves = _bitMoveGenerator.GetEnPassant(ChessColor.White).ToList();

            Assert.AreEqual(1, moves.Count);
            Assert.IsTrue(moves.Contains(BitMove.CreateCapture(PieceType.Pawn, Square.B5, Square.A6, PieceType.Pawn, Square.A5, PieceType.Empty, ChessColor.White, 0)), "B5xA6 ep missing");
        }

        [TestMethod]
        public void GetMoves_WhenBlackCanCaptureEnPassant_ThenListEnPassant()
        {
            var stateMock = new Mock<IBitBoardState>();
            stateMock.Setup(s => s.LastEnPassantSquare).Returns(Square.D3);
            _board = new Bitboards(new Mock<IHashtable>().Object, stateMock.Object);
            _bitMoveGenerator = new BitMoveGenerator(_board, _helperBits);
            _board.SetPosition(".......k" +
                              "........" +
                              "........" +
                              "........" +
                              "..pPp..." +
                              "........" +
                              "........" +
                              "...K....");

            var moves = _bitMoveGenerator.GetEnPassant(ChessColor.Black).ToList();
            Assert.AreEqual(2, moves.Count);
            Assert.IsTrue(moves.Contains(BitMove.CreateCapture(PieceType.Pawn, Square.C4, Square.D3, PieceType.Pawn, Square.D4, PieceType.Empty, ChessColor.Black, 0)), "C4xD3 ep missing");
            Assert.IsTrue(moves.Contains(BitMove.CreateCapture(PieceType.Pawn, Square.E4, Square.D3, PieceType.Pawn, Square.D4, PieceType.Empty, ChessColor.Black, 0)), "E4xD3 ep missing");
        }

        [TestMethod]
        public void GetMoves_WhenBlackPawn_ThenAllMoves()
        {
            _board.SetPosition("........" +
                              "..p....." +
                              ".R.B...." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "........");

            var moves = _bitMoveGenerator.GetPawnMoves(ChessColor.Black).ToList();

            Assert.AreEqual(4, moves.Count);
            Assert.IsTrue(moves.Contains(BitMove.CreateMove(PieceType.Pawn, Square.C7, Square.C6, PieceType.Empty, ChessColor.Black, 0)), "C7-C6 missing");
            Assert.IsTrue(moves.Contains(BitMove.CreateMove(PieceType.Pawn, Square.C7, Square.C5, PieceType.Empty, ChessColor.Black, 0)), "C7-C5 missing");
            Assert.IsTrue(moves.Contains(BitMove.CreateCapture(PieceType.Pawn, Square.C7, Square.B6, PieceType.Rook, Square.B6, PieceType.Empty, ChessColor.Black, 0)), "C7xB6 missing");
            Assert.IsTrue(moves.Contains(BitMove.CreateCapture(PieceType.Pawn, Square.C7, Square.D6, PieceType.Bishop, Square.D6, PieceType.Empty, ChessColor.Black, 0)), "C7xD6 missing");
        }

        [TestMethod]
        public void GetMoves_WhenBlackPawnAta4_ThenAllMoves()
        {
            _board.SetPosition("........" +
                              "........" +
                              "........" +
                              ".p......" + // black pawn must not do b5-b3 !
                              "........" +
                              "........" +
                              "........" +
                              "........");

            var moves = _bitMoveGenerator.GetPawnMoves(ChessColor.Black).ToList();

            Assert.AreEqual(1, moves.Count);
            Assert.IsTrue(moves.Contains(BitMove.CreateCapture(PieceType.Pawn, Square.B5, Square.B4, PieceType.Empty, Square.NoSquare, PieceType.Empty, ChessColor.Black, 0)), "B5-B4 missing");
        }

        [TestMethod]
        public void GetMoves_WhenWhitePawnAt8Straight_ThenPromoted()
        {
            _board.SetPosition("........" +
                              ".P......" + // white pawn gets promoted to queen, rook, bishop or knight
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "........");

            var moves = _bitMoveGenerator.GetPawnMoves(ChessColor.White).ToList();

            Assert.AreEqual(4, moves.Count);
            Assert.IsTrue(moves.Contains(BitMove.CreateMove(PieceType.Pawn, Square.B7, Square.B8, PieceType.Queen, ChessColor.White, 0)), "b7-b8Q missing");
            Assert.IsTrue(moves.Contains(BitMove.CreateMove(PieceType.Pawn, Square.B7, Square.B8, PieceType.Rook, ChessColor.White, 0)), "b7-b8R missing");
            Assert.IsTrue(moves.Contains(BitMove.CreateMove(PieceType.Pawn, Square.B7, Square.B8, PieceType.Bishop, ChessColor.White, 0)), "b7-b8B missing");
            Assert.IsTrue(moves.Contains(BitMove.CreateMove(PieceType.Pawn, Square.B7, Square.B8, PieceType.Knight, ChessColor.White, 0)), "b7-b8N missing");
        }

        [TestMethod]
        public void GetMoves_WhenBlackPawnAt1Straight_ThenPromoted()
        {
            _board.SetPosition("........" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              ".p......" +  // black pawn gets promoted to queen, rook, bishop or knight
                              "........");

            var moves = _bitMoveGenerator.GetPawnMoves(ChessColor.Black).ToList();

            Assert.AreEqual(4, moves.Count);
            Assert.IsTrue(moves.Contains(BitMove.CreateMove(PieceType.Pawn, Square.B2, Square.B1, PieceType.Queen, ChessColor.Black, 0)), "b2-b1Q missing");
            Assert.IsTrue(moves.Contains(BitMove.CreateMove(PieceType.Pawn, Square.B2, Square.B1, PieceType.Rook, ChessColor.Black, 0)), "b2-b1R missing");
            Assert.IsTrue(moves.Contains(BitMove.CreateMove(PieceType.Pawn, Square.B2, Square.B1, PieceType.Bishop, ChessColor.Black, 0)), "b2-b1B missing");
            Assert.IsTrue(moves.Contains(BitMove.CreateMove(PieceType.Pawn, Square.B2, Square.B1, PieceType.Knight, ChessColor.Black, 0)), "b2-b1N missing");
        }

        [TestMethod]
        public void GetMoves_WhenWhitePawnAt8Capturing_ThenPromoted()
        {
            _board.SetPosition("rN......" +
                              ".P......" + // white pawn gets promoted to queen, rook, bishop or knight
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "........");

            var moves = _bitMoveGenerator.GetPawnMoves(ChessColor.White).ToList();

            Assert.AreEqual(4, moves.Count);
            Assert.IsTrue(moves.Contains(BitMove.CreateCapture(PieceType.Pawn, Square.B7, Square.A8, PieceType.Rook, Square.A8, PieceType.Queen, ChessColor.White, 0)), "b7xa8Q missing");
            Assert.IsTrue(moves.Contains(BitMove.CreateCapture(PieceType.Pawn, Square.B7, Square.A8, PieceType.Rook, Square.A8, PieceType.Rook, ChessColor.White, 0)), "b7xa8R missing");
            Assert.IsTrue(moves.Contains(BitMove.CreateCapture(PieceType.Pawn, Square.B7, Square.A8, PieceType.Rook, Square.A8, PieceType.Bishop, ChessColor.White, 0)), "b7xa8B missing");
            Assert.IsTrue(moves.Contains(BitMove.CreateCapture(PieceType.Pawn, Square.B7, Square.A8, PieceType.Rook, Square.A8, PieceType.Knight, ChessColor.White, 0)), "b7xa8N missing");
        }

        [TestMethod]
        public void GetMoves_WhenBlackPawnAt1Capturing_ThenPromoted()
        {
            _board.SetPosition("........" +
                              "........" + // white pawn gets promoted to queen, rook, bishop or knight
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              ".p......" +
                              "Rn......");

            var moves = _bitMoveGenerator.GetPawnMoves(ChessColor.Black).ToList();

            Assert.AreEqual(4, moves.Count);
            Assert.IsTrue(moves.Contains(BitMove.CreateCapture(PieceType.Pawn, Square.B2, Square.A1, PieceType.Rook, Square.A1, PieceType.Queen, ChessColor.Black, 0)), "b2xa1Q missing");
            Assert.IsTrue(moves.Contains(BitMove.CreateCapture(PieceType.Pawn, Square.B2, Square.A1, PieceType.Rook, Square.A1, PieceType.Rook, ChessColor.Black, 0)), "b2xa1R missing");
            Assert.IsTrue(moves.Contains(BitMove.CreateCapture(PieceType.Pawn, Square.B2, Square.A1, PieceType.Rook, Square.A1, PieceType.Bishop, ChessColor.Black, 0)), "b2xa1B missing");
            Assert.IsTrue(moves.Contains(BitMove.CreateCapture(PieceType.Pawn, Square.B2, Square.A1, PieceType.Rook, Square.A1, PieceType.Knight, ChessColor.Black, 0)), "b2xa1N missing");
        }

        // ----------------------------------------------------------------------------------------------------
        // Get All Moves Tests
        // ----------------------------------------------------------------------------------------------------

        [TestMethod]
        public void GetAllMoves_WhenTwoPieces_ThenShowMovesOfBothPieces()
        {
            _board.SetPosition("........" +
                              "........" +
                              "....p..." +
                              "........" +
                              "........" +
                              "........" +
                              "....P..." +
                              "....K...");

            var moves = _bitMoveGenerator.GetAllMoves(ChessColor.White).ToList();

            Assert.AreEqual(6, moves.Count);
            Assert.IsTrue(moves.Contains(BitMove.CreateMove(PieceType.Pawn, Square.E2, Square.E3, PieceType.Empty, ChessColor.White, 0)), "e2-e3 missing");
            Assert.IsTrue(moves.Contains(BitMove.CreateMove(PieceType.Pawn, Square.E2, Square.E4, PieceType.Empty, ChessColor.White, 0)), "e2-e4 missing");
            Assert.IsTrue(moves.Contains(BitMove.CreateMove(PieceType.King, Square.E1, Square.D1, PieceType.Empty, ChessColor.White, 0)), "Ke1-d1 missing");
            Assert.IsTrue(moves.Contains(BitMove.CreateMove(PieceType.King, Square.E1, Square.D2, PieceType.Empty, ChessColor.White, 0)), "Ke1-d2 missing");
            Assert.IsTrue(moves.Contains(BitMove.CreateMove(PieceType.King, Square.E1, Square.F2, PieceType.Empty, ChessColor.White, 0)), "Ke1-f2 missing");
            Assert.IsTrue(moves.Contains(BitMove.CreateMove(PieceType.King, Square.E1, Square.F1, PieceType.Empty, ChessColor.White, 0)), "Ke1-f1 missing");
        }

        [TestMethod]
        public void GetAllMoves_CaptureMoveMustBeListedOnlyOnce()
        {
            _board.SetPosition("k......." +
                              "p......." +
                              "R......." +
                              "P......." +
                              "P......." +
                              "P......." +
                              "P......." +
                              "K.......");

            var moves = _bitMoveGenerator.GetAllMoves(ChessColor.White).ToList();

            Assert.IsTrue(moves.Contains(BitMove.CreateMove(PieceType.King, Square.A1, Square.B1, PieceType.Empty, ChessColor.White, 0)), "Ka1-b1 missing");
            Assert.IsTrue(moves.Contains(BitMove.CreateMove(PieceType.King, Square.A1, Square.B2, PieceType.Empty, ChessColor.White, 0)), "Ka1-b2 missing");
            Assert.IsTrue(moves.Contains(BitMove.CreateMove(PieceType.Rook, Square.A6, Square.B6, PieceType.Empty, ChessColor.White, 0)), "Ra6-b6 missing");
            Assert.IsTrue(moves.Contains(BitMove.CreateMove(PieceType.Rook, Square.A6, Square.C6, PieceType.Empty, ChessColor.White, 0)), "Ra6-c6 missing");
            Assert.IsTrue(moves.Contains(BitMove.CreateMove(PieceType.Rook, Square.A6, Square.D6, PieceType.Empty, ChessColor.White, 0)), "Ra6-d6 missing");
            Assert.IsTrue(moves.Contains(BitMove.CreateMove(PieceType.Rook, Square.A6, Square.E6, PieceType.Empty, ChessColor.White, 0)), "Ra6-e6 missing");
            Assert.IsTrue(moves.Contains(BitMove.CreateMove(PieceType.Rook, Square.A6, Square.F6, PieceType.Empty, ChessColor.White, 0)), "Ra6-f6 missing");
            Assert.IsTrue(moves.Contains(BitMove.CreateMove(PieceType.Rook, Square.A6, Square.G6, PieceType.Empty, ChessColor.White, 0)), "Ra6-g6 missing");
            Assert.IsTrue(moves.Contains(BitMove.CreateMove(PieceType.Rook, Square.A6, Square.H6, PieceType.Empty, ChessColor.White, 0)), "Ra6-h6 missing");

            var rookCaptureMove = BitMove.CreateCapture(PieceType.Rook, Square.A6, Square.A7, PieceType.Pawn, Square.A7, PieceType.Empty, ChessColor.White, 0);
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
            var board = new Board(new Mock<IHashtable>().Object);
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

        [TestMethod, Ignore]
        public void IsMoveValidTest_WhenInvalidMoveWhite_ThenFalse()
        {
            var board = new Board(new Mock<IHashtable>().Object);
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

        [TestMethod, Ignore]
        public void IsMoveValidTest_WhenValidMoveBlack_ThenTrue()
        {
            var board = new Board(new Mock<IHashtable>().Object);
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

        [TestMethod, Ignore]
        public void IsMoveValidTest_WhenWrongSideMoves_ThenFalse()
        {
            var board = new Board(new Mock<IHashtable>().Object);
            MoveGenerator target = new MoveGenerator(board);
            board.SetInitialPosition();

            bool valid = target.IsMoveValid(new NormalMove(Piece.MakePiece(PieceType.Pawn, ChessColor.Black), Square.E7, Square.E5, null)); // BLACK pawn
            Assert.AreEqual(false, valid, "This is white's move. Black must not move.");
        }

        // ----------------------------------------------------------------------------------------------------
        // Get correct move
        // ----------------------------------------------------------------------------------------------------

        [TestMethod, Ignore]
        public void GetCorrectMoveTest_Whene2e4_ThenAddDot()
        {
            var board = new Board(new Mock<IHashtable>().Object);
            var factory = new MoveFactory(board);
            MoveGenerator target = new MoveGenerator(board);
            board.SetInitialPosition();

            IMove actualMove = factory.MakeMoveUci("e2e4");
            Assert.AreEqual("e2e4.", actualMove.ToString());
        }

        [TestMethod, Ignore]
        public void GetCorrectMoveTest_WhenCaptureNormal_ThenAddCapturedPiece()
        {
            var board = new Board(new Mock<IHashtable>().Object);
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

        [TestMethod, Ignore]
        public void GetCorrectMoveTest_WhenCaptureEnPassant_ThenAddCapturedPiece()
        {
            var board = new Board(new Mock<IHashtable>().Object);
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

        [TestMethod, Ignore]
        public void GetCorrectMoveTest_WhenPromotion_ThenReturnPromotionMove()
        {
            var board = new Board(new Mock<IHashtable>().Object);
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

            IMove actualMove = factory.MakeMoveUci("a7a8");

            var expectedMove = new PromotionMove(Piece.MakePiece(PieceType.Pawn, ChessColor.White), Square.A7, Square.A8, null, PieceType.Queen);
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
            _board = new Bitboards(new Mock<IHashtable>().Object, stateMock.Object);
            _bitMoveGenerator = new BitMoveGenerator(_board, _helperBits);
            _board.SetPosition("r...k..r" +
                              "p......." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "P......." +
                              "R...K..R");

            var moves = _bitMoveGenerator.GetCastling(ChessColor.White).ToList();

            Assert.AreEqual(2, moves.Count);
            Assert.IsTrue(moves.Contains(BitMove.CreateCastling(ChessColor.White, MantaCommon.CastlingType.KingSide, 0)), "Ke1-g1 0-0 castling missing");
            Assert.IsTrue(moves.Contains(BitMove.CreateCastling(ChessColor.White, MantaCommon.CastlingType.QueenSide, 0)), "Ke1-c1 0-0-0 castling missing");
        }

        [TestMethod]
        public void GetMovesTest_WhenKingMoved_ThenNoCastling()
        {
            var stateMock = new Mock<IBitBoardState>();
            stateMock.Setup(s => s.LastCastlingRightWhiteKingSide).Returns(true);
            stateMock.Setup(s => s.LastCastlingRightWhiteQueenSide).Returns(true);
            Bitboards board = new Bitboards(new Mock<IHashtable>().Object, stateMock.Object);
            _bitMoveGenerator = new BitMoveGenerator(board, _helperBits);
            board.SetPosition("r...k..r" +
                              "p......." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "P......." +
                              "R..K...R");

            var moves = _bitMoveGenerator.GetCastling(ChessColor.White).ToList();

            Assert.AreEqual(0, moves.Count);
            Assert.AreEqual(false, moves.Contains(BitMove.CreateCastling(ChessColor.White, MantaCommon.CastlingType.KingSide, 0)), "Ke1-g1 0-0 castling not possible");
            Assert.AreEqual(false, moves.Contains(BitMove.CreateCastling(ChessColor.White, MantaCommon.CastlingType.QueenSide, 0)), "Ke1-c1 0-0-0 castling not possible");
        }

        [TestMethod]
        public void GetMovesTest_WhenRookMoved_ThenNoCastling()
        {
            var stateMock = new Mock<IBitBoardState>();
            stateMock.Setup(s => s.LastCastlingRightWhiteKingSide).Returns(true);
            stateMock.Setup(s => s.LastCastlingRightWhiteQueenSide).Returns(true);
            Bitboards board = new Bitboards(new Mock<IHashtable>().Object, stateMock.Object);
            _bitMoveGenerator = new BitMoveGenerator(board, _helperBits);
            board.SetPosition("r...k..r" +
                              "p......." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "P......R" +
                              ".R..K...");

            var moves = _bitMoveGenerator.GetCastling(ChessColor.White).ToList();

            Assert.AreEqual(0, moves.Count);
            Assert.AreEqual(false, moves.Contains(BitMove.CreateCastling(ChessColor.White, MantaCommon.CastlingType.KingSide, 0)), "Ke1-g1 0-0 castling not possible");
            Assert.AreEqual(false, moves.Contains(BitMove.CreateCastling(ChessColor.White, MantaCommon.CastlingType.QueenSide, 0)), "Ke1-c1 0-0-0 castling not possible");
        }

        [TestMethod]
        public void GetMovesTest_WhenCastlingBlockedKingSide_ThenNoCastlingOnKingSide()
        {
            var stateMock = new Mock<IBitBoardState>();
            stateMock.Setup(s => s.LastCastlingRightWhiteKingSide).Returns(true);
            stateMock.Setup(s => s.LastCastlingRightWhiteQueenSide).Returns(true);
            Bitboards board = new Bitboards(new Mock<IHashtable>().Object, stateMock.Object);
            _bitMoveGenerator = new BitMoveGenerator(board, _helperBits);
            board.SetPosition("r...k..r" +
                              "p......." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "P......." +
                              "R...K.NR");

            var moves = _bitMoveGenerator.GetCastling(ChessColor.White).ToList();

            Assert.AreEqual(1, moves.Count);
            Assert.IsFalse(moves.Contains(BitMove.CreateCastling(ChessColor.White, MantaCommon.CastlingType.KingSide, 0)), "Ke1-g1 0-0 castling not possible");
            Assert.IsTrue(moves.Contains(BitMove.CreateCastling(ChessColor.White, MantaCommon.CastlingType.QueenSide, 0)), "Ke1-c1 0-0-0 castling missing");
        }

        [TestMethod]
        public void GetMovesTest_WhenCastlingBlockedQueenSide_ThenNoCastlingOnQueenSide()
        {
            var stateMock = new Mock<IBitBoardState>();
            stateMock.Setup(s => s.LastCastlingRightWhiteKingSide).Returns(true);
            stateMock.Setup(s => s.LastCastlingRightWhiteQueenSide).Returns(true);
            Bitboards board = new Bitboards(new Mock<IHashtable>().Object, stateMock.Object);
            _bitMoveGenerator = new BitMoveGenerator(board, _helperBits);
            board.SetPosition("r...k..r" +
                              "p......." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "P......." +
                              "R..QK..R");

            var moves = _bitMoveGenerator.GetCastling(ChessColor.White).ToList();

            Assert.AreEqual(1, moves.Count);
            Assert.IsTrue(moves.Contains(BitMove.CreateCastling(ChessColor.White, MantaCommon.CastlingType.KingSide, 0)), "Ke1-g1 0-0 castling missing");
            Assert.IsFalse(moves.Contains(BitMove.CreateCastling(ChessColor.White, MantaCommon.CastlingType.QueenSide, 0)), "Ke1-c1 0-0-0 castling not possible");
        }

        [TestMethod]
        public void GetMovesTest_WhenFieldNextToKingIsAttacked_ThenNoCastling()
        {
            _board.SetPosition("....k..." +
                              "p..r.r.." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "P......." +
                              "R...K..R");

            var moves = _bitMoveGenerator.GetCastling(ChessColor.White).ToList();

            Assert.IsFalse(moves.Contains(BitMove.CreateCastling(ChessColor.White, MantaCommon.CastlingType.KingSide, 0)), "0-0 castling not possible");
            Assert.IsFalse(moves.Contains(BitMove.CreateCastling(ChessColor.White, MantaCommon.CastlingType.QueenSide, 0)), "0-0-0 castling not possible");
        }

        [TestMethod]
        public void GetMovesTest_WhenNewKingFieldIsAttacked_ThenNoCastling()
        {
            _board.SetPosition("....k..." +
                              "p.r...r." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "P......." +
                              "R...K..R");

            var moves = _bitMoveGenerator.GetCastling(ChessColor.White).ToList();

            Assert.IsFalse(moves.Contains(BitMove.CreateCastling(ChessColor.White, MantaCommon.CastlingType.KingSide, 0)), "0-0 castling not possible. g1 is attacked.");
            Assert.IsFalse(moves.Contains(BitMove.CreateCastling(ChessColor.White, MantaCommon.CastlingType.QueenSide, 0)), "0-0-0 castling not possible. c1 is attacked.");
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
            _board = new Bitboards(new Mock<IHashtable>().Object, stateMock.Object);
            _bitMoveGenerator = new BitMoveGenerator(_board, _helperBits);
            _board.SetPosition("r...k..r" +
                              "p......." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "P...K..." +
                              "R......R");
            



            // white king moves -> loses castling right
            _board.Move(BitMove.CreateMove(PieceType.King, Square.E2, Square.E1, PieceType.Empty, ChessColor.White, 0));

            // black move
            _board.Move(BitMove.CreateMove(PieceType.Pawn, Square.A7, Square.A6, PieceType.Empty, ChessColor.Black, 0));

            // get legal moves of king. should not include castling.
            var moves = _bitMoveGenerator.GetCastling(ChessColor.White).ToList();

            Assert.IsFalse(moves.Contains(BitMove.CreateCastling(ChessColor.White, MantaCommon.CastlingType.KingSide, 0)), "0-0 castling not allowed");
            Assert.IsFalse(moves.Contains(BitMove.CreateCastling(ChessColor.White, MantaCommon.CastlingType.QueenSide, 0)), "0-0-0 castling not allowed");
        }

        [TestMethod]
        public void GetMovesTest_WhenWhiteKingRookMoved_ThenCastlingRightKingSideLost()
        {
            _board.SetPosition("r...k..r" +
                              "p......." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "P......." +
                              "R...K..R");

            // white king side rook moves -> loses castling right
            _board.Move(BitMove.CreateMove(PieceType.Rook, Square.H1, Square.H2, PieceType.Empty, ChessColor.White, 0));

            // moves to bring rook back to original position
            _board.Move(BitMove.CreateMove(PieceType.Pawn, Square.A7, Square.A6, PieceType.Empty, ChessColor.Black, 0));
            _board.Move(BitMove.CreateMove(PieceType.Rook, Square.H2, Square.H1, PieceType.Empty, ChessColor.White, 0));
            _board.Move(BitMove.CreateMove(PieceType.Pawn, Square.A6, Square.A5, PieceType.Empty, ChessColor.Black, 0));

            // get legal moves of king. should not include castling.
            var moves = _bitMoveGenerator.GetCastling(ChessColor.White).ToList();

            Assert.IsFalse(moves.Contains(BitMove.CreateCastling(ChessColor.White, MantaCommon.CastlingType.KingSide, 0)), "0-0 castling not allowed");
            Assert.IsTrue(moves.Contains(BitMove.CreateCastling(ChessColor.White, MantaCommon.CastlingType.QueenSide, 0)), "0-0-0 castling missing");
        }

        [TestMethod]
        public void GetMovesTest_WhenWhiteQueenRookMoved_ThenCastlingRightQueenSideLost()
        {
            _board.SetPosition("r...k..r" +
                              "p......." +
                              "........" +
                              "........" +
                              "........" +
                              "P......." +
                              "........" +
                              "R...K..R");

            // white king side rook moves -> loses castling right
            _board.Move(BitMove.CreateMove(PieceType.Rook, Square.A1, Square.A2, PieceType.Empty, ChessColor.White, 0));

            // moves to bring white rook back to original position
            _board.Move(BitMove.CreateMove(PieceType.Pawn, Square.A7, Square.A6, PieceType.Empty, ChessColor.Black, 0));
            _board.Move(BitMove.CreateMove(PieceType.Rook, Square.A2, Square.A1, PieceType.Empty, ChessColor.White, 0));
            _board.Move(BitMove.CreateMove(PieceType.Pawn, Square.A6, Square.A5, PieceType.Empty, ChessColor.Black, 0));

            // get legal moves of king. should not include castling.
            var moves = _bitMoveGenerator.GetCastling(ChessColor.White).ToList();

            Assert.IsTrue(moves.Contains(BitMove.CreateCastling(ChessColor.White, MantaCommon.CastlingType.KingSide, 0)), "0-0 castling missing");
            Assert.IsFalse(moves.Contains(BitMove.CreateCastling(ChessColor.White, MantaCommon.CastlingType.QueenSide, 0)), "0-0-0 castling not allowed");
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

            _board = new Bitboards(new Mock<IHashtable>().Object, stateMock.Object);
            _bitMoveGenerator = new BitMoveGenerator(_board, _helperBits);
            _board.SetPosition("r...k..r" +
                              "p......." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "P......." +
                              "R...K..R");
            

            var moves = _bitMoveGenerator.GetCastling(ChessColor.Black).ToList();

            Assert.AreEqual(2, moves.Count);
            Assert.IsTrue(moves.Contains(BitMove.CreateCastling(ChessColor.Black, MantaCommon.CastlingType.KingSide, 0)), "Ke8-g8 0-0 castling missing");
            Assert.IsTrue(moves.Contains(BitMove.CreateCastling(ChessColor.Black, MantaCommon.CastlingType.QueenSide, 0)), "Ke8-c8 0-0-0 castling missing");
        }

        [TestMethod]
        public void GetMovesTest_WhenKingMoved_ThenNoCastling_Black()
        {
            var stateMock = new Mock<IBitBoardState>();
            stateMock.Setup(s => s.LastCastlingRightBlackKingSide).Returns(true);
            stateMock.Setup(s => s.LastCastlingRightBlackQueenSide).Returns(true);
            _board = new Bitboards(new Mock<IHashtable>().Object, stateMock.Object);
            _bitMoveGenerator = new BitMoveGenerator(_board, _helperBits);
            _board.SetPosition("r..k...r" + // king moved!
                              "p......." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "P......R" +
                              "...QK...");

            var moves = _bitMoveGenerator.GetCastling(ChessColor.Black).ToList();

            Assert.AreEqual(0, moves.Count);
            Assert.IsFalse(moves.Contains(BitMove.CreateCastling(ChessColor.Black, MantaCommon.CastlingType.KingSide, 0)), "Ke8-g8 0-0 castling not possible");
            Assert.IsFalse(moves.Contains(BitMove.CreateCastling(ChessColor.Black, MantaCommon.CastlingType.QueenSide, 0)), "Ke8-c8 0-0-0 castling not possible");
        }

        [TestMethod]
        public void GetMovesTest_WhenRookMoved_ThenNoCastling_Black()
        {
            var stateMock = new Mock<IBitBoardState>();
            stateMock.Setup(s => s.LastCastlingRightBlackKingSide).Returns(true);
            stateMock.Setup(s => s.LastCastlingRightBlackQueenSide).Returns(true);
            _board = new Bitboards(new Mock<IHashtable>().Object, stateMock.Object);
            _bitMoveGenerator = new BitMoveGenerator(_board, _helperBits);
            _board.SetPosition(".r..k.r." + // rooks moved!
                              "p......." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "P......R" +
                              "...QK...");

            var moves = _bitMoveGenerator.GetCastling(ChessColor.Black).ToList();

            Assert.AreEqual(0, moves.Count);
            Assert.IsFalse(moves.Contains(BitMove.CreateCastling(ChessColor.Black, MantaCommon.CastlingType.KingSide, 0)), "Ke8-g8 0-0 castling not possible");
            Assert.IsFalse(moves.Contains(BitMove.CreateCastling(ChessColor.Black, MantaCommon.CastlingType.QueenSide, 0)), "Ke8-c8 0-0-0 castling not possible");
        }

        [TestMethod]
        public void GetMovesTest_WhenCastlingBlocked_ThenNoCastling_Black()
        {
            var stateMock = new Mock<IBitBoardState>();
            stateMock.Setup(s => s.LastCastlingRightBlackKingSide).Returns(true);
            stateMock.Setup(s => s.LastCastlingRightBlackQueenSide).Returns(true);
            _board = new Bitboards(new Mock<IHashtable>().Object, stateMock.Object);
            _bitMoveGenerator = new BitMoveGenerator(_board, _helperBits);
            _board.SetPosition("r.b.k.nr" +
                              "p......." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "P......." +
                              "R..QK.NR");

            var moves = _bitMoveGenerator.GetCastling(ChessColor.Black).ToList();

            Assert.AreEqual(0, moves.Count);
            Assert.IsFalse(moves.Contains(BitMove.CreateCastling(ChessColor.Black, MantaCommon.CastlingType.KingSide, 0)), "Ke8-g8 0-0 castling not possible");
            Assert.IsFalse(moves.Contains(BitMove.CreateCastling(ChessColor.Black, MantaCommon.CastlingType.QueenSide, 0)), "Ke8-c8 0-0-0 castling not possible");
        }

        [TestMethod]
        public void GetMovesTest_WhenFieldNextToKingAttacked_ThenNoCastling_Black()
        {
            _board.SetPosition("r...k..r" +
                              "p......." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "P......." +
                              "...RKR..");

            var moves = _bitMoveGenerator.GetCastling(ChessColor.Black).ToList();
            Assert.IsFalse(moves.Contains(BitMove.CreateCastling(ChessColor.Black, MantaCommon.CastlingType.KingSide, 0)), "0-0 castling not possible. f8 attacked");
            Assert.IsFalse(moves.Contains(BitMove.CreateCastling(ChessColor.Black, MantaCommon.CastlingType.QueenSide, 0)), "0-0-0 castling not possible. d8 attacked");
        }

        [TestMethod]
        public void GetMovesTest_WhenNewKingFieldAttacked_ThenNoCastling_Black()
        {
            _board.SetPosition("r...k..r" +
                              "p......." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "P......." +
                              "..R.K.R.");

            var moves = _bitMoveGenerator.GetCastling(ChessColor.Black).ToList();
            Assert.IsFalse(moves.Contains(BitMove.CreateCastling(ChessColor.Black, MantaCommon.CastlingType.KingSide, 0)), "0-0 castling not possible. f8 attacked");
            Assert.IsFalse(moves.Contains(BitMove.CreateCastling(ChessColor.Black, MantaCommon.CastlingType.QueenSide, 0)), "0-0-0 castling not possible. d8 attacked");
        }

        /// -----------------------------------------------------------------------------------------
        /// Black Castling rights lost after king or rook move
        /// -----------------------------------------------------------------------------------------

        [TestMethod]
        public void GetMovesTest_WhenBlackKingMoved_ThenCastlingRightLost()
        {
            _board.SetPosition("r......r" +
                              "p...k..." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "P......." +
                              "R...K..R");

            // Black king moves -> loses castling right
            _board.Move(BitMove.CreateMove(PieceType.King, Square.E7, Square.E8, PieceType.Empty, ChessColor.Black, 0));

            // white move
            _board.Move(BitMove.CreateMove(PieceType.Pawn, Square.A2, Square.A1, PieceType.Empty, ChessColor.White, 0));

            // get legal moves of king. should not include castling.
            var kingMoves = _bitMoveGenerator.GetCastling(ChessColor.Black).ToList();

            Assert.IsFalse(kingMoves.Contains(BitMove.CreateCastling(ChessColor.Black, MantaCommon.CastlingType.KingSide, 0)), "0-0 castling not allowed as king already moved");
            Assert.IsFalse(kingMoves.Contains(BitMove.CreateCastling(ChessColor.Black, MantaCommon.CastlingType.QueenSide, 0)), "0-0-0 castling not allowed as king already moved");
        }

        [TestMethod]
        public void GetMovesTest_WhenBlackKingRookMoved_ThenCastlingRightKingSideLost()
        {
            _board.SetPosition("r...k..r" +
                               "p......." +
                               "........" +
                               "........" +
                               "........" +
                               "........" +
                               "P......." +
                               "R...K..R");

            // black king side rook moves -> loses castling right
            _board.Move(BitMove.CreateMove(PieceType.Rook, Square.H8, Square.H7, PieceType.Empty, ChessColor.Black, 0));

            // some moves to bring black rook back to origin position
            _board.Move(BitMove.CreateMove(PieceType.Pawn, Square.A2, Square.A3, PieceType.Empty, ChessColor.White, 0));
            _board.Move(BitMove.CreateMove(PieceType.Rook, Square.H7, Square.H8, PieceType.Empty, ChessColor.Black, 0));
            _board.Move(BitMove.CreateMove(PieceType.Pawn, Square.A3, Square.A4, PieceType.Empty, ChessColor.White, 0));

            // get legal moves of king. should not include kingside castling.
            var kingMoves = _bitMoveGenerator.GetCastling(ChessColor.Black).ToList();

            Assert.IsFalse(kingMoves.Contains(BitMove.CreateCastling(ChessColor.Black, MantaCommon.CastlingType.KingSide, 0)), "0-0 castling not allowed as rook already moved");
            Assert.IsTrue(kingMoves.Contains(BitMove.CreateCastling(ChessColor.Black, MantaCommon.CastlingType.QueenSide, 0)), "0-0-0 castling missing");
        }

        [TestMethod]
        public void GetMovesTest_WhenBlackQueenRookMoved_ThenCastlingRightQueenSideLost()
        {
            _board.SetPosition("r...k..r" +
                              "........" +
                              "p......." +
                              "........" +
                              "........" +
                              "........" +
                              "P......." +
                              "R...K..R");

            // Black queen side rook moves -> loses castling right
            _board.Move(BitMove.CreateMove(PieceType.Rook, Square.A8, Square.A7, PieceType.Empty, ChessColor.Black, 0));

            // some moves to bring black rook back to origin position
            _board.Move(BitMove.CreateMove(PieceType.Pawn, Square.A2, Square.A3, PieceType.Empty, ChessColor.White, 0));
            _board.Move(BitMove.CreateMove(PieceType.Rook, Square.A7, Square.A8, PieceType.Empty, ChessColor.Black, 0));
            _board.Move(BitMove.CreateMove(PieceType.Pawn, Square.A3, Square.A4, PieceType.Empty, ChessColor.White, 0));

            // get legal moves of king. should not include queenside castling.
            var kingMoves = _bitMoveGenerator.GetCastling(ChessColor.Black).ToList();

            Assert.IsTrue(kingMoves.Contains(BitMove.CreateCastling(ChessColor.Black, MantaCommon.CastlingType.KingSide, 0)), "0-0 castling missing");
            Assert.IsFalse(kingMoves.Contains(BitMove.CreateCastling(ChessColor.Black, MantaCommon.CastlingType.QueenSide, 0)), "0-0-0 castling not allowed as rook already moved");
        }

        [TestMethod]
        public void GetMovesTest_KiwipetePosition_WhenKingInCheck_ThenNoCastlingPossible()
        {
            // Kiwipete position
            // Position is after "r3k2r/p1ppqpb1/bn2pnp1/3PN3/1p2P3/2N2Q1p/PPPBBPPP/R3K2R w KQkq - 0 0"
            // and move d5 x e6     d7-d5
            //          e6 x f7

            _board.SetPosition("r...k..r" +
                              "p.ppqpb." +
                              "bn..pnp." +
                              "...PN..." +
                              ".p..P..." +
                              "..N..Q.p" +
                              "PPPBBPPP" +
                              "R...K..R");

            // White pawn captures d5 x e6
            _board.Move(BitMove.CreateCapture(PieceType.Pawn, Square.D5, Square.E6, PieceType.Pawn, Square.E6, PieceType.Empty, ChessColor.White, 0));

            // Black pawn move d7-d5
            _board.Move(BitMove.CreateMove(PieceType.Pawn, Square.D7, Square.D5, PieceType.Empty, ChessColor.Black, 0));

            // white pawn move e6 X f7 (results black king is in check)
            _board.Move(BitMove.CreateCapture(PieceType.Pawn, Square.E6, Square.F7, PieceType.Pawn, Square.F7, PieceType.Empty, ChessColor.White, 0));

            // get legal moves of king. should not include castling.
            var moves = _bitMoveGenerator.GetCastling(ChessColor.Black).ToList();

            Assert.IsFalse(moves.Contains(BitMove.CreateCastling(ChessColor.Black, MantaCommon.CastlingType.KingSide, 0)), "0-0 castling not allowed. king in check.");
            Assert.IsFalse(moves.Contains(BitMove.CreateCastling(ChessColor.Black, MantaCommon.CastlingType.QueenSide, 0)), "0-0-0 castling not allowed. king in check.");
        }

        [TestMethod]
        public void GetMovesTest_KiwipetePosition_PawnAttacksFieldsNextToBlackKing_ThenNoCastlingPossible()
        {
            // Kiwipete position
            // position fen r3k2r/p1ppqpb1/bn2pnp1/3PN3/1p2P3/2N2Q1p/PPPBBPPP/R3K2R w KQkq - 0 0 moves d5e6 e7c5 e6e7

            _board.SetPosition("r...k..r" +
                              "p.ppP.b." +
                              "bn...np." +
                              "..q.N..." +
                              ".p..P..." +
                              "..N..Q.p" +
                              "PPPBBPPP" +
                              "R...K..R");

            // get legal moves of king. should not include castling.
            var moves = _bitMoveGenerator.GetCastling(ChessColor.Black).ToList();

            Assert.IsFalse(moves.Contains(BitMove.CreateCastling(ChessColor.Black, MantaCommon.CastlingType.KingSide, 0)), "0-0 castling not allowed. f8 is attacked.");
            Assert.IsFalse(moves.Contains(BitMove.CreateCastling(ChessColor.Black, MantaCommon.CastlingType.QueenSide, 0)), "0-0-0 castling not allowed. d8 is attacked.");
        }

        // ----------------------------------------------------------------
        // IsCheck Test
        // ----------------------------------------------------------------

        [TestMethod]
        public void IsCheckTest_WhenKingAttacked_ThenTrue()
        {
            var engine = new MantaBitboardEngine.MantaBitboardEngine(256);
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
            var engine = new MantaBitboardEngine.MantaBitboardEngine(256);
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
            var engine = new MantaBitboardEngine.MantaBitboardEngine(256);
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

        [TestMethod]
        public void IsCheckTest_WhenKingAttacksKing_IsCheck()
        {
            var engine = new MantaBitboardEngine.MantaBitboardEngine(256);
            string position = ".....k.." +
                              ".....K.." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "........";
            engine.SetPosition(position);

            Assert.IsTrue(engine.IsCheck(ChessColor.White), "king is attacked! IsCheck shoult return true.");
        }

        // ----------------------------------------------------------------
        // IsAttacked Test
        // ----------------------------------------------------------------

        [TestMethod]
        public void IsAttackedTest_FieldDiagonalOfPawnIsAttacked()
        {
            _board.SetPosition("r...k..." +
                              "...p...." +
                              ".N......" +
                              ".......r" +
                              "........" +
                              "........" +
                              "....P..." +
                              "...QK..R");

            // field in front of pawn
            Assert.IsFalse(_bitMoveGenerator.IsAttacked(ChessColor.Black, Square.E3), "Field in front of pawn is not attacked");

            // field diagonal in front of pawn
            Assert.IsTrue(_bitMoveGenerator.IsAttacked(ChessColor.Black, Square.D3), "Field diagonal in front of pawn is attacked");
            Assert.IsTrue(_bitMoveGenerator.IsAttacked(ChessColor.Black, Square.F3), "Field diagonal in front of pawn is attacked");

            // black rook is attacked
            Assert.IsTrue(_bitMoveGenerator.IsAttacked(ChessColor.Black, Square.H5), "Black rook is attacked");

            // fields between the rooks is attacked by white rook
            Assert.IsTrue(_bitMoveGenerator.IsAttacked(ChessColor.Black, Square.H5), "Fields between the rooks is attacked by the white rook");
            Assert.IsTrue(_bitMoveGenerator.IsAttacked(ChessColor.Black, Square.H4), "Fields between the rooks is attacked by the white rook");
            Assert.IsTrue(_bitMoveGenerator.IsAttacked(ChessColor.Black, Square.H3), "Fields between the rooks is attacked by the white rook");
            Assert.IsTrue(_bitMoveGenerator.IsAttacked(ChessColor.Black, Square.H2), "Fields between the rooks is attacked by the white rook");

            // Knight attacks
            Assert.IsTrue(_bitMoveGenerator.IsAttacked(ChessColor.Black, Square.D7), "Field D7 is attacked by knight");
            Assert.IsTrue(_bitMoveGenerator.IsAttacked(ChessColor.Black, Square.A8), "Field A8 is attacked by knight");
            Assert.IsFalse(_bitMoveGenerator.IsAttacked(ChessColor.Black, Square.B8), "b8 not attacked by knight");

            // Queen attacks
            Assert.IsTrue(_bitMoveGenerator.IsAttacked(ChessColor.Black, Square.D6), "Field D6 is attacked by queen");
            Assert.IsFalse(_bitMoveGenerator.IsAttacked(ChessColor.Black, Square.D8), "Field D8 is not attacked by queen");
            Assert.IsFalse(_bitMoveGenerator.IsAttacked(ChessColor.Black, Square.A2), "Field A2 not attacked by queen");
        }

        /// <summary>
        /// I do not fix this because this is an illegal board position. There must be a king.
        /// The move generator finds king moves from a1.
        /// </summary>
        [TestMethod, Ignore]
        public void NoPiecesMustGenerateNoMovesTest()
        {
            _board.SetPosition("........" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "........");

            var moves = _bitMoveGenerator.GetAllMoves(ChessColor.White).ToList();
            Assert.AreEqual(0, moves.Count, "Should not find any moves. Board is empty.");
        }
    }
}