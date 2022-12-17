using System.Collections.Generic;
using static MantaChessEngine.Definitions;
using MantaCommon;

namespace MantaChessEngine
{
    public interface IMoveOrder
    {
        IEnumerable<IMove> OrderMoves(IList<IMove> unsortedMoves, ChessColor movingColor, int currentLevel);
    }
}
