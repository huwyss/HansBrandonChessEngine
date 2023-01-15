using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MantaChessEngine;
using MantaCommon;
using Moq;

namespace MantaChessEngineTest
{
    [TestClass]
    public class BoardTest
    {
        private Board _target;

        [TestInitialize]
        public void Setup()
        {
            _target = new Board(new Mock<IHashtable>().Object);
        }

        [TestMethod]
        public void GetSetPieceTest_WhenSetPieceRookToD8_ThenGetPieceD8ShouldReturnRook()
        {
            _target.SetPiece(Piece.MakePiece(PieceType.Rook, ChessColor.White), Square.D8);
            char piece = _target.GetPiece(Square.D8).Symbol;
            Assert.AreEqual('R', piece);
        }

        [TestMethod]
        public void GetSetPieceTest_WhenSetPieceRookTo48_ThenGetPiece48ShouldReturnRook()
        {
            _target.SetPiece(Piece.MakePiece(PieceType.Rook, ChessColor.White), Square.D8);
            char piece = _target.GetPiece(Square.D8).Symbol;
            Assert.AreEqual('R', piece);
        }

        [TestMethod]
        public void GetPiece_WhenNewBoard_ThenAllPositionsEmpty()
        {
            Piece piece = _target.GetPiece(Square.D8);
            Assert.AreEqual(null, piece);
        }

        [TestMethod]
        public void InitPosition_WhenInitializedPosition_ThenPiecesAtInitPosition()
        {
            _target.SetInitialPosition();

            Assert.AreEqual(new Rook(ChessColor.White), _target.GetPiece(Square.A1));
            Assert.AreEqual(new Knight(ChessColor.White), _target.GetPiece(Square.B1));
            Assert.AreEqual(new Bishop(ChessColor.White), _target.GetPiece(Square.C1));
            Assert.AreEqual(new Queen(ChessColor.White), _target.GetPiece(Square.D1));
            Assert.AreEqual(new King(ChessColor.White), _target.GetPiece(Square.E1));
            Assert.AreEqual(new Bishop(ChessColor.White), _target.GetPiece(Square.F1));
            Assert.AreEqual(new Knight(ChessColor.White), _target.GetPiece(Square.G1));
            Assert.AreEqual(new Rook(ChessColor.White), _target.GetPiece(Square.H1));

            Assert.AreEqual(new Pawn(ChessColor.White), _target.GetPiece(Square.B2)); // white pawn
            Assert.AreEqual(null, _target.GetPiece(Square.C3)); // empty
            Assert.AreEqual(null, _target.GetPiece(Square.D4)); // empty
            Assert.AreEqual(null, _target.GetPiece(Square.E5)); // empty
            Assert.AreEqual(null, _target.GetPiece(Square.F6)); // empty
            Assert.AreEqual(new Pawn(ChessColor.Black), _target.GetPiece(Square.G7)); // black pawn

            Assert.AreEqual(new Rook(ChessColor.Black), _target.GetPiece(Square.A8));
            Assert.AreEqual(new Knight(ChessColor.Black), _target.GetPiece(Square.B8));
            Assert.AreEqual(new Bishop(ChessColor.Black), _target.GetPiece(Square.C8));
            Assert.AreEqual(new Queen(ChessColor.Black), _target.GetPiece(Square.D8));
            Assert.AreEqual(new King(ChessColor.Black), _target.GetPiece(Square.E8));
            Assert.AreEqual(new Bishop(ChessColor.Black), _target.GetPiece(Square.F8));
            Assert.AreEqual(new Knight(ChessColor.Black), _target.GetPiece(Square.G8));
            Assert.AreEqual(new Rook(ChessColor.Black), _target.GetPiece(Square.H8));

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

            _target.Move(new NormalMove(Piece.MakePiece(PieceType.Pawn, ChessColor.White), Square.E2, Square.E4, null));
            Assert.AreEqual(null, _target.GetPiece(Square.E2));
            Assert.AreEqual(new Pawn(ChessColor.White), _target.GetPiece(Square.E4));
            Assert.AreEqual(ChessColor.Black, _target.BoardState.SideToMove);
        }

        [TestMethod]
        public void MoveTest_WhenPawnMovesNormalAndMoveIsOfTypeMove_ThenNewPositionOk()
        {
            _target.SetInitialPosition();
            _target.Move(new NormalMove(Piece.MakePiece(PieceType.Pawn, ChessColor.White), Square.E2, Square.E4, null));

            Assert.AreEqual(null, _target.GetPiece(Square.E2));
            Assert.AreEqual(new Pawn(ChessColor.White), _target.GetPiece(Square.E4));
            Assert.AreEqual(ChessColor.Black, _target.BoardState.SideToMove);
        }

        [TestMethod]
        public void MoveTest_WhenQueenCapturesPiece_ThenNewPositionOk()
        {
            _target.SetInitialPosition();
            _target.RemovePiece(Square.D2);

            _target.Move(new NormalMove(new Queen(ChessColor.White), Square.D1, Square.D7, Piece.MakePiece(PieceType.Pawn, ChessColor.Black)));
            Assert.AreEqual(null, _target.GetPiece(Square.D1));
            Assert.AreEqual(new Queen(ChessColor.White), _target.GetPiece(Square.D7));
            Assert.AreEqual(ChessColor.Black, _target.BoardState.SideToMove);

            Assert.AreEqual(1, _target.BoardState.Moves.Count);
            Assert.AreEqual(new NormalMove(new Queen(ChessColor.White), Square.D1, Square.D7, Piece.MakePiece(PieceType.Pawn, ChessColor.Black)), _target.BoardState.Moves[0]);
        }

        [TestMethod]
        public void MoveTest_WhenPawnMovesTwoFields_ThenEnPassantFieldSet_Black()
        {
            string position = ".......k" +
                              "p......." +
                              "........" +
                              ".P......" +
                              "........" +
                              "........" +
                              "........" +
                              "...K....";
            _target.SetPosition(position);
            _target.Move(new NormalMove(Piece.MakePiece(PieceType.Pawn, ChessColor.Black), Square.A7, Square.A5, null));
            Assert.AreEqual(Square.A6, _target.BoardState.LastEnPassantSquare);
        }

        [TestMethod]
        public void MoveTest_WhenPawnMovesTwoFields_ThenEnPassantFieldSet_White()
        {
            string position = ".......k" +
                              "........" +
                              "........" +
                              "........" +
                              "p......." +
                              "........" +
                              ".P......" +
                              "...K....";
            _target.SetPosition(position);
            _target.Move(new NormalMove(Piece.MakePiece(PieceType.Pawn, ChessColor.White), Square.B2, Square.B4, null));
            Assert.AreEqual(Square.B3, _target.BoardState.LastEnPassantSquare);
        }

        [TestMethod]
        public void MoveTest_WhenBlackCapturesEnPassant_ThenMoveCorrect_BlackMoves()
        {
            string position = ".......k" +
                              "........" +
                              "........" +
                              "........" +
                              "p......." +
                              "........" +
                              ".P......" +
                              "...K....";
            _target.SetPosition(position);
            _target.Move(new NormalMove(Piece.MakePiece(PieceType.Pawn, ChessColor.White), Square.B2, Square.B4, null));

            _target.Move(new EnPassantCaptureMove(Piece.MakePiece(PieceType.Pawn, ChessColor.Black), Square.A4, Square.B3, Piece.MakePiece(PieceType.Pawn, ChessColor.White))); // capture en passant

            string expPosit = ".......k" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              ".p......" +
                              "........" +
                              "...K....";
            Assert.AreEqual(expPosit, _target.GetPositionString, "En passant capture not correct move.");
            Assert.AreEqual(_target.BoardState.Moves[1], new EnPassantCaptureMove(Piece.MakePiece(PieceType.Pawn, ChessColor.Black), Square.A4, Square.B3, Piece.MakePiece(PieceType.Pawn, ChessColor.White)));
        }

        [TestMethod]
        public void MoveTest_WhenWhiteCapturesEnPassant_ThenMoveCorrect_WhiteMoves()
        {
            string position = ".......k" +
                              ".p......" +
                              "........" +
                              "P......." +
                              "........" +
                              "........" +
                              "........" +
                              "...K....";
            _target.SetPosition(position);
            _target.Move(new NormalMove(Piece.MakePiece(PieceType.Pawn, ChessColor.Black), Square.B7, Square.B5, null));

            _target.Move(new EnPassantCaptureMove(Piece.MakePiece(PieceType.Pawn, ChessColor.White), Square.A5, Square.B6, Piece.MakePiece(PieceType.Pawn, ChessColor.Black))); // capture en passant

            string expPosit = ".......k" +
                              "........" +
                              ".P......" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "...K....";
            Assert.AreEqual(expPosit, _target.GetPositionString, "En passant capture not correct move.");
            Assert.AreEqual(_target.BoardState.Moves[1], new EnPassantCaptureMove(Piece.MakePiece(PieceType.Pawn, ChessColor.White), Square.A5, Square.B6, Piece.MakePiece(PieceType.Pawn, ChessColor.Black)));
        }

        [TestMethod]
        public void GetColorTest()
        {
            _target.SetInitialPosition();
            Assert.AreEqual(ChessColor.White, _target.GetColor(Square.E2));
            Assert.AreEqual(ChessColor.Empty, _target.GetColor(Square.E3));
            Assert.AreEqual(ChessColor.Black, _target.GetColor(Square.E7));
            Assert.AreEqual(ChessColor.Empty, _target.GetColor(Square.E5));
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
            _target.Move(new NormalMove(Piece.MakePiece(PieceType.Pawn, ChessColor.White), Square.E2, Square.E4, null));
            _target.Move(new NormalMove(Piece.MakePiece(PieceType.Pawn, ChessColor.Black), Square.E7, Square.E5, null));

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
            string position = ".......k" +
                              "........" +
                              "........" +
                              "........" +
                              "p......." +
                              "........" +
                              ".P......" +
                              "...K....";
            _target.SetPosition(position);
            _target.Move(new NormalMove(Piece.MakePiece(PieceType.Pawn, ChessColor.White), Square.B2, Square.B4, null));
            _target.Move(new EnPassantCaptureMove(Piece.MakePiece(PieceType.Pawn, ChessColor.Black), Square.A4, Square.B3, Piece.MakePiece(PieceType.Pawn, ChessColor.White))); // capture en passant

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

        // -------------------------------------------------------------------
        // Castling tests
        // -------------------------------------------------------------------

        [TestMethod]
        public void CastlingRightTest_WhenKingOrRookMoved_ThenRightFalse()
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

            Assert.AreEqual(true, _target.BoardState.LastCastlingRightWhiteQueenSide);
            Assert.AreEqual(true, _target.BoardState.LastCastlingRightWhiteKingSide);
            Assert.AreEqual(true, _target.BoardState.LastCastlingRightBlackQueenSide);
            Assert.AreEqual(true, _target.BoardState.LastCastlingRightBlackKingSide);

            _target.Move(new NormalMove(Piece.MakePiece(PieceType.Rook, ChessColor.White), Square.H1, Square.G1, null));
            Assert.AreEqual(true, _target.BoardState.LastCastlingRightWhiteQueenSide);
            Assert.AreEqual(false, _target.BoardState.LastCastlingRightWhiteKingSide);

            _target.Move(new NormalMove(Piece.MakePiece(PieceType.Rook, ChessColor.White), Square.A1, Square.C1, null));
            Assert.AreEqual(false, _target.BoardState.LastCastlingRightWhiteQueenSide);
            Assert.AreEqual(false, _target.BoardState.LastCastlingRightWhiteKingSide);

            _target.Move(new NormalMove(Piece.MakePiece(PieceType.King, ChessColor.Black), Square.E8, Square.F8, null));
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

            _target.Move(new CastlingMove(MantaChessEngine.CastlingType.WhiteKingSide, new King(ChessColor.White)));

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
            Assert.AreEqual(true, _target.BoardState.LastCastlingRightWhiteKingSide, "castling right must be true after back.");
            Assert.AreEqual(true, _target.BoardState.LastCastlingRightWhiteQueenSide, "castling right must be true after back.");
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

            _target.Move(new CastlingMove(MantaChessEngine.CastlingType.WhiteQueenSide, new King(ChessColor.White)));

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
            Assert.AreEqual(true, _target.BoardState.LastCastlingRightWhiteKingSide, "castling right must be true after back.");
            Assert.AreEqual(true, _target.BoardState.LastCastlingRightWhiteQueenSide, "castling right must be true after back.");
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

            _target.Move(new CastlingMove(MantaChessEngine.CastlingType.BlackKingSide, new King(ChessColor.Black)));

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
            Assert.AreEqual(true, _target.BoardState.LastCastlingRightBlackKingSide, "castling right must be true after back.");
            Assert.AreEqual(true, _target.BoardState.LastCastlingRightBlackQueenSide, "castling right must be true after back.");
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

            _target.Move(new CastlingMove(MantaChessEngine.CastlingType.BlackQueenSide, new King(ChessColor.Black)));

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
            Assert.AreEqual(true, _target.BoardState.LastCastlingRightBlackKingSide, "castling right must be true after back.");
            Assert.AreEqual(true, _target.BoardState.LastCastlingRightBlackQueenSide, "castling right must be true after back.");
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

            _target.Move(new PromotionMove(Piece.MakePiece(PieceType.Pawn, ChessColor.White), Square.A7, Square.A8, null, PieceType.Queen)); 

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

            _target.Move(new PromotionMove(Piece.MakePiece(PieceType.Pawn, ChessColor.White), Square.A7, Square.A8, null, PieceType.Rook));

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

            _target.Move(new PromotionMove(Piece.MakePiece(PieceType.Pawn, ChessColor.White), Square.A7, Square.B8, Piece.MakePiece(PieceType.Rook, ChessColor.Black), PieceType.Queen));

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

            _target.Move(new PromotionMove(Piece.MakePiece(PieceType.Pawn, ChessColor.Black), Square.A2, Square.A1, null, PieceType.Queen));

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

            _target.Move(new PromotionMove(Piece.MakePiece(PieceType.Pawn, ChessColor.Black), Square.A2, Square.B1, Piece.MakePiece(PieceType.Rook, ChessColor.White), PieceType.Queen));

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