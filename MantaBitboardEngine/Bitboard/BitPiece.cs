using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MantaCommon;

namespace MantaBitboardEngine
{
    public struct BitPiece
    {
        public BitPiece(ChessColor color, PieceType piece)
        {
            Color = color;
            Piece = piece;
        }

        public ChessColor Color { get; }
        public PieceType Piece { get; }
    }
}
