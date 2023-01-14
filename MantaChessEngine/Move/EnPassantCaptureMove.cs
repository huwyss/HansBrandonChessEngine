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

        public override Square CapturedSquare // for en passant capture it is different from TargetSquare
        {
            get
            {
                if (MovingColor == ChessColor.White)
                {
                    return ToSquare - 8;
                }

                return ToSquare + 8;
            }
        }

        public override void ExecuteMove(IBoard board)
        {
            board.SetPiece(null /*Definitions.EmptyField*/, CapturedSquare); // remove captured pawn if it is en passant
            base.ExecuteMove(board);
        }

        public override void UndoMove(IBoard board)
        {
            // note: CapturedRank is overridden and therefore taken from this class!
            base.UndoMove(board);
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