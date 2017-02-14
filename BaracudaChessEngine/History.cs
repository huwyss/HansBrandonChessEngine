using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaracudaChessEngine
{
    public class History
    {
        public int EnPassantFile { get; set; } // 1..8
        public int EnPassantRank { get; set; } // 1..8
        
        public List<Move> Moves { get; set; }

        public bool CastlingRightFirstMover { get; set; }
        public bool CastlingRightSecondMover { get; set; }
    }
}
