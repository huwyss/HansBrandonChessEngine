﻿using System;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using log4net;

[assembly: InternalsVisibleTo("HansBrandonBitboardEngineTest")]
[assembly: InternalsVisibleTo("HansBrandonChessEngineTest")]
namespace HBCommon
{
    public class GenericSearchAlphaBeta<TMove> : ISearchService<TMove>
        where TMove : IGenericMove
    {
        private const int AspirationWindowHalfSizeInitial = 100;

        private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private readonly ISearchableBoard<TMove> _board;
        private readonly IMoveGenerator<TMove> _moveGenerator;
        private readonly IEvaluator _evaluator;
        private readonly IHashtable _hashtable;
        private readonly IMoveFactory<TMove> _moveFactory;
        private readonly IMoveRatingFactory<TMove> _moveRatingFactory;

        private int _maxDepth;
        private int _additionalSelectiveDepth;
        private int _selectiveDepth;

        private IMoveRating<TMove> _previousPV;

        private int evaluatedPositions;
        private int _pruningCount;

        private bool _abortSearch;

        public GenericSearchAlphaBeta(ISearchableBoard<TMove> board, IEvaluator evaluator, IMoveGenerator<TMove> moveGenerator, IHashtable hashtable, IMoveFactory<TMove> moveFactory, IMoveRatingFactory<TMove> moveRatingFactory, int maxDepth)
        {
            _board = board;
            _additionalSelectiveDepth = 0;
            _evaluator = evaluator;
            _moveGenerator = moveGenerator;
            _hashtable = hashtable;
            _moveFactory = moveFactory;
            _moveRatingFactory = moveRatingFactory;

            _maxDepth = maxDepth;
            _abortSearch = false;

            UpdateSelectiveDepth();
        }

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

        // Note: _selectiveDepth <= 0
        private void UpdateSelectiveDepth()
        {
            _selectiveDepth = _additionalSelectiveDepth <= 0
                ? 0
                : (_maxDepth + _additionalSelectiveDepth) % 2 == 0
                    ? -_additionalSelectiveDepth
                    : -_additionalSelectiveDepth - 1;
        }

        public void ClearPreviousPV()
        {
            _previousPV = null;
        }

        /// <summary>
        /// Search for the best move.
        /// </summary>
        /// <param name="board">Board to be searched in</param>
        /// <param name="color">Color of next move</param>
        /// <param name="score">Score of endposition of the returned move.</param>
        /// <returns>best move for color.</returns>
        public IMoveRating<TMove> Search(ChessColor color)
        {
            try
            {
                _abortSearch = false;
                _pruningCount = 0;
                evaluatedPositions = 0;

                var succeed = false;

                IMoveRating<TMove> moveRating = null;

                var windowHalfSize = AspirationWindowHalfSizeInitial;

                var alphaStart = _previousPV != null ? _previousPV.Score - windowHalfSize : int.MinValue;
                var betaStart = _previousPV != null ? _previousPV.Score + windowHalfSize : int.MaxValue;

                while (!succeed)
                {
                    moveRating = SearchLevel(color, _maxDepth, alphaStart, betaStart);

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
            catch (HansBrandonSearchAbortedException ex)
            {
                var level = ex.AbortedOnLevel;
                for (var i = 1; i < level; i++)
                {
                    _board.Back();
                }

                return _moveRatingFactory.CreateMoveRatingSearchAborted();
            }
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
        internal virtual IMoveRating<TMove> SearchLevel(ChessColor color, int level, int alpha, int beta)
        {
            if (_abortSearch)
            {
                throw new HansBrandonSearchAbortedException($"Aborted Search on level {level}", level);
            }

            IMoveRating<TMove> bestRating = _moveRatingFactory.CreateMoveRatingWithWorstScore(color);
            IMoveRating<TMove> currentRating = _moveRatingFactory.CreateMoveRating();

            var hasLegalMoves = false; // we do not know yet if there are legal moves

            List<TMove> movesToEvaluate;
            if (level > 0)
            {
                movesToEvaluate = _moveGenerator.GetAllMoves(color).ToList();
            }
            else
            {
                movesToEvaluate = _moveGenerator.GetAllCaptures(color).ToList();
                if (movesToEvaluate.Count() == 0)
                {
                    return null;
                }
            }

            var movePVHash = _hashtable.LookupPvMove(color);
            if (movePVHash != null)
            {
                var currentPvMove = _moveFactory.MakeMove(movePVHash.From, movePVHash.To, movePVHash.PromotionPiece);
                if (movesToEvaluate.Remove(currentPvMove))
                {
                    movesToEvaluate.Insert(0, currentPvMove);
                }
            }

            // Evaluate the previousPV of the current level first
            ////if (_previousPV != null && _previousPV.PrincipalVariation.Count > level)
            ////{
            ////    var currentPvMove = _previousPV.PrincipalVariation[level - 1];
            ////    if (currentPvMove != null && movesToEvaluate.Contains(currentPvMove))
            ////    {
            ////        movesToEvaluate.Remove(currentPvMove);
            ////        movesToEvaluate.Insert(0, currentPvMove); // we start with the stored pv from the last search (that was one ply less deep)
            ////        _previousPV.PrincipalVariation[level - 1] = null; // we have used this move up.
            ////    }
            ////}

            foreach (var currentMove in movesToEvaluate)
            {
                _board.Move(currentMove);
                if (_moveGenerator.IsCheck(currentMove.MovingColor))
                {
                    _board.Back();
                    continue;
                }

                hasLegalMoves = true;

                if (level > 1 || (level > _selectiveDepth + 1 && currentMove.IsCapture())) // we need to do more move levels...
                //// if (level > 1)
                //// if (level > _selectiveDepth + 1)
                {
                    currentRating = SearchLevel(CommonHelper.OtherColor(color), level - 1, alpha, beta); // recursive...

                    if (currentRating == null) // we are in a level > maxdepth and tried to find a capture move but there was no capture move.
                    {
                        currentRating = _moveRatingFactory.CreateMoveRating(_evaluator.Evaluate(), -level + _maxDepth + 1);
                        evaluatedPositions++;
                    }

                    _board.Back();
                }
                else // we reached the bottom of the tree and evaluate the position
                {
                    currentRating.Score = _evaluator.Evaluate();
                    currentRating.EvaluationLevel = -level + _maxDepth + 1;
                    evaluatedPositions++;
                    _board.Back();
                }

                // update the best move in the current level
                if (currentRating.IsBetter(color, bestRating))
                {
                    if (level == 0 && currentRating.EvaluationLevel > bestRating.EvaluationLevel)
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

            // no legal moves means the game is over. It is either stall mate or check mate.
            if (!hasLegalMoves)
            {
                return _moveRatingFactory.CreateMoveRatingForGameEnd(color, -level + _maxDepth + 1);
            }

            bestRating.Alpha = alpha;
            bestRating.Beta = beta;
            bestRating.PrincipalVariation.Insert(0, bestRating.Move);

            _hashtable.AddHash(color, level, bestRating.Score, HashEntryType.Exact, bestRating.Move.FromSquare, bestRating.Move.ToSquare, bestRating.Move.PromotionPiece);

            return bestRating;
        }

        public void AbortSearch()
        {
            _abortSearch = true;
        }
    }
}

