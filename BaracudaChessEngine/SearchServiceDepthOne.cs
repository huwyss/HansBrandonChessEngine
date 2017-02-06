using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaracudaChessEngine
{
    class SearchServiceDepthOne : ISearchService
    {
        private IEvaluator _evaluator;

        public SearchServiceDepthOne(IEvaluator evaluator)
        {
            _evaluator = evaluator;
        }

        public Move Search(Board board, Definitions.ChessColor color)
        {
            Move bestMove = null;
            float bestScore;
            if (color == Definitions.ChessColor.White)
            {
                bestScore = -10000;
            }
            else
            {
                bestScore = 10000;
            }

            var possibleMoves = board.GetAllMoves(color);
            foreach (Move move in possibleMoves)
            {
                // find best move
            }


            return bestMove;
        }
    }
}
