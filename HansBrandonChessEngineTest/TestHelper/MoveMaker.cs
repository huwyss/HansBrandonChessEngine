using HansBrandonChessEngine;
using HBCommon;

namespace HansBrandonChessEngineTest
{
    public class MoveMaker
    {
        public static IMove White(int x)
        {
            return White(x, 0);
        }

        public static IMove White(int x, int y)
        {
            return new NormalMove(Piece.MakePiece(PieceType.Queen, ChessColor.White), (Square)x, (Square)y, null);
        }

        public static IMove Black(int x)
        {
            return Black(x, 0);
        }

        public static IMove Black(int x, int y)
        {
            return new NormalMove(Piece.MakePiece(PieceType.Queen, ChessColor.Black), (Square)x, (Square)y, null);
        }

        public static IMove WhiteCapture(int x, int y)
        {
            return new NormalMove(Piece.MakePiece(PieceType.Queen, ChessColor.White), (Square)x, (Square)y, Piece.MakePiece(PieceType.Queen, ChessColor.Black));
        }

        public static IMove BlackCapture(int x, int y)
        {
            return new NormalMove(Piece.MakePiece(PieceType.Queen, ChessColor.Black), (Square)x, (Square)y, Piece.MakePiece(PieceType.Queen, ChessColor.White));
        }
    }
}
