using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MantaChessEngine;

namespace MantaChessEngineTest
{
    [TestClass]
    public class BitboardTest
    {
        [TestMethod]
        public void PiecesBitboardTest()
        {
            var target = new Bitboards();
            target.SetInitialPosition();

            Assert.AreEqual(target.Bitboard_WhitePawn, Bitboards.ConvertToUInt64(new byte[64]
            {// a1
                0, 0, 0, 0, 0, 0, 0, 0,
                1, 1, 1, 1, 1, 1, 1, 1,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0
            }));                  // h8

            Assert.AreEqual(target.Bitboard_WhiteRook, Bitboards.ConvertToUInt64(new byte[64]
            {
                1, 0, 0, 0, 0, 0, 0, 1,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0
            }));

            Assert.AreEqual(target.Bitboard_WhiteKnight, Bitboards.ConvertToUInt64(new byte[64]
            {
                0, 1, 0, 0, 0, 0, 1, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
            }));

            Assert.AreEqual(target.Bitboard_WhiteBishop, Bitboards.ConvertToUInt64(new byte[64]
            {
                0, 0, 1, 0, 0, 1, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0
            }));

            Assert.AreEqual(target.Bitboard_WhiteQueen, Bitboards.ConvertToUInt64(new byte[64]
            {
                0, 0, 0, 1, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0
            }));

            Assert.AreEqual(target.Bitboard_WhiteKing, Bitboards.ConvertToUInt64(new byte[64]
            {
                0, 0, 0, 0, 1, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0
            }));

            Assert.AreEqual(target.Bitboard_BlackPawn, Bitboards.ConvertToUInt64(new byte[64]
            {
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                1, 1, 1, 1, 1, 1, 1, 1,
                0, 0, 0, 0, 0, 0, 0, 0
            }));

            Assert.AreEqual(target.Bitboard_BlackRook, Bitboards.ConvertToUInt64(new byte[64]
            {
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                1, 0, 0, 0, 0, 0, 0, 1
            }));

            Assert.AreEqual(target.Bitboard_BlackKnight, Bitboards.ConvertToUInt64(new byte[64]
            {
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 1, 0, 0, 0, 0, 1, 0
            }));

            Assert.AreEqual(target.Bitboard_BlackBishop, Bitboards.ConvertToUInt64(new byte[64]
            {
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 1, 0, 0, 1, 0, 0
            }));

            Assert.AreEqual(target.Bitboard_BlackQueen, Bitboards.ConvertToUInt64(new byte[64]
            {
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 1, 0, 0, 0, 0
            }));

            Assert.AreEqual(target.Bitboard_BlackKing, Bitboards.ConvertToUInt64(new byte[64]
            {
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 1, 0, 0, 0
            }));
        }

        [TestMethod]
        public void BetweenMatrixTest()
        {
            var target = new Bitboards();

            Assert.AreEqual((UInt64)0x00, target.BetweenMatrix[0, 1]);
            Assert.AreEqual((UInt64)0x00, target.BetweenMatrix[27, 28]);
            Assert.AreEqual((UInt64)0x00, target.BetweenMatrix[14, 1]);
            Assert.AreEqual((UInt64)0x00, target.BetweenMatrix[0, 8]);

            Assert.AreEqual(Bitboards.ConvertToUInt64(new byte[64]
            {//(A1)     (D1)
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 1, 0, 0, 0, 0,
                0, 0, 0, 1, 0, 0, 0, 0,
                0, 0, 0, 1, 0, 0, 0, 0,
                0, 0, 0, 1, 0, 0, 0, 0,
                0, 0, 0, 1, 0, 0, 0, 0,
                0, 0, 0, 1, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0
                //     (D8)         (H8)
            }), target.BetweenMatrix[Chess.D1, Chess.D8]);

            Assert.AreEqual(Bitboards.ConvertToUInt64(new byte[64]
            {//(A1)     (D1)
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 1, 0, 0, 0, 0,
                0, 0, 0, 1, 0, 0, 0, 0,
                0, 0, 0, 1, 0, 0, 0, 0,
                0, 0, 0, 1, 0, 0, 0, 0,
                0, 0, 0, 1, 0, 0, 0, 0,
                0, 0, 0, 1, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0
                //     (D8)         (H8)
            }), target.BetweenMatrix[Chess.D8, Chess.D1]);

            Assert.AreEqual(Bitboards.ConvertToUInt64(new byte[64]
            {//(A1)   
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
         /*b3*/ 0, 0, 1, 1, 1, 1, 1, 0, // h3
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0
                //                  (H8)
            }), target.BetweenMatrix[Chess.B3, Chess.H3]);

            Assert.AreEqual(Bitboards.ConvertToUInt64(new byte[64]
            {//(A1)   
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
         /*b3*/ 0, 0, 1, 1, 1, 1, 1, 0, // h3
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0
                //     (D8)         (H8)
            }), target.BetweenMatrix[Chess.H3, Chess.B3]);

            Assert.AreEqual(Bitboards.ConvertToUInt64(new byte[64]
            {//(A1)   
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0, // h3
                0, 0, 0, 0, 0, 0, 1, 0,
                0, 0, 0, 0, 0, 1, 0, 0,
                0, 0, 0, 0, 1, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0
               //       d7         (H8)
            }), target.BetweenMatrix[Chess.H3, Chess.D7]);

            Assert.AreEqual(Bitboards.ConvertToUInt64(new byte[64]
            {//(A1)   
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0, // h3
                0, 0, 0, 0, 0, 0, 1, 0,
                0, 0, 0, 0, 0, 1, 0, 0,
                0, 0, 0, 0, 1, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0
               //       d7         (H8)
            }), target.BetweenMatrix[Chess.D7, Chess.H3]);

            Assert.AreEqual(Bitboards.ConvertToUInt64(new byte[64]
            {//    b1 
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 1, 0, 0, 0, 0, 0,
                0, 0, 0, 1, 0, 0, 0, 0,
                0, 0, 0, 0, 1, 0, 0, 0,
                0, 0, 0, 0, 0, 1, 0, 0,
                0, 0, 0, 0, 0, 0, 1, 0,
                0, 0, 0, 0, 0, 0, 0, 0, // h7
                0, 0, 0, 0, 0, 0, 0, 0
               //       d7         (H8)
            }), target.BetweenMatrix[Chess.B1, Chess.H7]);

            Assert.AreEqual(Bitboards.ConvertToUInt64(new byte[64]
            {//    b1 
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 1, 0, 0, 0, 0, 0,
                0, 0, 0, 1, 0, 0, 0, 0,
                0, 0, 0, 0, 1, 0, 0, 0,
                0, 0, 0, 0, 0, 1, 0, 0,
                0, 0, 0, 0, 0, 0, 1, 0,
                0, 0, 0, 0, 0, 0, 0, 0, // h7
                0, 0, 0, 0, 0, 0, 0, 0
               //       d7         (H8)
            }), target.BetweenMatrix[Chess.H7, Chess.B1]);
        }
    }
}
