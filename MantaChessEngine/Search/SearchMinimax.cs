using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("MantaChessEngineTest")]
namespace MantaChessEngine
{
    public class SearchMinimax : ISearchService
    {
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        
        private IMoveGenerator _moveGenerator;
        private IEvaluator _evaluator;
        private int _maxDepth;

        private Random _rand;
        private readonly MoveFactory _moveFactory;
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

        public SearchMinimax(IEvaluator evaluator, IMoveGenerator moveGenerator, int maxDepth = Definitions.DEFAULT_MAXLEVEL)
        {
            _evaluator = evaluator;
            _moveGenerator = moveGenerator;
            _maxDepth = maxDepth;
            _rand = new Random();
            _moveFactory = new MoveFactory();
        }

        /// <summary>
        /// Search for the best move.
        /// </summary>
        /// <param name="board">Board to be searched in</param>
        /// <param name="color">Color of next move</param>
        /// <param name="score">Score of endposition of the returned move.</param>
        /// <returns>best move for color.</returns>
        public IMove Search(IBoard board, Definitions.ChessColor color, out float score)
        {
            evaluatedPositions = 0;
            IEnumerable<MoveRating> moveRatings = SearchLevel(board, color, 1);
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
            var bestRating = new MoveRating() { Score = InitWithWorstScorePossible(color) };
            MoveRating currentRating = new MoveRating();
            List<MoveRating> bestMoveRatings = new List<MoveRating>();

            var possibleMoves = _moveGenerator.GetLegalMoves(board, color);

            // no legal moves means the game is over. It is either stall mate or check mate.
            if (possibleMoves == null || possibleMoves.Count == 0)
            {
                return new MoveRating[]
                {
                        new MoveRating()
                        {
                            Score = !_moveGenerator.IsCheck(board, color)
                            ? 0
                            : color == Definitions.ChessColor.White ? Definitions.ScoreBlackWins : Definitions.ScoreWhiteWins,
                            Move = new NoLegalMove(),
                            GameEndLevel = level
                        }
                };
            }

            foreach (IMove currentMove in possibleMoves)
            {
                board.Move(currentMove);

                if (level < _maxDepth) // we need to do more move levels...
                {
                    // we are only interested in the first score. all scores are the same.
                    currentRating = SearchLevel(board, Helper.GetOppositeColor(color), level + 1).First(); // recursive...
                    board.Back();
                }
                else // we reached the bottom of the tree and evaluate the position
                {
                    currentRating.Score = _evaluator.Evaluate(board);
                    currentRating.GameEndLevel = level;
                    evaluatedPositions++;
                    board.Back();
                }

                // update the best move in the current level
                if (currentRating.IsEquallyGood(bestRating))
                {
                    currentRating.Move = currentMove;
                    bestMoveRatings.Add(currentRating.Clone());
                }
                else if (currentRating.IsBetter(color, bestRating))
                {
                    currentRating.Move = currentMove;
                    bestRating = currentRating.Clone();
                    bestMoveRatings = new List<MoveRating> { currentRating.Clone() };
                }
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
    }
}

