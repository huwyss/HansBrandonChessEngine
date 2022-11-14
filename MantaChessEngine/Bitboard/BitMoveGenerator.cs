using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MantaChessEngine.Definitions;
using Bitboard = System.UInt64;

namespace MantaChessEngine
{
    public class BitMoveGenerator
    {
        private readonly Bitboards _bitboards;
        private List<BitMove> _moves;
        private List<BitMove> _captures;

        public BitMoveGenerator(Bitboards bitboards)
        {
            _bitboards = bitboards;
        }

        public IEnumerable<BitMove> GetAllMoves(ChessColor color, bool includeCastling = true, bool includePawnMoves = true)
        {
            _moves = new List<BitMove>();
            _captures = new List<BitMove>();

            if (color == ChessColor.White)
            {
                GeneratePawnMoves(_bitboards.Bitboard_WhitePawn, color);
            }

            ////GenerateEnpassant();
            ////GenerateCastling();
            ////GenerateKnightMoves();
            ////GenerateSlidingMoves();
            ////GenerateKingMoves();
            return _captures.Concat(_moves);
        }

        private void GeneratePawnMoves(Bitboard pawnBitboard, ChessColor color)
        {
            Bitboard pawnCapturesToLeft;
            Bitboard pawnCapturesToRight;
            Bitboard pawnMoveStraight;

            if (color == ChessColor.White)
            {
                pawnCapturesToLeft = _bitboards.Bitboard_WhitePawn & (_bitboards.Bitboard_BlackAllPieces & _bitboards.Not_H_file) >> 7;
                pawnCapturesToRight = _bitboards.Bitboard_WhitePawn & (_bitboards.Bitboard_BlackAllPieces & _bitboards.Not_A_file) >> 9;
                pawnMoveStraight = _bitboards.Bitboard_WhitePawn & (~_bitboards.Bitboard_AllPieces) >> 8;
            }
            else
            {
                pawnCapturesToLeft = _bitboards.Bitboard_BlackPawn & (_bitboards.Bitboard_WhiteAllPieces & _bitboards.Not_H_file) << 9;
                pawnCapturesToRight = _bitboards.Bitboard_BlackPawn & (_bitboards.Bitboard_WhiteAllPieces & _bitboards.Not_A_file) << 7;
                pawnMoveStraight = _bitboards.Bitboard_BlackPawn & (~_bitboards.Bitboard_AllPieces) << 8;
            }

            while(color == ChessColor.White && pawnCapturesToLeft != 0)
            {
                var fromSquareMovingPawn = _bitboards.BitScanForward(pawnCapturesToLeft);
                pawnCapturesToLeft &= _bitboards.NotIndexMask[fromSquareMovingPawn];
                var toSquare = _bitboards.WhitePawnLeft[fromSquareMovingPawn]; // todo not white but index
                AddCapture(Const.Pawn, (byte)fromSquareMovingPawn, (byte)toSquare, Const.Empty, 0); // empty ?, value ?
            }
        }

        private void AddMove(byte movingPiece, byte fromSquare, byte toSquare, byte promotionPiece, byte value)
        {
            _moves.Add(new BitMove(movingPiece, fromSquare, toSquare, Const.Empty, Const.Empty, promotionPiece, value));
        }

        private void AddCapture(byte movingPiece, byte fromSquare, byte toSquare, byte capturedPiece, byte value)
        {
            _captures.Add(new BitMove(movingPiece, fromSquare, toSquare, capturedPiece, toSquare, Const.Empty, value));
        }

        private void AddEnpassant()
        {
            // zusätzlich: enpassant capture square
        }

        private void AddCastling()
        {
            // wie castling ?
        }


        public IEnumerable<BitMove> GetAllCaptures(IBoard board, ChessColor color)
        {
            return null;
        }

        public IEnumerable<IMove> GetLegalMoves(IBoard board, ChessColor color)
        {
            throw new NotImplementedException();
        }

        public bool IsAttacked(IBoard board, ChessColor color, int file, int rank)
        {
            throw new NotImplementedException();
        }

        public bool IsCheck(IBoard board, ChessColor color)
        {
            throw new NotImplementedException();
        }

        public bool IsMoveValid(IBoard board, IMove move)
        {
            throw new NotImplementedException();
        }

        //////////////////////////////////////////////////////
        // Testing interface
        //////////////////////////////////////////////////////

        internal IEnumerable<BitMove> GetPawnMoves(Bitboards board, ChessColor color)
        {
            _moves = new List<BitMove>();
            _captures = new List<BitMove>();

            if (color == ChessColor.White)
            {
                GeneratePawnMoves(board.Bitboard_WhitePawn, color);
            }

            return _moves;
        }

    }
}
