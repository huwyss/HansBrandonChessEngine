using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MantaChessEngine
{
    public class King : Piece
    {
        public King(Definitions.ChessColor color) : base(color)
        {
        }

        public override char Symbol
        {
            get
            {
                return Color == Definitions.ChessColor.White ? 'K' : 'k';
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is King)
            {
                return base.Equals(obj);
            }

            return false;
        }
    }
}
