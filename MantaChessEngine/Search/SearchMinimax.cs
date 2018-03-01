using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("MantaChessEngineTest")]
namespace MantaChessEngine
{
    public class SearchMinimax : ISearchService
    {
        private IMoveGenerator _moveGenerator;
        private IEvaluator _evaluator;
        private int _level;

        private Random _rand;

        private static int evaluatedPositions;

        /// <summary>
        /// Set the number of moves that are calculated in advance.
        /// </summary>
        /// <param name="level">
        /// Level = 1 means: engine calculate 1 half move (if white's move: calculate move of white.)
        /// Level = 2 means: engine calculate 2 half moves (if white's move: calculate move of white and of black.)
        /// Level = 3 means: engine calculate 3 half moves (if white's move: calculate move of white, black, white)
        /// Level = 4 means: engine calculate 4 half moves (if white's move: calculate move of white, black, white, black)
        /// </param>
        public void SetMaxDepth(int level)
        {
            if (level > 0)
            {
                _level = level;
            }
        }

        public SearchMinimax(IEvaluator evaluator, IMoveGenerator moveGenerator)
        {
            _evaluator = evaluator;
            _moveGenerator = moveGenerator;
            _level = Definitions.DEFAULT_MAXLEVEL;
            _rand = new Random();
        }

        // Explanation:

        // ceckmate now --> SearchLevel(2) --> NoLegalMove

        // checkmate in 2  --> SearchLevel(2) --> normal move
        //                 --> SearchLevel(4) --> NoLegalMove

        // checkmate in 4  --> SearchLevel(2) --> normal move
        //                 --> SearchLevel(4) --> normal Move
        //                 --> SearchLevel(6) --> NoLegalMove

        /// <summary>
        /// Search for the best move.
        /// </summary>
        /// <param name="board">Board to be searched in</param>
        /// <param name="color">Color of next move</param>
        /// <param name="score">Score of endposition of the returned move.</param>
        /// <returns>best move for color.</returns>
        public IMove Search(IBoard board, Definitions.ChessColor color, out float score)
        {
            int currentLevel = _level;
            evaluatedPositions = 0;
            IMove move;

            // try finding a legal move. Play until actually check mate.
            // Example: If it was not legal for level 4 try find a move for level 2
            do
            {
                move = SearchLevel(board, color, currentLevel, out score);
                currentLevel = currentLevel - (currentLevel % 2)- 2;
            } while (move is NoLegalMove && currentLevel > 0);

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
        internal virtual IMove SearchLevel(IBoard board, Definitions.ChessColor color, int level, out float score)
        {
            IMove bestMove = null; // todo should probably be NoLegalMove !?
            float bestScore = InitWithWorstScorePossible(color);
            float currentScore;

            var possibleMoves = _moveGenerator.GetAllMoves(board, color);
            foreach (IMove currentMove in possibleMoves)
            {
                board.Move(currentMove);

                if (level > 1) // we need to do more move levels...
                {
                    // todo: dies ist ein Spezialfall einer Optimierung, die mit einem besseren Suchalgorithmus nicht mehr nötig ist!
                    //if (board.IsWinner(color))
                    //{
                    //    currentScore = InitWithWorstScorePossible(Helper.GetOppositeColor(color)); // opposite color has lost king
                    //    board.Back();
                    //}
                    //else
                    {
                        IMove moveRec = SearchLevel(board, Helper.GetOppositeColor(color), level - 1, out currentScore);
                        board.Back();
                    }
                }
                else // we calculated all levels and reached the last position that we need to evaluate
                {
                    currentScore = _evaluator.Evaluate(board);
                    board.Back();
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
            if (bestScore > 1000 && color == Definitions.ChessColor.Black ||
                bestScore < -1000 && color == Definitions.ChessColor.White)
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
                //else if (currentScore == bestScoreSoFar)
                //{
                //    return _rand.Next(0, 2) == 0;
                //}
            }
            else
            {
                if (currentScore < bestScoreSoFar)
                {
                    return true;
                }
                //else if (currentScore == bestScoreSoFar)
                //{
                //    return _rand.Next(0, 2) == 0;
                //}
            }

            return false;
        }
    }
}

