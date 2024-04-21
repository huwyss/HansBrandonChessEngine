using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HBCommon;

namespace HansBrandonChessEngine
{
    public class NormalMove : MoveBase
    {
        public NormalMove(Piece movingPiece, Square fromSquare, Square toSquare, Piece capturedPiece)
            : base(movingPiece, fromSquare, toSquare, capturedPiece)
        {
        }

        public override bool Equals(System.Object obj)
        {
            if (!(obj is NormalMove))
            {
                return false;
            }

            return base.Equals(obj);
        }

        public override string ToString()
        {
            string moveString = base.ToString();
            return moveString;
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }
    }
}
