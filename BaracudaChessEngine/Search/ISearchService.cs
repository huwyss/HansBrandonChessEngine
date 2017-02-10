using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaracudaChessEngine
{
    public interface ISearchService
    {
        Move Search(Board board, Definitions.ChessColor color, out float score);
    }
}
