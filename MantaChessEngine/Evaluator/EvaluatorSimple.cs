namespace MantaChessEngine
{
    public class EvaluatorSimple : IEvaluator
    {
        private static readonly int ValuePawn = Definitions.ValuePawn;
        private static readonly int ValueKnight = Definitions.ValueKnight;
        private static readonly int ValueBishop = Definitions.ValueBishop;
        private static readonly int ValueRook = Definitions.ValueRook;
        private static readonly int ValueQueen = Definitions.ValueQueen;

        /// <summary>
        /// Calculates the score from white's point. + --> white is better, - --> black is better.
        /// </summary>
        public int Evaluate(IBoard board)
        {
            var scoreWhite = 0;
            var scoreBlack = 0;

            for (int file = 1; file <= 8; file++)
            {
                for (int rank = 1; rank <= 8; rank++)
                {
                    Piece piece = board.GetPiece(file, rank);
                    int pieceScore = GetPieceScore(piece);
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

            int score = scoreWhite - scoreBlack;

            return score;
        }

        private int GetPieceScore(Piece piece)
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
            else
                return 0;
        }
    }
}
