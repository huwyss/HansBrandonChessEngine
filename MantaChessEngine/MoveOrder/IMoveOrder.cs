using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MantaChessEngine.Definitions;

namespace MantaChessEngine
{
    public interface IMoveOrder
    {
        IEnumerable<IMove> OrderMoves(IEnumerable<IMove> unsortedMoves, ChessColor movingColor);
    }

    public class MoveOrder : IMoveOrder
    {
        public IEnumerable<IMove> OrderMoves(IEnumerable<IMove> unsortedMoves, ChessColor movingColor)
        {
            List<IMove> sortedMoves;
            sortedMoves = movingColor == ChessColor.White
                ? unsortedMoves.OrderBy(m => m.GetMoveImportance()).ToList()
                : unsortedMoves.OrderByDescending(m => m.GetMoveImportance()).ToList();

            return sortedMoves;
        }
    }
}
