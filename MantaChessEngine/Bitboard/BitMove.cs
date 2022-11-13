namespace MantaChessEngine
{
    public struct BitMove
    {
        public BitMove(
            byte movingPiece,
            byte fromSquare,
            byte toSquare,
            byte capturedPiece,
            byte capturedSquare,
            byte promotionPiece,
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

        public byte MovingPiece { get; }
        public byte FromSquare { get; }
        public byte ToSquare { get; }
        public byte CapturedPiece { get; }
        public byte CapturedSquare { get; }
        public byte PromotionPiece { get; }
        public byte Value { get; }
    }

    public static class BitMoveExtension
    {
        public static bool IsCaptureMove(this BitMove move)
        {
            return move.CapturedPiece != Const.Empty;
        }

        public static bool IsPromotionMove(this BitMove move)
        {
            return move.PromotionPiece != Const.Empty; 
        }

        public static bool IsEnpassantMove(this BitMove move)
        {
            return move.MovingPiece == Const.Pawn &&
                move.CapturedPiece != Const.Empty &&
                move.CapturedSquare != move.ToSquare;
        }
    }
}
