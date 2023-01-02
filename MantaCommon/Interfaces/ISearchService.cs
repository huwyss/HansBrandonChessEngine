using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MantaCommon
{
    public interface ISearchService<TMove> where TMove : IGenericMove
    {
        IMoveRating<TMove> Search(ChessColor color);

        void SetMaxDepth(int maxDepth);

        void SetAdditionalSelectiveDepth(int additionalDepth);

        void ClearPreviousPV();
    }
}
