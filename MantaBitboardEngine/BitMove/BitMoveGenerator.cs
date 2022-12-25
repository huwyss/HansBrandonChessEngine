using System;
using System.Collections.Generic;
using System.Linq;
using Bitboard = System.UInt64;
using MantaCommon;
using System.Diagnostics;

namespace MantaBitboardEngine
{
    public class BitMoveGenerator
    {
        private readonly Bitboards _bitboards;
        private List<BitMove> _moves;
        private HelperBitboards _helperBits;
        ////private List<BitMove> _captures;

        public BitMoveGenerator(Bitboards bitboards, HelperBitboards helperBits)
        {
            _bitboards = bitboards;
            _moves = new List<BitMove>();
            _helperBits = helperBits;
            ////_captures = new List<BitMove>();
        }

        private void ClearLists()
        {
            _moves.Clear();
            ////_captures.Clear();
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

            return _moves;
        }

        private void GeneratePawnMoves(ChessColor color)
        {
            Bitboard pawnCapturingToLeft;
            Bitboard pawnCapturingToRight;
            Bitboard pawnMoveStraight;

            Bitboard pawnBitboard = _bitboards.Bitboard_Pieces[(int)color, (int)BitPieceType.Pawn];

            if (color == ChessColor.White)
            {
                pawnCapturingToLeft = pawnBitboard & ((_bitboards.Bitboard_ColoredPieces[(int)ChessColor.Black] & _helperBits.Not_H_file) >> 7);
                pawnCapturingToRight = pawnBitboard & ((_bitboards.Bitboard_ColoredPieces[(int)ChessColor.Black] & _helperBits.Not_A_file) >> 9);
                pawnMoveStraight = pawnBitboard & ~(_bitboards.Bitboard_AllPieces >> 8);
            }
            else
            {
                pawnCapturingToLeft = pawnBitboard & (_bitboards.Bitboard_ColoredPieces[(int)ChessColor.White] & _helperBits.Not_H_file) << 9;
                pawnCapturingToRight = pawnBitboard & (_bitboards.Bitboard_ColoredPieces[(int)ChessColor.White] & _helperBits.Not_A_file) << 7;
                pawnMoveStraight = pawnBitboard & ~(_bitboards.Bitboard_AllPieces << 8);
            }

            while(pawnCapturingToLeft != 0)
            {
                var fromSquareMovingPawn = BitHelper.BitScanForward(pawnCapturingToLeft);
                pawnCapturingToLeft &= _helperBits.NotIndexMask[fromSquareMovingPawn];
                var toSquare = _helperBits.PawnLeft[(int)color, fromSquareMovingPawn];
                var capturedPiece = _bitboards.BoardAllPieces[(int)toSquare];
                if (_helperBits.Rank[(int)color, (int)toSquare] < 7) // normal move
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
                var fromSquareMovingPawn = BitHelper.BitScanForward(pawnCapturingToRight);
                pawnCapturingToRight &= _helperBits.NotIndexMask[fromSquareMovingPawn];
                var toSquare = _helperBits.PawnRight[(int)color, fromSquareMovingPawn];
                var capturedPiece = _bitboards.BoardAllPieces[(int)toSquare];
                if (_helperBits.Rank[(int)color, (int)toSquare] < 7) // normal move
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
                var fromSquareMovingPawn = BitHelper.BitScanForward(pawnMoveStraight);
                pawnMoveStraight &= _helperBits.NotIndexMask[fromSquareMovingPawn];
                var toSquare = _helperBits.PawnStep[(int)color, fromSquareMovingPawn];
                if (_helperBits.Rank[(int)color, (int)toSquare] < 7) // normal move
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

                if (_helperBits.Rank[(int)color, fromSquareMovingPawn] == 1 &&
                    _bitboards.BoardAllPieces[(int)_helperBits.PawnDoubleStep[(int)color, fromSquareMovingPawn]] == BitPieceType.Empty)
                {
                    AddMove(BitPieceType.Pawn, (Square)fromSquareMovingPawn, (Square)_helperBits.PawnDoubleStep[(int)color, fromSquareMovingPawn], BitPieceType.Empty, color, 0);
                }
            }
        }

        private void GenerateKnightMoves(ChessColor color)
        {
            Bitboard knightBitboard = _bitboards.Bitboard_Pieces[(int)color, (int)BitPieceType.Knight];

            while (knightBitboard != 0)
            {
                var fromSquareMovingKnight = BitHelper.BitScanForward(knightBitboard);
                knightBitboard &= _helperBits.NotIndexMask[fromSquareMovingKnight];

                var knightCaptures = _helperBits.MovesPieces[(int)BitPieceType.Knight, fromSquareMovingKnight] & _bitboards.Bitboard_ColoredPieces[(int)CommonHelper.OtherColor(color)];
                while (knightCaptures != 0)
                {
                    var toSquare = BitHelper.BitScanForward(knightCaptures);
                    knightCaptures &= _helperBits.NotIndexMask[toSquare];
                    AddCapture(BitPieceType.Knight, (Square)fromSquareMovingKnight, (Square)toSquare, _bitboards.BoardAllPieces[(int)toSquare] , (Square)toSquare, BitPieceType.Empty, color, 0);
                }

                var knightMoves = _helperBits.MovesPieces[(int)BitPieceType.Knight, fromSquareMovingKnight] & ~_bitboards.Bitboard_AllPieces;
                while(knightMoves != 0)
                {
                    var toSquare = BitHelper.BitScanForward(knightMoves);
                    knightMoves &= _helperBits.NotIndexMask[toSquare];
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
                var fromSquareMovingPiece = BitHelper.BitScanForward(pieceBitboard);
                pieceBitboard &= _helperBits.NotIndexMask[fromSquareMovingPiece];

                var pieceCaptures = _helperBits.MovesPieces[(int)piece, fromSquareMovingPiece] & _bitboards.Bitboard_ColoredPieces[(int)CommonHelper.OtherColor(color)];
                while (pieceCaptures != 0)
                {
                    var toSquare = BitHelper.BitScanForward(pieceCaptures);
                    pieceCaptures &= _helperBits.NotIndexMask[toSquare];
                    if ((_helperBits.BetweenMatrix[(int)fromSquareMovingPiece, (int)toSquare] & _bitboards.Bitboard_AllPieces) == 0)
                    {
                        AddCapture(piece, (Square)fromSquareMovingPiece, (Square)toSquare, _bitboards.BoardAllPieces[(int)toSquare], (Square)toSquare, BitPieceType.Empty, color, 0);
                    }
                }

                var pieceMoves = _helperBits.MovesPieces[(int)piece, fromSquareMovingPiece] & ~_bitboards.Bitboard_AllPieces;
                while (pieceMoves != 0)
                {
                    var toSquare = BitHelper.BitScanForward(pieceMoves);
                    pieceMoves &= _helperBits.NotIndexMask[toSquare];

                    if ((_helperBits.BetweenMatrix[fromSquareMovingPiece, toSquare] & _bitboards.Bitboard_AllPieces) == 0)
                    {
                        AddMove(piece, (Square)fromSquareMovingPiece, (Square)toSquare, BitPieceType.Empty, color, 0);
                    }
                }
            }
        }

        private void GenerateKingMoves(ChessColor color)
        {
            Bitboard kingBitboard = _bitboards.Bitboard_Pieces[(int)color, (int)BitPieceType.King];

            Debug.Assert(kingBitboard != 0, "Illegal situation detected: The king is missing on the board.");

            var fromSquareMovingKing = BitHelper.BitScanForward(kingBitboard);

            var kingCaptures = _helperBits.MovesPieces[(int)BitPieceType.King, fromSquareMovingKing] & _bitboards.Bitboard_ColoredPieces[(int)CommonHelper.OtherColor(color)];
            while (kingCaptures != 0)
            {
                var toSquare = BitHelper.BitScanForward(kingCaptures);
                kingCaptures &= _helperBits.NotIndexMask[toSquare];
                AddCapture(BitPieceType.King, (Square)fromSquareMovingKing, (Square)toSquare, _bitboards.BoardAllPieces[(int)toSquare], (Square)toSquare, BitPieceType.Empty, color, 0);
            }

            var kingMoves = _helperBits.MovesPieces[(int)BitPieceType.King, fromSquareMovingKing] & ~_bitboards.Bitboard_AllPieces;

            while (kingMoves != 0)
            {
                var toSquare = BitHelper.BitScanForward(kingMoves);
                kingMoves &= _helperBits.NotIndexMask[toSquare];
                AddMove(BitPieceType.King, (Square)fromSquareMovingKing, (Square)toSquare, BitPieceType.Empty, color, 0);
            }
        }

        private void GenerateEnpassant(ChessColor color)
        {
            var enpassantSquare = _bitboards.BoardState.LastEnPassantSquare;
            if (enpassantSquare == Square.NoSquare)
            {
                return;
            }

            var capturedPawnSquare = color == ChessColor.White ? enpassantSquare - 8 : enpassantSquare + 8;
            var fromSquareCapturingToLeft = _helperBits.PawnRight[(int)CommonHelper.OtherColor(color), (int)enpassantSquare];
            var fromSquareCapturingToRight = _helperBits.PawnLeft[(int)CommonHelper.OtherColor(color), (int)enpassantSquare];

            // capture to the left
            if (fromSquareCapturingToLeft != Square.NoSquare && (_bitboards.Bitboard_Pieces[(int)color, (int)BitPieceType.Pawn] & _helperBits.IndexMask[(int)fromSquareCapturingToLeft]) != 0)
            {
                AddCapture(BitPieceType.Pawn, fromSquareCapturingToLeft, enpassantSquare, _bitboards.BoardAllPieces[(int)capturedPawnSquare], capturedPawnSquare, BitPieceType.Empty, color, 0);
            }

            // capture to the right
            if (fromSquareCapturingToRight != Square.NoSquare && (_bitboards.Bitboard_Pieces[(int)color, (int)BitPieceType.Pawn] & _helperBits.IndexMask[(int)fromSquareCapturingToRight]) != 0)
            {
                AddCapture(BitPieceType.Pawn, fromSquareCapturingToRight, enpassantSquare, _bitboards.BoardAllPieces[(int)capturedPawnSquare], capturedPawnSquare, BitPieceType.Empty, color, 0);
            }
        }

        private void GenerateCastling(ChessColor color)
        {
            bool castlingRightKingSide, castlingRightQueenSide;
            Square kingSquare;
            Square rookKingSquare, rookQueenSquare;

            if (color == ChessColor.White)
            {
                castlingRightKingSide = _bitboards.BoardState.LastCastlingRightWhiteKingSide;
                castlingRightQueenSide = _bitboards.BoardState.LastCastlingRightWhiteQueenSide;
                kingSquare = Square.E1;
                rookKingSquare = Square.H1;
                rookQueenSquare =  Square.A1;
            }
            else
            {
                castlingRightKingSide = _bitboards.BoardState.LastCastlingRightBlackKingSide;
                castlingRightQueenSide = _bitboards.BoardState.LastCastlingRightBlackQueenSide;
                kingSquare = Square.E8;
                rookKingSquare = Square.H8;
                rookQueenSquare = Square.A8;
            }

            if (castlingRightKingSide && // castling right available
                (_helperBits.BetweenMatrix[(int)kingSquare, (int)rookKingSquare] & _bitboards.Bitboard_AllPieces) == 0 && // space between king and rook free
                _bitboards.BoardAllPieces[(int)kingSquare] == BitPieceType.King && _bitboards.BoardColor[(int)kingSquare] == color && // king is on original square. necessary ??? should be in right
                _bitboards.BoardAllPieces[(int)rookKingSquare] == BitPieceType.Rook && _bitboards.BoardColor[(int)rookKingSquare] == color && // rook is on original square. necessary ??? should be in right
                !CastlingSquaresAttacked(color, CastlingType.KingSide))
            {
                AddCastlingMove(color, CastlingType.KingSide, 0);
            }

            if (castlingRightQueenSide && (_helperBits.BetweenMatrix[(int)kingSquare, (int)rookQueenSquare] & _bitboards.Bitboard_AllPieces) == 0 &&
                _bitboards.BoardAllPieces[(int)kingSquare] == BitPieceType.King && _bitboards.BoardColor[(int)kingSquare] == color &&
                _bitboards.BoardAllPieces[(int)rookQueenSquare] == BitPieceType.Rook && _bitboards.BoardColor[(int)rookQueenSquare] == color &&
                !CastlingSquaresAttacked(color, CastlingType.QueenSide))
            {
                AddCastlingMove(color, CastlingType.QueenSide, 0);
            }
        }

        private bool CastlingSquaresAttacked(ChessColor color, CastlingType castlingType)
        {
            if (color == ChessColor.White)
            {
                if (castlingType == CastlingType.KingSide)
                {
                    return IsAttacked(color, Square.E1) || IsAttacked(color, Square.F1) || IsAttacked(color, Square.G1);
                }
                else
                {
                    return IsAttacked(color, Square.E1) || IsAttacked(color, Square.D1) || IsAttacked(color, Square.C1);
                }
            }
            else
            {
                if (castlingType == CastlingType.KingSide)
                {
                    return IsAttacked(color, Square.E8) || IsAttacked(color, Square.F8) || IsAttacked(color, Square.G8);
                }
                else
                {
                    return IsAttacked(color, Square.E8) || IsAttacked(color, Square.D8) || IsAttacked(color, Square.C8);
                }
            }
        }


        private void AddMove(BitPieceType movingPiece, Square fromSquare, Square toSquare, BitPieceType promotionPiece, ChessColor movingColor, byte value)
        {
            _moves.Add(BitMove.CreateMove(movingPiece, fromSquare, toSquare, promotionPiece, movingColor, value));
        }

        private void AddCapture(BitPieceType movingPiece, Square fromSquare, Square toSquare, BitPieceType capturedPiece, Square capturedSquare, BitPieceType promotionPiece, ChessColor movingColor, byte value)
        {
            var capture = BitMove.CreateCapture(movingPiece, fromSquare, toSquare, capturedPiece, capturedSquare, promotionPiece, movingColor, value);
            _moves.Add(capture);
        }

        private void AddCastlingMove(ChessColor movingColor, CastlingType castling, byte value)
        {
            _moves.Add(BitMove.CreateCastling(movingColor, castling, value));
        }

        public IEnumerable<BitMove> GetAllCaptures(IBitBoard board, ChessColor color)
        {
            // todo user _captures
            return null;
        }

        public IEnumerable<BitMove> GetLegalMoves(ChessColor color)
        {
            return GetAllMoves(color);
            ////var pseudolegalMoves = GetAllMoves(color);
            ////var legalMoves = new List<BitMove>();

            ////foreach (var move in pseudolegalMoves)
            ////{
            ////    _bitboards.Move(move);
            ////    if (!IsCheck(color))
            ////    {
            ////        legalMoves.Add(move);
            ////    }

            ////    _bitboards.Back();
            ////}

            ////return legalMoves;
        }

        public bool IsAttacked(ChessColor color, Square square)
        {
            var otherColor = CommonHelper.OtherColor(color);

            // is attacked by pawn
            var bitboardAttackingPawnsToSquare = _helperBits.PawnCaptures[(int)color, (int)square];
            if ((bitboardAttackingPawnsToSquare & _bitboards.Bitboard_Pieces[(int)otherColor, (int)BitPieceType.Pawn]) != 0)
            {
                return true;
            }

            // is attacked by knight
            var bitboardAttackingKnightToSquare = _helperBits.MovesPieces[(int)BitPieceType.Knight, (int)square];
            if ((bitboardAttackingKnightToSquare & _bitboards.Bitboard_Pieces[(int)otherColor, (int)BitPieceType.Knight]) != 0)
            {
                return true;
            }

            // is attacked by rook, queen, bishop
            for (BitPieceType piece = BitPieceType.Bishop; piece <= BitPieceType.Queen; piece++)
            {
                var bitboardAttackingPieceToSquare = _helperBits.MovesPieces[(int)piece, (int)square] & _bitboards.Bitboard_Pieces[(int)otherColor, (int)piece]; ;
                while (bitboardAttackingPieceToSquare != 0)
                {
                    var attackingPieceSquare = BitHelper.BitScanForward(bitboardAttackingPieceToSquare);
                    bitboardAttackingPieceToSquare &= _helperBits.NotIndexMask[attackingPieceSquare];
                    if ((_helperBits.BetweenMatrix[(int)attackingPieceSquare, (int)square] & _bitboards.Bitboard_AllPieces) == 0)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public bool IsCheck(ChessColor color)
        {
            var kingSquare = (Square)BitHelper.BitScanForward(_bitboards.Bitboard_Pieces[(int)color, (int)BitPieceType.King]);
            return IsAttacked(color, kingSquare);
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

        internal IEnumerable<BitMove> GetCastling(ChessColor color)
        {
            ClearLists();
            GenerateCastling(color);
            return _moves;
        }
    }
}
