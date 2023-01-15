using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MantaCommon;

namespace MantaChessEngine
{
    /// <summary>
    /// Pawn is promoted to queen, rook, bishop or knight.
    /// </summary>
    public class PromotionMove : MoveBase
    {
        private Piece _pawn = null;
        private Piece _promotionPiece = null;

        public PromotionMove(Piece movingPiece, Square fromSquare, Square toSquare, Piece capturedPiece, PieceType promotionPiece)
            : base(movingPiece, fromSquare, toSquare, capturedPiece)
        {
            MakePromotionPiece(promotionPiece);
        }

        public override PieceType PromotionPiece
        {
            get
            {
                return GePieceType(_promotionPiece.Symbol);
            }
        }

        private static PieceType GePieceType(char symbol)
        {
            switch (symbol)
            {
                case 'P':
                case 'p':
                    return PieceType.Pawn;
                case 'N':
                case 'n':
                    return PieceType.Knight;
                case 'B':
                case 'b':
                    return PieceType.Bishop;
                case 'R':
                case 'r':
                    return PieceType.Rook;
                case 'Q':
                case 'q':
                    return PieceType.Queen;
                default:
                    return PieceType.Empty;
            }
        }

        private void MakePromotionPiece(PieceType promotionPiece)
        {
            _promotionPiece = Piece.MakePiece(promotionPiece, MovingColor);
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

            // Store the pawn for UndoMove()
            _pawn = board.GetPiece(ToSquare); 
                                              
            // Replace pawn with promotion piece (queen, rook, bishop or knight)
            board.RemovePiece(ToSquare); // 
            board.SetPiece(_promotionPiece, ToSquare); 
        }

        public override void UndoMove(IBoard board)
        {
            // Replace promotion piece with stored pawn
            board.RemovePiece(ToSquare);
            board.SetPiece(_pawn, ToSquare);

            base.UndoMove(board);
        }
    }
}
