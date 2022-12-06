using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MantaBitboardEngine
{
    public class BitMoveExecutor
    {
        public void DoMove(BitMove bitMove, IBitBoard bitBoards)
        {
            bitBoards.RemovePiece(bitMove.FromSquare);
            if (bitMove.CapturedSquare != Square.NoSquare)
            {
                bitBoards.RemovePiece(bitMove.CapturedSquare);
            }

            if (bitMove.IsPromotionMove())
            {
                bitBoards.SetPiece(bitMove.MovingColor, bitMove.PromotionPiece, bitMove.ToSquare);
            }
            else
            {
                bitBoards.SetPiece(bitMove.MovingColor, bitMove.MovingPiece, bitMove.ToSquare);
            }

            switch (bitMove.Castling)
            {
                case CastlingType.KingSide:
                    if (bitMove.MovingColor == BitColor.White)
                    {
                        bitBoards.SetPiece(BitColor.Empty, BitPieceType.Empty, Square.H1);
                        bitBoards.SetPiece(BitColor.White, BitPieceType.Rook, Square.F1);
                    }
                    else
                    {
                        bitBoards.SetPiece(BitColor.Empty, BitPieceType.Empty, Square.H8);
                        bitBoards.SetPiece(BitColor.White, BitPieceType.Rook, Square.F8);
                    }
                    break;

                case CastlingType.QueenSide:
                    if (bitMove.MovingColor == BitColor.White)
                    {
                        bitBoards.SetPiece(BitColor.Empty, BitPieceType.Empty, Square.A1);
                        bitBoards.SetPiece(BitColor.White, BitPieceType.Rook, Square.D1);
                    }
                    else
                    {
                        bitBoards.SetPiece(BitColor.Empty, BitPieceType.Empty, Square.A8);
                        bitBoards.SetPiece(BitColor.White, BitPieceType.Rook, Square.D8);
                    }
                    break;

                default:
                    break;
            }

            // todo: bitBoards.BoardState correctly...
            var enPassantSquare = Square.NoSquare; // todo 
            bitBoards.BoardState.Add(
                bitMove,
                enPassantSquare,
                true, // castlingRightWhiteQueenSide,
                true, // castlingRightWhiteKingSide,
                true, // castlingRightBlackQueenSide,
                true, // castlingRightBlackKingSide,
                BitHelper.OtherColor(bitMove.MovingColor));
        }
    }
}
