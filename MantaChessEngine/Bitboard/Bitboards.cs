using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bitboard = System.UInt64;


namespace MantaChessEngine
{
    public class Chess
    {
        public const int A1 = 0;
        public const int B1 = 1;
        public const int C1 = 2;
        public const int D1 = 3;
        public const int E1 = 4;
        public const int F1 = 5;
        public const int G1 = 6;
        public const int H1 = 7;

        public const int A2 = 8;
        public const int B2 = 9;
        public const int C2 = 10;
        public const int D2 = 11;
        public const int E2 = 12;
        public const int F2 = 13;
        public const int G2 = 14;
        public const int H2 = 15;

        public const int A3 = 16;
        public const int B3 = 17;
        public const int C3 = 18;
        public const int D3 = 19;
        public const int E3 = 20;
        public const int F3 = 21;
        public const int G3 = 22;
        public const int H3 = 23;

        public const int A4 = 24;
        public const int B4 = 25;
        public const int C4 = 26;
        public const int D4 = 27;
        public const int E4 = 28;
        public const int F4 = 29;
        public const int G4 = 30;
        public const int H4 = 31;

        public const int A5 = 32;
        public const int B5 = 33;
        public const int C5 = 34;
        public const int D5 = 35;
        public const int E5 = 36;
        public const int F5 = 37;
        public const int G5 = 38;
        public const int H5 = 39;

        public const int A6 = 40;
        public const int B6 = 41;
        public const int C6 = 42;
        public const int D6 = 43;
        public const int E6 = 44;
        public const int F6 = 45;
        public const int G6 = 46;
        public const int H6 = 47;

        public const int A7 = 48;
        public const int B7 = 49;
        public const int C7 = 50;
        public const int D7 = 51;
        public const int E7 = 52;
        public const int F7 = 53;
        public const int G7 = 54;
        public const int H7 = 55;

        public const int A8 = 56;
        public const int B8 = 57;
        public const int C8 = 58;
        public const int D8 = 59;
        public const int E8 = 60;
        public const int F8 = 61;
        public const int G8 = 62;
        public const int H8 = 63;

        public const int Pawn = 0;
        public const int Knight = 1;
        public const int Bishop = 2;
        public const int Rook = 3;
        public const int Queen = 3;
        public const int King = 5;

        public const int White = 1;
        public const int Black = -1;
    }

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
            SetBetweenMatrix();
            SetRayAfterMatrix();
            SetKingAndQueenSideMask();
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
                7, 6, 5, 4, 3, 2, 1, 0,
                7, 6, 5, 4, 3, 2, 1, 0,
                7, 6, 5, 4, 3, 2, 1, 0,
                7, 6, 5, 4, 3, 2, 1, 0,
                7, 6, 5, 4, 3, 2, 1, 0,
                7, 6, 5, 4, 3, 2, 1, 0,
                7, 6, 5, 4, 3, 2, 1, 0,
                7, 6, 5, 4, 3, 2, 1, 0,
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

        private int GetEdge(int square, int direction)
        {
            while (square >= 0 && Col[square] < 7 && Col[square] > 0 && Row[square] < 7 && Row[square] > 0)
            {
                square += direction;
            }
            
            return square;
        }

        private void SetKingAndQueenSideMask()
        {
            Bitboard queenSideMask = 0;
            Bitboard kingSideMask = 0;
            for (int x = 0; x < 64; x++)
            {
                if (Col[x] < 2)
                {
                    SetBit(ref queenSideMask, x);
                }

                if (Col[x] > 5)
                {
                    SetBit(ref kingSideMask, x);
                }
            }

            QueenSideMask = queenSideMask;
            KingSideMask = kingSideMask;
        }

        private static void SetBit(ref UInt64 bitboard, int index)
        {
            bitboard |= ((UInt64)0x01 << index);
        }

        private static void ClearBit(ref UInt64 bitboard, int index)
        {
            bitboard &= (~((UInt64)0x01 << index));
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
