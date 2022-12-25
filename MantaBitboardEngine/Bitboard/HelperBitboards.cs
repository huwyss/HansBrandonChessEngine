using System;
using MantaCommon;
using Bitboard = System.UInt64;

namespace MantaBitboardEngine
{
    public class HelperBitboards
    {
        public Bitboard[,] BetweenMatrix { get; set; }

        public Bitboard[,] RayAfterMatrix { get; set; }

        public Bitboard QueenSideMask { get; set; }

        public Bitboard KingSideMask { get; set; }

        /// <summary>
        /// ToSquare of pawn attacking left. Dimensions: ChessColor, Square.
        /// </summary>
        public Square[,] PawnLeft { get; set; }

        /// <summary>
        /// ToSquare of pawn attacking right. Dimensions: ChessColor, Square.
        /// </summary>
        public Square[,] PawnRight { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Square[,] PawnStep { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Square[,] PawnDoubleStep { get; set; }

        /// <summary>
        /// Bitboard of all capturing pawn move of a color from square. Dimensions: ChessColor, Square.
        /// </summary>
        public Bitboard[,] PawnCaptures { get; set; }

        /// <summary>
        /// Bitboard of all defending pawn move of a color from square. Dimensions: ChessColor, Square.
        /// </summary>
        public Bitboard[,] PawnDefends { get; set; }

        /// <summary>
        /// Bitboard of all pawn move of a color from square. Dimensions: ChessColor, Square.
        /// </summary>
        public Bitboard[,] PawnMoves { get; set; }

        /// <summary>
        /// Bitboard of all moves of piece type from square. Dimensions: BitPieceType, Square (piece: only knight, bishop, rook, queen, king, not for pawn)
        /// </summary>
        public Bitboard[,] MovesPieces { get; set; }

        public int[] Row { get; set; }
        public int[] Col { get; set; }
        public int[] DiagNO { get; set; }
        public int[] DiagNW { get; set; }

        public int[,] Rank { get; set; }

        public Bitboard[] IndexMask { get; set; }
        public Bitboard[] NotIndexMask { get; set; }
        public Bitboard[] MaskIsolatedPawns { get; set; }
        public Bitboard[] MaskWhitePassedPawns { get; set; }
        public Bitboard[] MaskWhitePawnsPath { get; set; }
        public Bitboard[] MaskBlackPassedPawns { get; set; }
        public Bitboard[] MaskBlackPawnsPath { get; set; }

        public Bitboard[] MaskCols { get; set; }
        public Bitboard Not_A_file { get; set; }
        public Bitboard Not_H_file { get; set; }

        public HelperBitboards()
        {
            MovesPieces = new Bitboard[6, 64];
            Initialize();
        }

        private void Initialize()
        {
            InitBitboardRowColDiag();
            SetBetweenMatrix();
            SetRayAfterMatrix();
            SetKingAndQueenSideMask();
            SetKnightMoves();
            SetKingMoves();
            SetQueenRookBishopMoves();
            SetIndexMask();
            SetPawnBitboards();
            SetRanks();
            SetMaskCols();
        }

        private void InitBitboardRowColDiag()
        {

            Row = new int[64]
            {
                0, 0, 0, 0, 0, 0, 0, 0,
                1, 1, 1, 1, 1, 1, 1, 1,
                2, 2, 2, 2, 2, 2, 2, 2,
                3, 3, 3, 3, 3, 3, 3, 3,
                4, 4, 4, 4, 4, 4, 4, 4,
                5, 5, 5, 5, 5, 5, 5, 5,
                6, 6, 6, 6, 6, 6, 6, 6,
                7, 7, 7, 7, 7, 7, 7, 7,
            };

            Col = new int[64]
            {
                 0, 1, 2, 3, 4, 5, 6, 7,
                 0, 1, 2, 3, 4, 5, 6, 7,
                 0, 1, 2, 3, 4, 5, 6, 7,
                 0, 1, 2, 3, 4, 5, 6, 7,
                 0, 1, 2, 3, 4, 5, 6, 7,
                 0, 1, 2, 3, 4, 5, 6, 7,
                 0, 1, 2, 3, 4, 5, 6, 7,
                 0, 1, 2, 3, 4, 5, 6, 7,
            };

            DiagNO = new int[64]
            {
                7, 8, 9,10,11,12,13,14,
                6, 7, 8, 9,10,11,12,13,
                5, 6, 7, 8, 9,10,11,12,
                4, 5, 6, 7, 8, 9,10,11,
                3, 4, 5, 6, 7, 8, 9,10,
                2, 3, 4, 5, 6, 7, 8, 9,
                1, 2, 3, 4, 5, 6, 7, 8,
                0, 1, 2, 3, 4, 5, 6, 7,
            };

            DiagNW = new int[64]
            {
               14,13,12,11,10, 9, 8, 7,
               13,12,11,10, 9, 8, 7, 6,
               12,11,10, 9, 8, 7, 6, 5,
               11,10, 9, 8, 7, 6, 5, 4,
               10, 9, 8, 7, 6, 5, 4, 3,
                9, 8, 7, 6, 5, 4, 3, 2,
                8, 7, 6, 5, 4, 3, 2, 1,
                7, 6, 5, 4, 3, 2, 1, 0,
            };
        }

        private void SetBetweenMatrix()
        {
            BetweenMatrix = new Bitboard[64, 64];

            for (int y = 0; y < 64; y++)
            {
                for (int x = 0; x < 64; x++)
                {
                    if (Col[x] == Col[y])
                    {
                        if (y > x)
                        {
                            for (int i = x + 8; i < y; i += 8)
                            {
                                BitHelper.SetBit(ref BetweenMatrix[x, y], i);
                            }
                        }
                        else
                        {
                            for (int i = x - 8; i > y; i -= 8)
                            {
                                BitHelper.SetBit(ref BetweenMatrix[x, y], i);
                            }
                        }
                    }
                    else if (Row[x] == Row[y])
                    {
                        if (y > x)
                        {
                            for (int i = x + 1; i < y; i++)
                            {
                                BitHelper.SetBit(ref BetweenMatrix[x, y], i);
                            }
                        }
                        else
                        {
                            for (int i = x - 1; i > y; i--)
                            {
                                BitHelper.SetBit(ref BetweenMatrix[x, y], i);
                            }
                        }
                    }
                    else if (DiagNW[x] == DiagNW[y])
                    {
                        if (y > x)
                        {
                            for (int i = x + 7; i < y; i += 7)
                            {
                                BitHelper.SetBit(ref BetweenMatrix[x, y], i);
                            }
                        }
                        else
                        {
                            for (int i = x - 7; i > y; i -= 7)
                            {
                                BitHelper.SetBit(ref BetweenMatrix[x, y], i);
                            }
                        }
                    }
                    else if (DiagNO[x] == DiagNO[y])
                    {
                        if (y > x)
                        {
                            for (int i = x + 9; i < y; i += 9)
                            {
                                BitHelper.SetBit(ref BetweenMatrix[x, y], i);
                            }
                        }
                        else
                        {
                            for (int i = x - 9; i > y; i -= 9)
                            {
                                BitHelper.SetBit(ref BetweenMatrix[x, y], i);
                            }
                        }
                    }
                }
            }
        }

        private void SetRayAfterMatrix()
        {
            RayAfterMatrix = new Bitboard[64, 64];

            for (int y = 0; y < 64; y++)
            {
                for (int x = 0; x < 64; x++)
                {
                    if (Col[x] == Col[y])
                    {
                        if (y > x)
                        {
                            var edge = GetEdge(y, 8);
                            for (int i = y; i <= edge; i += 8)
                            {
                                BitHelper.SetBit(ref RayAfterMatrix[x, y], i);
                            }
                        }
                        else
                        {
                            var edge = GetEdge(y, -8);
                            for (int i = y; i >= edge; i -= 8)
                            {
                                BitHelper.SetBit(ref RayAfterMatrix[x, y], i);
                            }
                        }
                    }
                    else if (Row[x] == Row[y])
                    {
                        if (y > x)
                        {
                            var edge = GetEdge(y, 1);
                            for (int i = y; i <= edge; i++)
                            {
                                BitHelper.SetBit(ref RayAfterMatrix[x, y], i);
                            }
                        }
                        else
                        {
                            var edge = GetEdge(y, -1);
                            for (int i = y; i >= edge; i--)
                            {
                                BitHelper.SetBit(ref RayAfterMatrix[x, y], i);
                            }
                        }
                    }
                    else if (DiagNW[x] == DiagNW[y])
                    {
                        if (y > x)
                        {
                            var edge = GetEdge(y, 7);
                            for (int i = y; i <= edge; i += 7)
                            {
                                BitHelper.SetBit(ref RayAfterMatrix[x, y], i);
                            }
                        }
                        else
                        {
                            var edge = GetEdge(y, -7);
                            for (int i = y; i >= edge; i -= 7)
                            {
                                BitHelper.SetBit(ref RayAfterMatrix[x, y], i);
                            }
                        }
                    }
                    else if (DiagNO[x] == DiagNO[y])
                    {
                        if (y > x)
                        {
                            var edge = GetEdge(y, 9);
                            for (int i = y; i <= edge; i += 9)
                            {
                                BitHelper.SetBit(ref RayAfterMatrix[x, y], i);
                            }
                        }
                        else
                        {
                            var edge = GetEdge(y, -9);
                            for (int i = y; i >= edge; i -= 9)
                            {
                                BitHelper.SetBit(ref RayAfterMatrix[x, y], i);
                            }
                        }
                    }
                }
            }
        }

        public int GetEdge(int square, int direction)
        {
            while (square >= 0 &&
                (
                direction == 1 && Col[square] < 7 ||
                direction == -1 && Col[square] > 0 ||
                direction == 8 && Row[square] < 7 ||
                direction == -8 && Row[square] > 0 ||

                direction == 7 && Col[square] > 0 && Row[square] < 7 ||
                direction == 9 && Col[square] < 7 && Row[square] < 7 ||
                direction == -7 && Col[square] < 7 && Row[square] > 0 ||
                direction == -9 && Col[square] > 0 && Row[square] > 0
                ))
            {
                square += direction;
            }

            return square;
        }

        private void SetKingAndQueenSideMask()
        {
            Bitboard queenSideMask = 0;
            Bitboard kingSideMask = 0;
            for (int i = 0; i < 64; i++)
            {
                if (Col[i] < 2)
                {
                    BitHelper.SetBit(ref queenSideMask, i);
                }

                if (Col[i] > 5)
                {
                    BitHelper.SetBit(ref kingSideMask, i);
                }
            }

            QueenSideMask = queenSideMask;
            KingSideMask = kingSideMask;
        }

        private void SetKnightMoves()
        {
            //      -17   -15
            //    -10        -6
            //          N
            //    +6         +10
            //      +15   +17

            for (int i = 0; i < 64; i++)
            {
                if (Col[i] < 6 && Row[i] < 7)
                {
                    BitHelper.SetBit(ref MovesPieces[(int)BitPieceType.Knight, i], i + 10);
                }

                if (Col[i] < 7 && Row[i] < 6)
                {
                    BitHelper.SetBit(ref MovesPieces[(int)BitPieceType.Knight, i], i + 17);
                }

                if (Col[i] > 0 && Row[i] < 6)
                {
                    BitHelper.SetBit(ref MovesPieces[(int)BitPieceType.Knight, i], i + 15);
                }

                if (Col[i] > 1 && Row[i] < 7)
                {
                    BitHelper.SetBit(ref MovesPieces[(int)BitPieceType.Knight, i], i + 6);
                }

                if (Col[i] > 0 && Row[i] > 1)
                {
                    BitHelper.SetBit(ref MovesPieces[(int)BitPieceType.Knight, i], i - 17);
                }

                if (Col[i] > 1 && Row[i] > 0)
                {
                    BitHelper.SetBit(ref MovesPieces[(int)BitPieceType.Knight, i], i - 10);
                }

                if (Col[i] < 7 && Row[i] > 1)
                {
                    BitHelper.SetBit(ref MovesPieces[(int)BitPieceType.Knight, i], i - 15);
                }

                if (Col[i] < 6 && Row[i] > 0)
                {
                    BitHelper.SetBit(ref MovesPieces[(int)BitPieceType.Knight, i], i - 6);
                }
            }
        }

        private void SetKingMoves()
        {
            //       -9  -8  -7
            //       -1   K  +1
            //       +7  +8  +9 

            for (int i = 0; i < 64; i++)
            {
                if (Col[i] > 0 && Row[i] > 0)
                {
                    BitHelper.SetBit(ref MovesPieces[(int)BitPieceType.King, i], i - 1);
                    BitHelper.SetBit(ref MovesPieces[(int)BitPieceType.King, i], i - 8);
                    BitHelper.SetBit(ref MovesPieces[(int)BitPieceType.King, i], i - 9);
                }

                if (Col[i] < 7 && Row[i] > 0)
                {
                    BitHelper.SetBit(ref MovesPieces[(int)BitPieceType.King, i], i - 7);
                    BitHelper.SetBit(ref MovesPieces[(int)BitPieceType.King, i], i - 8);
                    BitHelper.SetBit(ref MovesPieces[(int)BitPieceType.King, i], i + 1);
                }

                if (Col[i] > 0 && Row[i] < 7)
                {
                    BitHelper.SetBit(ref MovesPieces[(int)BitPieceType.King, i], i - 1);
                    BitHelper.SetBit(ref MovesPieces[(int)BitPieceType.King, i], i + 7);
                    BitHelper.SetBit(ref MovesPieces[(int)BitPieceType.King, i], i + 8);
                }

                if (Col[i] < 7 && Row[i] < 7)
                {
                    BitHelper.SetBit(ref MovesPieces[(int)BitPieceType.King, i], i + 1);
                    BitHelper.SetBit(ref MovesPieces[(int)BitPieceType.King, i], i + 8);
                    BitHelper.SetBit(ref MovesPieces[(int)BitPieceType.King, i], i + 9);
                }
            }
        }

        private void SetQueenRookBishopMoves()
        {
            //       -9  -8  -7
            //       -1   K  +1
            //       +7  +8  +9 

            for (int i = 0; i < 64; i++)
            {
                if (Row[i] < 7)
                {
                    var edge = GetEdge(i, 8);
                    for (int z = i + 8; z <= edge; z += 8)
                    {
                        BitHelper.SetBit(ref MovesPieces[(int)BitPieceType.Rook, i], z);
                        BitHelper.SetBit(ref MovesPieces[(int)BitPieceType.Queen, i], z);
                    }
                }

                if (Row[i] > 0)
                {
                    var edge = GetEdge(i, -8);
                    for (int z = i - 8; z >= edge; z -= 8)
                    {
                        BitHelper.SetBit(ref MovesPieces[(int)BitPieceType.Rook, i], z);
                        BitHelper.SetBit(ref MovesPieces[(int)BitPieceType.Queen, i], z);
                    }
                }

                if (Col[i] < 7)
                {
                    var edge = GetEdge(i, 1);
                    for (int z = i + 1; z <= edge; z++)
                    {
                        BitHelper.SetBit(ref MovesPieces[(int)BitPieceType.Rook, i], z);
                        BitHelper.SetBit(ref MovesPieces[(int)BitPieceType.Queen, i], z);
                    }
                }

                if (Col[i] > 0)
                {
                    var edge = GetEdge(i, -1);
                    for (int z = i - 1; z >= edge; z--)
                    {
                        BitHelper.SetBit(ref MovesPieces[(int)BitPieceType.Rook, i], z);
                        BitHelper.SetBit(ref MovesPieces[(int)BitPieceType.Queen, i], z);
                    }
                }

                if (Col[i] > 0 && Row[i] < 7)
                {
                    var edge = GetEdge(i, 7);
                    for (int z = i + 7; z <= edge; z += 7)
                    {
                        BitHelper.SetBit(ref MovesPieces[(int)BitPieceType.Bishop, i], z);
                        BitHelper.SetBit(ref MovesPieces[(int)BitPieceType.Queen, i], z);
                    }
                }

                if (Col[i] < 7 && Row[i] < 7)
                {
                    var edge = GetEdge(i, 9);
                    for (int z = i + 9; z <= edge; z += 9)
                    {
                        BitHelper.SetBit(ref MovesPieces[(int)BitPieceType.Bishop, i], z);
                        BitHelper.SetBit(ref MovesPieces[(int)BitPieceType.Queen, i], z);
                    }
                }

                if (Col[i] < 7 && Row[i] > 0)
                {
                    var edge = GetEdge(i, -7);
                    for (int z = i - 7; z >= edge; z -= 7)
                    {
                        BitHelper.SetBit(ref MovesPieces[(int)BitPieceType.Bishop, i], z);
                        BitHelper.SetBit(ref MovesPieces[(int)BitPieceType.Queen, i], z);
                    }
                }

                if (Col[i] > 0 && Row[i] > 0)
                {
                    var edge = GetEdge(i, -9);
                    for (int z = i - 9; z >= edge; z -= 9)
                    {
                        BitHelper.SetBit(ref MovesPieces[(int)BitPieceType.Bishop, i], z);
                        BitHelper.SetBit(ref MovesPieces[(int)BitPieceType.Queen, i], z);
                    }
                }
            }
        }

        private void SetIndexMask()
        {
            IndexMask = new Bitboard[64];
            NotIndexMask = new Bitboard[64];

            for (int i = 0; i < 64; i++)
            {
                BitHelper.SetBit(ref IndexMask[i], i);
                NotIndexMask[i] = ~IndexMask[i];
            }
        }

        private void SetPawnBitboards()
        {
            SetPawnCaptures();
            SetPawnStraightMoves();
            SetPawnMasks();
        }

        private void SetPawnCaptures()
        {
            PawnCaptures = new Bitboard[2, 64];
            PawnLeft = new Square[2, 64];
            PawnRight = new Square[2, 64];
            PawnDefends = new Bitboard[2, 64];

            // clear PawnLeft and PawnRight
            for (int i = 0; i < 64; i++)
            {
                PawnLeft[(int)ChessColor.White, i] = Square.NoSquare;
                PawnLeft[(int)ChessColor.Black, i] = Square.NoSquare;
                PawnRight[(int)ChessColor.White, i] = Square.NoSquare;
                PawnRight[(int)ChessColor.Black, i] = Square.NoSquare;
            }

            for (int i = 0; i < 64; i++)
            {
                if (Col[i] > 0)
                {
                    if (Row[i] < 7)
                    {
                        // white captures left
                        var stepLeftWhite = i + 7;
                        BitHelper.SetBit(ref PawnCaptures[(int)ChessColor.White, i], stepLeftWhite);
                        PawnLeft[(int)ChessColor.White, i] = (Square)stepLeftWhite;
                    }

                    if (Row[i] > 0)
                    {
                        // black captures left
                        var stepLeftBlack = i - 9;
                        BitHelper.SetBit(ref PawnCaptures[(int)ChessColor.Black, i], stepLeftBlack);
                        PawnLeft[(int)ChessColor.Black, i] = (Square)stepLeftBlack;
                    }
                }

                if (Col[i] < 7)
                {
                    if (Row[i] < 7)
                    {
                        // white captures right
                        var stepRightWhite = i + 9;
                        BitHelper.SetBit(ref PawnCaptures[(int)ChessColor.White, i], stepRightWhite);
                        PawnRight[(int)ChessColor.White, i] = (Square)stepRightWhite;
                    }

                    if (Row[i] > 0)
                    {
                        // black captures right
                        var stepRightBlack = i - 7;
                        BitHelper.SetBit(ref PawnCaptures[(int)ChessColor.Black, i], stepRightBlack);
                        PawnRight[(int)ChessColor.Black, i] = (Square)stepRightBlack;
                    }
                }

                PawnDefends[(int)ChessColor.White, i] = PawnCaptures[(int)ChessColor.Black, i];
                PawnDefends[(int)ChessColor.Black, i] = PawnCaptures[(int)ChessColor.White, i];
            }
        }

        private void SetPawnStraightMoves()
        {
            PawnMoves = new Bitboard[2, 64];
            PawnStep = new Square[2, 64];
            PawnDoubleStep = new Square[2, 64];

            for (int i = 0; i < 64; i++)
            {
                if (Row[i] < 7)
                {
                    // step white
                    BitHelper.SetBit(ref PawnMoves[(int)ChessColor.White, i], i + 8);
                    PawnStep[(int)ChessColor.White, i] = (Square)(i + 8);
                }

                if (Row[i] == 1)
                {
                    // double step white
                    BitHelper.SetBit(ref PawnMoves[(int)ChessColor.White, i], i + 16);
                    PawnDoubleStep[(int)ChessColor.White, i] = (Square)(i + 16);
                }

                if (Row[i] > 0)
                {
                    // step black
                    BitHelper.SetBit(ref PawnMoves[(int)ChessColor.Black, i], i - 8);
                    PawnStep[(int)ChessColor.Black, i] = (Square)(i - 8);
                }

                if (Row[i] == 6)
                {
                    // step black
                    BitHelper.SetBit(ref PawnMoves[(int)ChessColor.Black, i], i - 16);
                    PawnDoubleStep[(int)ChessColor.Black, i] = (Square)(i - 16);
                }
            }
        }

        private void SetPawnMasks()
        {
            MaskIsolatedPawns = new Bitboard[64];

            MaskWhitePassedPawns = new Bitboard[64];
            MaskWhitePawnsPath = new Bitboard[64];
            MaskBlackPassedPawns = new Bitboard[64];
            MaskBlackPawnsPath = new Bitboard[64];

            for (int x = 0; x < 64; x++)
            {
                for (int y = 0; y < 64; y++)
                {
                    if (Math.Abs(Col[x] - Col[y]) < 2)
                    {
                        if (Row[x] < Row[y] && Row[y] < 7)
                        {
                            BitHelper.SetBit(ref MaskWhitePassedPawns[x], y);
                        }

                        if (Row[x] > Row[y] && Row[y] > 0)
                        {
                            BitHelper.SetBit(ref MaskBlackPassedPawns[x], y);
                        }
                    }

                    if (Math.Abs(Col[x] - Col[y]) == 1)
                    {
                        BitHelper.SetBit(ref MaskIsolatedPawns[x], y);
                        BitHelper.SetBit(ref MaskIsolatedPawns[x], y);
                    }

                    if (Col[x] == Col[y])
                    {
                        if (Row[x] < Row[y])
                        {
                            BitHelper.SetBit(ref MaskWhitePawnsPath[x], y);
                        }

                        if (Row[x] > Row[y])
                        {
                            BitHelper.SetBit(ref MaskBlackPawnsPath[x], y);
                        }
                    }
                }
            }
        }

        private void SetRanks()
        {
            Rank = new int[2, 64];
            for (int i = 0; i < 64; i++)
            {
                Rank[0, i] = Row[i];
                Rank[1, i] = 7 - Row[i];

            }
        }

        private void SetMaskCols()
        {
            MaskCols = new Bitboard[64];

            for (int x = 0; x < 64; x++)
            {
                for (int y = 0; y < 64; y++)
                {
                    if (Col[x] == Col[y])
                    {
                        BitHelper.SetBit(ref MaskCols[x], y);
                    }
                }
            }

            Not_A_file = ~MaskCols[0];
            Not_H_file = ~MaskCols[7];
        }
    }
}
