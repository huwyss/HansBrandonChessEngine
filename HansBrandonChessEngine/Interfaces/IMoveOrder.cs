using System.Collections.Generic;
using static HansBrandonChessEngine.Definitions;
using HBCommon;

namespace HansBrandonChessEngine
{
    public interface IMoveOrder
    {
        IEnumerable<IMove> OrderMoves(IList<IMove> unsortedMoves, ChessColor movingColor, int currentLevel);
    }
}
