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

        public MoveRating()
        {
            Score = 0;
            IllegalMoveCount = -1;
            Move = null;
            KingCapturedAtLevel = 0;
            CapturedKing = ChessColor.Empty;
        }

        public MoveRating Clone()
        {
            return new MoveRating()
            {
                Score = this.Score,
                IllegalMoveCount = this.IllegalMoveCount,
                Move = this.Move,
                KingCapturedAtLevel = this.KingCapturedAtLevel,
                CapturedKing = this.CapturedKing
            };
        }
    }
}
