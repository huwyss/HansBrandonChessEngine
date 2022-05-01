﻿using System;
using System.Text;
using Bitboard = System.UInt64;

namespace MantaChessEngine
{
    public class Bitboards : IBoard
    {
        private FenParser _fenParser;

        // todo these are Pieces[2][7]
        public Bitboard Bitboard_WhitePawn { get; set; }
        public Bitboard Bitboard_WhiteRook { get; set; }
        public Bitboard Bitboard_WhiteKnight { get; set; }
        public Bitboard Bitboard_WhiteBishop { get; set; }
        public Bitboard Bitboard_WhiteQueen { get; set; }
        public Bitboard Bitboard_WhiteKing { get; set; }

        public Bitboard Bitboard_BlackPawn { get; set; }
        public Bitboard Bitboard_BlackRook { get; set; }
        public Bitboard Bitboard_BlackKnight { get; set; }
        public Bitboard Bitboard_BlackBishop { get; set; }
        public Bitboard Bitboard_BlackQueen { get; set; }
        public Bitboard Bitboard_BlackKing { get; set; }

        public Bitboard[,] BetweenMatrix { get; set; }
        public Bitboard[,] RayAfterMatrix { get; set; }

        public Bitboard QueenSideMask { get; set; }
        public Bitboard KingSideMask { get; set; }

        public Bitboard[] MovesPawnLeft { get; set; }
        public Bitboard[] MovesPawnRight { get; set; }
        public Bitboard[] MovesPawnWalk { get; set; }
        public Bitboard[] MovesKnight { get; set; }
        public Bitboard[] MovesBishop { get; set; }
        public Bitboard[] MovesRook { get; set; }
        public Bitboard[] MovesQueen { get; set; }
        public Bitboard[] MovesKing { get; set; }



        public int[] Row { get; set; }
        public int[] Col { get; set; }
        public int[] DiagNO { get; set; }
        public int[] DiagNW { get; set; }
        public Definitions.ChessColor SideToMove { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int MoveCountSincePawnOrCapture { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public History History { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public IMove LastMove => throw new NotImplementedException();

        public int EnPassantFile => throw new NotImplementedException();

        public int EnPassantRank => throw new NotImplementedException();

        public bool CastlingRightWhiteQueenSide => throw new NotImplementedException();

        public bool CastlingRightWhiteKingSide => throw new NotImplementedException();

        public bool CastlingRightBlackQueenSide => throw new NotImplementedException();

        public bool CastlingRightBlackKingSide => throw new NotImplementedException();

        public bool WhiteDidCastling { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool BlackDidCastling { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public string GetPositionString => throw new NotImplementedException();

        public string GetPrintString => throw new NotImplementedException();

        public Bitboards()
        {
            _fenParser = new FenParser();
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
            History.Add(
                null,
                positionInfo.EnPassantFile - '0',
                positionInfo.EnPassantRank,
                positionInfo.CastlingRightWhiteQueenSide,
                positionInfo.CastlingRightWhiteKingSide,
                positionInfo.CastlingRightBlackQueenSide,
                positionInfo.CastlingRightBlackKingSide);
            SideToMove = positionInfo.SideToMove;

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

        public Piece GetPiece(char fileChar, int rank)
        {
            throw new NotImplementedException();
        }

        public void SetPiece(Piece piece, int file, int rank)
        {
            throw new NotImplementedException();
        }

        public void SetPiece(Piece piece, char fileChar, int rank)
        {
            throw new NotImplementedException();
        }

        public void Move(IMove nextMove)
        {
            throw new NotImplementedException();
        }

        public void Back()
        {
            throw new NotImplementedException();
        }

        public void RedoMove()
        {
            throw new NotImplementedException();
        }

        public Definitions.ChessColor GetColor(int file, int rank)
        {
            throw new NotImplementedException();
        }

        public bool IsWinner(Definitions.ChessColor color)
        {
            throw new NotImplementedException();
        }

        public Position GetKing(Definitions.ChessColor color)
        {
            throw new NotImplementedException();
        }
    }
}
