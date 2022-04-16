using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MantaChessEngine
{
    public interface ISearchService
    {
        MoveRating Search(IBoard board, Definitions.ChessColor color);
        void SetMaxDepth(int maxDepth);
    }
}
