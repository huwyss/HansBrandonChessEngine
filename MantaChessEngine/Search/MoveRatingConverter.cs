using MantaCommon;
using System.Collections.Generic;

namespace MantaChessEngine
{
    public class MoveRatingConverter
    {
        public static UciMoveRating NewFrom(MoveRating moveRating)
        {
            var uciMoveRating = new UciMoveRating();
            uciMoveRating.Move = moveRating.Move.ToUciString();
            uciMoveRating.MovingColor = moveRating.Move.Color;
            uciMoveRating.PrincipalVariation = new List<string>();
            foreach (var move in moveRating.PrincipalVariation)
            {
                uciMoveRating.PrincipalVariation.Add(move.ToUciString());
            }

            return uciMoveRating;
        }
    }
}
