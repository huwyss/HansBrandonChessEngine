using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MantaCommon
{
    public interface IMoveRatingFactory<TMove> where TMove : IGenericMove
    {
        IMoveRating<TMove> CreateMoveRating();

        IMoveRating<TMove> CreateMoveRating(int score, int evaluationLevel);

        IMoveRating<TMove> CreateMoveRatingWithWorstScore(ChessColor color);

        IMoveRating<TMove> CreateMoveRatingForGameEnd(ChessColor color, int curentLevel);

        IMoveRating<TMove> CreateMoveRatingSearchAborted();
    }
}
