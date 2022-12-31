using MantaChessEngine;
using System;
using System.Text;
using System.Runtime.CompilerServices;
using Bitboard = System.UInt64;
using MantaCommon;

/*
 * 
 * todo Bitboard engine:
 * 
 * - tests for search in bitboard
 * - hashtable
 * 
 * 
 * ok - Generate capture moves
 * ok - more differenciated move values
 * ok - selective search depth
 * 
 */



[assembly: InternalsVisibleTo("MantaBitboardEngineTest")]
namespace MantaBitboardEngine
{
    public class Bitboards : IBitBoard
    {
        private readonly FenParser _fenParser;
        private readonly BitMoveExecutor _moveExecutor;
        private readonly IHashtable _hashtable;

        public BitPieceType[] BoardAllPieces;
        public ChessColor[] BoardColor;

        /// <summary>
        /// Bitboard of each piece. Dimension: ChessColor, BitPieceType.
        /// </summary>
        public Bitboard[,] Bitboard_Pieces { get; set; }
        
        /// <summary>
        /// Bitboard of the pieces of one color. Dimendion: ChessColor.
        /// </summary>
        public Bitboard[] Bitboard_ColoredPieces { get; set; }

        /// <summary>
        /// All pieces in one bitboard.
        /// </summary>
        public Bitboard Bitboard_AllPieces;

        
        /// <summary>
        /// Side to move.
        /// true = white, false = black.
        /// </summary>
        public bool Side { get; private set; }

        /// <summary>
        /// Other side.
        /// </summary>
        public bool XSide => !Side;

        public IBitBoardState BoardState { get ; }

        public Bitboards(IHashtable hashtable)
        {
            _hashtable = hashtable;
            _fenParser = new FenParser();
            _moveExecutor = new BitMoveExecutor();
            BoardState = new BitBoardState();

            Bitboard_Pieces = new Bitboard[2, 7]; // todo: what is the 7th son of a seventh son?
            BoardAllPieces = new BitPieceType[64];
            BoardColor = new ChessColor[64];
            Bitboard_ColoredPieces = new Bitboard[2];

            ClearAllPieces();
        }

        public Bitboards(IHashtable hashtable, IBitBoardState state) : this(hashtable)
        {
            BoardState = state;
        }

        public string GetPositionString
        {
            get
            {
                string positionString = "";
                var row = 7;
                var col = 0;

                for (int i = 0; i < 64; i++)
                {
                    var square = (Square)(col + 8 * row);
                    var piece = GetPiece(square);
                    positionString += BitHelper.GetSymbol(piece.Color, piece.Piece);

                    col++;
                    if (col >= 8)
                    {
                        row--;
                        col = 0;
                    }
                }

                return positionString;
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
                    boardString += " " + BitHelper.GetSymbol(piece.Color, piece.Piece) + " ";

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

        public void ClearAllPieces()
        {
            for (var i = 0; i < 64; i++)
            {
                BoardAllPieces[i] = BitPieceType.Empty;
                BoardColor[i] = ChessColor.Empty;
            }

            for (var color = 0; color < 2; color++)
            {
                for (var pieceType = 0; pieceType < 7; pieceType++)
                {
                    Bitboard_Pieces[color, pieceType] = 0;
                }

                Bitboard_ColoredPieces[color] = 0;
            }

            Bitboard_AllPieces = 0;
        }

        public static string PrintBitboard(Bitboard bitboard)
        {
            var board = new StringBuilder();
            for (int y = 7; y >=0; y--)
            {
                for (int x = 0; x<= 7; x++)
                {
                    board.Append(BitHelper.GetBit(bitboard, x + 8 * y) ? " 1" : " .");
                }

                board.Append("\n");
            }

            return board.ToString();
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

            BoardState.SetState(
                enpassantSquare,
                positionInfo.CastlingRightWhiteQueenSide,
                positionInfo.CastlingRightWhiteKingSide,
                positionInfo.CastlingRightBlackQueenSide,
                positionInfo.CastlingRightBlackKingSide,
                positionInfo.SideToMove == ChessColor.White ? ChessColor.White : ChessColor.Black);

            return string.Empty;
        }

        public string GetFenString()
        {
            throw new NotImplementedException();
        }

        public void SetInitialPosition()
        {
            var fenStartPosition = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1";
            SetFenPosition(fenStartPosition);
        }

        public void SetPosition(string position)
        {
            BoardState.SetState(Square.NoSquare, true, true, true, true, ChessColor.White);
            ClearAllPieces();
            var row = 7;
            var col = 0;

            for (int i = 0; i < 64; i++)
            {
                var square = (Square)(col + 8 * row);

                var bitPiece = BitHelper.GetBitPiece(position[i]);
                if (bitPiece.Color != ChessColor.Empty && bitPiece.Piece != BitPieceType.Empty)
                {
                    SetPiece(bitPiece.Color, bitPiece.Piece, square);
                }

                col++;
                if (col >= 8)
                {
                    row--;
                    col = 0;
                }
            }
        }

        public BitPiece GetPiece(Square square)
        {
            return new BitPiece(BoardColor[(int)square], BoardAllPieces[(int)square]);
        }

        public void SetPiece(ChessColor color, BitPieceType piece, Square square)
        {
            BoardAllPieces[(int)square] = piece;
            BoardColor[(int)square] = color;
            BitHelper.SetBit(ref Bitboard_Pieces[(int)color, (int)piece], (int)square);
            BitHelper.SetBit(ref Bitboard_AllPieces, (int)square); // todo test this
            BitHelper.SetBit(ref Bitboard_ColoredPieces[(int) color], (int)square); // todo test this

            _hashtable.AddKey(color, piece, square);

        }

        public void RemovePiece(Square square)
        {
            var piece = BoardAllPieces[(int)square];
            var color = BoardColor[(int)square];
            BitHelper.ClearBit(ref Bitboard_Pieces[(int)color, (int)piece], (int)square);
            BitHelper.ClearBit(ref Bitboard_AllPieces, (int)square); // todo test this
            BitHelper.ClearBit(ref Bitboard_ColoredPieces[(int)color], (int)square); // todo test this

            BoardAllPieces[(int)square] = BitPieceType.Empty;
            BoardColor[(int)square] = ChessColor.Empty;

            _hashtable.AddKey(color, piece, square);
        }

        public void Move(BitMove nextMove)
        {
            _moveExecutor.DoMove(nextMove, this);
        }

        public void Back()
        {
            _moveExecutor.UndoMove(BoardState.LastMove, this);
        }
    }
}
