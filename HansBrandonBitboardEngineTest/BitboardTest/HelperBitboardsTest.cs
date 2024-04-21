using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HansBrandonBitboardEngine;
using HBCommon;

namespace HansBrandonBitboardEngineTest
{
    [TestClass]
    public class HelperBitboardsTest
    {
        [TestMethod]
        public void BetweenMatrixTest()
        {
            var target = new HelperBitboards();

            Assert.AreEqual((UInt64)0x00, target.BetweenMatrix[0, 1]);
            Assert.AreEqual((UInt64)0x00, target.BetweenMatrix[27, 28]);
            Assert.AreEqual((UInt64)0x00, target.BetweenMatrix[14, 1]);
            Assert.AreEqual((UInt64)0x00, target.BetweenMatrix[0, 8]);

            Assert.AreEqual(BitHelper.ConvertToUInt64(new byte[64]
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

            Assert.AreEqual(BitHelper.ConvertToUInt64(new byte[64]
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

            Assert.AreEqual(BitHelper.ConvertToUInt64(new byte[64]
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

            Assert.AreEqual(BitHelper.ConvertToUInt64(new byte[64]
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

            Assert.AreEqual(BitHelper.ConvertToUInt64(new byte[64]
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

            Assert.AreEqual(BitHelper.ConvertToUInt64(new byte[64]
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

            Assert.AreEqual(BitHelper.ConvertToUInt64(new byte[64]
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

            Assert.AreEqual(BitHelper.ConvertToUInt64(new byte[64]
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
            var target = new HelperBitboards();

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
            var target = new HelperBitboards();

            Assert.AreEqual((UInt64)0x00, target.RayAfterMatrix[14, 1]);

            Assert.AreEqual(BitHelper.ConvertToUInt64(new byte[64]
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

            Assert.AreEqual(BitHelper.ConvertToUInt64(new byte[64]
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
            Assert.AreEqual(BitHelper.ConvertToUInt64(new byte[64]
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
            Assert.AreEqual(BitHelper.ConvertToUInt64(new byte[64]
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

            Assert.AreEqual(BitHelper.ConvertToUInt64(new byte[64]
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

            Assert.AreEqual(BitHelper.ConvertToUInt64(new byte[64]
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

            Assert.AreEqual(BitHelper.ConvertToUInt64(new byte[64]
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

            Assert.AreEqual(BitHelper.ConvertToUInt64(new byte[64]
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

            Assert.AreEqual(BitHelper.ConvertToUInt64(new byte[64]
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

            Assert.AreEqual(BitHelper.ConvertToUInt64(new byte[64]
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
            var target = new HelperBitboards();

            var boardE5 = Bitboards.PrintBitboard(target.MovesPieces[(int)PieceType.Knight, (int)Square.E5]);
            var boardD1 = Bitboards.PrintBitboard(target.MovesPieces[(int)PieceType.Knight, (int)Square.D1]);
            var boardD2 = Bitboards.PrintBitboard(target.MovesPieces[(int)PieceType.Knight, (int)Square.D2]);
            var boardB1 = Bitboards.PrintBitboard(target.MovesPieces[(int)PieceType.Knight, (int)Square.B1]);
            var boardG1 = Bitboards.PrintBitboard(target.MovesPieces[(int)PieceType.Knight, (int)Square.G1]);

            Assert.AreEqual(BitHelper.ConvertToUInt64(new byte[64]
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
            }), target.MovesPieces[(int)PieceType.Knight, (int)Square.D1]);

            Assert.AreEqual(BitHelper.ConvertToUInt64(new byte[64]
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
            }), target.MovesPieces[(int)PieceType.Knight, (int)Square.D2]);

            Assert.AreEqual(BitHelper.ConvertToUInt64(new byte[64]
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
            }), target.MovesPieces[(int)PieceType.Knight, (int)Square.E5]);

            Assert.AreEqual(BitHelper.ConvertToUInt64(new byte[64]
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
            }), target.MovesPieces[(int)PieceType.Knight, (int)Square.B1]);

            Assert.AreEqual(BitHelper.ConvertToUInt64(new byte[64]
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
            }), target.MovesPieces[(int)PieceType.Knight, (int)Square.G1]);
        }

        [TestMethod]
        public void MovesKingTest()
        {
            var target = new HelperBitboards();

            var boardE5 = Bitboards.PrintBitboard(target.MovesPieces[(int)PieceType.King, (int)Square.E5]);
            var boardD1 = Bitboards.PrintBitboard(target.MovesPieces[(int)PieceType.King, (int)Square.D1]);
            var boardH1 = Bitboards.PrintBitboard(target.MovesPieces[(int)PieceType.King, (int)Square.H1]);
            var boardB1 = Bitboards.PrintBitboard(target.MovesPieces[(int)PieceType.King, (int)Square.B1]);

            Assert.AreEqual(BitHelper.ConvertToUInt64(new byte[64]
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
            }), target.MovesPieces[(int)PieceType.King, (int)Square.D1]);

            Assert.AreEqual(BitHelper.ConvertToUInt64(new byte[64]
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
           }), target.MovesPieces[(int)PieceType.King, (int)Square.H1]);

            Assert.AreEqual(BitHelper.ConvertToUInt64(new byte[64]
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
            }), target.MovesPieces[(int)PieceType.King, (int)Square.E5]);

            Assert.AreEqual(BitHelper.ConvertToUInt64(new byte[64]
            {//(A1)         
                1, 0, 1, 0, 0, 0, 0, 0,
                1, 1, 1, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0
                //                  (H8)
            }), target.MovesPieces[(int)PieceType.King, (int)Square.B1]);
        }

        [TestMethod]
        public void MovesRookTest()
        {
            var target = new HelperBitboards();

            var boardE5 = Bitboards.PrintBitboard(target.MovesPieces[(int)PieceType.Rook, (int)Square.E5]);
            var boardD1 = Bitboards.PrintBitboard(target.MovesPieces[(int)PieceType.Rook, (int)Square.D1]);
            var boardH1 = Bitboards.PrintBitboard(target.MovesPieces[(int)PieceType.Rook, (int)Square.H1]);
            var boardB7 = Bitboards.PrintBitboard(target.MovesPieces[(int)PieceType.Rook, (int)Square.B7]);
            var boardB2 = Bitboards.PrintBitboard(target.MovesPieces[(int)PieceType.Rook, (int)Square.B2]);

            Assert.AreEqual(BitHelper.ConvertToUInt64(new byte[64]
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
            }), target.MovesPieces[(int)PieceType.Rook, (int)Square.D1]);

            Assert.AreEqual(BitHelper.ConvertToUInt64(new byte[64]
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
           }), target.MovesPieces[(int)PieceType.Rook, (int)Square.H1]);

            Assert.AreEqual(BitHelper.ConvertToUInt64(new byte[64]
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
            }), target.MovesPieces[(int)PieceType.Rook, (int)Square.E5]);

            Assert.AreEqual(BitHelper.ConvertToUInt64(new byte[64]
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
            }), target.MovesPieces[(int)PieceType.Rook, (int)Square.B2]);

            Assert.AreEqual(BitHelper.ConvertToUInt64(new byte[64]
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
            }), target.MovesPieces[(int)PieceType.Rook, (int)Square.B7]);
        }

        [TestMethod]
        public void MovesBishopTest()
        {
            var target = new HelperBitboards();

            var boardE5 = Bitboards.PrintBitboard(target.MovesPieces[(int)PieceType.Bishop, (int)Square.E5]);
            var boardD1 = Bitboards.PrintBitboard(target.MovesPieces[(int)PieceType.Bishop, (int)Square.D1]);
            var boardH1 = Bitboards.PrintBitboard(target.MovesPieces[(int)PieceType.Bishop, (int)Square.H1]);

            Assert.AreEqual(BitHelper.ConvertToUInt64(new byte[64]
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
            }), target.MovesPieces[(int)PieceType.Bishop, (int)Square.D1]);

            Assert.AreEqual(BitHelper.ConvertToUInt64(new byte[64]
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
           }), target.MovesPieces[(int)PieceType.Bishop, (int)Square.H1]);

            Assert.AreEqual(BitHelper.ConvertToUInt64(new byte[64]
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
            }), target.MovesPieces[(int)PieceType.Bishop, (int)Square.E5]);
        }

        [TestMethod]
        public void MovesQueenTest()
        {
            var target = new HelperBitboards();

            var boardE5 = Bitboards.PrintBitboard(target.MovesPieces[(int)PieceType.Queen, (int)Square.E5]);
            var boardD1 = Bitboards.PrintBitboard(target.MovesPieces[(int)PieceType.Queen, (int)Square.D1]);
            var boardH1 = Bitboards.PrintBitboard(target.MovesPieces[(int)PieceType.Queen, (int)Square.H1]);
            var boardB2 = Bitboards.PrintBitboard(target.MovesPieces[(int)PieceType.Queen, (int)Square.B2]);
            var boardB7 = Bitboards.PrintBitboard(target.MovesPieces[(int)PieceType.Queen, (int)Square.B7]);

            Assert.AreEqual(BitHelper.ConvertToUInt64(new byte[64]
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
            }), target.MovesPieces[(int)PieceType.Queen, (int)Square.D1]);

            Assert.AreEqual(BitHelper.ConvertToUInt64(new byte[64]
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
           }), target.MovesPieces[(int)PieceType.Queen, (int)Square.H1]);

            Assert.AreEqual(BitHelper.ConvertToUInt64(new byte[64]
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
            }), target.MovesPieces[(int)PieceType.Queen, (int)Square.E5]);

            Assert.AreEqual(BitHelper.ConvertToUInt64(new byte[64]
            {//(A1)         
                0, 1, 0, 0, 0, 0, 0, 1,
                0, 1, 0, 0, 0, 0, 1, 0,
                0, 1, 0, 0, 0, 1, 0, 0,
                0, 1, 0, 0, 1, 0, 0, 0,
                0, 1, 0, 1, 0, 0, 0, 0,
                1, 1, 1, 0, 0, 0, 0, 0,
                1, 0, 1, 1, 1, 1, 1, 1,
                1, 1, 1, 0, 0, 0, 0, 0
                //                  (H8)
            }), target.MovesPieces[(int)PieceType.Queen, (int)Square.B7]);

            Assert.AreEqual(BitHelper.ConvertToUInt64(new byte[64]
            {//(A1)         
                1, 1, 1, 0, 0, 0, 0, 0,
                1, 0, 1, 1, 1, 1, 1, 1,
                1, 1, 1, 0, 0, 0, 0, 0,
                0, 1, 0, 1, 0, 0, 0, 0,
                0, 1, 0, 0, 1, 0, 0, 0,
                0, 1, 0, 0, 0, 1, 0, 0,
                0, 1, 0, 0, 0, 0, 1, 0,
                0, 1, 0, 0, 0, 0, 0, 1
                //                  (H8)
            }), target.MovesPieces[(int)PieceType.Queen, (int)Square.B2]);
        }

        [TestMethod]
        public void WhitePawnCaptuesTest()
        {
            var target = new HelperBitboards();

            var boardE5 = Bitboards.PrintBitboard(target.PawnCaptures[(int)ChessColor.White, (int)Square.E5]);
            var boardA2 = Bitboards.PrintBitboard(target.PawnCaptures[(int)ChessColor.White, (int)Square.A2]);
            var boardH2 = Bitboards.PrintBitboard(target.PawnCaptures[(int)ChessColor.White, (int)Square.H2]);

            Assert.AreEqual(BitHelper.ConvertToUInt64(new byte[64]
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
            }), target.PawnCaptures[(int)ChessColor.White, (int)Square.A2]);
            Assert.AreEqual(Square.B3, target.PawnRight[(int)ChessColor.White, (int)Square.A2]);

            Assert.AreEqual(BitHelper.ConvertToUInt64(new byte[64]
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
            }), target.PawnCaptures[(int)ChessColor.White, (int)Square.H2]);
            Assert.AreEqual(Square.G3, target.PawnLeft[(int)ChessColor.White, (int)Square.H2]);

            Assert.AreEqual(BitHelper.ConvertToUInt64(new byte[64]
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
            }), target.PawnCaptures[(int)ChessColor.White, (int)Square.E5]);
            Assert.AreEqual(Square.F6, target.PawnRight[(int)ChessColor.White, (int)Square.E5]);
            Assert.AreEqual(Square.D6, target.PawnLeft[(int)ChessColor.White, (int)Square.E5]);

            Assert.AreEqual(Square.NoSquare, target.PawnRight[(int)ChessColor.White, (int)Square.H5]);
            Assert.AreEqual(Square.NoSquare, target.PawnLeft[(int)ChessColor.White, (int)Square.A5]);

        }

        [TestMethod]
        public void BlackPawnCaptuesTest()
        {
            var target = new HelperBitboards();

            var boardE5 = Bitboards.PrintBitboard(target.PawnCaptures[(int)ChessColor.Black, (int)Square.E5]);
            var boardA7 = Bitboards.PrintBitboard(target.PawnCaptures[(int)ChessColor.Black, (int)Square.A7]);
            var boardH7 = Bitboards.PrintBitboard(target.PawnCaptures[(int)ChessColor.Black, (int)Square.H7]);

            Assert.AreEqual(BitHelper.ConvertToUInt64(new byte[64]
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
            }), target.PawnCaptures[(int)ChessColor.Black, (int)Square.A7]);
            Assert.AreEqual(Square.B6, target.PawnRight[(int)ChessColor.Black, (int)Square.A7]);

            Assert.AreEqual(BitHelper.ConvertToUInt64(new byte[64]
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
            }), target.PawnCaptures[(int)ChessColor.Black, (int)Square.H7]);
            Assert.AreEqual(Square.G6, target.PawnLeft[(int)ChessColor.Black, (int)Square.H7]);

            Assert.AreEqual(BitHelper.ConvertToUInt64(new byte[64]
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
            }), target.PawnCaptures[(int)ChessColor.Black, (int)Square.E5]);
            Assert.AreEqual(Square.F4, target.PawnRight[(int)ChessColor.Black, (int)Square.E5]);
            Assert.AreEqual(Square.D4, target.PawnLeft[(int)ChessColor.Black, (int)Square.E5]);
        }

        [TestMethod]
        public void WhitePawnStraightMovesTest()
        {
            var target = new HelperBitboards();

            var boardE5 = Bitboards.PrintBitboard(target.PawnMoves[(int)ChessColor.White, (int)Square.E5]);
            var boardA2 = Bitboards.PrintBitboard(target.PawnMoves[(int)ChessColor.White, (int)Square.A2]);
            var boardH2 = Bitboards.PrintBitboard(target.PawnMoves[(int)ChessColor.White, (int)Square.H2]);

            Assert.AreEqual(BitHelper.ConvertToUInt64(new byte[64]
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
            }), target.PawnMoves[(int)ChessColor.White, (int)Square.A2]);
            Assert.AreEqual(Square.A3, target.PawnStep[(int)ChessColor.White, (int)Square.A2]);
            Assert.AreEqual(Square.A4, target.PawnDoubleStep[(int)ChessColor.White, (int)Square.A2]);

            Assert.AreEqual(BitHelper.ConvertToUInt64(new byte[64]
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
            }), target.PawnMoves[(int)ChessColor.White, (int)Square.H2]);
            Assert.AreEqual(Square.H3, target.PawnStep[(int)ChessColor.White, (int)Square.H2]);
            Assert.AreEqual(Square.H4, target.PawnDoubleStep[(int)ChessColor.White, (int)Square.H2]);

            Assert.AreEqual(BitHelper.ConvertToUInt64(new byte[64]
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
            }), target.PawnMoves[(int)ChessColor.White, (int)Square.E5]);
            Assert.AreEqual(Square.E6, target.PawnStep[(int)ChessColor.White, (int)Square.E5]);
        }

        [TestMethod]
        public void BlackPawnStraightMovesTest()
        {
            var target = new HelperBitboards();

            var boardE5 = Bitboards.PrintBitboard(target.PawnMoves[(int)ChessColor.Black, (int)Square.E5]);
            var boardA7 = Bitboards.PrintBitboard(target.PawnMoves[(int)ChessColor.Black, (int)Square.A7]);
            var boardH7 = Bitboards.PrintBitboard(target.PawnMoves[(int)ChessColor.Black, (int)Square.H7]);

            Assert.AreEqual(BitHelper.ConvertToUInt64(new byte[64]
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
            }), target.PawnMoves[(int)ChessColor.Black, (int)Square.A7]);
            Assert.AreEqual(Square.A6, target.PawnStep[(int)ChessColor.Black, (int)Square.A7]);
            Assert.AreEqual(Square.A5, target.PawnDoubleStep[(int)ChessColor.Black, (int)Square.A7]);

            Assert.AreEqual(BitHelper.ConvertToUInt64(new byte[64]
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
            }), target.PawnMoves[(int)ChessColor.Black, (int)Square.H7]);
            Assert.AreEqual(Square.H6, target.PawnStep[(int)ChessColor.Black, (int)Square.H7]);
            Assert.AreEqual(Square.H5, target.PawnDoubleStep[(int)ChessColor.Black, (int)Square.H7]);

            Assert.AreEqual(BitHelper.ConvertToUInt64(new byte[64]
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
            }), target.PawnMoves[(int)ChessColor.Black, (int)Square.E5]);
            Assert.AreEqual(Square.E4, target.PawnStep[(int)ChessColor.Black, (int)Square.E5]);
        }

        [TestMethod]
        public void MaskWhitePassedPawnTest()
        {
            var target = new HelperBitboards();

            var boardE5 = Bitboards.PrintBitboard(target.MaskWhitePassedPawns[(int)Square.E5]);
            var boardA2 = Bitboards.PrintBitboard(target.MaskWhitePassedPawns[(int)Square.A2]);
            var boardH2 = Bitboards.PrintBitboard(target.MaskWhitePassedPawns[(int)Square.H2]);

            Assert.AreEqual(BitHelper.ConvertToUInt64(new byte[64]
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

            Assert.AreEqual(BitHelper.ConvertToUInt64(new byte[64]
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

            Assert.AreEqual(BitHelper.ConvertToUInt64(new byte[64]
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
            var target = new HelperBitboards();

            var boardE5 = Bitboards.PrintBitboard(target.MaskBlackPassedPawns[(int)Square.E5]);
            var boardA7 = Bitboards.PrintBitboard(target.MaskBlackPassedPawns[(int)Square.A7]);
            var boardH7 = Bitboards.PrintBitboard(target.MaskBlackPassedPawns[(int)Square.H7]);

            Assert.AreEqual(BitHelper.ConvertToUInt64(new byte[64]
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

            Assert.AreEqual(BitHelper.ConvertToUInt64(new byte[64]
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

            Assert.AreEqual(BitHelper.ConvertToUInt64(new byte[64]
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
            var target = new HelperBitboards();

            var boardE5 = Bitboards.PrintBitboard(target.MaskIsolatedPawns[(int)Square.E5]);
            var boardA2 = Bitboards.PrintBitboard(target.MaskIsolatedPawns[(int)Square.A2]);
            var boardH2 = Bitboards.PrintBitboard(target.MaskIsolatedPawns[(int)Square.H2]);

            Assert.AreEqual(BitHelper.ConvertToUInt64(new byte[64]
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

            Assert.AreEqual(BitHelper.ConvertToUInt64(new byte[64]
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

            Assert.AreEqual(BitHelper.ConvertToUInt64(new byte[64]
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
            var target = new HelperBitboards();

            var boardE5 = Bitboards.PrintBitboard(target.MaskWhitePawnsPath[(int)Square.E5]);
            var boardA2 = Bitboards.PrintBitboard(target.MaskWhitePawnsPath[(int)Square.A2]);
            var boardH2 = Bitboards.PrintBitboard(target.MaskWhitePawnsPath[(int)Square.H2]);

            Assert.AreEqual(BitHelper.ConvertToUInt64(new byte[64]
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

            Assert.AreEqual(BitHelper.ConvertToUInt64(new byte[64]
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

            Assert.AreEqual(BitHelper.ConvertToUInt64(new byte[64]
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
            var target = new HelperBitboards();

            var boardE5 = Bitboards.PrintBitboard(target.MaskBlackPawnsPath[(int)Square.E5]);
            var boardA7 = Bitboards.PrintBitboard(target.MaskBlackPawnsPath[(int)Square.A7]);
            var boardH7 = Bitboards.PrintBitboard(target.MaskBlackPawnsPath[(int)Square.H7]);

            Assert.AreEqual(BitHelper.ConvertToUInt64(new byte[64]
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

            Assert.AreEqual(BitHelper.ConvertToUInt64(new byte[64]
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

            Assert.AreEqual(BitHelper.ConvertToUInt64(new byte[64]
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
        public void MaskColsTest()
        {
            var target = new HelperBitboards();

            var boardH7 = Bitboards.PrintBitboard(target.MaskCols[(int)Square.H7]);
            var boardD4 = Bitboards.PrintBitboard(target.MaskCols[(int)Square.D4]);
            var boardNotA = Bitboards.PrintBitboard(target.Not_A_file);
            var boardNotH = Bitboards.PrintBitboard(target.Not_H_file);

            Assert.AreEqual(BitHelper.ConvertToUInt64(new byte[64]
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

            Assert.AreEqual(BitHelper.ConvertToUInt64(new byte[64]
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

            Assert.AreEqual(BitHelper.ConvertToUInt64(new byte[64]
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

            Assert.AreEqual(BitHelper.ConvertToUInt64(new byte[64]
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
        public void RanksTest()
        {
            var target = new HelperBitboards();

            // white
            Assert.AreEqual(0, target.Rank[0, 1]);
            Assert.AreEqual(4, target.Rank[0, 35]);
            Assert.AreEqual(7, target.Rank[0, 62]);

            // black
            Assert.AreEqual(7, target.Rank[1, 1]);
            Assert.AreEqual(3, target.Rank[1, 35]);
            Assert.AreEqual(0, target.Rank[1, 62]);
        }
    }
}
