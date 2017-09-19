using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MantaChessEngine
{
    public class Knight : SingleStepPiece
    {
        public Knight(Definitions.ChessColor color) : base(color)
        {
        }

        public override char Symbol
        {
            get
            {
                return Color == Definitions.ChessColor.White ? 'N' : 'n';
            }
        }

        public override IEnumerable<string> GetMoveDirectionSequences()
        {
            return new List<string>() { "uul", "uur", "rru", "rrd", "ddr", "ddl", "lld", "llu" }; // up up left, up up right, ...
        }

        public override bool Equals(object obj)
        {
            if (obj is Knight)
            {
                return base.Equals(obj);
            }

            return false;
        }
    }
}
