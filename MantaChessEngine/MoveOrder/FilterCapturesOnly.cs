using System.Collections.Generic;

namespace MantaChessEngine
{
    public class FilterCapturesOnly : IMoveFilter
    {
        public IList<IMove> Filter(IList<IMove> possibleMovesUnsorted)
        {
            var filteredList = new List<IMove>();

            foreach (var move in possibleMovesUnsorted)
            {
                if (move.CapturedPiece != null)
                {
                    filteredList.Add(move);
                }
            }

            return filteredList;
        }
    }
}
