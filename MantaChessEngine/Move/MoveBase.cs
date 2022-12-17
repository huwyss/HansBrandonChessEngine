using static MantaChessEngine.Definitions;
using MantaCommon;

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

        public ChessColor Color
        {
            get
            {
                return MovingPiece != null ? MovingPiece.Color : ChessColor.Empty;
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

        private void InitializeMove(Piece movingPiece, int sourceFile, int sourceRank, int targetFile, int targetRank, Piece capturedPiece)
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

            // If parameter cannot be cast to MoveBase return false.
            MoveBase other = obj as MoveBase;
            if (other == null)
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
            else if (CapturedPiece == null && other.CapturedPiece != null)
            {
                equal = false;
            }

            if (MovingPiece != null && other.MovingPiece != null)
            {
                equal &= MovingPiece.Equals(other.MovingPiece);
            }
            else
            {
                equal = false;
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
            moveString += CapturedPiece != null ? CapturedPiece.Symbol : EmptyField;
            return moveString;
        }

        public virtual string ToPrintString()
        {
            string moveString = "";
            if (!(MovingPiece is Pawn))
            {
                moveString += MovingPiece.UniversalSymbol;
            }
            moveString += Helper.FileToFileChar(SourceFile);
            moveString += SourceRank.ToString();

            if (CapturedPiece == null)
            {
                moveString += "-";
            }
            else
            {
                moveString += "x";
            }

            moveString += Helper.FileToFileChar(TargetFile);
            moveString += TargetRank;
            
            return moveString;
        }

        public virtual string ToUciString()
        {
            string moveString = "";
            moveString += Helper.FileToFileChar(SourceFile);
            moveString += SourceRank.ToString();
            moveString += Helper.FileToFileChar(TargetFile);
            moveString += TargetRank;
            return moveString;
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }
        
        public virtual void ExecuteMove(IBoard board)
        {
            board.SetPiece(MovingPiece, TargetFile, TargetRank); // set MovingPiece to new position (and overwrite captured piece)
            board.SetPiece(null, SourceFile, SourceRank); // empty MovingPiece's old position

            SetEnPassantFields(this, out int enPassantFile, out int enPassantRank);

            // Set Castling rights
            // if white king moved --> castling right white both sides = false
            // if white rook queen side moved --> castling right white queen side = false
            // if white rook king side moved --> castling right white king side = false
            // same for black
            bool blackKingMoved = MovingPiece is King && MovingPiece.Color == ChessColor.Black;
            bool whiteKingMoved = MovingPiece is King && MovingPiece.Color == ChessColor.White;
            bool blackRookKingSideMoved = MovingPiece is Rook && MovingPiece.Color == ChessColor.Black && SourceFile == 8;
            bool whiteRookKingSideMoved = MovingPiece is Rook && MovingPiece.Color == ChessColor.White && SourceFile == 8;
            bool blackRookQueenSideMoved = MovingPiece is Rook && MovingPiece.Color == ChessColor.Black && SourceFile == 1;
            bool whiteRookQueenSideMoved = MovingPiece is Rook && MovingPiece.Color == ChessColor.White && SourceFile == 1;
            bool castlingRightWhiteQueenSide = board.BoardState.LastCastlingRightWhiteQueenSide & !whiteKingMoved & !whiteRookQueenSideMoved;
            bool castlingRightWhiteKingSide = board.BoardState.LastCastlingRightWhiteKingSide & !whiteKingMoved & !whiteRookKingSideMoved;
            bool castlingRightBlackQueenSide = board.BoardState.LastCastlingRightBlackQueenSide & !blackKingMoved & !blackRookQueenSideMoved;
            bool castlingRightBlackKingSide = board.BoardState.LastCastlingRightBlackKingSide & !blackKingMoved & !blackRookKingSideMoved;

            board.BoardState.Add(
                this,
                enPassantFile,
                enPassantRank,
                castlingRightWhiteQueenSide,
                castlingRightWhiteKingSide,
                castlingRightBlackQueenSide,
                castlingRightBlackKingSide,
                Helper.GetOppositeColor(board.BoardState.SideToMove));
        }

        public virtual void UndoMove(IBoard board)
        {
            board.SetPiece(MovingPiece, SourceFile, SourceRank);
            board.SetPiece(null, /*Definitions.EmptyField,*/ TargetFile, TargetRank);     // TargetFile is equal to CapturedFile
            board.SetPiece(CapturedPiece, CapturedFile, CapturedRank); // TargetRank differs from TargetRank for en passant capture

            board.BoardState.Back();
            board.BoardState.SideToMove = Helper.GetOppositeColor(board.BoardState.SideToMove);
        }

        private void SetEnPassantFields(MoveBase move, out int enPassantFile, out int enPassantRank)
        {
            enPassantFile = 0;
            enPassantRank = 0;

            // set black en passant field
            if (move.MovingPiece is Pawn && move.MovingPiece.Color == ChessColor.Black) // black pawn
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
            if (move.MovingPiece is Pawn && move.MovingPiece.Color == ChessColor.White) // white pawn
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

        public virtual int GetMoveImportance()
        {
            int importance = 0;
            if (CapturedPiece != null)
            {
                importance += CapturedPiece.GetPlainPieceValue() - MovingPiece.GetPlainPieceValue() + ImportanceCapture;
            }
            
            if (MovingPiece is Pawn)
            {
                importance += ImportancePawnMove;
            }

            return importance;
        }
    }
}
