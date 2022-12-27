using MantaCommon;
using System.Collections.Generic;
using System.Linq;

namespace MantaBitboardEngine
{
    public class BitMoveRating
    {
        private const int Tolerance = 5;

        /// <summary>
        /// Move that is being rated.
        /// </summary>
        public BitMove Move { get; set; }

        public IList<BitMove> PrincipalVariation { get; set; }

        public int Alpha { get; set; }
        public int Beta { get; set; }

        /// <summary>
        /// Score of the move. Positive means good for white, negative means good for black.
        /// </summary>
        public int Score { get; set; }

        public int EvaluationLevel { get; set; }

        public bool WhiteWins { get; set; }

        public bool BlackWins { get; set; }

        public bool Stallmate { get; set; }

        public BitMoveRating()
        {
            Score = 0;
            WhiteWins = false;
            BlackWins = false;
            Stallmate = false;
            Move = null;
            PrincipalVariation = new List<BitMove>();
            Alpha = 0;
            Beta = 0;
            SelectiveDepth = 0;
            EvaluationLevel = 0;
        }

        public BitMoveRating Clone()
        {
            return new BitMoveRating()
            {
                Score = this.Score,
                WhiteWins = this.WhiteWins,
                BlackWins = this.BlackWins,
                Stallmate = this.Stallmate,
                Move = this.Move,
                PrincipalVariation = this.PrincipalVariation,
                Alpha = this.Alpha,
                Beta = this.Beta,
                SelectiveDepth = this.SelectiveDepth,
                EvaluationLevel = this.EvaluationLevel,
            };
        }

        // Additional Info
        public int EvaluatedPositions { get; set; }

        public int Depth { get; set; }

        public int SelectiveDepth { get; set; }

        public int PruningCount { get; set; }

        /// <summary>
        /// True if current score is as good as best within tolerance.
        /// </summary>
        public bool IsEquallyGood(BitMoveRating otherRating)
        {
            bool sameScore = this.Score <= otherRating.Score + Tolerance &&
                             this.Score >= otherRating.Score - Tolerance;

            return sameScore;
        }

        /// <summary>
        /// True if current score is better or wins earlier or looses later.
        /// </summary>
        public bool IsBetter(ChessColor color, BitMoveRating otherRating)
        {
            return color == ChessColor.White ? Score > otherRating.Score : Score < otherRating.Score;
        }
    }
}
