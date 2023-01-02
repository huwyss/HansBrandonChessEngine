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
            return BitMove.CreateMove(BitPieceType.Queen, (Square)file - 1, Square.A1, BitPieceType.Empty, ChessColor.White, 0);
        }

        public static BitMove White(int file, int rank)
        {
            return BitMove.CreateMove(BitPieceType.Queen, (Square)file - 1, (Square)rank - 1, BitPieceType.Empty, ChessColor.White, 0);
        }

        public static BitMove Black(int file)
        {
            return BitMove.CreateMove(BitPieceType.Queen, (Square)file - 1, Square.A1, BitPieceType.Empty, ChessColor.Black, 0);
        }

        public static BitMove Black(int file, int rank)
        {
            return BitMove.CreateMove(BitPieceType.Queen, (Square)file - 1, (Square)rank - 1, BitPieceType.Empty, ChessColor.Black, 0);
        }

        public static BitMove WhiteCapture(int file, int rank)
        {
            return BitMove.CreateCapture(BitPieceType.Queen, (Square)file - 1, (Square)rank - 1, BitPieceType.Pawn, Square.A1, BitPieceType.Empty, ChessColor.White, 0);
        }

        public static BitMove BlackCapture(int file, int rank)
        {
            return BitMove.CreateCapture(BitPieceType.Queen, (Square)file - 1, (Square)rank - 1, BitPieceType.Pawn, Square.A1, BitPieceType.Empty, ChessColor.Black, 0);
        }
    }
}


