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
        private MoveGenerator _moveGenerator;

        public SearchRandom(MoveGenerator moveGenerator)
        {
            _moveGenerator = moveGenerator;
            _rand = new Random();
        }

        public IMove Search(Board board, Definitions.ChessColor color, out float score)
        {
            IMove nextMove = null;
            var possibleMovesComputer = _moveGenerator.GetAllMoves(board, color);
            int numberPossibleMoves = possibleMovesComputer.Count;

            if (numberPossibleMoves > 0)
            {
                int randomMoveIndex = _rand.Next(0, numberPossibleMoves - 1);
                nextMove = possibleMovesComputer[randomMoveIndex];
            }

            score = 0;
            return nextMove;
        }
    }
}
