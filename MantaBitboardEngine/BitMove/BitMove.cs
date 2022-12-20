using MantaCommon;
using System.Text;

namespace MantaBitboardEngine
{
    public struct BitMove
    {
        // capture constructor
        private BitMove(
            BitPieceType movingPiece,
            Square fromSquare,
            Square toSquare,
            BitPieceType capturedPiece,
            Square capturedSquare,
            BitPieceType promotionPiece,
            CastlingType castling,
            ChessColor movingColor,
            byte value)
        {
            MovingPiece = movingPiece;
            FromSquare = fromSquare;
            ToSquare = toSquare;
            CapturedPiece = capturedPiece;
            CapturedSquare = capturedSquare;
            PromotionPiece = promotionPiece;
            Castling = castling;
            MovingColor = movingColor;
            Value = value;
        }

        public static BitMove CreateCapture(
            BitPieceType movingPiece,
            Square fromSquare,
            Square toSquare,
            BitPieceType capturedPiece,
            Square capturedSquare,
            BitPieceType promotionPiece,
            ChessColor movingColor,
            byte value)
        {
            return new BitMove(movingPiece, fromSquare, toSquare, capturedPiece, capturedSquare, promotionPiece, CastlingType.None, movingColor, value);
        }

        // move constructor
        public static BitMove CreateMove(
            BitPieceType movingPiece,
            Square fromSquare,
            Square toSquare,
            BitPieceType promotionPiece,
            ChessColor movingColor,
            byte value)
        {
            return new BitMove(movingPiece, fromSquare, toSquare, BitPieceType.Empty, Square.NoSquare, promotionPiece, CastlingType.None, movingColor, value);
        }

        // castling constructor
        public static BitMove CreateCastling(
            ChessColor movingColor,
            CastlingType castling,
            byte value)
        {
            if (movingColor == ChessColor.White)
            {
                if (castling == CastlingType.KingSide)
                {
                    return new BitMove(BitPieceType.King, Square.E1, Square.G1, BitPieceType.Empty, Square.NoSquare, BitPieceType.Empty, castling, movingColor, value);
                }
                else
                {
                    return new BitMove(BitPieceType.King, Square.E1, Square.C1, BitPieceType.Empty, Square.NoSquare, BitPieceType.Empty, castling, movingColor, value);
                }
            }
            else
            {
                if (castling == CastlingType.KingSide)
                {
                    return new BitMove(BitPieceType.King, Square.E8, Square.G8, BitPieceType.Empty, Square.NoSquare, BitPieceType.Empty, castling, movingColor, value);
                }
                else
                {
                    return new BitMove(BitPieceType.King, Square.E8, Square.C8, BitPieceType.Empty, Square.NoSquare, BitPieceType.Empty, castling, movingColor, value);
                }
            }
        }

        public BitPieceType MovingPiece { get; }
        public Square FromSquare { get; }
        public Square ToSquare { get; }
        public BitPieceType CapturedPiece { get; }
        public Square CapturedSquare { get; }
        public BitPieceType PromotionPiece { get; }
        public CastlingType Castling { get; }
        public ChessColor MovingColor { get; }
        public byte Value { get; }
    }

    public static class BitMoveExtension
    {
        public static bool IsCaptureMove(this BitMove move)
        {
            return move.CapturedPiece != BitPieceType.Empty;
        }

        public static bool IsPromotionMove(this BitMove move)
        {
            return move.PromotionPiece != BitPieceType.Empty; 
        }

        public static bool IsEnpassantCapture(this BitMove move)
        {
            return move.MovingPiece == BitPieceType.Pawn &&
                move.CapturedPiece != BitPieceType.Empty &&
                move.CapturedSquare != move.ToSquare;
        }

        public static string ToPrintString(this BitMove move)
        {
            var builder = new StringBuilder();
            builder.Append(move.MovingPiece != BitPieceType.Pawn ? Bitboards.GetSymbol(move.MovingColor, move.MovingPiece).ToString() : "");
            builder.Append(move.FromSquare);
            builder.Append(move.IsCaptureMove() ? "x" : "-");
            builder.Append(move.ToSquare);
            builder.Append(move.IsPromotionMove() ? Bitboards.GetSymbol(move.MovingColor, move.PromotionPiece).ToString() : "");
            builder.Append(move.IsEnpassantCapture() ? " ep" : "");

            return builder.ToString();
        }
    }
}
