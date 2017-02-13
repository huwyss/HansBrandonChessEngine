using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaracudaChessEngine
{
    class SearchServiceDepthHalfMove : ISearchService
    {
        private IEvaluator _evaluator;

        public SearchServiceDepthHalfMove(IEvaluator evaluator)
        {
            _evaluator = evaluator;
        }

        public IMove Search(Board board, Definitions.ChessColor color, out float score)
        {
            Move bestMove = null;
            float bestScore = InitBestScoreSofar(color);

            var possibleMoves = board.GetAllMoves(color);
            foreach (Move currentMove in possibleMoves)
            {
                Board boardWithMove = board.Clone();
                boardWithMove.Move(currentMove);
                float scoreCurrentMove = _evaluator.Evaluate(boardWithMove);
                if (IsBestMoveSofar(color, bestScore, scoreCurrentMove))
                {
                    bestMove = currentMove;
                    bestScore = scoreCurrentMove;
                }
            }

            score = bestScore;
            return bestMove;
        }

        private float InitBestScoreSofar(Definitions.ChessColor color)
        {
            if (color == Definitions.ChessColor.White)
            {
                return -10000;
            }
            else
            {
                return 10000;
            }
        }

        private bool IsBestMoveSofar(Definitions.ChessColor color, float bestScoreSoFar, float currentScore)
        {
            if (color == Definitions.ChessColor.White)
            {
                if (currentScore > bestScoreSoFar)
                {
                    return true;
                }
            }
            else
            {
                if (currentScore < bestScoreSoFar)
                {
                    return true;
                }
            }

            return false;
        }
    }
}

