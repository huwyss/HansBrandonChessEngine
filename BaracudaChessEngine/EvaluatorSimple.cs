using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaracudaChessEngine
{
    public class EvaluatorSimple : IEvaluator
    {
        public float ValuePawn = 1f;
        public float ValueKnight = 3f;
        public float ValueBishop = 3f;
        public float ValueRook = 5f;
        public float ValueQueen = 9f;
        public float ValueKing = 100f;

        public Score Evaluate(Board board)
        {
            float scoreWhite = 0;
            float scoreBlack = 0;

            for (int file = 1; file <= 8; file++)
            {
                for (int rank = 1; rank <= 8; rank++)
                {
                    char piece = board.GetPiece(file, rank);
                    float pieceScore = GetPieceScore(piece);
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

            return new Score(scoreWhite, scoreBlack);
        }

        private float GetPieceScore(char piece)
        {
            switch (piece.ToString().ToLower()[0])
            {
                case Definitions.PAWN:
                    return ValuePawn;
                case Definitions.KNIGHT:
                    return ValueKnight;
                case Definitions.BISHOP:
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

    }
}
