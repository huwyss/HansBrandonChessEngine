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
            var hash = new Hashtable(1024);
            var board = new Board(hash);

            var keyEmpty = hash.CurrentKey;
            board.SetPiece(Piece.MakePiece(PieceType.Pawn, ChessColor.White), Square.A2);

            var keyWithPawn = hash.CurrentKey;
            Assert.AreNotEqual(keyEmpty, keyWithPawn, "Keys should differ.");

            board.RemovePiece(Square.A2);
            Assert.AreEqual(keyEmpty, hash.CurrentKey);
        }

        [TestMethod]
        public void PawnMoveHashTest()
        {
            var hash = new Hashtable(1024);
            var board = new Board(hash);
            board.SetInitialPosition();
            var moveFactory = new MoveFactory(board);

            var startKey = hash.CurrentKey;
            board.Move(moveFactory.MakeMoveUci("e2e4"));
            Assert.AreNotEqual(startKey, hash.CurrentKey, "Keys should differ.");

            board.Back();
            Assert.AreEqual(startKey, hash.CurrentKey, "Keys should be equal.");
        }

        [TestMethod]
        public void PawnCaptureHashTest()
        {
            var hash = new Hashtable(1024);
            var board = new Board(hash);
            board.SetInitialPosition();
            var moveFactory = new MoveFactory(board);
            board.Move(moveFactory.MakeMoveUci("e2e4"));
            board.Move(moveFactory.MakeMoveUci("d7d5"));

            var startKey = hash.CurrentKey;
            board.Move(moveFactory.MakeMoveUci("e4d5")); // capture
            Assert.AreNotEqual(startKey, hash.CurrentKey, "Keys should differ.");

            board.Back();
            Assert.AreEqual(startKey, hash.CurrentKey, "Keys should be equal.");
        }
    }
}
