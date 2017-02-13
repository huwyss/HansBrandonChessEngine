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

        public IMove Search(Board board, Definitions.ChessColor color, out float score)
        {
            float bestScoreSecondMover = InitBestScoreSofar(Helper.GetOpositeColor(color));
            float bestScoreFirstMover = InitBestScoreSofar(color);
            Move bestMoveFirstMover = null;

            var possibleMoves = board.GetAllMoves(color);
            foreach (Move currentMove in possibleMoves)
            {
                Board boardWithMove = board.Clone();
                boardWithMove.Move(currentMove);

                Move bestMoveSecondMover = CalcScoreLevelZero(boardWithMove, Helper.GetOpositeColor(color), out bestScoreSecondMover);
                if (IsBestMoveSofar(color, bestScoreFirstMover, bestScoreSecondMover))
                {
                    bestScoreFirstMover = bestScoreSecondMover;
                    bestMoveFirstMover = currentMove;
                }
            }

            score = bestScoreFirstMover;
            return bestMoveFirstMover;
        }

        //private Move SearchLevel(Board board, Definitions.ChessColor color, int level, out float score)
        //{
        //    score = 0;
        //    Move moveZero;
        //    float scoreZero = 0;
        //    level--;
        //    if (level == 0)
        //    {
        //        moveZero = CalcScoreLevelZero(board, color, out scoreZero); // Calculate the score of the final position

        //    }
        //    else if ()
        //    {
                
        //    }

        //}

        internal Move CalcScoreLevelZero(Board board, Definitions.ChessColor color, out float score)
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

