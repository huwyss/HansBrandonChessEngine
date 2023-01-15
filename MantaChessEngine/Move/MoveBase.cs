using static MantaChessEngine.Definitions;
using MantaCommon;

namespace MantaChessEngine
{
    public abstract class MoveBase : IMove
    {
        public virtual PieceType PromotionPiece => PieceType.Empty;

        public bool IsCapture()
        {
            return CapturedPiece != null;
        }

        public Piece MovingPiece { get; set; }
        public Square FromSquare { get; set; }
        public Square ToSquare { get; set; }
        public Piece CapturedPiece { get; set; }

        public ChessColor MovingColor
        {
            get
            {
                return MovingPiece != null ? MovingPiece.Color : ChessColor.Empty;
            }
        }

        public virtual Square CapturedSquare // same as target file
        {
            get
            {
                return CapturedPiece != null
                    ? ToSquare
                    : Square.NoSquare;
            }
        } 

        public MoveBase(Piece movingPiece, Square fromSquare, Square toSquare, Piece capturedPiece)
        {
            MovingPiece = movingPiece;
            FromSquare = fromSquare;
            ToSquare = toSquare;
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

            bool equal = FromSquare == other.FromSquare;
            equal &= ToSquare == other.ToSquare;
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
            moveString += FromSquare;
            moveString += ToSquare.ToString();
            moveString += CapturedPiece != null ? CapturedPiece.Symbol : CommonDefinitions.EmptyField;
            return moveString.ToLower();
        }

        public virtual string ToPrintString()
        {
            string moveString = "";
            if (!(MovingPiece is Pawn))
            {
                moveString += MovingPiece.UniversalSymbol;
            }
            moveString += FromSquare;

            if (CapturedPiece == null)
            {
                moveString += "-";
            }
            else
            {
                moveString += "x";
            }

            moveString += ToSquare;
            
            return moveString;
        }

        public virtual string ToUciString()
        {
            string moveString = "";
            moveString += FromSquare;
            moveString += ToSquare;
            return moveString.ToLower();
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }
        
        public virtual void ExecuteMove(IBoard board)
        {
            if (CapturedPiece != null)
            {
                board.RemovePiece(CapturedSquare);
            }

            board.SetPiece(MovingPiece, ToSquare); // set MovingPiece to new position (and overwrite captured piece)
            board.RemovePiece(FromSquare); // empty MovingPiece's old position

            SetEnPassantFields(this, out Square enPassantSquare);

            // Set Castling rights
            // if white king moved --> castling right white both sides = false
            // if white rook queen side moved --> castling right white queen side = false
            // if white rook king side moved --> castling right white king side = false
            // same for black
            bool blackKingMoved = MovingPiece is King && MovingPiece.Color == ChessColor.Black;
            bool whiteKingMoved = MovingPiece is King && MovingPiece.Color == ChessColor.White;
            bool blackRookKingSideMoved = MovingPiece is Rook && MovingPiece.Color == ChessColor.Black && FromSquare == Square.H8;
            bool whiteRookKingSideMoved = MovingPiece is Rook && MovingPiece.Color == ChessColor.White && FromSquare == Square.H1;
            bool blackRookQueenSideMoved = MovingPiece is Rook && MovingPiece.Color == ChessColor.Black && FromSquare == Square.A8;
            bool whiteRookQueenSideMoved = MovingPiece is Rook && MovingPiece.Color == ChessColor.White && FromSquare == Square.A1;
            bool castlingRightWhiteQueenSide = board.BoardState.LastCastlingRightWhiteQueenSide & !whiteKingMoved & !whiteRookQueenSideMoved;
            bool castlingRightWhiteKingSide = board.BoardState.LastCastlingRightWhiteKingSide & !whiteKingMoved & !whiteRookKingSideMoved;
            bool castlingRightBlackQueenSide = board.BoardState.LastCastlingRightBlackQueenSide & !blackKingMoved & !blackRookQueenSideMoved;
            bool castlingRightBlackKingSide = board.BoardState.LastCastlingRightBlackKingSide & !blackKingMoved & !blackRookKingSideMoved;

            board.BoardState.Add(
                this,
                enPassantSquare,
                castlingRightWhiteQueenSide,
                castlingRightWhiteKingSide,
                castlingRightBlackQueenSide,
                castlingRightBlackKingSide,
                Helper.GetOppositeColor(board.BoardState.SideToMove));
        }

        public virtual void UndoMove(IBoard board)
        {
            board.SetPiece(MovingPiece, FromSquare);
            board.RemovePiece(ToSquare);

            if (CapturedPiece != null)
            {
                board.SetPiece(CapturedPiece, CapturedSquare); // CapturedSquare differs from ToSquare for en passant capture
            }

            board.BoardState.Back();
            board.BoardState.SideToMove = Helper.GetOppositeColor(board.BoardState.SideToMove);
        }

        private void SetEnPassantFields(MoveBase move, out Square enPassantSquare)
        {
            enPassantSquare = Square.NoSquare;

            // set black en passant field
            if (move.MovingPiece is Pawn && move.MovingPiece.Color == ChessColor.Black) // black pawn
            {
                // set en passant field
                if (move.FromSquare - 16 == move.ToSquare) // if 2 fields move and straight move 
                {
                    enPassantSquare = move.ToSquare + 8;
                }
            }

            // set white en passant field
            if (move.MovingPiece is Pawn && move.MovingPiece.Color == ChessColor.White) // white pawn
            {
                // set en passant field
                if (move.FromSquare + 16 == move.ToSquare) // if 2 fields move and straight move 
                {
                    enPassantSquare = move.ToSquare - 8;
                }
            }
        }
    }
}
