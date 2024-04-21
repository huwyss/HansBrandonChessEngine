using HBCommon;
using System.Collections.Generic;

namespace HansBrandonChessEngine
{
    public class MoveRatingConverter
    {
        public static UciMoveRating NewFrom(IMoveRating<IMove> moveRating)
        {
            var uciMoveRating = new UciMoveRating();

            if (moveRating.SearchAborted)
            {
                uciMoveRating.SearchAborted = true;
                return uciMoveRating;
            }

            uciMoveRating.Move = moveRating.Move.ToUciString();
            uciMoveRating.MovingColor = moveRating.Move.MovingColor;
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
