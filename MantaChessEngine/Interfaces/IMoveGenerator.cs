using System.Collections.Generic;
using static MantaChessEngine.Definitions;

namespace MantaChessEngine
{
    public interface IMoveGenerator
    {
        IEnumerable<IMove> GetLegalMoves(IBoard board, ChessColor color);

        /// <summary>
        /// Returns all pseudo legal moves of that piece. Pseudo means the king is allowed to be under attack but
        /// otherwise the move must be legal.
        /// </summary>
        IEnumerable<IMove> GetAllMoves(IBoard board, ChessColor color, bool includeCastling = true, bool includePawnMoves = true);
        
        bool IsMoveValid(IBoard board, IMove move);

        bool IsAttacked(IBoard board, ChessColor color, int file, int rank);

        bool IsCheck(IBoard board, ChessColor color);
    }
}