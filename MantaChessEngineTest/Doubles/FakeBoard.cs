using MantaChessEngine;
using MantaCommon;

namespace MantaChessEngineTest.Doubles
{
    public class FakeBoard : IBoard
    {
        public BoardState BoardState { get; set; }
        public string GetPositionString { get; }
        public string GetPrintString { get; }

        public void SetInitialPosition() { }

        public void SetPosition(string position) { }
        public string SetFenPosition(string fen) { return string.Empty; }

        public string GetFenString() { return string.Empty; }

        public Piece GetPiece(Square square)
        {
            return null;
        }

        public void SetPiece(Piece piece, Square square)
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

        public ChessColor GetColor(Square square)
        {
            return ChessColor.White;
        }

        public bool IsWinner(ChessColor color)
        {
            return false;
        }

        public Square GetKing(ChessColor color)
        {
            return Square.NoSquare;
        }
    }
}
