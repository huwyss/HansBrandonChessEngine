using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MantaChessEngine.BitboardEngine
{
    public interface IBitBoardState
    {
        List<BitMove> Moves { get; }
        bool WhiteDidCastling { get; }
        bool BlackDidCastling { get; }
        BitColor SideToMove { get; }

        int MoveCountSincePawnOrCapture { get; }

        void Clear();

        void Add(BitMove move, Square enPassantSquare, bool castlingRightWhiteQueenSide, bool castlingRightWhiteKingSide, bool castlingRightBlackQueenSide, bool castlingRightBlackKingSide, BitColor sideToMove);

        void Back();

        int Count { get; }

        BitMove LastMove { get; }
        
        Square LastEnPassantSquare { get; }
        bool LastCastlingRightWhiteQueenSide { get; }
        bool LastCastlingRightWhiteKingSide { get; }
        bool LastCastlingRightBlackQueenSide { get; }
        bool LastCastlingRightBlackKingSide { get; }
    }
}
