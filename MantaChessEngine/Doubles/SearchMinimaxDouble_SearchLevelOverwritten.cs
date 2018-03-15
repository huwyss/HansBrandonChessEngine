using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MantaChessEngine.Doubles
{
    public class SearchMinimaxDouble_SearchLevelOverwritten : SearchMinimax
    {
        private MoveAndScore[] _moveAndRating;

        public SearchMinimaxDouble_SearchLevelOverwritten(IEvaluator evaluator, IMoveGenerator moveGenerator, MoveAndScore[] scoresAndMoves) 
            : base(evaluator, moveGenerator)
        {
            _moveAndRating = scoresAndMoves;
        }

        internal override IMove SearchLevel(IBoard board, Definitions.ChessColor color, int level, out Rating rating)
        {
            rating = _moveAndRating[level].Rating;
            return _moveAndRating[level].Move;
        }
    }

    public class MoveAndScore
    {
        public IMove Move { get; set; }
        public Rating Rating { get; set; }
    }

}
