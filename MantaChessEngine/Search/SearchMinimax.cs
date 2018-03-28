using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using log4net;

[assembly: InternalsVisibleTo("MantaChessEngineTest")]
namespace MantaChessEngine
{
    public class SearchMinimax : ISearchService
    {
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private const float Tolerance = 0.05f;
        private IMoveGenerator _moveGenerator;
        private IEvaluator _evaluator;
        private int _maxDepth;

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
                _maxDepth = level;
            }
        }

        public SearchMinimax(IEvaluator evaluator, IMoveGenerator moveGenerator)
        {
            _evaluator = evaluator;
            _moveGenerator = moveGenerator;
            _maxDepth = Definitions.DEFAULT_MAXLEVEL;
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
            int currentLevel = _maxDepth;
            evaluatedPositions = 0;
            IEnumerable<MoveRating> moveRatings = SearchLevel(board, color, currentLevel);
            var count = moveRatings.Count();
            var randomIndex = _rand.Next(0, count);
            MoveRating rating = moveRatings.ElementAt(randomIndex);
            score = rating.Score;
            _log.Debug("evaluated positons: " + evaluatedPositions);
            return rating.Move;
        }

        /// <summary>
        /// Search best move. Calculate level number of moves.
        /// This method is recursive. condition to stop is 
        ///     1) we reached the max depth level or 
        ///     2) there are no legal moves in the current position (king is lost)
        /// </summary>
        /// <param name="board">Board to be searched in</param>
        /// <param name="color">Color of next move</param>
        /// <param name="level">Number of levels to be searched (1=ie whites move, 2=moves of white, black, 3=move of white,black,white...</param>
        /// <param name="score">Score of position on current level</param>
        /// <returns></returns>
        internal virtual IEnumerable<MoveRating> SearchLevel(IBoard board, Definitions.ChessColor color, int level)
        {
            float bestScore = InitWithWorstScorePossible(color);
            MoveRating currentRating = new MoveRating();
            List<MoveRating> bestMoveRatings = new List<MoveRating>();

            var possibleMoves = _moveGenerator.GetAllMoves(board, color);

            if (possibleMoves == null || possibleMoves.Count == 0)
            {
                // the playing color has just lost its king.
                // we did not reach the search depth yet. 
                // still, we do not evaluate and we go back up in the tree.
                return new MoveRating[]
                {
                    new MoveRating()
                    {
                        Score = color == Definitions.ChessColor.White ? Definitions.ScoreBlackWins : Definitions.ScoreWhiteWins,
                        //IsLegal = false,      // not legal, game is lost
                        IllegalMoveCount = 1, // there is no king
                        Move = new NoLegalMove()
                    }
                };
            }

            foreach (IMove currentMoveLoop in possibleMoves)
            {
                IMove currentMove = currentMoveLoop;
                board.Move(currentMove);

                if (level > 1) // we need to do more move levels...
                {
                    // we are only interested in the first score. all scores are the same.
                    currentRating = SearchLevel(board, Helper.GetOppositeColor(color), level - 1).First(); // recursive...
                    board.Back();
                }
                else // we reached the bottom of the tree and evaluate the position
                {
                    currentRating.Score = _evaluator.Evaluate(board);
                    //currentRating.IsLegal = true;
                    currentRating.IllegalMoveCount = -1;
                    evaluatedPositions++;
                    board.Back();

                    // the distinction between stall mate and check mate must be done 2 plys before king is lost (2 x back)
                    // If white is winning and its blacks move then black cannot move
                    // if black is winning and its whites move then white cannot move
                    // (assuming we are at the deepest level that we calculate. Search() will then try to 
                    // calculate a move for a less deeper level)
                    if (currentRating.Score == Definitions.ScoreWhiteWins || currentRating.Score == Definitions.ScoreBlackWins)
                    {
                        var factory = new MoveFactory();
                        currentRating.Move = factory.MakeNoLegalMove();
                        //currentRating.IsLegal = false;
                        currentRating.IllegalMoveCount = 1;
                    }
                }

                // update the best move in the current level
                if (IsEquallyGood(color, bestScore, currentRating.Score))
                {
                    currentRating.Move = /*(!currentRating.IsLegal) &&*/ 0 <= currentRating.IllegalMoveCount ? new NoLegalMove() : currentMove;
                    bestMoveRatings.Add(currentRating.Clone());
                }
                else if (IsBestMoveSofar(color, bestScore, currentRating.Score))
                {
                    currentRating.Move = /*(!currentRating.IsLegal) &&*/ 0 <= currentRating.IllegalMoveCount ? new NoLegalMove() : currentMove;
                    bestScore = currentRating.Score;
                    bestMoveRatings = new List<MoveRating> { currentRating.Clone() };
                }
                
            }

            // update score to 0 if it is stall mate
            foreach (var bestRating in bestMoveRatings)
            {
                if (bestRating.IllegalMoveCount == 0 && bestRating.Move is NoLegalMove)
                {
                    if (!_moveGenerator.IsCheck(board, color))
                    {
                        bestRating.Score = 0;
                    }
                }

                bestRating.IllegalMoveCount--;
            }

            
            return bestMoveRatings;
        }

        // Must return a worse score than the score for a lost game so that losing is better than the initialized best score.
        private float InitWithWorstScorePossible(Definitions.ChessColor color)
        {
            if (color == Definitions.ChessColor.White)
            {
                return Int32.MinValue;
            }
            else
            {
                return Int32.MaxValue;
            }
        }

        /// <summary>
        /// True if current score is as good as best within tolerance.
        /// </summary>
        private bool IsEquallyGood(Definitions.ChessColor color, float bestScoreSoFar, float currentScore)
        {
            return (currentScore < bestScoreSoFar + Tolerance && currentScore > bestScoreSoFar - Tolerance);
        }

        /// <summary>
        /// True if current score is better than best plus tolerance.
        /// </summary>
        /// <param name="color"></param>
        /// <param name="bestScoreSoFar"></param>
        /// <param name="currentScore"></param>
        /// <returns></returns>
        private bool IsBestMoveSofar(Definitions.ChessColor color, float bestScoreSoFar, float currentScore)
        {
            if (color == Definitions.ChessColor.White)
            {
                return (currentScore > bestScoreSoFar + Tolerance);
            }
            else
            {
                return (currentScore < bestScoreSoFar - Tolerance);
            }
        }
    }
}

