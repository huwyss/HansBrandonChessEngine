namespace MantaChessEngine
{
    public struct BitMove
    {
        public BitMove(
            PieceType movingPiece,
            Square fromSquare,
            Square toSquare,
            PieceType capturedPiece,
            Square capturedSquare,
            PieceType promotionPiece,
            byte value)
        {
            FromSquare = fromSquare;
            ToSquare = toSquare;
            MovingPiece = movingPiece;
            CapturedPiece = capturedPiece;
            CapturedSquare = capturedSquare;
            PromotionPiece = promotionPiece;
            Value = value;
        }

        public PieceType MovingPiece { get; }
        public Square FromSquare { get; }
        public Square ToSquare { get; }
        public PieceType CapturedPiece { get; }
        public Square CapturedSquare { get; }
        public PieceType PromotionPiece { get; }
        public byte Value { get; }
    }

    public static class BitMoveExtension
    {
        public static bool IsCaptureMove(this BitMove move)
        {
            return move.CapturedPiece != PieceType.Empty;
        }

        public static bool IsPromotionMove(this BitMove move)
        {
            return move.PromotionPiece != PieceType.Empty; 
        }

        public static bool IsEnpassantMove(this BitMove move)
        {
            return move.MovingPiece == PieceType.Pawn &&
                move.CapturedPiece != PieceType.Empty &&
                move.CapturedSquare != move.ToSquare;
        }
    }
}
