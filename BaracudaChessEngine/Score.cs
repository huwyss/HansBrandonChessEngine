using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaracudaChessEngine
{
    public class Score
    {
        public float ScoreWhite { get; set; }
        public float ScoreBlack { get; set; }

        public Score()
        {
            ScoreWhite = 0;
            ScoreBlack = 0;
        }

        public Score(float scoreWhite, float scoreBlack)
        {
            ScoreWhite = scoreWhite;
            ScoreBlack = scoreBlack;
        }
    }
}
