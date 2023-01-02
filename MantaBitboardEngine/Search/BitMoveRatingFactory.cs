using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MantaCommon;

namespace MantaBitboardEngine
{
    public class BitMoveRatingFactory : IMoveRatingFactory<BitMove>
    {
        private readonly BitMoveGenerator _moveGenerator;

        public BitMoveRatingFactory(BitMoveGenerator moveGenerator)
        {
            _moveGenerator = moveGenerator;
        }

        public IMoveRating<BitMove> CreateMoveRating(int score, int evaluationLevel)
        {
            return new BitMoveRating() { Score = score, EvaluationLevel = evaluationLevel };
        }

        public IMoveRating<BitMove> CreateMoveRatingWithWorstScore(ChessColor color)
        {
            if (color == ChessColor.White)
            {
                return new BitMoveRating() { Score = int.MinValue };
            }
            else
            {
                return new BitMoveRating() { Score = int.MaxValue };
            }
        }

        public IMoveRating<BitMove> CreateMoveRating()
        {
            return new BitMoveRating();
        }

        public IMoveRating<BitMove> CreateMoveRatingForGameEnd(ChessColor color, int curentLevel)
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

            return new BitMoveRating()
            {
                Score = score,
                WhiteWins = whiteWins,
                BlackWins = blackWins,
                Stallmate = stallmate,
                Move = BitMove.CreateEmptyMove(), /// new NoLegalMove(),
            };
        }

        private const int ScoreWhiteWins = 10000;
        private const int ScoreBlackWins = -10000;
        private const int SignificantFactor = 8; // fast multiplier
    }
}
