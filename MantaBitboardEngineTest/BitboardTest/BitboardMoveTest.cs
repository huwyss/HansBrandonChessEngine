using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MantaChessEngine;
using MantaBitboardEngine;
using MantaCommon;
using Moq;

namespace MantaBitboardEngineTest
{
    [TestClass]
    public class BitboardMoveTest
    {
        private Bitboards _target;

        [TestInitialize]
        public void Setup()
        {
            _target = new Bitboards(new Mock<IHashtable>().Object);
        }

        [TestMethod]
        public void GetSetPieceTest_WhenSetPieceRookToD8_ThenGetPieceD8ShouldReturnRook()
        {
            _target.SetPiece(ChessColor.White, PieceType.Rook, Square.D8);
            
            var piece = _target.GetPiece(Square.D8);
            Assert.AreEqual(PieceType.Rook, piece.Piece);
            Assert.AreEqual(ChessColor.White, piece.Color);
        }

       [TestMethod]
        public void GetPiece_WhenNewBoard_ThenAllPositionsEmpty()
        {
            var piece = _target.GetPiece(Square.D8);
            Assert.AreEqual(PieceType.Empty, piece.Piece);
            Assert.AreEqual(ChessColor.Empty, piece.Color);
        }

       [TestMethod]
        public void InitPosition_WhenInitializedPosition_ThenPiecesAtInitPosition()
        {
            _target.SetInitialPosition();

            Assert.AreEqual(PieceType.Rook,  _target.GetPiece(Square.A1).Piece);
            Assert.AreEqual(ChessColor.White, _target.GetPiece(Square.A1).Color);

            Assert.AreEqual(PieceType.Knight, _target.GetPiece(Square.B1).Piece);
            Assert.AreEqual(ChessColor.White, _target.GetPiece(Square.B1).Color);

            Assert.AreEqual(PieceType.Bishop, _target.GetPiece(Square.C1).Piece);
            Assert.AreEqual(ChessColor.White, _target.GetPiece(Square.C1).Color);

            Assert.AreEqual(PieceType.Queen, _target.GetPiece(Square.D1).Piece);
            Assert.AreEqual(ChessColor.White, _target.GetPiece(Square.D1).Color);

            Assert.AreEqual(PieceType.King, _target.GetPiece(Square.E1).Piece);
            Assert.AreEqual(ChessColor.White, _target.GetPiece(Square.E1).Color);

            Assert.AreEqual(PieceType.Bishop, _target.GetPiece(Square.F1).Piece);
            Assert.AreEqual(ChessColor.White, _target.GetPiece(Square.F1).Color);

            Assert.AreEqual(PieceType.Knight, _target.GetPiece(Square.G1).Piece);
            Assert.AreEqual(ChessColor.White, _target.GetPiece(Square.G1).Color);

            Assert.AreEqual(PieceType.Rook, _target.GetPiece(Square.H1).Piece);
            Assert.AreEqual(ChessColor.White, _target.GetPiece(Square.H1).Color);

            Assert.AreEqual(PieceType.Pawn, _target.GetPiece(Square.B2).Piece); // white pawn
            Assert.AreEqual(ChessColor.White, _target.GetPiece(Square.B2).Color);


            Assert.AreEqual(PieceType.Empty, _target.GetPiece(Square.C3).Piece); // empty
            Assert.AreEqual(ChessColor.Empty, _target.GetPiece(Square.C3).Color);

            Assert.AreEqual(PieceType.Empty, _target.GetPiece(Square.D4).Piece); // empty
            Assert.AreEqual(ChessColor.Empty, _target.GetPiece(Square.D4).Color);

            Assert.AreEqual(PieceType.Empty, _target.GetPiece(Square.E5).Piece); // empty
            Assert.AreEqual(ChessColor.Empty, _target.GetPiece(Square.E5).Color);

            Assert.AreEqual(PieceType.Empty, _target.GetPiece(Square.F6).Piece); // empty
            Assert.AreEqual(ChessColor.Empty, _target.GetPiece(Square.F6).Color);


            Assert.AreEqual(PieceType.Pawn, _target.GetPiece(Square.G7).Piece); // black pawn
            Assert.AreEqual(ChessColor.Black, _target.GetPiece(Square.G7).Color);

            Assert.AreEqual(PieceType.Rook, _target.GetPiece(Square.A8).Piece);
            Assert.AreEqual(ChessColor.Black, _target.GetPiece(Square.A8).Color);

            Assert.AreEqual(PieceType.Knight, _target.GetPiece(Square.B8).Piece);
            Assert.AreEqual(ChessColor.Black, _target.GetPiece(Square.B8).Color);

            Assert.AreEqual(PieceType.Bishop, _target.GetPiece(Square.C8).Piece);
            Assert.AreEqual(ChessColor.Black, _target.GetPiece(Square.C8).Color);

            Assert.AreEqual(PieceType.Queen, _target.GetPiece(Square.D8).Piece);
            Assert.AreEqual(ChessColor.Black, _target.GetPiece(Square.D8).Color);

            Assert.AreEqual(PieceType.King, _target.GetPiece(Square.E8).Piece);
            Assert.AreEqual(ChessColor.Black, _target.GetPiece(Square.E8).Color);

            Assert.AreEqual(PieceType.Bishop, _target.GetPiece(Square.F8).Piece);
            Assert.AreEqual(ChessColor.Black, _target.GetPiece(Square.F8).Color);

            Assert.AreEqual(PieceType.Knight, _target.GetPiece(Square.G8).Piece);
            Assert.AreEqual(ChessColor.Black, _target.GetPiece(Square.G8).Color);

            Assert.AreEqual(PieceType.Rook, _target.GetPiece(Square.H8).Piece);
            Assert.AreEqual(ChessColor.Black, _target.GetPiece(Square.H8).Color);

            Assert.AreEqual(Square.NoSquare, _target.BoardState.LastEnPassantSquare);
            Assert.AreEqual(true, _target.BoardState.LastCastlingRightWhiteKingSide);  // Castling white  
            Assert.AreEqual(true, _target.BoardState.LastCastlingRightWhiteQueenSide); // 
            Assert.AreEqual(true, _target.BoardState.LastCastlingRightBlackKingSide);  // castling black
            Assert.AreEqual(true, _target.BoardState.LastCastlingRightBlackQueenSide); // 
        }

       [TestMethod]
        public void MoveTest_WhenPawnMovesNormal_ThenNewPositionOk()
        {
            _target.SetInitialPosition();

            _target.Move(BitMove.CreateMove(PieceType.Pawn, Square.E2, Square.E4, PieceType.Empty, ChessColor.White, 0));
            
            Assert.AreEqual(PieceType.Empty, _target.GetPiece(Square.E2).Piece);
            Assert.AreEqual(ChessColor.Empty, _target.GetPiece(Square.E2).Color);

            Assert.AreEqual(PieceType.Pawn, _target.GetPiece(Square.E4).Piece);
            Assert.AreEqual(ChessColor.White, _target.GetPiece(Square.E4).Color);

            Assert.AreEqual(ChessColor.Black, _target.BoardState.SideToMove);
        }

        [TestMethod]
        public void MoveTest_WhenQueenCapturesPiece_ThenNewPositionOk()
        {
            _target.SetInitialPosition();
            _target.RemovePiece(Square.D2);

            var capture = BitMove.CreateCapture(PieceType.Queen, Square.D1, Square.D7, PieceType.Pawn, Square.D7, PieceType.Empty, ChessColor.White, 0);
            _target.Move(capture);
            
            Assert.AreEqual(PieceType.Empty, _target.GetPiece(Square.D1).Piece);
            Assert.AreEqual(ChessColor.Empty, _target.GetPiece(Square.D1).Color);

            Assert.AreEqual(PieceType.Queen, _target.GetPiece(Square.D7).Piece);
            Assert.AreEqual(ChessColor.White, _target.GetPiece(Square.D7).Color);

            Assert.AreEqual(ChessColor.Black, _target.BoardState.SideToMove);

            Assert.AreEqual(1, _target.BoardState.Moves.Count);
            Assert.AreEqual(capture, _target.BoardState.Moves[0]);
        }

       [TestMethod]
        public void MoveTest_WhenPawnMovesTwoFields_ThenEnPassantFieldSet_Black()
        {
            _target.SetPosition(".......k" +
                               "p......." +
                               "........" +
                               ".P......" +
                               "........" +
                               "........" +
                               "........" +
                               "...K....");

            _target.Move(BitMove.CreateMove(PieceType.Pawn, Square.A7, Square.A5, PieceType.Empty, ChessColor.Black, 0));

            Assert.AreEqual(Square.A6, _target.BoardState.LastEnPassantSquare);
        }

       [TestMethod]
        public void MoveTest_WhenPawnMovesTwoFields_ThenEnPassantFieldSet_White()
        {
            _target.SetPosition(".......k" +
                               "........" +
                               "........" +
                               "........" +
                               "p......." +
                               "........" +
                               ".P......" +
                               "...K....");

            _target.Move(BitMove.CreateMove(PieceType.Pawn, Square.B2, Square.B4, PieceType.Empty, ChessColor.White, 0));

            Assert.AreEqual(Square.B3, _target.BoardState.LastEnPassantSquare);
        }

        [TestMethod]
        public void MoveTest_WhenBlackCapturesEnPassant_ThenMoveCorrect_BlackMoves()
        {
            _target.SetPosition(".......k" +
                               "........" +
                               "........" +
                               "........" +
                               "p......." +
                               "........" +
                               ".P......" +
                               "...K....");

            _target.Move(BitMove.CreateMove(PieceType.Pawn, Square.B2, Square.B4, PieceType.Empty, ChessColor.White, 0));

            var enPassantCapture = BitMove.CreateCapture(PieceType.Pawn, Square.A4, Square.B3, PieceType.Pawn, Square.B4, PieceType.Empty, ChessColor.Black, 0);
            _target.Move(enPassantCapture);

            string expPosit = ".......k" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              ".p......" +
                              "........" +
                              "...K....";
            Assert.AreEqual(expPosit, _target.GetPositionString, "En passant capture not correct move.");
            Assert.AreEqual(_target.BoardState.Moves[1], enPassantCapture);
        }

       [TestMethod]
        public void MoveTest_WhenWhiteCapturesEnPassant_ThenMoveCorrect_WhiteMoves()
        {
            _target.SetPosition(".......k" +
                               ".p......" +
                               "........" +
                               "P......." +
                               "........" +
                               "........" +
                               "........" +
                               "...K....");

            _target.Move(BitMove.CreateMove(PieceType.Pawn, Square.B7, Square.B5, PieceType.Empty, ChessColor.Black, 0));

            var enpassantCapture = BitMove.CreateCapture(PieceType.Pawn, Square.A5, Square.B6, PieceType.Pawn, Square.B5, PieceType.Empty, ChessColor.White, 0);
            _target.Move(enpassantCapture); // capture en passant

            string expPosit = ".......k" +
                              "........" +
                              ".P......" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "...K....";
            Assert.AreEqual(expPosit, _target.GetPositionString, "En passant capture not correct move.");
            Assert.AreEqual(_target.BoardState.Moves[1], enpassantCapture);
        }

       [TestMethod]
        public void GetColorTest()
        {
            _target.SetInitialPosition();
            Assert.AreEqual(ChessColor.White, _target.GetPiece(Square.E2).Color);
            Assert.AreEqual(ChessColor.Empty, _target.GetPiece(Square.E3).Color);
            Assert.AreEqual(ChessColor.Black, _target.GetPiece(Square.E7).Color);
            Assert.AreEqual(ChessColor.Empty, _target.GetPiece(Square.E5).Color);
        }

       [TestMethod]
        public void GetStringTest_WhenInitPos_ThenCorrect()
        {
            _target.SetInitialPosition();

            string boardString = _target.GetPositionString;
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
            _target.SetInitialPosition();

            string boardString = _target.GetPrintString;
            string expectedString =
                "    a  b  c  d  e  f  g  h\n" +
                "  +------------------------+\n" +
                "8 | r  n  b  q  k  b  n  r | 8\n" +
                "  |                        |\n" +
                "7 | p  p  p  p  p  p  p  p | 7\n" +
                "  |                        |\n" +
                "6 | .  .  .  .  .  .  .  . | 6\n" +
                "  |                        |\n" +
                "5 | .  .  .  .  .  .  .  . | 5\n" +
                "  |                        |\n" +
                "4 | .  .  .  .  .  .  .  . | 4\n" +
                "  |                        |\n" +
                "3 | .  .  .  .  .  .  .  . | 3\n" +
                "  |                        |\n" +
                "2 | P  P  P  P  P  P  P  P | 2\n" +
                "  |                        |\n" +
                "1 | R  N  B  Q  K  B  N  R | 1\n" +
                "  +------------------------+\n" +
                "    a  b  c  d  e  f  g  h\n";

            Assert.AreEqual(expectedString, boardString);
        }
        
        // -------------------------------------------------------------------
        // Back tests
        // -------------------------------------------------------------------

        [TestMethod]
        public void BackTest_WhenWhiteAndBlackMovesDone_ThenGoBackToInitPosition()
        {
            _target.SetInitialPosition();
            _target.Move(BitMove.CreateMove(PieceType.Pawn, Square.E2, Square.E4, PieceType.Empty, ChessColor.White, 0));
            _target.Move(BitMove.CreateMove(PieceType.Pawn, Square.E7, Square.E5, PieceType.Empty, ChessColor.Black, 0));


            _target.Back();
            string expectedString = "rnbqkbnr" +
                                    "pppppppp" +
                                    "........" +
                                    "........" +
                                    "....P..." +
                                    "........" +
                                    "PPPP.PPP" +
                                    "RNBQKBNR";
            Assert.AreEqual(expectedString, _target.GetPositionString);
            Assert.AreEqual(ChessColor.Black, _target.BoardState.SideToMove);
            Assert.AreEqual(Square.E3, _target.BoardState.LastEnPassantSquare, "en passant square wrong after 1st back");

            _target.Back();
            expectedString = "rnbqkbnr" +
                             "pppppppp" +
                             "........" +
                             "........" +
                             "........" +
                             "........" +
                             "PPPPPPPP" +
                             "RNBQKBNR";
            Assert.AreEqual(expectedString, _target.GetPositionString);
            Assert.AreEqual(ChessColor.White, _target.BoardState.SideToMove);
            Assert.AreEqual(Square.NoSquare, _target.BoardState.LastEnPassantSquare, "en passant square wrong after 2dn back");
        }

        [TestMethod]
        public void MoveTest_WhenBackAfterEnPassant_ThenMoveCorrect()
        {
            // init move: white pawn moves two fields and black captures en passant
            var position = ".......k" +
                           "........" +
                           "........" +
                           "........" +
                           "p......." +
                           "........" +
                           ".P......" +
                           "...K....";
            _target.SetPosition(position);

            _target.Move(BitMove.CreateMove(PieceType.Pawn, Square.B2, Square.B4, PieceType.Empty, ChessColor.White, 0));
            _target.Move(BitMove.CreateCapture(PieceType.Pawn, Square.A4, Square.B3, PieceType.Pawn, Square.B4, PieceType.Empty, ChessColor.Black, 0));

            string expPosit = ".......k" + // position after capture en passant
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              ".p......" +
                              "........" +
                              "...K....";
            Assert.AreEqual(expPosit, _target.GetPositionString, "En passant capture not correct move.");

            // en passant back
            _target.Back();
            expPosit = ".......k" + // position before capture en passant
                       "........" +
                       "........" +
                       "........" +
                       "pP......" +
                       "........" +
                       "........" +
                       "...K....";
            Assert.AreEqual(expPosit, _target.GetPositionString, "Back after en passant capture not correct.");
            Assert.AreEqual(Square.B3, _target.BoardState.LastEnPassantSquare, "En passant square wrong after 1st back.");

            _target.Back();
            Assert.AreEqual(position, _target.GetPositionString, "2nd back after en passant capture not correct.");
            Assert.AreEqual(Square.NoSquare, _target.BoardState.LastEnPassantSquare, "En passant square wrong after 2nd back.");
        }

        [TestMethod]
        public void BackOfCaptureMoveTest()
        {
            _target.SetInitialPosition();
            _target.Move(BitMove.CreateMove(PieceType.Pawn, Square.E2, Square.E4, PieceType.Empty, ChessColor.White, 0));
            _target.Move(BitMove.CreateMove(PieceType.Pawn, Square.D7, Square.D5, PieceType.Empty, ChessColor.Black, 0));
            _target.Move(BitMove.CreateCapture(PieceType.Pawn, Square.E4, Square.D5, PieceType.Pawn, Square.D5, PieceType.Empty, ChessColor.White, 0));


            _target.Back();
            string expectedString = "rnbqkbnr" +
                                    "ppp.pppp" +
                                    "........" +
                                    "...p...." +
                                    "....P..." +
                                    "........" +
                                    "PPPP.PPP" +
                                    "RNBQKBNR";
            Assert.AreEqual(expectedString, _target.GetPositionString);
            Assert.AreEqual(ChessColor.White, _target.BoardState.SideToMove);
            Assert.AreEqual(Square.D6, _target.BoardState.LastEnPassantSquare, "en passant square wrong after 1st back");

            _target.Back();
            expectedString = "rnbqkbnr" +
                             "pppppppp" +
                             "........" +
                             "........" +
                             "....P..." +
                             "........" +
                             "PPPP.PPP" +
                             "RNBQKBNR";
            Assert.AreEqual(expectedString, _target.GetPositionString);
            Assert.AreEqual(ChessColor.Black, _target.BoardState.SideToMove);
            Assert.AreEqual(Square.E3, _target.BoardState.LastEnPassantSquare, "en passant square wrong after 2dn back");

            _target.Back();
            expectedString = "rnbqkbnr" +
                             "pppppppp" +
                             "........" +
                             "........" +
                             "........" +
                             "........" +
                             "PPPPPPPP" +
                             "RNBQKBNR";
            Assert.AreEqual(expectedString, _target.GetPositionString);
            Assert.AreEqual(ChessColor.White, _target.BoardState.SideToMove);
            Assert.AreEqual(Square.NoSquare, _target.BoardState.LastEnPassantSquare, "en passant square wrong after 3rd back");
        }

        // -------------------------------------------------------------------
        // Castling tests
        // -------------------------------------------------------------------

        [TestMethod]
        public void CastlingRightTest_WhenKingOrRookMoved_ThenRightFalse()
        {
            _target.SetPosition("r...k..r" +
                               "p......." +
                               "........" +
                               "........" +
                               "........" +
                               "........" +
                               "P......." +
                               "R...K..R");

            Assert.AreEqual(true, _target.BoardState.LastCastlingRightWhiteQueenSide);
            Assert.AreEqual(true, _target.BoardState.LastCastlingRightWhiteKingSide);
            Assert.AreEqual(true, _target.BoardState.LastCastlingRightBlackQueenSide);
            Assert.AreEqual(true, _target.BoardState.LastCastlingRightBlackKingSide);

            _target.Move(BitMove.CreateMove(PieceType.Rook, Square.H1, Square.G1, PieceType.Empty, ChessColor.White, 0));

            Assert.AreEqual(true, _target.BoardState.LastCastlingRightWhiteQueenSide);
            Assert.AreEqual(false, _target.BoardState.LastCastlingRightWhiteKingSide);

            _target.Move(BitMove.CreateMove(PieceType.Rook, Square.A1, Square.C1, PieceType.Empty, ChessColor.White, 0));
            Assert.AreEqual(false, _target.BoardState.LastCastlingRightWhiteQueenSide);
            Assert.AreEqual(false, _target.BoardState.LastCastlingRightWhiteKingSide);

            _target.Move(BitMove.CreateMove(PieceType.King, Square.E8, Square.F8, PieceType.Empty, ChessColor.Black, 0));
            Assert.AreEqual(false, _target.BoardState.LastCastlingRightWhiteQueenSide);
            Assert.AreEqual(false, _target.BoardState.LastCastlingRightWhiteKingSide);
            Assert.AreEqual(false, _target.BoardState.LastCastlingRightBlackQueenSide);
            Assert.AreEqual(false, _target.BoardState.LastCastlingRightBlackKingSide);
        }

        [TestMethod]
        public void MoveTest_WhenKingSideCastling_ThenCorrectMove_White()
        {
            string position = "r...k..r" +
                              "p......." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "P......." +
                              "R...K..R";
            _target.SetPosition(position);

            _target.Move(BitMove.CreateCastling(ChessColor.White, MantaCommon.CastlingType.KingSide, 0));
            string expecPos = "r...k..r" +
                              "p......." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "P......." +
                              "R....RK.";
            Assert.AreEqual(expecPos, _target.GetPositionString, "White King Side Castling not correct.");

            _target.Back();

            Assert.AreEqual(position, _target.GetPositionString, "White King Side Castling: back not correct.");
            Assert.IsTrue(_target.BoardState.LastCastlingRightWhiteKingSide, "castling right must be true after back.");
            Assert.IsTrue(_target.BoardState.LastCastlingRightWhiteQueenSide, "castling right must be true after back.");
        }

        [TestMethod]
        public void MoveTest_WhenQueenSideCastling_ThenCorrectMove_White()
        {
            string position = "r...k..r" +
                              "p......." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "P......." +
                              "R...K..R";
            _target.SetPosition(position);

            _target.Move(BitMove.CreateCastling(ChessColor.White, MantaCommon.CastlingType.QueenSide, 0));

            string expecPos = "r...k..r" +
                              "p......." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "P......." +
                              "..KR...R";
            Assert.AreEqual(expecPos, _target.GetPositionString, "White Queen Side Castling not correct.");

            _target.Back();

            Assert.AreEqual(position, _target.GetPositionString, "White Queen Side Castling: back not correct.");
            Assert.IsTrue(_target.BoardState.LastCastlingRightWhiteKingSide, "castling right must be true after back.");
            Assert.IsTrue(_target.BoardState.LastCastlingRightWhiteQueenSide, "castling right must be true after back.");
        }

        [TestMethod]
        public void MoveTest_WhenKingSideCastling_ThenCorrectMove_Black()
        {
            string position = "r...k..r" +
                              "p......." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "P......." +
                              "R...K..R";
            _target.SetPosition(position);

            _target.Move(BitMove.CreateCastling(ChessColor.Black, MantaCommon.CastlingType.KingSide, 0));

            string expecPos = "r....rk." +
                              "p......." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "P......." +
                              "R...K..R";
            Assert.AreEqual(expecPos, _target.GetPositionString, "Black King Side Castling not correct.");

            _target.Back();

            Assert.AreEqual(position, _target.GetPositionString, "Black King Side Castling: back not correct.");
            Assert.IsTrue(_target.BoardState.LastCastlingRightBlackKingSide, "castling right must be true after back.");
            Assert.IsTrue(_target.BoardState.LastCastlingRightBlackQueenSide, "castling right must be true after back.");
        }

        [TestMethod]
        public void MoveTest_WhenQueenSideCastling_ThenCorrectMove_Black()
        {
            string position = "r...k..r" +
                              "p......." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "P......." +
                              "R...K..R";
            _target.SetPosition(position);

            _target.Move(BitMove.CreateCastling(ChessColor.Black, MantaCommon.CastlingType.QueenSide, 0));

            string expecPos = "..kr...r" +
                              "p......." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "P......." +
                              "R...K..R";
            Assert.AreEqual(expecPos, _target.GetPositionString, "Black Queen Side Castling not correct.");

            _target.Back();

            Assert.AreEqual(position, _target.GetPositionString, "Black Queen Side Castling: back not correct.");
            Assert.IsTrue(_target.BoardState.LastCastlingRightBlackKingSide, "castling right must be true after back.");
            Assert.IsTrue(_target.BoardState.LastCastlingRightBlackQueenSide, "castling right must be true after back.");
        }

        // -------------------------------------------------------------------
        // Promotion tests
        // -------------------------------------------------------------------

        [TestMethod]
        public void MoveTest_WhenWhitePromotion_ThenCorrectMove()
        {
            string position = "....k..." +
                              "P......." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "....K...";
            _target.SetPosition(position);

            _target.Move(BitMove.CreateMove(PieceType.Pawn, Square.A7, Square.A8, PieceType.Queen, ChessColor.White, 0));

            string expecPos = "Q...k..." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "....K...";
            Assert.AreEqual(expecPos, _target.GetPositionString, "White straight promotion not correct.");

            _target.Back();

            Assert.AreEqual(position, _target.GetPositionString, "White straight promotion: back not correct.");
        }

        [TestMethod]
        public void MoveTest_WhenMinorWhitePromotion_ThenCorrectMove()
        {
            string position = "....k..." +
                              "P......." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "....K...";
            _target.SetPosition(position);

            _target.Move(BitMove.CreateMove(PieceType.Pawn, Square.A7, Square.A8, PieceType.Rook, ChessColor.White, 0));

            string expecPos = "R...k..." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "....K...";
            Assert.AreEqual(expecPos, _target.GetPositionString, "White straight minor (rook) promotion not correct.");

            _target.Back();

            Assert.AreEqual(position, _target.GetPositionString, "White straight minor (rook) promotion: back not correct.");
        }

        [TestMethod]
        public void MoveTest_WhenWhitePromotionWithCapture_ThenCorrectMove()
        {
            string position = ".r..k..." +
                              "P......." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "....K...";
            _target.SetPosition(position);

            _target.Move(BitMove.CreateCapture(PieceType.Pawn, Square.A7, Square.B8, PieceType.Rook, Square.B8, PieceType.Queen, ChessColor.White, 0));

            string expecPos = ".Q..k..." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "....K...";
            Assert.AreEqual(expecPos, _target.GetPositionString, "White promotion with capture not correct.");

            _target.Back();

            Assert.AreEqual(position, _target.GetPositionString, "White promotion with capture: back not correct.");
        }

        [TestMethod]
        public void MoveTest_WhenBlackPromotion_ThenCorrectMove()
        {
            string position = "....k..." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "p......." +
                              "....K...";
            _target.SetPosition(position);

            _target.Move(BitMove.CreateMove(PieceType.Pawn, Square.A2, Square.A1, PieceType.Queen, ChessColor.Black, 0));

            string expecPos = "....k..." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "q...K...";
            Assert.AreEqual(expecPos, _target.GetPositionString, "Black straight promotion not correct.");

            _target.Back();

            Assert.AreEqual(position, _target.GetPositionString, "Black straight promotion: back not correct.");
        }

        [TestMethod]
        public void MoveTest_WhenBlackPromotionWithCapture_ThenCorrectMove()
        {
            string position = "....k..." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "p......." +
                              ".R..K...";
            _target.SetPosition(position);

            _target.Move(BitMove.CreateCapture(PieceType.Pawn, Square.A2, Square.B1, PieceType.Rook, Square.B1, PieceType.Queen, ChessColor.Black, 0));

            string expecPos = "....k..." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              ".q..K...";
            Assert.AreEqual(expecPos, _target.GetPositionString, "Black promotion with capture not correct.");

            _target.Back();

            Assert.AreEqual(position, _target.GetPositionString, "Black promotion with capture: back not correct.");
        }
        
    }
}