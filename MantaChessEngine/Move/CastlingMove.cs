using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MantaChessEngine
{
    public enum CastlingType
    {
        WhiteKingSide,
        WhiteQueenSide,
        BlackKingSide,
        BlackQueenSide
    }

    public class CastlingMove : Move
    {
        private CastlingType _castlingType;

        public CastlingMove(char movingPiece, int sourceFile, int sourceRank, int targetFile, int targetRank, CastlingType castlingType)
            : base(movingPiece, sourceFile, sourceRank, targetFile, targetRank, Definitions.EmptyField, false)
        {
            _castlingType = castlingType;
        }

        public override void ExecuteMove(Board board)
        {
            if (_castlingType == CastlingType.WhiteKingSide)
            {
                board.SetPiece(Definitions.ROOK.ToString().ToUpper()[0], Helper.FileCharToFile('f'), 1); // move rook next to king
                board.SetPiece(Definitions.EmptyField, Helper.FileCharToFile('h'), 1); // remove old rook
                board.WhiteDidCastling = true;
            }

            if (_castlingType == CastlingType.WhiteQueenSide)
            {
                board.SetPiece(Definitions.ROOK.ToString().ToUpper()[0], Helper.FileCharToFile('d'), 1); // move rook next to king
                board.SetPiece(Definitions.EmptyField, Helper.FileCharToFile('a'), 1); // remove old rook
                board.WhiteDidCastling = true;
            }

            if (_castlingType == CastlingType.BlackKingSide)
            {
                board.SetPiece(Definitions.ROOK.ToString().ToLower()[0], Helper.FileCharToFile('f'), 8); // move rook next to king
                board.SetPiece(Definitions.EmptyField, Helper.FileCharToFile('h'), 8); // remove old rook
                board.BlackDidCastling = true;
            }

            if (_castlingType == CastlingType.BlackQueenSide)
            {
                board.SetPiece(Definitions.ROOK.ToString().ToLower()[0], Helper.FileCharToFile('d'), 8); // move rook next to king
                board.SetPiece(Definitions.EmptyField, Helper.FileCharToFile('a'), 8); // remove old rook
                board.BlackDidCastling = true;
            }

            base.ExecuteMove(board);
        }

        public override void UndoMove(Board board)
        {
            if (_castlingType == CastlingType.WhiteKingSide)
            {
                board.SetPiece(Definitions.ROOK.ToString().ToUpper()[0], Helper.FileCharToFile('h'), 1); // move rook next to king
                board.SetPiece(Definitions.EmptyField, Helper.FileCharToFile('f'), 1); // remove old rook
                board.WhiteDidCastling = false;
            }

            if (_castlingType == CastlingType.WhiteQueenSide)
            {
                board.SetPiece(Definitions.ROOK.ToString().ToUpper()[0], Helper.FileCharToFile('a'), 1); // move rook next to king
                board.SetPiece(Definitions.EmptyField, Helper.FileCharToFile('d'), 1); // remove old rook
                board.WhiteDidCastling = false;
            }

            if (_castlingType == CastlingType.BlackKingSide)
            {
                board.SetPiece(Definitions.ROOK.ToString().ToLower()[0], Helper.FileCharToFile('h'), 8); // move rook next to king
                board.SetPiece(Definitions.EmptyField, Helper.FileCharToFile('f'), 8); // remove old rook
                board.BlackDidCastling = false;
            }

            if (_castlingType == CastlingType.BlackQueenSide)
            {
                board.SetPiece(Definitions.ROOK.ToString().ToLower()[0], Helper.FileCharToFile('a'), 8); // move rook next to king
                board.SetPiece(Definitions.EmptyField, Helper.FileCharToFile('d'), 8); // remove old rook
                board.BlackDidCastling = false;
            }

            base.UndoMove(board);
        }
    }
}
