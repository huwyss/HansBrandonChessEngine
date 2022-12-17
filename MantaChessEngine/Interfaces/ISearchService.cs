using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MantaCommon;

namespace MantaChessEngine
{
    public interface ISearchService
    {
        MoveRating Search(IBoard board, ChessColor color);

        void SetMaxDepth(int maxDepth);

        void SetAdditionalSelectiveDepth(int additionalDepth);

        void ClearPreviousPV();
    }
}
