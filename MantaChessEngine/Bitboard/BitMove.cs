namespace MantaChessEngine.BitboardEngine
{
    public struct BitMove
    {
        // capture constructor
        private BitMove(
            BitPieceType movingPiece,
            Square fromSquare,
            Square toSquare,
            BitPieceType capturedPiece,
            Square capturedSquare,
            BitPieceType promotionPiece,
            bool castling,
            byte value)
        {
            MovingPiece = movingPiece;
            FromSquare = fromSquare;
            ToSquare = toSquare;
            CapturedPiece = capturedPiece;
            CapturedSquare = capturedSquare;
            PromotionPiece = promotionPiece;
            Castling = castling;
            Value = value;
        }

        public static BitMove CreateCapture(
            BitPieceType movingPiece,
            Square fromSquare,
            Square toSquare,
            BitPieceType capturedPiece,
            Square capturedSquare,
            BitPieceType promotionPiece,
            byte value)
        {
            return new BitMove(movingPiece, fromSquare, toSquare, capturedPiece, capturedSquare, promotionPiece, false, value);
        }

        // move constructor
        public static BitMove CreateMove(
            BitPieceType movingPiece,
            Square fromSquare,
            Square toSquare,
            BitPieceType promotionPiece,
            byte value)
        {
            return new BitMove(movingPiece, fromSquare, toSquare, BitPieceType.Empty, Square.NoSquare, promotionPiece, false, value);
        }

        // castling constructor
        public static BitMove CreateCastling(
            BitPieceType movingPiece,
            Square fromSquare,
            Square toSquare,
            byte value)
        {
            return new BitMove(movingPiece, fromSquare, toSquare, BitPieceType.Empty, Square.NoSquare, BitPieceType.Empty, true, value);
        }

        public BitPieceType MovingPiece { get; }
        public Square FromSquare { get; }
        public Square ToSquare { get; }
        public BitPieceType CapturedPiece { get; }
        public Square CapturedSquare { get; }
        public BitPieceType PromotionPiece { get; }
        public bool Castling { get; }
        public byte Value { get; }
    }

    public static class BitMoveExtension
    {
        public static bool IsCaptureMove(this BitMove move)
        {
            return move.CapturedPiece != BitPieceType.Empty;
        }

        public static bool IsPromotionMove(this BitMove move)
        {
            return move.PromotionPiece != BitPieceType.Empty; 
        }

        public static bool IsEnpassantCapture(this BitMove move)
        {
            return move.MovingPiece == BitPieceType.Pawn &&
                move.CapturedPiece != BitPieceType.Empty &&
                move.CapturedSquare != move.ToSquare;
        }
    }
}
