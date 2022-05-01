namespace MantaChessEngine
{
    public struct BitMove
    {
        public BitMove(byte fromSquare, byte toSquare, char movingPiece, char capturedPiece, byte promotion, byte value)
        {
            FromSquare = fromSquare;
            ToSquare = toSquare;
            MovingPiece = movingPiece;
            CapturedPiece = capturedPiece;
            Promotion = promotion;
            Value = value;
        }

        public byte FromSquare { get; }
        public byte ToSquare { get; }
        public char MovingPiece { get; }
        public char CapturedPiece { get; }
        public byte Promotion { get; }
        public byte Value { get; }
    }
}
