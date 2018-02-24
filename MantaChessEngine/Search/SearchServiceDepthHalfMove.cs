using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MantaChessEngine
{
    class SearchServiceDepthHalfMove : ISearchService
    {
        private IEvaluator _evaluator;
        private MoveGenerator _moveGenerator;

        public SearchServiceDepthHalfMove(IEvaluator evaluator, MoveGenerator generator)
        {
            _evaluator = evaluator;
            _moveGenerator = generator;
        }

        public IMove Search(Board board, Definitions.ChessColor color, out float score)
        {
            IMove bestMove = null;
            float bestScore = InitBestScoreSofar(color);

            var possibleMoves = _moveGenerator.GetAllMoves(board, color);
            foreach (IMove currentMove in possibleMoves)
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

