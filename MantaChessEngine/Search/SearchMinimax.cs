using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using static MantaChessEngine.Definitions;
using log4net;

[assembly: InternalsVisibleTo("MantaChessEngineTest")]
namespace MantaChessEngine
{
    /// <summary>
    /// Full search until level _maxDepth.
    /// </summary>
    public class SearchMinimax : ISearchService
    {
        private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        
        private IMoveGenerator _moveGenerator;
        private IEvaluator _evaluator;
        private int _maxDepth;

        private Random _rand;
        private static int evaluatedPositions;

        /// <summary>
        /// Set the search depth in plys (half moves).
        /// </summary>
        public void SetMaxDepth(int ply)
        {
            _maxDepth = ply > 0 ? _maxDepth = ply : 1;
        }

        public SearchMinimax(IEvaluator evaluator, IMoveGenerator moveGenerator, int maxDepth = Definitions.DEFAULT_MAXLEVEL)
        {
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
        public MoveRating Search(IBoard board, ChessColor color)
        {
            evaluatedPositions = 0;
            IEnumerable<MoveRating> moveRatings = SearchLevel(board, color, 1);
            var count = moveRatings.Count();
            var randomIndex = _rand.Next(0, count);
            MoveRating rating = moveRatings.ElementAt(randomIndex);
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
        internal virtual IEnumerable<MoveRating> SearchLevel(IBoard board, ChessColor color, int level)
        {
            var bestRating = new MoveRating() { Score = InitWithWorstScorePossible(color) };
            MoveRating currentRating = new MoveRating();
            List<MoveRating> bestMoveRatings = new List<MoveRating>();

            var possibleMoves = _moveGenerator.GetLegalMoves(board, color);

            // no legal moves means the game is over. It is either stall mate or check mate.
            if (possibleMoves.Count == 0)
            {
                return MakeMoveRatingForGameEnd(board, color, level);
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
                    evaluatedPositions++;
                    board.Back();
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
                    bestMoveRatings = new List<MoveRating> { currentRating.Clone() };
                }
            }

            bestRating.PrincipalVariation.Insert(0, bestRating.Move);
            return bestMoveRatings;
        }

        // Must return a worse score than the score for a lost game so that losing is better than the initialized best score.
        private float InitWithWorstScorePossible(ChessColor color)
        {
            if (color == ChessColor.White)
            {
                return float.MinValue;
            }
            else
            {
                return float.MaxValue;
            }
        }

        private IEnumerable<MoveRating> MakeMoveRatingForGameEnd(IBoard board, ChessColor color, int curentLevel)
        {
            float score;
            bool whiteWins = false;
            bool blackWins = false;
            bool stallmate = false;

            if (_moveGenerator.IsCheck(board, color))
            {
                if (color == ChessColor.White)
                {
                    score = ScoreBlackWins + curentLevel;
                    blackWins = true;
                }
                else
                {
                    score = ScoreWhiteWins - curentLevel;
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

    }
}

