﻿namespace MantaChessEngine
{
    public interface IMove
    {
        int CapturedFile { get; }
        Piece CapturedPiece { get; set; }
        int CapturedRank { get; }
        Definitions.ChessColor Color { get; }
        Piece MovingPiece { get; set; }
        int SourceFile { get; set; }
        int SourceRank { get; set; }
        int TargetFile { get; set; }
        int TargetRank { get; set; }

        bool Equals(object obj);
        void ExecuteMove(IBoard board);
        int GetHashCode();
        void InitializeMove(Piece movingPiece, int sourceFile, int sourceRank, int targetFile, int targetRank, Piece capturedPiece);
        string ToString();
        string ToPrintString();
        void UndoMove(IBoard board);
    }

    public interface IEvaluatedMove
    {
        IMove Move { get; }

        float Score { get; }
    }

    public class EvaluatedMove : IEvaluatedMove
    {
        public IMove Move { get; set; }

        public float Score { get; set; }
    }
}