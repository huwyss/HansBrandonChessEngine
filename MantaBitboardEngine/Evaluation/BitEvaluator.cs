﻿using MantaCommon;

namespace MantaBitboardEngine
{
    public class BitEvaluator
    {
        private readonly HelperBitboards _helperBits;

        private readonly int[] _value;
        private readonly int[,,] _positionBonus; // dimension: color, pieceType, square

        public BitEvaluator(HelperBitboards helperBits)
        {
            _helperBits = helperBits;
            _value = new int[(int)BitPieceType.King + 1];
            _value[(int)BitPieceType.Pawn] = 100;
            _value[(int)BitPieceType.Knight] = 300;
            _value[(int)BitPieceType.Bishop] = 300;
            _value[(int)BitPieceType.Rook] = 500;
            _value[(int)BitPieceType.Queen] = 900;
            _value[(int)BitPieceType.King] = 0; // king has only position bonus

            _positionBonus = new int[2, 6, 64]; // pawn, knight, bishop, rook, queen, king 

            Initialize();
        }

        public int Evaluate(Bitboards board)
        {
            var score = 0;
            var whiteBishops = 0;
            var whiteKnights = 0;
            var blackBishops = 0;
            var blackKnights = 0;

            for (int color = (int)ChessColor.White; color <= (int)ChessColor.Black; color++)
            {
                for (int piece = (int)BitPieceType.Pawn; piece <= (int)BitPieceType.King; piece++)
                {
                    var whitePieceBit = board.Bitboard_Pieces[color, piece];
                    while (whitePieceBit != 0)
                    {
                        var square = BitHelper.BitScanForward(whitePieceBit);
                        whitePieceBit &= _helperBits.NotIndexMask[square];

                        if (color == (int)ChessColor.White)
                        {
                            score += _value[piece] + _positionBonus[color, piece, square];
                        }
                        else
                        {
                            score -= _value[piece] + _positionBonus[color, piece, square];
                        }

                        if (piece == (int)BitPieceType.Knight)
                        {
                            if (color == (int)ChessColor.White)
                            {
                                whiteKnights++;
                            }
                            else
                            {
                                blackKnights++;
                            }
                        }
                        else if (piece == (int)BitPieceType.Bishop)
                        {
                            if (color == (int)ChessColor.White)
                            {
                                whiteBishops++;
                            }
                            else
                            {
                                blackBishops++;
                            }
                        }
                    }
                }
            }

            if ((whiteBishops == 2 && whiteKnights == 1 && blackBishops == 1 && blackKnights == 2) ||
                (whiteBishops == 2 && whiteKnights == 0 && blackBishops == 1 && blackKnights == 1) ||
                (whiteBishops == 2 && whiteKnights == 0 && blackBishops == 0 && blackKnights == 2))
            {
                score += 10;
            }
            else if ((whiteBishops == 1 && whiteKnights == 2 && blackBishops == 2 && blackKnights == 1) ||
                    (whiteBishops == 1 && whiteKnights == 1 && blackBishops == 2 && blackKnights == 0) ||
                    (whiteBishops == 0 && whiteKnights == 2 && blackBishops == 2 && blackKnights == 0))
            {
                score -= 10;
            }

            return score;
        }

        private void Initialize()
        {
            var row = 7;
            var col = 0;

            for (var i = 0; i < 64; i++)
            {
                _positionBonus[(int)ChessColor.White, (int)BitPieceType.Pawn, i] = PawnPositionBonus[i];
                _positionBonus[(int)ChessColor.Black, (int)BitPieceType.Pawn, i] = PawnPositionBonus[i];

                _positionBonus[(int)ChessColor.White, (int)BitPieceType.Knight, i] = KnightPositionBonus[i];
                _positionBonus[(int)ChessColor.Black, (int)BitPieceType.Knight, i] = KnightPositionBonus[i];

                _positionBonus[(int)ChessColor.White, (int)BitPieceType.Bishop, i] = BishopPositionBonus[i];
                _positionBonus[(int)ChessColor.Black, (int)BitPieceType.Bishop, i] = BishopPositionBonus[i];

                _positionBonus[(int)ChessColor.White, (int)BitPieceType.Rook, i] = RookPositionBonus[col + 8 * row];
                _positionBonus[(int)ChessColor.Black, (int)BitPieceType.Rook, i] = RookPositionBonus[i];

                _positionBonus[(int)ChessColor.White, (int)BitPieceType.Queen, i] = QueenPositionBonus[col + 8 * row];
                _positionBonus[(int)ChessColor.Black, (int)BitPieceType.Queen, i] = QueenPositionBonus[i];

                _positionBonus[(int)ChessColor.White, (int)BitPieceType.King, i] = KingPositionBonus[col + 8 * row];
                _positionBonus[(int)ChessColor.Black, (int)BitPieceType.King, i] = KingPositionBonus[i];

                col++;
                if (col >= 8)
                {
                    row--;
                    col = 0;
                }
            }
        }

        private readonly int[] PawnPositionBonus = new int[64] // pawn in center are more valuable (at least in opening)
        {
            0,  0,  0,  0,  0,  0,  0,  0,
            0,  0,  0,  0,  0,  0,  0,  0,
            0,  0,  5, 10, 10,  5,  0,  0,
            0,  0, 10, 15, 15, 10,  0,  0,
            0,  0, 10, 15, 15, 10,  0,  0,
            0,  0,  5, 10, 10,  5,  0,  0,
            0,  0,  0,  0,  0,  0,  0,  0,
            0,  0,  0,  0,  0,  0,  0,  0,

        //// 0,  0,  0,  0,  0,  0,  0,  0,   // h
        ////24, 28, 32, 35, 35, 32, 28, 24,   // g
        //// 5, 10, 16, 25, 25, 16, 10,  5,   // f
        //// 4,  8, 14, 19, 19, 14,  8,  4,   // e
        //// 3,  5,  6, 12, 12,  6,  5,  3,   // d
        //// 2,  4,  3,  7,  7,  3,  4,  2,   // c
        //// 1,  2,  3, -8, -9,  3,  2,  1,   // b
        //// 0,  0,  0,  0,  0,  0,  0,  0    // a
        };

        private readonly int[] KnightPositionBonus = new int[64] // Der Springer am Rande ist eine Schande
        {
            -10, -5, -5, -5, -5, -5, -5, -10,
             -5,  0,  0,  0,  0,  0,  0,  -5,
             -5,  0,  5,  5,  5,  5,  0,  -5,
             -5,  0,  5,  5,  5,  5,  0,  -5,
             -5,  0,  5,  5,  5,  5,  0,  -5,
             -5,  0,  5,  5,  5,  5,  0,  -5,
             -5,  0,  0,  0,  0,  0,  0,  -5,
            -10, -5, -5, -5, -5, -5, -5, -10
        };

        private readonly int[] BishopPositionBonus = new int[64]
        {
           -8, -8, -6, -6, -6, -6, -8, -8,
            0,  6,  5,  5,  5,  5,  6,  0,
            0,  4,  5,  6,  6,  5,  4,  0,
            4,  6,  8,  8,  8,  8,  6,  4,
            4,  6,  8,  8,  8,  8,  6,  4,
            0,  4,  5,  6,  6,  5,  4,  0,
            0,  6,  5,  5,  5,  5,  6,  0,
           -8, -8, -6, -6, -6, -6, -8, -8
        };

        private readonly int[] RookPositionBonus = new int[64]
        {
            0,  1,  2,  4,  4,  2,  1,  0,  // h
            3,  5,  7,  7,  7,  7,  5,  3,  // g
            3,  4,  5,  6,  6,  5,  4,  3,  // f
           -2,  0,  4,  5,  5,  4,  0, -2,  // e
           -4, -2,  3,  4,  4,  3, -2, -4,  // d
           -6, -2,  3,  4,  4,  3, -2, -6,  // c
           -6, -2,  4,  4,  4,  4, -2, -6,  // b
           -4, -2,  2,  4,  4,  2, -2, -4   // a
        };

        private readonly int[] QueenPositionBonus = new int[64]
        {
            0,  0,  0,  0,  0,  0,  0,  0,  // h
            0,  2,  3,  4,  4,  3,  2,  0,  // g
            0,  2,  4,  5,  5,  3,  2,  0,  // f
            0,  2,  4,  8,  8,  4,  2,  0,  // e
           -1,  0,  3,  6,  6,  3,  0, -1,  // d
           -1,  0,  2,  3,  3,  2,  0, -1,  // c
           -2, -1,  0,  0,  0,  0, -1, -2,  // b
           -3, -2, -1,  0,  0, -1, -2, -3   // a
        };

        private readonly int[] KingPositionBonus = new int[64]
        {
            0,  0,  0,  0,  0,  0,  0,  0,  // h
            0,  0,  0,  0,  0,  0,  0,  0,  // g
            0,  0,  0,  0,  0,  0,  0,  0,  // f
            0,  0,  0,  0,  0,  0,  0,  0,  // e
            0,  0,  0,  0,  0,  0,  0,  0,  // d
            0,  0,  0,  0,  0,  0,  0,  0,  // c
            0,  0,  0,  0,  0,  0,  0,  0,  // b
            0,  0,  5,  0,  0,  0,  5,  0   // a
        };
    }
}