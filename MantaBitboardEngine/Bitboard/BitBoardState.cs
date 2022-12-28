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
            Moves = new List<BitMove>();
            EnPassantSquare = new List<Square>();
            CastlingRightWhiteQueenSide = new List<bool>();
            CastlingRightWhiteKingSide = new List<bool>();
            CastlingRightBlackQueenSide = new List<bool>();
            CastlingRightBlackKingSide = new List<bool>();

            SetState(Square.NoSquare, true, true, true, true, ChessColor.White);
        }

        public void Add(BitMove move, Square enPassantSquare, bool castlingRightWhiteQueenSide, bool castlingRightWhiteKingSide, bool castlingRightBlackQueenSide, bool castlingRightBlackKingSide, ChessColor sideToMove)
        {
            Moves.Add(move);
            Add(enPassantSquare, castlingRightWhiteQueenSide, castlingRightWhiteKingSide, castlingRightBlackQueenSide, castlingRightBlackKingSide, sideToMove);
        }

        private void Add(Square enPassantSquare, bool castlingRightWhiteQueenSide, bool castlingRightWhiteKingSide, bool castlingRightBlackQueenSide, bool castlingRightBlackKingSide, ChessColor sideToMove)
        { 
            EnPassantSquare.Add(enPassantSquare);
            CastlingRightWhiteQueenSide.Add(castlingRightWhiteQueenSide);
            CastlingRightWhiteKingSide.Add(castlingRightWhiteKingSide);
            CastlingRightBlackQueenSide.Add(castlingRightBlackQueenSide);
            CastlingRightBlackKingSide.Add(castlingRightBlackKingSide);
            SideToMove = sideToMove;
        }

        public void SetState(Square enPassantSquare, bool castlingRightWhiteQueenSide, bool castlingRightWhiteKingSide, bool castlingRightBlackQueenSide, bool castlingRightBlackKingSide, ChessColor sideToMove)
        {
            SideToMove = ChessColor.White;
            WhiteDidCastling = false;
            BlackDidCastling = false;
            MoveCountSincePawnOrCapture = 0;
            Moves.Clear();
            EnPassantSquare.Clear();
            CastlingRightWhiteQueenSide.Clear();
            CastlingRightWhiteKingSide.Clear();
            CastlingRightBlackQueenSide.Clear();
            CastlingRightBlackKingSide.Clear();

            Add(enPassantSquare, castlingRightWhiteQueenSide, castlingRightWhiteKingSide, castlingRightBlackQueenSide, castlingRightBlackKingSide, sideToMove);
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
        public BitMove LastMove => Count > 0 ? Moves[Count - 1] : BitMove.CreateEmptyMove();
        public Square LastEnPassantSquare => (Square)EnPassantSquare[Count];
        public bool LastCastlingRightWhiteQueenSide => CastlingRightWhiteQueenSide[Count];
        public bool LastCastlingRightWhiteKingSide => CastlingRightWhiteKingSide[Count];
        public bool LastCastlingRightBlackQueenSide => CastlingRightBlackQueenSide[Count];
        public bool LastCastlingRightBlackKingSide => CastlingRightBlackKingSide[Count];
    }
}
