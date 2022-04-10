﻿using static MantaChessEngine.Definitions;

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

        public int GameEndLevel { get; set; }

        public MoveRating()
        {
            Score = 0;
            Move = null;
            GameEndLevel = 0;
        }

        public MoveRating Clone()
        {
            return new MoveRating()
            {
                Score = this.Score,
                Move = this.Move,
                GameEndLevel = this.GameEndLevel
            };
        }

        /// <summary>
        /// True if current score is as good as best within tolerance.
        /// </summary>
        public bool IsEquallyGood(MoveRating otherRating)
        {
            bool sameScore = this.Score <= otherRating.Score + Tolerance &&
                             this.Score >= otherRating.Score - Tolerance;

            bool sameGameEndLevel = otherRating.GameEndLevel == this.GameEndLevel;

            return sameScore && sameGameEndLevel;
        }

        /// <summary>
        /// True if current score is better or wins earlier or looses later.
        /// </summary>
        public bool IsBetter(ChessColor color, MoveRating otherRating)
        {
            bool isCurrentScoreBetter;
            if (color == ChessColor.White)
            {
                if (Score == ScoreWhiteWins && Score == otherRating.Score)
                {
                    isCurrentScoreBetter = GameEndLevel < otherRating.GameEndLevel;
                }
                else if (Score == ScoreBlackWins && Score == otherRating.Score)
                {
                    isCurrentScoreBetter = GameEndLevel > otherRating.GameEndLevel;
                }
                else
                {
                    isCurrentScoreBetter = Score > otherRating.Score;
                }
            }
            else
            {
                if (Score == ScoreBlackWins && Score == otherRating.Score)
                {
                    isCurrentScoreBetter = GameEndLevel < otherRating.GameEndLevel;
                }
                else if (Score == ScoreWhiteWins && Score == otherRating.Score)
                {
                    isCurrentScoreBetter = GameEndLevel > otherRating.GameEndLevel;
                }
                else
                {
                    isCurrentScoreBetter = Score < otherRating.Score;
                }
            }
           
            return isCurrentScoreBetter;
        }
    }
}
