using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace MantaChessEngine
{
    public class Move : IMove
    {
        public char MovingPiece { get; set; }
        public int SourceFile { get; set; }
        public int SourceRank { get; set; }
        public int TargetFile { get; set; }
        public int TargetRank { get; set; }
        public char CapturedPiece { get; set; }
        public bool EnPassant { get; set; }

        public Definitions.ChessColor Color
        {
            get
            {
                Definitions.ChessColor color = (MovingPiece >= 'A' && MovingPiece <= 'Z')
                    ? Definitions.ChessColor.White
                    : Definitions.ChessColor.Black;
                return color;
            }
        }

        public int CapturedFile // same as target file
        {
            get { return TargetFile; }
            
        } 

        public virtual int CapturedRank // mostly this is the same as target rank but for en passant capture it is different
        {
            get
            {
                //if (EnPassant)
                //{
                //    if (Color == Definitions.ChessColor.White)
                //    {
                //        return TargetRank - 1;
                //    }

                //    return TargetRank + 1;
                //}

                return TargetRank;
            }
        }

        

        public Move(char movingPiece, int sourceFile, int sourceRank, int targetFile, int targetRank, char capturedPiece, bool enPassant = false)
        {
            MovingPiece = movingPiece;
            SourceFile = sourceFile;
            SourceRank = sourceRank;
            TargetFile = targetFile;
            TargetRank = targetRank;
            CapturedPiece = capturedPiece;
            EnPassant = enPassant;
        }

        public Move(string moveString)
        {
            if (moveString.Length >= 4)
            {
                SourceFile = Helper.FileCharToFile(moveString[0]);
                SourceRank = int.Parse(moveString[1].ToString());
                TargetFile = Helper.FileCharToFile(moveString[2]);
                TargetRank = int.Parse(moveString[3].ToString());
            }

            if (moveString.Length >= 5)
            {
                CapturedPiece = moveString[4];
            }

            if (moveString.Length >= 6)
            {
                if (moveString[5] == 'e')
                {
                    EnPassant = true;
                }
            }

            MovingPiece = (char)0;
        }

        public override bool Equals(System.Object obj)
        {
            // If parameter is null return false.
            if (obj == null)
            {
                return false;
            }

            // If parameter cannot be cast to Move return false.
            Move other = obj as Move;
            if ((System.Object)other == null)
            {
                return false;
            }

            bool equal = SourceFile == other.SourceFile;
            equal &= SourceRank == other.SourceRank;
            equal &= TargetFile == other.TargetFile;
            equal &= TargetRank == other.TargetRank;
            equal &= CapturedPiece == other.CapturedPiece;
            equal &= EnPassant == other.EnPassant;

            // note: only check MovingPiece if they are set in both objects
            // new Move("a2a3") is equal to new Move('p', a, 2, a, 3, nocapture, enpassant=false)
            // --> this is useful for tests!
            if (MovingPiece != (char) 0 && other.MovingPiece != (char) 0)
            {
                equal &= MovingPiece == other.MovingPiece;
            }
            
            return equal;
        }

        public override string ToString()
        {
            string moveString = "";
            moveString += Helper.FileToFileChar(SourceFile);
            moveString += SourceRank.ToString();
            moveString += Helper.FileToFileChar(TargetFile);
            moveString += TargetRank;
            moveString += CapturedPiece;
            moveString += EnPassant ? "e" : "";
            return moveString;
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public static bool IsCorrectMove(string moveString)
        {
            bool correct = true;

            if (moveString.Length >= 4)
            {
                correct &= moveString[0] >= 'a';
                correct &= moveString[0] <= 'h';

                correct &= moveString[1] >= '1';
                correct &= moveString[1] <= '8';

                correct &= moveString[2] >= 'a';
                correct &= moveString[2] <= 'h';

                correct &= moveString[3] >= '1';
                correct &= moveString[3] <= '8';
            }
            else
            {
                return false;
            }

            if (moveString.Length >= 5)
            {
                char capturedPiece = moveString[4].ToString().ToLower()[0];
                correct &= capturedPiece == Definitions.KING ||
                           capturedPiece == Definitions.QUEEN ||
                           capturedPiece == Definitions.ROOK ||
                           capturedPiece == Definitions.BISHOP ||
                           capturedPiece == Definitions.KNIGHT ||
                           capturedPiece == Definitions.PAWN;
            }

            return correct;
        }

        public virtual void ExecuteMove(Board board)
        {
            board.SetPiece(MovingPiece, TargetFile, TargetRank); // set MovingPiece to new position (and overwrite captured piece)
            board.SetPiece(Definitions.EmptyField, SourceFile, SourceRank); // empty MovingPiece's old position

            //if (EnPassant)
            //{
            //    board.SetPiece(Definitions.EmptyField, CapturedFile, CapturedRank); // remove captured pawn if it is en passant
            //}

            // if white king side castling
            //if (MovingPiece == Definitions.KING.ToString().ToUpper()[0] &&
            //    SourceFile == Helper.FileCharToFile('e') && SourceRank == 1 &&
            //    TargetFile == Helper.FileCharToFile('g') && TargetRank == 1)
            //{
            //    board.SetPiece(Definitions.ROOK.ToString().ToUpper()[0], Helper.FileCharToFile('f'), 1); // move rook next to king
            //    board.SetPiece(Definitions.EmptyField, Helper.FileCharToFile('h'), 1); // remove old rook
            //    board.WhiteDidCastling = true;
            //}

            //// if white queen side castling
            //if (MovingPiece == Definitions.KING.ToString().ToUpper()[0] &&
            //    SourceFile == Helper.FileCharToFile('e') && SourceRank == 1 &&
            //    TargetFile == Helper.FileCharToFile('c') && TargetRank == 1)
            //{
            //    board.SetPiece(Definitions.ROOK.ToString().ToUpper()[0], Helper.FileCharToFile('d'), 1); // move rook next to king
            //    board.SetPiece(Definitions.EmptyField, Helper.FileCharToFile('a'), 1); // remove old rook
            //    board.WhiteDidCastling = true;
            //}

            // if black king side castling
            //if (MovingPiece == Definitions.KING.ToString().ToLower()[0] &&
            //    SourceFile == Helper.FileCharToFile('e') && SourceRank == 8 &&
            //    TargetFile == Helper.FileCharToFile('g') && TargetRank == 8)
            //{
            //    board.SetPiece(Definitions.ROOK.ToString().ToLower()[0], Helper.FileCharToFile('f'), 8); // move rook next to king
            //    board.SetPiece(Definitions.EmptyField, Helper.FileCharToFile('h'), 8); // remove old rook
            //    board.BlackDidCastling = true;
            //}

            // if black queen side castling
            //if (MovingPiece == Definitions.KING.ToString().ToLower()[0] &&
            //    SourceFile == Helper.FileCharToFile('e') && SourceRank == 8 &&
            //    TargetFile == Helper.FileCharToFile('c') && TargetRank == 8)
            //{
            //    board.SetPiece(Definitions.ROOK.ToString().ToLower()[0], Helper.FileCharToFile('d'), 8); // move rook next to king
            //    board.SetPiece(Definitions.EmptyField, Helper.FileCharToFile('a'), 8); // remove old rook
            //    board.BlackDidCastling = true;
            //}

            int enPassantFile = 0;
            int enPassantRank = 0;
            SetEnPassantFields(this, out enPassantFile, out enPassantRank);

            // Set Castling rights
            // if white king moved --> castling right white both sides = false
            // if white rook queen side moved --> castling right white queen side = false
            // if white rook king side moved --> castling right white king side = false
            // same for black
            bool blackKingMoved = MovingPiece == Definitions.KING.ToString().ToLower()[0];
            bool blackRookKingSideMoved = MovingPiece == Definitions.ROOK.ToString().ToLower()[0] && SourceFile == 8;
            bool blackRookQueenSideMoved = MovingPiece == Definitions.ROOK.ToString().ToLower()[0] && SourceFile == 1;
            bool whiteKingMoved = MovingPiece == Definitions.KING.ToString().ToUpper()[0];
            bool whiteRookKingSideMoved = MovingPiece == Definitions.ROOK.ToString().ToUpper()[0] && SourceFile == 8;
            bool whiteRookQueenSideMoved = MovingPiece == Definitions.ROOK.ToString().ToUpper()[0] && SourceFile == 1;
            bool castlingRightWhiteQueenSide = board.History.LastCastlingRightWhiteQueenSide & !whiteKingMoved & !whiteRookQueenSideMoved;
            bool castlingRightWhiteKingSide = board.History.LastCastlingRightWhiteKingSide & !whiteKingMoved & !whiteRookKingSideMoved;
            bool castlingRightBlackQueenSide = board.History.LastCastlingRightBlackQueenSide & !blackKingMoved & !blackRookQueenSideMoved;
            bool castlingRightBlackKingSide = board.History.LastCastlingRightBlackKingSide & !blackKingMoved & !blackRookKingSideMoved;

            board.History.Add(this, enPassantFile, enPassantRank, castlingRightWhiteQueenSide, castlingRightWhiteKingSide,
                castlingRightBlackQueenSide, castlingRightBlackKingSide);
            board.SideToMove = Helper.GetOpositeColor(board.SideToMove);
        }

        public virtual void UndoMove(Board board)
        {
            //var lastMove = this; //board.History.LastMove;
            board.SetPiece(MovingPiece, SourceFile, SourceRank);

            board.SetPiece(Definitions.EmptyField, TargetFile, TargetRank);     // TargetFile is equal to CapturedFile
            board.SetPiece(CapturedPiece, CapturedFile, CapturedRank); // TargetRank differs from TargetRank for en passant capture

            //// if white king side castling
            //if (MovingPiece == Definitions.KING.ToString().ToUpper()[0] &&
            //    SourceFile == Helper.FileCharToFile('e') && SourceRank == 1 &&
            //    TargetFile == Helper.FileCharToFile('g') && TargetRank == 1)
            //{
            //    board.SetPiece(Definitions.ROOK.ToString().ToUpper()[0], Helper.FileCharToFile('h'), 1); // move rook next to king
            //    board.SetPiece(Definitions.EmptyField, Helper.FileCharToFile('f'), 1); // remove old rook
            //    board.WhiteDidCastling = false;
            //}

            //// if white queen side castling
            //if (MovingPiece == Definitions.KING.ToString().ToUpper()[0] &&
            //    SourceFile == Helper.FileCharToFile('e') && SourceRank == 1 &&
            //    TargetFile == Helper.FileCharToFile('c') && TargetRank == 1)
            //{
            //    board.SetPiece(Definitions.ROOK.ToString().ToUpper()[0], Helper.FileCharToFile('a'), 1); // move rook next to king
            //    board.SetPiece(Definitions.EmptyField, Helper.FileCharToFile('d'), 1); // remove old rook
            //    board.WhiteDidCastling = false;
            //}

            //// if black king side castling
            //if (MovingPiece == Definitions.KING.ToString().ToLower()[0] &&
            //    SourceFile == Helper.FileCharToFile('e') && SourceRank == 8 &&
            //    TargetFile == Helper.FileCharToFile('g') && TargetRank == 8)
            //{
            //    board.SetPiece(Definitions.ROOK.ToString().ToLower()[0], Helper.FileCharToFile('h'), 8); // move rook next to king
            //    board.SetPiece(Definitions.EmptyField, Helper.FileCharToFile('f'), 8); // remove old rook
            //    board.BlackDidCastling = false;
            //}

            //// if black queen side castling
            //if (MovingPiece == Definitions.KING.ToString().ToLower()[0] &&
            //    SourceFile == Helper.FileCharToFile('e') && SourceRank == 8 &&
            //    TargetFile == Helper.FileCharToFile('c') && TargetRank == 8)
            //{
            //    board.SetPiece(Definitions.ROOK.ToString().ToLower()[0], Helper.FileCharToFile('a'), 8); // move rook next to king
            //    board.SetPiece(Definitions.EmptyField, Helper.FileCharToFile('d'), 8); // remove old rook
            //    board.BlackDidCastling = false;
            //}

            board.History.Back();
            board.SideToMove = Helper.GetOpositeColor(board.SideToMove);
        }

        private void SetEnPassantFields(Move move, out int enPassantFile, out int enPassantRank)
        {
            enPassantFile = 0;
            enPassantRank = 0;

            // set black en passant field
            if (move.MovingPiece == Definitions.PAWN.ToString().ToLower()[0]) // black pawn
            {
                // set en passant field
                if (move.SourceRank - 2 == move.TargetRank && // if 2 fields move
                    move.SourceFile == move.TargetFile)       // and straight move 
                {
                    enPassantRank = move.SourceRank - 1;
                    enPassantFile = move.SourceFile;
                }
            }

            // set white en passant field
            if (move.MovingPiece == Definitions.PAWN.ToString().ToUpper()[0]) // white pawn
            {
                // set en passant field
                if (move.SourceRank + 2 == move.TargetRank && // if 2 fields move
                    move.SourceFile == move.TargetFile)       // and straight move 
                {
                    enPassantRank = move.SourceRank + 1;
                    enPassantFile = move.SourceFile;
                }
            }
        }
    }
}
