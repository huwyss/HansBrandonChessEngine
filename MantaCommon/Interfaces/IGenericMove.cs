using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MantaCommon
{
    public interface IGenericMove
    {
        ChessColor MovingColor { get; }
        Square FromSquare { get; }
        Square ToSquare { get; }
        PieceType PromotionPiece { get; }
        bool IsCapture();
    }
}
