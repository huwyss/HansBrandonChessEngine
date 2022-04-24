namespace MantaChessEngine
{
    public class EvaluatorPosition : IEvaluator
    {
        private static readonly int ValuePawn = Definitions.ValuePawn;
        private static readonly int ValueKnight = Definitions.ValueKnight;
        private static readonly int ValueBishop = Definitions.ValueBishop;
        private static readonly int ValueRook = Definitions.ValueRook;
        private static readonly int ValueQueen = Definitions.ValueQueen;

        private readonly int AdvantageDoubleBishop = 50;
        private readonly int CastlingScore = 100;

        private int numberWhiteBishop = 0;
        private int numberBlackBishop = 0;

        /// <summary>
        /// Calculates the score from white's point. + --> white is better, - --> black is better.
        /// </summary>
        public int Evaluate(IBoard board)
        {
            int scoreWhite = 0;
            int scoreBlack = 0;

            numberWhiteBishop = 0;
            numberBlackBishop = 0;

            for (int file = 1; file <= 8; file++)
            {
                for (int rank = 1; rank <= 8; rank++)
                {
                    Piece piece = board.GetPiece(file, rank);
                    int pieceScore = GetPieceScore(piece, file, rank);
                    if (board.GetColor(file, rank) == Definitions.ChessColor.White)
                    {
                        scoreWhite += pieceScore;
                    }
                    else
                    {
                        scoreBlack += pieceScore;
                    }
                }
            }

            if (numberWhiteBishop >= 2)
            {
                scoreWhite += AdvantageDoubleBishop;
            }

            if (numberBlackBishop >= 2)
            {
                scoreBlack += AdvantageDoubleBishop;
            }

            if (board.WhiteDidCastling)
            {
                scoreWhite += CastlingScore;
            }

            if (board.BlackDidCastling)
            {
                scoreBlack += CastlingScore;
            }

            int score = scoreWhite - scoreBlack;

            return score;
        }

        private int GetPieceScore(Piece piece, int file, int rank)
        {
            int index = 8 * (rank - 1) + file - 1;

            if (piece is Pawn)
                return ValuePawn + PawnPositionBonus[index];
            else if (piece is Knight)
                return ValueKnight + KnightPositionBonus[index];
            else if (piece is Bishop)
            {
                if (piece.Color == Definitions.ChessColor.Black)
                {
                    numberBlackBishop++;
                }
                else
                {
                    numberWhiteBishop++;
                }

                return ValueBishop + BishopPositionBonus[index];
            }
            else if (piece is Rook)
                return ValueRook;
            else if (piece is Queen)
                return ValueQueen;
            else
                return 0;
        }
    
        private readonly int[] PawnPositionBonus = new int[64] // pawn in center are more valuable (at least in opening)
        {
            0,  0,  0,  0,  0,  0,  0,  0,
            0,  0,  0,  0,  0,  0,  0,  0,
            0,  0,  5, 10, 10,  5,  0,  0,
            0,  0, 10, 15, 15, 10,  0,  0,
            0,  0, 10, 15, 15, 10,  0,  0,
            0,  0,  5, 10, 10,  5,  0,  0,
            0,  0,  0,  0,  0,  0,  0,  0,
            0,  0,  0,  0,  0,  0,  0,  0,

        //// 0,  0,  0,  0,  0,  0,  0,  0,
        ////24, 28, 32, 35, 35, 32, 28, 24,
        //// 5, 10, 16, 25, 25, 16, 10,  5,
        //// 4,  8, 14, 19, 19, 14,  8,  4,
        //// 3,  5,  6, 12, 12,  6,  5,  3,
        //// 2,  4,  3,  7,  7,  3,  4,  2,
        //// 1,  2,  3, -8, -9,  3,  2,  1,
        //// 0,  0,  0,  0,  0,  0,  0,  0
        };

        private readonly int[] KnightPositionBonus = new int[64] // Der Springer am Rande ist eine Schande
        {
            -10, -5, -5, -5, -5, -5, -5, -10,
             -5,  0,  0,  0,  0,  0,  0,  -5,
             -5,  0,  5,  5,  5,  5,  0,  -5,
             -5,  0,  5,  5,  5,  5,  0,  -5,
             -5,  0,  5,  5,  5,  5,  0,  -5,
             -5,  0,  5,  5,  5,  5,  0,  -5,
             -5,  0,  0,  0,  0,  0,  0,  -5,
            -10, -5, -5, -5, -5, -5, -5, -10
        };

        private readonly int[] BishopPositionBonus = new int[64]
        {
           -8, -8, -6, -6, -6, -6, -8, -8,
            0,  6,  5,  5,  5,  5,  6,  0,
            0,  4,  5,  6,  6,  5,  4,  0,
            4,  6,  8,  8,  8,  8,  6,  4,
            4,  6,  8,  8,  8,  8,  6,  4,
            0,  4,  5,  6,  6,  5,  4,  0,
            0,  6,  5,  5,  5,  5,  6,  0,
           -8, -8, -6, -6, -6, -6, -8, -8
        };

        ////private readonly int[] RookPositionBonus = new int[64]
        ////{
        ////    0,  1,  2,  4,  4,  2,  1,  0,
        ////    3,  5,  7,  7,  7,  7,  5,  3,
        ////    3,  4,  5,  6,  6,  5,  4,  3,
        ////   -2,  0,  4,  5,  5,  4,  0, -2,
        ////   -4, -2,  3,  4,  4,  3, -2, -4,
        ////   -6, -2,  3,  4,  4,  3, -2, -6,
        ////   -6, -2,  4,  4,  4,  4, -2, -6,
        ////   -4, -2,  2,  4,  4,  2, -2, -4
        ////};

        ////private readonly int[] QueenPositionBonus = new int[64]
        ////{
        ////    0,  0,  0,  0,  0,  0,  0,  0,
        ////    0,  2,  3,  4,  4,  3,  2,  0,
        ////    0,  2,  4,  5,  5,  3,  2,  0,
        ////    0,  2,  4,  8,  8,  4,  2,  0,
        ////   -1,  0,  3,  6,  6,  3,  0, -1,
        ////   -1,  0,  2,  3,  3,  2,  0, -1,
        ////   -2, -1,  0,  0,  0,  0, -1, -2,
        ////   -3, -2, -1,  0,  0, -1, -2, -3
        ////};
    }
}
