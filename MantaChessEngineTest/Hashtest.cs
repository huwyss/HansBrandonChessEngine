using Microsoft.VisualStudio.TestTools.UnitTesting;
using MantaChessEngine;
using MantaCommon;

namespace MantaChessEngineTest
{
    [TestClass]
    public class Hashtest
    {
        [TestMethod]
        public void SetPieceAndRemovePieceResultsInOriginalHashKeyTest()
        {
            var hash = new Hashtable(512);
            var board = new Board(hash);

            var keyEmpty = hash.CurrentKey;
            board.SetPiece(Piece.MakePiece(PieceType.Pawn, ChessColor.White), Square.A2);

            var keyWithPawn = hash.CurrentKey;
            Assert.AreNotEqual(keyEmpty, keyWithPawn, "Keys should differ.");

            board.RemovePiece(Square.A2);
            Assert.AreEqual(keyEmpty, hash.CurrentKey);
        }
    }
}
