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

        public const int ScoreWhiteWins = 1000000;
        public const int ScoreBlackWins = -1000000;

        public const int SignificantFactor = 8; // fast multiplier

        public const int ValuePawn = 100;
        public const int ValueKnight = 300;
        public const int ValueBishop = 300;
        public const int ValueRook = 500;
        public const int ValueQueen = 900;

        // values for move ordering
        public const int ImportanceCapture = 100;
        public const int ImportancePromotion = 90;
        public const int ImportancePawnMove = 80;
    }
}
