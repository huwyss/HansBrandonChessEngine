using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBCommon
{
    public interface IMoveFactory<TMove> where TMove : IGenericMove
    {
        TMove MakeMove(Square fromSquare, Square toSquare, PieceType promotionPiece);

        TMove MakeMoveUci(string moveStringUci);
    }
}
