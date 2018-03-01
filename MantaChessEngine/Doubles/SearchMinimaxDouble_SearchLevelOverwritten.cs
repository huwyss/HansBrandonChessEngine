using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MantaChessEngine.Doubles
{
    public class SearchMinimaxDouble_SearchLevelOverwritten : SearchMinimax
    {
        private MoveAndScore[] _moveAndScores;

        public SearchMinimaxDouble_SearchLevelOverwritten(IEvaluator evaluator, IMoveGenerator moveGenerator, MoveAndScore[] scoresAndMoves) 
            : base(evaluator, moveGenerator)
        {
            _moveAndScores = scoresAndMoves;
        }

        internal override IMove SearchLevel(IBoard board, Definitions.ChessColor color, int level, out float score)
        {
            score = _moveAndScores[level].Score;
            return _moveAndScores[level].Move;
        }
    }

    public class MoveAndScore
    {
        public IMove Move { get; set; }
        public float Score { get; set; }
    }

}
