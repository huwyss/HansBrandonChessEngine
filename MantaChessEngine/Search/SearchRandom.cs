using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MantaCommon;

namespace MantaChessEngine
{
    public class SearchRandom : ISearchService<IMove>
    {
        private readonly IMoveGenerator<IMove> _moveGenerator;
        private Random _rand;

        public void SetMaxDepth(int maxLevel) { }

        public void SetAdditionalSelectiveDepth(int additionalDepth) {  }

        public void SetPreviousPV(MoveRating previousPV) { }

        public SearchRandom(IMoveGenerator<IMove> moveGenerator)
        {
            _moveGenerator = moveGenerator;
            _rand = new Random();
        }

        public IMoveRating<IMove> Search(ChessColor color)
        {
            IMove nextMove = null;
            var possibleMovesComputer = _moveGenerator.GetAllMoves(color).ToList<IMove>(); // todo: only legal moves
            int numberPossibleMoves = possibleMovesComputer.Count;

            if (numberPossibleMoves > 0)
            {
                int randomMoveIndex = _rand.Next(0, numberPossibleMoves - 1);
                nextMove = possibleMovesComputer[randomMoveIndex];
            }

            return new MoveRating() { Move = nextMove };
        }

        public void ClearPreviousPV()
        {
        }
    }
}
