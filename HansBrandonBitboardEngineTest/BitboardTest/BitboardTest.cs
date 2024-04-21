using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HansBrandonBitboardEngine;
using HBCommon;
using Moq;

namespace HansBrandonBitboardEngineTest
{
    [TestClass]
    public class BitboardTest
    {
        private Bitboards _target;

        [TestInitialize]
        public void Setup()
        {
            _target = new Bitboards(new Mock<IHashtable>().Object);
        }

        [TestMethod]
        public void PiecesBitboardTest()
        {
            _target.SetInitialPosition();

            Assert.AreEqual(BitHelper.ConvertToUInt64(new byte[64]
            {// a1
                0, 0, 0, 0, 0, 0, 0, 0,
                1, 1, 1, 1, 1, 1, 1, 1,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0
            })                   // h8
            , _target.Bitboard_Pieces[(int)ChessColor.White, (int)PieceType.Pawn]);

            Assert.AreEqual(BitHelper.ConvertToUInt64(new byte[64]
            {
                1, 0, 0, 0, 0, 0, 0, 1,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0
            }), _target.Bitboard_Pieces[(int)ChessColor.White, (int)PieceType.Rook]);

            Assert.AreEqual(BitHelper.ConvertToUInt64(new byte[64]
            {
                0, 1, 0, 0, 0, 0, 1, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
            }), _target.Bitboard_Pieces[(int)ChessColor.White, (int)PieceType.Knight]);

            Assert.AreEqual(BitHelper.ConvertToUInt64(new byte[64]
            {
                0, 0, 1, 0, 0, 1, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0
            }), _target.Bitboard_Pieces[(int)ChessColor.White, (int)PieceType.Bishop]);

            Assert.AreEqual(BitHelper.ConvertToUInt64(new byte[64]
            {
                0, 0, 0, 1, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0
            }), _target.Bitboard_Pieces[(int)ChessColor.White, (int)PieceType.Queen]);

            Assert.AreEqual(BitHelper.ConvertToUInt64(new byte[64]
            {
                0, 0, 0, 0, 1, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0
            }), _target.Bitboard_Pieces[(int)ChessColor.White, (int)PieceType.King]);

            Assert.AreEqual(BitHelper.ConvertToUInt64(new byte[64]
            {
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                1, 1, 1, 1, 1, 1, 1, 1,
                0, 0, 0, 0, 0, 0, 0, 0
            }), _target.Bitboard_Pieces[(int)ChessColor.Black, (int)PieceType.Pawn]);

            Assert.AreEqual(BitHelper.ConvertToUInt64(new byte[64]
            {
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                1, 0, 0, 0, 0, 0, 0, 1
            }), _target.Bitboard_Pieces[(int)ChessColor.Black, (int)PieceType.Rook]);

            Assert.AreEqual(BitHelper.ConvertToUInt64(new byte[64]
            {
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 1, 0, 0, 0, 0, 1, 0
            }), _target.Bitboard_Pieces[(int)ChessColor.Black, (int)PieceType.Knight]);

            Assert.AreEqual(BitHelper.ConvertToUInt64(new byte[64]
            {
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 1, 0, 0, 1, 0, 0
            }), _target.Bitboard_Pieces[(int)ChessColor.Black, (int)PieceType.Bishop]);

            Assert.AreEqual(BitHelper.ConvertToUInt64(new byte[64]
            {
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 1, 0, 0, 0, 0
            }), _target.Bitboard_Pieces[(int)ChessColor.Black, (int)PieceType.Queen]);

            Assert.AreEqual(BitHelper.ConvertToUInt64(new byte[64]
            {
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 1, 0, 0, 0
            }), _target.Bitboard_Pieces[(int)ChessColor.Black, (int)PieceType.King]);
        }

        [TestMethod]
        public void SetPieceQueenTest()
        {
            _target.SetPiece(ChessColor.White, PieceType.Queen, Square.F5);

            Assert.AreEqual(PieceType.Queen, _target.BoardAllPieces[(int)Square.F5]);
            Assert.AreEqual(ChessColor.White, _target.BoardColor[(int)Square.F5]);
            Assert.AreEqual(Math.Pow(2, (int)Square.F5), _target.Bitboard_Pieces[(int)ChessColor.White, (int)PieceType.Queen]);
            Assert.AreEqual(Math.Pow(2, (int)Square.F5), _target.Bitboard_Pieces[(int)ChessColor.White, (int)PieceType.Queen]);
        }

        [TestMethod]
        public void SetPieceTwoQueensTest()
        {
            _target.SetPiece(ChessColor.White, PieceType.Queen, Square.F5);
            _target.SetPiece(ChessColor.White, PieceType.Queen, Square.A7);

            Assert.AreEqual(PieceType.Queen, _target.BoardAllPieces[(int)Square.F5]);
            Assert.AreEqual(PieceType.Queen, _target.BoardAllPieces[(int)Square.A7]);
            Assert.AreEqual(ChessColor.White, _target.BoardColor[(int)Square.F5]);
            Assert.AreEqual(ChessColor.White, _target.BoardColor[(int)Square.A7]);
            Assert.AreEqual(Math.Pow(2, (int)Square.F5) + Math.Pow(2, (int)Square.A7), _target.Bitboard_Pieces[(int)ChessColor.White, (int)PieceType.Queen]);
        }

        [TestMethod]
        public void GetPieceQueenTest()
        {
            _target.SetPiece(ChessColor.White, PieceType.Queen, Square.F5);
            var result = _target.GetPiece(Square.F5);

            Assert.AreEqual(ChessColor.White, result.Color);
            Assert.AreEqual(PieceType.Queen, result.Piece);
        }

        [TestMethod]
        public void GetPieceTwoQueenTest()
        {
            _target.SetPiece(ChessColor.White, PieceType.Queen, Square.F5);
            _target.SetPiece(ChessColor.White, PieceType.Queen, Square.B3);

            var result = _target.GetPiece(Square.F5);
            var result2 = _target.GetPiece(Square.B3);

            Assert.AreEqual(ChessColor.White, result.Color);
            Assert.AreEqual(PieceType.Queen, result.Piece);
            Assert.AreEqual(ChessColor.White, result2.Color);
            Assert.AreEqual(PieceType.Queen, result2.Piece);
        }
    }
}
