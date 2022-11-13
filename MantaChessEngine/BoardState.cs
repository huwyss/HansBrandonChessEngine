using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MantaChessEngine.Definitions;

namespace MantaChessEngine
{
    public class BoardState
    {
        private List<int> EnPassantFile { get; set; } // 1..8
        private List<int> EnPassantRank { get; set; } // 1..8
        private List<bool> CastlingRightWhiteQueenSide { get; set; }
        private List<bool> CastlingRightWhiteKingSide { get; set; }
        private List<bool> CastlingRightBlackQueenSide { get; set; }
        private List<bool> CastlingRightBlackKingSide { get; set; }

        public List<IMove> Moves { get; set; }
        public bool WhiteDidCastling { get; set; }
        public bool BlackDidCastling { get; set; }
        public ChessColor SideToMove { get; set; }

        public int MoveCountSincePawnOrCapture { get; private set; } // todo implement this rule...

        public BoardState()
        {
            Clear();
        }

        public void Clear()
        {
            Moves = new List<IMove>();

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

            SideToMove = ChessColor.White;
            WhiteDidCastling = false;
            BlackDidCastling = false;

            MoveCountSincePawnOrCapture = 0;
        }

        public void Add(IMove move, int enPassantFile, int enPassantRank, bool castlingRightWhiteQueenSide, bool castlingRightWhiteKingSide, bool castlingRightBlackQueenSide, bool castlingRightBlackKingSide, ChessColor sideToMove)
        {
            Moves.Add(move);
            EnPassantFile.Add(enPassantFile);
            EnPassantRank.Add(enPassantRank);
            CastlingRightWhiteQueenSide.Add(castlingRightWhiteQueenSide);
            CastlingRightWhiteKingSide.Add(castlingRightWhiteKingSide);
            CastlingRightBlackQueenSide.Add(castlingRightBlackQueenSide);
            CastlingRightBlackKingSide.Add(castlingRightBlackKingSide);
            SideToMove = sideToMove;
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

        public IMove LastMove
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
