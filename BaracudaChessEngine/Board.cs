using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaracudaChessEngine
{
    public class Board
    {
        char[] _board;
        private int[] _threatenedByWhite;
        private int[] _threatenedByBlack;

        string initPosition = "rnbqkbnr" + // black a8-h8
                              "pppppppp" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "PPPPPPPP" +
                              "RNBQKBNR"; // white a1-h1

        public Definitions.ChessColor SideToMove { get; set; }
        public int EnPassantField { get; set; }
        public bool CastlingRightFirstMover { get; set; }
        public bool CastlingRightSecondMover { get; set; }

        public List<Move> Moves { get; set; }

        /// <summary>
        /// Constructor. Board is empty.
        /// </summary>
        public Board()
        {
            _board = new char[64];
            for (int i = 0; i < 64; i++)
            {
                _board[i] = Definitions.EmptyField;
            }

            InitVariables();
        }

        /// <summary>
        /// Sets the initial chess position.
        /// </summary>
        public void SetInitialPosition()
        {
            SetPosition(initPosition);
            InitVariables();
        }

        private void InitVariables()
        {
            SideToMove = Definitions.ChessColor.White;
            EnPassantField = -1;
            CastlingRightFirstMover = true;
            CastlingRightSecondMover = true;
            Moves = new List<Move>();
        }

        public void SetPosition(string position)
        {
            if (position.Length != 64)
            {
                return;
            }

            for (int rank0 = 0; rank0 < 8; rank0++)
            {
                for (int file0 = 0; file0 < 8; file0++)
                {
                    _board[8*rank0 + file0] = position[8*(7 - rank0) + file0];
                }
            }
        }

        /// <summary>
        /// Returns the chess piece.
        /// </summary>
        /// <param name="file">1 to 8</param>
        /// <param name="rank">1 to 8</param>
        /// <returns></returns>
        public char GetPiece(int file, int rank)
        {
            return _board[8*(rank - 1) + file - 1];
        }

        /// <summary>
        /// Returns the chess piece.
        /// </summary>
        /// <param name="fileChar">'a' to 'h'</param>
        /// <param name="rank">1 to 8</param>
        /// <returns></returns>
        public char GetPiece(char fileChar, int rank)
        {
            return GetPiece(Helper.FileCharToFile(fileChar), rank);
        }

        /// <summary>
        /// Sets a chess piece to the field.
        /// </summary>
        /// <param name="piece">chess piece: p-pawn, k-king, q-queen, r-rook, n-knight, b-bishop, r-rook, ' '-empty. small=black, capital=white</param>
        /// <param name="file">1 to 8</param>
        /// <param name="rank">1 to 8</param>
        public void SetPiece(char piece, int file, int rank)
        {
            _board[8*(rank - 1) + file - 1] = piece;
        }

        /// <summary>
        /// Sets a chess piece to the field.
        /// </summary>
        /// <param name="piece">chess piece: p-pawn, k-king, q-queen, r-rook, n-knight, b-bishop, r-rook, ' '-empty. small=black, capital=white</param>
        /// <param name="fileChar">'a' to 'h'</param>
        /// <param name="rank">1 to 8</param>
        public void SetPiece(char piece, char fileChar, int rank)
        {
            SetPiece(piece, Helper.FileCharToFile(fileChar), rank);
        }

        /// <summary>
        /// Do a move and update the board.
        /// </summary>
        public void Move(char sourceFileChar, int sourceRank, char targetFileChar, int targetRank)
        {
            Move(Helper.FileCharToFile(sourceFileChar), sourceRank, Helper.FileCharToFile(targetFileChar), targetRank);
        }

        /// <summary>
        /// Move parameter of Type Move.
        /// </summary>
        public void Move(Move nextMove)
        {
            // note: todo? captured piece is ignored.
            Move(nextMove.SourceFile, nextMove.SourceRank, nextMove.TargetFile, nextMove.TargetRank);
        }

        /// <summary>
        /// Do a move and update the board.
        /// </summary>
        public void Move(int sourceFile, int sourceRank, int targetFile, int targetRank)
        {
            char pieceToMove = GetPiece(sourceFile, sourceRank);
            char capturedPiece = GetPiece(targetFile, targetRank);

            var currentMove = new Move(sourceFile, sourceRank, targetFile, targetRank, capturedPiece);
            Moves.Add(currentMove);

            SetPiece(pieceToMove, targetFile, targetRank);
            SetPiece(Definitions.EmptyField, sourceFile, sourceRank);
        }

        public Definitions.ChessColor GetColor(int file, int rank)
        {
            char piece = GetPiece(file, rank);
            if (piece >= 'a' && piece <= 'z')
            {
                return Definitions.ChessColor.Black;
            }
            else if (piece >= 'A' && piece <= 'Z')
            {
                return Definitions.ChessColor.White;
            }
            else
            {
                return Definitions.ChessColor.Empty;
            }
        }

        public bool IsWinner(Definitions.ChessColor color)
        {
            bool hasWhiteKing = false;
            bool hasBlackKing = false;

            for (int rank = 1; rank <= 8; rank++)
            {
                for (int file = 1; file <= 8; file++)
                {
                    char piece = GetPiece(file, rank);
                    if (piece == Definitions.KING.ToString().ToUpper()[0])
                    {
                        hasWhiteKing = true;
                    }
                    if (piece == Definitions.KING.ToString().ToLower()[0])
                    {
                        hasBlackKing = true;
                    }
                }

            }

            if (color == Definitions.ChessColor.White)
            {
                return !hasBlackKing;
            }

            if (color == Definitions.ChessColor.Black)
            {
                return !hasWhiteKing;
            }

            return false;
        }

        public string GetString()
        {
            string boardString = "";

            for (int rank = 8; rank >= 1 ; rank--)
            {
                for (int file = 1; file <= 8; file++)
                {
                    char piece = GetPiece(file, rank);
                    boardString += piece;
                }
            }

            return boardString;
        }

        public string GetPrintString()
        {
            string boardString = "";

            for (int rank = 8; rank >= 1; rank--)
            {
                boardString += rank + "   ";
                for (int file = 1; file <= 8; file++)
                {
                    char piece = GetPiece(file, rank);
                    boardString += piece + " ";

                }

                boardString += "\n";
            }

            boardString += "\n    a b c d e f g h \n";

            return boardString;
        }
    }
}
