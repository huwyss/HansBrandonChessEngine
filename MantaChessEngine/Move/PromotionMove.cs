using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MantaChessEngine
{
    /// <summary>
    /// Pawn is promoted to queen. Minor promotions are not supported.
    /// </summary>
    public class PromotionMove : MoveBase
    {
        private Piece _pawn = null;
        private Piece _queen = null;

        public PromotionMove(Piece movingPiece, char sourceFile, int sourceRank, char targetFile, int targetRank, Piece capturedPiece)
            : base(movingPiece, sourceFile, sourceRank, targetFile, targetRank, capturedPiece)
        {
            MakeQueen();
        }

        public PromotionMove(Piece movingPiece, int sourceFile, int sourceRank, int targetFile, int targetRank, Piece capturedPiece)
            : base(movingPiece, sourceFile, sourceRank, targetFile, targetRank, capturedPiece)
        {
            MakeQueen();
        }

        private void MakeQueen()
        {
            _queen = Piece.MakePiece(Definitions.QUEEN, Color);
        }

        public override bool Equals(System.Object obj)
        {
            if (!(obj is PromotionMove))
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

        public override void ExecuteMove(Board board)
        {
            base.ExecuteMove(board);

            // replace pawn with queen
            _pawn = board.GetPiece(TargetFile, TargetRank);
            board.SetPiece(_queen, TargetFile, TargetRank); 
        }

        public override void UndoMove(Board board)
        {
            board.SetPiece(_pawn, TargetFile, TargetRank);
            base.UndoMove(board);
        }
    }
}
