using MantaCommon;

namespace MantaChessEngine
{
    public interface IMove : IGenericMove
    {
        Piece MovingPiece { get; }
        Square FromSquare { get; }
        Square ToSquare { get; }
        Piece CapturedPiece { get; }
        Square CapturedSquare { get; }
        
        string ToPrintString();
        string ToUciString();
        void ExecuteMove(IBoard board);
        void UndoMove(IBoard board);
    }
}