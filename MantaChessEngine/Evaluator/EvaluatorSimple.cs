using MantaCommon;

namespace MantaChessEngine
{
    public class EvaluatorSimple : IEvaluator
    {
        private readonly IBoard _board;

        private static readonly int ValuePawn = Definitions.ValuePawn;
        private static readonly int ValueKnight = Definitions.ValueKnight;
        private static readonly int ValueBishop = Definitions.ValueBishop;
        private static readonly int ValueRook = Definitions.ValueRook;
        private static readonly int ValueQueen = Definitions.ValueQueen;

        public EvaluatorSimple(IBoard board)
        {
            _board = board;
        }

        /// <summary>
        /// Calculates the score from white's point. + --> white is better, - --> black is better.
        /// </summary>
        public int Evaluate()
        {
            var scoreWhite = 0;
            var scoreBlack = 0;

            for (var square = Square.A1; square <= Square.H8; square++)
            {
                Piece piece = _board.GetPiece(square);
                int pieceScore = GetPieceScore(piece);
                if (_board.GetColor(square) == ChessColor.White)
                {
                    scoreWhite += pieceScore;
                }
                else
                {
                    scoreBlack += pieceScore;
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
