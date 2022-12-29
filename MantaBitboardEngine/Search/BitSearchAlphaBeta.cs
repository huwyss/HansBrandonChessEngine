using System;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MantaCommon;
using log4net;

////[assembly: InternalsVisibleTo("MantaChessEngineTest")]
namespace MantaBitboardEngine
{
    public class BitSearchAlphaBeta
    {
        private const int AspirationWindowHalfSizeInitial = 10;

        private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private readonly BitMoveGenerator _moveGenerator;
        private readonly BitEvaluator _evaluator;
        private int _maxDepth;
        private int _additionalSelectiveDepth;
        private int _selectiveDepth;

        private BitMoveRating _previousPV;

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

        public BitSearchAlphaBeta(BitEvaluator evaluator, BitMoveGenerator moveGenerator, int maxDepth) ////, IMoveOrder moveOrder, IMoveFilter moveFilter)
        {
            _additionalSelectiveDepth = 0;
            _evaluator = evaluator;
            _moveGenerator = moveGenerator;
            _maxDepth = maxDepth;

            UpdateSelectiveDepth();
        }

        /// <summary>
        /// Search for the best move.
        /// </summary>
        /// <param name="board">Board to be searched in</param>
        /// <param name="color">Color of next move</param>
        /// <param name="score">Score of endposition of the returned move.</param>
        /// <returns>best move for color.</returns>
        public BitMoveRating Search(Bitboards board, ChessColor color)
        {
            _pruningCount = 0;
            evaluatedPositions = 0;

            _pruningCount = 0;
            evaluatedPositions = 0;

            var succeed = false;

            BitMoveRating moveRating = null;

            var windowHalfSize = AspirationWindowHalfSizeInitial;

            var alphaStart = _previousPV != null ? _previousPV.Score - windowHalfSize : int.MinValue;
            var betaStart = _previousPV != null ? _previousPV.Score + windowHalfSize : int.MaxValue;

            while (!succeed)
            {
                moveRating = SearchLevel(board, color, 1, alphaStart, betaStart);

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
        internal virtual BitMoveRating SearchLevel(Bitboards board, ChessColor color, int level, int alpha, int beta)
        {
            var bestRating = new BitMoveRating() { Score = InitWithWorstScorePossible(color) };
            var currentRating = new BitMoveRating();

            var hasLegalMoves = false; // we do not know yet if there are legal moves

            List<BitMove> movesToEvaluate;
            if (level <= _maxDepth)
            {
                movesToEvaluate = _moveGenerator.GetAllMoves(color).ToList();
            }
            else
            {
                movesToEvaluate = _moveGenerator.GetCaptures(color).ToList();
                if (movesToEvaluate.Count() == 0)
                {
                    return null;
                }
            }

            // Evaluate the previousPV of the current level first
            if (_previousPV != null && _previousPV.PrincipalVariation.Count > level)
            {
                var currentPvMove = _previousPV.PrincipalVariation[level - 1];
                if (currentPvMove != null && movesToEvaluate.Contains(currentPvMove))
                {
                    movesToEvaluate.Remove(currentPvMove);
                    movesToEvaluate.Insert(0, currentPvMove); // we start with the stored pv from the last search (that was one ply less deep)
                    _previousPV.PrincipalVariation[level - 1] = null; // we have used this move up.
                }
            }

            foreach (var currentMove in movesToEvaluate)
            {
                board.Move(currentMove);
                if (_moveGenerator.IsCheck(currentMove.MovingColor))
                {
                    board.Back();
                    continue;
                }

                hasLegalMoves = true;

                if (level < _maxDepth || (level < _selectiveDepth && currentMove.CapturedPiece != BitPieceType.Empty)) // we need to do more move levels...
                //// if (level < _maxDepth)
                {
                    currentRating = SearchLevel(board, CommonHelper.OtherColor(color), level + 1, alpha, beta); // recursive...

                    if (currentRating == null) // we are in a level > maxdepth and tried to find a capture move but there was no capture move.
                    {
                        currentRating = new BitMoveRating() { Score = _evaluator.Evaluate(board), EvaluationLevel = level };
                        evaluatedPositions++;
                    }

                    board.Back();
                }
                else // we reached the bottom of the tree and evaluate the position
                {
                    currentRating.Score = _evaluator.Evaluate(board);
                    currentRating.EvaluationLevel = level;
                    evaluatedPositions++;
                    board.Back();
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

            if (!hasLegalMoves)
            {
                return MakeMoveRatingForGameEnd(board, color, level);
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

        private BitMoveRating MakeMoveRatingForGameEnd(Bitboards board, ChessColor color, int curentLevel)
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

            return new BitMoveRating()
            {
                Score = score,
                WhiteWins = whiteWins,
                BlackWins = blackWins,
                Stallmate = stallmate,
                Move = BitMove.CreateEmptyMove(), /// new NoLegalMove(),
            };
        }

        public const int ScoreWhiteWins = 10000;
        public const int ScoreBlackWins = -10000;
        public const int SignificantFactor = 8; // fast multiplier
    }
}

