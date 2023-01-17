using Microsoft.VisualStudio.TestTools.UnitTesting;
using MantaChessEngine;
using MantaCommon;

namespace MantaChessEngineTest
{
    [TestClass]
    public class HashtableTest
    {
        IHashtable hash;
        IBoard board;
        IMoveFactory<IMove> moveFactory;

        [TestInitialize]
        public void Setup()
        {
            hash = new Hashtable(1024);
            board = new Board(hash);
            moveFactory = new MoveFactory(board);
        }

        [TestMethod]
        public void SetPieceAndRemovePieceResultsInOriginalHashKeyTest()
        {
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
            board.SetInitialPosition();

            var startKey = hash.CurrentKey;
            board.Move(moveFactory.MakeMoveUci("e2e4"));
            Assert.AreNotEqual(startKey, hash.CurrentKey, "Keys should differ.");

            board.Back();
            Assert.AreEqual(startKey, hash.CurrentKey, "Keys should be equal.");
        }

        [TestMethod]
        public void PawnCaptureHashTest()
        {
            board.SetInitialPosition();
            board.Move(moveFactory.MakeMoveUci("e2e4"));
            board.Move(moveFactory.MakeMoveUci("d7d5"));

            var startKey = hash.CurrentKey;
            board.Move(moveFactory.MakeMoveUci("e4d5")); // capture
            Assert.AreNotEqual(startKey, hash.CurrentKey, "Keys should differ.");

            board.Back();
            Assert.AreEqual(startKey, hash.CurrentKey, "Keys should be equal.");
        }

        [TestMethod]
        public void EnPassantCaptureHashTest()
        {
            board.SetPosition(".......k" +
                              ".p......" +
                              "........" +
                              "P......." +
                              "........" +
                              "........" +
                              "........" +
                              "...K....");
            board.Move(moveFactory.MakeMoveUci("b7b5")); // after move en passant square is b6

            var startKey = hash.CurrentKey;
            board.Move(moveFactory.MakeMoveUci("a5b6")); // en passant capture
            Assert.AreNotEqual(startKey, hash.CurrentKey, "Keys should differ.");

            board.Back();
            Assert.AreEqual(startKey, hash.CurrentKey, "Keys should be equal.");
        }

        [TestMethod]
        public void WhiteKingSideCastlingHashTest()
        {
            board.SetPosition(".k......" +
                              ".p......" +
                              "........" +
                              "P......." +
                              "........" +
                              "........" +
                              "........" +
                              "....K..R");

            var startKey = hash.CurrentKey;
            board.Move(moveFactory.MakeMoveUci("e1g1")); // white king side castling
            Assert.AreNotEqual(startKey, hash.CurrentKey, "Keys should differ.");

            board.Back();
            Assert.AreEqual(startKey, hash.CurrentKey, "Keys should be equal.");
        }

        [TestMethod]
        public void WhiteQueenSideCastlingHashTest()
        {
            board.SetPosition(".k......" +
                              ".p......" +
                              "........" +
                              "P......." +
                              "........" +
                              "........" +
                              "........" +
                              "R...K..R");

            var startKey = hash.CurrentKey;
            board.Move(moveFactory.MakeMoveUci("e1c1")); // white queen side castling
            Assert.AreNotEqual(startKey, hash.CurrentKey, "Keys should differ.");

            board.Back();
            Assert.AreEqual(startKey, hash.CurrentKey, "Keys should be equal.");
        }

        [TestMethod]
        public void BlackKingSideCastlingHashTest()
        {
            board.SetPosition("r...k..r" +
                              ".p......" +
                              "........" +
                              "P......." +
                              "........" +
                              "........" +
                              "........" +
                              "....K...");

            var startKey = hash.CurrentKey;
            board.Move(moveFactory.MakeMoveUci("e8g8")); // black king side castling
            Assert.AreNotEqual(startKey, hash.CurrentKey, "Keys should differ.");

            board.Back();
            Assert.AreEqual(startKey, hash.CurrentKey, "Keys should be equal.");
        }

        [TestMethod]
        public void BlackQueenSideCastlingHashTest()
        {
            board.SetPosition("r...k..r" +
                              ".p......" +
                              "........" +
                              "P......." +
                              "........" +
                              "........" +
                              "........" +
                              "....K...");

            var startKey = hash.CurrentKey;
            board.Move(moveFactory.MakeMoveUci("e8c8")); // black queen side castling
            Assert.AreNotEqual(startKey, hash.CurrentKey, "Keys should differ.");

            board.Back();
            Assert.AreEqual(startKey, hash.CurrentKey, "Keys should be equal.");
        }

        [TestMethod]
        public void PromotionMoveHashTest()
        {
            board.SetPosition(".......k" +
                              "P......." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "...K....");

            var startKey = hash.CurrentKey;
            board.Move(moveFactory.MakeMoveUci("a7a8r")); // Promotion to rook
            Assert.AreNotEqual(startKey, hash.CurrentKey, "Keys should differ.");

            board.Back();
            Assert.AreEqual(startKey, hash.CurrentKey, "Keys should be equal.");
        }

        [TestMethod]
        public void PromotionCaptureMoveHashTest()
        {
            board.SetPosition(".r.....k" +
                              "P......." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "...K....");

            var startKey = hash.CurrentKey;
            board.Move(moveFactory.MakeMoveUci("a7b8q")); // promotion capture to queen
            Assert.AreNotEqual(startKey, hash.CurrentKey, "Keys should differ.");

            board.Back();
            Assert.AreEqual(startKey, hash.CurrentKey, "Keys should be equal.");
        }
    }
}
