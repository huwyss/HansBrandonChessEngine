using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HansBrandonChessEngine;
using HBCommon;

namespace HansBrandonChessEngineTest.Doubles
{
    public class SearchMinimaxDouble_SearchLevelOverwritten : SearchMinimax
    {
        private MoveAndScore[] _moveAndRating;

        public SearchMinimaxDouble_SearchLevelOverwritten(IBoard board, IEvaluator evaluator, IMoveGenerator<IMove> moveGenerator, MoveAndScore[] scoresAndMoves) 
            : base(board, evaluator, moveGenerator)
        {
            _moveAndRating = scoresAndMoves;
        }

        internal override IEnumerable<IMoveRating<IMove>> SearchLevel(ChessColor color, int level)
        {
            //rating = _moveAndRating[level].Rating;
            //return _moveAndRating[level].Move;
            return null;
        }
    }

    public class MoveAndScore
    {
        public IMove Move { get; set; }
        public MoveRating Rating { get; set; }
    }

}
