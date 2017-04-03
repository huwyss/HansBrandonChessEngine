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

        public CastlingMove(char movingPiece, int sourceFile, int sourceRank, int targetFile, int targetRank, CastlingType castlingType)
            : base(movingPiece, sourceFile, sourceRank, targetFile, targetRank, Definitions.EmptyField)
        {
            _castlingType = castlingType;
        }

        //public CastlingMove(CastlingType castlingType)
        //    : base((char)0, 0, 0, 0, 0, (char)0)
        //{
        //    _castlingType = castlingType;
        //    switch (castlingType)
        //    {
        //        case CastlingType.WhiteKingSide:
        //            MovingPiece = 'K';
        //            SourceFile = Helper.FileCharToFile('e');
        //            SourceRank = 1;
        //            TargetFile = Helper.FileCharToFile('g');
        //            TargetRank = 1;
        //            break;
        //        case CastlingType.WhiteQueenSide:
        //            MovingPiece = 'K';
        //            SourceFile = Helper.FileCharToFile('e');
        //            SourceRank = 1;
        //            TargetFile = Helper.FileCharToFile('c');
        //            TargetRank = 1;
        //            break;
        //        case CastlingType.BlackKingSide:
        //            MovingPiece = 'K';ddd
        //            SourceFile = Helper.FileCharToFile('e');
        //            SourceRank = 1;
        //            TargetFile = Helper.FileCharToFile('g');
        //            TargetRank = 1;
        //            break;
        //    }
        //}


        public override void ExecuteMove(Board board)
        {
            if (_castlingType == CastlingType.WhiteKingSide)
            {
                SetWhiteKingSideRook();
                board.WhiteDidCastling = true;
            }

            if (_castlingType == CastlingType.WhiteQueenSide)
            {
                SetWhiteQueenSideRook();
                board.WhiteDidCastling = true;
            }

            if (_castlingType == CastlingType.BlackKingSide)
            {
                SetBlackKingSideRook();
                board.BlackDidCastling = true;
            }

            if (_castlingType == CastlingType.BlackQueenSide)
            {
                SetBlackQueenSideRook();
                board.BlackDidCastling = true;
            }

            board.SetPiece(_rook, _rookCastledFile, _rookCastledRank); // move rook next to king
            board.SetPiece(Definitions.EmptyField, _rookOriginalFile, _rookOriginalRank); // remove old rook

            base.ExecuteMove(board);
        }

        public override void UndoMove(Board board)
        {
            if (_castlingType == CastlingType.WhiteKingSide)
            {
                SetWhiteKingSideRook();
                board.WhiteDidCastling = false;
            }

            if (_castlingType == CastlingType.WhiteQueenSide)
            {
                SetWhiteQueenSideRook();
                board.WhiteDidCastling = false;
            }

            if (_castlingType == CastlingType.BlackKingSide)
            {
                SetBlackKingSideRook();
                board.BlackDidCastling = false;
            }

            if (_castlingType == CastlingType.BlackQueenSide)
            {
                SetBlackQueenSideRook();
                board.BlackDidCastling = false;
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
