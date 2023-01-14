using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MantaChessEngine;
using MantaCommon;

namespace MantaChessEngineTest
{
    [TestClass]
    public class BoardTest
    {
        [TestMethod]
        public void GetSetPieceTest_WhenSetPieceRookToD8_ThenGetPieceD8ShouldReturnRook()
        {
            var target = new Board();
            target.SetPiece(Piece.MakePiece(PieceType.Rook, ChessColor.White), Square.D8);
            char piece = target.GetPiece(Square.D8).Symbol;
            Assert.AreEqual('R', piece);
        }

        [TestMethod]
        public void GetSetPieceTest_WhenSetPieceRookTo48_ThenGetPiece48ShouldReturnRook()
        {
            var target = new Board();
            target.SetPiece(Piece.MakePiece(PieceType.Rook, ChessColor.White), Square.D8);
            char piece = target.GetPiece(Square.D8).Symbol;
            Assert.AreEqual('R', piece);
        }

        [TestMethod]
        public void GetPiece_WhenNewBoard_ThenAllPositionsEmpty()
        {
            var target = new Board();
            Piece piece = target.GetPiece(Square.D8);
            Assert.AreEqual(null, piece);
        }

        [TestMethod]
        public void InitPosition_WhenInitializedPosition_ThenPiecesAtInitPosition()
        {
            var target = new Board();
            target.SetInitialPosition();

            Assert.AreEqual(new Rook(ChessColor.White), target.GetPiece(Square.A1));
            Assert.AreEqual(new Knight(ChessColor.White), target.GetPiece(Square.B1));
            Assert.AreEqual(new Bishop(ChessColor.White), target.GetPiece(Square.C1));
            Assert.AreEqual(new Queen(ChessColor.White), target.GetPiece(Square.D1));
            Assert.AreEqual(new King(ChessColor.White), target.GetPiece(Square.E1));
            Assert.AreEqual(new Bishop(ChessColor.White), target.GetPiece(Square.F1));
            Assert.AreEqual(new Knight(ChessColor.White), target.GetPiece(Square.G1));
            Assert.AreEqual(new Rook(ChessColor.White), target.GetPiece(Square.H1));

            Assert.AreEqual(new Pawn(ChessColor.White), target.GetPiece(Square.B2)); // white pawn
            Assert.AreEqual(null, target.GetPiece(Square.C3)); // empty
            Assert.AreEqual(null, target.GetPiece(Square.D4)); // empty
            Assert.AreEqual(null, target.GetPiece(Square.E5)); // empty
            Assert.AreEqual(null, target.GetPiece(Square.F6)); // empty
            Assert.AreEqual(new Pawn(ChessColor.Black), target.GetPiece(Square.G7)); // black pawn

            Assert.AreEqual(new Rook(ChessColor.Black), target.GetPiece(Square.A8));
            Assert.AreEqual(new Knight(ChessColor.Black), target.GetPiece(Square.B8));
            Assert.AreEqual(new Bishop(ChessColor.Black), target.GetPiece(Square.C8));
            Assert.AreEqual(new Queen(ChessColor.Black), target.GetPiece(Square.D8));
            Assert.AreEqual(new King(ChessColor.Black), target.GetPiece(Square.E8));
            Assert.AreEqual(new Bishop(ChessColor.Black), target.GetPiece(Square.F8));
            Assert.AreEqual(new Knight(ChessColor.Black), target.GetPiece(Square.G8));
            Assert.AreEqual(new Rook(ChessColor.Black), target.GetPiece(Square.H8));

            Assert.AreEqual(Square.NoSquare, target.BoardState.LastEnPassantSquare);
            Assert.AreEqual(true, target.BoardState.LastCastlingRightWhiteKingSide);  // Castling white  
            Assert.AreEqual(true, target.BoardState.LastCastlingRightWhiteQueenSide); // 
            Assert.AreEqual(true, target.BoardState.LastCastlingRightBlackKingSide);  // castling black
            Assert.AreEqual(true, target.BoardState.LastCastlingRightBlackQueenSide); // 
        }

        [TestMethod]
        public void MoveTest_WhenPawnMovesNormal_ThenNewPositionOk()
        {
            Board target = new Board();
            target.SetInitialPosition();

            target.Move(new NormalMove(Piece.MakePiece(PieceType.Pawn, ChessColor.White), Square.E2, Square.E4, null));
            Assert.AreEqual(null, target.GetPiece(Square.E2));
            Assert.AreEqual(new Pawn(ChessColor.White), target.GetPiece(Square.E4));
            Assert.AreEqual(ChessColor.Black, target.BoardState.SideToMove);
        }

        [TestMethod]
        public void MoveTest_WhenPawnMovesNormalAndMoveIsOfTypeMove_ThenNewPositionOk()
        {
            Board target = new Board();
            target.SetInitialPosition();
            target.Move(new NormalMove(Piece.MakePiece(PieceType.Pawn, ChessColor.White), Square.E2, Square.E4, null));

            Assert.AreEqual(null, target.GetPiece(Square.E2));
            Assert.AreEqual(new Pawn(ChessColor.White), target.GetPiece(Square.E4));
            Assert.AreEqual(ChessColor.Black, target.BoardState.SideToMove);
        }

        [TestMethod]
        public void MoveTest_WhenQueenCapturesPiece_ThenNewPositionOk()
        {
            Board target = new Board();
            target.SetInitialPosition();
            target.SetPiece(null, Square.D2);

            target.Move(new NormalMove(new Queen(ChessColor.White), Square.D1, Square.D7, Piece.MakePiece(PieceType.Pawn, ChessColor.Black)));
            Assert.AreEqual(null, target.GetPiece(Square.D1));
            Assert.AreEqual(new Queen(ChessColor.White), target.GetPiece(Square.D7));
            Assert.AreEqual(ChessColor.Black, target.BoardState.SideToMove);

            Assert.AreEqual(1, target.BoardState.Moves.Count);
            Assert.AreEqual(new NormalMove(new Queen(ChessColor.White), Square.D1, Square.D7, Piece.MakePiece(PieceType.Pawn, ChessColor.Black)), target.BoardState.Moves[0]);
        }

        [TestMethod]
        public void MoveTest_WhenPawnMovesTwoFields_ThenEnPassantFieldSet_Black()
        {
            Board board = new Board();
            string position = ".......k" +
                              "p......." +
                              "........" +
                              ".P......" +
                              "........" +
                              "........" +
                              "........" +
                              "...K....";
            board.SetPosition(position);
            board.Move(new NormalMove(Piece.MakePiece(PieceType.Pawn, ChessColor.Black), Square.A7, Square.A5, null));
            Assert.AreEqual(Square.A6, board.BoardState.LastEnPassantSquare);
        }

        [TestMethod]
        public void MoveTest_WhenPawnMovesTwoFields_ThenEnPassantFieldSet_White()
        {
            Board board = new Board();
            string position = ".......k" +
                              "........" +
                              "........" +
                              "........" +
                              "p......." +
                              "........" +
                              ".P......" +
                              "...K....";
            board.SetPosition(position);
            board.Move(new NormalMove(Piece.MakePiece(PieceType.Pawn, ChessColor.White), Square.B2, Square.B4, null));
            Assert.AreEqual(Square.B3, board.BoardState.LastEnPassantSquare);
        }

        [TestMethod]
        public void MoveTest_WhenBlackCapturesEnPassant_ThenMoveCorrect_BlackMoves()
        {
            Board board = new Board();
            string position = ".......k" +
                              "........" +
                              "........" +
                              "........" +
                              "p......." +
                              "........" +
                              ".P......" +
                              "...K....";
            board.SetPosition(position);
            board.Move(new NormalMove(Piece.MakePiece(PieceType.Pawn, ChessColor.White), Square.B2, Square.B4, null));

            board.Move(new EnPassantCaptureMove(Piece.MakePiece(PieceType.Pawn, ChessColor.Black), Square.A4, Square.B3, Piece.MakePiece(PieceType.Pawn, ChessColor.White))); // capture en passant

            string expPosit = ".......k" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              ".p......" +
                              "........" +
                              "...K....";
            Assert.AreEqual(expPosit, board.GetPositionString, "En passant capture not correct move.");
            Assert.AreEqual(board.BoardState.Moves[1], new EnPassantCaptureMove(Piece.MakePiece(PieceType.Pawn, ChessColor.Black), Square.A4, Square.B3, Piece.MakePiece(PieceType.Pawn, ChessColor.White)));
        }

        [TestMethod]
        public void MoveTest_WhenWhiteCapturesEnPassant_ThenMoveCorrect_WhiteMoves()
        {
            Board board = new Board();
            string position = ".......k" +
                              ".p......" +
                              "........" +
                              "P......." +
                              "........" +
                              "........" +
                              "........" +
                              "...K....";
            board.SetPosition(position);
            board.Move(new NormalMove(Piece.MakePiece(PieceType.Pawn, ChessColor.Black), Square.B7, Square.B5, null));

            board.Move(new EnPassantCaptureMove(Piece.MakePiece(PieceType.Pawn, ChessColor.White), Square.A5, Square.B6, Piece.MakePiece(PieceType.Pawn, ChessColor.Black))); // capture en passant

            string expPosit = ".......k" +
                              "........" +
                              ".P......" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "...K....";
            Assert.AreEqual(expPosit, board.GetPositionString, "En passant capture not correct move.");
            Assert.AreEqual(board.BoardState.Moves[1], new EnPassantCaptureMove(Piece.MakePiece(PieceType.Pawn, ChessColor.White), Square.A5, Square.B6, Piece.MakePiece(PieceType.Pawn, ChessColor.Black)));
        }

        [TestMethod]
        public void GetColorTest()
        {
            var target = new Board();
            target.SetInitialPosition();
            Assert.AreEqual(ChessColor.White, target.GetColor(Square.E2));
            Assert.AreEqual(ChessColor.Empty, target.GetColor(Square.E3));
            Assert.AreEqual(ChessColor.Black, target.GetColor(Square.E7));
            Assert.AreEqual(ChessColor.Empty, target.GetColor(Square.E5));
        }

        [TestMethod]
        public void GetStringTest_WhenInitPos_ThenCorrect()
        {
            var target = new Board();
            target.SetInitialPosition();

            string boardString = target.GetPositionString;
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
            var target = new Board();
            target.SetInitialPosition();

            string boardString = target.GetPrintString;
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
            Board target = new Board();
            target.SetInitialPosition();
            target.Move(new NormalMove(Piece.MakePiece(PieceType.Pawn, ChessColor.White), Square.E2, Square.E4, null));
            target.Move(new NormalMove(Piece.MakePiece(PieceType.Pawn, ChessColor.Black), Square.E7, Square.E5, null));

            target.Back();
            string expectedString = "rnbqkbnr" +
                                    "pppppppp" +
                                    "........" +
                                    "........" +
                                    "....P..." +
                                    "........" +
                                    "PPPP.PPP" +
                                    "RNBQKBNR";
            Assert.AreEqual(expectedString, target.GetPositionString);
            Assert.AreEqual(ChessColor.Black, target.BoardState.SideToMove);
            Assert.AreEqual(Square.E3, target.BoardState.LastEnPassantSquare, "en passant square wrong after 1st back");

            target.Back();
            expectedString = "rnbqkbnr" +
                             "pppppppp" +
                             "........" +
                             "........" +
                             "........" +
                             "........" +
                             "PPPPPPPP" +
                             "RNBQKBNR";
            Assert.AreEqual(expectedString, target.GetPositionString);
            Assert.AreEqual(ChessColor.White, target.BoardState.SideToMove);
            Assert.AreEqual(Square.NoSquare, target.BoardState.LastEnPassantSquare, "en passant square wrong after 2dn back");
        }

        [TestMethod]
        public void MoveTest_WhenBackAfterEnPassant_ThenMoveCorrect()
        {
            // init move: white pawn moves two fields and black captures en passant
            Board board = new Board();
            string position = ".......k" +
                              "........" +
                              "........" +
                              "........" +
                              "p......." +
                              "........" +
                              ".P......" +
                              "...K....";
            board.SetPosition(position);
            board.Move(new NormalMove(Piece.MakePiece(PieceType.Pawn, ChessColor.White), Square.B2, Square.B4, null));
            board.Move(new EnPassantCaptureMove(Piece.MakePiece(PieceType.Pawn, ChessColor.Black), Square.A4, Square.B3, Piece.MakePiece(PieceType.Pawn, ChessColor.White))); // capture en passant

            string expPosit = ".......k" + // position after capture en passant
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              ".p......" +
                              "........" +
                              "...K....";
            Assert.AreEqual(expPosit, board.GetPositionString, "En passant capture not correct move.");
             
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
            Assert.AreEqual(expPosit, board.GetPositionString, "Back after en passant capture not correct.");
            Assert.AreEqual(Square.B3, board.BoardState.LastEnPassantSquare, "En passant square wrong after 1st back.");

            board.Back();
            Assert.AreEqual(position, board.GetPositionString, "2nd back after en passant capture not correct.");
            Assert.AreEqual(Square.NoSquare, board.BoardState.LastEnPassantSquare, "En passant square wrong after 2nd back.");
        }

        // -------------------------------------------------------------------
        // Castling tests
        // -------------------------------------------------------------------

        [TestMethod]
        public void CastlingRightTest_WhenKingOrRookMoved_ThenRightFalse()
        {
            Board board = new Board();
            string position = "r...k..r" +
                              "p......." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "P......." +
                              "R...K..R";
            board.SetPosition(position);

            Assert.AreEqual(true, board.BoardState.LastCastlingRightWhiteQueenSide);
            Assert.AreEqual(true, board.BoardState.LastCastlingRightWhiteKingSide);
            Assert.AreEqual(true, board.BoardState.LastCastlingRightBlackQueenSide);
            Assert.AreEqual(true, board.BoardState.LastCastlingRightBlackKingSide);

            board.Move(new NormalMove(Piece.MakePiece(PieceType.Rook, ChessColor.White), Square.H1, Square.G1, null));
            Assert.AreEqual(true, board.BoardState.LastCastlingRightWhiteQueenSide);
            Assert.AreEqual(false, board.BoardState.LastCastlingRightWhiteKingSide);

            board.Move(new NormalMove(Piece.MakePiece(PieceType.Rook, ChessColor.White), Square.A1, Square.C1, null));
            Assert.AreEqual(false, board.BoardState.LastCastlingRightWhiteQueenSide);
            Assert.AreEqual(false, board.BoardState.LastCastlingRightWhiteKingSide);

            board.Move(new NormalMove(Piece.MakePiece(PieceType.King, ChessColor.Black), Square.E8, Square.F8, null));
            Assert.AreEqual(false, board.BoardState.LastCastlingRightWhiteQueenSide);
            Assert.AreEqual(false, board.BoardState.LastCastlingRightWhiteKingSide);
            Assert.AreEqual(false, board.BoardState.LastCastlingRightBlackQueenSide);
            Assert.AreEqual(false, board.BoardState.LastCastlingRightBlackKingSide);
        }

        [TestMethod]
        public void MoveTest_WhenKingSideCastling_ThenCorrectMove_White()
        {
            Board board = new Board();
            string position = "r...k..r" +
                              "p......." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "P......." +
                              "R...K..R";
            board.SetPosition(position);

            board.Move(new CastlingMove(MantaChessEngine.CastlingType.WhiteKingSide, new King(ChessColor.White)));

            string expecPos = "r...k..r" +
                              "p......." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "P......." +
                              "R....RK.";
            Assert.AreEqual(expecPos, board.GetPositionString, "White King Side Castling not correct.");

            board.Back();

            Assert.AreEqual(position, board.GetPositionString, "White King Side Castling: back not correct.");
            Assert.AreEqual(true, board.BoardState.LastCastlingRightWhiteKingSide, "castling right must be true after back.");
            Assert.AreEqual(true, board.BoardState.LastCastlingRightWhiteQueenSide, "castling right must be true after back.");
        }

        [TestMethod]
        public void MoveTest_WhenQueenSideCastling_ThenCorrectMove_White()
        {
            Board board = new Board();
            string position = "r...k..r" +
                              "p......." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "P......." +
                              "R...K..R";
            board.SetPosition(position);

            board.Move(new CastlingMove(MantaChessEngine.CastlingType.WhiteQueenSide, new King(ChessColor.White)));

            string expecPos = "r...k..r" +
                              "p......." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "P......." +
                              "..KR...R";
            Assert.AreEqual(expecPos, board.GetPositionString, "White Queen Side Castling not correct.");

            board.Back();

            Assert.AreEqual(position, board.GetPositionString, "White Queen Side Castling: back not correct.");
            Assert.AreEqual(true, board.BoardState.LastCastlingRightWhiteKingSide, "castling right must be true after back.");
            Assert.AreEqual(true, board.BoardState.LastCastlingRightWhiteQueenSide, "castling right must be true after back.");
        }

        [TestMethod]
        public void MoveTest_WhenKingSideCastling_ThenCorrectMove_Black()
        {
            Board board = new Board();
            string position = "r...k..r" +
                              "p......." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "P......." +
                              "R...K..R";
            board.SetPosition(position);

            board.Move(new CastlingMove(MantaChessEngine.CastlingType.BlackKingSide, new King(ChessColor.Black)));

            string expecPos = "r....rk." +
                              "p......." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "P......." +
                              "R...K..R";
            Assert.AreEqual(expecPos, board.GetPositionString, "Black King Side Castling not correct.");

            board.Back();

            Assert.AreEqual(position, board.GetPositionString, "Black King Side Castling: back not correct.");
            Assert.AreEqual(true, board.BoardState.LastCastlingRightBlackKingSide, "castling right must be true after back.");
            Assert.AreEqual(true, board.BoardState.LastCastlingRightBlackQueenSide, "castling right must be true after back.");
        }

        [TestMethod]
        public void MoveTest_WhenQueenSideCastling_ThenCorrectMove_Black()
        {
            Board board = new Board();
            string position = "r...k..r" +
                              "p......." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "P......." +
                              "R...K..R";
            board.SetPosition(position);

            board.Move(new CastlingMove(MantaChessEngine.CastlingType.BlackQueenSide, new King(ChessColor.Black)));

            string expecPos = "..kr...r" +
                              "p......." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "P......." +
                              "R...K..R";
            Assert.AreEqual(expecPos, board.GetPositionString, "Black Queen Side Castling not correct.");
            
            board.Back();

            Assert.AreEqual(position, board.GetPositionString, "Black Queen Side Castling: back not correct.");
            Assert.AreEqual(true, board.BoardState.LastCastlingRightBlackKingSide, "castling right must be true after back.");
            Assert.AreEqual(true, board.BoardState.LastCastlingRightBlackQueenSide, "castling right must be true after back.");
        }

        // -------------------------------------------------------------------
        // Promotion tests
        // -------------------------------------------------------------------

        [TestMethod]
        public void MoveTest_WhenWhitePromotion_ThenCorrectMove()
        {
            Board board = new Board();
            string position = "....k..." +
                              "P......." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "....K...";
            board.SetPosition(position);

            board.Move(new PromotionMove(Piece.MakePiece(PieceType.Pawn, ChessColor.White), Square.A7, Square.A8, null, PieceType.Queen)); 

            string expecPos = "Q...k..." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "....K...";
            Assert.AreEqual(expecPos, board.GetPositionString, "White straight promotion not correct.");

            board.Back();

            Assert.AreEqual(position, board.GetPositionString, "White straight promotion: back not correct.");
        }

        [TestMethod]
        public void MoveTest_WhenMinorWhitePromotion_ThenCorrectMove()
        {
            Board board = new Board();
            string position = "....k..." +
                              "P......." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "....K...";
            board.SetPosition(position);

            board.Move(new PromotionMove(Piece.MakePiece(PieceType.Pawn, ChessColor.White), Square.A7, Square.A8, null, PieceType.Rook));

            string expecPos = "R...k..." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "....K...";
            Assert.AreEqual(expecPos, board.GetPositionString, "White straight minor (rook) promotion not correct.");

            board.Back();

            Assert.AreEqual(position, board.GetPositionString, "White straight minor (rook) promotion: back not correct.");
        }

        [TestMethod]
        public void MoveTest_WhenWhitePromotionWithCapture_ThenCorrectMove()
        {
            Board board = new Board();
            string position = ".r..k..." +
                              "P......." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "....K...";
            board.SetPosition(position);

            board.Move(new PromotionMove(Piece.MakePiece(PieceType.Pawn, ChessColor.White), Square.A7, Square.B8, Piece.MakePiece(PieceType.Rook, ChessColor.Black), PieceType.Queen));

            string expecPos = ".Q..k..." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "....K...";
            Assert.AreEqual(expecPos, board.GetPositionString, "White promotion with capture not correct.");

            board.Back();

            Assert.AreEqual(position, board.GetPositionString, "White promotion with capture: back not correct.");
        }

        [TestMethod]
        public void MoveTest_WhenBlackPromotion_ThenCorrectMove()
        {
            Board board = new Board();
            string position = "....k..." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "p......." +
                              "....K...";
            board.SetPosition(position);

            board.Move(new PromotionMove(Piece.MakePiece(PieceType.Pawn, ChessColor.Black), Square.A2, Square.A1, null, PieceType.Queen));

            string expecPos = "....k..." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "q...K...";
            Assert.AreEqual(expecPos, board.GetPositionString, "Black straight promotion not correct.");

            board.Back();

            Assert.AreEqual(position, board.GetPositionString, "Black straight promotion: back not correct.");
        }

        [TestMethod]
        public void MoveTest_WhenBlackPromotionWithCapture_ThenCorrectMove()
        {
            Board board = new Board();
            string position = "....k..." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "p......." +
                              ".R..K...";
            board.SetPosition(position);

            board.Move(new PromotionMove(Piece.MakePiece(PieceType.Pawn, ChessColor.Black), Square.A2, Square.B1, Piece.MakePiece(PieceType.Rook, ChessColor.White), PieceType.Queen));

            string expecPos = "....k..." +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              ".q..K...";
            Assert.AreEqual(expecPos, board.GetPositionString, "Black promotion with capture not correct.");

            board.Back();

            Assert.AreEqual(position, board.GetPositionString, "Black promotion with capture: back not correct.");
        }
        
    }
}