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

       [TestMethod, Ignore]
        public void InitPosition_WhenInitializedPosition_ThenPiecesAtInitPosition()
        {
            var target = new Board();
            target.SetInitialPosition();

            Assert.AreEqual(new Rook(Definitions.ChessColor.White), target.GetPiece(Helper.FileCharToFile('a'), 1));
            Assert.AreEqual(new Knight(Definitions.ChessColor.White), target.GetPiece(Helper.FileCharToFile('b'), 1));
            Assert.AreEqual(new Bishop(Definitions.ChessColor.White), target.GetPiece(Helper.FileCharToFile('c'), 1));
            Assert.AreEqual(new Queen(Definitions.ChessColor.White), target.GetPiece(Helper.FileCharToFile('d'), 1));
            Assert.AreEqual(new King(Definitions.ChessColor.White), target.GetPiece(Helper.FileCharToFile('e'), 1));
            Assert.AreEqual(new Bishop(Definitions.ChessColor.White), target.GetPiece(Helper.FileCharToFile('f'), 1));
            Assert.AreEqual(new Knight(Definitions.ChessColor.White), target.GetPiece(Helper.FileCharToFile('g'), 1));
            Assert.AreEqual(new Rook(Definitions.ChessColor.White), target.GetPiece(Helper.FileCharToFile('h'), 1));

            Assert.AreEqual(new Pawn(Definitions.ChessColor.White), target.GetPiece(Helper.FileCharToFile('b'), 2)); // white pawn
            Assert.AreEqual(null, target.GetPiece(Helper.FileCharToFile('c'), 3)); // empty
            Assert.AreEqual(null, target.GetPiece(Helper.FileCharToFile('d'), 4)); // empty
            Assert.AreEqual(null, target.GetPiece(Helper.FileCharToFile('e'), 5)); // empty
            Assert.AreEqual(null, target.GetPiece(Helper.FileCharToFile('f'), 6)); // empty
            Assert.AreEqual(new Pawn(Definitions.ChessColor.Black), target.GetPiece(Helper.FileCharToFile('g'), 7)); // black pawn

            Assert.AreEqual(new Rook(Definitions.ChessColor.Black), target.GetPiece(Helper.FileCharToFile('a'), 8));
            Assert.AreEqual(new Knight(Definitions.ChessColor.Black), target.GetPiece(Helper.FileCharToFile('b'), 8));
            Assert.AreEqual(new Bishop(Definitions.ChessColor.Black), target.GetPiece(Helper.FileCharToFile('c'), 8));
            Assert.AreEqual(new Queen(Definitions.ChessColor.Black), target.GetPiece(Helper.FileCharToFile('d'), 8));
            Assert.AreEqual(new King(Definitions.ChessColor.Black), target.GetPiece(Helper.FileCharToFile('e'), 8));
            Assert.AreEqual(new Bishop(Definitions.ChessColor.Black), target.GetPiece(Helper.FileCharToFile('f'), 8));
            Assert.AreEqual(new Knight(Definitions.ChessColor.Black), target.GetPiece(Helper.FileCharToFile('g'), 8));
            Assert.AreEqual(new Rook(Definitions.ChessColor.Black), target.GetPiece(Helper.FileCharToFile('h'), 8));

            Assert.AreEqual(0, target.BoardState.LastEnPassantFile);
            Assert.AreEqual(0, target.BoardState.LastEnPassantRank);
            Assert.AreEqual(true, target.BoardState.LastCastlingRightWhiteKingSide);  // Castling white  
            Assert.AreEqual(true, target.BoardState.LastCastlingRightWhiteQueenSide); // 
            Assert.AreEqual(true, target.BoardState.LastCastlingRightBlackKingSide);  // castling black
            Assert.AreEqual(true, target.BoardState.LastCastlingRightBlackQueenSide); // 
        }

       [TestMethod, Ignore]
        public void MoveTest_WhenPawnMovesNormal_ThenNewPositionOk()
        {
            Board target = new Board();
            target.SetInitialPosition();

            target.Move(new NormalMove(Piece.MakePiece('P'),'e',2,'e',4,null));
            Assert.AreEqual(null, target.GetPiece(Helper.FileCharToFile('e'), 2));
            Assert.AreEqual(new Pawn(Definitions.ChessColor.White), target.GetPiece(Helper.FileCharToFile('e'), 4));
            Assert.AreEqual(Definitions.ChessColor.Black, target.BoardState.SideToMove);
        }

       [TestMethod, Ignore]
        public void MoveTest_WhenPawnMovesNormalAndMoveIsOfTypeMove_ThenNewPositionOk()
        {
            Board target = new Board();
            target.SetInitialPosition();
            target.Move(new NormalMove(Piece.MakePiece('P'), 'e', 2, 'e', 4, null));

            Assert.AreEqual(null, target.GetPiece(Helper.FileCharToFile('e'), 2));
            Assert.AreEqual(new Pawn(Definitions.ChessColor.White), target.GetPiece(Helper.FileCharToFile('e'), 4));
            Assert.AreEqual(Definitions.ChessColor.Black, target.BoardState.SideToMove);
        }

       [TestMethod, Ignore]
        public void MoveTest_WhenQueenCapturesPiece_ThenNewPositionOk()
        {
            Board target = new Board();
            target.SetInitialPosition();
            target.SetPiece(null, Helper.FileCharToFile('d'), 2);

            target.Move(new NormalMove(new Queen(Definitions.ChessColor.White),'d',1,'d',7,Piece.MakePiece('p')));
            Assert.AreEqual(null, target.GetPiece(Helper.FileCharToFile('d'), 1));
            Assert.AreEqual(new Queen(Definitions.ChessColor.White), target.GetPiece(Helper.FileCharToFile('d'), 7));
            Assert.AreEqual(Definitions.ChessColor.Black, target.BoardState.SideToMove);

            Assert.AreEqual(1, target.BoardState.Moves.Count);
            Assert.AreEqual(new NormalMove(new Queen(Definitions.ChessColor.White), 4, 1, 4, 7, Piece.MakePiece('p')), target.BoardState.Moves[0]);
        }

       [TestMethod, Ignore]
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
            board.Move(new NormalMove(Piece.MakePiece('p'),'a',7,'a',5,null));
            Assert.AreEqual(Helper.FileCharToFile('a'), board.BoardState.LastEnPassantFile);
            Assert.AreEqual(6, board.BoardState.LastEnPassantRank);
        }

       [TestMethod, Ignore]
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
            board.Move(new NormalMove(Piece.MakePiece('P'),'b',2,'b',4,null));
            Assert.AreEqual(Helper.FileCharToFile('b'), board.BoardState.LastEnPassantFile);
            Assert.AreEqual(3, board.BoardState.LastEnPassantRank);
        }

       [TestMethod, Ignore]
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
            board.Move(new NormalMove(Piece.MakePiece('P'),'b',2,'b',4,null));

            board.Move(new EnPassantCaptureMove(Piece.MakePiece('p'),'a',4,'b',3,Piece.MakePiece('P'))); // capture en passant

            string expPosit = ".......k" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              ".p......" +
                              "........" +
                              "...K....";
            Assert.AreEqual(expPosit, board.GetPositionString, "En passant capture not correct move.");
            Assert.AreEqual(board.BoardState.Moves[1], new EnPassantCaptureMove(Piece.MakePiece('p'), 'a', 4, 'b', 3, Piece.MakePiece('P')));
        }

       [TestMethod, Ignore]
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
            board.Move(new NormalMove(Piece.MakePiece('p'),'b',7,'b',5,null));

            board.Move(new EnPassantCaptureMove(Piece.MakePiece('P'),'a',5,'b',6,Piece.MakePiece('p'))); // capture en passant

            string expPosit = ".......k" +
                              "........" +
                              ".P......" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "...K....";
            Assert.AreEqual(expPosit, board.GetPositionString, "En passant capture not correct move.");
            Assert.AreEqual(board.BoardState.Moves[1], new EnPassantCaptureMove(Piece.MakePiece('P'), 'a', 5, 'b', 6, Piece.MakePiece('p')));
        }

       [TestMethod, Ignore]
        public void GetColorTest()
        {
            var target = new Board();
            target.SetInitialPosition();
            Assert.AreEqual(Definitions.ChessColor.White, target.GetColor(5, 2));
            Assert.AreEqual(Definitions.ChessColor.Empty, target.GetColor(5, 3));
            Assert.AreEqual(Definitions.ChessColor.Black, target.GetColor(5, 7));
            Assert.AreEqual(Definitions.ChessColor.Empty, target.GetColor(5, 5));
        }

       [TestMethod, Ignore]
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

       [TestMethod, Ignore]
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

       [TestMethod, Ignore]
        public void BackTest_WhenWhiteAndBlackMovesDone_ThenGoBackToInitPosition()
        {
            Board target = new Board();
            target.SetInitialPosition();
            target.Move(new NormalMove(Piece.MakePiece('P'),'e',2,'e',4,null));
            target.Move(new NormalMove(Piece.MakePiece('p'),'e',7,'e',5,null));

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
            Assert.AreEqual(Helper.FileCharToFile('e'), target.BoardState.LastEnPassantFile, "en passant file wrong after 1st back");
            Assert.AreEqual(3, target.BoardState.LastEnPassantRank, "en passant rank wrong after 1st back");

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
            Assert.AreEqual(0, target.BoardState.LastEnPassantFile, "en passant file wrong after 2dn back");
            Assert.AreEqual(0, target.BoardState.LastEnPassantRank, "en passant rank wrong after 2dn back");
        }

       [TestMethod, Ignore]
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
            board.Move(new NormalMove(Piece.MakePiece('P'),'b',2,'b',4,null));
            board.Move(new EnPassantCaptureMove(Piece.MakePiece('p'),'a',4,'b',3, Piece.MakePiece('P'))); // capture en passant

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
            Assert.AreEqual(Helper.FileCharToFile('b'), board.BoardState.LastEnPassantFile, "En passant file wrong after 1st back.");
            Assert.AreEqual(3, board.BoardState.LastEnPassantRank, "En passant rank wrong after 1st back.");

            board.Back();
            Assert.AreEqual(position, board.GetPositionString, "2nd back after en passant capture not correct.");
            Assert.AreEqual(0, board.BoardState.LastEnPassantFile, "En passant file wrong after 2nd back.");
            Assert.AreEqual(0, board.BoardState.LastEnPassantRank, "En passant rank wrong after 2nd back.");
        }

        // -------------------------------------------------------------------
        // Castling tests
        // -------------------------------------------------------------------

       [TestMethod, Ignore]
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

            board.Move(new NormalMove(Piece.MakePiece('R'),'h',1,'g',1,null));
            Assert.AreEqual(true, board.BoardState.LastCastlingRightWhiteQueenSide);
            Assert.AreEqual(false, board.BoardState.LastCastlingRightWhiteKingSide);

            board.Move(new NormalMove(Piece.MakePiece('R'),'a',1,'c',1,null));
            Assert.AreEqual(false, board.BoardState.LastCastlingRightWhiteQueenSide);
            Assert.AreEqual(false, board.BoardState.LastCastlingRightWhiteKingSide);

            board.Move(new NormalMove(Piece.MakePiece('k'),'e',8,'f',8,null));
            Assert.AreEqual(false, board.BoardState.LastCastlingRightWhiteQueenSide);
            Assert.AreEqual(false, board.BoardState.LastCastlingRightWhiteKingSide);
            Assert.AreEqual(false, board.BoardState.LastCastlingRightBlackQueenSide);
            Assert.AreEqual(false, board.BoardState.LastCastlingRightBlackKingSide);
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