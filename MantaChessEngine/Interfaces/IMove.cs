using MantaCommon;

namespace MantaChessEngine
{
    public interface IMove : IGenericMove
    {
        Piece CapturedPiece { get; }
        int CapturedFile { get; }
        int CapturedRank { get; }
        ////ChessColor MovingColor { get; }
        Piece MovingPiece { get; }
        int SourceFile { get; }
        int SourceRank { get; }
        int TargetFile { get; }
        int TargetRank { get; }
        int GetMoveImportance();
        string ToPrintString();
        string ToUciString();
        void ExecuteMove(IBoard board);
        void UndoMove(IBoard board);
    }
}