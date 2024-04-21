using HBCommon;
using System.Collections.Generic;
using System.Linq;

namespace HBCommon
{
    public class UciMoveRating
    {
        private const int Tolerance = 5;

        /// <summary>
        /// Move that is being rated.
        /// </summary>
        public string Move { get; set; }

        public ChessColor MovingColor { get; set; } // todo new !!

        public IList<string> PrincipalVariation { get; set; }

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

        public bool SearchAborted { get; set; }

        public UciMoveRating()
        {
            Score = 0;
            WhiteWins = false;
            BlackWins = false;
            Stallmate = false;
            Move = null;
            PrincipalVariation = new List<string>();
            Alpha = 0;
            Beta = 0;
            SelectiveDepth = 0;
            EvaluationLevel = 0;
        }

        // Additional Info
        public int EvaluatedPositions { get; set; }

        public int Depth { get; set; }

        public int SelectiveDepth { get; set; }

        public int PruningCount { get; set; }

    }
}
