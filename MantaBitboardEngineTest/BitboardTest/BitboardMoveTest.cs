using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MantaChessEngine;
using MantaBitboardEngine;

namespace MantaBitboardEngineTest
{
    [TestClass]
    public class BitboardMoveTest
    {
        [TestMethod]
        public void GetSetPieceTest_WhenSetPieceRookToD8_ThenGetPieceD8ShouldReturnRook()
        {
            var target = new Bitboards();
            target.Initialize();
            target.SetPiece(BitColor.White, BitPieceType.Rook, Square.D8);
            
            var piece = target.GetPiece(Square.D8);
            Assert.AreEqual(BitPieceType.Rook, piece.Piece);
            Assert.AreEqual(BitColor.White, piece.Color);
        }

       [TestMethod]
        public void GetPiece_WhenNewBoard_ThenAllPositionsEmpty()
        {
            var target = new Bitboards();
            target.Initialize();
            var piece = target.GetPiece(Square.D8);
            Assert.AreEqual(BitPieceType.Empty, piece.Piece);
            Assert.AreEqual(BitColor.Empty, piece.Color);
        }

       [TestMethod]
        public void InitPosition_WhenInitializedPosition_ThenPiecesAtInitPosition()
        {
            var target = new Bitboards();
            target.Initialize();
            target.SetInitialPosition();

            Assert.AreEqual(BitPieceType.Rook,  target.GetPiece(Square.A1).Piece);
            Assert.AreEqual(BitColor.White, target.GetPiece(Square.A1).Color);

            Assert.AreEqual(BitPieceType.Knight, target.GetPiece(Square.B1).Piece);
            Assert.AreEqual(BitColor.White, target.GetPiece(Square.B1).Color);

            Assert.AreEqual(BitPieceType.Bishop, target.GetPiece(Square.C1).Piece);
            Assert.AreEqual(BitColor.White, target.GetPiece(Square.C1).Color);

            Assert.AreEqual(BitPieceType.Queen, target.GetPiece(Square.D1).Piece);
            Assert.AreEqual(BitColor.White, target.GetPiece(Square.D1).Color);

            Assert.AreEqual(BitPieceType.King, target.GetPiece(Square.E1).Piece);
            Assert.AreEqual(BitColor.White, target.GetPiece(Square.E1).Color);

            Assert.AreEqual(BitPieceType.Bishop, target.GetPiece(Square.F1).Piece);
            Assert.AreEqual(BitColor.White, target.GetPiece(Square.F1).Color);

            Assert.AreEqual(BitPieceType.Knight, target.GetPiece(Square.G1).Piece);
            Assert.AreEqual(BitColor.White, target.GetPiece(Square.G1).Color);

            Assert.AreEqual(BitPieceType.Rook, target.GetPiece(Square.H1).Piece);
            Assert.AreEqual(BitColor.White, target.GetPiece(Square.H1).Color);

            Assert.AreEqual(BitPieceType.Pawn, target.GetPiece(Square.B2).Piece); // white pawn
            Assert.AreEqual(BitColor.White, target.GetPiece(Square.B2).Color);


            Assert.AreEqual(BitPieceType.Empty, target.GetPiece(Square.C3).Piece); // empty
            Assert.AreEqual(BitColor.Empty, target.GetPiece(Square.C3).Color);

            Assert.AreEqual(BitPieceType.Empty, target.GetPiece(Square.D4).Piece); // empty
            Assert.AreEqual(BitColor.Empty, target.GetPiece(Square.D4).Color);

            Assert.AreEqual(BitPieceType.Empty, target.GetPiece(Square.E5).Piece); // empty
            Assert.AreEqual(BitColor.Empty, target.GetPiece(Square.E5).Color);

            Assert.AreEqual(BitPieceType.Empty, target.GetPiece(Square.F6).Piece); // empty
            Assert.AreEqual(BitColor.Empty, target.GetPiece(Square.F6).Color);


            Assert.AreEqual(BitPieceType.Pawn, target.GetPiece(Square.G7).Piece); // black pawn
            Assert.AreEqual(BitColor.Black, target.GetPiece(Square.G7).Color);

            Assert.AreEqual(BitPieceType.Rook, target.GetPiece(Square.A8).Piece);
            Assert.AreEqual(BitColor.Black, target.GetPiece(Square.A8).Color);

            Assert.AreEqual(BitPieceType.Knight, target.GetPiece(Square.B8).Piece);
            Assert.AreEqual(BitColor.Black, target.GetPiece(Square.B8).Color);

            Assert.AreEqual(BitPieceType.Bishop, target.GetPiece(Square.C8).Piece);
            Assert.AreEqual(BitColor.Black, target.GetPiece(Square.C8).Color);

            Assert.AreEqual(BitPieceType.Queen, target.GetPiece(Square.D8).Piece);
            Assert.AreEqual(BitColor.Black, target.GetPiece(Square.D8).Color);

            Assert.AreEqual(BitPieceType.King, target.GetPiece(Square.E8).Piece);
            Assert.AreEqual(BitColor.Black, target.GetPiece(Square.E8).Color);

            Assert.AreEqual(BitPieceType.Bishop, target.GetPiece(Square.F8).Piece);
            Assert.AreEqual(BitColor.Black, target.GetPiece(Square.F8).Color);

            Assert.AreEqual(BitPieceType.Knight, target.GetPiece(Square.G8).Piece);
            Assert.AreEqual(BitColor.Black, target.GetPiece(Square.G8).Color);

            Assert.AreEqual(BitPieceType.Rook, target.GetPiece(Square.H8).Piece);
            Assert.AreEqual(BitColor.Black, target.GetPiece(Square.H8).Color);

            Assert.AreEqual(Square.NoSquare, target.BoardState.LastEnPassantSquare);
            Assert.AreEqual(true, target.BoardState.LastCastlingRightWhiteKingSide);  // Castling white  
            Assert.AreEqual(true, target.BoardState.LastCastlingRightWhiteQueenSide); // 
            Assert.AreEqual(true, target.BoardState.LastCastlingRightBlackKingSide);  // castling black
            Assert.AreEqual(true, target.BoardState.LastCastlingRightBlackQueenSide); // 
        }

       [TestMethod]
        public void MoveTest_WhenPawnMovesNormal_ThenNewPositionOk()
        {
            var target = new Bitboards();
            target.Initialize();
            target.SetInitialPosition();

            target.Move(BitMove.CreateMove(BitPieceType.Pawn, Square.E2, Square.E4, BitPieceType.Empty, BitColor.White, 0));
            
            Assert.AreEqual(BitPieceType.Empty, target.GetPiece(Square.E2).Piece);
            Assert.AreEqual(BitColor.Empty, target.GetPiece(Square.E2).Color);

            Assert.AreEqual(BitPieceType.Pawn, target.GetPiece(Square.E4).Piece);
            Assert.AreEqual(BitColor.White, target.GetPiece(Square.E4).Color);

            Assert.AreEqual(BitColor.Black, target.BoardState.SideToMove);
        }

        [TestMethod]
        public void MoveTest_WhenQueenCapturesPiece_ThenNewPositionOk()
        {
            var target = new Bitboards();
            target.Initialize();
            target.SetInitialPosition();
            target.RemovePiece(Square.D2);

            var capture = BitMove.CreateCapture(BitPieceType.Queen, Square.D1, Square.D7, BitPieceType.Pawn, Square.D7, BitPieceType.Empty, BitColor.White, 0);
            target.Move(capture);
            
            Assert.AreEqual(BitPieceType.Empty, target.GetPiece(Square.D1).Piece);
            Assert.AreEqual(BitColor.Empty, target.GetPiece(Square.D1).Color);

            Assert.AreEqual(BitPieceType.Queen, target.GetPiece(Square.D7).Piece);
            Assert.AreEqual(BitColor.White, target.GetPiece(Square.D7).Color);

            Assert.AreEqual(BitColor.Black, target.BoardState.SideToMove);

            Assert.AreEqual(1, target.BoardState.Moves.Count);
            Assert.AreEqual(capture, target.BoardState.Moves[0]);
        }

       [TestMethod]
        public void MoveTest_WhenPawnMovesTwoFields_ThenEnPassantFieldSet_Black()
        {
            var target = new Bitboards();
            target.Initialize();
            target.SetPosition(".......k" +
                               "p......." +
                               "........" +
                               ".P......" +
                               "........" +
                               "........" +
                               "........" +
                               "...K....");

            target.Move(BitMove.CreateMove(BitPieceType.Pawn, Square.A7, Square.A5, BitPieceType.Empty, BitColor.Black, 0));

            Assert.AreEqual(Square.A6, target.BoardState.LastEnPassantSquare);
        }

       [TestMethod]
        public void MoveTest_WhenPawnMovesTwoFields_ThenEnPassantFieldSet_White()
        {
            var target = new Bitboards();
            target.Initialize();
            target.SetPosition(".......k" +
                               "........" +
                               "........" +
                               "........" +
                               "p......." +
                               "........" +
                               ".P......" +
                               "...K....");

            target.Move(BitMove.CreateMove(BitPieceType.Pawn, Square.B2, Square.B4, BitPieceType.Empty, BitColor.White, 0));

            Assert.AreEqual(Square.B3, target.BoardState.LastEnPassantSquare);
        }

        [TestMethod]
        public void MoveTest_WhenBlackCapturesEnPassant_ThenMoveCorrect_BlackMoves()
        {
            var target = new Bitboards();
            target.Initialize();
            target.SetPosition(".......k" +
                               "........" +
                               "........" +
                               "........" +
                               "p......." +
                               "........" +
                               ".P......" +
                               "...K....");

            target.Move(BitMove.CreateMove(BitPieceType.Pawn, Square.B2, Square.B4, BitPieceType.Empty, BitColor.White, 0));

            var enPassantCapture = BitMove.CreateCapture(BitPieceType.Pawn, Square.A4, Square.B3, BitPieceType.Pawn, Square.B4, BitPieceType.Empty, BitColor.Black, 0);
            target.Move(enPassantCapture);

            string expPosit = ".......k" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              ".p......" +
                              "........" +
                              "...K....";
            Assert.AreEqual(expPosit, target.GetPositionString, "En passant capture not correct move.");
            Assert.AreEqual(target.BoardState.Moves[1], enPassantCapture);
        }

       [TestMethod]
        public void MoveTest_WhenWhiteCapturesEnPassant_ThenMoveCorrect_WhiteMoves()
        {
            var target = new Bitboards();
            target.Initialize();
            target.SetPosition(".......k" +
                               ".p......" +
                               "........" +
                               "P......." +
                               "........" +
                               "........" +
                               "........" +
                               "...K....");

            target.Move(BitMove.CreateMove(BitPieceType.Pawn, Square.B7, Square.B5, BitPieceType.Empty, BitColor.Black, 0));

            var enpassantCapture = BitMove.CreateCapture(BitPieceType.Pawn, Square.A5, Square.B6, BitPieceType.Pawn, Square.B5, BitPieceType.Empty, BitColor.White, 0);
            target.Move(enpassantCapture); // capture en passant

            string expPosit = ".......k" +
                              "........" +
                              ".P......" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "...K....";
            Assert.AreEqual(expPosit, target.GetPositionString, "En passant capture not correct move.");
            Assert.AreEqual(target.BoardState.Moves[1], enpassantCapture);
        }

       [TestMethod]
        public void GetColorTest()
        {
            var target = new Bitboards();
            target.Initialize();
            target.SetInitialPosition();
            Assert.AreEqual(BitColor.White, target.GetPiece(Square.E2).Color);
            Assert.AreEqual(BitColor.Empty, target.GetPiece(Square.E3).Color);
            Assert.AreEqual(BitColor.Black, target.GetPiece(Square.E7).Color);
            Assert.AreEqual(BitColor.Empty, target.GetPiece(Square.E5).Color);
        }

       [TestMethod]
        public void GetStringTest_WhenInitPos_ThenCorrect()
        {
            var target = new Bitboards();
            target.Initialize();
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
            var target = new Bitboards();
            target.Initialize();
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
            var target = new Bitboards();
            target.Initialize();
            target.SetInitialPosition();
            target.Move(BitMove.CreateMove(BitPieceType.Pawn, Square.E2, Square.E4, BitPieceType.Empty, BitColor.White, 0));
            target.Move(BitMove.CreateMove(BitPieceType.Pawn, Square.E7, Square.E5, BitPieceType.Empty, BitColor.Black, 0));


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
            Assert.AreEqual(Definitions.ChessColor.Black, target.BoardState.SideToMove);
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
            Assert.AreEqual(Definitions.ChessColor.White, target.BoardState.SideToMove);
            Assert.AreEqual(Square.NoSquare, target.BoardState.LastEnPassantSquare, "en passant square wrong after 2dn back");
        }

        [TestMethod]
        public void MoveTest_WhenBackAfterEnPassant_ThenMoveCorrect()
        {
            // init move: white pawn moves two fields and black captures en passant
            var target = new Bitboards();
            target.Initialize();
            var position = ".......k" +
                           "........" +
                           "........" +
                           "........" +
                           "p......." +
                           "........" +
                           ".P......" +
                           "...K....";
            target.SetPosition(position);

            target.Move(BitMove.CreateMove(BitPieceType.Pawn, Square.B2, Square.B4, BitPieceType.Empty, BitColor.White, 0));
            target.Move(BitMove.CreateCapture(BitPieceType.Pawn, Square.A4, Square.B3, BitPieceType.Pawn, Square.B4, BitPieceType.Empty, BitColor.White, 0));

            string expPosit = ".......k" + // position after capture en passant
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              ".p......" +
                              "........" +
                              "...K....";
            Assert.AreEqual(expPosit, target.GetPositionString, "En passant capture not correct move.");

            // en passant back
            target.Back();
            expPosit = ".......k" + // position before capture en passant
                       "........" +
                       "........" +
                       "........" +
                       "pP......" +
                       "........" +
                       "........" +
                       "...K....";
            Assert.AreEqual(expPosit, target.GetPositionString, "Back after en passant capture not correct.");
            Assert.AreEqual(Square.B3, target.BoardState.LastEnPassantSquare, "En passant square wrong after 1st back.");

            target.Back();
            Assert.AreEqual(position, target.GetPositionString, "2nd back after en passant capture not correct.");
            Assert.AreEqual(0, target.BoardState.LastEnPassantSquare, "En passant square wrong after 2nd back.");
        }

        // -------------------------------------------------------------------
        // Castling tests
        // -------------------------------------------------------------------

        [TestMethod]
        public void CastlingRightTest_WhenKingOrRookMoved_ThenRightFalse()
        {
            var target = new Bitboards();
            target.Initialize();
            target.SetPosition("r...k..r" +
                               "p......." +
                               "........" +
                               "........" +
                               "........" +
                               "........" +
                               "P......." +
                               "R...K..R");

            Assert.AreEqual(true, target.BoardState.LastCastlingRightWhiteQueenSide);
            Assert.AreEqual(true, target.BoardState.LastCastlingRightWhiteKingSide);
            Assert.AreEqual(true, target.BoardState.LastCastlingRightBlackQueenSide);
            Assert.AreEqual(true, target.BoardState.LastCastlingRightBlackKingSide);

            target.Move(BitMove.CreateMove(BitPieceType.Rook, Square.H1, Square.G1, BitPieceType.Empty, BitColor.White, 0));

            Assert.AreEqual(true, target.BoardState.LastCastlingRightWhiteQueenSide);
            Assert.AreEqual(false, target.BoardState.LastCastlingRightWhiteKingSide);

            target.Move(BitMove.CreateMove(BitPieceType.Rook, Square.A1, Square.C1, BitPieceType.Empty, BitColor.White, 0));
            Assert.AreEqual(false, target.BoardState.LastCastlingRightWhiteQueenSide);
            Assert.AreEqual(false, target.BoardState.LastCastlingRightWhiteKingSide);

            target.Move(BitMove.CreateMove(BitPieceType.King, Square.E8, Square.F8, BitPieceType.Empty, BitColor.Black, 0));
            Assert.AreEqual(false, target.BoardState.LastCastlingRightWhiteQueenSide);
            Assert.AreEqual(false, target.BoardState.LastCastlingRightWhiteKingSide);
            Assert.AreEqual(false, target.BoardState.LastCastlingRightBlackQueenSide);
            Assert.AreEqual(false, target.BoardState.LastCastlingRightBlackKingSide);
        }

        [TestMethod, Ignore]
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

            board.Move(new CastlingMove(MantaChessEngine.CastlingType.WhiteKingSide, new King(Definitions.ChessColor.White)));

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

        [TestMethod, Ignore]
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

            board.Move(new CastlingMove(MantaChessEngine.CastlingType.WhiteQueenSide, new King(Definitions.ChessColor.White)));

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

        [TestMethod, Ignore]
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

            board.Move(new CastlingMove(MantaChessEngine.CastlingType.BlackKingSide, new King(Definitions.ChessColor.Black)));

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

        [TestMethod, Ignore]
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

            board.Move(new CastlingMove(MantaChessEngine.CastlingType.BlackQueenSide, new King(Definitions.ChessColor.Black)));

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

        [TestMethod, Ignore]
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

            board.Move(new PromotionMove(Piece.MakePiece('P'), 'a', 7, 'a', 8, null, Definitions.QUEEN)); 

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

        [TestMethod, Ignore]
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

            board.Move(new PromotionMove(Piece.MakePiece('P'), 'a', 7, 'a', 8, null, Definitions.ROOK));

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

        [TestMethod, Ignore]
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

            board.Move(new PromotionMove(Piece.MakePiece('P'), 'a', 7, 'b', 8, Piece.MakePiece('r'), Definitions.QUEEN));

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

        [TestMethod, Ignore]
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

            board.Move(new PromotionMove(Piece.MakePiece('p'), 'a', 2, 'a', 1, null, Definitions.QUEEN));

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

        [TestMethod, Ignore]
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

            board.Move(new PromotionMove(Piece.MakePiece('p'), 'a', 2, 'b', 1, Piece.MakePiece('R'), Definitions.QUEEN));

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