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
        string initPosition = "RNBQKBNR" + // zeile 1 weiss
                              "PPPPPPPP" +
                              "        " +
                              "        " +
                              "        " +
                              "        " +
                              "pppppppp" +
                              "rnbqkbnr"; // zeile 8 schwarz

        Definitions.ChessColor SideToMove { get; set; }
        int EnPassantField { get; set; }
        bool CastlingRightFirstMover { get; set; }
        bool CastlingRightSecondMover { get; set; }

        /// <summary>
        /// Constructor. Board is empty.
        /// </summary>
        public Board()
        {
            _board = new char[64];
            for (int i = 0; i < 64; i++)
            {
                _board[i] = ' ';
            }

            SideToMove = Definitions.ChessColor.White;
            EnPassantField = 0;
            CastlingRightFirstMover = true;
            CastlingRightSecondMover = true;
        }

        /// <summary>
        /// Sets the initial chess position.
        /// </summary>
        public void  SetInitialPosition()
        {
            for (int i = 0; i < 64; i++)
            {
                _board[i] = initPosition[i];
            }

            SideToMove = Definitions.ChessColor.White;
            EnPassantField = 0;
            CastlingRightFirstMover = true;
            CastlingRightSecondMover = true;
        }

        /// <summary>
        /// Returns the chess piece.
        /// </summary>
        /// <param name="file">1 to 8</param>
        /// <param name="rank">1 to 8</param>
        /// <returns></returns>
        public char GetPiece(int file, int rank)
        {
            return _board[8 * (rank - 1) + file - 1];
        }

        /// <summary>
        /// Returns the chess piece.
        /// </summary>
        /// <param name="fileChar">'a' to 'h'</param>
        /// <param name="rank">1 to 8</param>
        /// <returns></returns>
        public char GetPiece(char fileChar, int rank)
        {
            return GetPiece(fileChar - 'a' + 1, rank);
        }

        /// <summary>
        /// Sets a chess piece to the field.
        /// </summary>
        /// <param name="piece">chess piece: p-pawn, k-king, q-queen, r-rook, n-knight, b-bishop, r-rook, ' '-empty. small=black, capital=white</param>
        /// <param name="file">1 to 8</param>
        /// <param name="rank">1 to 8</param>
        public void SetPiece(char piece, int file, int rank)
        {
            _board[8 * (rank - 1) + file - 1] = piece;
        }

        /// <summary>
        /// Sets a chess piece to the field.
        /// </summary>
        /// <param name="piece">chess piece: p-pawn, k-king, q-queen, r-rook, n-knight, b-bishop, r-rook, ' '-empty. small=black, capital=white</param>
        /// <param name="file">'a' to 'h'</param>
        /// <param name="rank">1 to 8</param>
        public void SetPiece(char piece, char fileChar, int rank)
        {
            SetPiece(piece, fileChar - 'a' + 1, rank);
        }

        public void Move(char sourceFileChar, int sourceRank, char targetFileChar, int targetRank)
        {
            Move(sourceFileChar - 'a' + 1, sourceRank, targetFileChar - 'a' + 1, targetRank);
        }

        public void Move(int sourceFile, int sourceRank, int targetFile, int targetRank)
        {
            char pieceToMove = GetPiece(sourceFile, sourceRank);
            if (GetColor(targetFile, targetRank) == Definitions.ChessColor.Empty)
            {
                SetPiece(pieceToMove, targetFile, targetRank);
                SetPiece(' ', sourceFile, sourceRank);
            }
            else if (GetColor(targetFile, targetRank) == Definitions.ChessColor.Black)
            {
                SetPiece(pieceToMove, targetFile, targetRank);
                SetPiece(' ', sourceFile, sourceRank);
                // todo store move and captured piece
            }
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


    }
}
