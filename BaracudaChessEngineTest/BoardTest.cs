using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BaracudaChessEngine;

namespace BaracudaChessEngineTest
{
    [TestClass]
    public class BoardTest
    {
        [TestMethod]
        public void GetSetPieceTest_WhenSetPieceRookToD8_ThenGetPieceD8ShouldReturnRook()
        {
            var target = new Board(null);
            target.SetPiece('R', 4, 8);
            char piece = target.GetPiece('d', 8);
            Assert.AreEqual('R', piece);
        }

        [TestMethod]
        public void GetSetPieceTest_WhenSetPieceRookTo48_ThenGetPiece48ShouldReturnRook()
        {
            var target = new Board(null);
            target.SetPiece('R', 4, 8);
            char piece = target.GetPiece(4, 8);
            Assert.AreEqual('R', piece);
        }

        [TestMethod]
        public void GetPiece_WhenNewBoard_ThenAllPositionsEmpty()
        {
            var target = new Board(null);
            char piece = target.GetPiece('d', 8);
            Assert.AreEqual(Definitions.EmptyField, piece);
        }

        [TestMethod]
        public void InitPosition_WhenInitializedPosition_ThenPiecesAtInitPosition()
        {
            var target = new Board(null);
            target.SetInitialPosition();

            Assert.AreEqual('R', target.GetPiece('a', 1));
            Assert.AreEqual('N', target.GetPiece('b', 1));
            Assert.AreEqual('B', target.GetPiece('c', 1));
            Assert.AreEqual('Q', target.GetPiece('d', 1));
            Assert.AreEqual('K', target.GetPiece('e', 1));
            Assert.AreEqual('B', target.GetPiece('f', 1));
            Assert.AreEqual('N', target.GetPiece('g', 1));
            Assert.AreEqual('R', target.GetPiece('h', 1));

            Assert.AreEqual('P', target.GetPiece('b', 2)); // white pawn
            Assert.AreEqual(Definitions.EmptyField, target.GetPiece('c', 3)); // empty
            Assert.AreEqual(Definitions.EmptyField, target.GetPiece('d', 4)); // empty
            Assert.AreEqual(Definitions.EmptyField, target.GetPiece('e', 5)); // empty
            Assert.AreEqual(Definitions.EmptyField, target.GetPiece('f', 6)); // empty
            Assert.AreEqual('p', target.GetPiece('g', 7)); // black pawn

            Assert.AreEqual('r', target.GetPiece('a', 8));
            Assert.AreEqual('n', target.GetPiece('b', 8));
            Assert.AreEqual('b', target.GetPiece('c', 8));
            Assert.AreEqual('q', target.GetPiece('d', 8));
            Assert.AreEqual('k', target.GetPiece('e', 8));
            Assert.AreEqual('b', target.GetPiece('f', 8));
            Assert.AreEqual('n', target.GetPiece('g', 8));
            Assert.AreEqual('r', target.GetPiece('h', 8));

            Assert.AreEqual(0, target.History.LastEnPassantFile);
            Assert.AreEqual(0, target.History.LastEnPassantRank);
            //Assert.AreEqual(true, target.CastlingRightFirstMover); // white  // todo castling
            //Assert.AreEqual(true, target.CastlingRightSecondMover); // black
        }

        [TestMethod]
        public void MoveTest_WhenPawnMovesNormal_ThenNewPositionOk()
        {
            MoveGenerator generator = new MoveGenerator();
            Board target = new Board(generator);
            target.SetInitialPosition();

            target.Move("e2e4");
            Assert.AreEqual(Definitions.EmptyField, target.GetPiece('e', 2));
            Assert.AreEqual('P', target.GetPiece('e', 4));
            Assert.AreEqual(Definitions.ChessColor.Black, target.SideToMove);
        }

        [TestMethod]
        public void MoveTest_WhenPawnMovesNormalAndMoveIsOfTypeMove_ThenNewPositionOk()
        {
            MoveGenerator generator = new MoveGenerator();
            Board target = new Board(generator);
            target.SetInitialPosition();
            target.Move("e2e4");

            Assert.AreEqual(Definitions.EmptyField, target.GetPiece('e', 2));
            Assert.AreEqual('P', target.GetPiece('e', 4));
            Assert.AreEqual(Definitions.ChessColor.Black, target.SideToMove);
        }

        [TestMethod]
        public void MoveTest_WhenQueenCapturesPiece_ThenNewPositionOk()
        {
            MoveGenerator generator = new MoveGenerator();
            Board target = new Board(generator);
            target.SetInitialPosition();
            target.SetPiece(Definitions.EmptyField, 'd', 2);

            target.Move("d1d7");
            Assert.AreEqual(Definitions.EmptyField, target.GetPiece('d', 1));
            Assert.AreEqual('Q', target.GetPiece('d', 7));
            Assert.AreEqual(Definitions.ChessColor.Black, target.SideToMove);

            var moveList = target.History.Moves;
            Assert.AreEqual(1, target.History.Moves.Count);
            Assert.AreEqual(new Move('Q', 4, 1, 4, 7, 'p'), target.History.Moves[0]);
        }

        [TestMethod]
        public void MoveTest_WhenPawnMovesTwoFields_ThenEnPassantFieldSet_Black()
        {
            MoveGenerator generator = new MoveGenerator();
            Board board = new Board(generator);
            string position = ".......k" +
                              "p......." +
                              "........" +
                              ".P......" +
                              "........" +
                              "........" +
                              "........" +
                              "...K....";
            board.SetPosition(position);
            board.Move("a7a5");
            Assert.AreEqual(Helper.FileCharToFile('a'), board.History.LastEnPassantFile);
            Assert.AreEqual(6, board.History.LastEnPassantRank);
        }

        [TestMethod]
        public void MoveTest_WhenPawnMovesTwoFields_ThenEnPassantFieldSet_White()
        {
            MoveGenerator generator = new MoveGenerator();
            Board board = new Board(generator);
            string position = ".......k" +
                              "........" +
                              "........" +
                              "........" +
                              "p......." +
                              "........" +
                              ".P......" +
                              "...K....";
            board.SetPosition(position);
            board.Move("b2b4");
            Assert.AreEqual(Helper.FileCharToFile('b'), board.History.LastEnPassantFile);
            Assert.AreEqual(3, board.History.LastEnPassantRank);
        }

        [TestMethod]
        public void MoveTest_WhenBlackCapturesEnPassant_ThenMoveCorrect_BlackMoves()
        {
            MoveGenerator generator = new MoveGenerator();
            Board board = new Board(generator);
            string position = ".......k" +
                              "........" +
                              "........" +
                              "........" +
                              "p......." +
                              "........" +
                              ".P......" +
                              "...K....";
            board.SetPosition(position);
            board.Move("b2b4");

            board.Move("a4b3Pe"); // capture en passant

            string expPosit = ".......k" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              ".p......" +
                              "........" +
                              "...K....";
            Assert.AreEqual(expPosit, board.GetString, "En passant capture not correct move.");
            Assert.AreEqual(board.History.Moves[1], new Move("a4b3Pe"));
        }

        [TestMethod]
        public void MoveTest_WhenWhiteCapturesEnPassant_ThenMoveCorrect_WhiteMoves()
        {
            MoveGenerator generator = new MoveGenerator();
            Board board = new Board(generator);
            string position = ".......k" +
                              ".p......" +
                              "........" +
                              "P......." +
                              "........" +
                              "........" +
                              "........" +
                              "...K....";
            board.SetPosition(position);
            board.Move("b7b5");

            board.Move("a5b6pe"); // capture en passant

            string expPosit = ".......k" +
                              "........" +
                              ".P......" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "...K....";
            Assert.AreEqual(expPosit, board.GetString, "En passant capture not correct move.");
            Assert.AreEqual(board.History.Moves[1], new Move("a5b6pe"));
        }

        [TestMethod]
        public void GetColorTest()
        {
            var target = new Board(null);
            target.SetInitialPosition();
            Assert.AreEqual(Definitions.ChessColor.White, target.GetColor(5, 2));
            Assert.AreEqual(Definitions.ChessColor.Empty, target.GetColor(5, 3));
            Assert.AreEqual(Definitions.ChessColor.Black, target.GetColor(5, 7));
            Assert.AreEqual(Definitions.ChessColor.Empty, target.GetColor(5, 5));
        }

        [TestMethod]
        public void GetStringTest_WhenInitPos_ThenCorrect()
        {
            var target = new Board(null);
            target.SetInitialPosition();

            string boardString = target.GetString;
            string expectedString = "rnbqkbnr" +
                                    "pppppppp" +
                                    "........" +
                                    "........" +
                                    "........" +
                                    "........" +
                                    "PPPPPPPP" +
                                    "RNBQKBNR";

            Assert.AreEqual(expectedString, boardString);
        }

        [TestMethod]
        public void GetPrintStringTest_WhenInitPos_ThenCorrect()
        {
            var target = new Board(null);
            target.SetInitialPosition();

            string boardString = target.GetPrintString;
            string expectedString = "8   r n b q k b n r \n" +
                                    "7   p p p p p p p p \n" +
                                    "6   . . . . . . . . \n" +
                                    "5   . . . . . . . . \n" +
                                    "4   . . . . . . . . \n" +
                                    "3   . . . . . . . . \n" +
                                    "2   P P P P P P P P \n" +
                                    "1   R N B Q K B N R \n" +
                                    "\n" +
                                    "    a b c d e f g h \n";

            Assert.AreEqual(expectedString, boardString);
        }

        // ----------------------------------------------------------------------------------------------------
        // Is Winner Test
        // ----------------------------------------------------------------------------------------------------

        [TestMethod]
        public void IsWinnerTest_WhenBlackKingMissing_ThenWhiteWins()
        {
            Board board = new Board(null);
            string position = "........" +
                              "........" +
                              "....p..." +
                              "........" +
                              "........" +
                              "........" +
                              "....P..." +
                              "....K...";
            board.SetPosition(position);

            bool whiteWins = board.IsWinner(Definitions.ChessColor.White);
            bool blackWins = board.IsWinner(Definitions.ChessColor.Black);

            Assert.AreEqual(true, whiteWins);
            Assert.AreEqual(false, blackWins);
        }

        [TestMethod]
        public void IsWinnerTest_WhenInitialPos_ThenNooneWins()
        {
            Board board = new Board(null);
            board.SetInitialPosition();

            bool whiteWins = board.IsWinner(Definitions.ChessColor.White);
            bool blackWins = board.IsWinner(Definitions.ChessColor.Black);

            Assert.AreEqual(false, whiteWins);
            Assert.AreEqual(false, blackWins);
        }

        [TestMethod]
        public void CloneTest_Normalcase()
        {
            MoveGenerator generator = new MoveGenerator();
            Board board = new Board(generator);
            board.SetInitialPosition();

            Board cloned = board.Clone();

            Assert.AreNotEqual(cloned, board, "must not return the same object!");
            Assert.AreEqual(board.GetString, cloned.GetString);
            Assert.AreEqual(board.History.LastEnPassantFile, cloned.History.LastEnPassantFile);
            Assert.AreEqual(board.History.LastEnPassantRank, cloned.History.LastEnPassantRank);
        }

        [TestMethod]
        public void IsCheckTest_WhenKingAttacked_ThenTrue()
        {
            MoveGenerator generator = new MoveGenerator();
            Board board = new Board(generator);
            string position = "....rk.." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "....K...";
            board.SetPosition(position);

            Assert.AreEqual(true, board.IsCheck(Definitions.ChessColor.White), "king is attacked by rook!");
        }

        [TestMethod]
        public void IsCheckTest_WhenKingNotAttacked_ThenFalse()
        {
            MoveGenerator generator = new MoveGenerator();
            Board board = new Board(generator);
            string position = ".....k.." +
                              ".....p.." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "....K...";
            board.SetPosition(position);

            Assert.AreEqual(false, board.IsCheck(Definitions.ChessColor.White), "king is not attacked!");
        }

        // -------------------------------------------------------------------
        // Back tests
        // -------------------------------------------------------------------

        [TestMethod]
        public void BackTest_WhenWhiteAndBlackMovesDone_ThenGoBackToInitPosition()
        {
            MoveGenerator generator = new MoveGenerator();
            Board target = new Board(generator);
            target.SetInitialPosition();
            target.Move("e2e4");
            target.Move("e7e5");

            target.Back();
            string expectedString = "rnbqkbnr" +
                                    "pppppppp" +
                                    "........" +
                                    "........" +
                                    "....P..." +
                                    "........" +
                                    "PPPP.PPP" +
                                    "RNBQKBNR";
            Assert.AreEqual(expectedString, target.GetString);
            Assert.AreEqual(Definitions.ChessColor.Black, target.SideToMove);
            Assert.AreEqual(Helper.FileCharToFile('e'), target.EnPassantFile, "en passant file wrong after 1st back");
            Assert.AreEqual(3, target.EnPassantRank, "en passant rank wrong after 1st back");

            target.Back();
            expectedString = "rnbqkbnr" +
                             "pppppppp" +
                             "........" +
                             "........" +
                             "........" +
                             "........" +
                             "PPPPPPPP" +
                             "RNBQKBNR";
            Assert.AreEqual(expectedString, target.GetString);
            Assert.AreEqual(Definitions.ChessColor.White, target.SideToMove);
            Assert.AreEqual(0, target.EnPassantFile, "en passant file wrong after 2dn back");
            Assert.AreEqual(0, target.EnPassantRank, "en passant rank wrong after 2dn back");
        }

        [TestMethod]
        public void MoveTest_WhenBackAfterEnPassant_ThenMoveCorrect()
        {
            // init move: white pawn moves two fields and black captures en passant
            MoveGenerator generator = new MoveGenerator();
            Board board = new Board(generator);
            string position = ".......k" +
                              "........" +
                              "........" +
                              "........" +
                              "p......." +
                              "........" +
                              ".P......" +
                              "...K....";
            board.SetPosition(position);
            board.Move("b2b4");
            board.Move("a4b3Pe"); // capture en passant

            string expPosit = ".......k" + // position after capture en passant
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              ".p......" +
                              "........" +
                              "...K....";
            Assert.AreEqual(expPosit, board.GetString, "En passant capture not correct move.");

            // en passant back
            board.Back();
            expPosit = ".......k" + // position before capture en passant
                       "........" +
                       "........" +
                       "........" +
                       "pP......" +
                       "........" +
                       "........" +
                       "...K....";
            Assert.AreEqual(expPosit, board.GetString, "Back after en passant capture not correct.");
            Assert.AreEqual(Helper.FileCharToFile('b'), board.EnPassantFile, "En passant file wrong after 1st back.");
            Assert.AreEqual(3, board.EnPassantRank, "En passant rank wrong after 1st back.");

            board.Back();
            Assert.AreEqual(position, board.GetString, "2nd back after en passant capture not correct.");
            Assert.AreEqual(0, board.EnPassantFile, "En passant file wrong after 2nd back.");
            Assert.AreEqual(0, board.EnPassantRank, "En passant rank wrong after 2nd back.");
        }

        // -------------------------------------------------------------------
        // Castling tests
        // -------------------------------------------------------------------

        [TestMethod]
        public void CastlingRightTest_WhenKingOrRookMoved_ThenRightFalse()
        {
            MoveGenerator generator = new MoveGenerator();
            Board board = new Board(generator);
            string position = "r...k..r" +
                              "p......." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "P......." +
                              "R...K..R";
            board.SetPosition(position);

            Assert.AreEqual(true, board.CastlingRightWhiteQueenSide);
            Assert.AreEqual(true, board.CastlingRightWhiteKingSide);
            Assert.AreEqual(true, board.CastlingRightBlackQueenSide);
            Assert.AreEqual(true, board.CastlingRightBlackKingSide);

            board.Move("h1g1");
            Assert.AreEqual(true, board.CastlingRightWhiteQueenSide);
            Assert.AreEqual(false, board.CastlingRightWhiteKingSide);

            board.Move("a1c1");
            Assert.AreEqual(false, board.CastlingRightWhiteQueenSide);
            Assert.AreEqual(false, board.CastlingRightWhiteKingSide);

            board.Move("e8f8");
            Assert.AreEqual(false, board.CastlingRightWhiteQueenSide);
            Assert.AreEqual(false, board.CastlingRightWhiteKingSide);
            Assert.AreEqual(false, board.CastlingRightBlackQueenSide);
            Assert.AreEqual(false, board.CastlingRightBlackKingSide);
        }

        [TestMethod]
        public void MoveTest_WhenKingSideCastling_ThenCorrectMove_White()
        {
            MoveGenerator generator = new MoveGenerator();
            Board board = new Board(generator);
            string position = "r...k..r" +
                              "p......." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "P......." +
                              "R...K..R";
            board.SetPosition(position);

            board.Move("e1g1");

            string expecPos = "r...k..r" +
                              "p......." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "P......." +
                              "R....RK.";
            Assert.AreEqual(expecPos, board.GetString, "White King Side Castling not correct.");

            board.Back();

            Assert.AreEqual(position, board.GetString, "White King Side Castling: back not correct.");
        }

        [TestMethod]
        public void MoveTest_WhenQueenSideCastling_ThenCorrectMove_White()
        {
            MoveGenerator generator = new MoveGenerator();
            Board board = new Board(generator);
            string position = "r...k..r" +
                              "p......." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "P......." +
                              "R...K..R";
            board.SetPosition(position);

            board.Move("e1c1");

            string expecPos = "r...k..r" +
                              "p......." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "P......." +
                              "..KR...R";
            Assert.AreEqual(expecPos, board.GetString, "White Queen Side Castling not correct.");

            board.Back();

            Assert.AreEqual(position, board.GetString, "White Queen Side Castling: back not correct.");
        }

        [TestMethod]
        public void MoveTest_WhenKingSideCastling_ThenCorrectMove_Black()
        {
            MoveGenerator generator = new MoveGenerator();
            Board board = new Board(generator);
            string position = "r...k..r" +
                              "p......." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "P......." +
                              "R...K..R";
            board.SetPosition(position);

            board.Move("e8g8");

            string expecPos = "r....rk." +
                              "p......." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "P......." +
                              "R...K..R";
            Assert.AreEqual(expecPos, board.GetString, "Black King Side Castling not correct.");

            board.Back();

            Assert.AreEqual(position, board.GetString, "Black King Side Castling: back not correct.");
        }

        [TestMethod]
        public void MoveTest_WhenQueenSideCastling_ThenCorrectMove_Black()
        {
            MoveGenerator generator = new MoveGenerator();
            Board board = new Board(generator);
            string position = "r...k..r" +
                              "p......." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "P......." +
                              "R...K..R";
            board.SetPosition(position);

            board.Move("e8c8");

            string expecPos = "..kr...r" +
                              "p......." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "P......." +
                              "R...K..R";
            Assert.AreEqual(expecPos, board.GetString, "Black Queen Side Castling not correct.");

            board.Back();

            Assert.AreEqual(position, board.GetString, "Black Queen Side Castling: back not correct.");
        }
    }
}