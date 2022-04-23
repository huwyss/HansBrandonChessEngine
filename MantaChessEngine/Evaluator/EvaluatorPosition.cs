namespace MantaChessEngine
{
    public class EvaluatorPosition : IEvaluator
    {
        private static readonly int ValuePawn = Definitions.ValuePawn / 100;
        private static readonly int ValueKnight = Definitions.ValueKnight / 100;
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
                return ValuePawn * PawnPositionValue[index];
            else if (piece is Knight)
                return ValueKnight * KnightPositionValue[index];
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
                return ValueBishop;
            }
            else if (piece is Rook)
                return ValueRook;
            else if (piece is Queen)
                return ValueQueen;
            else
                return 0;
        }
    
        private readonly int[] PawnPositionValue = new int[64] // pawn in center are more valuable (at least in opening)
        {
            100, 100, 100, 100, 100, 100, 100, 100,
            100, 100, 100, 100, 100, 100, 100, 100,
            100, 100, 105, 110, 110, 105, 100, 100,
            100, 100, 110, 120, 120, 110, 100, 100,
            100, 100, 110, 120, 120, 110, 100, 100,
            100, 100, 105, 110, 110, 105, 100, 100,
            100, 100, 100, 100, 100, 100, 100, 100,
            100, 100, 100, 100, 100, 100, 100, 100,
        };

        private readonly int[] KnightPositionValue = new int[64] // Der Springer am Rande ist eine Schande
        {
            95,  95,  95,  95,  95,  95,  95,  95,
            95, 100, 100, 100, 100, 100, 100,  95,
            95, 100, 100, 100, 100, 100, 100,  95,
            95, 100, 100, 100, 100, 100, 100,  95,
            95, 100, 100, 100, 100, 100, 100,  95,
            95, 100, 100, 100, 100, 100, 100,  95,
            95, 100, 100, 100, 100, 100, 100,  95,
            95,  95,  95,  95,  95,  95,  95,  95,
        };
    }
}
