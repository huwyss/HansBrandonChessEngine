using System;
using System.Collections.Generic;
using System.Linq;
using Bitboard = System.UInt64;

namespace MantaChessEngine.BitboardEngine
{
    public class BitMoveGenerator
    {
        private readonly Bitboards _bitboards;
        private List<BitMove> _moves;
        private List<BitMove> _captures;

        public BitMoveGenerator(Bitboards bitboards)
        {
            _bitboards = bitboards;
            _moves = new List<BitMove>();
            _captures = new List<BitMove>();
        }

        private void ClearLists()
        {
            _moves.Clear();
            _captures.Clear();
        }

        public IEnumerable<BitMove> GetAllMoves(BitColor color, bool includeCastling = true, bool includePawnMoves = true)
        {
            ClearLists();

            GeneratePawnMoves(color);
            GenerateKnightMoves(color);
            GenerateSlidingMoves(color);
            GenerateKingMoves(color);

            GenerateEnpassant(color);
            ////GenerateCastlingcolor);

            return _captures.Concat(_moves);
        }

        private void GeneratePawnMoves(BitColor color)
        {
            Bitboard pawnCapturingToLeft;
            Bitboard pawnCapturingToRight;
            Bitboard pawnMoveStraight;

            Bitboard pawnBitboard = _bitboards.Bitboard_Pieces[(int)color, (int)BitPieceType.Pawn];

            if (color == BitColor.White)
            {
                pawnCapturingToLeft = pawnBitboard & ((_bitboards.Bitboard_ColoredPieces[(int)BitColor.Black] & _bitboards.Not_H_file) >> 7);
                pawnCapturingToRight = pawnBitboard & ((_bitboards.Bitboard_ColoredPieces[(int)BitColor.Black] & _bitboards.Not_A_file) >> 9);
                pawnMoveStraight = pawnBitboard & ~(_bitboards.Bitboard_AllPieces >> 8);
            }
            else
            {
                pawnCapturingToLeft = pawnBitboard & (_bitboards.Bitboard_ColoredPieces[(int)BitColor.White] & _bitboards.Not_H_file) << 9;
                pawnCapturingToRight = pawnBitboard & (_bitboards.Bitboard_ColoredPieces[(int)BitColor.White] & _bitboards.Not_A_file) << 7;
                pawnMoveStraight = pawnBitboard & ~(_bitboards.Bitboard_AllPieces << 8);
            }

            while(pawnCapturingToLeft != 0)
            {
                var fromSquareMovingPawn = _bitboards.BitScanForward(pawnCapturingToLeft);
                pawnCapturingToLeft &= _bitboards.NotIndexMask[fromSquareMovingPawn];
                var toSquare = _bitboards.PawnLeft[(int)color, fromSquareMovingPawn];
                var capturedPiece = _bitboards.BoardAllPieces[(int)toSquare];
                AddCapture(BitPieceType.Pawn, (Square)fromSquareMovingPawn, toSquare, capturedPiece, toSquare, BitPieceType.Empty, 0); // empty ?, value ?
            }

            while (pawnCapturingToRight != 0)
            {
                var fromSquareMovingPawn = _bitboards.BitScanForward(pawnCapturingToRight);
                pawnCapturingToRight &= _bitboards.NotIndexMask[fromSquareMovingPawn];
                var toSquare = _bitboards.PawnRight[(int)color, fromSquareMovingPawn];
                var capturedPiece = _bitboards.BoardAllPieces[(int)toSquare];
                AddCapture(BitPieceType.Pawn, (Square)fromSquareMovingPawn, toSquare, capturedPiece, toSquare, BitPieceType.Empty, 0); // empty ?, value ?
            }

            while (pawnMoveStraight != 0)
            {
                var fromSquareMovingPawn = _bitboards.BitScanForward(pawnMoveStraight);
                pawnMoveStraight &= _bitboards.NotIndexMask[fromSquareMovingPawn];
                var toSquare = _bitboards.PawnStep[(int)color, fromSquareMovingPawn];
                AddMove(BitPieceType.Pawn, (Square)fromSquareMovingPawn, (Square)toSquare, BitPieceType.Empty, 0); // value ?

                if (_bitboards.Rank[(int)color, fromSquareMovingPawn] == 1 &&
                    _bitboards.BoardAllPieces[_bitboards.PawnDoubleStep[(int)color, fromSquareMovingPawn]] == BitPieceType.Empty)
                {
                    AddMove(BitPieceType.Pawn, (Square)fromSquareMovingPawn, (Square)_bitboards.PawnDoubleStep[(int)color, fromSquareMovingPawn], BitPieceType.Empty, 0);
                }
            }
        }

        private void GenerateKnightMoves(BitColor color)
        {
            Bitboard knightBitboard = _bitboards.Bitboard_Pieces[(int)color, (int)BitPieceType.Knight];

            while (knightBitboard != 0)
            {
                var fromSquareMovingKnight = _bitboards.BitScanForward(knightBitboard);
                knightBitboard &= _bitboards.NotIndexMask[fromSquareMovingKnight];

                var knightCaptures = _bitboards.MovesPieces[(int)BitPieceType.Knight, fromSquareMovingKnight] & _bitboards.Bitboard_ColoredPieces[(int)BitHelper.OtherColor(color)];
                while (knightCaptures != 0)
                {
                    var toSquare = _bitboards.BitScanForward(knightCaptures);
                    knightCaptures &= _bitboards.NotIndexMask[toSquare];
                    AddCapture(BitPieceType.Knight, (Square)fromSquareMovingKnight, (Square)toSquare, _bitboards.BoardAllPieces[(int)toSquare] , (Square)toSquare, BitPieceType.Empty, 0);
                }

                var knightMoves = _bitboards.MovesPieces[(int)BitPieceType.Knight, fromSquareMovingKnight] & ~_bitboards.Bitboard_AllPieces;
                while(knightMoves != 0)
                {
                    var toSquare = _bitboards.BitScanForward(knightMoves);
                    knightMoves &= _bitboards.NotIndexMask[toSquare];
                    AddMove(BitPieceType.Knight, (Square)fromSquareMovingKnight, (Square)toSquare, BitPieceType.Empty, 0);
                }
            }
        }

        private void GenerateSlidingMoves(BitColor color)
        {
            for (BitPieceType piece = BitPieceType.Bishop; piece <= BitPieceType.Queen; piece++)
            {
                GenerateSlidingPieceMoves(color, piece);
            }
        }

        private void GenerateSlidingPieceMoves(BitColor color, BitPieceType piece)
        {
            Bitboard pieceBitboard = _bitboards.Bitboard_Pieces[(int)color, (int)piece];

            while (pieceBitboard != 0)
            {
                var fromSquareMovingPiece = _bitboards.BitScanForward(pieceBitboard);
                pieceBitboard &= _bitboards.NotIndexMask[fromSquareMovingPiece];

                var bishopCaptures = _bitboards.MovesPieces[(int)piece, fromSquareMovingPiece] & _bitboards.Bitboard_ColoredPieces[(int)BitHelper.OtherColor(color)];
                while (bishopCaptures != 0)
                {
                    var toSquare = _bitboards.BitScanForward(bishopCaptures);
                    bishopCaptures &= _bitboards.NotIndexMask[toSquare];
                    if ((_bitboards.BetweenMatrix[(int)fromSquareMovingPiece, (int)toSquare] & _bitboards.Bitboard_AllPieces) == 0)
                    {
                        AddCapture(piece, (Square)fromSquareMovingPiece, (Square)toSquare, _bitboards.BoardAllPieces[(int)toSquare], (Square)toSquare, BitPieceType.Empty, 0);
                    }
                }

                var bishopMoves = _bitboards.MovesPieces[(int)piece, fromSquareMovingPiece] & ~_bitboards.Bitboard_AllPieces;
                while (bishopMoves != 0)
                {
                    var toSquare = _bitboards.BitScanForward(bishopMoves);
                    bishopMoves &= _bitboards.NotIndexMask[toSquare];

                    if ((_bitboards.BetweenMatrix[fromSquareMovingPiece, toSquare] & _bitboards.Bitboard_AllPieces) == 0)
                    {
                        AddMove(piece, (Square)fromSquareMovingPiece, (Square)toSquare, BitPieceType.Empty, 0);
                    }
                }
            }
        }

        private void GenerateKingMoves(BitColor color)
        {
            Bitboard kingBitboard = _bitboards.Bitboard_Pieces[(int)color, (int)BitPieceType.King];
            var fromSquareMovingKing = _bitboards.BitScanForward(kingBitboard);

            var kingCaptures = _bitboards.MovesPieces[(int)BitPieceType.King, fromSquareMovingKing] & _bitboards.Bitboard_ColoredPieces[(int)BitHelper.OtherColor(color)];
            while (kingCaptures != 0)
            {
                var toSquare = _bitboards.BitScanForward(kingCaptures);
                kingCaptures &= _bitboards.NotIndexMask[toSquare];
                AddCapture(BitPieceType.King, (Square)fromSquareMovingKing, (Square)toSquare, _bitboards.BoardAllPieces[(int)toSquare], (Square)toSquare, BitPieceType.Empty, 0);
            }

            var kingMoves = _bitboards.MovesPieces[(int)BitPieceType.King, fromSquareMovingKing] & ~_bitboards.Bitboard_AllPieces;

            while (kingMoves != 0)
            {
                var toSquare = _bitboards.BitScanForward(kingMoves);
                kingMoves &= _bitboards.NotIndexMask[toSquare];
                AddMove(BitPieceType.King, (Square)fromSquareMovingKing, (Square)toSquare, BitPieceType.Empty, 0);
            }
        }

        private void GenerateEnpassant(BitColor color)
        {
            var enpassantSquare = _bitboards.BoardState.LastEnPassantSquare;
            var capturedPawnSquare = color == BitColor.White ? enpassantSquare - 8 : enpassantSquare + 8;
            var fromSquareCapturingToLeft = _bitboards.PawnRight[(int)BitHelper.OtherColor(color), (int)enpassantSquare];
            var fromSquareCapturingToRight = _bitboards.PawnLeft[(int)BitHelper.OtherColor(color), (int)enpassantSquare];

            // capture to the left
            if (fromSquareCapturingToLeft != Square.NoSquare && (_bitboards.Bitboard_Pieces[(int)color, (int)BitPieceType.Pawn] & _bitboards.IndexMask[(int)fromSquareCapturingToLeft]) != 0)
            {
                AddCapture(BitPieceType.Pawn, fromSquareCapturingToLeft, enpassantSquare, _bitboards.BoardAllPieces[(int)capturedPawnSquare], capturedPawnSquare, BitPieceType.Empty, 0);
            }

            // capture to the right
            if (fromSquareCapturingToRight != Square.NoSquare && (_bitboards.Bitboard_Pieces[(int)color, (int)BitPieceType.Pawn] & _bitboards.IndexMask[(int)fromSquareCapturingToRight]) != 0)
            {
                AddCapture(BitPieceType.Pawn, fromSquareCapturingToRight, enpassantSquare, _bitboards.BoardAllPieces[(int)capturedPawnSquare], capturedPawnSquare, BitPieceType.Empty, 0);
            }
        }

        private void AddCastling()
        {
            // wie castling ?
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


        public IEnumerable<BitMove> GetAllCaptures(IBitBoard board, BitColor color)
        {
            return null;
        }

        public IEnumerable<BitMove> GetLegalMoves(IBitBoard board, BitColor color)
        {
            throw new NotImplementedException();
        }

        public bool IsAttacked(IBitBoard board, BitColor color, Square square)
        {
            throw new NotImplementedException();
        }

        public bool IsCheck(IBitBoard board, BitColor color)
        {
            throw new NotImplementedException();
        }

        public bool IsMoveValid(IBitBoard board, BitMove move)
        {
            throw new NotImplementedException();
        }

        //////////////////////////////////////////////////////
        // Testing interface
        //////////////////////////////////////////////////////

        internal IEnumerable<BitMove> GetPawnMoves(BitColor color)
        {
            ClearLists();
            GeneratePawnMoves(color);
            return _moves;
        }

        internal IEnumerable<BitMove> GetKnightMoves(BitColor color)
        {
            ClearLists();
            GenerateKnightMoves(color);
            return _moves;
        }

        internal IEnumerable<BitMove> GetBishopMoves(BitColor color)
        {
            ClearLists();
            GenerateSlidingPieceMoves(color, BitPieceType.Bishop);
            return _moves;
        }

        internal IEnumerable<BitMove> GetRookMoves(BitColor color)
        {
            ClearLists();
            GenerateSlidingPieceMoves(color, BitPieceType.Rook);
            return _moves;
        }

        internal IEnumerable<BitMove> GetQueenMoves(BitColor color)
        {
            ClearLists();
            GenerateSlidingPieceMoves(color, BitPieceType.Queen);
            return _moves;
        }

        internal IEnumerable<BitMove> GetKingMoves(BitColor color)
        {
            ClearLists();
            GenerateKingMoves(color);
            return _moves;
        }

        internal IEnumerable<BitMove> GetEnPassant(BitColor color)
        {
            ClearLists();
            GenerateEnpassant(color);
            return _moves;
        }
    }
}
