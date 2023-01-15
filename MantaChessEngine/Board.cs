using System;
using static MantaChessEngine.Definitions;
using MantaCommon;

namespace MantaChessEngine
{
    public class Board : IBoard
    {
        private readonly IHashtable _hashtable;
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
        public Board(IHashtable hashtable)
        {
            _hashtable = hashtable;
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
                    var pieceSymbol = position[8 * (7 - rank0) + file0];
                    var pieceType = CommonHelper.GetPieceType(pieceSymbol);
                    var color = Helper.GetPieceColor(pieceSymbol);
                    _board[8*rank0 + file0] = Piece.MakePiece(pieceType, color);
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
            var enpassantSquare = positionInfo.EnPassantFile != '\0'
                ? (Square)(positionInfo.EnPassantFile - '0' - 1 + 8 * positionInfo.EnPassantRank)
                : Square.NoSquare;
            BoardState.Add(
                null,
                enpassantSquare,
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
        public Piece GetPiece(Square square)
        {
            return _board[(int)square];
        }

        /// <summary>
        /// Sets a chess piece to the field.
        /// </summary>
        /// <param name="piece">chess piece: p-pawn, k-king, q-queen, r-rook, n-knight, b-bishop, r-rook, ' '-empty. small=black, capital=white</param>
        /// <param name="file">1 to 8</param>
        /// <param name="rank">1 to 8</param>
        public void SetPiece(Piece piece, Square square)
        {
            if (piece == null)
            {
                throw new MantaEngineException($"SetPiece called with null is not allowed! on square: {square}");
            }

            _board[(int)square] = piece;

            _hashtable.AddKey(piece.Color, piece.PieceType, square);
        }

        public void RemovePiece(Square square)
        {
            var deletedPiece = GetPiece(square);
            if (deletedPiece == null)
            {
                throw new MantaEngineException($"RemovePiece called but the square is already empty! square: {square}");
            }

            _board[(int)square] = null;

            _hashtable.AddKey(deletedPiece.Color, deletedPiece.PieceType, square);
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

        public ChessColor GetColor(Square square)
        {
            Piece piece = GetPiece(square);
            return piece != null ? piece.Color : ChessColor.Empty;
        }

        public string GetPositionString
        {
            get
            {
                string boardString = "";
                var row = 7;
                var col = 0;

                for (int i = 0; i < 64; i++)
                {
                    var square = (Square)(col + 8 * row);
                    var piece = GetPiece(square);

                    boardString += piece!=null ? piece.Symbol : CommonDefinitions.EmptyField;
                    col++;
                    if (col >= 8)
                    {
                        row--;
                        col = 0;
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
                var row = 7;
                var col = 0;

                boardString += "    a  b  c  d  e  f  g  h\n";
                boardString += "  +------------------------+\n";

                for (int i = 0; i < 64; i++)
                {
                    if (col == 0)
                    {
                        boardString += (row + 1) + " |";
                    }

                    var square = (Square)(col + 8 * row);
                    var piece = GetPiece(square);
                    
                    boardString += " ";
                    boardString += piece != null ? piece.Symbol : CommonDefinitions.EmptyField;
                    boardString += " ";


                    if (col == 7)
                    {
                        boardString += "| " + (row + 1) + "\n";
                    }

                    col++;
                    if (col >= 8)
                    {
                        row--;
                        col = 0;
                        if (row >= 0)
                        {
                            boardString += "  |                        |\n";
                        }
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

        public Square GetKing(ChessColor color)
        {
            for (int i = 0; i < 64; i++)
            {
                var king = GetPiece((Square)i) as King;
                if (king != null && king.Color == color)
                {
                    return (Square)i;
                }
            }

            return Square.NoSquare;
        }
    }
}
