using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MantaChessEngine
{
    public class Rook : Piece
    {
        public Rook(Definitions.ChessColor color) : base(color)
        {
        }

        public override char Symbol
        {
            get
            {
                return Color == Definitions.ChessColor.White ? 'R' : 'r';
            }
        }

        public override IEnumerable<string> GetMoveSequences()
        {
            return new List<string>() { "u", "r", "d", "l" }; // up, right, down, left
        }

        public override bool Equals(object obj)
        {
            if (obj is Rook)
            {
                return base.Equals(obj);
            }

            return false;
        }
    }
}
