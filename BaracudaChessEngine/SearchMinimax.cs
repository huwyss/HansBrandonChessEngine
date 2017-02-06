using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaracudaChessEngine
{
    public class SearchMinimax : ISearchService
    {
        private IEvaluator _evaluator;

        public SearchMinimax(IEvaluator evaluator)
        {
            _evaluator = evaluator;
        }

        public Move Search(Board board, Definitions.ChessColor color)
        {
            return new Move("e2e4.");
        }
    }
}
