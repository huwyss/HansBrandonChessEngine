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
            }), target.BetweenMatrix[(int)Square.D1, (int)Square.D8]);

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
            }), target.BetweenMatrix[(int)Square.D8, (int)Square.D1]);

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
            }), target.BetweenMatrix[(int)Square.B3, (int)Square.H3]);

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
            }), target.BetweenMatrix[(int)Square.H3, (int)Square.B3]);

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
            }), target.BetweenMatrix[(int)Square.H3, (int)Square.D7]);

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
            }), target.BetweenMatrix[(int)Square.D7, (int)Square.H3]);

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
            }), target.BetweenMatrix[(int)Square.B1, (int)Square.H7]);

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
            }), target.BetweenMatrix[(int)Square.H7, (int)Square.B1]);
        }

        [TestMethod]
        public void GetEdgeTest()
        {
            var target = new Bitboards();

            Assert.AreEqual((int)Square.A1, target.GetEdge((int)Square.A1, -1));
            Assert.AreEqual((int)Square.H1, target.GetEdge((int)Square.H1, 1));
            Assert.AreEqual((int)Square.A1, target.GetEdge((int)Square.A1, -8));
            Assert.AreEqual((int)Square.A8, target.GetEdge((int)Square.A8, 8));

            Assert.AreEqual((int)Square.H1, target.GetEdge((int)Square.C1, 1));
            Assert.AreEqual((int)Square.A1, target.GetEdge((int)Square.F1, -1));
            Assert.AreEqual((int)Square.G1, target.GetEdge((int)Square.G5, -8));
            Assert.AreEqual((int)Square.H8, target.GetEdge((int)Square.H5, 8));

            Assert.AreEqual((int)Square.A4, target.GetEdge((int)Square.D1, 7));
            Assert.AreEqual((int)Square.H5, target.GetEdge((int)Square.D1, 9));
            Assert.AreEqual((int)Square.H4, target.GetEdge((int)Square.D8, -7));
            Assert.AreEqual((int)Square.A5, target.GetEdge((int)Square.D8, -9));

            Assert.AreEqual((int)Square.H8, target.GetEdge((int)Square.E5, 9));
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
            }), target.RayAfterMatrix[(int)Square.D1, (int)Square.D6]);

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
            }), target.RayAfterMatrix[(int)Square.D6, (int)Square.D2]);

            var t1 = Bitboards.PrintBitboard(target.RayAfterMatrix[(int)Square.A1, (int)Square.A8]);
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
            }), target.RayAfterMatrix[(int)Square.A1, (int)Square.A8]);

            var t2 = Bitboards.PrintBitboard(target.RayAfterMatrix[(int)Square.A1, (int)Square.A2]);
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
            }), target.RayAfterMatrix[(int)Square.A1, (int)Square.A2]);

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
            }), target.RayAfterMatrix[(int)Square.B2, (int)Square.E2]);

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
            }), target.RayAfterMatrix[(int)Square.E2, (int)Square.B2]);

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
            }), target.RayAfterMatrix[(int)Square.B1, (int)Square.C2]);

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
            }), target.RayAfterMatrix[(int)Square.C3, (int)Square.B2]);

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
           }), target.RayAfterMatrix[(int)Square.B4, (int)Square.C3]);

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
            }), target.RayAfterMatrix[(int)Square.C3, (int)Square.B4]);
        }

        [TestMethod]
        public void MovesKnightTest()
        {
            var target = new Bitboards();
            target.Initialize();

            var boardE5 = Bitboards.PrintBitboard(target.MovesKnight[(int)Square.E5]);
            var boardD1 = Bitboards.PrintBitboard(target.MovesKnight[(int)Square.D1]);
            var boardD2 = Bitboards.PrintBitboard(target.MovesKnight[(int)Square.D2]);

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
            }), target.MovesKnight[(int)Square.D1]);

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
            }), target.MovesKnight[(int)Square.D2]);

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
            }), target.MovesKnight[(int)Square.E5]);
        }

        [TestMethod]
        public void MovesKingTest()
        {
            var target = new Bitboards();
            target.Initialize();

            var boardE5 = Bitboards.PrintBitboard(target.MovesKing[(int)Square.E5]);
            var boardD1 = Bitboards.PrintBitboard(target.MovesKing[(int)Square.D1]);
            var boardH1 = Bitboards.PrintBitboard(target.MovesKing[(int)Square.H1]);

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
            }), target.MovesKing[(int)Square.D1]);

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
           }), target.MovesKing[(int)Square.H1]);

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
            }), target.MovesKing[(int)Square.E5]);
        }

        [TestMethod]
        public void MovesRookTest()
        {
            var target = new Bitboards();
            target.Initialize();

            var boardE5 = Bitboards.PrintBitboard(target.MovesRook[(int)Square.E5]);
            var boardD1 = Bitboards.PrintBitboard(target.MovesRook[(int)Square.D1]);
            var boardH1 = Bitboards.PrintBitboard(target.MovesRook[(int)Square.H1]);

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
            }), target.MovesRook[(int)Square.D1]);

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
           }), target.MovesRook[(int)Square.H1]);

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
            }), target.MovesRook[(int)Square.E5]);
        }

        [TestMethod]
        public void MovesBishopTest()
        {
            var target = new Bitboards();
            target.Initialize();

            var boardE5 = Bitboards.PrintBitboard(target.MovesBishop[(int)Square.E5]);
            var boardD1 = Bitboards.PrintBitboard(target.MovesBishop[(int)Square.D1]);
            var boardH1 = Bitboards.PrintBitboard(target.MovesBishop[(int)Square.H1]);

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
            }), target.MovesBishop[(int)Square.D1]);

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
           }), target.MovesBishop[(int)Square.H1]);

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
            }), target.MovesBishop[(int)Square.E5]);
        }

        [TestMethod]
        public void MovesQueenTest()
        {
            var target = new Bitboards();
            target.Initialize();

            var boardE5 = Bitboards.PrintBitboard(target.MovesQueen[(int)Square.E5]);
            var boardD1 = Bitboards.PrintBitboard(target.MovesQueen[(int)Square.D1]);
            var boardH1 = Bitboards.PrintBitboard(target.MovesQueen[(int)Square.H1]);

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
            }), target.MovesQueen[(int)Square.D1]);

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
           }), target.MovesQueen[(int)Square.H1]);

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
            }), target.MovesQueen[(int)Square.E5]);
        }

        [TestMethod]
        public void WhitePawnCaptuesTest()
        {
            var target = new Bitboards();
            target.Initialize();

            var boardE5 = Bitboards.PrintBitboard(target.WhitePawnCaptures[(int)Square.E5]);
            var boardA2 = Bitboards.PrintBitboard(target.WhitePawnCaptures[(int)Square.A2]);
            var boardH2 = Bitboards.PrintBitboard(target.WhitePawnCaptures[(int)Square.H2]);

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
            }), target.WhitePawnCaptures[(int)Square.A2]);
            Assert.AreEqual((int)Square.B3, target.WhitePawnRight[(int)Square.A2]);

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
            }), target.WhitePawnCaptures[(int)Square.H2]);
            Assert.AreEqual((int)Square.G3, target.WhitePawnLeft[(int)Square.H2]);

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
            }), target.WhitePawnCaptures[(int)Square.E5]);
            Assert.AreEqual((int)Square.F6, target.WhitePawnRight[(int)Square.E5]);
            Assert.AreEqual((int)Square.D6, target.WhitePawnLeft[(int)Square.E5]);
        }

        [TestMethod]
        public void BlackPawnCaptuesTest()
        {
            var target = new Bitboards();
            target.Initialize();

            var boardE5 = Bitboards.PrintBitboard(target.BlackPawnCaptures[(int)Square.E5]);
            var boardA7 = Bitboards.PrintBitboard(target.BlackPawnCaptures[(int)Square.A7]);
            var boardH7 = Bitboards.PrintBitboard(target.BlackPawnCaptures[(int)Square.H7]);

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
            }), target.BlackPawnCaptures[(int)Square.A7]);
            Assert.AreEqual((int)Square.B6, target.BlackPawnRight[(int)Square.A7]);

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
            }), target.BlackPawnCaptures[(int)Square.H7]);
            Assert.AreEqual((int)Square.G6, target.BlackPawnLeft[(int)Square.H7]);

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
            }), target.BlackPawnCaptures[(int)Square.E5]);
            Assert.AreEqual((int)Square.F4, target.BlackPawnRight[(int)Square.E5]);
            Assert.AreEqual((int)Square.D4, target.BlackPawnLeft[(int)Square.E5]);
        }

        [TestMethod]
        public void WhitePawnStraightMovesTest()
        {
            var target = new Bitboards();
            target.Initialize();

            var boardE5 = Bitboards.PrintBitboard(target.WhiteMovesPawn[(int)Square.E5]);
            var boardA2 = Bitboards.PrintBitboard(target.WhiteMovesPawn[(int)Square.A2]);
            var boardH2 = Bitboards.PrintBitboard(target.WhiteMovesPawn[(int)Square.H2]);

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
            }), target.WhiteMovesPawn[(int)Square.A2]);

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
            }), target.WhiteMovesPawn[(int)Square.H2]);

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
            }), target.WhiteMovesPawn[(int)Square.E5]);
        }

        [TestMethod]
        public void BlackPawnStraightMovesTest()
        {
            var target = new Bitboards();
            target.Initialize();

            var boardE5 = Bitboards.PrintBitboard(target.BlackMovesPawn[(int)Square.E5]);
            var boardA7 = Bitboards.PrintBitboard(target.BlackMovesPawn[(int)Square.A7]);
            var boardH7 = Bitboards.PrintBitboard(target.BlackMovesPawn[(int)Square.H7]);

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
            }), target.BlackMovesPawn[(int)Square.A7]);

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
            }), target.BlackMovesPawn[(int)Square.H7]);

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
            }), target.BlackMovesPawn[(int)Square.E5]);
        }

        [TestMethod]
        public void MaskWhitePassedPawnTest()
        {
            var target = new Bitboards();
            target.Initialize();

            var boardE5 = Bitboards.PrintBitboard(target.MaskWhitePassedPawns[(int)Square.E5]);
            var boardA2 = Bitboards.PrintBitboard(target.MaskWhitePassedPawns[(int)Square.A2]);
            var boardH2 = Bitboards.PrintBitboard(target.MaskWhitePassedPawns[(int)Square.H2]);

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
            }), target.MaskWhitePassedPawns[(int)Square.A2]);

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
            }), target.MaskWhitePassedPawns[(int)Square.H2]);

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
            }), target.MaskWhitePassedPawns[(int)Square.E5]);
        }

        [TestMethod]
        public void MaskBlackPassedPawnTest()
        {
            var target = new Bitboards();
            target.Initialize();

            var boardE5 = Bitboards.PrintBitboard(target.MaskBlackPassedPawns[(int)Square.E5]);
            var boardA7 = Bitboards.PrintBitboard(target.MaskBlackPassedPawns[(int)Square.A7]);
            var boardH7 = Bitboards.PrintBitboard(target.MaskBlackPassedPawns[(int)Square.H7]);

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
            }), target.MaskBlackPassedPawns[(int)Square.A7]);

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
            }), target.MaskBlackPassedPawns[(int)Square.H7]);

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
            }), target.MaskBlackPassedPawns[(int)Square.E5]);
        }

        [TestMethod]
        public void MaskIsolatedPawnTest()
        {
            var target = new Bitboards();
            target.Initialize();

            var boardE5 = Bitboards.PrintBitboard(target.MaskIsolatedPawns[(int)Square.E5]);
            var boardA2 = Bitboards.PrintBitboard(target.MaskIsolatedPawns[(int)Square.A2]);
            var boardH2 = Bitboards.PrintBitboard(target.MaskIsolatedPawns[(int)Square.H2]);

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
            }), target.MaskIsolatedPawns[(int)Square.A2]);

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
            }), target.MaskIsolatedPawns[(int)Square.H2]);

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
            }), target.MaskIsolatedPawns[(int)Square.E5]);
        }

        [TestMethod]
        public void MaskWhitePawnPathTest()
        {
            var target = new Bitboards();
            target.Initialize();

            var boardE5 = Bitboards.PrintBitboard(target.MaskWhitePawnsPath[(int)Square.E5]);
            var boardA2 = Bitboards.PrintBitboard(target.MaskWhitePawnsPath[(int)Square.A2]);
            var boardH2 = Bitboards.PrintBitboard(target.MaskWhitePawnsPath[(int)Square.H2]);

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
            }), target.MaskWhitePawnsPath[(int)Square.A2]);

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
            }), target.MaskWhitePawnsPath[(int)Square.H2]);

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
            }), target.MaskWhitePawnsPath[(int)Square.E5]);
        }

        [TestMethod]
        public void MaskBlackPawnPathTest()
        {
            var target = new Bitboards();
            target.Initialize();

            var boardE5 = Bitboards.PrintBitboard(target.MaskBlackPawnsPath[(int)Square.E5]);
            var boardA7 = Bitboards.PrintBitboard(target.MaskBlackPawnsPath[(int)Square.A7]);
            var boardH7 = Bitboards.PrintBitboard(target.MaskBlackPawnsPath[(int)Square.H7]);

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
            }), target.MaskBlackPawnsPath[(int)Square.A7]);

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
            }), target.MaskBlackPawnsPath[(int)Square.H7]);

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
            }), target.MaskBlackPawnsPath[(int)Square.E5]);
        }

        [TestMethod]
        public void BitScanForwardTest_FindingTheLSB()
        {
            var target = new Bitboards();
            var result = target.BitScanForward(0x000001);
            Assert.AreEqual(0, result);

            result = target.BitScanForward(0x00000100);
            Assert.AreEqual(8, result);

            result = target.BitScanForward(0x20000000000);
            Assert.AreEqual(41, result);

            result = target.BitScanForward(0x800000000000000);
            Assert.AreEqual(59, result);

            result = target.BitScanForward(0x0018); // binary: 11000
            Assert.AreEqual(3, result); // 3 zeros at the end
        }

        [TestMethod]
        public void RanksTest()
        {
            var target = new Bitboards();
            target.Initialize();

            // white
            Assert.AreEqual(0, target.Rank[0, 1]);
            Assert.AreEqual(4, target.Rank[0, 35]);
            Assert.AreEqual(7, target.Rank[0, 62]);

            // black
            Assert.AreEqual(7, target.Rank[1, 1]);
            Assert.AreEqual(3, target.Rank[1, 35]);
            Assert.AreEqual(0, target.Rank[1, 62]);
        }

        [TestMethod]
        public void MaskColsTest()
        {
            var target = new Bitboards();
            target.Initialize();

            var boardH7 = Bitboards.PrintBitboard(target.MaskCols[(int)Square.H7]);
            var boardD4 = Bitboards.PrintBitboard(target.MaskCols[(int)Square.D4]);
            var boardNotA = Bitboards.PrintBitboard(target.Not_A_file);
            var boardNotH = Bitboards.PrintBitboard(target.Not_H_file);

            Assert.AreEqual(Bitboards.ConvertToUInt64(new byte[64]
            {//(A1)     (D1)
                0, 0, 0, 0, 0, 0, 0, 1,
                0, 0, 0, 0, 0, 0, 0, 1,
                0, 0, 0, 0, 0, 0, 0, 1,
                0, 0, 0, 0, 0, 0, 0, 1,
                0, 0, 0, 0, 0, 0, 0, 1,
                0, 0, 0, 0, 0, 0, 0, 1,
                0, 0, 0, 0, 0, 0, 0, 1,
                0, 0, 0, 0, 0, 0, 0, 1,
                //                  (H8)
            }), target.MaskCols[(int)Square.H7]);

            Assert.AreEqual(Bitboards.ConvertToUInt64(new byte[64]
            {//(A1)     (D1)
                0, 0, 0, 1, 0, 0, 0, 0,
                0, 0, 0, 1, 0, 0, 0, 0,
                0, 0, 0, 1, 0, 0, 0, 0,
                0, 0, 0, 1, 0, 0, 0, 0,
                0, 0, 0, 1, 0, 0, 0, 0,
                0, 0, 0, 1, 0, 0, 0, 0,
                0, 0, 0, 1, 0, 0, 0, 0,
                0, 0, 0, 1, 0, 0, 0, 0,
                //                  (H8)
            }), target.MaskCols[(int)Square.D4]);

            Assert.AreEqual(Bitboards.ConvertToUInt64(new byte[64]
            {//(A1)     (D1)
                0, 1, 1, 1, 1, 1, 1, 1,
                0, 1, 1, 1, 1, 1, 1, 1,
                0, 1, 1, 1, 1, 1, 1, 1,
                0, 1, 1, 1, 1, 1, 1, 1,
                0, 1, 1, 1, 1, 1, 1, 1,
                0, 1, 1, 1, 1, 1, 1, 1,
                0, 1, 1, 1, 1, 1, 1, 1,
                0, 1, 1, 1, 1, 1, 1, 1,
                //                  (H8)
            }), target.Not_A_file);

            Assert.AreEqual(Bitboards.ConvertToUInt64(new byte[64]
            {//(A1)     (D1)
                1, 1, 1, 1, 1, 1, 1, 0,
                1, 1, 1, 1, 1, 1, 1, 0,
                1, 1, 1, 1, 1, 1, 1, 0,
                1, 1, 1, 1, 1, 1, 1, 0,
                1, 1, 1, 1, 1, 1, 1, 0,
                1, 1, 1, 1, 1, 1, 1, 0,
                1, 1, 1, 1, 1, 1, 1, 0,
                1, 1, 1, 1, 1, 1, 1, 0,
                //                  (H8)
            }), target.Not_H_file);
        }

        [TestMethod]
        public void SetPieceQueenTest()
        {
            var target = new Bitboards();
            target.Initialize();

            target.SetPiece(ColorType.White, PieceType.Queen, Square.F5);

            Assert.AreEqual(PieceType.Queen, target.BoardAllPieces[(int)Square.F5]);
            Assert.AreEqual(ColorType.White, target.BoardColor[(int)Square.F5]);
            Assert.AreEqual(Math.Pow(2, (int)Square.F5), target.Bitboard_Pieces[(int)ColorType.White, (int)PieceType.Queen]);
            Assert.AreEqual(Math.Pow(2, (int)Square.F5), target.Bitboard_Pieces[(int)ColorType.White, (int)PieceType.Queen]);
        }

        [TestMethod]
        public void SetPieceTwoQueensTest()
        {
            var target = new Bitboards();
            target.Initialize();

            target.SetPiece(ColorType.White, PieceType.Queen, Square.F5);
            target.SetPiece(ColorType.White, PieceType.Queen, Square.A7);

            Assert.AreEqual(PieceType.Queen, target.BoardAllPieces[(int)Square.F5]);
            Assert.AreEqual(PieceType.Queen, target.BoardAllPieces[(int)Square.A7]);
            Assert.AreEqual(ColorType.White, target.BoardColor[(int)Square.F5]);
            Assert.AreEqual(ColorType.White, target.BoardColor[(int)Square.A7]);
            Assert.AreEqual(Math.Pow(2, (int)Square.F5) + Math.Pow(2, (int)Square.A7), target.Bitboard_Pieces[(int)ColorType.White, (int)PieceType.Queen]);
        }

        [TestMethod]
        public void GetPieceQueenTest()
        {
            var target = new Bitboards();
            target.Initialize();
            target.SetPiece(ColorType.White, PieceType.Queen, Square.F5);

            var result = target.GetPiece((int)Square.F5);

            Assert.AreEqual(ColorType.White, result.Color);
            Assert.AreEqual(PieceType.Queen, result.Piece);
        }

        [TestMethod]
        public void GetPieceTwoQueenTest()
        {
            var target = new Bitboards();
            target.Initialize();
            target.SetPiece(ColorType.White, PieceType.Queen, Square.F5);
            target.SetPiece(ColorType.White, PieceType.Queen, Square.B3);

            var result = target.GetPiece((int)Square.F5);
            var result2 = target.GetPiece((int)Square.B3);

            Assert.AreEqual(ColorType.White, result.Color);
            Assert.AreEqual(PieceType.Queen, result.Piece);
            Assert.AreEqual(ColorType.White, result2.Color);
            Assert.AreEqual(PieceType.Queen, result2.Piece);
        }
    }
}
