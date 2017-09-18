using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MantaChessEngine
{
    public class Bishop : Piece
    {
        public Bishop(Definitions.ChessColor color) : base(color)
        {
        }

        public override char Symbol
        {
            get
            {
                return Color == Definitions.ChessColor.White ? 'B' : 'b';
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is Bishop)
            {
                return base.Equals(obj);
            }

            return false;
        }
    }
}
