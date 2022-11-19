using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MantaChessEngine
{
    public struct BitPiece
    {
        public BitPiece(byte color, byte piece)
        {
            Color = color;
            Piece = piece;
        }

        public byte Color { get; }
        public byte Piece { get; }
    }
}
