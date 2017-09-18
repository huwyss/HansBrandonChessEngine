using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MantaChessEngine
{
    public class Pawn : Piece
    {
        public Pawn(Definitions.ChessColor color) : base(color)
        {
        }

        public override char Symbol
        {
            get
            {
                return Color == Definitions.ChessColor.White ? 'P' : 'p';
            }
        }

        public override IEnumerable<string> GetMoveDirectionSequences()
        {
            return new List<string>() { "u", "uu", "ul", "ur" }; // up, up up, up left, up right
        }

        public override bool Equals(object obj)
        {
            if (obj is Pawn)
            {
                return base.Equals(obj);
            }

            return false;
        }
    }
}
