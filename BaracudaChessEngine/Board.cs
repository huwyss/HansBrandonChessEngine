using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaracudaChessEngine
{
    public class Board
    {
        public Definitions.ChessColor SideToMove { get; set; }
        public History History { get; set; }
        public Move LastMove { get { return History.LastMove; } }

        public int EnPassantFile
        {
            get
            {
                if (!IsClonedBoard)
                {
                    return History.LastEnPassantFile;
                }
                
                return ClonedEnPassantFile;
            }
        }

        public int EnPassantRank
        {
            get
            {
                if (!IsClonedBoard)
                {
                    return History.LastEnPassantRank;
                }

                return ClonedEnPassantRank;
            }
        }

        public int ClonedEnPassantFile { get; set; }
        public int ClonedEnPassantRank { get; set; }

        public bool IsClonedBoard { get; set; }

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

        /// <summary>
        /// Constructor. Board is empty.
        /// </summary>
        public Board(MoveGenerator moveGenerator)
        {
            _board = new char[64];
            for (int i = 0; i < 64; i++)
            {
                _board[i] = Definitions.EmptyField;
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
            clonedBoard.IsClonedBoard = true;
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

            SetPiece(move.MovingPiece, move.TargetFile, move.TargetRank);
            SetPiece(Definitions.EmptyField, move.SourceFile, move.SourceRank);

            if (move.EnPassant)
            {
                SetPiece(Definitions.EmptyField, move.CapturedFile, move.CapturedRank);
            }

            int sourceFile = move.SourceFile;
            int sourceRank = move.SourceRank;
            int targetFile = move.TargetFile;
            int targetRank = move.TargetRank;

            bool enPassant = move.EnPassant;

            char pieceToMove = GetPiece(sourceFile, sourceRank);
            Definitions.ChessColor color = GetColor(sourceFile, sourceRank);

            char capturedPiece;

            int enPassantFile = 0;
            int enPassantRank = 0;

            //if (enPassant)
            //{
            //    // Black
            //    if (color == Definitions.ChessColor.Black)
            //    {
            //        capturedPiece = GetPiece(targetFile, targetRank + 1);
            //        SetPiece(Definitions.EmptyField, targetFile, targetRank + 1); // if en passant capture then remove the passed pawn
            //    }
            //    else // white
            //    {
            //        capturedPiece = GetPiece(targetFile, targetRank - 1);
            //        SetPiece(Definitions.EmptyField, targetFile, targetRank - 1); // if en passant capture then remove the passed pawn
            //    }
            //}
            //else
            //{
            //    capturedPiece = GetPiece(targetFile, targetRank);
            //}

            //var currentMove = new Move(pieceToMove, sourceFile, sourceRank, targetFile, targetRank, capturedPiece, enPassant);

            //SetPiece(pieceToMove, targetFile, targetRank);
            //SetPiece(Definitions.EmptyField, sourceFile, sourceRank);

            // set black en passant field
            if (move.MovingPiece == Definitions.PAWN.ToString().ToLower()[0]) // black pawn
            {
                // set en passant field
                if (sourceRank - 2 == targetRank && // if 2 fields move
                    sourceFile == targetFile)       // and straight move 
                {
                    enPassantRank = sourceRank - 1;
                    enPassantFile = sourceFile;
                }
            }

            // set white en passant field
            if (move.MovingPiece == Definitions.PAWN.ToString().ToUpper()[0]) // white pawn
            {
                // set en passant field
                if (sourceRank + 2 == targetRank && // if 2 fields move
                    sourceFile == targetFile)       // and straight move 
                {
                    enPassantRank = sourceRank + 1;
                    enPassantFile = sourceFile;
                }
            }

            //History.Add(currentMove, enPassantFile, enPassantRank);
            History.Add(move, enPassantFile, enPassantRank);
            SideToMove = Helper.GetOpositeColor(SideToMove);
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
            return _moveGenerator.GetCorrectMove(this, moveStringUser);
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
            var moves = _moveGenerator.GetAllMoves(this, Helper.GetOpositeColor(color));

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

        public void Back()
        {
            if (History.Count >= 1)
            {
                var lastMove = History.LastMove;
                Definitions.ChessColor color = lastMove.Color; 
                SetPiece(lastMove.MovingPiece, lastMove.SourceFile, lastMove.SourceRank);

                SetPiece(Definitions.EmptyField, lastMove.TargetFile, lastMove.TargetRank);     // TargetFile is equal to CapturedFile
                SetPiece(lastMove.CapturedPiece, lastMove.CapturedFile, lastMove.CapturedRank); // TargetRank differs from TargetRank for en passant capture

                History.Back(); 
                SideToMove = Helper.GetOpositeColor(SideToMove);
            }
        }
    }
}
