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

            var castlingDoneWhiteQueenSide = false;
            var castlingDoneRightWhiteKingSide = false;
            var castlingDoneRightBlackQueenSide = false;
            var castlingDoneRightBlackKingSide = false;

            switch (bitMove.Castling)
            {
                case CastlingType.KingSide:
                    if (bitMove.MovingColor == BitColor.White)
                    {
                        bitBoards.SetPiece(BitColor.Empty, BitPieceType.Empty, Square.H1);
                        bitBoards.SetPiece(BitColor.White, BitPieceType.Rook, Square.F1);
                        castlingDoneRightWhiteKingSide = true;
                    }
                    else
                    {
                        bitBoards.SetPiece(BitColor.Empty, BitPieceType.Empty, Square.H8);
                        bitBoards.SetPiece(BitColor.White, BitPieceType.Rook, Square.F8);
                        castlingDoneRightBlackKingSide = true;
                    }
                    break;

                case CastlingType.QueenSide:
                    if (bitMove.MovingColor == BitColor.White)
                    {
                        bitBoards.SetPiece(BitColor.Empty, BitPieceType.Empty, Square.A1);
                        bitBoards.SetPiece(BitColor.White, BitPieceType.Rook, Square.D1);
                        castlingDoneWhiteQueenSide = true;
                    }
                    else
                    {
                        bitBoards.SetPiece(BitColor.Empty, BitPieceType.Empty, Square.A8);
                        bitBoards.SetPiece(BitColor.White, BitPieceType.Rook, Square.D8);
                        castlingDoneRightBlackQueenSide = true;
                    }
                    break;

                default:
                    break;
            }



            bitBoards.BoardState.Add(
                bitMove,
                GetEnPassantSquare(bitMove),
                bitBoards.BoardState.LastCastlingRightWhiteQueenSide && !castlingDoneWhiteQueenSide,
                bitBoards.BoardState.LastCastlingRightWhiteKingSide && !castlingDoneRightWhiteKingSide,
                bitBoards.BoardState.LastCastlingRightWhiteQueenSide && !castlingDoneRightBlackQueenSide,
                bitBoards.BoardState.LastCastlingRightWhiteKingSide && !castlingDoneRightBlackKingSide,
                BitHelper.OtherColor(bitMove.MovingColor));
        }

        private Square GetEnPassantSquare(BitMove move)
        {
            var enPassantSquare = Square.NoSquare;

            if (move.MovingPiece != BitPieceType.Pawn)
            {
                return enPassantSquare;
            }

            // black en passant field
            if (move.MovingColor == BitColor.Black) // black pawn
            {
                if (move.FromSquare - move.ToSquare == 16) // 2 fields move
                {
                    enPassantSquare = move.FromSquare - 8;
                }
            }
            else // white pawn
            {
                if (move.ToSquare - move.FromSquare == 16) // 2 fields move
                {
                    enPassantSquare = move.FromSquare + 8;
                }
            }

            return enPassantSquare;
        }
    }
}
