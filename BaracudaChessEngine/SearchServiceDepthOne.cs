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
            foreach (Move currentMove in possibleMoves)
            {
                Board boardWithMove = board.Clone();
                boardWithMove.Move(currentMove);
                float scoreCurrentMove = _evaluator.Evaluate(boardWithMove);
                if (color == Definitions.ChessColor.White)
                {
                    if (scoreCurrentMove > bestScore)
                    {
                        bestMove = currentMove;
                        bestScore = scoreCurrentMove;
                    }
                }
                else
                {
                    if (scoreCurrentMove < bestScore)
                    {
                        bestMove = currentMove;
                        bestScore = scoreCurrentMove;
                    }
                }
            }

            return bestMove;
        }
    }
}
