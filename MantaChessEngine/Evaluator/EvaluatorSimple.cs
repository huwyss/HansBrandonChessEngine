using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MantaChessEngine
{
    public class EvaluatorSimple : IEvaluator
    {
        public float ValuePawn = 1f;
        public float ValueKnight = 3f;
        public float ValueBishop = 3f;
        public float ValueRook = 5f;
        public float ValueQueen = 9f;
        public float ValueKing = 10000f;

        /// <summary>
        /// Calculates the score from white's point. + --> white is better, - --> black is better.
        /// </summary>
        public float Evaluate(IBoard board)
        {
            float scoreWhite = 0;
            float scoreBlack = 0;

            for (int file = 1; file <= 8; file++)
            {
                for (int rank = 1; rank <= 8; rank++)
                {
                    Piece piece = board.GetPiece(file, rank);
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

            return scoreWhite - scoreBlack;
        }

        private float GetPieceScore(Piece piece)
        {
            if (piece is Pawn)
                return ValuePawn;
            else if (piece is Knight)
                return ValueKnight;
            else if (piece is Bishop)
                return ValueBishop;
            else if (piece is Rook)
                return ValueRook;
            else if (piece is Queen)
                return ValueQueen;
            else if (piece is King)
                return ValueKing;
            else
                return 0;
        }
    }
}
