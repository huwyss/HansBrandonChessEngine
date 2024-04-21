using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBCommon
{
    public interface IMoveGenerator<TMove> where TMove : IGenericMove
    {
        IEnumerable<TMove> GetAllMoves(ChessColor color);
        IEnumerable<TMove> GetAllCaptures(ChessColor color);
        bool IsAttacked(ChessColor color, Square square);
        bool IsCheck(ChessColor color);
        bool IsMoveValid(TMove move);
    }
}
