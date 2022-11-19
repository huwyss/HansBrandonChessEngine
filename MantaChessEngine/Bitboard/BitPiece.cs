using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MantaChessEngine
{
    public struct BitPiece
    {
        public BitPiece(ColorType color, PieceType piece)
        {
            Color = color;
            Piece = piece;
        }

        public ColorType Color { get; }
        public PieceType Piece { get; }
    }
}
