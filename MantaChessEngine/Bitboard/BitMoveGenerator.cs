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

        public IEnumerable<BitMove> GetAllMoves(BitColor color, bool includeCastling = true, bool includePawnMoves = true)
        {
            _moves = new List<BitMove>();
            _captures = new List<BitMove>();

            GeneratePawnMoves(_bitboards.Bitboard_Pieces[(int)color, (int)BitPieceType.Pawn], color);

            ////GenerateEnpassant();
            ////GenerateCastling();
            ////GenerateKnightMoves();
            ////GenerateSlidingMoves();
            ////GenerateKingMoves();
            return _captures.Concat(_moves);
        }

        private void GeneratePawnMoves(Bitboard pawnBitboard, BitColor color)
        {
            Bitboard pawnCapturesToLeft;
            Bitboard pawnCapturesToRight;
            Bitboard pawnMoveStraight;

            if (color == BitColor.White)
            {
                pawnCapturesToLeft = pawnBitboard & (_bitboards.Bitboard_BlackAllPieces & _bitboards.Not_H_file) >> 7;
                pawnCapturesToRight = pawnBitboard & (_bitboards.Bitboard_BlackAllPieces & _bitboards.Not_A_file) >> 9;
                pawnMoveStraight = pawnBitboard & (~_bitboards.Bitboard_AllPieces) >> 8;
            }
            else
            {
                pawnCapturesToLeft = pawnBitboard & (_bitboards.Bitboard_WhiteAllPieces & _bitboards.Not_H_file) << 9;
                pawnCapturesToRight = pawnBitboard & (_bitboards.Bitboard_WhiteAllPieces & _bitboards.Not_A_file) << 7;
                pawnMoveStraight = pawnBitboard & (~_bitboards.Bitboard_AllPieces) << 8;
            }

            while(color == BitColor.White && pawnCapturesToLeft != 0)
            {
                var fromSquareMovingPawn = _bitboards.BitScanForward(pawnCapturesToLeft);
                pawnCapturesToLeft &= _bitboards.NotIndexMask[fromSquareMovingPawn];
                var toSquare = _bitboards.WhitePawnLeft[fromSquareMovingPawn]; // todo not white but index
                var capturedPiece = BitPieceType.Rook; // todo: im buch: px[board[n]], n ist toSquare
                AddCapture(BitPieceType.Pawn, (Square)fromSquareMovingPawn, (Square)toSquare, capturedPiece, (Square)toSquare, BitPieceType.Empty, 0); // empty ?, value ?
            }
        }

        private void AddMove(BitPieceType movingPiece, Square fromSquare, Square toSquare, BitPieceType promotionPiece, byte value)
        {
            _moves.Add(new BitMove(movingPiece, fromSquare, toSquare, BitPieceType.Empty, Square.NoSquare, promotionPiece, value));
        }

        private void AddCapture(BitPieceType movingPiece, Square fromSquare, Square toSquare, BitPieceType capturedPiece, Square capturedSquare, BitPieceType promotionPiece, byte value)
        {
            _captures.Add(new BitMove(movingPiece, fromSquare, toSquare, capturedPiece, capturedSquare, promotionPiece, value));
            _moves.Add(new BitMove(movingPiece, fromSquare, toSquare, capturedPiece, capturedSquare, promotionPiece, value));
        }

        private void AddEnpassant()
        {
            // zusätzlich: enpassant capture square
        }

        private void AddCastling()
        {
            // wie castling ?
        }


        public IEnumerable<BitMove> GetAllCaptures(IBoard board, BitColor color)
        {
            return null;
        }

        public IEnumerable<IMove> GetLegalMoves(IBoard board, BitColor color)
        {
            throw new NotImplementedException();
        }

        public bool IsAttacked(IBoard board, BitColor color, int file, int rank)
        {
            throw new NotImplementedException();
        }

        public bool IsCheck(IBoard board, BitColor color)
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

        internal IEnumerable<BitMove> GetPawnMoves(BitColor color)
        {
            _moves = new List<BitMove>();
            _captures = new List<BitMove>();

            GeneratePawnMoves(_bitboards.Bitboard_Pieces[(int)color, (int)BitPieceType.Pawn], color);

            return _moves;
        }

    }
}
