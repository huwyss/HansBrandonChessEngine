using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HBCommon;

namespace HansBrandonChessEngine
{
    public class Queen : MultiStepPiece
    {
        public Queen(ChessColor color) : base(color)
        {
        }

        public override char Symbol
        {
            get
            {
                return Color == ChessColor.White ? 'Q' : 'q';
            }
        }

        public override PieceType PieceType => PieceType.Queen;
        public override IEnumerable<string> GetMoveDirectionSequences()
        {
            return new List<string>() { "u", "ur", "r", "rd", "d", "dl", "l", "lu" }; // up, up right, right, right down, ...
        }

        public override bool Equals(object obj)
        {
            if (obj is Queen)
            {
                return base.Equals(obj);
            }

            return false;
        }

        public override int GetPlainPieceValue()
        {
            return Definitions.ValueQueen;
        }
    }
}
