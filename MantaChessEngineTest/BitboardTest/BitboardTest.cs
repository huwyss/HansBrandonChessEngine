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

            Assert.AreEqual((UInt64)0xff00, target.Bitboard_WhitePawn);
            Assert.AreEqual((UInt64)0xff000000000000, target.Bitboard_BlackPawn);
        }

        [TestMethod]
        public void BetweenMatrixTest()
        {
            var target = new Bitboards();

            Assert.AreEqual((UInt64)0x00, target.BetweenMatrix[0, 1]);
            Assert.AreEqual((UInt64)0x00, target.BetweenMatrix[27, 28]);
            Assert.AreEqual((UInt64)0x00, target.BetweenMatrix[14, 1]);
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
