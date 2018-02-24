using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("MantaChessEngineTest")]
namespace MantaChessEngine
{
    class SearchMinimax : ISearchService
    {
        private MoveGenerator _moveGenerator;
        private IEvaluator _evaluator;
        private int _level;
        const int DEFAULT_LEVEL = 3;

        private static int evaluatedPositions;

        public SearchMinimax(IEvaluator evaluator, MoveGenerator moveGenerator)
        {
            _evaluator = evaluator;
            _moveGenerator = moveGenerator;
            _level = DEFAULT_LEVEL;
        }

        /// <summary>
        /// Set the number of moves that are calculated in advance.
        /// </summary>
        /// <param name="level">
        /// Level = 1 means: engine calculate 1 half move (if white's move: calculate move of white.)
        /// Level = 2 means: engine calculate 2 half moves (if white's move: calculate move of white and of black.)
        /// Level = 3 means: engine calculate 3 half moves (if white's move: calculate move of white, black, white)
        /// Level = 4 means: engine calculate 4 half moves (if white's move: calculate move of white, black, white, black)
        /// </param>
        public void SetLevel(int level)
        {
            _level = level;
        }

        /// <summary>
        /// Search for the best move.
        /// </summary>
        /// <param name="board">Board to be searched in</param>
        /// <param name="color">Color of next move</param>
        /// <param name="score">Score of endposition of the returned move.</param>
        /// <returns>best move for color.</returns>
        public IMove Search(Board board, Definitions.ChessColor color, out float score)
        {
            evaluatedPositions = 0;
            IMove move = SearchLevel(board, color, _level, out score);
            Console.WriteLine("evaluated positons: " + evaluatedPositions);
            return move;
        }

        /// <summary>
        /// Search best move. Calculate level number of moves.
        /// </summary>
        /// <param name="board">Board to be searched in</param>
        /// <param name="color">Color of next move</param>
        /// <param name="level">Number of levels to be searched (1=ie whites move, 2=moves of white, black, 3=move of white,black,white...</param>
        /// <param name="score">Score of position on current level</param>
        /// <returns></returns>
        internal IMove SearchLevel(Board board, Definitions.ChessColor color, int level, out float score)
        {
            IMove bestMove = null;
            float bestScore = InitWithWorstScorePossible(color);
            float currentScore;

            var possibleMoves = _moveGenerator.GetAllMoves(board, color);
            foreach (IMove currentMove in possibleMoves)
            {
                Board boardWithMove = board.Clone();
                boardWithMove.Move(currentMove);

                if (level > 1) // we need to do another move level...
                {
                    if (boardWithMove.IsWinner(color))
                    {
                        currentScore = InitWithWorstScorePossible(Helper.GetOpositeColor(color)); // oposit color has lost king
                    }
                    else
                    {
                        IMove moveRec = SearchLevel(boardWithMove, Helper.GetOpositeColor(color), level - 1, out currentScore);
                    }
                }
                else // we calculated all levels and reached the last position that we need to evaluate
                {
                    currentScore = _evaluator.Evaluate(boardWithMove);
                    evaluatedPositions++;
                }

                // update the best move in the current level
                if (IsBestMoveSofar(color, bestScore, currentScore))
                {
                    bestMove = currentMove;
                    bestScore = currentScore;
                }
            }

            score = bestScore;
            if (bestMove == null)
            {
                var factory = new MoveFactory();
                return factory.MakeNoLegalMove();
            }
            
            return bestMove;
        }

        private float InitWithWorstScorePossible(Definitions.ChessColor color)
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

