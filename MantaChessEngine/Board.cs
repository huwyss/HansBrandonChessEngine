using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MantaChessEngine
{
    public class Board : IBoard
    {
        private IMove _undoneMove = null;

        public Definitions.ChessColor SideToMove { get; set; }
        public History History { get; set; }
        public IMove LastMove { get { return History.LastMove; } }

        public int EnPassantFile { get { return History.LastEnPassantFile ; } }
        public int EnPassantRank { get { return History.LastEnPassantRank ; } }

        public bool CastlingRightWhiteQueenSide { get { return History.LastCastlingRightWhiteQueenSide; } }
        public bool CastlingRightWhiteKingSide { get { return History.LastCastlingRightWhiteKingSide; } } 
        public bool CastlingRightBlackQueenSide { get { return History.LastCastlingRightBlackQueenSide; } }
        public bool CastlingRightBlackKingSide { get { return History.LastCastlingRightBlackKingSide; } } 

        public bool WhiteDidCastling { get; set; }
        public bool BlackDidCastling { get; set; }

        private Piece[] _board;

        string initPosition = "rnbqkbnr" + // black a8-h8
                              "pppppppp" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "PPPPPPPP" +
                              "RNBQKBNR"; // white a1-h1

        /// <summary>
        /// Constructor. Board is empty.
        /// </summary>
        public Board()
        {
            _board = new Piece[64];

            for (int i = 0; i < 64; i++)
            {
                _board[i] = null; // Definitions.EmptyField;
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
            History = new History();
            WhiteDidCastling = false;
            BlackDidCastling = false;
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
                    _board[8*rank0 + file0] = Piece.MakePiece(position[8*(7 - rank0) + file0]);
                }
            }
        }

        /// <summary>
        /// Returns the chess piece.
        /// </summary>
        /// <param name="file">1 to 8</param>
        /// <param name="rank">1 to 8</param>
        /// <returns></returns>
        public Piece GetPiece(int file, int rank)
        {
            return _board[8*(rank - 1) + file - 1];
        }

        /// <summary>
        /// Returns the chess piece.
        /// </summary>
        /// <param name="fileChar">'a' to 'h'</param>
        /// <param name="rank">1 to 8</param>
        /// <returns></returns>
        public Piece GetPiece(char fileChar, int rank)
        {
            return GetPiece(Helper.FileCharToFile(fileChar), rank);
        }

        /// <summary>
        /// Sets a chess piece to the field.
        /// </summary>
        /// <param name="piece">chess piece: p-pawn, k-king, q-queen, r-rook, n-knight, b-bishop, r-rook, ' '-empty. small=black, capital=white</param>
        /// <param name="file">1 to 8</param>
        /// <param name="rank">1 to 8</param>
        public void SetPiece(Piece piece, int file, int rank)
        {
            _board[8*(rank - 1) + file - 1] = piece;
        }

        /// <summary>
        /// Sets a chess piece to the field.
        /// </summary>
        /// <param name="piece">chess piece: p-pawn, k-king, q-queen, r-rook, n-knight, b-bishop, r-rook, ' '-empty. small=black, capital=white</param>
        /// <param name="fileChar">'a' to 'h'</param>
        /// <param name="rank">1 to 8</param>
        public void SetPiece(Piece piece, char fileChar, int rank)
        {
            SetPiece(piece, Helper.FileCharToFile(fileChar), rank);
        }

        /// <summary>
        /// Do a move and update the board
        /// </summary>
        public void Move(IMove nextMove)
        {
            IMove move = nextMove as IMove;
            if (move == null)
            {
                return;
            }

            move.ExecuteMove(this);
        }

        public Definitions.ChessColor GetColor(int file, int rank)
        {
            Piece piece = GetPiece(file, rank);
            return piece != null ? piece.Color : Definitions.ChessColor.Empty;
        }

        public bool IsWinner(Definitions.ChessColor color)
        {
            // todo das stimmt nicht. Winner ist schon vorher, wenn noch König vorhanden aber im Schachmatt!

            bool hasWhiteKing = false;
            bool hasBlackKing = false;

            for (int rank = 1; rank <= 8; rank++)
            {
                for (int file = 1; file <= 8; file++)
                {
                    Piece piece = GetPiece(file, rank);
                    if (piece is King && piece.Color == Definitions.ChessColor.White)
                    {
                        hasWhiteKing = true;
                    }

                    if (piece is King && piece.Color == Definitions.ChessColor.Black)
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

        public string GetString
        {
            get
            {
                string boardString = "";

                for (int rank = 8; rank >= 1; rank--)
                {
                    for (int file = 1; file <= 8; file++)
                    {
                        Piece piece = GetPiece(file, rank);

                        boardString += piece!=null ? piece.Symbol : Definitions.EmptyField;
                    }
                }

                return boardString;
            }
        }

        public string GetPrintString
        {
            get
            {
                string boardString = "";

                for (int rank = 8; rank >= 1; rank--)
                {
                    boardString += rank + "   ";
                    for (int file = 1; file <= 8; file++)
                    {
                        Piece piece = GetPiece(file, rank);
                        boardString += piece != null ? piece.Symbol : Definitions.EmptyField;
                        boardString += " ";
                    }

                    boardString += "\n";
                }

                boardString += "\n    a b c d e f g h \n";

                return boardString;
            }
        }

        public string GetPrintString2
        {
            get
            {
                string boardString = "";

                boardString += "    a  b  c  d  e  f  g  h\n";
                boardString += "  +------------------------+\n";

                for (int rank = 8; rank >= 1; rank--)
                {
                    boardString += rank + " |";
                    for (int file = 1; file <= 8; file++)
                    {
                        Piece piece = GetPiece(file, rank);
                        boardString += " ";
                        boardString += piece != null ? piece.Symbol : Definitions.EmptyField;
                        boardString += " ";
                    }

                    boardString += "| "+ rank + "\n";

                    if (rank != 1)
                    {
                        boardString += "  |                        |\n";
                    }
                }

                boardString += "  +------------------------+\n";
                boardString += "    a  b  c  d  e  f  g  h\n";

                return boardString;
            }
        }

        public void Back()
        {
            if (History.Count >= 1)
            {
                _undoneMove = LastMove;
                LastMove.UndoMove(this);
            }
        }

        /// <summary>
        /// can redo 1 move that was previously undone
        /// </summary>
        public void RedoMove() 
        {
            if (_undoneMove != null)
            {
                _undoneMove.ExecuteMove(this);
            }

            _undoneMove = null;
        }

        public Position GetKing(Definitions.ChessColor color)
        {
            for (int rank = 1; rank <= 8; rank++)
            {
                for (int file = 1; file <= 8; file++)
                {
                    var king = GetPiece(file, rank) as King;
                    if (king != null && king.Color == color)
                    {
                        return new Position { Rank = rank, File = file };
                    }
                }
            }

            return null;
        }
    }

    public class Position
    {
        public int Rank { get; set; }
        public int File { get; set; }
    }           
}
