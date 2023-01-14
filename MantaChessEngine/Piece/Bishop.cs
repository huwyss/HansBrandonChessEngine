using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MantaCommon;

namespace MantaChessEngine
{
    public class Bishop : MultiStepPiece
    {
        public Bishop(ChessColor color) : base(color)
        {
        }

        public override char Symbol
        {
            get
            {
                return Color == ChessColor.White ? 'B' : 'b';
            }
        }

        public override PieceType PieceType => PieceType.Bishop;

        public override IEnumerable<string> GetMoveDirectionSequences()
        {
            return new List<string>() { "ur", "rd", "dl", "lu" }; // up right, right down, down left, left up
        }
        
        public override bool Equals(object obj)
        {
            if (obj is Bishop)
            {
                return base.Equals(obj);
            }

            return false;
        }

        public override int GetPlainPieceValue()
        {
            return Definitions.ValueBishop;
        }
    }
}
