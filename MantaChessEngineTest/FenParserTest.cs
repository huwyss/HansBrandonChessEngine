using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MantaChessEngine;

namespace MantaChessEngineTest
{
    [TestClass]
    public class FenParserTest
    {
        FenParser _fenParser = new FenParser(); // under test

        [TestMethod]
        public void SetStartPositionFromFenTest()
        {
            var fen = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1";
            string expectedString = "rnbqkbnr" +
                                    "pppppppp" +
                                    "........" +
                                    "........" +
                                    "........" +
                                    "........" +
                                    "PPPPPPPP" +
                                    "RNBQKBNR";

            var actualPosInfo = _fenParser.ToPositionInfo(fen);
            
            Assert.AreEqual(expectedString, actualPosInfo.PositionString);
        }

        [TestMethod]
        public void SetOtherPositionFromFenTest()
        {
            var fen = " rnbqkbnr/pp1ppppp/8/2p5/4P3/5N2/PPPP1PPP/RNBQKB1R b KQkq - 1 2 ";
            string expectedString = "rnbqkbnr" +
                                    "pp.ppppp" +
                                    "........" +
                                    "..p....." +
                                    "....P..." +
                                    ".....N.." +
                                    "PPPP.PPP" +
                                    "RNBQKB.R";

            var actualPosInfo = _fenParser.ToPositionInfo(fen);

            Assert.AreEqual(expectedString, actualPosInfo.PositionString);
        }

        [TestMethod]
        public void SideToMoveIsWhiteTest()
        {
            var fen = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1";
            var expectedSide = Definitions.ChessColor.White;

            var actualSideToMove = _fenParser.ToPositionInfo(fen).SideToMove;

            Assert.AreEqual(expectedSide, actualSideToMove);
        }

        [TestMethod]
        public void SideToMoveIsBlackTest()
        {
            var fen = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR b KQkq - 0 1";
            var expectedSide = Definitions.ChessColor.Black;

            var actualSideToMove = _fenParser.ToPositionInfo(fen).SideToMove;

            Assert.AreEqual(expectedSide, actualSideToMove);
        }

        [TestMethod]
        public void CastlingRightAllTest()
        {
            var fen = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1";

            var posInfo = _fenParser.ToPositionInfo(fen);

            Assert.AreEqual(true, posInfo.CastlingRightWhiteKingSide);
            Assert.AreEqual(true, posInfo.CastlingRightWhiteQueenSide);
            Assert.AreEqual(true, posInfo.CastlingRightBlackKingSide);
            Assert.AreEqual(true, posInfo.CastlingRightBlackQueenSide);
        }

        [TestMethod]
        public void CastlingRightNoneTest()
        {
            var fen = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w - - 0 1";

            var posInfo = _fenParser.ToPositionInfo(fen);

            Assert.AreEqual(false, posInfo.CastlingRightWhiteKingSide);
            Assert.AreEqual(false, posInfo.CastlingRightWhiteQueenSide);
            Assert.AreEqual(false, posInfo.CastlingRightBlackKingSide);
            Assert.AreEqual(false, posInfo.CastlingRightBlackQueenSide);
        }

        [TestMethod]
        public void EnPassantFieldNoneTest()
        {
            var fen = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w - - 0 1";

            var posInfo = _fenParser.ToPositionInfo(fen);

            Assert.AreEqual(0, posInfo.EnPassantFile);
            Assert.AreEqual(0, posInfo.EnPassantRank);
        }

        [TestMethod]
        public void EnPassantFieldAvailableRank3Test()
        {
            var fen = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w - c3 0 1";

            var posInfo = _fenParser.ToPositionInfo(fen);

            Assert.AreEqual('c', posInfo.EnPassantFile);
            Assert.AreEqual(3, posInfo.EnPassantRank);
        }

        [TestMethod]
        public void EnPassantFieldAvailableRank6Test()
        {
            var fen = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR b - c6 0 1";

            var posInfo = _fenParser.ToPositionInfo(fen);

            Assert.AreEqual('c', posInfo.EnPassantFile);
            Assert.AreEqual(6, posInfo.EnPassantRank);
        }

        [TestMethod]
        public void MoveSinceCaptureOrPawnAndMoveCounterTest()
        {
            var fen = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR b - c6 7 29";

            var posInfo = _fenParser.ToPositionInfo(fen);

            Assert.AreEqual(7, posInfo.MoveCountSincePawnOrCapture);
            Assert.AreEqual(29, posInfo.MoveNumber);
        }
    }
}

