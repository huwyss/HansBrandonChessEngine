using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaracudaChessEngine
{
    public static class Definitions
    {
        public enum ChessColor
        {
            White,
            Black,
            Empty
        }

        public static char EmptyField = '.';

        public const char KING = 'k';
        public const char PAWN = 'p';
        public const char KNIGHT = 'n';
        public const char ROOK = 'r';
        public const char QUEEN = 'q';
        public const char BISHOP = 'b';

    }
}
