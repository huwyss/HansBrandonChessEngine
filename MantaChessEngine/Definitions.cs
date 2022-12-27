using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MantaChessEngine
{
    public static class Definitions
    {
        public const char UP = 'u';
        public const char DOWN = 'd';
        public const char LEFT = 'l';
        public const char RIGHT = 'r';

        public const int DEFAULT_MAXLEVEL = 3;

        public const int ScoreWhiteWins = 10000;
        public const int ScoreBlackWins = -10000;

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
