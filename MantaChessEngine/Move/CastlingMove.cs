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

    public class CastlingMove : MoveBase
    {
        private CastlingType _castlingType;

        int _rookOriginalFile = 0;
        int _rookOriginalRank = 0;
        int _rookCastledFile = 0;
        int _rookCastledRank = 0;

        public CastlingMove(CastlingType castlingType, Piece king)
            : base(null, 0, 0, 0, 0, null)
        {
            _castlingType = castlingType;
            switch (castlingType)
            {
                case CastlingType.WhiteKingSide:
                    MovingPiece = king;
                    SourceFile = Helper.FileCharToFile('e');
                    SourceRank = 1;
                    TargetFile = Helper.FileCharToFile('g');
                    TargetRank = 1;
                    break;
                case CastlingType.WhiteQueenSide:
                    MovingPiece = king;
                    SourceFile = Helper.FileCharToFile('e');
                    SourceRank = 1;
                    TargetFile = Helper.FileCharToFile('c');
                    TargetRank = 1;
                    break;
                case CastlingType.BlackKingSide:
                    MovingPiece = king;
                    SourceFile = Helper.FileCharToFile('e');
                    SourceRank = 8;
                    TargetFile = Helper.FileCharToFile('g');
                    TargetRank = 8;
                    break;
                case CastlingType.BlackQueenSide:
                    MovingPiece = king;
                    SourceFile = Helper.FileCharToFile('e');
                    SourceRank = 8;
                    TargetFile = Helper.FileCharToFile('c');
                    TargetRank = 8;
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

            var rookPiece = board.GetPiece(_rookOriginalFile, _rookOriginalRank);
            board.SetPiece(rookPiece, _rookCastledFile, _rookCastledRank); // move rook next to king
            board.SetPiece(null, _rookOriginalFile, _rookOriginalRank); // remove old rook

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

            var rookPiece = board.GetPiece(_rookCastledFile, _rookCastledRank);
            board.SetPiece(rookPiece, _rookOriginalFile, _rookOriginalRank); // move rook next to king
            board.SetPiece(null, _rookCastledFile, _rookCastledRank); // remove old rook

            base.UndoMove(board);
        }

        private void SetWhiteKingSideRook()
        {
            _rookOriginalFile = Helper.FileCharToFile('h');
            _rookOriginalRank = 1;
            _rookCastledFile = Helper.FileCharToFile('f');
            _rookCastledRank = 1;
        }

        private void SetWhiteQueenSideRook()
        {
            _rookOriginalFile = Helper.FileCharToFile('a');
            _rookOriginalRank = 1;
            _rookCastledFile = Helper.FileCharToFile('d');
            _rookCastledRank = 1;
        }

        private void SetBlackKingSideRook()
        {
            _rookOriginalFile = Helper.FileCharToFile('h');
            _rookOriginalRank = 8;
            _rookCastledFile = Helper.FileCharToFile('f');
            _rookCastledRank = 8;
        }

        private void SetBlackQueenSideRook()
        {
            _rookOriginalFile = Helper.FileCharToFile('a');
            _rookOriginalRank = 8;
            _rookCastledFile = Helper.FileCharToFile('d');
            _rookCastledRank = 8;
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
