using System;
using static MantaChessEngine.Definitions;

namespace MantaChessEngine
{
    public class Board : IBoard
    {
        private readonly FenParser _fenParser;
        private IMove _undoneMove = null;

        public BoardState BoardState { get; }

        private readonly Piece[] _board;

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

            BoardState = new BoardState();
            InitVariables();

            _fenParser = new FenParser();
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
            BoardState.Clear();
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
            var positionInfo = new PositionInfo()
            {
                PositionString = GetPositionString
            };

            var fen = _fenParser.ToFen(positionInfo);
            return fen;
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
        /// Do a move and update the board
        /// </summary>
        public void Move(IMove nextMove)
        {
            if (nextMove == null)
            {
                return;
            }

            nextMove.ExecuteMove(this);
        }

        public ChessColor GetColor(int file, int rank)
        {
            Piece piece = GetPiece(file, rank);
            return piece != null ? piece.Color : ChessColor.Empty;
        }

        public string GetPositionString
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

                boardString += "    a  b  c  d  e  f  g  h\n";
                boardString += "  +------------------------+\n";

                for (int rank = 8; rank >= 1; rank--)
                {
                    boardString += rank + " |";
                    for (int file = 1; file <= 8; file++)
                    {
                        Piece piece = GetPiece(file, rank);
                        boardString += " ";
                        boardString += piece != null ? piece.Symbol : EmptyField;
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
            if (BoardState.Count >= 1)
            {
                _undoneMove = BoardState.LastMove;
                _undoneMove.UndoMove(this);
            }
        }

        public Position GetKing(ChessColor color)
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
