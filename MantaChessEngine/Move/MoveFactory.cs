using MantaCommon;

namespace MantaChessEngine
{
    public class MoveFactory : IMoveFactory<IMove>
    {
        private readonly IBoard _board;

        public static NormalMove MakeNormalMove(Piece movingPiece, Square fromSquare, Square toSquare, Piece capturedPiece)
        {
            return new NormalMove(movingPiece, fromSquare, toSquare, capturedPiece);
        }

        public static EnPassantCaptureMove MakeEnPassantCaptureMove(Piece movingPiece, Square fromSquare, Square toSquare, Piece capturedPiece)
        {
            return new EnPassantCaptureMove(movingPiece, fromSquare, toSquare, capturedPiece);
        }

        public static PromotionMove MakePromotionMove(Piece movingPiece, Square fromSquare, Square toSquare, Piece capturedPiece, PieceType promotionPiece)
        {
            return new PromotionMove(movingPiece, fromSquare, toSquare, capturedPiece, promotionPiece);
        }

        public static CastlingMove MakeCastlingMove(CastlingType castlingType, Piece king)
        {
            return new CastlingMove(castlingType, king);
        }

        public NoLegalMove MakeNoLegalMove()
        {
            return new NoLegalMove();
        }

        public MoveFactory(IBoard board)
        {
            _board = board;
        }

        public IMove MakeMoveUci(string moveStringUci) // input is like "e2e4" or "a7a8q" (Promotion)
        {
            if (!CommonHelper.IsCorrectMoveUci(moveStringUci))
            {
                return null;
            }

            var fromSquare = CommonHelper.GetSquare(moveStringUci.Substring(0, 2));
            var toSquare = CommonHelper.GetSquare(moveStringUci.Substring(2, 2));
            var promotionPiece = CommonHelper.GetPieceType(moveStringUci.Length == 5 ? moveStringUci[4] : ' ');

            return MakeMove(fromSquare, toSquare, promotionPiece);
        }

        public IMove MakeMove(Square fromSquare, Square toSquare, PieceType promotionPiece)
        {
            Piece movingPiece;
            Piece capturedPiece;
            bool enPassant = false;

            movingPiece = _board.GetPiece(fromSquare);

            // set captured Piece
            if (IsEnPassantCapture(movingPiece, toSquare))
            {
                capturedPiece = _board.GetColor(fromSquare) == ChessColor.White
                    ? _board.GetPiece(toSquare - 8)
                    : _board.GetPiece(toSquare + 8);
                enPassant = true;
            }
            else
            {
                capturedPiece = _board.GetPiece(toSquare);
            }

            if (enPassant)
            {
                return new EnPassantCaptureMove(movingPiece, fromSquare, toSquare, capturedPiece);
            }

            if (IsWhiteKingSideCastling(movingPiece, fromSquare, toSquare))
            {
                return new CastlingMove(CastlingType.WhiteKingSide, movingPiece);
            }

            if (IsWhiteQueenSideCastling(movingPiece, fromSquare, toSquare))
            {
                return new CastlingMove(CastlingType.WhiteQueenSide, movingPiece);
            }

            if (IsBlackKingSideCastling(movingPiece, fromSquare, toSquare))
            {
                return new CastlingMove(CastlingType.BlackKingSide, movingPiece);
            }

            if (IsBlackQueenSideCastling(movingPiece, fromSquare, toSquare))
            {
                return new CastlingMove(CastlingType.BlackQueenSide, movingPiece);
            }

            if (IsPromotion(movingPiece, toSquare, promotionPiece))
            {
                return new PromotionMove(movingPiece, fromSquare, toSquare, capturedPiece, promotionPiece);
            }

            return new NormalMove(movingPiece, fromSquare, toSquare, capturedPiece);
        }

        private bool IsEnPassantCapture(Piece movingPiece, Square toSquare)
        {
            return movingPiece is Pawn &&
                 _board.GetPiece(toSquare) == null &&
                   _board.BoardState.LastEnPassantSquare == toSquare;
        }

        private bool IsWhiteKingSideCastling(Piece movingPiece, Square fromSquare, Square toSquare)
        {
            return movingPiece is King && movingPiece.Color == ChessColor.White &&
                fromSquare == Square.E1 && toSquare == Square.G1;
        }

        private bool IsWhiteQueenSideCastling(Piece movingPiece, Square fromSquare, Square toSquare)
        {
            return movingPiece is King && movingPiece.Color == ChessColor.White &&
                fromSquare == Square.E1 && toSquare == Square.C1;
        }

        private bool IsBlackKingSideCastling(Piece movingPiece, Square fromSquare, Square toSquare)
        {
            return movingPiece is King && movingPiece.Color == ChessColor.Black &&
                fromSquare == Square.E8 && toSquare == Square.G8;
        }

        private bool IsBlackQueenSideCastling(Piece movingPiece, Square fromSquare, Square toSquare)
        {
            return movingPiece is King && movingPiece.Color == ChessColor.Black &&
                fromSquare == Square.E8 && toSquare == Square.C8;
        }

        private bool IsPromotion(Piece movingPiece, Square toSquare, PieceType promotionPieceType)
        {
            return (promotionPieceType != PieceType.Empty &&
                (movingPiece is Pawn && movingPiece.Color == ChessColor.White && toSquare >= Square.A8) || // white promotion
                (movingPiece is Pawn && movingPiece.Color == ChessColor.Black && toSquare <= Square.H1));  // black promotion
        }
    }
}
