using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MantaChessEngine
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

        public const char UP = 'u';
        public const char DOWN = 'd';
        public const char LEFT = 'l';
        public const char RIGHT = 'r';

        public const int DEFAULT_MAXLEVEL = 3;

        public const float ScoreWhiteWins = 10000;
        public const float ScoreBlackWins = -10000;
    }
}
