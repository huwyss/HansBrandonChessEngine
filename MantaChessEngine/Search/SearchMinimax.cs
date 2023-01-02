using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using static MantaChessEngine.Definitions;
using MantaCommon;
using log4net;

[assembly: InternalsVisibleTo("MantaChessEngineTest")]
namespace MantaChessEngine
{
    /// <summary>
    /// Full search until level _maxDepth.
    /// </summary>
    public class SearchMinimax : ISearchService<IMove>
    {
        private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private IBoard _board;
        private IMoveGenerator<IMove> _moveGenerator;
        private IEvaluator _evaluator;
        private int _maxDepth;

        private Random _rand;
        private static int evaluatedPositions;

        /// <summary>
        /// Set the search depth in plys (half moves).
        /// </summary>
        public void SetMaxDepth(int maxDepth)
        {
            _maxDepth = maxDepth > 0 ? _maxDepth = maxDepth : 1;
        }

        public void SetAdditionalSelectiveDepth(int additionalDepth)
        {
        }

        public SearchMinimax(IBoard board, IEvaluator evaluator, IMoveGenerator<IMove> moveGenerator, int maxDepth = Definitions.DEFAULT_MAXLEVEL)
        {
            _board = board;
            _evaluator = evaluator;
            _moveGenerator = moveGenerator;
            _maxDepth = maxDepth;
            _rand = new Random();
        }

        /// <summary>
        /// Search for the best move.
        /// </summary>
        /// <param name="board">Board to be searched in</param>
        /// <param name="color">Color of next move</param>
        /// <param name="score">Score of endposition of the returned move.</param>
        /// <returns>best move for color.</returns>
        public IMoveRating<IMove> Search(ChessColor color)
        {
            evaluatedPositions = 0;
            IEnumerable<IMoveRating<IMove>> moveRatings = SearchLevel(color, 1);
            var count = moveRatings.Count();
            var randomIndex = _rand.Next(0, count);
            IMoveRating<IMove> rating = moveRatings.ElementAt(randomIndex);
            rating.EvaluatedPositions = evaluatedPositions;
            rating.Depth = _maxDepth;
            _log.Debug("evaluated positons: " + evaluatedPositions);
            return rating;
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
        internal virtual IEnumerable<IMoveRating<IMove>> SearchLevel(ChessColor color, int level)
        {
            IMoveRating<IMove> bestRating = new MoveRating() { Score = InitWithWorstScorePossible(color) };
            IMoveRating<IMove> currentRating = new MoveRating();
            List<IMoveRating<IMove>> bestMoveRatings = new List<IMoveRating<IMove>>();

            var hasLegalMoves = false;

            var possibleMoves = _moveGenerator.GetAllMoves(color).ToList<IMove>(); // todo only legal moves

            foreach (IMove currentMove in possibleMoves)
            {
                _board.Move(currentMove);
                if (_moveGenerator.IsCheck(currentMove.MovingColor))
                {
                    _board.Back();
                    continue;
                }

                hasLegalMoves = true;

                if (level < _maxDepth) // we need to do more move levels...
                {
                    // we are only interested in the first score. all scores are the same.
                    currentRating = SearchLevel(Helper.GetOppositeColor(color), level + 1).First(); // recursive...
                    _board.Back();
                }
                else // we reached the bottom of the tree and evaluate the position
                {
                    currentRating.Score = _evaluator.Evaluate();
                    evaluatedPositions++;
                    _board.Back();
                }

                // update the best move in the current level
                if (currentRating.IsEquallyGood(bestRating))
                {
                    currentRating.Move = currentMove;
                    bestMoveRatings.Add(currentRating.Clone());
                }
                else
                if (currentRating.IsBetter(color, bestRating))
                {
                    currentRating.Move = currentMove;
                    bestRating = currentRating.Clone();
                    bestMoveRatings = new List<IMoveRating<IMove>> { currentRating.Clone() };
                }
            }

            // no legal moves means the game is over. It is either stall mate or check mate.
            if (!hasLegalMoves)
            {
                return MakeMoveRatingForGameEnd(_board, color, level);
            }

            bestRating.PrincipalVariation.Insert(0, bestRating.Move);
            return bestMoveRatings;
        }

        // Must return a worse score than the score for a lost game so that losing is better than the initialized best score.
        private int InitWithWorstScorePossible(ChessColor color)
        {
            if (color == ChessColor.White)
            {
                return int.MinValue;
            }
            else
            {
                return int.MaxValue;
            }
        }

        private IEnumerable<MoveRating> MakeMoveRatingForGameEnd(IBoard board, ChessColor color, int curentLevel)
        {
            int score;
            bool whiteWins = false;
            bool blackWins = false;
            bool stallmate = false;

            if (_moveGenerator.IsCheck(color))
            {
                if (color == ChessColor.White)
                {
                    score = ScoreBlackWins + curentLevel * SignificantFactor;
                    blackWins = true;
                }
                else
                {
                    score = ScoreWhiteWins - curentLevel * SignificantFactor;
                    whiteWins = true;
                }
            }
            else
            {
                score = 0;
                stallmate = true;
            }

            return new MoveRating[]
                {
                    new MoveRating()
                    {
                        Score = score,
                        WhiteWins = whiteWins,
                        BlackWins = blackWins,
                        Stallmate = stallmate,
                        Move = new NoLegalMove(),
                    }
                };
        }

        public void ClearPreviousPV()
        {
        }
    }
}

