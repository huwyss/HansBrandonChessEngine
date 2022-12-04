namespace MantaBitboardEngine
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
            BitColor movingColor,
            byte value)
        {
            MovingPiece = movingPiece;
            FromSquare = fromSquare;
            ToSquare = toSquare;
            CapturedPiece = capturedPiece;
            CapturedSquare = capturedSquare;
            PromotionPiece = promotionPiece;
            Castling = castling;
            MovingColor = movingColor;
            Value = value;
        }

        public static BitMove CreateCapture(
            BitPieceType movingPiece,
            Square fromSquare,
            Square toSquare,
            BitPieceType capturedPiece,
            Square capturedSquare,
            BitPieceType promotionPiece,
            BitColor movingColor,
            byte value)
        {
            return new BitMove(movingPiece, fromSquare, toSquare, capturedPiece, capturedSquare, promotionPiece, false, movingColor, value);
        }

        // move constructor
        public static BitMove CreateMove(
            BitPieceType movingPiece,
            Square fromSquare,
            Square toSquare,
            BitPieceType promotionPiece,
            BitColor movingColor,
            byte value)
        {
            return new BitMove(movingPiece, fromSquare, toSquare, BitPieceType.Empty, Square.NoSquare, promotionPiece, false, movingColor, value);
        }

        // castling constructor
        public static BitMove CreateCastling(
            BitPieceType movingPiece,
            Square fromSquare,
            Square toSquare,
            BitColor movingColor,
            byte value)
        {
            return new BitMove(movingPiece, fromSquare, toSquare, BitPieceType.Empty, Square.NoSquare, BitPieceType.Empty, true, movingColor, value);
        }

        public BitPieceType MovingPiece { get; }
        public Square FromSquare { get; }
        public Square ToSquare { get; }
        public BitPieceType CapturedPiece { get; }
        public Square CapturedSquare { get; }
        public BitPieceType PromotionPiece { get; }
        public bool Castling { get; }
        public BitColor MovingColor { get; }
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
