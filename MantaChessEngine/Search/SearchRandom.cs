using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MantaChessEngine
{
    public class SearchRandom : ISearchService
    {
        private Random _rand;
        private IMoveGenerator _moveGenerator;

        public void SetMaxDepth(int maxLevel) { }

        public void SetAdditionalSelectiveDepth(int additionalDepth) {  }

        public void SetPreviousPV(MoveRating previousPV) { }

        public SearchRandom(IMoveGenerator moveGenerator)
        {
            _moveGenerator = moveGenerator;
            _rand = new Random();
        }

        public MoveRating Search(IBoard board, Definitions.ChessColor color)
        {
            IMove nextMove = null;
            var possibleMovesComputer = _moveGenerator.GetLegalMoves(board, color).ToList<IMove>();
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
