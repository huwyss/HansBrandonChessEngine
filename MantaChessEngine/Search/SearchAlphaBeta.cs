using System;
using System.Linq;
using System.Runtime.CompilerServices;
using static MantaChessEngine.Definitions;
using MantaCommon;
using log4net;

[assembly: InternalsVisibleTo("MantaChessEngineTest")]
namespace MantaChessEngine
{
    public class SearchAlphaBeta : ISearchService<IMove>
    {
        private const int AspirationWindowHalfSizeInitial = 50;

        private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private readonly IBoard _board;
        private readonly IMoveGenerator<IMove> _moveGenerator;
        private readonly IEvaluator _evaluator;
        private readonly IMoveOrder _moveOrder;
        private readonly IMoveFilter _moveFilter;
        private int _maxDepth;
        private int _additionalSelectiveDepth;
        private int _selectiveDepth;

        private IMoveRating<IMove> _previousPV;

        private int evaluatedPositions;
        private int _pruningCount;

        /// <summary>
        /// Set the search depth in plys (half moves).
        /// </summary>
        public void SetMaxDepth(int maxDepth)
        {
            _maxDepth = maxDepth > 0 ? maxDepth : 1;
            UpdateSelectiveDepth();
        }

        public void SetAdditionalSelectiveDepth(int additionalDepth)
        {
            _additionalSelectiveDepth = additionalDepth;
            UpdateSelectiveDepth();
        }

        private void UpdateSelectiveDepth()
        {
            _selectiveDepth = _additionalSelectiveDepth <= 0
                ? _maxDepth
                : (_maxDepth + _additionalSelectiveDepth) % 2 == 0
                    ? _maxDepth + _additionalSelectiveDepth
                    : _maxDepth + _additionalSelectiveDepth + 1;
        }
       
        public void ClearPreviousPV()
        {
            _previousPV = null;
        }

        public SearchAlphaBeta(IBoard board, IEvaluator evaluator, IMoveGenerator<IMove> moveGenerator, int maxDepth, IMoveOrder moveOrder, IMoveFilter moveFilter)
        {
            _board = board;
            _additionalSelectiveDepth = 0;
            _evaluator = evaluator;
            _moveGenerator = moveGenerator;
            _maxDepth = maxDepth;
            _moveOrder = moveOrder;
            _moveFilter = moveFilter;

            UpdateSelectiveDepth();
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
            _pruningCount = 0;
            evaluatedPositions = 0;

            (_moveOrder as OrderPvAndImportance)?.SetMoveRatingPV(_previousPV);
            _pruningCount = 0;
            evaluatedPositions = 0;

            var succeed = false;

            IMoveRating<IMove> moveRating = null;

            var windowHalfSize = AspirationWindowHalfSizeInitial;

            var alphaStart = _previousPV != null ? _previousPV.Score - windowHalfSize : int.MinValue;
            var betaStart = _previousPV != null ? _previousPV.Score + windowHalfSize : int.MaxValue;

            while (!succeed)
            {
                moveRating = SearchLevel(color, 1, alphaStart, betaStart);

                _log.Debug("evaluated positons: " + evaluatedPositions);
                moveRating.EvaluatedPositions = evaluatedPositions;
                moveRating.Depth = _maxDepth;
                moveRating.PruningCount = _pruningCount;

                _previousPV = moveRating;

                if (moveRating.Score >= betaStart)
                {
                    Console.WriteLine($"info Search failed high. Score >= BetaStart. Score: {moveRating.Score}, Alpha: {alphaStart}, Beta: {betaStart}");
                    windowHalfSize *= windowHalfSize / 5; // 50 -> 500 -> 50'000
                    betaStart += windowHalfSize;
                }
                else if (moveRating.Score <= alphaStart)
                {
                    Console.WriteLine($"info Search failed low. Score <= AlphaStart. Score: {moveRating.Score}, Alpha: {alphaStart}, Beta: {betaStart}");
                    windowHalfSize *= windowHalfSize / 5; // 50 -> 500 -> 50'000
                    alphaStart -= windowHalfSize;
                }
                else
                {
                    succeed = true;
                }
            }

            moveRating.SelectiveDepth = moveRating.PrincipalVariation.Count();
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
        internal virtual IMoveRating<IMove> SearchLevel(ChessColor color, int level, int alpha, int beta)
        {
            IMoveRating<IMove> bestRating = new MoveRating() { Score = InitWithWorstScorePossible(color) };
            IMoveRating<IMove> currentRating = new MoveRating();

            var allLegalMovesUnsortedUnfiltered = _moveGenerator.GetAllMoves(color).ToList<IMove>(); // todo should be only legal moves

            // no legal moves means the game is over. It is either stall mate or check mate.
            if (allLegalMovesUnsortedUnfiltered.Count() == 0)
            {
                return MakeMoveRatingForGameEnd(_board, color, level);
            }

            var allLegalMovesUnsorted = _moveFilter != null && level > _maxDepth
                ? _moveFilter.Filter(allLegalMovesUnsortedUnfiltered)
                : allLegalMovesUnsortedUnfiltered;

            var possibleMoves = _moveOrder != null
                ? _moveOrder.OrderMoves(allLegalMovesUnsorted, color, level)
                : allLegalMovesUnsorted;

            if (possibleMoves.Count() == 0)
            {
                return null;
            }

            foreach (IMove currentMove in possibleMoves)
            {
                _board.Move(currentMove);

                if (level < _maxDepth || (level < _selectiveDepth && currentMove.CapturedPiece != null)) // we need to do more move levels...
                //// if (level < _maxDepth)
                {
                    currentRating = SearchLevel(Helper.GetOppositeColor(color), level + 1, alpha, beta); // recursive...

                    if (currentRating == null) // we are in a level > maxdepth and tried to find a capture move but there was no capture move.
                    {
                        currentRating = new MoveRating() { Score = _evaluator.Evaluate(), EvaluationLevel = level };
                        evaluatedPositions++;
                    }

                    _board.Back();
                }
                else // we reached the bottom of the tree and evaluate the position
                {
                    currentRating.Score = _evaluator.Evaluate();
                    currentRating.EvaluationLevel = level;
                    evaluatedPositions++;
                    _board.Back();
                }

                // update the best move in the current level
                if (currentRating.IsBetter(color, bestRating))
                {
                    if (level == _maxDepth && currentRating.EvaluationLevel < bestRating.EvaluationLevel)
                    {
                        currentRating.PrincipalVariation.Clear(); // wir löschen zu oft. Warum werden meistens (?) alle auf depth > maxDepth gelöscht ?
                    }
                    currentRating.Move = currentMove;
                    bestRating = currentRating.Clone();
                }

                // Alpha Beta Pruning
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

            bestRating.Alpha = alpha;
            bestRating.Beta = beta;
            bestRating.PrincipalVariation.Insert(0, bestRating.Move);
            return bestRating;
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

        private MoveRating MakeMoveRatingForGameEnd(IBoard board, ChessColor color, int curentLevel)
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

