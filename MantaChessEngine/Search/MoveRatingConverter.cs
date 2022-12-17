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

            uciMoveRating.Alpha = moveRating.Alpha;
            uciMoveRating.Beta = moveRating.Beta;
            
            uciMoveRating.Depth = moveRating.Depth;
            uciMoveRating.EvaluatedPositions = moveRating.EvaluatedPositions;
            uciMoveRating.EvaluationLevel = moveRating.EvaluationLevel;
            uciMoveRating.PruningCount = moveRating.PruningCount;
            uciMoveRating.Score = moveRating.Score;
            uciMoveRating.SelectiveDepth = moveRating.SelectiveDepth;
            
            uciMoveRating.Stallmate = moveRating.Stallmate;
            uciMoveRating.WhiteWins = moveRating.WhiteWins;
            uciMoveRating.BlackWins = moveRating.BlackWins;

            return uciMoveRating;
        }
    }
}
