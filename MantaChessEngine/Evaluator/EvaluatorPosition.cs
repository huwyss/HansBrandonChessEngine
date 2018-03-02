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
        public readonly float ValueKing = 10000f;

        private readonly float AdvantageDoubleBishop = 0.5f;
        private readonly float CastlingScore = 1f;

        private int numberWhiteBishop = 0;
        private int numberBlackBishop = 0;

        /// <summary>
        /// Calculates the score from white's point. + --> white is better, - --> black is better.
        /// </summary>
        public float Evaluate(IBoard board)
        {
            float scoreWhite = 0;
            float scoreBlack = 0;

            numberWhiteBishop = 0;
            numberBlackBishop = 0;

            for (int file = 1; file <= 8; file++)
            {
                for (int rank = 1; rank <= 8; rank++)
                {
                    Piece piece = board.GetPiece(file, rank);
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

            if (board.WhiteDidCastling)
            {
                scoreWhite += CastlingScore;
            }

            if (board.BlackDidCastling)
            {
                scoreBlack += CastlingScore;
            }

            float score = scoreWhite - scoreBlack;
            score = score < -1000 ? Definitions.ScoreBlackWins : score;
            score = score > 1000 ? Definitions.ScoreWhiteWins : score;

            return score;
        }

        private float GetPieceScore(Piece piece, int file, int rank)
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
            else if (piece is King)
                return ValueKing;
            else
                return 0;
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
