using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MantaBitboardEngine
{
    public struct BitPiece
    {
        public BitPiece(BitColor color, BitPieceType piece)
        {
            Color = color;
            Piece = piece;
        }

        public BitColor Color { get; }
        public BitPieceType Piece { get; }
    }
}
