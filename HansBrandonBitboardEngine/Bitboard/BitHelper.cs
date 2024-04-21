using System;
using HBCommon;

namespace HansBrandonBitboardEngine
{
    public class BitHelper
    {
        public static void SetBit(ref UInt64 bitboard, int index)
        {
            bitboard |= ((UInt64)0x01 << index);
        }

        public static void ClearBit(ref UInt64 bitboard, int index)
        {
            bitboard &= (~((UInt64)0x01 << index));
        }

        public static bool GetBit(UInt64 bitboard, int index)
        {
            return (bitboard & ((UInt64)0x01 << index)) != (UInt64)0x00;
        }

        public static UInt64 ConvertToUInt64(byte[] input)
        {
            UInt64 result = 0;
            for (int i = 0; i < 64; i++)
            {
                if (input[i] != 0)
                {
                    SetBit(ref result, i);
                }
            }

            return result;
        }

        private const ulong DeBruijnSequence = 0x37E84A99DAE458F;

        private static readonly int[] MultiplyDeBruijnBitPosition =
        {
            0, 1, 17, 2, 18, 50, 3, 57,
            47, 19, 22, 51, 29, 4, 33, 58,
            15, 48, 20, 27, 25, 23, 52, 41,
            54, 30, 38, 5, 43, 34, 59, 8,
            63, 16, 49, 56, 46, 21, 28, 32,
            14, 26, 24, 40, 53, 37, 42, 7,
            62, 55, 45, 31, 13, 39, 36, 6,
            61, 44, 12, 35, 60, 11, 10, 9,
        };

        /// <summary>
        /// Search the mask data from least significant bit (LSB) to the most significant bit (MSB) for a set bit (1)
        /// using De Bruijn sequence approach. Warning: Will return zero for b = 0.
        /// </summary>
        /// <param name="b">Target number.</param>
        /// <returns>Zero-based position of LSB (from right to left).</returns>
        public static int BitScanForward(ulong b)
        {
            return MultiplyDeBruijnBitPosition[((ulong)((long)b & -(long)b) * DeBruijnSequence) >> 58];
        }

        public static string GetSymbol(ChessColor color, PieceType piece)
        {
            if (color == ChessColor.White && piece == PieceType.Pawn)
            {
                return "P";
            }
            else if (color == ChessColor.White && piece == PieceType.Knight)
            {
                return "N";
            }
            else if (color == ChessColor.White && piece == PieceType.Bishop)
            {
                return "B";
            }
            else if (color == ChessColor.White && piece == PieceType.Rook)
            {
                return "R";
            }
            else if (color == ChessColor.White && piece == PieceType.Queen)
            {
                return "Q";
            }
            else if (color == ChessColor.White && piece == PieceType.King)
            {
                return "K";
            }
            else if (color == ChessColor.Black && piece == PieceType.Pawn)
            {
                return "p";
            }
            else if (color == ChessColor.Black && piece == PieceType.Knight)
            {
                return "n";
            }
            else if (color == ChessColor.Black && piece == PieceType.Bishop)
            {
                return "b";
            }
            else if (color == ChessColor.Black && piece == PieceType.Rook)
            {
                return "r";
            }
            else if (color == ChessColor.Black && piece == PieceType.Queen)
            {
                return "q";
            }
            else if (color == ChessColor.Black && piece == PieceType.King)
            {
                return "k";
            }
            else if (color == ChessColor.Empty && piece == PieceType.Empty)
            {
                return ".";
            }

            return ".";
        }

        public static PieceType GetBitPieceType(char symbol)
        {
            return GetBitPiece(symbol).Piece;
        }

        public static BitPiece GetBitPiece(char symbol)
        {
            BitPiece bitPiece;

            switch (symbol)
            {
                case 'P':
                    bitPiece = new BitPiece(ChessColor.White, PieceType.Pawn);
                    break;
                case 'N':
                    bitPiece = new BitPiece(ChessColor.White, PieceType.Knight);
                    break;
                case 'B':
                    bitPiece = new BitPiece(ChessColor.White, PieceType.Bishop);
                    break;
                case 'R':
                    bitPiece = new BitPiece(ChessColor.White, PieceType.Rook);
                    break;
                case 'Q':
                    bitPiece = new BitPiece(ChessColor.White, PieceType.Queen);
                    break;
                case 'K':
                    bitPiece = new BitPiece(ChessColor.White, PieceType.King);
                    break;
                case 'p':
                    bitPiece = new BitPiece(ChessColor.Black, PieceType.Pawn);
                    break;
                case 'n':
                    bitPiece = new BitPiece(ChessColor.Black, PieceType.Knight);
                    break;
                case 'b':
                    bitPiece = new BitPiece(ChessColor.Black, PieceType.Bishop);
                    break;
                case 'r':
                    bitPiece = new BitPiece(ChessColor.Black, PieceType.Rook);
                    break;
                case 'q':
                    bitPiece = new BitPiece(ChessColor.Black, PieceType.Queen);
                    break;
                case 'k':
                    bitPiece = new BitPiece(ChessColor.Black, PieceType.King);
                    break;
                case '.':
                case ' ':
                    bitPiece = new BitPiece(ChessColor.Empty, PieceType.Empty);
                    break;
                default:
                    throw new HansBrandonEngineException($"Piece character unknown: {symbol}");
            }

            return bitPiece;
        }
    }
}
