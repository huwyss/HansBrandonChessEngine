using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MantaCommon;

namespace MantaChessEngine
{
    public class Knight : SingleStepPiece
    {
        public Knight(ChessColor color) : base(color)
        {
        }

        public override char Symbol
        {
            get
            {
                return Color == ChessColor.White ? 'N' : 'n';
            }
        }

        public override PieceType PieceType => PieceType.Knight;

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

        public override int GetPlainPieceValue()
        {
            return Definitions.ValueKnight;
        }
    }
}
