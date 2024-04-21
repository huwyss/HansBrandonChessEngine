using System.Collections.Generic;

namespace HansBrandonChessEngine
{
    public interface IMoveFilter
    { 
        IList<IMove> Filter(IList<IMove> possibleMovesUnsorted);
    }
}
