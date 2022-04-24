using System.Collections.Generic;

namespace MantaChessEngine
{
    public interface IMoveFilter
    { 
        IList<IMove> Filter(IList<IMove> possibleMovesUnsorted);
    }
}
