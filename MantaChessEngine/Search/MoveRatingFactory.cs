using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MantaCommon;

namespace MantaChessEngine
{
    public class MoveRatingFactory : IMoveRatingFactory<IMove>
    {
        private readonly IMoveGenerator<IMove> _moveGenerator;

        private const int ScoreWhiteWins = 10000;
        private const int ScoreBlackWins = -10000;
        private const int SignificantFactor = 8; // fast multiplier

        public MoveRatingFactory(IMoveGenerator<IMove> moveGenerator)
        {
            _moveGenerator = moveGenerator;
        }

        public IMoveRating<IMove> CreateMoveRating(int score, int evaluationLevel)
        {
            return new MoveRating() { Score = score, EvaluationLevel = evaluationLevel };
        }

        public IMoveRating<IMove> CreateMoveRatingWithWorstScore(ChessColor color)
        {
            if (color == ChessColor.White)
            {
                return new MoveRating() { Score = int.MinValue };
            }
            else
            {
                return new MoveRating() { Score = int.MaxValue };
            }
        }

        public IMoveRating<IMove> CreateMoveRating()
        {
            return new MoveRating();
        }

        public IMoveRating<IMove> CreateMoveRatingForGameEnd(ChessColor color, int curentLevel)
        {
            int score;
            bool whiteWins = false;
            bool blackWins = false;
            bool stallmate = false;

            if (_moveGenerator.IsCheck(color))
            {
                if (color == ChessColor.White)
                {
                    score = ScoreBlackWins + curentLevel * SignificantFactor;
                    blackWins = true;
                }
                else
                {
                    score = ScoreWhiteWins - curentLevel * SignificantFactor;
                    whiteWins = true;
                }
            }
            else
            {
                score = 0;
                stallmate = true;
            }

            return new MoveRating()
            {
                Score = score,
                WhiteWins = whiteWins,
                BlackWins = blackWins,
                Stallmate = stallmate,
                Move = new NoLegalMove(),
            };
        }

        public IMoveRating<IMove> CreateMoveRatingSearchAborted()
        {
            return new MoveRating() { SearchAborted = true };
        }
    }
}
