﻿namespace MantaChessEngine.BitboardEngine
{
    public struct BitMove
    {
        public BitMove(
            BitPieceType movingPiece,
            Square fromSquare,
            Square toSquare,
            BitPieceType capturedPiece,
            Square capturedSquare,
            BitPieceType promotionPiece,
            byte value)
        {
            FromSquare = fromSquare;
            ToSquare = toSquare;
            MovingPiece = movingPiece;
            CapturedPiece = capturedPiece;
            CapturedSquare = capturedSquare;
            PromotionPiece = promotionPiece;
            Value = value;
        }

        public BitMove(
            BitPieceType movingPiece,
            Square fromSquare,
            Square toSquare,
            byte value)
            : this (movingPiece, fromSquare, toSquare, BitPieceType.Empty, Square.NoSquare, BitPieceType.Empty, value)
        {           
        }

        public BitPieceType MovingPiece { get; }
        public Square FromSquare { get; }
        public Square ToSquare { get; }
        public BitPieceType CapturedPiece { get; }
        public Square CapturedSquare { get; }
        public BitPieceType PromotionPiece { get; }
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
    }
}
