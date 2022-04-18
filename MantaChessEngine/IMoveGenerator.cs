using System.Collections.Generic;
using static MantaChessEngine.Definitions;

namespace MantaChessEngine
{
    public interface IMoveGenerator
    {
        List<IMove> GetLegalMoves(IBoard board, ChessColor color);

        List<IMove> GetAllMoves(IBoard board, ChessColor color, bool includeCastling = true);

        /// <summary>
        /// Returns all pseudo legal moves of that piece. Pseudo means the king is allowed to be under attack but
        /// otherwise the move must be legal.
        /// </summary>
        /// <param name="file"></param>
        /// <param name="rank"></param>
        /// <returns></returns>
        bool IsMoveValid(IBoard board, IMove move);
        bool IsAttacked(IBoard board, ChessColor color, int file, int rank);
        bool IsCheck(IBoard board, ChessColor color);
    }
}