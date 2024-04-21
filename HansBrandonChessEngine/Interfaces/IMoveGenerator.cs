using System.Collections.Generic;
using HBCommon;

namespace HansBrandonChessEngine
{
    public interface IMoveGenerator
    {
        IEnumerable<IMove> GetLegalMoves(IBoard board, ChessColor color);

        bool IsMoveValid(IBoard board, IMove move);

        bool IsAttacked(IBoard board, ChessColor color, int file, int rank);

        bool IsCheck(IBoard board, ChessColor color);
    }
}