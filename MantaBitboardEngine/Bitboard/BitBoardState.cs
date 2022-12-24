using System.Collections.Generic;
using MantaCommon;
    
namespace MantaBitboardEngine
{
    public class BitBoardState : IBitBoardState
    {
        private List<Square> EnPassantSquare { get; set; }
        private List<bool> CastlingRightWhiteQueenSide { get; set; }
        private List<bool> CastlingRightWhiteKingSide { get; set; }
        private List<bool> CastlingRightBlackQueenSide { get; set; }
        private List<bool> CastlingRightBlackKingSide { get; set; }

        public List<BitMove> Moves { get; set; }
        public bool WhiteDidCastling { get; set; }
        public bool BlackDidCastling { get; set; }
        public ChessColor SideToMove { get; set; }

        public int MoveCountSincePawnOrCapture { get; private set; } // todo implement this rule...

        public BitBoardState()
        {
            Clear();
        }

        public void Clear()
        {
            Moves = new List<BitMove>();

            EnPassantSquare = new List<Square>();
            EnPassantSquare.Add(Square.NoSquare);

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

        public void Add(BitMove move, Square enPassantSquare, bool castlingRightWhiteQueenSide, bool castlingRightWhiteKingSide, bool castlingRightBlackQueenSide, bool castlingRightBlackKingSide, ChessColor sideToMove)
        {
            Moves.Add(move);
            EnPassantSquare.Add(enPassantSquare);
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
                EnPassantSquare.RemoveAt(Count);

                CastlingRightWhiteQueenSide.RemoveAt(Count);
                CastlingRightWhiteKingSide.RemoveAt(Count);
                CastlingRightBlackQueenSide.RemoveAt(Count);
                CastlingRightBlackKingSide.RemoveAt(Count);

                Moves.RemoveAt(Count - 1); // do this last. it changes Count!

                SideToMove = CommonHelper.OtherColor(SideToMove);
            }
            else
            {
                throw new MantaEngineException("Back called on BitboardState but counter 0!");
            }
        }

        public int Count { get { return Moves.Count; } }

        public BitMove LastMove
        {
            get
            {
                if (Count > 0)
                {
                    return Moves[Count - 1];
                }
                else
                {
                    return BitMove.CreateEmptyMove();
                }
            }
        }

        public Square LastEnPassantSquare { get { return (Square)EnPassantSquare[Count]; } }
        public bool LastCastlingRightWhiteQueenSide { get { return CastlingRightWhiteQueenSide[Count]; } }
        public bool LastCastlingRightWhiteKingSide { get { return CastlingRightWhiteKingSide[Count]; } }
        public bool LastCastlingRightBlackQueenSide { get { return CastlingRightBlackQueenSide[Count]; } }
        public bool LastCastlingRightBlackKingSide { get { return CastlingRightBlackKingSide[Count]; } }
    }
}
