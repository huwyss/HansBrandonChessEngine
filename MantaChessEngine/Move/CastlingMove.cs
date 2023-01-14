using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MantaCommon;

namespace MantaChessEngine
{
    public enum CastlingType
    {
        WhiteKingSide,
        WhiteQueenSide,
        BlackKingSide,
        BlackQueenSide
    }

    public class CastlingMove : MoveBase
    {
        private CastlingType _castlingType;

        Square _rookOriginal = Square.NoSquare;
        Square _rookCastled = Square.NoSquare;

        public CastlingMove(CastlingType castlingType, Piece king)
            : base(null, Square.NoSquare, Square.NoSquare, null)
        {
            _castlingType = castlingType;
            switch (castlingType)
            {
                case CastlingType.WhiteKingSide:
                    MovingPiece = king;
                    FromSquare = Square.E1;
                    ToSquare = Square.G1;
                    break;
                case CastlingType.WhiteQueenSide:
                    MovingPiece = king;
                    FromSquare = Square.E1;
                    ToSquare = Square.C1;
                    break;
                case CastlingType.BlackKingSide:
                    MovingPiece = king;
                    FromSquare = Square.E8;
                    ToSquare = Square.G8;
                    break;
                case CastlingType.BlackQueenSide:
                    MovingPiece = king;
                    FromSquare = Square.E8;
                    ToSquare = Square.C8;
                    break;
            }
        }

        public override void ExecuteMove(IBoard board)
        {
            switch (_castlingType)
            {
                case CastlingType.WhiteKingSide:
                    SetWhiteKingSideRook();
                    board.BoardState.WhiteDidCastling = true;
                    break;
                case CastlingType.WhiteQueenSide:
                    SetWhiteQueenSideRook();
                    board.BoardState.WhiteDidCastling = true;
                    break;
                case CastlingType.BlackKingSide:
                    SetBlackKingSideRook();
                    board.BoardState.BlackDidCastling = true;
                    break;
                case CastlingType.BlackQueenSide:
                    SetBlackQueenSideRook();
                    board.BoardState.BlackDidCastling = true;
                    break;
            }

            var rookPiece = board.GetPiece(_rookOriginal);
            board.SetPiece(rookPiece, _rookCastled); // move rook next to king
            board.SetPiece(null, _rookOriginal); // remove old rook

            base.ExecuteMove(board);
        }

        public override void UndoMove(IBoard board)
        {
            switch (_castlingType)
            {
                case CastlingType.WhiteKingSide:
                    SetWhiteKingSideRook();
                    board.BoardState.WhiteDidCastling = false;
                    break;
                case CastlingType.WhiteQueenSide:
                    SetWhiteQueenSideRook();
                    board.BoardState.WhiteDidCastling = false;
                    break;
                case CastlingType.BlackKingSide:
                    SetBlackKingSideRook();
                    board.BoardState.BlackDidCastling = false;
                    break;
                case CastlingType.BlackQueenSide:
                    SetBlackQueenSideRook();
                    board.BoardState.BlackDidCastling = false;
                    break;
            }

            var rookPiece = board.GetPiece(_rookCastled);
            board.SetPiece(rookPiece, _rookOriginal); // move rook next to king
            board.SetPiece(null, _rookCastled); // remove old rook

            base.UndoMove(board);
        }

        private void SetWhiteKingSideRook()
        {
            _rookOriginal = Square.H1;
            _rookCastled = Square.F1;
        }

        private void SetWhiteQueenSideRook()
        {
            _rookOriginal = Square.A1;
            _rookCastled = Square.D1;
        }

        private void SetBlackKingSideRook()
        {
            _rookOriginal = Square.H8;
            _rookCastled = Square.F8;
        }

        private void SetBlackQueenSideRook()
        {
            _rookOriginal = Square.A8;
            _rookCastled = Square.D8;
        }

        public override bool Equals(System.Object obj)
        {
            if (!(obj is CastlingMove))
            {
                return false;
            }

            return base.Equals(obj);
        }

        public override string ToString()
        {
            if (_castlingType == CastlingType.WhiteKingSide ||
                _castlingType == CastlingType.BlackKingSide)
            {
                return "0-0";
            }
            else
            {
                return "0-0-0";
            }
        }

        public override string ToPrintString()
        {
            return ToString();
        }
    }
}
