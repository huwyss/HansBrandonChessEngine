using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MantaChessEngine.Doubles
{
    public class FakeBoard : IBoard
    {
        public Definitions.ChessColor SideToMove { get; set; }
        public History History { get; set; }
        public IMove LastMove { get; }
        public int EnPassantFile { get; }
        public int EnPassantRank { get; }
        public bool CastlingRightWhiteQueenSide { get; }
        public bool CastlingRightWhiteKingSide { get; }
        public bool CastlingRightBlackQueenSide { get; }
        public bool CastlingRightBlackKingSide { get; }
        public bool WhiteDidCastling { get; set; }
        public bool BlackDidCastling { get; set; }
        public string GetString { get; }
        public string GetPrintString { get; }
        public void SetInitialPosition()
        {
        }

        public void SetPosition(string position)
        {
        }

        public Piece GetPiece(int file, int rank)
        {
            return null;
        }

        public Piece GetPiece(char fileChar, int rank)
        {
            return null;
        }

        public void SetPiece(Piece piece, int file, int rank)
        {
        }

        public void SetPiece(Piece piece, char fileChar, int rank)
        {
        }

        public void Move(IMove nextMove)
        {
        }

        public void Back()
        {
        }

        public void RedoMove()
        {
        }

        public Definitions.ChessColor GetColor(int file, int rank)
        {
            return Definitions.ChessColor.White;
        }

        public bool IsWinner(Definitions.ChessColor color)
        {
            return false;
        }

        
    }
}
