using static MantaChessEngine.Definitions;
using System.Collections.Generic;
using System.Linq;

namespace MantaChessEngine
{
    public class MoveRating
    {
        private const float Tolerance = 0.05f;

        /// <summary>
        /// Move that is being rated.
        /// </summary>
        public IMove Move { get; set; }

        public IList<IMove> PrincipalVariation { get; set; }

        /// <summary>
        /// Score of the move. Positive means good for white, negative means good for black.
        /// </summary>
        public float Score { get; set; }

        public bool WhiteWins { get; set; }

        public bool BlackWins { get; set; }

        public bool Stallmate { get; set; }

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
            WhiteWins = false;
            BlackWins = false;
            Stallmate = false;
            Move = null;
            GameEndLevel = 0;
            PrincipalVariation = new List<IMove>();
        }

        public MoveRating Clone()
        {
            return new MoveRating()
            {
                Score = this.Score,
                WhiteWins = this.WhiteWins,
                BlackWins = this.BlackWins,
                Stallmate = this.Stallmate,
                Move = this.Move,
                GameEndLevel = this.GameEndLevel,
                PrincipalVariation = this.PrincipalVariation,
            };
        }

        // Additional Info
        public int EvaluatedPositions { get; set; }

        public int Depth { get; set; }

        public int PruningCount { get; set; }

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

        public bool IsBetterFaster(ChessColor color, MoveRating otherRating)
        {
            return color == ChessColor.White ? Score > otherRating.Score : Score < otherRating.Score;
        }
    }
}
