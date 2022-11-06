using static MantaChessEngine.Definitions;

namespace MantaChessEngine.Doubles
{
    public class FakeBoard : IBoard
    {
        public ChessColor SideToMove { get; set; }
        public int MoveCountSincePawnOrCapture { get; set; }
        public BoardState BoardState { get; set; }
        public IMove LastMove { get; }
        public int EnPassantFile { get; }
        public int EnPassantRank { get; }
        public bool CastlingRightWhiteQueenSide { get; }
        public bool CastlingRightWhiteKingSide { get; }
        public bool CastlingRightBlackQueenSide { get; }
        public bool CastlingRightBlackKingSide { get; }
        public bool WhiteDidCastling { get; set; }
        public bool BlackDidCastling { get; set; }
        public string GetPositionString { get; }
        public string GetPrintString { get; }
        

        public void SetInitialPosition() { }

        public void SetPosition(string position) { }
        public string SetFenPosition(string fen) { return string.Empty; }

        public string GetFenString() { return string.Empty; }

        public Piece GetPiece(int file, int rank)
        {
            return null;
        }

        public Piece GetPiece(char fileChar, int rank)
        {
            return null;
        }

        public void SetPiece(Piece piece, int file, int rank)
        {
        }

        public void SetPiece(Piece piece, char fileChar, int rank)
        {
        }

        public void Move(IMove nextMove)
        {
        }

        public void Back()
        {
        }

        public void RedoMove()
        {
        }

        public ChessColor GetColor(int file, int rank)
        {
            return ChessColor.White;
        }

        public bool IsWinner(ChessColor color)
        {
            return false;
        }

        public Position GetKing(ChessColor color)
        {
            return null;
        }
    }
}
