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
            target.Initialize();
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
            target.Initialize();

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
            }), target.BetweenMatrix[Const.D1, Const.D8]);

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
            }), target.BetweenMatrix[Const.D8, Const.D1]);

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
            }), target.BetweenMatrix[Const.B3, Const.H3]);

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
            }), target.BetweenMatrix[Const.H3, Const.B3]);

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
            }), target.BetweenMatrix[Const.H3, Const.D7]);

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
            }), target.BetweenMatrix[Const.D7, Const.H3]);

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
            }), target.BetweenMatrix[Const.B1, Const.H7]);

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
            }), target.BetweenMatrix[Const.H7, Const.B1]);
        }

        [TestMethod]
        public void GetEdgeTest()
        {
            var target = new Bitboards();

            Assert.AreEqual(Const.A1, target.GetEdge(Const.A1, -1));
            Assert.AreEqual(Const.H1, target.GetEdge(Const.H1, 1));
            Assert.AreEqual(Const.A1, target.GetEdge(Const.A1, -8));
            Assert.AreEqual(Const.A8, target.GetEdge(Const.A8, 8));

            Assert.AreEqual(Const.H1, target.GetEdge(Const.C1, 1));
            Assert.AreEqual(Const.A1, target.GetEdge(Const.F1, -1));
            Assert.AreEqual(Const.G1, target.GetEdge(Const.G5, -8));
            Assert.AreEqual(Const.H8, target.GetEdge(Const.H5, 8));

            Assert.AreEqual(Const.A4, target.GetEdge(Const.D1, 7));
            Assert.AreEqual(Const.H5, target.GetEdge(Const.D1, 9));
            Assert.AreEqual(Const.H4, target.GetEdge(Const.D8, -7));
            Assert.AreEqual(Const.A5, target.GetEdge(Const.D8, -9));

            Assert.AreEqual(Const.H8, target.GetEdge(Const.E5, 9));
        }

        [TestMethod]
        public void RayAfterMatrixTest()
        {
            var target = new Bitboards();
            target.Initialize();

            Assert.AreEqual((UInt64)0x00, target.RayAfterMatrix[14, 1]);

            Assert.AreEqual(Bitboards.ConvertToUInt64(new byte[64]
            {//(A1)     (D1)
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 1, 0, 0, 0, 0,
                0, 0, 0, 1, 0, 0, 0, 0,
                0, 0, 0, 1, 0, 0, 0, 0
                //     (D8)         (H8)
            }), target.RayAfterMatrix[Const.D1, Const.D6]);

            Assert.AreEqual(Bitboards.ConvertToUInt64(new byte[64]
            {//(A1)     (D1)
                0, 0, 0, 1, 0, 0, 0, 0,
                0, 0, 0, 1, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0
                //     (D8)         (H8)
            }), target.RayAfterMatrix[Const.D6, Const.D2]);

            var t1 = Bitboards.PrintBitboard(target.RayAfterMatrix[Const.A1, Const.A8]);
            Assert.AreEqual(Bitboards.ConvertToUInt64(new byte[64]
            {//(A1)     (D1)
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                1, 0, 0, 0, 0, 0, 0, 0
                //                  (H8)
            }), target.RayAfterMatrix[Const.A1, Const.A8]);

            var t2 = Bitboards.PrintBitboard(target.RayAfterMatrix[Const.A1, Const.A2]);
            Assert.AreEqual(Bitboards.ConvertToUInt64(new byte[64]
            {//(A1)     (D1)
                0, 0, 0, 0, 0, 0, 0, 0,
                1, 0, 0, 0, 0, 0, 0, 0,
                1, 0, 0, 0, 0, 0, 0, 0,
                1, 0, 0, 0, 0, 0, 0, 0,
                1, 0, 0, 0, 0, 0, 0, 0,
                1, 0, 0, 0, 0, 0, 0, 0,
                1, 0, 0, 0, 0, 0, 0, 0,
                1, 0, 0, 0, 0, 0, 0, 0
                //                  (H8)
            }), target.RayAfterMatrix[Const.A1, Const.A2]);

            Assert.AreEqual(Bitboards.ConvertToUInt64(new byte[64]
            {//(A1)     (D1)
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 1, 1, 1, 1,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0
                //     (D8)         (H8)
            }), target.RayAfterMatrix[Const.B2, Const.E2]);

            Assert.AreEqual(Bitboards.ConvertToUInt64(new byte[64]
            {//(A1)     (D1)
                0, 0, 0, 0, 0, 0, 0, 0,
                1, 1, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0
                //     (D8)         (H8)
            }), target.RayAfterMatrix[Const.E2, Const.B2]);

            Assert.AreEqual(Bitboards.ConvertToUInt64(new byte[64]
            {//(A1)          
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 1, 0, 0, 0, 0, 0,
                0, 0, 0, 1, 0, 0, 0, 0,
                0, 0, 0, 0, 1, 0, 0, 0,
                0, 0, 0, 0, 0, 1, 0, 0,
                0, 0, 0, 0, 0, 0, 1, 0,
                0, 0, 0, 0, 0, 0, 0, 1,
                0, 0, 0, 0, 0, 0, 0, 0
                //     (D8)         (H8)
            }), target.RayAfterMatrix[Const.B1, Const.C2]);

            Assert.AreEqual(Bitboards.ConvertToUInt64(new byte[64]
            {//(A1)     (D1)
                1, 0, 0, 0, 0, 0, 0, 0,
                0, 1, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0
                //     (D8)         (H8)
            }), target.RayAfterMatrix[Const.C3, Const.B2]);

            Assert.AreEqual(Bitboards.ConvertToUInt64(new byte[64]
           {//    b1 
                0, 0, 0, 0, 1, 0, 0, 0,
                0, 0, 0, 1, 0, 0, 0, 0,
                0, 0, 1, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0, // h7
                0, 0, 0, 0, 0, 0, 0, 0
               //       d7         (H8)
           }), target.RayAfterMatrix[Const.B4, Const.C3]);

            Assert.AreEqual(Bitboards.ConvertToUInt64(new byte[64]
            {//    b1 
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 1, 0, 0, 0, 0, 0, 0,
                1, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0, // h7
                0, 0, 0, 0, 0, 0, 0, 0
               //       d7         (H8)
            }), target.RayAfterMatrix[Const.C3, Const.B4]);
        }

        [TestMethod]
        public void MovesKnightTest()
        {
            var target = new Bitboards();
            target.Initialize();

            var boardE5 = Bitboards.PrintBitboard(target.MovesKnight[Const.E5]);
            var boardD1 = Bitboards.PrintBitboard(target.MovesKnight[Const.D1]);
            var boardD2 = Bitboards.PrintBitboard(target.MovesKnight[Const.D2]);

            Assert.AreEqual(Bitboards.ConvertToUInt64(new byte[64]
            {//(A1)     (D1)
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 1, 0, 0, 0, 1, 0, 0,
                0, 0, 1, 0, 1, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0
                //                  (H8)
            }), target.MovesKnight[Const.D1]);

            Assert.AreEqual(Bitboards.ConvertToUInt64(new byte[64]
            {//(A1)     (D2)
                0, 1, 0, 0, 0, 1, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 1, 0, 0, 0, 1, 0, 0,
                0, 0, 1, 0, 1, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                //                  (H8)
            }), target.MovesKnight[Const.D2]);

            Assert.AreEqual(Bitboards.ConvertToUInt64(new byte[64]
            {//(A1)         
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 1, 0, 1, 0, 0,
                0, 0, 1, 0, 0, 0, 1, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 1, 0, 0, 0, 1, 0,
                0, 0, 0, 1, 0, 1, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0
                //                  (H8)
            }), target.MovesKnight[Const.E5]);
        }

        [TestMethod]
        public void MovesKingTest()
        {
            var target = new Bitboards();
            target.Initialize();

            var boardE5 = Bitboards.PrintBitboard(target.MovesKing[Const.E5]);
            var boardD1 = Bitboards.PrintBitboard(target.MovesKing[Const.D1]);
            var boardH1 = Bitboards.PrintBitboard(target.MovesKing[Const.H1]);

            Assert.AreEqual(Bitboards.ConvertToUInt64(new byte[64]
            {//(A1)     (D1)
                0, 0, 1, 0, 1, 0, 0, 0,
                0, 0, 1, 1, 1, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0
                //                  (H8)
            }), target.MovesKing[Const.D1]);

            Assert.AreEqual(Bitboards.ConvertToUInt64(new byte[64]
           {//(A1)         
                0, 0, 0, 0, 0, 0, 1, 0,
                0, 0, 0, 0, 0, 0, 1, 1,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0
               //                  (H8)
           }), target.MovesKing[Const.H1]);

            Assert.AreEqual(Bitboards.ConvertToUInt64(new byte[64]
            {//(A1)         
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 1, 1, 1, 0, 0,
                0, 0, 0, 1, 0, 1, 0, 0,
                0, 0, 0, 1, 1, 1, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0
                //                  (H8)
            }), target.MovesKing[Const.E5]);
        }

        [TestMethod]
        public void MovesRookTest()
        {
            var target = new Bitboards();
            target.Initialize();

            var boardE5 = Bitboards.PrintBitboard(target.MovesRook[Const.E5]);
            var boardD1 = Bitboards.PrintBitboard(target.MovesRook[Const.D1]);
            var boardH1 = Bitboards.PrintBitboard(target.MovesRook[Const.H1]);

            Assert.AreEqual(Bitboards.ConvertToUInt64(new byte[64]
            {//(A1)     (D1)
                1, 1, 1, 0, 1, 1, 1, 1,
                0, 0, 0, 1, 0, 0, 0, 0,
                0, 0, 0, 1, 0, 0, 0, 0,
                0, 0, 0, 1, 0, 0, 0, 0,
                0, 0, 0, 1, 0, 0, 0, 0,
                0, 0, 0, 1, 0, 0, 0, 0,
                0, 0, 0, 1, 0, 0, 0, 0,
                0, 0, 0, 1, 0, 0, 0, 0
                //                  (H8)
            }), target.MovesRook[Const.D1]);

            Assert.AreEqual(Bitboards.ConvertToUInt64(new byte[64]
           {//(A1)         
                1, 1, 1, 1, 1, 1, 1, 0,
                0, 0, 0, 0, 0, 0, 0, 1,
                0, 0, 0, 0, 0, 0, 0, 1,
                0, 0, 0, 0, 0, 0, 0, 1,
                0, 0, 0, 0, 0, 0, 0, 1,
                0, 0, 0, 0, 0, 0, 0, 1,
                0, 0, 0, 0, 0, 0, 0, 1,
                0, 0, 0, 0, 0, 0, 0, 1
               //                  (H8)
           }), target.MovesRook[Const.H1]);

            Assert.AreEqual(Bitboards.ConvertToUInt64(new byte[64]
            {//(A1)         
                0, 0, 0, 0, 1, 0, 0, 0,
                0, 0, 0, 0, 1, 0, 0, 0,
                0, 0, 0, 0, 1, 0, 0, 0,
                0, 0, 0, 0, 1, 0, 0, 0,
                1, 1, 1, 1, 0, 1, 1, 1,
                0, 0, 0, 0, 1, 0, 0, 0,
                0, 0, 0, 0, 1, 0, 0, 0,
                0, 0, 0, 0, 1, 0, 0, 0
                //                  (H8)
            }), target.MovesRook[Const.E5]);
        }

        [TestMethod]
        public void MovesBishopTest()
        {
            var target = new Bitboards();
            target.Initialize();

            var boardE5 = Bitboards.PrintBitboard(target.MovesBishop[Const.E5]);
            var boardD1 = Bitboards.PrintBitboard(target.MovesBishop[Const.D1]);
            var boardH1 = Bitboards.PrintBitboard(target.MovesBishop[Const.H1]);

            Assert.AreEqual(Bitboards.ConvertToUInt64(new byte[64]
            {//(A1)     (D1)
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 1, 0, 1, 0, 0, 0,
                0, 1, 0, 0, 0, 1, 0, 0,
                1, 0, 0, 0, 0, 0, 1, 0,
                0, 0, 0, 0, 0, 0, 0, 1,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0
                //                  (H8)
            }), target.MovesBishop[Const.D1]);

            Assert.AreEqual(Bitboards.ConvertToUInt64(new byte[64]
           {//(A1)         
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 1, 0,
                0, 0, 0, 0, 0, 1, 0, 0,
                0, 0, 0, 0, 1, 0, 0, 0,
                0, 0, 0, 1, 0, 0, 0, 0,
                0, 0, 1, 0, 0, 0, 0, 0,
                0, 1, 0, 0, 0, 0, 0, 0,
                1, 0, 0, 0, 0, 0, 0, 0
               //                  (H8)
           }), target.MovesBishop[Const.H1]);

            Assert.AreEqual(Bitboards.ConvertToUInt64(new byte[64]
            {//(A1)         
                1, 0, 0, 0, 0, 0, 0, 0,
                0, 1, 0, 0, 0, 0, 0, 1,
                0, 0, 1, 0, 0, 0, 1, 0,
                0, 0, 0, 1, 0, 1, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 1, 0, 1, 0, 0,
                0, 0, 1, 0, 0, 0, 1, 0,
                0, 1, 0, 0, 0, 0, 0, 1
                //                  (H8)
            }), target.MovesBishop[Const.E5]);
        }

        [TestMethod]
        public void MovesQueenTest()
        {
            var target = new Bitboards();
            target.Initialize();

            var boardE5 = Bitboards.PrintBitboard(target.MovesQueen[Const.E5]);
            var boardD1 = Bitboards.PrintBitboard(target.MovesQueen[Const.D1]);
            var boardH1 = Bitboards.PrintBitboard(target.MovesQueen[Const.H1]);

            Assert.AreEqual(Bitboards.ConvertToUInt64(new byte[64]
            {//(A1)     (D1)
                1, 1, 1, 0, 1, 1, 1, 1,
                0, 0, 1, 1, 1, 0, 0, 0,
                0, 1, 0, 1, 0, 1, 0, 0,
                1, 0, 0, 1, 0, 0, 1, 0,
                0, 0, 0, 1, 0, 0, 0, 1,
                0, 0, 0, 1, 0, 0, 0, 0,
                0, 0, 0, 1, 0, 0, 0, 0,
                0, 0, 0, 1, 0, 0, 0, 0
                //                  (H8)
            }), target.MovesQueen[Const.D1]);

            Assert.AreEqual(Bitboards.ConvertToUInt64(new byte[64]
           {//(A1)         
                1, 1, 1, 1, 1, 1, 1, 0,
                0, 0, 0, 0, 0, 0, 1, 1,
                0, 0, 0, 0, 0, 1, 0, 1,
                0, 0, 0, 0, 1, 0, 0, 1,
                0, 0, 0, 1, 0, 0, 0, 1,
                0, 0, 1, 0, 0, 0, 0, 1,
                0, 1, 0, 0, 0, 0, 0, 1,
                1, 0, 0, 0, 0, 0, 0, 1
               //                  (H8)
           }), target.MovesQueen[Const.H1]);

            Assert.AreEqual(Bitboards.ConvertToUInt64(new byte[64]
            {//(A1)         
                1, 0, 0, 0, 1, 0, 0, 0,
                0, 1, 0, 0, 1, 0, 0, 1,
                0, 0, 1, 0, 1, 0, 1, 0,
                0, 0, 0, 1, 1, 1, 0, 0,
                1, 1, 1, 1, 0, 1, 1, 1,
                0, 0, 0, 1, 1, 1, 0, 0,
                0, 0, 1, 0, 1, 0, 1, 0,
                0, 1, 0, 0, 1, 0, 0, 1
                //                  (H8)
            }), target.MovesQueen[Const.E5]);
        }

        [TestMethod]
        public void WhitePawnCaptuesTest()
        {
            var target = new Bitboards();
            target.Initialize();

            var boardE5 = Bitboards.PrintBitboard(target.WhitePawnCaptures[Const.E5]);
            var boardA2 = Bitboards.PrintBitboard(target.WhitePawnCaptures[Const.A2]);
            var boardH2 = Bitboards.PrintBitboard(target.WhitePawnCaptures[Const.H2]);

            Assert.AreEqual(Bitboards.ConvertToUInt64(new byte[64]
            {//(A1)     (D1)
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 1, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                //                  (H8)
            }), target.WhitePawnCaptures[Const.A2]);

            Assert.AreEqual(Bitboards.ConvertToUInt64(new byte[64]
            {//(A1)
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 1, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                //                  (H8)
            }), target.WhitePawnCaptures[Const.H2]);

            Assert.AreEqual(Bitboards.ConvertToUInt64(new byte[64]
            {//(A1)
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 1, 0, 1, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                //                  (H8)
            }), target.WhitePawnCaptures[Const.E5]);
        }

        [TestMethod]
        public void BlackPawnCaptuesTest()
        {
            var target = new Bitboards();
            target.Initialize();

            var boardE5 = Bitboards.PrintBitboard(target.BlackPawnCaptures[Const.E5]);
            var boardA7 = Bitboards.PrintBitboard(target.BlackPawnCaptures[Const.A7]);
            var boardH7 = Bitboards.PrintBitboard(target.BlackPawnCaptures[Const.H7]);

            Assert.AreEqual(Bitboards.ConvertToUInt64(new byte[64]
            {//(A1)     (D1)
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 1, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                //                  (H8)
            }), target.BlackPawnCaptures[Const.A7]);

            Assert.AreEqual(Bitboards.ConvertToUInt64(new byte[64]
            {//(A1)
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 1, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                //                  (H8)
            }), target.BlackPawnCaptures[Const.H7]);

            Assert.AreEqual(Bitboards.ConvertToUInt64(new byte[64]
            {//(A1)
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 1, 0, 1, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                //                  (H8)
            }), target.BlackPawnCaptures[Const.E5]);
        }

        [TestMethod]
        public void WhitePawnStraightMovesTest()
        {
            var target = new Bitboards();
            target.Initialize();

            var boardE5 = Bitboards.PrintBitboard(target.WhiteMovesPawn[Const.E5]);
            var boardA2 = Bitboards.PrintBitboard(target.WhiteMovesPawn[Const.A2]);
            var boardH2 = Bitboards.PrintBitboard(target.WhiteMovesPawn[Const.H2]);

            Assert.AreEqual(Bitboards.ConvertToUInt64(new byte[64]
            {//(A1)     (D1)
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                1, 0, 0, 0, 0, 0, 0, 0,
                1, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                //                  (H8)
            }), target.WhiteMovesPawn[Const.A2]);

            Assert.AreEqual(Bitboards.ConvertToUInt64(new byte[64]
            {//(A1)
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 1,
                0, 0, 0, 0, 0, 0, 0, 1,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                //                  (H8)
            }), target.WhiteMovesPawn[Const.H2]);

            Assert.AreEqual(Bitboards.ConvertToUInt64(new byte[64]
            {//(A1)
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 1, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                //                  (H8)
            }), target.WhiteMovesPawn[Const.E5]);
        }

        [TestMethod]
        public void BlackPawnStraightMovesTest()
        {
            var target = new Bitboards();
            target.Initialize();

            var boardE5 = Bitboards.PrintBitboard(target.BlackMovesPawn[Const.E5]);
            var boardA7 = Bitboards.PrintBitboard(target.BlackMovesPawn[Const.A7]);
            var boardH7 = Bitboards.PrintBitboard(target.BlackMovesPawn[Const.H7]);

            Assert.AreEqual(Bitboards.ConvertToUInt64(new byte[64]
            {//(A1)     (D1)
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                1, 0, 0, 0, 0, 0, 0, 0,
                1, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                //                  (H8)
            }), target.BlackMovesPawn[Const.A7]);

            Assert.AreEqual(Bitboards.ConvertToUInt64(new byte[64]
            {//(A1)
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 1,
                0, 0, 0, 0, 0, 0, 0, 1,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                //                  (H8)
            }), target.BlackMovesPawn[Const.H7]);

            Assert.AreEqual(Bitboards.ConvertToUInt64(new byte[64]
            {//(A1)
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 1, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                //                  (H8)
            }), target.BlackMovesPawn[Const.E5]);
        }

        [TestMethod]
        public void MaskWhitePassedPawnTest()
        {
            var target = new Bitboards();
            target.Initialize();

            var boardE5 = Bitboards.PrintBitboard(target.MaskWhitePassedPawns[Const.E5]);
            var boardA2 = Bitboards.PrintBitboard(target.MaskWhitePassedPawns[Const.A2]);
            var boardH2 = Bitboards.PrintBitboard(target.MaskWhitePassedPawns[Const.H2]);

            Assert.AreEqual(Bitboards.ConvertToUInt64(new byte[64]
            {//(A1)     (D1)
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                1, 1, 0, 0, 0, 0, 0, 0,
                1, 1, 0, 0, 0, 0, 0, 0,
                1, 1, 0, 0, 0, 0, 0, 0,
                1, 1, 0, 0, 0, 0, 0, 0,
                1, 1, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                //                  (H8)
            }), target.MaskWhitePassedPawns[Const.A2]);

            Assert.AreEqual(Bitboards.ConvertToUInt64(new byte[64]
            {//(A1)
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 1, 1,
                0, 0, 0, 0, 0, 0, 1, 1,
                0, 0, 0, 0, 0, 0, 1, 1,
                0, 0, 0, 0, 0, 0, 1, 1,
                0, 0, 0, 0, 0, 0, 1, 1,
                0, 0, 0, 0, 0, 0, 0, 0,
                //                  (H8)
            }), target.MaskWhitePassedPawns[Const.H2]);

            Assert.AreEqual(Bitboards.ConvertToUInt64(new byte[64]
            {//(A1)
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 1, 1, 1, 0, 0,
                0, 0, 0, 1, 1, 1, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                //                  (H8)
            }), target.MaskWhitePassedPawns[Const.E5]);
        }

        [TestMethod]
        public void MaskBlackPassedPawnTest()
        {
            var target = new Bitboards();
            target.Initialize();

            var boardE5 = Bitboards.PrintBitboard(target.MaskBlackPassedPawns[Const.E5]);
            var boardA7 = Bitboards.PrintBitboard(target.MaskBlackPassedPawns[Const.A7]);
            var boardH7 = Bitboards.PrintBitboard(target.MaskBlackPassedPawns[Const.H7]);

            Assert.AreEqual(Bitboards.ConvertToUInt64(new byte[64]
            {//(A1)     (D1)
                0, 0, 0, 0, 0, 0, 0, 0,
                1, 1, 0, 0, 0, 0, 0, 0,
                1, 1, 0, 0, 0, 0, 0, 0,
                1, 1, 0, 0, 0, 0, 0, 0,
                1, 1, 0, 0, 0, 0, 0, 0,
                1, 1, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                //                  (H8)
            }), target.MaskBlackPassedPawns[Const.A7]);

            Assert.AreEqual(Bitboards.ConvertToUInt64(new byte[64]
            {//(A1)
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 1, 1,
                0, 0, 0, 0, 0, 0, 1, 1,
                0, 0, 0, 0, 0, 0, 1, 1,
                0, 0, 0, 0, 0, 0, 1, 1,
                0, 0, 0, 0, 0, 0, 1, 1,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                //                  (H8)
            }), target.MaskBlackPassedPawns[Const.H7]);

            Assert.AreEqual(Bitboards.ConvertToUInt64(new byte[64]
            {//(A1)
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 1, 1, 1, 0, 0,
                0, 0, 0, 1, 1, 1, 0, 0,
                0, 0, 0, 1, 1, 1, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                //                  (H8)
            }), target.MaskBlackPassedPawns[Const.E5]);
        }

        [TestMethod]
        public void MaskIsolatedPawnTest()
        {
            var target = new Bitboards();
            target.Initialize();

            var boardE5 = Bitboards.PrintBitboard(target.MaskIsolatedPawns[Const.E5]);
            var boardA2 = Bitboards.PrintBitboard(target.MaskIsolatedPawns[Const.A2]);
            var boardH2 = Bitboards.PrintBitboard(target.MaskIsolatedPawns[Const.H2]);

            Assert.AreEqual(Bitboards.ConvertToUInt64(new byte[64]
            {//(A1)     (D1)
                0, 1, 0, 0, 0, 0, 0, 0,
                0, 1, 0, 0, 0, 0, 0, 0,
                0, 1, 0, 0, 0, 0, 0, 0,
                0, 1, 0, 0, 0, 0, 0, 0,
                0, 1, 0, 0, 0, 0, 0, 0,
                0, 1, 0, 0, 0, 0, 0, 0,
                0, 1, 0, 0, 0, 0, 0, 0,
                0, 1, 0, 0, 0, 0, 0, 0,
                //                  (H8)
            }), target.MaskIsolatedPawns[Const.A2]);

            Assert.AreEqual(Bitboards.ConvertToUInt64(new byte[64]
            {//(A1)
                0, 0, 0, 0, 0, 0, 1, 0,
                0, 0, 0, 0, 0, 0, 1, 0,
                0, 0, 0, 0, 0, 0, 1, 0,
                0, 0, 0, 0, 0, 0, 1, 0,
                0, 0, 0, 0, 0, 0, 1, 0,
                0, 0, 0, 0, 0, 0, 1, 0,
                0, 0, 0, 0, 0, 0, 1, 0,
                0, 0, 0, 0, 0, 0, 1, 0,
                //                  (H8)
            }), target.MaskIsolatedPawns[Const.H2]);

            Assert.AreEqual(Bitboards.ConvertToUInt64(new byte[64]
            {//(A1)
                0, 0, 0, 1, 0, 1, 0, 0,
                0, 0, 0, 1, 0, 1, 0, 0,
                0, 0, 0, 1, 0, 1, 0, 0,
                0, 0, 0, 1, 0, 1, 0, 0,
                0, 0, 0, 1, 0, 1, 0, 0,
                0, 0, 0, 1, 0, 1, 0, 0,
                0, 0, 0, 1, 0, 1, 0, 0,
                0, 0, 0, 1, 0, 1, 0, 0,
                //                  (H8)
            }), target.MaskIsolatedPawns[Const.E5]);
        }

        [TestMethod]
        public void MaskWhitePawnPathTest()
        {
            var target = new Bitboards();
            target.Initialize();

            var boardE5 = Bitboards.PrintBitboard(target.MaskWhitePawnsPath[Const.E5]);
            var boardA2 = Bitboards.PrintBitboard(target.MaskWhitePawnsPath[Const.A2]);
            var boardH2 = Bitboards.PrintBitboard(target.MaskWhitePawnsPath[Const.H2]);

            Assert.AreEqual(Bitboards.ConvertToUInt64(new byte[64]
            {//(A1)     (D1)
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                1, 0, 0, 0, 0, 0, 0, 0,
                1, 0, 0, 0, 0, 0, 0, 0,
                1, 0, 0, 0, 0, 0, 0, 0,
                1, 0, 0, 0, 0, 0, 0, 0,
                1, 0, 0, 0, 0, 0, 0, 0,
                1, 0, 0, 0, 0, 0, 0, 0,
                //                  (H8)
            }), target.MaskWhitePawnsPath[Const.A2]);

            Assert.AreEqual(Bitboards.ConvertToUInt64(new byte[64]
            {//(A1)
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 1,
                0, 0, 0, 0, 0, 0, 0, 1,
                0, 0, 0, 0, 0, 0, 0, 1,
                0, 0, 0, 0, 0, 0, 0, 1,
                0, 0, 0, 0, 0, 0, 0, 1,
                0, 0, 0, 0, 0, 0, 0, 1,
                //                  (H8)
            }), target.MaskWhitePawnsPath[Const.H2]);

            Assert.AreEqual(Bitboards.ConvertToUInt64(new byte[64]
            {//(A1)
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 1, 0, 0, 0,
                0, 0, 0, 0, 1, 0, 0, 0,
                0, 0, 0, 0, 1, 0, 0, 0,
                //                  (H8)
            }), target.MaskWhitePawnsPath[Const.E5]);
        }

        [TestMethod]
        public void MaskBlackPawnPathTest()
        {
            var target = new Bitboards();
            target.Initialize();

            var boardE5 = Bitboards.PrintBitboard(target.MaskBlackPawnsPath[Const.E5]);
            var boardA7 = Bitboards.PrintBitboard(target.MaskBlackPawnsPath[Const.A7]);
            var boardH7 = Bitboards.PrintBitboard(target.MaskBlackPawnsPath[Const.H7]);

            Assert.AreEqual(Bitboards.ConvertToUInt64(new byte[64]
            {//(A1)     (D1)
                1, 0, 0, 0, 0, 0, 0, 0,
                1, 0, 0, 0, 0, 0, 0, 0,
                1, 0, 0, 0, 0, 0, 0, 0,
                1, 0, 0, 0, 0, 0, 0, 0,
                1, 0, 0, 0, 0, 0, 0, 0,
                1, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                //                  (H8)
            }), target.MaskBlackPawnsPath[Const.A7]);

            Assert.AreEqual(Bitboards.ConvertToUInt64(new byte[64]
            {//(A1)
                0, 0, 0, 0, 0, 0, 0, 1,
                0, 0, 0, 0, 0, 0, 0, 1,
                0, 0, 0, 0, 0, 0, 0, 1,
                0, 0, 0, 0, 0, 0, 0, 1,
                0, 0, 0, 0, 0, 0, 0, 1,
                0, 0, 0, 0, 0, 0, 0, 1,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                //                  (H8)
            }), target.MaskBlackPawnsPath[Const.H7]);

            Assert.AreEqual(Bitboards.ConvertToUInt64(new byte[64]
            {//(A1)
                0, 0, 0, 0, 1, 0, 0, 0,
                0, 0, 0, 0, 1, 0, 0, 0,
                0, 0, 0, 0, 1, 0, 0, 0,
                0, 0, 0, 0, 1, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                //                  (H8)
            }), target.MaskBlackPawnsPath[Const.E5]);
        }
    }
}
