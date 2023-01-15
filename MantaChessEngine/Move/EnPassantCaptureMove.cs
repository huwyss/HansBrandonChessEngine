using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MantaCommon;

namespace MantaChessEngine
{
    public class EnPassantCaptureMove : MoveBase
    {
        public EnPassantCaptureMove(Piece movingPiece, Square fromSquare, Square toSquare, Piece capturedPiece)
            : base(movingPiece, fromSquare, toSquare, capturedPiece)
        {
        }

        public override Square CapturedSquare // For en passant capture CapturedSquare is different from ToSquare
        {
            get
            {
                return MovingColor == ChessColor.White
                    ? ToSquare - 8
                    : ToSquare + 8;
            }
        }

        public override bool Equals(System.Object obj)
        {
            if (!(obj is EnPassantCaptureMove))
            {
                return false;
            }

            return base.Equals(obj);
        }

        public override string ToString()
        {
            string moveString = base.ToString();
            moveString += "e";
            return moveString;
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }
    }
}