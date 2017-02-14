using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaracudaChessEngine
{
    public class History
    {
        public List<int> EnPassantFile { get; set; } // 1..8
        public List<int> EnPassantRank { get; set; } // 1..8
        
        public List<Move> Moves { get; set; }

        public bool CastlingRightFirstMover { get; set; }
        public bool CastlingRightSecondMover { get; set; }

        public History()
        {
            Moves = new List<Move>();
            EnPassantFile = new List<int>();
            EnPassantRank = new List<int>();
            EnPassantFile.Add(0);
            EnPassantRank.Add(0);
        }

        public void Add(Move move, int enPassantFile, int enPassantRank)
        {
            Moves.Add(move);
            EnPassantFile.Add(enPassantFile);
            EnPassantRank.Add(enPassantRank);
        }

        public void Back()
        {
            if (Count > 0)
            {
                EnPassantFile.RemoveAt(Count);
                EnPassantRank.RemoveAt(Count);
                Moves.RemoveAt(Count - 1); // do this last. is changes Count!
            }
        }

        public int Count { get { return Moves.Count; } }

        public Move LastMove
        {
            get
            {
                if (Count > 0)
                {
                    return Moves[Count - 1];
                }
                else
                {
                    return null;
                }
            }
        }

        public int LastEnPassantFile { get { return EnPassantFile[Count]; } }
        public int LastEnPassantRank { get { return EnPassantRank[Count]; } }
    }
}
