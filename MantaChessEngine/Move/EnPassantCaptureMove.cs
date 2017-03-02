using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MantaChessEngine
{
    public class EnPassantCaptureMove : Move
    {
        public EnPassantCaptureMove(char movingPiece, int sourceFile, int sourceRank, int targetFile, int targetRank, char capturedPiece)
            : base(movingPiece, sourceFile, sourceRank, targetFile, targetRank, capturedPiece, true)
        {
        }

        public override int CapturedRank // for en passant capture it is different from TargetRank
        {
            get
            {
                if (Color == Definitions.ChessColor.White)
                {
                    return TargetRank - 1;
                }

                return TargetRank + 1;
            }
        }

        public override void ExecuteMove(Board board)
        {
            board.SetPiece(Definitions.EmptyField, CapturedFile, CapturedRank); // remove captured pawn if it is en passant
            base.ExecuteMove(board);
        }

        public override void UndoMove(Board board)
        {
            // note: CapturedRank is overridden and therefore taken from this class!
            base.UndoMove(board);
        }
    }
}