using System;

namespace MantaCommon
{
    public interface IMantaEngine
    {
        void SetInitialPosition();
        void SetPosition(string position);
        string SetFenPosition(string fen);
        string GetString();
        string GetPrintString();
        bool MoveUci(string moveStringUci);

        ////public bool Move(string moveStringUser)
        bool UndoMove();
        UciMoveRating DoBestMove(ChessColor color);
        UciMoveRating DoBestMove();
        ChessColor SideToMove();
        bool IsCheck(ChessColor color);
        void Back();
        void SetMaxSearchDepth(int maxDepth);
        void SetAdditionalSelectiveDepth(int additionalSelectiveDepth);
        void ClearPreviousPV();
        UInt64 Perft(int depth);

        ////UInt64 PerftCastling(int depth, IMove moveParam);
        void Divide(int depth);

        string GetPvMovesFromHashtable(ChessColor color);
    }
}
