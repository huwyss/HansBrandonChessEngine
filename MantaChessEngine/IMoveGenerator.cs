using System.Collections.Generic;

namespace MantaChessEngine
{
    public interface IMoveGenerator
    {
        List<MoveBase> GetAllMoves(Board board, Definitions.ChessColor color, bool includeCastling = true);

        /// <summary>
        /// Returns all pseudo legal moves of that piece. (King might be under attack).
        /// </summary>
        /// <param name="file"></param>
        /// <param name="rank"></param>
        /// <returns></returns>
        /// todo: pawn promotion
        List<MoveBase> GetMoves(Board board, int file, int rank, bool includeCastling = true);

        bool IsMoveValid(Board board, MoveBase move);
        bool IsAttacked(Board board, Definitions.ChessColor color, int file, int rank);
        bool IsCheck(Board board, Definitions.ChessColor color);
    }
}