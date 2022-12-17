using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MantaCommon;

namespace MantaChessEngine
{
    public class Rook : MultiStepPiece
    {
        public Rook(ChessColor color) : base(color)
        {
        }

        public override char Symbol
        {
            get
            {
                return Color == ChessColor.White ? 'R' : 'r';
            }
        }

        public override IEnumerable<string> GetMoveDirectionSequences()
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

        public override int GetPlainPieceValue()
        {
            return Definitions.ValueRook;
        }
    }
}
