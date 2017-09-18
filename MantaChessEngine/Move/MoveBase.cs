using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace MantaChessEngine
{
    public abstract class MoveBase : IMove
    {
        public Piece MovingPiece { get; set; }
        public int SourceFile { get; set; }
        public int SourceRank { get; set; }
        public int TargetFile { get; set; }
        public int TargetRank { get; set; }
        public Piece CapturedPiece { get; set; }

        public Definitions.ChessColor Color
        {
            get
            {
                return MovingPiece.Color;
                //Definitions.ChessColor color = (MovingPiece >= 'A' && MovingPiece <= 'Z')
                //    ? Definitions.ChessColor.White
                //    : Definitions.ChessColor.Black;
                //return color;
            }
        }

        public int CapturedFile // same as target file
        {
            get { return TargetFile; }
        } 

        public virtual int CapturedRank // mostly this is the same as target rank but for en passant capture it is different
        {
            get { return TargetRank; }
        }

        public MoveBase(Piece movingPiece, char sourceFile, int sourceRank, char targetFile, int targetRank, Piece capturedPiece)
        {
            InitializeMove(movingPiece, Helper.FileCharToFile(sourceFile), sourceRank, Helper.FileCharToFile(targetFile), targetRank, capturedPiece);
        }

        public MoveBase(Piece movingPiece, int sourceFile, int sourceRank, int targetFile, int targetRank, Piece capturedPiece)
        {
            InitializeMove(movingPiece, sourceFile, sourceRank, targetFile, targetRank, capturedPiece);
        }

        public void InitializeMove(Piece movingPiece, int sourceFile, int sourceRank, int targetFile, int targetRank, Piece capturedPiece)
        {
            MovingPiece = movingPiece;
            SourceFile = sourceFile;
            SourceRank = sourceRank;
            TargetFile = targetFile;
            TargetRank = targetRank;
            CapturedPiece = capturedPiece;
        }

        public override bool Equals(System.Object obj)
        {
            // If parameter is null return false.
            if (obj == null)
            {
                return false;
            }

            // If parameter cannot be cast to CurrentMove return false.
            MoveBase other = obj as MoveBase;
            if ((System.Object)other == null)
            {
                return false;
            }

            bool equal = SourceFile == other.SourceFile;
            equal &= SourceRank == other.SourceRank;
            equal &= TargetFile == other.TargetFile;
            equal &= TargetRank == other.TargetRank;
            if (CapturedPiece != null)
            {
                equal &= CapturedPiece.Equals(other.CapturedPiece);
            }

            // note: only check MovingPiece if they are set in both objects
            // new CurrentMove("a2a3") is equal to new CurrentMove('p', a, 2, a, 3, nocapture, enpassant=false)
            // --> this is useful for tests!
            // todo: is this also for captured piece ???
            if (MovingPiece != null && other.MovingPiece != null)
            {
                equal &= MovingPiece.Equals(other.MovingPiece);
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
            moveString += CapturedPiece != null ? CapturedPiece.Symbol : Definitions.EmptyField;
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
            board.SetPiece(null, /*Definitions.EmptyField,*/ SourceFile, SourceRank); // empty MovingPiece's old position

            int enPassantFile = 0;
            int enPassantRank = 0;
            SetEnPassantFields(this, out enPassantFile, out enPassantRank);

            // Set Castling rights
            // if white king moved --> castling right white both sides = false
            // if white rook queen side moved --> castling right white queen side = false
            // if white rook king side moved --> castling right white king side = false
            // same for black
            bool blackKingMoved = MovingPiece is King && MovingPiece.Color == Definitions.ChessColor.Black;
            bool whiteKingMoved = MovingPiece is King && MovingPiece.Color == Definitions.ChessColor.White;
            bool blackRookKingSideMoved = MovingPiece is Rook && MovingPiece.Color == Definitions.ChessColor.Black && SourceFile == 8;
            bool whiteRookKingSideMoved = MovingPiece is Rook && MovingPiece.Color == Definitions.ChessColor.White && SourceFile == 8;
            bool blackRookQueenSideMoved = MovingPiece is Rook && MovingPiece.Color == Definitions.ChessColor.Black && SourceFile == 1;
            bool whiteRookQueenSideMoved = MovingPiece is Rook && MovingPiece.Color == Definitions.ChessColor.White && SourceFile == 1;
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
            board.SetPiece(MovingPiece, SourceFile, SourceRank);
            board.SetPiece(null, /*Definitions.EmptyField,*/ TargetFile, TargetRank);     // TargetFile is equal to CapturedFile
            board.SetPiece(CapturedPiece, CapturedFile, CapturedRank); // TargetRank differs from TargetRank for en passant capture

            board.History.Back();
            board.SideToMove = Helper.GetOpositeColor(board.SideToMove);
        }

        private void SetEnPassantFields(MoveBase move, out int enPassantFile, out int enPassantRank)
        {
            enPassantFile = 0;
            enPassantRank = 0;

            // set black en passant field
            if (move.MovingPiece is Pawn && move.MovingPiece.Color == Definitions.ChessColor.Black) // black pawn
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
            if (move.MovingPiece is Pawn && move.MovingPiece.Color == Definitions.ChessColor.White) // white pawn
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
