using Microsoft.VisualStudio.TestTools.UnitTesting;
using MantaBitboardEngine;
using MantaCommon;

namespace MantaBitboardEngineTest
{
    [TestClass]
    public class HashtableTest
    {
        IHashtable _hash;
        Bitboards _board;
        IMoveFactory<BitMove> _moveFactory;

        [TestInitialize]
        public void Setup()
        {
            _hash = new Hashtable(1024);
            _board = new Bitboards(_hash);
            _moveFactory = new BitMoveFactory(_board);
        }

        [TestMethod]
        public void SetPieceAndRemovePieceResultsInOriginalHashKeyTest()
        {
            var keyEmpty = _hash.CurrentKey;
            _board.SetPiece(ChessColor.White, PieceType.Pawn, Square.A2);

            var keyWithPawn = _hash.CurrentKey;
            Assert.AreNotEqual(keyEmpty, keyWithPawn, "Keys should differ.");

            _board.RemovePiece(Square.A2);
            Assert.AreEqual(keyEmpty, _hash.CurrentKey);
        }

        [TestMethod]
        public void PawnMoveHashTest()
        {
            _board.SetInitialPosition();

            var startKey = _hash.CurrentKey;
            _board.Move(_moveFactory.MakeMoveUci("e2e4"));
            Assert.AreNotEqual(startKey, _hash.CurrentKey, "Keys should differ.");

            _board.Back();
            Assert.AreEqual(startKey, _hash.CurrentKey, "Keys should be equal.");
        }

        [TestMethod]
        public void PawnCaptureHashTest()
        {
            _board.SetInitialPosition();
            _board.Move(_moveFactory.MakeMoveUci("e2e4"));
            _board.Move(_moveFactory.MakeMoveUci("d7d5"));

            var startKey = _hash.CurrentKey;
            _board.Move(_moveFactory.MakeMoveUci("e4d5")); // capture
            Assert.AreNotEqual(startKey, _hash.CurrentKey, "Keys should differ.");

            _board.Back();
            Assert.AreEqual(startKey, _hash.CurrentKey, "Keys should be equal.");
        }

        [TestMethod]
        public void EnPassantCaptureHashTest()
        {
            _board.SetPosition(".......k" +
                              ".p......" +
                              "........" +
                              "P......." +
                              "........" +
                              "........" +
                              "........" +
                              "...K....");
            _board.Move(_moveFactory.MakeMoveUci("b7b5")); // after move en passant square is b6

            var startKey = _hash.CurrentKey;
            _board.Move(_moveFactory.MakeMoveUci("a5b6")); // en passant capture
            Assert.AreNotEqual(startKey, _hash.CurrentKey, "Keys should differ.");

            _board.Back();
            Assert.AreEqual(startKey, _hash.CurrentKey, "Keys should be equal.");
        }

        [TestMethod]
        public void WhiteKingSideCastlingHashTest()
        {
            _board.SetPosition(".k......" +
                              ".p......" +
                              "........" +
                              "P......." +
                              "........" +
                              "........" +
                              "........" +
                              "....K..R");

            var startKey = _hash.CurrentKey;
            _board.Move(_moveFactory.MakeMoveUci("e1g1")); // white king side castling
            Assert.AreNotEqual(startKey, _hash.CurrentKey, "Keys should differ.");

            _board.Back();
            Assert.AreEqual(startKey, _hash.CurrentKey, "Keys should be equal.");
        }

        [TestMethod]
        public void WhiteQueenSideCastlingHashTest()
        {
            _board.SetPosition(".k......" +
                              ".p......" +
                              "........" +
                              "P......." +
                              "........" +
                              "........" +
                              "........" +
                              "R...K..R");

            var startKey = _hash.CurrentKey;
            _board.Move(_moveFactory.MakeMoveUci("e1c1")); // white queen side castling
            Assert.AreNotEqual(startKey, _hash.CurrentKey, "Keys should differ.");

            _board.Back();
            Assert.AreEqual(startKey, _hash.CurrentKey, "Keys should be equal.");
        }

        [TestMethod]
        public void BlackKingSideCastlingHashTest()
        {
            _board.SetPosition("r...k..r" +
                              ".p......" +
                              "........" +
                              "P......." +
                              "........" +
                              "........" +
                              "........" +
                              "....K...");

            var startKey = _hash.CurrentKey;
            _board.Move(_moveFactory.MakeMoveUci("e8g8")); // black king side castling
            Assert.AreNotEqual(startKey, _hash.CurrentKey, "Keys should differ.");

            _board.Back();
            Assert.AreEqual(startKey, _hash.CurrentKey, "Keys should be equal.");
        }

        [TestMethod]
        public void BlackQueenSideCastlingHashTest()
        {
            _board.SetPosition("r...k..r" +
                              ".p......" +
                              "........" +
                              "P......." +
                              "........" +
                              "........" +
                              "........" +
                              "....K...");

            var startKey = _hash.CurrentKey;
            _board.Move(_moveFactory.MakeMoveUci("e8c8")); // black queen side castling
            Assert.AreNotEqual(startKey, _hash.CurrentKey, "Keys should differ.");

            _board.Back();
            Assert.AreEqual(startKey, _hash.CurrentKey, "Keys should be equal.");
        }

        [TestMethod]
        public void PromotionMoveHashTest()
        {
            _board.SetPosition(".......k" +
                              "P......." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "...K....");

            var startKey = _hash.CurrentKey;
            _board.Move(_moveFactory.MakeMoveUci("a7a8r")); // Promotion to rook
            Assert.AreNotEqual(startKey, _hash.CurrentKey, "Keys should differ.");

            _board.Back();
            Assert.AreEqual(startKey, _hash.CurrentKey, "Keys should be equal.");
        }

        [TestMethod]
        public void PromotionCaptureMoveHashTest()
        {
            _board.SetPosition(".r.....k" +
                              "P......." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "...K....");

            var startKey = _hash.CurrentKey;
            _board.Move(_moveFactory.MakeMoveUci("a7b8q")); // promotion capture to queen
            Assert.AreNotEqual(startKey, _hash.CurrentKey, "Keys should differ.");

            _board.Back();
            Assert.AreEqual(startKey, _hash.CurrentKey, "Keys should be equal.");
        }

        [TestMethod]
        public void CurrentKeyWithInitPositionMustNotBe0Test()
        {
            _board.SetInitialPosition();
            Assert.AreNotEqual(0, (int)_hash.CurrentKey);
        }

        [TestMethod]
        public void MoveCanBeStoredInHashtableTest()
        {
            _board.SetInitialPosition();

            var startKey = _hash.CurrentKey;
            var bestMove = _moveFactory.MakeMoveUci("e2e4");

            _hash.AddHash(bestMove.MovingColor, 3, 200, HashEntryType.Exact, Square.E2, Square.E4, PieceType.Empty);
            var hashEntry = _hash.LookupPvMove(ChessColor.White);
            if (hashEntry != null)
            {
                var moveFromHashtable = _moveFactory.MakeMove(hashEntry.From, hashEntry.To, hashEntry.PromotionPiece);
                Assert.AreEqual(bestMove, moveFromHashtable, "move from hashtable should be the same!");
            }
            else
            {
                Assert.Fail("Move was not in hashtable but should.");
            }
        }

        [TestMethod]
        public void MoveCanBeStoredInHashtableLaterInGameTest()
        {
            _board.SetInitialPosition();

            var bestMove = _moveFactory.MakeMoveUci("e2e4");

            _hash.AddHash(bestMove.MovingColor, 3, 200, HashEntryType.Exact, Square.E2, Square.E4, PieceType.Empty);

            // do some moves to come back to same position
            _board.Move(_moveFactory.MakeMoveUci("g8f6"));
            _board.Move(_moveFactory.MakeMoveUci("g1f3"));
            _board.Move(_moveFactory.MakeMoveUci("f6g8"));
            _board.Move(_moveFactory.MakeMoveUci("f3g1"));

            var hashEntry = _hash.LookupPvMove(ChessColor.White);
            if (hashEntry != null)
            {
                var moveFromHashtable = _moveFactory.MakeMove(hashEntry.From, hashEntry.To, hashEntry.PromotionPiece);
                Assert.AreEqual(bestMove, moveFromHashtable, "move from hashtable should be the same!");
            }
            else
            {
                Assert.Fail("Move was not in hashtable but should.");
            }
        }

        // todo: only overwrite move in hash if newer is calculated deeper !

        [TestMethod]
        public void HashKeyIsConstantAtInitialPosition()
        {
            _board.SetInitialPosition();
            var startKey = _hash.CurrentKey;

            _board.SetInitialPosition();
            Assert.AreEqual(startKey, _hash.CurrentKey);

            _board.Move(_moveFactory.MakeMoveUci("e2e4"));
            _board.Move(_moveFactory.MakeMoveUci("e7e5"));

            _board.SetInitialPosition();
            Assert.AreEqual(startKey, _hash.CurrentKey);
        }
    }
}
