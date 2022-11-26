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

            Assert.AreEqual(Bitboards.ConvertToUInt64(new byte[64]
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
            , target.Bitboard_Pieces[(int)BitColor.White, (int)BitPieceType.Pawn]);

            Assert.AreEqual(Bitboards.ConvertToUInt64(new byte[64]
            {
                1, 0, 0, 0, 0, 0, 0, 1,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0
            }), target.Bitboard_Pieces[(int)BitColor.White, (int)BitPieceType.Rook]);

            Assert.AreEqual(Bitboards.ConvertToUInt64(new byte[64]
            {
                0, 1, 0, 0, 0, 0, 1, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
            }), target.Bitboard_Pieces[(int)BitColor.White, (int)BitPieceType.Knight]);

            Assert.AreEqual(Bitboards.ConvertToUInt64(new byte[64]
            {
                0, 0, 1, 0, 0, 1, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0
            }), target.Bitboard_Pieces[(int)BitColor.White, (int)BitPieceType.Bishop]);

            Assert.AreEqual(Bitboards.ConvertToUInt64(new byte[64]
            {
                0, 0, 0, 1, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0
            }), target.Bitboard_Pieces[(int)BitColor.White, (int)BitPieceType.Queen]);

            Assert.AreEqual(Bitboards.ConvertToUInt64(new byte[64]
            {
                0, 0, 0, 0, 1, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0
            }), target.Bitboard_Pieces[(int)BitColor.White, (int)BitPieceType.King]);

            Assert.AreEqual(Bitboards.ConvertToUInt64(new byte[64]
            {
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                1, 1, 1, 1, 1, 1, 1, 1,
                0, 0, 0, 0, 0, 0, 0, 0
            }), target.Bitboard_Pieces[(int)BitColor.Black, (int)BitPieceType.Pawn]);

            Assert.AreEqual(Bitboards.ConvertToUInt64(new byte[64]
            {
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                1, 0, 0, 0, 0, 0, 0, 1
            }), target.Bitboard_Pieces[(int)BitColor.Black, (int)BitPieceType.Rook]);

            Assert.AreEqual(Bitboards.ConvertToUInt64(new byte[64]
            {
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 1, 0, 0, 0, 0, 1, 0
            }), target.Bitboard_Pieces[(int)BitColor.Black, (int)BitPieceType.Knight]);

            Assert.AreEqual(Bitboards.ConvertToUInt64(new byte[64]
            {
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 1, 0, 0, 1, 0, 0
            }), target.Bitboard_Pieces[(int)BitColor.Black, (int)BitPieceType.Bishop]);

            Assert.AreEqual(Bitboards.ConvertToUInt64(new byte[64]
            {
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 1, 0, 0, 0, 0
            }), target.Bitboard_Pieces[(int)BitColor.Black, (int)BitPieceType.Queen]);

            Assert.AreEqual(Bitboards.ConvertToUInt64(new byte[64]
            {
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 1, 0, 0, 0
            }), target.Bitboard_Pieces[(int)BitColor.Black, (int)BitPieceType.King]);
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

            Assert.AreEqual((int)Square.A2, target.GetEdge((int)Square.B2, -1));
            Assert.AreEqual((int)Square.A7, target.GetEdge((int)Square.B7, -1));
            Assert.AreEqual((int)Square.B1, target.GetEdge((int)Square.B2, -8));
            Assert.AreEqual((int)Square.B8, target.GetEdge((int)Square.B7, 8));
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

            var boardE5 = Bitboards.PrintBitboard(target.MovesPieces[(int)BitPieceType.Knight, (int)Square.E5]);
            var boardD1 = Bitboards.PrintBitboard(target.MovesPieces[(int)BitPieceType.Knight, (int)Square.D1]);
            var boardD2 = Bitboards.PrintBitboard(target.MovesPieces[(int)BitPieceType.Knight, (int)Square.D2]);
            var boardB1 = Bitboards.PrintBitboard(target.MovesPieces[(int)BitPieceType.Knight, (int)Square.B1]);
            var boardG1 = Bitboards.PrintBitboard(target.MovesPieces[(int)BitPieceType.Knight, (int)Square.G1]);

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
            }), target.MovesPieces[(int)BitPieceType.Knight, (int)Square.D1]);

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
            }), target.MovesPieces[(int)BitPieceType.Knight, (int)Square.D2]);

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
            }), target.MovesPieces[(int)BitPieceType.Knight, (int)Square.E5]);

            Assert.AreEqual(Bitboards.ConvertToUInt64(new byte[64]
            {//(A1)         
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 1, 0, 0, 0, 0,
                1, 0, 1, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0
                //                  (H8)
            }), target.MovesPieces[(int)BitPieceType.Knight, (int)Square.B1]);

            Assert.AreEqual(Bitboards.ConvertToUInt64(new byte[64]
            {//(A1)         
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 1, 0, 0, 0,
                0, 0, 0, 0, 0, 1, 0, 1,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0
                //                  (H8)
            }), target.MovesPieces[(int)BitPieceType.Knight, (int)Square.G1]);
        }

        [TestMethod]
        public void MovesKingTest()
        {
            var target = new Bitboards();
            target.Initialize();

            var boardE5 = Bitboards.PrintBitboard(target.MovesPieces[(int)BitPieceType.King, (int)Square.E5]);
            var boardD1 = Bitboards.PrintBitboard(target.MovesPieces[(int)BitPieceType.King, (int)Square.D1]);
            var boardH1 = Bitboards.PrintBitboard(target.MovesPieces[(int)BitPieceType.King, (int)Square.H1]);

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
            }), target.MovesPieces[(int)BitPieceType.King, (int)Square.D1]);

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
           }), target.MovesPieces[(int)BitPieceType.King, (int)Square.H1]);

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
            }), target.MovesPieces[(int)BitPieceType.King, (int)Square.E5]);
        }

        [TestMethod]
        public void MovesRookTest()
        {
            var target = new Bitboards();
            target.Initialize();

            var boardE5 = Bitboards.PrintBitboard(target.MovesPieces[(int)BitPieceType.Rook, (int)Square.E5]);
            var boardD1 = Bitboards.PrintBitboard(target.MovesPieces[(int)BitPieceType.Rook, (int)Square.D1]);
            var boardH1 = Bitboards.PrintBitboard(target.MovesPieces[(int)BitPieceType.Rook, (int)Square.H1]);
            var boardB7 = Bitboards.PrintBitboard(target.MovesPieces[(int)BitPieceType.Rook, (int)Square.B7]);
            var boardB2 = Bitboards.PrintBitboard(target.MovesPieces[(int)BitPieceType.Rook, (int)Square.B2]);

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
            }), target.MovesPieces[(int)BitPieceType.Rook, (int)Square.D1]);

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
           }), target.MovesPieces[(int)BitPieceType.Rook, (int)Square.H1]);

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
            }), target.MovesPieces[(int)BitPieceType.Rook, (int)Square.E5]);

            Assert.AreEqual(Bitboards.ConvertToUInt64(new byte[64]
            {//(A1)         
                0, 1, 0, 0, 0, 0, 0, 0,
                1, 0, 1, 1, 1, 1, 1, 1,
                0, 1, 0, 0, 0, 0, 0, 0,
                0, 1, 0, 0, 0, 0, 0, 0,
                0, 1, 0, 0, 0, 0, 0, 0,
                0, 1, 0, 0, 0, 0, 0, 0,
                0, 1, 0, 0, 0, 0, 0, 0,
                0, 1, 0, 0, 0, 0, 0, 0
                //                  (H8)
            }), target.MovesPieces[(int)BitPieceType.Rook, (int)Square.B7]);

            Assert.AreEqual(Bitboards.ConvertToUInt64(new byte[64]
            {//(A1)         
                0, 1, 0, 0, 0, 0, 0, 0,
                0, 1, 0, 0, 0, 0, 0, 0,
                0, 1, 0, 0, 0, 0, 0, 0,
                0, 1, 0, 0, 0, 0, 0, 0,
                0, 1, 0, 0, 0, 0, 0, 0,
                0, 1, 0, 0, 0, 0, 0, 0,
                1, 0, 1, 1, 1, 1, 1, 1,
                0, 1, 0, 0, 0, 0, 0, 0
                //                  (H8)
            }), target.MovesPieces[(int)BitPieceType.Rook, (int)Square.B2]);
        }

        [TestMethod]
        public void MovesBishopTest()
        {
            var target = new Bitboards();
            target.Initialize();

            var boardE5 = Bitboards.PrintBitboard(target.MovesPieces[(int)BitPieceType.Bishop, (int)Square.E5]);
            var boardD1 = Bitboards.PrintBitboard(target.MovesPieces[(int)BitPieceType.Bishop, (int)Square.D1]);
            var boardH1 = Bitboards.PrintBitboard(target.MovesPieces[(int)BitPieceType.Bishop, (int)Square.H1]);

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
            }), target.MovesPieces[(int)BitPieceType.Bishop, (int)Square.D1]);

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
           }), target.MovesPieces[(int)BitPieceType.Bishop, (int)Square.H1]);

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
            }), target.MovesPieces[(int)BitPieceType.Bishop, (int)Square.E5]);
        }

        [TestMethod]
        public void MovesQueenTest()
        {
            var target = new Bitboards();
            target.Initialize();

            var boardE5 = Bitboards.PrintBitboard(target.MovesPieces[(int)BitPieceType.Queen, (int)Square.E5]);
            var boardD1 = Bitboards.PrintBitboard(target.MovesPieces[(int)BitPieceType.Queen, (int)Square.D1]);
            var boardH1 = Bitboards.PrintBitboard(target.MovesPieces[(int)BitPieceType.Queen, (int)Square.H1]);
            var boardB2 = Bitboards.PrintBitboard(target.MovesPieces[(int)BitPieceType.Queen, (int)Square.B2]);
            var boardB7 = Bitboards.PrintBitboard(target.MovesPieces[(int)BitPieceType.Queen, (int)Square.B7]);

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
            }), target.MovesPieces[(int)BitPieceType.Queen, (int)Square.D1]);

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
           }), target.MovesPieces[(int)BitPieceType.Queen, (int)Square.H1]);

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
            }), target.MovesPieces[(int)BitPieceType.Queen, (int)Square.E5]);

            Assert.AreEqual(Bitboards.ConvertToUInt64(new byte[64]
            {//(A1)         
                0, 1, 0, 0, 0, 0, 0, 1,
                0, 1, 0, 0, 0, 0, 1, 0,
                0, 1, 0, 0, 0, 1, 0, 0,
                0, 1, 0, 0, 1, 0, 0, 0,
                0, 1, 0, 1, 0, 0, 0, 0,
                1, 1, 1, 0, 0, 0, 0, 0,
                1, 1, 1, 1, 1, 1, 1, 1,
                1, 1, 1, 0, 0, 0, 0, 0
                //                  (H8)
            }), target.MovesPieces[(int)BitPieceType.Queen, (int)Square.B7]);

            Assert.AreEqual(Bitboards.ConvertToUInt64(new byte[64]
            {//(A1)         
                1, 1, 1, 0, 0, 0, 0, 0,
                1, 1, 1, 1, 1, 1, 1, 1,
                1, 1, 1, 0, 0, 0, 0, 0,
                0, 1, 0, 1, 0, 0, 0, 0,
                0, 1, 0, 0, 1, 0, 0, 0,
                0, 1, 0, 0, 0, 1, 0, 0,
                0, 1, 0, 0, 0, 0, 1, 0,
                0, 1, 0, 0, 0, 0, 0, 1
                //                  (H8)
            }), target.MovesPieces[(int)BitPieceType.Queen, (int)Square.B2]);
        }

        [TestMethod]
        public void WhitePawnCaptuesTest()
        {
            var target = new Bitboards();
            target.Initialize();

            var boardE5 = Bitboards.PrintBitboard(target.PawnCaptures[(int)BitColor.White, (int)Square.E5]);
            var boardA2 = Bitboards.PrintBitboard(target.PawnCaptures[(int)BitColor.White, (int)Square.A2]);
            var boardH2 = Bitboards.PrintBitboard(target.PawnCaptures[(int)BitColor.White, (int)Square.H2]);

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
            }), target.PawnCaptures[(int)BitColor.White, (int)Square.A2]);
            Assert.AreEqual((int)Square.B3, target.PawnRight[(int)BitColor.White, (int)Square.A2]);

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
            }), target.PawnCaptures[(int)BitColor.White, (int)Square.H2]);
            Assert.AreEqual((int)Square.G3, target.PawnLeft[(int)BitColor.White, (int)Square.H2]);

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
            }), target.PawnCaptures[(int)BitColor.White, (int)Square.E5]);
            Assert.AreEqual((int)Square.F6, target.PawnRight[(int)BitColor.White, (int)Square.E5]);
            Assert.AreEqual((int)Square.D6, target.PawnLeft[(int)BitColor.White, (int)Square.E5]);
        }

        [TestMethod]
        public void BlackPawnCaptuesTest()
        {
            var target = new Bitboards();
            target.Initialize();

            var boardE5 = Bitboards.PrintBitboard(target.PawnCaptures[(int)BitColor.Black, (int)Square.E5]);
            var boardA7 = Bitboards.PrintBitboard(target.PawnCaptures[(int)BitColor.Black, (int)Square.A7]);
            var boardH7 = Bitboards.PrintBitboard(target.PawnCaptures[(int)BitColor.Black, (int)Square.H7]);

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
            }), target.PawnCaptures[(int)BitColor.Black, (int)Square.A7]);
            Assert.AreEqual((int)Square.B6, target.PawnRight[(int)BitColor.Black, (int)Square.A7]);

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
            }), target.PawnCaptures[(int)BitColor.Black, (int)Square.H7]);
            Assert.AreEqual((int)Square.G6, target.PawnLeft[(int)BitColor.Black, (int)Square.H7]);

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
            }), target.PawnCaptures[(int)BitColor.Black, (int)Square.E5]);
            Assert.AreEqual((int)Square.F4, target.PawnRight[(int)BitColor.Black, (int)Square.E5]);
            Assert.AreEqual((int)Square.D4, target.PawnLeft[(int)BitColor.Black, (int)Square.E5]);
        }

        [TestMethod]
        public void WhitePawnStraightMovesTest()
        {
            var target = new Bitboards();
            target.Initialize();

            var boardE5 = Bitboards.PrintBitboard(target.PawnMoves[(int)BitColor.White, (int)Square.E5]);
            var boardA2 = Bitboards.PrintBitboard(target.PawnMoves[(int)BitColor.White, (int)Square.A2]);
            var boardH2 = Bitboards.PrintBitboard(target.PawnMoves[(int)BitColor.White, (int)Square.H2]);

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
            }), target.PawnMoves[(int)BitColor.White, (int)Square.A2]);
            Assert.AreEqual((int)Square.A3, target.PawnStep[(int)BitColor.White, (int)Square.A2]);
            Assert.AreEqual((int)Square.A4, target.PawnDoubleStep[(int)BitColor.White, (int)Square.A2]);

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
            }), target.PawnMoves[(int)BitColor.White, (int)Square.H2]);
            Assert.AreEqual((int)Square.H3, target.PawnStep[(int)BitColor.White, (int)Square.H2]);
            Assert.AreEqual((int)Square.H4, target.PawnDoubleStep[(int)BitColor.White, (int)Square.H2]);

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
            }), target.PawnMoves[(int)BitColor.White, (int)Square.E5]);
            Assert.AreEqual((int)Square.E6, target.PawnStep[(int)BitColor.White, (int)Square.E5]);
        }

        [TestMethod]
        public void BlackPawnStraightMovesTest()
        {
            var target = new Bitboards();
            target.Initialize();

            var boardE5 = Bitboards.PrintBitboard(target.PawnMoves[(int)BitColor.Black, (int)Square.E5]);
            var boardA7 = Bitboards.PrintBitboard(target.PawnMoves[(int)BitColor.Black, (int)Square.A7]);
            var boardH7 = Bitboards.PrintBitboard(target.PawnMoves[(int)BitColor.Black, (int)Square.H7]);

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
            }), target.PawnMoves[(int)BitColor.Black, (int)Square.A7]);
            Assert.AreEqual((int)Square.A6, target.PawnStep[(int)BitColor.Black, (int)Square.A7]);
            Assert.AreEqual((int)Square.A5, target.PawnDoubleStep[(int)BitColor.Black, (int)Square.A7]);

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
            }), target.PawnMoves[(int)BitColor.Black, (int)Square.H7]);
            Assert.AreEqual((int)Square.H6, target.PawnStep[(int)BitColor.Black, (int)Square.H7]);
            Assert.AreEqual((int)Square.H5, target.PawnDoubleStep[(int)BitColor.Black, (int)Square.H7]);

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
            }), target.PawnMoves[(int)BitColor.Black, (int)Square.E5]);
            Assert.AreEqual((int)Square.E4, target.PawnStep[(int)BitColor.Black, (int)Square.E5]);
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

            target.SetPiece(BitColor.White, BitPieceType.Queen, Square.F5);

            Assert.AreEqual(BitPieceType.Queen, target.BoardAllPieces[(int)Square.F5]);
            Assert.AreEqual(BitColor.White, target.BoardColor[(int)Square.F5]);
            Assert.AreEqual(Math.Pow(2, (int)Square.F5), target.Bitboard_Pieces[(int)BitColor.White, (int)BitPieceType.Queen]);
            Assert.AreEqual(Math.Pow(2, (int)Square.F5), target.Bitboard_Pieces[(int)BitColor.White, (int)BitPieceType.Queen]);
        }

        [TestMethod]
        public void SetPieceTwoQueensTest()
        {
            var target = new Bitboards();
            target.Initialize();

            target.SetPiece(BitColor.White, BitPieceType.Queen, Square.F5);
            target.SetPiece(BitColor.White, BitPieceType.Queen, Square.A7);

            Assert.AreEqual(BitPieceType.Queen, target.BoardAllPieces[(int)Square.F5]);
            Assert.AreEqual(BitPieceType.Queen, target.BoardAllPieces[(int)Square.A7]);
            Assert.AreEqual(BitColor.White, target.BoardColor[(int)Square.F5]);
            Assert.AreEqual(BitColor.White, target.BoardColor[(int)Square.A7]);
            Assert.AreEqual(Math.Pow(2, (int)Square.F5) + Math.Pow(2, (int)Square.A7), target.Bitboard_Pieces[(int)BitColor.White, (int)BitPieceType.Queen]);
        }

        [TestMethod]
        public void GetPieceQueenTest()
        {
            var target = new Bitboards();
            target.Initialize();
            target.SetPiece(BitColor.White, BitPieceType.Queen, Square.F5);

            var result = target.GetPiece(Square.F5);

            Assert.AreEqual(BitColor.White, result.Color);
            Assert.AreEqual(BitPieceType.Queen, result.Piece);
        }

        [TestMethod]
        public void GetPieceTwoQueenTest()
        {
            var target = new Bitboards();
            target.Initialize();
            target.SetPiece(BitColor.White, BitPieceType.Queen, Square.F5);
            target.SetPiece(BitColor.White, BitPieceType.Queen, Square.B3);

            var result = target.GetPiece(Square.F5);
            var result2 = target.GetPiece(Square.B3);

            Assert.AreEqual(BitColor.White, result.Color);
            Assert.AreEqual(BitPieceType.Queen, result.Piece);
            Assert.AreEqual(BitColor.White, result2.Color);
            Assert.AreEqual(BitPieceType.Queen, result2.Piece);
        }
    }
}
