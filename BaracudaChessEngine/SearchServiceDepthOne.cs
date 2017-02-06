using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaracudaChessEngine
{
    class SearchServiceDepthOne : ISearchService
    {
        public Move Search(Board board, Definitions.ChessColor color)
        {
            return new Move("e2e4.");
        }
    }
}
