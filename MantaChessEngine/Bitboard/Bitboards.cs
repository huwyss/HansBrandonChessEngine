using System;
using System.Text;
using Bitboard = System.UInt64;

namespace MantaChessEngine
{
    public class Bitboards : IBoard
    {
        private readonly FenParser _fenParser;
        private readonly BitMoveExecutor _moveExecutor;

        // todo these are Pieces[2][7]
        public Bitboard Bitboard_WhitePawn { get; set; }
        public Bitboard Bitboard_WhiteRook { get; set; }
        public Bitboard Bitboard_WhiteKnight { get; set; }
        public Bitboard Bitboard_WhiteBishop { get; set; }
        public Bitboard Bitboard_WhiteQueen { get; set; }
        public Bitboard Bitboard_WhiteKing { get; set; }
        public Bitboard Bitboard_WhiteAllPieces =>
            Bitboard_WhitePawn |
            Bitboard_WhiteRook |
            Bitboard_WhiteKnight |
            Bitboard_WhiteBishop |
            Bitboard_WhiteQueen |
            Bitboard_WhiteKing;

        public Bitboard Bitboard_BlackPawn { get; set; }
        public Bitboard Bitboard_BlackRook { get; set; }
        public Bitboard Bitboard_BlackKnight { get; set; }
        public Bitboard Bitboard_BlackBishop { get; set; }
        public Bitboard Bitboard_BlackQueen { get; set; }
        public Bitboard Bitboard_BlackKing { get; set; }
        public Bitboard Bitboard_BlackAllPieces =>
            Bitboard_BlackPawn |
            Bitboard_BlackRook |
            Bitboard_BlackKnight |
            Bitboard_BlackBishop |
            Bitboard_BlackQueen |
            Bitboard_BlackKing;

        public Bitboard Bitboard_AllPieces => Bitboard_WhiteAllPieces | Bitboard_BlackAllPieces;

        public Bitboard[,] BetweenMatrix { get; set; }
        public Bitboard[,] RayAfterMatrix { get; set; }

        public Bitboard QueenSideMask { get; set; }
        public Bitboard KingSideMask { get; set; }

        public Bitboard[] WhitePawnCaptures { get; set; }
        public Bitboard[] WhitePawnDefends { get; set; }
        public Bitboard[] WhiteMovesPawn { get; set; }
        public Bitboard[] WhitePawnStep { get; set; }
        public Bitboard[] WhitePawnDoubleStep { get; set; }
        public int[] WhitePawnLeft { get; set; } // todo: muss das mit illegal initialisiert werden?
        public int[] WhitePawnRight { get; set; } // todo: muss das mit illegal initialisiert werden?

        public Bitboard[] BlackPawnCaptures { get; set; }
        public Bitboard[] BlackPawnDefends { get; set; }
        public Bitboard[] BlackMovesPawn { get; set; }
        public Bitboard[] BlackPawnStep { get; set; }
        public Bitboard[] BlackPawnDoubleStep { get; set; }
        public int[] BlackPawnLeft { get; set; } // todo: muss das mit illegal initialisiert werden?
        public int[] BlackPawnRight { get; set; } // todo: muss das mit illegal initialisiert werden?

        public Bitboard[] MovesKnight { get; set; }
        public Bitboard[] MovesBishop { get; set; }
        public Bitboard[] MovesRook { get; set; }
        public Bitboard[] MovesQueen { get; set; }
        public Bitboard[] MovesKing { get; set; }

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

        /// <summary>
        /// Side to move.
        /// true = white, false = black.
        /// </summary>
        public bool Side { get; private set; }

        /// <summary>
        /// Other side.
        /// </summary>
        public bool XSide => !Side;

        /// <summary>
        /// Counts plys since last capture or pawn move.
        /// </summary>
        public int FiftyCounter { get; private set; }

        // ---- from IBoard ----

        public int MoveCountSincePawnOrCapture { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
       
        public BoardState BoardState { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
       
        public string GetPositionString => throw new NotImplementedException();

        public string GetPrintString => throw new NotImplementedException();

        public Bitboards()
        {
            _fenParser = new FenParser();
            _moveExecutor = new BitMoveExecutor();
            InitBitboard();
        }

        public void Initialize()
        {
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

        private void InitBitboard()
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
                    if (Col[ x ] == Col[ y ])
                    {
                        if (y > x)
                        {
                            for (int i = x + 8; i < y; i += 8)
                            {
                                SetBit(ref BetweenMatrix[x, y], i);
                            }
                        }
                        else
                        {
                            for (int i = x - 8; i > y; i -= 8)
                            {
                                SetBit(ref BetweenMatrix[x, y], i);
                            }
                        }
                    }
                    else if (Row[ x ] == Row[ y ])
                    {
                        if (y > x)
                        {
                            for (int i = x + 1; i < y; i++)
                            {
                                SetBit(ref BetweenMatrix[x, y], i);
                            }
                        }
                        else
                        {
                            for (int i = x - 1; i > y; i--)
                            {
                                SetBit(ref BetweenMatrix[x, y], i);
                            }
                        }
                    }
                    else if (DiagNW[x] == DiagNW[y])
                    {
                        if (y > x)
                        {
                            for (int i = x + 7; i < y; i += 7)
                            {
                                SetBit(ref BetweenMatrix[x, y], i);
                            }
                        }
                        else
                        {
                            for (int i = x - 7; i > y; i -= 7)
                            {
                                SetBit(ref BetweenMatrix[x, y], i);
                            }
                        }
                    }
                    else if (DiagNO[x] == DiagNO[y])
                    {
                        if (y > x)
                        {
                            for (int i = x + 9; i < y; i += 9)
                            {
                                SetBit(ref BetweenMatrix[x, y], i);
                            }
                        }
                        else
                        {
                            for (int i = x - 9; i > y; i -= 9)
                            {
                                SetBit(ref BetweenMatrix[x, y], i);
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
                                SetBit(ref RayAfterMatrix[x, y], i);
                            }
                        }
                        else
                        {
                            var edge = GetEdge(y, -8);
                            for (int i = y; i >= edge; i -= 8)
                            {
                                SetBit(ref RayAfterMatrix[x, y], i);
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
                                SetBit(ref RayAfterMatrix[x, y], i);
                            }
                        }
                        else
                        {
                            var edge = GetEdge(y, -1);
                            for (int i = y; i >= edge; i--)
                            {
                                SetBit(ref RayAfterMatrix[x, y], i);
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
                                SetBit(ref RayAfterMatrix[x, y], i);
                            }
                        }
                        else
                        {
                            var edge = GetEdge(y, -7);
                            for (int i = y; i >= edge; i -= 7)
                            {
                                SetBit(ref RayAfterMatrix[x, y], i);
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
                                SetBit(ref RayAfterMatrix[x, y], i);
                            }
                        }
                        else
                        {
                            var edge = GetEdge(y, -9);
                            for (int i = y; i >= edge; i -= 9)
                            {
                                SetBit(ref RayAfterMatrix[x, y], i);
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
                    SetBit(ref queenSideMask, i);
                }

                if (Col[i] > 5)
                {
                    SetBit(ref kingSideMask, i);
                }
            }

            QueenSideMask = queenSideMask;
            KingSideMask = kingSideMask;
        }

        private void SetKnightMoves()
        {
            MovesKnight = new Bitboard[64];

            //      -17   -15
            //    -10        -6
            //          N
            //    +6         +10
            //      +15   +17

            for (int i = 0; i < 64; i++)
            {
                if (Col[i] < 7 && Row[i] < 7)
                {
                    SetBit(ref MovesKnight[i], i + 10);
                }

                if (Col[i] < 6 && Row[i] < 6)
                {
                    SetBit(ref MovesKnight[i], i + 17);
                }

                if (Col[i] > 1 && Row[i] < 6)
                {
                    SetBit(ref MovesKnight[i], i + 15);
                }

                if (Col[i] > 0 && Row[i] < 7)
                {
                    SetBit(ref MovesKnight[i], i + 6);
                }

                if (Col[i] > 1 && Row[i] > 1)
                {
                    SetBit(ref MovesKnight[i], i - 17);
                }

                if (Col[i] > 0 && Row[i] > 0)
                {
                    SetBit(ref MovesKnight[i], i - 10);
                }

                if (Col[i] < 6 && Row[i] > 1)
                {
                    SetBit(ref MovesKnight[i], i - 15);
                }

                if (Col[i] < 7 && Row[i] > 0)
                {
                    SetBit(ref MovesKnight[i], i - 6);
                }
            }
        }

        private void SetKingMoves()
        {
            MovesKing = new Bitboard[64];

            //       -9  -8  -7
            //       -1   K  +1
            //       +7  +8  +9 

            for (int i = 0; i < 64; i++)
            {
                if (Col[i] > 1 && Row[i] > 1)
                {
                    SetBit(ref MovesKing[i], i - 1);
                    SetBit(ref MovesKing[i], i - 8);
                    SetBit(ref MovesKing[i], i - 9);
                }

                if (Col[i] < 7 && Row[i] > 1)
                {
                    SetBit(ref MovesKing[i], i - 7);
                    SetBit(ref MovesKing[i], i - 8);
                    SetBit(ref MovesKing[i], i + 1);
                }

                if (Col[i] > 1 && Row[i] < 7)
                {
                    SetBit(ref MovesKing[i], i - 1);
                    SetBit(ref MovesKing[i], i + 7);
                    SetBit(ref MovesKing[i], i + 8);
                }

                if (Col[i] < 7 && Row[i] < 7)
                {
                    SetBit(ref MovesKing[i], i + 1);
                    SetBit(ref MovesKing[i], i + 8);
                    SetBit(ref MovesKing[i], i + 9);
                }
            }
        }

        private void SetQueenRookBishopMoves()
        {
            MovesQueen = new Bitboard[64];
            MovesRook = new Bitboard[64];
            MovesBishop = new Bitboard[64];

            //       -9  -8  -7
            //       -1   K  +1
            //       +7  +8  +9 

            for (int i = 0; i < 64; i++)
            {
                if (Row[i] < 7)
                {
                    var edge = GetEdge(i, 8);
                    for (int z = i+8; z <= edge; z += 8)
                    {
                        SetBit(ref MovesRook[i], z);
                        SetBit(ref MovesQueen[i], z);
                    }
                }

                if (Row[i] > 1)
                {
                    var edge = GetEdge(i, -8);
                    for (int z = i - 8; z >= edge; z -= 8)
                    {
                        SetBit(ref MovesRook[i], z);
                        SetBit(ref MovesQueen[i], z);
                    }
                }

                if (Col[i] < 7)
                {
                    var edge = GetEdge(i, 1);
                    for (int z = i + 1; z <= edge; z++)
                    {
                        SetBit(ref MovesRook[i], z);
                        SetBit(ref MovesQueen[i], z);
                    }
                }

                if (Col[i] > 1)
                {
                    var edge = GetEdge(i, -1);
                    for (int z = i - 1; z >= edge; z--)
                    {
                        SetBit(ref MovesRook[i], z);
                        SetBit(ref MovesQueen[i], z);
                    }
                }

                if (Col[i] > 0 && Row[i] < 7)
                {
                    var edge = GetEdge(i, 7);
                    for (int z=i + 7; z <= edge; z +=7)
                    {
                        SetBit(ref MovesBishop[i], z);
                        SetBit(ref MovesQueen[i], z);
                    }
                }

                if (Col[i] < 7 && Row[i] < 7)
                {
                    var edge = GetEdge(i, 9);
                    for (int z = i + 9; z <= edge; z += 9)
                    {
                        SetBit(ref MovesBishop[i], z);
                        SetBit(ref MovesQueen[i], z);
                    }
                }

                if (Col[i] < 7 && Row[i] > 0)
                {
                    var edge = GetEdge(i, -7);
                    for (int z = i - 7; z >= edge; z -= 7)
                    {
                        SetBit(ref MovesBishop[i], z);
                        SetBit(ref MovesQueen[i], z);
                    }
                }

                if (Col[i] > 0 && Row[i] > 0)
                {
                    var edge = GetEdge(i, -9);
                    for (int z = i - 9; z >= edge; z -= 9)
                    {
                        SetBit(ref MovesBishop[i], z); 
                        SetBit(ref MovesQueen[i], z);
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
                SetBit(ref IndexMask[i], i);
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
            WhitePawnCaptures = new Bitboard[64];
            WhitePawnLeft = new int[64];
            WhitePawnRight = new int[64];
            WhitePawnDefends = new Bitboard[64];

            BlackPawnCaptures = new Bitboard[64];
            BlackPawnLeft = new int[64];
            BlackPawnRight = new int[64];
            BlackPawnDefends = new Bitboard[64];

            for (int i = 0; i < 64; i++)
            {
                if (Col[i] > 0)
                {
                    if (Row[i] < 7)
                    {
                        // white captures left
                        var stepLeftWhite = i + 7;
                        SetBit(ref WhitePawnCaptures[i], stepLeftWhite);
                        WhitePawnLeft[i] = stepLeftWhite;
                    }

                    if (Row[i] > 0)
                    {
                        // black captures left
                        var stepLeftBlack = i - 9;
                        SetBit(ref BlackPawnCaptures[i], stepLeftBlack);
                        BlackPawnLeft[i] = stepLeftBlack;
                    }
                }

                if (Col[i] < 7)
                {
                    if (Row[i] < 7)
                    {
                        // white captures right
                        var stepRightWhite = i + 9;
                        SetBit(ref WhitePawnCaptures[i], stepRightWhite);
                        WhitePawnRight[i] = stepRightWhite;
                    }

                    if (Row[i] > 0)
                    {
                        // black captures right
                        var stepRightBlack = i - 7;
                        SetBit(ref BlackPawnCaptures[i], stepRightBlack);
                        BlackPawnRight[i] = stepRightBlack;
                    }
                }

                WhitePawnDefends[i] = BlackPawnCaptures[i];
                BlackPawnDefends[i] = WhitePawnCaptures[i];
            }
        }

        private void SetPawnStraightMoves()
        {
            WhiteMovesPawn = new Bitboard[64];
            WhitePawnStep = new Bitboard[64];
            WhitePawnDoubleStep = new Bitboard[64];

            BlackMovesPawn = new Bitboard[64];
            BlackPawnStep = new Bitboard[64];
            BlackPawnDoubleStep = new Bitboard[64];

            for (int i = 0; i < 64; i++)
            {
                if (Row[i] < 7 )
                {
                    // step white
                    SetBit(ref WhiteMovesPawn[i], i + 8);
                    SetBit(ref WhitePawnStep[i], i + 8);
                }

                if (Row[i] == 1)
                {
                    // double step white
                    SetBit(ref WhiteMovesPawn[i], i + 16);
                    SetBit(ref WhitePawnDoubleStep[i], i + 16);
                }

                if (Row[i] > 0)
                {
                    // step black
                    SetBit(ref BlackMovesPawn[i], i - 8);
                    SetBit(ref BlackPawnStep[i], i - 8);
                }

                if (Row[i] == 6)
                {
                    // step black
                    SetBit(ref BlackMovesPawn[i], i - 16);
                    SetBit(ref BlackPawnDoubleStep[i], i - 16);
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
                            SetBit(ref MaskWhitePassedPawns[x], y);
                        }

                        if (Row[x] > Row[y] && Row[y] > 0)
                        {
                            SetBit(ref MaskBlackPassedPawns[x], y);
                        }
                    }

                    if (Math.Abs(Col[x] - Col[y]) == 1)
                    {
                        SetBit(ref MaskIsolatedPawns[x], y);
                        SetBit(ref MaskIsolatedPawns[x], y);
                    }

                    if (Col[x] == Col[y])
                    {
                        if (Row[x] < Row[y])
                        {
                            SetBit(ref MaskWhitePawnsPath[x], y);
                        }

                        if (Row[x] > Row[y])
                        {
                            SetBit(ref MaskBlackPawnsPath[x], y);
                        }
                    }
                }
            }
        }

        public static string PrintBitboard(Bitboard bitboard)
        {
            var board = new StringBuilder();
            for (int y = 7; y >=0; y--)
            {
                for (int x = 0; x<= 7; x++)
                {
                    board.Append(GetBit(bitboard, x + 8 * y) ? " 1" : " .");
                }

                board.Append("\n");
            }

            return board.ToString();
        }

        private static void SetBit(ref UInt64 bitboard, int index)
        {
            bitboard |= ((UInt64)0x01 << index);
        }

        private static void ClearBit(ref UInt64 bitboard, int index)
        {
            bitboard &= (~((UInt64)0x01 << index));
        }
        private static bool GetBit(UInt64 bitboard, int index)
        {
            return (bitboard & ((UInt64)0x01 << index)) != (UInt64)0x00;
        }

        public static UInt64 ConvertToUInt64(byte[] input)
        {
            UInt64 result = 0;
            for (int i=0; i<64; i++)
            {
                if (input[i] != 0)
                {
                    SetBit(ref result, i);
                }
            }

            return result;
        }

        private const ulong DeBruijnSequence = 0x37E84A99DAE458F;

        private readonly int[] MultiplyDeBruijnBitPosition =
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
        public int BitScanForward(ulong b)
        {
            return MultiplyDeBruijnBitPosition[((ulong)((long)b & -(long)b) * DeBruijnSequence) >> 58];
        }

        private void SetRanks()
        {
            Rank = new int[2, 64];
            for (int i=0; i<64; i++)
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
                        SetBit(ref MaskCols[x], y);
                    }
                }
            }

            Not_A_file = ~MaskCols[0];
            Not_H_file = ~MaskCols[7];
        }

        public string SetFenPosition(string fen)
        {
            PositionInfo positionInfo;
            try
            {
                positionInfo = _fenParser.ToPositionInfo(fen);
            }
            catch (Exception ex)
            {
                return "FEN error: " + ex.StackTrace;
            }

            SetPosition(positionInfo.PositionString);
            BoardState.Add(
                null,
                positionInfo.EnPassantFile - '0',
                positionInfo.EnPassantRank,
                positionInfo.CastlingRightWhiteQueenSide,
                positionInfo.CastlingRightWhiteKingSide,
                positionInfo.CastlingRightBlackQueenSide,
                positionInfo.CastlingRightBlackKingSide,
                positionInfo.SideToMove);

            return string.Empty;
        }

        public string GetFenString()
        {
            throw new NotImplementedException();
        }

        public void SetInitialPosition()
        {
            string initPosition = "rnbqkbnr" + // black a8-h8
                                  "pppppppp" +
                                  "........" +
                                  "........" +
                                  "........" +
                                  "........" +
                                  "PPPPPPPP" +
                                  "RNBQKBNR"; // white a1-h1

            SetPosition(initPosition);
        }

        public void SetPosition(string position)
        {
            Bitboard_WhitePawn = GetBitboard(position, 'P');
            Bitboard_WhiteKnight = GetBitboard(position, 'N');
            Bitboard_WhiteBishop = GetBitboard(position, 'B');
            Bitboard_WhiteRook = GetBitboard(position, 'R');
            Bitboard_WhiteQueen = GetBitboard(position, 'Q');
            Bitboard_WhiteKing = GetBitboard(position, 'K');

            Bitboard_BlackPawn = GetBitboard(position, 'p');
            Bitboard_BlackKnight = GetBitboard(position, 'n');
            Bitboard_BlackBishop = GetBitboard(position, 'b');
            Bitboard_BlackRook = GetBitboard(position, 'r');
            Bitboard_BlackQueen = GetBitboard(position, 'q');
            Bitboard_BlackKing = GetBitboard(position, 'k');
        }

        private Bitboard GetBitboard(string position, char piece)
        {
            if (position.Length != 64)
            {
                throw new Exception("position string must be of length 64!");
            }

            Bitboard bitboard = 0;
            int i = 0;
            for (int row = 7; row >= 0; row--)
            {
                for (int col = 0; col <= 7; col++)
                {
                    if (position[row * 8 + col].Equals(piece))
                    {
                        SetBit(ref bitboard, i);
                    }

                    i++;
                }
            }

            return bitboard;
        }

        public Piece GetPiece(int file, int rank)
        {
            throw new NotImplementedException();
        }

        public void SetPiece(Piece piece, int file, int rank)
        {
            throw new NotImplementedException();
        }
       
        public void Move(IMove nextMove)
        {
            throw new NotImplementedException();
        }

        public void Move(BitMove nextMove)
        {
            _moveExecutor.DoMove(nextMove, this);
        }

        public void Back()
        {
            throw new NotImplementedException();
        }

        public Definitions.ChessColor GetColor(int file, int rank)
        {
            throw new NotImplementedException();
        }

        public Position GetKing(Definitions.ChessColor color)
        {
            throw new NotImplementedException();
        }
    }
}
