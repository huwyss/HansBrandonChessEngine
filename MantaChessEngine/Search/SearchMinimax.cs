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

        public SearchMinimax(IEvaluator evaluator, IMoveGenerator moveGenerator)
        {
            _evaluator = evaluator;
            _moveGenerator = moveGenerator;
            _maxDepth = Definitions.DEFAULT_MAXLEVEL;
            _rand = new Random();
            _moveFactory = new MoveFactory();
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
            var bestRating = new MoveRating() { Score = InitWithWorstScorePossible(color) };
            MoveRating currentRating = new MoveRating();
            List<MoveRating> bestMoveRatings = new List<MoveRating>();

            var possibleMoves = _moveGenerator.GetAllMoves(board, color);

            if (possibleMoves == null || possibleMoves.Count == 0)
            {
                // The playing color has just lost its king.
                // We did not reach the search depth yet.
                // still, we do not evaluate and we go back up in the tree.
                return new MoveRating[]
                {
                        new MoveRating()
                        {
                            Score = color == Definitions.ChessColor.White ? Definitions.ScoreBlackWins : Definitions.ScoreWhiteWins,
                            IllegalMoveCount = 1, // there is no king
                            ////KingCapturedAtLevel = level,
                            ////CapturedKing =  Helper.GetOppositeColor(color),
                            Move = new NoLegalMove()
                        }
                };
            }

            foreach (IMove currentMove in possibleMoves)
            {
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
                        currentRating.Move = _moveFactory.MakeNoLegalMove();
                        currentRating.IllegalMoveCount = 1;
                        ////currentRating.KingCapturedAtLevel = level;
                        ////currentRating.CapturedKing = Helper.GetOppositeColor(color);
                    }
                }

                // update the best move in the current level
                if (IsEquallyGood(color, bestRating, currentRating))
                {
                    ////currentRating.Move = 0 < currentRating.KingCapturedAtLevel ? new NoLegalMove() : currentMove;

                    currentRating.Move = 0 <= currentRating.IllegalMoveCount ? new NoLegalMove() : currentMove;                    
                    bestMoveRatings.Add(currentRating.Clone());
                }
                else if (IsBestMoveSofar(color, bestRating, currentRating))
                {
                    ////currentRating.Move = 0 < currentRating.KingCapturedAtLevel ? new NoLegalMove() : currentMove;

                    currentRating.Move = 0 <= currentRating.IllegalMoveCount ? new NoLegalMove() : currentMove;
                    bestRating = currentRating.Clone();
                    bestMoveRatings = new List<MoveRating> { currentRating.Clone() };
                }
            }


            // Achtung:
            // Wir können hier nicht einfach einen Sieg oder Niederlage in ein Patt verwandeln, weil wir 
            // aufgrund der Sieg oder Niederlage schon die Züge ausgewählt haben.
            // Aber wie? Beim Evaluieren können wir Patt und Matt nicht unterscheiden. Wir müssen 2 Halbzüge zurück.

            // update score to 0 if it is stall mate
            foreach (var rating in bestMoveRatings)
            {
                //// if (rating.KingCapturedAtLevel == level - 1 && rating.Move is NoLegalMove)

                if (rating.IllegalMoveCount == 0 && rating.Move is NoLegalMove)
                
                {
                    if (!_moveGenerator.IsCheck(board, color))
                    {
                        rating.Score = 0;
                    }
                }

                rating.IllegalMoveCount--;
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
        internal bool IsEquallyGood(Definitions.ChessColor color, MoveRating bestRatingSoFar, MoveRating currentRating)
        {
            bool bothLost;
            bool bothWon;
            
            if (color == Definitions.ChessColor.White)
            {
                bothLost = bestRatingSoFar.Score == Definitions.ScoreBlackWins &&
                           currentRating.Score == Definitions.ScoreBlackWins;
                ////bothWon = bestRatingSoFar.Score == Definitions.ScoreWhiteWins &&
                ////          currentRating.Score == Definitions.ScoreWhiteWins;
            }
            else
            {
                bothLost = bestRatingSoFar.Score == Definitions.ScoreWhiteWins &&
                           currentRating.Score == Definitions.ScoreWhiteWins;
                ////bothWon = bestRatingSoFar.Score == Definitions.ScoreBlackWins &&
                ////          currentRating.Score == Definitions.ScoreBlackWins;
            }
            
            bool sameScore = currentRating.Score <= bestRatingSoFar.Score + Tolerance &&
                             currentRating.Score >= bestRatingSoFar.Score - Tolerance;
            bool sameIllegalMoveCount = bestRatingSoFar.IllegalMoveCount == currentRating.IllegalMoveCount;

            return (sameScore) &&
                   ((bothLost && sameIllegalMoveCount) || !bothLost);

            ////bool sameCapturedKingLevel = currentRating.KingCapturedAtLevel == bestRatingSoFar.KingCapturedAtLevel;
            ////return sameScore && !bothWon && !bothLost ||
            ////       (bothLost && sameCapturedKingLevel) ||
            ////       (bothWon && sameCapturedKingLevel);
        }

        /// <summary>
        /// True if current score is better than best plus tolerance.
        /// </summary>
        internal bool IsBestMoveSofar(Definitions.ChessColor color, MoveRating bestRatingSoFar, MoveRating currentRating)
        {
            bool bothLost;
            bool bothWon;
            bool isCurrentScoreBetter;
            if (color == Definitions.ChessColor.White)
            {
                isCurrentScoreBetter = (currentRating.Score > bestRatingSoFar.Score + Tolerance);
                bothLost = bestRatingSoFar.Score == Definitions.ScoreBlackWins &&
                           currentRating.Score == Definitions.ScoreBlackWins;
                ////bothWon = bestRatingSoFar.Score == Definitions.ScoreWhiteWins &&
                ////          currentRating.Score == Definitions.ScoreWhiteWins;
            }
            else
            {
                isCurrentScoreBetter = (currentRating.Score < bestRatingSoFar.Score - Tolerance);
                bothLost = bestRatingSoFar.Score == Definitions.ScoreWhiteWins &&
                           currentRating.Score == Definitions.ScoreWhiteWins;
                ////bothWon = bestRatingSoFar.Score == Definitions.ScoreBlackWins &&
                ////          currentRating.Score == Definitions.ScoreBlackWins;
            }

            bool smallerIllegalMoveCount = currentRating.IllegalMoveCount > bestRatingSoFar.IllegalMoveCount;
            return isCurrentScoreBetter ||
                (bothLost && smallerIllegalMoveCount);

            //// bool currentCapturedKingEarlier = currentRating.KingCapturedAtLevel > bestRatingSoFar.KingCapturedAtLevel;
            ////return isCurrentScoreBetter ||
            ////    (bothLost && !currentCapturedKingEarlier) ||
            ////    (bothWon && currentCapturedKingEarlier);
        }
    }
}

