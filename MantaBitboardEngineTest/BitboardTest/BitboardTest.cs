using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MantaBitboardEngine;
using MantaCommon;

namespace MantaBitboardEngineTest
{
    [TestClass]
    public class BitboardTest
    {
        [TestMethod]
        public void PiecesBitboardTest()
        {
            var target = new Bitboards();
            target.SetInitialPosition();

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
            , target.Bitboard_Pieces[(int)ChessColor.White, (int)BitPieceType.Pawn]);

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
            }), target.Bitboard_Pieces[(int)ChessColor.White, (int)BitPieceType.Rook]);

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
            }), target.Bitboard_Pieces[(int)ChessColor.White, (int)BitPieceType.Knight]);

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
            }), target.Bitboard_Pieces[(int)ChessColor.White, (int)BitPieceType.Bishop]);

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
            }), target.Bitboard_Pieces[(int)ChessColor.White, (int)BitPieceType.Queen]);

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
            }), target.Bitboard_Pieces[(int)ChessColor.White, (int)BitPieceType.King]);

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
            }), target.Bitboard_Pieces[(int)ChessColor.Black, (int)BitPieceType.Pawn]);

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
            }), target.Bitboard_Pieces[(int)ChessColor.Black, (int)BitPieceType.Rook]);

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
            }), target.Bitboard_Pieces[(int)ChessColor.Black, (int)BitPieceType.Knight]);

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
            }), target.Bitboard_Pieces[(int)ChessColor.Black, (int)BitPieceType.Bishop]);

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
            }), target.Bitboard_Pieces[(int)ChessColor.Black, (int)BitPieceType.Queen]);

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
            }), target.Bitboard_Pieces[(int)ChessColor.Black, (int)BitPieceType.King]);
        }

        [TestMethod]
        public void SetPieceQueenTest()
        {
            var target = new Bitboards();

            target.SetPiece(ChessColor.White, BitPieceType.Queen, Square.F5);

            Assert.AreEqual(BitPieceType.Queen, target.BoardAllPieces[(int)Square.F5]);
            Assert.AreEqual(ChessColor.White, target.BoardColor[(int)Square.F5]);
            Assert.AreEqual(Math.Pow(2, (int)Square.F5), target.Bitboard_Pieces[(int)ChessColor.White, (int)BitPieceType.Queen]);
            Assert.AreEqual(Math.Pow(2, (int)Square.F5), target.Bitboard_Pieces[(int)ChessColor.White, (int)BitPieceType.Queen]);
        }

        [TestMethod]
        public void SetPieceTwoQueensTest()
        {
            var target = new Bitboards();

            target.SetPiece(ChessColor.White, BitPieceType.Queen, Square.F5);
            target.SetPiece(ChessColor.White, BitPieceType.Queen, Square.A7);

            Assert.AreEqual(BitPieceType.Queen, target.BoardAllPieces[(int)Square.F5]);
            Assert.AreEqual(BitPieceType.Queen, target.BoardAllPieces[(int)Square.A7]);
            Assert.AreEqual(ChessColor.White, target.BoardColor[(int)Square.F5]);
            Assert.AreEqual(ChessColor.White, target.BoardColor[(int)Square.A7]);
            Assert.AreEqual(Math.Pow(2, (int)Square.F5) + Math.Pow(2, (int)Square.A7), target.Bitboard_Pieces[(int)ChessColor.White, (int)BitPieceType.Queen]);
        }

        [TestMethod]
        public void GetPieceQueenTest()
        {
            var target = new Bitboards();

            target.SetPiece(ChessColor.White, BitPieceType.Queen, Square.F5);
            var result = target.GetPiece(Square.F5);

            Assert.AreEqual(ChessColor.White, result.Color);
            Assert.AreEqual(BitPieceType.Queen, result.Piece);
        }

        [TestMethod]
        public void GetPieceTwoQueenTest()
        {
            var target = new Bitboards();
            target.SetPiece(ChessColor.White, BitPieceType.Queen, Square.F5);
            target.SetPiece(ChessColor.White, BitPieceType.Queen, Square.B3);

            var result = target.GetPiece(Square.F5);
            var result2 = target.GetPiece(Square.B3);

            Assert.AreEqual(ChessColor.White, result.Color);
            Assert.AreEqual(BitPieceType.Queen, result.Piece);
            Assert.AreEqual(ChessColor.White, result2.Color);
            Assert.AreEqual(BitPieceType.Queen, result2.Piece);
        }
    }
}
