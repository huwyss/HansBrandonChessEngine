using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MantaChessEngine.Definitions;

namespace MantaChessEngine
{
    public class MoveRating
    {
        private const float Tolerance = 0.05f;

        /// <summary>
        /// Move that is being rated.
        /// </summary>
        public IMove Move { get; set; }

        /// <summary>
        /// Score of the move. Positive means good for white, negative means good for black.
        /// </summary>
        public float Score { get; set; }

        /// <summary>
        /// The number of illegal moves already played.
        /// 0 = all good
        /// 1 = this is the 1st illegal move (king left in check)
        /// 2 = this is the 2nd illegal move (capture opponents king)
        /// 3 = this is the 3rd illegal move (own king is already lost)
        /// </summary>
        public int IllegalMoveCount { get; set; }

        public int KingCapturedAtLevel { get; set; }

        public ChessColor CapturedKing { get; set; }

        public int GameEndLevel { get; set; }

        public MoveRating()
        {
            Score = 0;
            IllegalMoveCount = -1;
            Move = null;
            KingCapturedAtLevel = 0;
            CapturedKing = ChessColor.Empty;
            GameEndLevel = 0;
        }

        public MoveRating Clone()
        {
            return new MoveRating()
            {
                Score = this.Score,
                IllegalMoveCount = this.IllegalMoveCount,
                Move = this.Move,
                KingCapturedAtLevel = this.KingCapturedAtLevel,
                CapturedKing = this.CapturedKing,
                GameEndLevel = this.GameEndLevel
            };
        }

        /// <summary>
        /// True if current score is as good as best within tolerance.
        /// </summary>
        internal bool IsEquallyGood(Definitions.ChessColor color, MoveRating bestRatingSoFar)
        {
            bool bothLost;
            bool bothWon;

            if (color == Definitions.ChessColor.White)
            {
                bothLost = bestRatingSoFar.Score == Definitions.ScoreBlackWins &&
                           this.Score == Definitions.ScoreBlackWins;
                ////bothWon = bestRatingSoFar.Score == Definitions.ScoreWhiteWins &&
                ////          currentRating.Score == Definitions.ScoreWhiteWins;
            }
            else
            {
                bothLost = bestRatingSoFar.Score == Definitions.ScoreWhiteWins &&
                           this.Score == Definitions.ScoreWhiteWins;
                ////bothWon = bestRatingSoFar.Score == Definitions.ScoreBlackWins &&
                ////          currentRating.Score == Definitions.ScoreBlackWins;
            }

            bool sameScore = this.Score <= bestRatingSoFar.Score + Tolerance &&
                             this.Score >= bestRatingSoFar.Score - Tolerance;
            bool sameGameEndLevel = bestRatingSoFar.GameEndLevel == this.GameEndLevel;

            return sameScore && sameGameEndLevel;
            //       ((bothLost && sameIllegalMoveCount) || !bothLost);

            ////bool sameCapturedKingLevel = currentRating.KingCapturedAtLevel == bestRatingSoFar.KingCapturedAtLevel;
            ////return sameScore && !bothWon && !bothLost ||
            ////       (bothLost && sameCapturedKingLevel) ||
            ////       (bothWon && sameCapturedKingLevel);
        }

        /// <summary>
        /// True if current score is better than best plus tolerance.
        /// </summary>
        internal bool IsBestMoveSofar(Definitions.ChessColor color, MoveRating bestRatingSoFar)
        {
            bool bothLost;
            bool bothWon;
            bool isCurrentScoreBetter;
            if (color == Definitions.ChessColor.White)
            {
                isCurrentScoreBetter = (this.Score > bestRatingSoFar.Score + Tolerance);
                bothLost = bestRatingSoFar.Score == Definitions.ScoreBlackWins &&
                           this.Score == Definitions.ScoreBlackWins;
                ////bothWon = bestRatingSoFar.Score == Definitions.ScoreWhiteWins &&
                ////          currentRating.Score == Definitions.ScoreWhiteWins;
            }
            else
            {
                isCurrentScoreBetter = (this.Score < bestRatingSoFar.Score - Tolerance);
                bothLost = bestRatingSoFar.Score == Definitions.ScoreWhiteWins &&
                           this.Score == Definitions.ScoreWhiteWins;
                ////bothWon = bestRatingSoFar.Score == Definitions.ScoreBlackWins &&
                ////          currentRating.Score == Definitions.ScoreBlackWins;
            }

            bool smallerIllegalMoveCount = this.IllegalMoveCount > bestRatingSoFar.IllegalMoveCount;
            bool sameScore = this.Score <= bestRatingSoFar.Score + Tolerance &&
                             this.Score >= bestRatingSoFar.Score - Tolerance;
            bool earlierLevel = bestRatingSoFar.GameEndLevel > this.GameEndLevel;
            return isCurrentScoreBetter || (sameScore && earlierLevel);
            //(bothLost && smallerIllegalMoveCount);

            //// bool currentCapturedKingEarlier = currentRating.KingCapturedAtLevel > bestRatingSoFar.KingCapturedAtLevel;
            ////return isCurrentScoreBetter ||
            ////    (bothLost && !currentCapturedKingEarlier) ||
            ////    (bothWon && currentCapturedKingEarlier);
        }
    }
}
