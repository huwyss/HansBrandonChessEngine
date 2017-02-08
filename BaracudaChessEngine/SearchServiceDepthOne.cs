using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("BaracudaChessEngineTest")]
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
            float bestScoreBlack = InitBestScoreSofar(Helper.GetOpositeColor(color));
            float bestScoreWhite = InitBestScoreSofar(color);
            Move bestMoveWhite = null;

            var possibleMoves = board.GetAllMoves(color);
            foreach (Move currentMove in possibleMoves)
            {
                Board boardWithMove = board.Clone();
                boardWithMove.Move(currentMove);

                Move bestMoveBlack = CalcScoreScoreOnNextLevel(boardWithMove, Helper.GetOpositeColor(color), out bestScoreBlack);
                if (IsBestMoveSofar(color, bestScoreWhite, bestScoreBlack))
                {
                    bestScoreWhite = bestScoreBlack;
                    bestMoveWhite = currentMove;
                }
            }

            return bestMoveWhite;
        }

        internal Move CalcScoreScoreOnNextLevel(Board board, Definitions.ChessColor color, out float score)
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

