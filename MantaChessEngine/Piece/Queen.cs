using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MantaChessEngine
{
    public class Queen : Piece
    {
        public Queen(Definitions.ChessColor color) : base(color)
        {
        }

        public override char Symbol
        {
            get
            {
                return Color == Definitions.ChessColor.White ? 'Q' : 'q';
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is Queen)
            {
                return base.Equals(obj);
            }

            return false;
        }
    }
}
