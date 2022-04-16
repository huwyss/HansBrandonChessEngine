using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MantaChessEngine
{
    /// <summary>
    /// Pawn is promoted to queen, rook, bishop or knight.
    /// </summary>
    public class PromotionMove : MoveBase
    {
        private Piece _pawn = null;
        private Piece _promotionPiece = null;

        public PromotionMove(Piece movingPiece, char sourceFile, int sourceRank, char targetFile, int targetRank, Piece capturedPiece, char promotionPiece)
            : base(movingPiece, sourceFile, sourceRank, targetFile, targetRank, capturedPiece)
        {
            MakePromotionPiece(promotionPiece);
        }

        public PromotionMove(Piece movingPiece, int sourceFile, int sourceRank, int targetFile, int targetRank, Piece capturedPiece, char promotionPiece)
            : base(movingPiece, sourceFile, sourceRank, targetFile, targetRank, capturedPiece)
        {
            MakePromotionPiece(promotionPiece);
        }

        private void MakePromotionPiece(char promotionPiece)
        {
            _promotionPiece = Piece.MakePiece(promotionPiece, Color);
        }

        public override bool Equals(System.Object obj)
        {
            if (!(obj is PromotionMove))
            {
                return false;
            }

            var otherMove = obj as PromotionMove;
            var equalPromotion = _promotionPiece.Equals(otherMove._promotionPiece);

            return base.Equals(obj) && equalPromotion;
        }

        public override string ToString()
        {
            string moveString = base.ToString();
            return moveString;
        }

        public override string ToUciString()
        {
            return base.ToUciString() + _promotionPiece.UniversalSymbol;
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public override void ExecuteMove(IBoard board)
        {
            base.ExecuteMove(board);

            // replace pawn with promotion piece (queen, rook, bishop or knight)
            _pawn = board.GetPiece(TargetFile, TargetRank);
            board.SetPiece(_promotionPiece, TargetFile, TargetRank); 
        }

        public override void UndoMove(IBoard board)
        {
            board.SetPiece(_pawn, TargetFile, TargetRank);
            base.UndoMove(board);
        }
    }
}
