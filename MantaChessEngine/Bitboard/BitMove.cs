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
            byte promotion, 
            byte value)
        {
            FromSquare = fromSquare;
            ToSquare = toSquare;
            MovingPiece = movingPiece;
            CapturedPiece = capturedPiece;
            CapturedSquare = capturedSquare;
            Promotion = promotion;
            Value = value;
        }

        public byte MovingPiece { get; }
        public byte FromSquare { get; }
        public byte ToSquare { get; }
        public byte CapturedPiece { get; }
        public byte CapturedSquare { get; }
        public byte Promotion { get; }
        public byte Value { get; }
    }
}
