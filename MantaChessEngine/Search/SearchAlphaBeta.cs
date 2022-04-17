using System;
using System.Linq;
using System.Runtime.CompilerServices;
using static MantaChessEngine.Definitions;
using log4net;

[assembly: InternalsVisibleTo("MantaChessEngineTest")]
namespace MantaChessEngine
{
    public class SearchAlphaBeta : ISearchService
    {
        private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        
        private readonly IMoveGenerator _moveGenerator;
        private readonly IEvaluator _evaluator;
        private readonly IMoveOrder _moveOrder;
        private int _maxDepth;

        private MoveRating _previousPV;

        private int evaluatedPositions;
        private int _pruningCount;

        /// <summary>
        /// Set the search depth in plys (half moves).
        /// </summary>
        public void SetMaxDepth(int ply)
        {
            _maxDepth = ply > 0 ? _maxDepth = ply : 1;
        }

        public SearchAlphaBeta(IEvaluator evaluator, IMoveGenerator moveGenerator, int maxDepth, IMoveOrder moveOrder)
        {
            _evaluator = evaluator;
            _moveGenerator = moveGenerator;
            _maxDepth = maxDepth;
            _moveOrder = moveOrder;
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
            _pruningCount = 0;
            evaluatedPositions = 0;

            (_moveOrder as MoveOrderPV)?.SetMoveRatingPV(_previousPV);
            _pruningCount = 0;
            evaluatedPositions = 0;

            var moveRating = SearchLevel(board, color, 1, float.MinValue, float.MaxValue);

            _log.Debug("evaluated positons: " + evaluatedPositions);
            moveRating.EvaluatedPositions = evaluatedPositions;
            moveRating.Depth = _maxDepth;
            moveRating.PruningCount = _pruningCount;

            _previousPV = moveRating;

            return moveRating;
        }

        /// <summary>
        /// Search best move. Calculate level number of moves.
        /// This method is recursive. condition to stop is 
        ///     1) we reached the max depth level or 
        ///     2) there are no legal moves in the current position
        /// </summary>
        /// <param name="board">Board to be searched in</param>
        /// <param name="color">Color of next move</param>
        /// <param name="level">Start level of search </param>
        internal virtual MoveRating SearchLevel(IBoard board, ChessColor color, int level, float alpha, float beta)
        {
            var bestRating = new MoveRating() { Score = InitWithWorstScorePossible(color) };
            MoveRating currentRating = new MoveRating();

            var possibleMovesUnsorted = _moveGenerator.GetLegalMoves(board, color);

            var possibleMoves = _moveOrder != null
                ? _moveOrder.OrderMoves(possibleMovesUnsorted, color, level)
                : possibleMovesUnsorted;

            // no legal moves means the game is over. It is either stall mate or check mate.
            if (possibleMoves.Count() == 0)
            {
                return MakeMoveRatingForGameEnd(board, color, level);
            }

            foreach (IMove currentMove in possibleMoves)
            {
                board.Move(currentMove);

                if (level < _maxDepth) // we need to do more move levels...
                {
                    // we are only interested in the first score. all scores are the same.
                    currentRating = SearchLevel(board, Helper.GetOppositeColor(color), level + 1, alpha, beta); // recursive...
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
                if (currentRating.IsBetterFaster(color, bestRating))
                {
                    currentRating.Move = currentMove;
                    bestRating = currentRating.Clone();
                }

                if (color == ChessColor.White)
                {
                    alpha = Math.Max(currentRating.Score, alpha);
                    if (beta <= alpha)
                    {
                        _pruningCount++;
                        break;
                    }
                }
                else
                {
                    beta = Math.Min(currentRating.Score, beta);
                    if (beta <= alpha)
                    {
                        _pruningCount++;
                        break;
                    }
                }
            }

            bestRating.PrincipalVariation.Insert(0, bestRating.Move);
            return bestRating;
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

        private MoveRating MakeMoveRatingForGameEnd(IBoard board, ChessColor color, int curentLevel)
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

            return new MoveRating()
            {
                Score = score,
                WhiteWins = whiteWins,
                BlackWins = blackWins,
                Stallmate = stallmate,
                Move = new NoLegalMove(),
            };
        }
    }
}

