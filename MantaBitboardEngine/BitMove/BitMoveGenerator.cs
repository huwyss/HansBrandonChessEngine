using System;
using System.Collections.Generic;
using System.Linq;
using Bitboard = System.UInt64;
using MantaCommon;

namespace MantaBitboardEngine
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

        public IEnumerable<BitMove> GetAllMoves(ChessColor color, bool includeCastling = true, bool includePawnMoves = true)
        {
            ClearLists();

            GeneratePawnMoves(color);
            GenerateKnightMoves(color);
            GenerateSlidingMoves(color);
            GenerateKingMoves(color);

            GenerateEnpassant(color);
            GenerateCastling(color);

            return _captures.Concat(_moves);
        }

        private void GeneratePawnMoves(ChessColor color)
        {
            Bitboard pawnCapturingToLeft;
            Bitboard pawnCapturingToRight;
            Bitboard pawnMoveStraight;

            Bitboard pawnBitboard = _bitboards.Bitboard_Pieces[(int)color, (int)BitPieceType.Pawn];

            if (color == ChessColor.White)
            {
                pawnCapturingToLeft = pawnBitboard & ((_bitboards.Bitboard_ColoredPieces[(int)ChessColor.Black] & _bitboards.Not_H_file) >> 7);
                pawnCapturingToRight = pawnBitboard & ((_bitboards.Bitboard_ColoredPieces[(int)ChessColor.Black] & _bitboards.Not_A_file) >> 9);
                pawnMoveStraight = pawnBitboard & ~(_bitboards.Bitboard_AllPieces >> 8);
            }
            else
            {
                pawnCapturingToLeft = pawnBitboard & (_bitboards.Bitboard_ColoredPieces[(int)ChessColor.White] & _bitboards.Not_H_file) << 9;
                pawnCapturingToRight = pawnBitboard & (_bitboards.Bitboard_ColoredPieces[(int)ChessColor.White] & _bitboards.Not_A_file) << 7;
                pawnMoveStraight = pawnBitboard & ~(_bitboards.Bitboard_AllPieces << 8);
            }

            while(pawnCapturingToLeft != 0)
            {
                var fromSquareMovingPawn = _bitboards.BitScanForward(pawnCapturingToLeft);
                pawnCapturingToLeft &= _bitboards.NotIndexMask[fromSquareMovingPawn];
                var toSquare = _bitboards.PawnLeft[(int)color, fromSquareMovingPawn];
                var capturedPiece = _bitboards.BoardAllPieces[(int)toSquare];
                if (_bitboards.Rank[(int)color, (int)toSquare] < 7) // normal move
                {
                    AddCapture(BitPieceType.Pawn, (Square)fromSquareMovingPawn, toSquare, capturedPiece, toSquare, BitPieceType.Empty, color, 0);
                }
                else // promotion
                {
                    for (BitPieceType promotionPiece = BitPieceType.Knight; promotionPiece <= BitPieceType.Queen; promotionPiece++)
                    {
                        AddCapture(BitPieceType.Pawn, (Square)fromSquareMovingPawn, toSquare, capturedPiece, toSquare, promotionPiece, color, 0);
                    }
                }
            }

            while (pawnCapturingToRight != 0)
            {
                var fromSquareMovingPawn = _bitboards.BitScanForward(pawnCapturingToRight);
                pawnCapturingToRight &= _bitboards.NotIndexMask[fromSquareMovingPawn];
                var toSquare = _bitboards.PawnRight[(int)color, fromSquareMovingPawn];
                var capturedPiece = _bitboards.BoardAllPieces[(int)toSquare];
                if (_bitboards.Rank[(int)color, (int)toSquare] < 7) // normal move
                {
                    AddCapture(BitPieceType.Pawn, (Square)fromSquareMovingPawn, toSquare, capturedPiece, toSquare, BitPieceType.Empty, color, 0);
                }
                else // promotion
                {
                    for (BitPieceType promotionPiece = BitPieceType.Knight; promotionPiece <= BitPieceType.Queen; promotionPiece++)
                    {
                        AddCapture(BitPieceType.Pawn, (Square)fromSquareMovingPawn, toSquare, capturedPiece, toSquare, promotionPiece, color, 0);
                    }
                }
            }

            while (pawnMoveStraight != 0)
            {
                var fromSquareMovingPawn = _bitboards.BitScanForward(pawnMoveStraight);
                pawnMoveStraight &= _bitboards.NotIndexMask[fromSquareMovingPawn];
                var toSquare = _bitboards.PawnStep[(int)color, fromSquareMovingPawn];
                if (_bitboards.Rank[(int)color, toSquare] < 7) // normal move
                {
                    AddMove(BitPieceType.Pawn, (Square)fromSquareMovingPawn, (Square)toSquare, BitPieceType.Empty, color, 0); // value ?
                }
                else // promotion
                {
                    for (BitPieceType promotionPiece = BitPieceType.Knight; promotionPiece <= BitPieceType.Queen; promotionPiece++)
                    {
                        AddMove(BitPieceType.Pawn, (Square)fromSquareMovingPawn, (Square)toSquare, promotionPiece, color, 0);
                    }
                }

                if (_bitboards.Rank[(int)color, fromSquareMovingPawn] == 1 &&
                    _bitboards.BoardAllPieces[_bitboards.PawnDoubleStep[(int)color, fromSquareMovingPawn]] == BitPieceType.Empty)
                {
                    AddMove(BitPieceType.Pawn, (Square)fromSquareMovingPawn, (Square)_bitboards.PawnDoubleStep[(int)color, fromSquareMovingPawn], BitPieceType.Empty, color, 0);
                }
            }
        }

        private void GenerateKnightMoves(ChessColor color)
        {
            Bitboard knightBitboard = _bitboards.Bitboard_Pieces[(int)color, (int)BitPieceType.Knight];

            while (knightBitboard != 0)
            {
                var fromSquareMovingKnight = _bitboards.BitScanForward(knightBitboard);
                knightBitboard &= _bitboards.NotIndexMask[fromSquareMovingKnight];

                var knightCaptures = _bitboards.MovesPieces[(int)BitPieceType.Knight, fromSquareMovingKnight] & _bitboards.Bitboard_ColoredPieces[(int)CommonHelper.OtherColor(color)];
                while (knightCaptures != 0)
                {
                    var toSquare = _bitboards.BitScanForward(knightCaptures);
                    knightCaptures &= _bitboards.NotIndexMask[toSquare];
                    AddCapture(BitPieceType.Knight, (Square)fromSquareMovingKnight, (Square)toSquare, _bitboards.BoardAllPieces[(int)toSquare] , (Square)toSquare, BitPieceType.Empty, color, 0);
                }

                var knightMoves = _bitboards.MovesPieces[(int)BitPieceType.Knight, fromSquareMovingKnight] & ~_bitboards.Bitboard_AllPieces;
                while(knightMoves != 0)
                {
                    var toSquare = _bitboards.BitScanForward(knightMoves);
                    knightMoves &= _bitboards.NotIndexMask[toSquare];
                    AddMove(BitPieceType.Knight, (Square)fromSquareMovingKnight, (Square)toSquare, BitPieceType.Empty, color, 0);
                }
            }
        }

        private void GenerateSlidingMoves(ChessColor color)
        {
            for (BitPieceType piece = BitPieceType.Bishop; piece <= BitPieceType.Queen; piece++)
            {
                GenerateSlidingPieceMoves(color, piece);
            }
        }

        private void GenerateSlidingPieceMoves(ChessColor color, BitPieceType piece)
        {
            Bitboard pieceBitboard = _bitboards.Bitboard_Pieces[(int)color, (int)piece];

            while (pieceBitboard != 0)
            {
                var fromSquareMovingPiece = _bitboards.BitScanForward(pieceBitboard);
                pieceBitboard &= _bitboards.NotIndexMask[fromSquareMovingPiece];

                var bishopCaptures = _bitboards.MovesPieces[(int)piece, fromSquareMovingPiece] & _bitboards.Bitboard_ColoredPieces[(int)CommonHelper.OtherColor(color)];
                while (bishopCaptures != 0)
                {
                    var toSquare = _bitboards.BitScanForward(bishopCaptures);
                    bishopCaptures &= _bitboards.NotIndexMask[toSquare];
                    if ((_bitboards.BetweenMatrix[(int)fromSquareMovingPiece, (int)toSquare] & _bitboards.Bitboard_AllPieces) == 0)
                    {
                        AddCapture(piece, (Square)fromSquareMovingPiece, (Square)toSquare, _bitboards.BoardAllPieces[(int)toSquare], (Square)toSquare, BitPieceType.Empty, color, 0);
                    }
                }

                var bishopMoves = _bitboards.MovesPieces[(int)piece, fromSquareMovingPiece] & ~_bitboards.Bitboard_AllPieces;
                while (bishopMoves != 0)
                {
                    var toSquare = _bitboards.BitScanForward(bishopMoves);
                    bishopMoves &= _bitboards.NotIndexMask[toSquare];

                    if ((_bitboards.BetweenMatrix[fromSquareMovingPiece, toSquare] & _bitboards.Bitboard_AllPieces) == 0)
                    {
                        AddMove(piece, (Square)fromSquareMovingPiece, (Square)toSquare, BitPieceType.Empty, color, 0);
                    }
                }
            }
        }

        private void GenerateKingMoves(ChessColor color)
        {
            Bitboard kingBitboard = _bitboards.Bitboard_Pieces[(int)color, (int)BitPieceType.King];
            var fromSquareMovingKing = _bitboards.BitScanForward(kingBitboard);

            var kingCaptures = _bitboards.MovesPieces[(int)BitPieceType.King, fromSquareMovingKing] & _bitboards.Bitboard_ColoredPieces[(int)CommonHelper.OtherColor(color)];
            while (kingCaptures != 0)
            {
                var toSquare = _bitboards.BitScanForward(kingCaptures);
                kingCaptures &= _bitboards.NotIndexMask[toSquare];
                AddCapture(BitPieceType.King, (Square)fromSquareMovingKing, (Square)toSquare, _bitboards.BoardAllPieces[(int)toSquare], (Square)toSquare, BitPieceType.Empty, color, 0);
            }

            var kingMoves = _bitboards.MovesPieces[(int)BitPieceType.King, fromSquareMovingKing] & ~_bitboards.Bitboard_AllPieces;

            while (kingMoves != 0)
            {
                var toSquare = _bitboards.BitScanForward(kingMoves);
                kingMoves &= _bitboards.NotIndexMask[toSquare];
                AddMove(BitPieceType.King, (Square)fromSquareMovingKing, (Square)toSquare, BitPieceType.Empty, color, 0);
            }
        }

        private void GenerateEnpassant(ChessColor color)
        {
            var enpassantSquare = _bitboards.BoardState.LastEnPassantSquare;
            var capturedPawnSquare = color == ChessColor.White ? enpassantSquare - 8 : enpassantSquare + 8;
            var fromSquareCapturingToLeft = _bitboards.PawnRight[(int)CommonHelper.OtherColor(color), (int)enpassantSquare];
            var fromSquareCapturingToRight = _bitboards.PawnLeft[(int)CommonHelper.OtherColor(color), (int)enpassantSquare];

            // capture to the left
            if (fromSquareCapturingToLeft != Square.NoSquare && (_bitboards.Bitboard_Pieces[(int)color, (int)BitPieceType.Pawn] & _bitboards.IndexMask[(int)fromSquareCapturingToLeft]) != 0)
            {
                AddCapture(BitPieceType.Pawn, fromSquareCapturingToLeft, enpassantSquare, _bitboards.BoardAllPieces[(int)capturedPawnSquare], capturedPawnSquare, BitPieceType.Empty, color, 0);
            }

            // capture to the right
            if (fromSquareCapturingToRight != Square.NoSquare && (_bitboards.Bitboard_Pieces[(int)color, (int)BitPieceType.Pawn] & _bitboards.IndexMask[(int)fromSquareCapturingToRight]) != 0)
            {
                AddCapture(BitPieceType.Pawn, fromSquareCapturingToRight, enpassantSquare, _bitboards.BoardAllPieces[(int)capturedPawnSquare], capturedPawnSquare, BitPieceType.Empty, color, 0);
            }
        }

        private void GenerateCastling(ChessColor color)
        {
            throw new NotImplementedException();
        }

        private void GenerateCastlingUnchecked(ChessColor color)
        {
            bool castlingRightKingSide, castlingRightQueenSide;
            Square kingSquare, kingToSquareQueenSide, kingToSquareKingSide;
            Square rookKingSquare, rookQueenSquare;

            if (color == ChessColor.White)
            {
                castlingRightKingSide = _bitboards.BoardState.LastCastlingRightWhiteKingSide;
                castlingRightQueenSide = _bitboards.BoardState.LastCastlingRightWhiteQueenSide;
                kingSquare = Square.E1;
                kingToSquareQueenSide = Square.C1;
                kingToSquareKingSide = Square.G1;
                rookKingSquare = Square.H1;
                rookQueenSquare =  Square.A1;
            }
            else
            {
                castlingRightKingSide = _bitboards.BoardState.LastCastlingRightBlackKingSide;
                castlingRightQueenSide = _bitboards.BoardState.LastCastlingRightBlackQueenSide;
                kingSquare = Square.E8;
                kingToSquareQueenSide = Square.C8;
                kingToSquareKingSide = Square.G8;
                rookKingSquare = Square.H8;
                rookQueenSquare = Square.A8;
            }

            if (castlingRightKingSide && (_bitboards.BetweenMatrix[(int)kingSquare, (int)rookKingSquare] & _bitboards.Bitboard_AllPieces) == 0 &&
                _bitboards.BoardAllPieces[(int)kingSquare] == BitPieceType.King && _bitboards.BoardColor[(int)kingSquare] == color &&
                _bitboards.BoardAllPieces[(int)rookKingSquare] == BitPieceType.Rook && _bitboards.BoardColor[(int)rookKingSquare] == color)
            {
                AddCastlingMove(BitPieceType.King, kingSquare, kingToSquareKingSide, color, CastlingType.KingSide, 0);
            }

            if (castlingRightQueenSide && (_bitboards.BetweenMatrix[(int)kingSquare, (int)rookQueenSquare] & _bitboards.Bitboard_AllPieces) == 0 &&
                _bitboards.BoardAllPieces[(int)kingSquare] == BitPieceType.King && _bitboards.BoardColor[(int)kingSquare] == color &&
                _bitboards.BoardAllPieces[(int)rookQueenSquare] == BitPieceType.Rook && _bitboards.BoardColor[(int)rookQueenSquare] == color)
            {
                AddCastlingMove(BitPieceType.King, kingSquare, kingToSquareQueenSide, color, CastlingType.QueenSide, 0);
            }
        }

        private void AddMove(BitPieceType movingPiece, Square fromSquare, Square toSquare, BitPieceType promotionPiece, ChessColor movingColor, byte value)
        {
            _moves.Add(BitMove.CreateMove(movingPiece, fromSquare, toSquare, promotionPiece, movingColor, value));
        }

        private void AddCapture(BitPieceType movingPiece, Square fromSquare, Square toSquare, BitPieceType capturedPiece, Square capturedSquare, BitPieceType promotionPiece, ChessColor movingColor, byte value)
        {
            var capture = BitMove.CreateCapture(movingPiece, fromSquare, toSquare, capturedPiece, capturedSquare, promotionPiece, movingColor, value);
            _captures.Add(capture);
            _moves.Add(capture);
        }

        private void AddCastlingMove(BitPieceType movingPiece, Square fromSquare, Square toSquare, ChessColor movingColor, CastlingType castling, byte value)
        {
            _moves.Add(BitMove.CreateCastling(movingColor, castling, value));
        }

        public IEnumerable<BitMove> GetAllCaptures(IBitBoard board, ChessColor color)
        {
            return null;
        }

        public IEnumerable<BitMove> GetLegalMoves(IBitBoard board, ChessColor color)
        {
            throw new NotImplementedException();
        }

        public bool IsAttacked(IBitBoard board, ChessColor color, Square square)
        {
            throw new NotImplementedException();
        }

        public bool IsCheck(IBitBoard board, ChessColor color)
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

        internal IEnumerable<BitMove> GetPawnMoves(ChessColor color)
        {
            ClearLists();
            GeneratePawnMoves(color);
            return _moves;
        }

        internal IEnumerable<BitMove> GetKnightMoves(ChessColor color)
        {
            ClearLists();
            GenerateKnightMoves(color);
            return _moves;
        }

        internal IEnumerable<BitMove> GetBishopMoves(ChessColor color)
        {
            ClearLists();
            GenerateSlidingPieceMoves(color, BitPieceType.Bishop);
            return _moves;
        }

        internal IEnumerable<BitMove> GetRookMoves(ChessColor color)
        {
            ClearLists();
            GenerateSlidingPieceMoves(color, BitPieceType.Rook);
            return _moves;
        }

        internal IEnumerable<BitMove> GetQueenMoves(ChessColor color)
        {
            ClearLists();
            GenerateSlidingPieceMoves(color, BitPieceType.Queen);
            return _moves;
        }

        internal IEnumerable<BitMove> GetKingMoves(ChessColor color)
        {
            ClearLists();
            GenerateKingMoves(color);
            return _moves;
        }

        internal IEnumerable<BitMove> GetEnPassant(ChessColor color)
        {
            ClearLists();
            GenerateEnpassant(color);
            return _moves;
        }

        internal IEnumerable<BitMove> GetCastlingUnchecked(ChessColor color)
        {
            ClearLists();
            GenerateCastlingUnchecked(color);
            return _moves;
        }
    }
}
