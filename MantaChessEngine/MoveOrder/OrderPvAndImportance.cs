using System.Linq;
using System.Collections.Generic;
using MantaCommon;

namespace MantaChessEngine
{
    public class OrderPvAndImportance : IMoveOrder
    {
        private IMoveRating<IMove> _previousMoveRatingPV = null;

        public void SetMoveRatingPV(IMoveRating<IMove> previousMoveRatingPV)
        {
            _previousMoveRatingPV = previousMoveRatingPV;
        }

        public IEnumerable<IMove> OrderMoves(IList<IMove> unsortedMoves, ChessColor movingColor, int currentLevel)
        {
            if (_previousMoveRatingPV == null || _previousMoveRatingPV.PrincipalVariation.Count() < currentLevel)
            {
                return OrderByImportance(unsortedMoves, movingColor);
            }

            var currentPvMove = _previousMoveRatingPV.PrincipalVariation[currentLevel - 1];
            if (currentPvMove != null && unsortedMoves.Contains(currentPvMove))
            {
                unsortedMoves.Remove(currentPvMove);
                var newlySorted = OrderByImportance(unsortedMoves, movingColor);
                newlySorted.Insert(0, currentPvMove); // we start with the stored pv from the last search (that was one ply less deep)
                _previousMoveRatingPV.PrincipalVariation[currentLevel - 1] = null; // we have used this move up.
                return newlySorted;
            }

            return OrderByImportance(unsortedMoves, movingColor);
        }

        private IList<IMove> OrderByImportance(IList<IMove> unsortedMoves, ChessColor movingColor)
        {
            List<IMove> sortedMoves;
            sortedMoves = movingColor == ChessColor.White
                ? unsortedMoves.OrderBy(m => m.GetMoveImportance()).ToList()
                : unsortedMoves.OrderByDescending(m => m.GetMoveImportance()).ToList();

            return sortedMoves;
        }
    }
}
