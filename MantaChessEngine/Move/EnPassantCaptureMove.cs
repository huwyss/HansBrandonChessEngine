using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MantaChessEngine
{
    public class EnPassantCaptureMove : MoveBase
    {
        public EnPassantCaptureMove(char movingPiece, int sourceFile, int sourceRank, int targetFile, int targetRank, char capturedPiece)
            : base(movingPiece, sourceFile, sourceRank, targetFile, targetRank, capturedPiece)
        {
        }

        public EnPassantCaptureMove(string moveStringUser)
            : base(moveStringUser)
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
            string moveString = "";
            moveString += Helper.FileToFileChar(SourceFile);
            moveString += SourceRank.ToString();
            moveString += Helper.FileToFileChar(TargetFile);
            moveString += TargetRank;
            moveString += CapturedPiece;
            moveString += "e";
            return moveString;
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }
    }
}