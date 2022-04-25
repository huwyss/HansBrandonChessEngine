using MantaChessEngine;

namespace MantaChessEngineTest
{
    public class MoveMaker
    {
        public static IMove White(int file)
        {
            return new NormalMove(Piece.MakePiece('Q'), file, 0, 0, 0, null);
        }

        public static IMove White(int file, int rank)
        {
            return new NormalMove(Piece.MakePiece('Q'), file, rank, 0, 0, null);
        }

        public static IMove Black(int file)
        {
            return new NormalMove(Piece.MakePiece('q'), file, 0, 0, 0, null);
        }

        public static IMove Black(int file, int rank)
        {
            return new NormalMove(Piece.MakePiece('q'), file, rank, 0, 0, null);
        }

        public static IMove WhiteCapture(int file, int rank)
        {
            return new NormalMove(Piece.MakePiece('Q'), file, rank, 0, 0, Piece.MakePiece('q'));
        }

        public static IMove BlackCapture(int file, int rank)
        {
            return new NormalMove(Piece.MakePiece('q'), file, rank, 0, 0, Piece.MakePiece('Q'));
        }
    }
}
