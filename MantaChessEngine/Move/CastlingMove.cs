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

        char _rook = (char)0;
        int _rookOriginalFile = 0;
        int _rookOriginalRank = 0;
        int _rookCastledFile = 0;
        int _rookCastledRank = 0;

        public CastlingMove(CastlingType castlingType)
            : base((char)0, 0, 0, 0, 0, Definitions.EmptyField)
        {
            _castlingType = castlingType;
            switch (castlingType)
            {
                case CastlingType.WhiteKingSide:
                    MovingPiece = Definitions.KING.ToString().ToUpper()[0];
                    SourceFile = Helper.FileCharToFile('e');
                    SourceRank = 1;
                    TargetFile = Helper.FileCharToFile('g');
                    TargetRank = 1;
                    break;
                case CastlingType.WhiteQueenSide:
                    MovingPiece = Definitions.KING.ToString().ToUpper()[0];
                    SourceFile = Helper.FileCharToFile('e');
                    SourceRank = 1;
                    TargetFile = Helper.FileCharToFile('c');
                    TargetRank = 1;
                    break;
                case CastlingType.BlackKingSide:
                    MovingPiece = Definitions.KING.ToString().ToLower()[0];
                    SourceFile = Helper.FileCharToFile('e');
                    SourceRank = 8;
                    TargetFile = Helper.FileCharToFile('g');
                    TargetRank = 8;
                    break;
                case CastlingType.BlackQueenSide:
                    MovingPiece = Definitions.KING.ToString().ToLower()[0];
                    SourceFile = Helper.FileCharToFile('e');
                    SourceRank = 8;
                    TargetFile = Helper.FileCharToFile('c');
                    TargetRank = 8;
                    break;
            }
        }

        public override void ExecuteMove(Board board)
        {
            switch (_castlingType)
            {
                case CastlingType.WhiteKingSide:
                    SetWhiteKingSideRook();
                    board.WhiteDidCastling = true;
                    break;
                case CastlingType.WhiteQueenSide:
                    SetWhiteQueenSideRook();
                    board.WhiteDidCastling = true;
                    break;
                case CastlingType.BlackKingSide:
                    SetBlackKingSideRook();
                    board.BlackDidCastling = true;
                    break;
                case CastlingType.BlackQueenSide:
                    SetBlackQueenSideRook();
                    board.BlackDidCastling = true;
                    break;
            }

            board.SetPiece(_rook, _rookCastledFile, _rookCastledRank); // move rook next to king
            board.SetPiece(Definitions.EmptyField, _rookOriginalFile, _rookOriginalRank); // remove old rook

            base.ExecuteMove(board);
        }

        public override void UndoMove(Board board)
        {
            switch (_castlingType)
            {
                case CastlingType.WhiteKingSide:
                    SetWhiteKingSideRook();
                    board.WhiteDidCastling = false;
                    break;
                case CastlingType.WhiteQueenSide:
                    SetWhiteQueenSideRook();
                    board.WhiteDidCastling = false;
                    break;
                case CastlingType.BlackKingSide:
                    SetBlackKingSideRook();
                    board.BlackDidCastling = false;
                    break;
                case CastlingType.BlackQueenSide:
                    SetBlackQueenSideRook();
                    board.BlackDidCastling = false;
                    break;
            }

            board.SetPiece(_rook, _rookOriginalFile, _rookOriginalRank); // move rook next to king
            board.SetPiece(Definitions.EmptyField, _rookCastledFile, _rookCastledRank); // remove old rook

            base.UndoMove(board);
        }

        private void SetWhiteKingSideRook()
        {
            _rook = Definitions.ROOK.ToString().ToUpper()[0];
            _rookOriginalFile = Helper.FileCharToFile('h');
            _rookOriginalRank = 1;
            _rookCastledFile = Helper.FileCharToFile('f');
            _rookCastledRank = 1;
        }

        private void SetWhiteQueenSideRook()
        {
            _rook = Definitions.ROOK.ToString().ToUpper()[0];
            _rookOriginalFile = Helper.FileCharToFile('a');
            _rookOriginalRank = 1;
            _rookCastledFile = Helper.FileCharToFile('d');
            _rookCastledRank = 1;
        }

        private void SetBlackKingSideRook()
        {
            _rook = Definitions.ROOK.ToString().ToLower()[0];
            _rookOriginalFile = Helper.FileCharToFile('h');
            _rookOriginalRank = 8;
            _rookCastledFile = Helper.FileCharToFile('f');
            _rookCastledRank = 8;
        }

        private void SetBlackQueenSideRook()
        {
            _rook = Definitions.ROOK.ToString().ToLower()[0];
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
    }
}
