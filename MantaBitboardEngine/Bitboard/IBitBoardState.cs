using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MantaCommon;

namespace MantaBitboardEngine
{
    public interface IBitBoardState
    {
        List<BitMove> Moves { get; }
        bool WhiteDidCastling { get; }
        bool BlackDidCastling { get; }
        ChessColor SideToMove { get; }

        int MoveCountSincePawnOrCapture { get; }

        void Add(BitMove move, Square enPassantSquare, bool castlingRightWhiteQueenSide, bool castlingRightWhiteKingSide, bool castlingRightBlackQueenSide, bool castlingRightBlackKingSide, ChessColor sideToMove);
        
        void SetState(Square enPassantSquare, bool castlingRightWhiteQueenSide, bool castlingRightWhiteKingSide, bool castlingRightBlackQueenSide, bool castlingRightBlackKingSide, ChessColor sideToMove);

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
