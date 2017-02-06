using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaracudaChessEngine
{
    public class SearchRandom : ISearchService
    {
        private Random _rand;

        public SearchRandom()
        {
            _rand = new Random();
        }

        public Move Search(Board board, Definitions.ChessColor color)
        {
            Move nextMove = null;
            var possibleMovesComputer = board.GetAllMoves(color);
            int numberPossibleMoves = possibleMovesComputer.Count;

            if (numberPossibleMoves > 0)
            {
                int randomMoveIndex = _rand.Next(0, numberPossibleMoves - 1);
                nextMove = possibleMovesComputer[randomMoveIndex];
            }

            return nextMove;
        }
    }
}
