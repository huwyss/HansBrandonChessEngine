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

        public List<bool> CastlingRightWhiteQueenSide { get; set; }
        public List<bool> CastlingRightWhiteKingSide { get; set; }
        public List<bool> CastlingRightBlackQueenSide { get; set; }
        public List<bool> CastlingRightBlackKingSide { get; set; }

        public List<Move> Moves { get; set; }

        public History()
        {
            Moves = new List<Move>();

            EnPassantFile = new List<int>();
            EnPassantRank = new List<int>();

            EnPassantFile.Add(0);
            EnPassantRank.Add(0);

            CastlingRightWhiteQueenSide = new List<bool>();
            CastlingRightWhiteKingSide = new List<bool>();
            CastlingRightBlackQueenSide = new List<bool>();
            CastlingRightBlackKingSide = new List<bool>();

            CastlingRightWhiteQueenSide.Add(true);
            CastlingRightWhiteKingSide.Add(true);
            CastlingRightBlackQueenSide.Add(true);
            CastlingRightBlackKingSide.Add(true);
        }

        public void Add(Move move, int enPassantFile, int enPassantRank, bool castlingRightWhiteQueenSide, bool castlingRightWhiteKingSide, bool castlingRightBlackQueenSide, bool castlingRightBlackKingSide)
        {
            Moves.Add(move);
            EnPassantFile.Add(enPassantFile);
            EnPassantRank.Add(enPassantRank);
            CastlingRightWhiteQueenSide.Add(castlingRightWhiteQueenSide);
            CastlingRightWhiteKingSide.Add(castlingRightWhiteKingSide);
            CastlingRightBlackQueenSide.Add(castlingRightBlackQueenSide);
            CastlingRightBlackKingSide.Add(castlingRightBlackKingSide);
        }

        public void Back()
        {
            if (Count > 0)
            {
                EnPassantFile.RemoveAt(Count);
                EnPassantRank.RemoveAt(Count);

                CastlingRightWhiteQueenSide.RemoveAt(Count);
                CastlingRightWhiteKingSide.RemoveAt(Count);
                CastlingRightBlackQueenSide.RemoveAt(Count);
                CastlingRightBlackKingSide.RemoveAt(Count);

                Moves.RemoveAt(Count - 1); // do this last. it changes Count!
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

        public bool LastCastlingRightWhiteQueenSide { get { return CastlingRightWhiteQueenSide[Count]; } }
        public bool LastCastlingRightWhiteKingSide { get { return CastlingRightWhiteKingSide[Count]; } }
        public bool LastCastlingRightBlackQueenSide { get { return CastlingRightBlackQueenSide[Count]; } }
        public bool LastCastlingRightBlackKingSide { get { return CastlingRightBlackKingSide[Count]; } }
    }
}
