using MantaCommon;

namespace MantaChessEngine
{
    public interface IMove
    {
        Piece CapturedPiece { get; }
        int CapturedFile { get; }
        int CapturedRank { get; }
        ChessColor Color { get; }
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