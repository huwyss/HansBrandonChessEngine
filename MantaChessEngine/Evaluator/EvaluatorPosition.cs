using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MantaChessEngine
{
    public class EvaluatorPosition : IEvaluator
    {
        public readonly float ValuePawn = 1f;
        public readonly float ValueKnight = 3f;
        public readonly float ValueBishop = 3f;
        public readonly float ValueRook = 5f;
        public readonly float ValueQueen = 9f;
        public readonly float ValueKing = 1000f;

        private readonly float AdvantageDoubleBishop = 0.5f;

        private int numberWhiteBishop = 0;
        private int numberBlackBishop = 0;

        /// <summary>
        /// Calculates the score from white's point. + --> white is better, - --> black is better.
        /// </summary>
        public float Evaluate(Board board)
        {
            float scoreWhite = 0;
            float scoreBlack = 0;

            numberWhiteBishop = 0;
            numberBlackBishop = 0;

            for (int file = 1; file <= 8; file++)
            {
                for (int rank = 1; rank <= 8; rank++)
                {
                    char piece = board.GetPiece(file, rank);
                    float pieceScore = GetPieceScore(piece, file, rank);
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

            return scoreWhite - scoreBlack;
        }

        private float GetPieceScore(char piece, int file, int rank)
        {
            int index = 8 * (rank - 1) + file - 1;
            switch (piece.ToString().ToLower()[0])
            {
                case Definitions.PAWN:
                    return ValuePawn * PawnPositionValue[index];
                case Definitions.KNIGHT:
                    return ValueKnight * KnightPositionValue[index];
                case Definitions.BISHOP:
                    if (piece == Definitions.BISHOP) // black bishop
                    {
                        numberBlackBishop++;
                    }
                    else
                    {
                        numberWhiteBishop++;
                    }
                    return ValueBishop;
                case Definitions.ROOK:
                    return ValueRook;
                case Definitions.QUEEN:
                    return ValueQueen;
                case Definitions.KING:
                    return ValueKing;
                default:
                    return 0;
            }
        }

        private readonly float[] PawnPositionValue = new float[64] // pawn in center are more valuable (at least in opening)
        {
            1.00f, 1.00f, 1.00f, 1.00f, 1.00f, 1.00f, 1.00f, 1.00f, 
            1.00f, 1.00f, 1.00f, 1.00f, 1.00f, 1.00f, 1.00f, 1.00f,
            1.00f, 1.00f, 1.05f, 1.10f, 1.10f, 1.05f, 1.00f, 1.00f,
            1.00f, 1.00f, 1.10f, 1.20f, 1.20f, 1.10f, 1.00f, 1.00f,
            1.00f, 1.00f, 1.10f, 1.20f, 1.20f, 1.10f, 1.00f, 1.00f,
            1.00f, 1.00f, 1.05f, 1.10f, 1.10f, 1.05f, 1.00f, 1.00f,
            1.00f, 1.00f, 1.00f, 1.00f, 1.00f, 1.00f, 1.00f, 1.00f,
            1.00f, 1.00f, 1.00f, 1.00f, 1.00f, 1.00f, 1.00f, 1.00f,
        };

        private readonly float[] KnightPositionValue = new float[64] // Der Springer am Rande ist eine Schande
        {
            0.95f, 0.95f, 0.95f, 0.95f, 0.95f, 0.95f, 0.95f, 0.95f,
            0.95f, 1.00f, 1.00f, 1.00f, 1.00f, 1.00f, 1.00f, 0.95f,
            0.95f, 1.00f, 1.00f, 1.00f, 1.00f, 1.00f, 1.00f, 0.95f,
            0.95f, 1.00f, 1.00f, 1.00f, 1.00f, 1.00f, 1.00f, 0.95f,
            0.95f, 1.00f, 1.00f, 1.00f, 1.00f, 1.00f, 1.00f, 0.95f,
            0.95f, 1.00f, 1.00f, 1.00f, 1.00f, 1.00f, 1.00f, 0.95f,
            0.95f, 1.00f, 1.00f, 1.00f, 1.00f, 1.00f, 1.00f, 0.95f,
            0.95f, 0.95f, 0.95f, 0.95f, 0.95f, 0.95f, 0.95f, 0.95f,
        };

    }
}
