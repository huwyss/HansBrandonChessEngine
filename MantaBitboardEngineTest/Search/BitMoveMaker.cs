using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MantaBitboardEngine;
using MantaCommon;

namespace MantaBitboardEngineTest
{

    public class BitMoveMaker
    {
        public static BitMove White(int file)
        {
            return BitMove.CreateMove(PieceType.Queen, (Square)file - 1, Square.A1, PieceType.Empty, ChessColor.White, 0);
        }

        public static BitMove White(int file, int rank)
        {
            return BitMove.CreateMove(PieceType.Queen, (Square)file - 1, (Square)rank - 1, PieceType.Empty, ChessColor.White, 0);
        }

        public static BitMove Black(int file)
        {
            return BitMove.CreateMove(PieceType.Queen, (Square)file - 1, Square.A1, PieceType.Empty, ChessColor.Black, 0);
        }

        public static BitMove Black(int file, int rank)
        {
            return BitMove.CreateMove(PieceType.Queen, (Square)file - 1, (Square)rank - 1, PieceType.Empty, ChessColor.Black, 0);
        }

        public static BitMove WhiteCapture(int file, int rank)
        {
            return BitMove.CreateCapture(PieceType.Queen, (Square)file - 1, (Square)rank - 1, PieceType.Pawn, Square.A1, PieceType.Empty, ChessColor.White, 0);
        }

        public static BitMove BlackCapture(int file, int rank)
        {
            return BitMove.CreateCapture(PieceType.Queen, (Square)file - 1, (Square)rank - 1, PieceType.Pawn, Square.A1, PieceType.Empty, ChessColor.Black, 0);
        }
    }
}


