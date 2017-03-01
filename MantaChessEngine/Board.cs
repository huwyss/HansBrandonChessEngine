using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MantaChessEngine
{
    public class Board
    {
        public Definitions.ChessColor SideToMove { get; set; }
        public History History { get; set; }
        public Move LastMove { get { return History.LastMove; } }

        public int EnPassantFile { get { return !IsClonedBoard ? History.LastEnPassantFile : ClonedEnPassantFile; } }
        public int EnPassantRank { get { return !IsClonedBoard ? History.LastEnPassantRank : ClonedEnPassantRank; } }

        public bool CastlingRightWhiteQueenSide { get { return !IsClonedBoard ? History.LastCastlingRightWhiteQueenSide : ClonedCastlingRightWhiteQueenSide; } }
        public bool CastlingRightWhiteKingSide { get { return !IsClonedBoard ? History.LastCastlingRightWhiteKingSide : ClonedCastlingRightWhiteKingSide; } }
        public bool CastlingRightBlackQueenSide { get { return !IsClonedBoard ? History.LastCastlingRightBlackQueenSide : ClonedCastlingRightBlackQueenSide; } }
        public bool CastlingRightBlackKingSide { get { return !IsClonedBoard ? History.LastCastlingRightBlackKingSide : ClonedCastlingRightBlackKingSide; } }

        public int ClonedEnPassantFile { get; set; }
        public int ClonedEnPassantRank { get; set; }

        public bool ClonedCastlingRightWhiteQueenSide { get; set; }
        public bool ClonedCastlingRightWhiteKingSide { get; set; }
        public bool ClonedCastlingRightBlackQueenSide { get; set; }
        public bool ClonedCastlingRightBlackKingSide { get; set; }

        public bool IsClonedBoard { get; set; }

        public bool WhiteDidCastling { get; set; }
        public bool BlackDidCastling { get; set; }

        private MoveGenerator _moveGenerator;
        private char[] _board;
        string initPosition = "rnbqkbnr" + // black a8-h8
                              "pppppppp" +
                              "........" +
                              "........" +
                              "........" +
                              "........" +
                              "PPPPPPPP" +
                              "RNBQKBNR"; // white a1-h1

        private int[] _attackedFields;

        /// <summary>
        /// Constructor. Board is empty.
        /// </summary>
        public Board(MoveGenerator moveGenerator)
        {
            _board = new char[64];
            _attackedFields = new int[64];
            for (int i = 0; i < 64; i++)
            {
                _board[i] = Definitions.EmptyField;
                _attackedFields[i] = 0;
            }

            InitVariables();
            _moveGenerator = moveGenerator;
        }

        /// <summary>
        /// Clone the Board (not the complete history, only en passant fields)
        /// </summary>
        public Board Clone()
        {
            Board clonedBoard = new Board(_moveGenerator);
            string position = GetString;
            clonedBoard.SetPosition(position);
            clonedBoard.ClonedEnPassantFile = EnPassantFile;
            clonedBoard.ClonedEnPassantRank = EnPassantRank;
            clonedBoard.ClonedCastlingRightWhiteQueenSide = CastlingRightWhiteQueenSide;
            clonedBoard.ClonedCastlingRightWhiteKingSide = CastlingRightWhiteKingSide;
            clonedBoard.ClonedCastlingRightBlackQueenSide = CastlingRightBlackQueenSide;
            clonedBoard.ClonedCastlingRightBlackKingSide = CastlingRightBlackKingSide;
            clonedBoard.IsClonedBoard = true;
            clonedBoard.WhiteDidCastling = WhiteDidCastling;
            clonedBoard.BlackDidCastling = BlackDidCastling;
            return clonedBoard;
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
            IsClonedBoard = false;
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

        public void Move(string userMoveString)
        {
            Move correctedMove = GetCorrectMove(userMoveString);
            Move(correctedMove);
        }

        /// <summary>
        /// Do a move and update the board
        /// </summary>
        public void Move(IMove nextMove)
        {
            Move move = nextMove as Move;
            if (move == null)
            {
                return;
            }

            move.ExecuteMove(this);
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

        public string GetString
        {
            get
            {
                string boardString = "";

                for (int rank = 8; rank >= 1; rank--)
                {
                    for (int file = 1; file <= 8; file++)
                    {
                        char piece = GetPiece(file, rank);
                        boardString += piece;
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
                        char piece = GetPiece(file, rank);
                        boardString += piece + " ";

                    }

                    boardString += "\n";
                }

                boardString += "\n    a b c d e f g h \n";

                return boardString;
            }
        }

        public Move GetCorrectMove(string moveStringUser)
        {
            return MoveFactory.GetCorrectMove(this, moveStringUser);
        }

        public bool IsMoveValid(Move move)
        {
            return _moveGenerator.IsMoveValid(this, move);
        }

        public List<Move> GetAllMoves(Definitions.ChessColor color)
        {
            return _moveGenerator.GetAllMoves(this, color);
        }

        public bool IsCheck(Definitions.ChessColor color)
        {
            char king;

            if (color == Definitions.ChessColor.White)
            {
                king = Definitions.KING.ToString().ToUpper()[0];
            }
            else
            {
                king = Definitions.KING.ToString().ToLower()[0];
            }

            // find all oponent moves
            var moves = _moveGenerator.GetAllMoves(this, Helper.GetOpositeColor(color), false);

            // if a move ends in king's position then king is in check
            foreach (Move move in moves)
            {
                if (move.CapturedPiece == king)
                {
                    return true;
                }
            }

            return false;
        }

        public bool IsAttacked(Definitions.ChessColor color, int file, int rank)
        {
            // find all oponent moves
            var moves = _moveGenerator.GetAllMoves(this, Helper.GetOpositeColor(color), false);

            // if a move ends in king's position then king is in check
            foreach (Move move in moves)
            {
                if (move.TargetFile == file && move.TargetRank == rank)
                {
                    return true;
                }
            }

            return false;
        }

        public void Back()
        {
            if (History.Count >= 1)
            {
                LastMove.UndoMove(this);
            }
        }
    }
}
