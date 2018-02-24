using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MantaChessEngine
{
    public class MoveInfo
    {
        public IMove Move { get; set; }
        public float Score { get; set; }

        public MoveInfo(IMove move, float score)
        {
            Move = move;
            Score = score;
        }
    }
}
