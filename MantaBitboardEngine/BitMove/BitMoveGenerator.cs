using System;
using System.Collections.Generic;
using System.Linq;
using Bitboard = System.UInt64;
using MantaCommon;
using System.Diagnostics;

namespace MantaBitboardEngine
{
    public class BitMoveGenerator : IMoveGenerator<BitMove>
    {
        private const int promotionValue = 200;
        private const int capturePromotionValue = 220;
        private const int pawnMoveValue = 10;
        private const int generalMoveValue = 0;
        private static byte[,] CaptureValues; // dimensions: capturing piece, captured piece

        private readonly Bitboards _bitboards;
        private List<BitMove> _moves;
        private HelperBitboards _helperBits;

        public BitMoveGenerator(Bitboards bitboards, HelperBitboards helperBits)
        {
            _bitboards = bitboards;
            _moves = new List<BitMove>();
            _helperBits = helperBits;

            CaptureValues = new byte[6, 6];
            InitializeCaptureValues();
        }

        private void ClearMoves()
        {
            _moves.Clear();
        }

        public IEnumerable<BitMove> GetAllMoves(ChessColor color)
        {
            ClearMoves();

            GenerateEnpassant(color);
            GeneratePawnMoves(color);
            GenerateKnightMoves(color);
            GenerateSlidingMoves(color);
            GenerateKingMoves(color);
            
            GenerateCastling(color);

            return _moves.OrderByDescending(m => m.Value);
        }

        public IEnumerable<BitMove> GetAllCaptures(ChessColor color)
        {
            ClearMoves();

            GenerateEnpassant(color);
            GeneratePawnMoves(color, true);
            GenerateKnightMoves(color, true);
            GenerateSlidingMoves(color, true);
            GenerateKingMoves(color, true);

            return _moves.OrderByDescending(m => m.Value);
        }

        private void GeneratePawnMoves(ChessColor color, bool capturesOnly = false)
        {
            Bitboard pawnCapturingToLeft;
            Bitboard pawnCapturingToRight;
            Bitboard pawnMoveStraight;

            Bitboard pawnBitboard = _bitboards.Bitboard_Pieces[(int)color, (int)BitPieceType.Pawn];

            if (color == ChessColor.White)
            {
                pawnCapturingToLeft = pawnBitboard & ((_bitboards.Bitboard_ColoredPieces[(int)ChessColor.Black] & _helperBits.Not_H_file) >> 7);
                pawnCapturingToRight = pawnBitboard & ((_bitboards.Bitboard_ColoredPieces[(int)ChessColor.Black] & _helperBits.Not_A_file) >> 9);
            }
            else
            {
                pawnCapturingToLeft = pawnBitboard & (_bitboards.Bitboard_ColoredPieces[(int)ChessColor.White] & _helperBits.Not_H_file) << 9;
                pawnCapturingToRight = pawnBitboard & (_bitboards.Bitboard_ColoredPieces[(int)ChessColor.White] & _helperBits.Not_A_file) << 7;
            }

            while(pawnCapturingToLeft != 0)
            {
                var fromSquareMovingPawn = BitHelper.BitScanForward(pawnCapturingToLeft);
                pawnCapturingToLeft &= _helperBits.NotIndexMask[fromSquareMovingPawn];
                var toSquare = _helperBits.PawnLeft[(int)color, fromSquareMovingPawn];
                var capturedPiece = _bitboards.BoardAllPieces[(int)toSquare];
                if (_helperBits.Rank[(int)color, (int)toSquare] < 7) // normal move
                {
                    AddCapture(BitPieceType.Pawn, (Square)fromSquareMovingPawn, toSquare, capturedPiece, toSquare, BitPieceType.Empty, color, CaptureValues[(int)BitPieceType.Pawn, (int)capturedPiece]);
                }
                else // promotion
                {
                    for (BitPieceType promotionPiece = BitPieceType.Queen; promotionPiece >= BitPieceType.Knight; promotionPiece--)
                    {
                        AddCapture(BitPieceType.Pawn, (Square)fromSquareMovingPawn, toSquare, capturedPiece, toSquare, promotionPiece, color, promotionValue);
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
                    AddCapture(BitPieceType.Pawn, (Square)fromSquareMovingPawn, toSquare, capturedPiece, toSquare, BitPieceType.Empty, color, CaptureValues[(int)BitPieceType.Pawn, (int)capturedPiece]);
                }
                else // promotion
                {
                    for (BitPieceType promotionPiece = BitPieceType.Queen; promotionPiece >= BitPieceType.Knight; promotionPiece--)
                    {
                        AddCapture(BitPieceType.Pawn, (Square)fromSquareMovingPawn, toSquare, capturedPiece, toSquare, promotionPiece, color, capturePromotionValue);
                    }
                }
            }

            if (capturesOnly)
            {
                return;
            }

            if (color == ChessColor.White)
            {
                pawnMoveStraight = pawnBitboard & ~(_bitboards.Bitboard_AllPieces >> 8);
            }
            else
            {
                pawnMoveStraight = pawnBitboard & ~(_bitboards.Bitboard_AllPieces << 8);
            }

            while (pawnMoveStraight != 0)
            {
                var fromSquareMovingPawn = BitHelper.BitScanForward(pawnMoveStraight);
                pawnMoveStraight &= _helperBits.NotIndexMask[fromSquareMovingPawn];
                var toSquare = _helperBits.PawnStep[(int)color, fromSquareMovingPawn];
                if (_helperBits.Rank[(int)color, (int)toSquare] < 7) // normal move
                {
                    AddMove(BitPieceType.Pawn, (Square)fromSquareMovingPawn, (Square)toSquare, BitPieceType.Empty, color, pawnMoveValue);
                }
                else // promotion
                {
                    for (BitPieceType promotionPiece = BitPieceType.Queen; promotionPiece >= BitPieceType.Knight; promotionPiece--)
                    {
                        AddMove(BitPieceType.Pawn, (Square)fromSquareMovingPawn, (Square)toSquare, promotionPiece, color, promotionValue);
                    }
                }

                if (_helperBits.Rank[(int)color, fromSquareMovingPawn] == 1 &&
                    _bitboards.BoardAllPieces[(int)_helperBits.PawnDoubleStep[(int)color, fromSquareMovingPawn]] == BitPieceType.Empty)
                {
                    AddMove(BitPieceType.Pawn, (Square)fromSquareMovingPawn, (Square)_helperBits.PawnDoubleStep[(int)color, fromSquareMovingPawn], BitPieceType.Empty, color, pawnMoveValue);
                }
            }
        }

        private void GenerateKnightMoves(ChessColor color, bool capturesOnly = false)
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
                    var capturedPiece = _bitboards.BoardAllPieces[(int)toSquare];
                    AddCapture(BitPieceType.Knight, (Square)fromSquareMovingKnight, (Square)toSquare, capturedPiece, (Square)toSquare, BitPieceType.Empty, color, CaptureValues[(int)BitPieceType.Knight, (int)capturedPiece]);
                }

                if (capturesOnly)
                {
                    continue;
                }

                var knightMoves = _helperBits.MovesPieces[(int)BitPieceType.Knight, fromSquareMovingKnight] & ~_bitboards.Bitboard_AllPieces;
                while(knightMoves != 0)
                {
                    var toSquare = BitHelper.BitScanForward(knightMoves);
                    knightMoves &= _helperBits.NotIndexMask[toSquare];
                    AddMove(BitPieceType.Knight, (Square)fromSquareMovingKnight, (Square)toSquare, BitPieceType.Empty, color, generalMoveValue);
                }
            }
        }

        private void GenerateSlidingMoves(ChessColor color, bool capturesOnly = false)
        {
            for (BitPieceType piece = BitPieceType.Bishop; piece <= BitPieceType.Queen; piece++)
            {
                GenerateSlidingPieceMoves(color, piece, capturesOnly);
            }
        }

        private void GenerateSlidingPieceMoves(ChessColor color, BitPieceType piece, bool capturesOnly)
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
                        var capturedPiece = _bitboards.BoardAllPieces[(int)toSquare];
                        AddCapture(piece, (Square)fromSquareMovingPiece, (Square)toSquare, capturedPiece, (Square)toSquare, BitPieceType.Empty, color, CaptureValues[(int)piece, (int)capturedPiece]);
                    }
                }

                if (capturesOnly)
                {
                    continue;
                }

                var pieceMoves = _helperBits.MovesPieces[(int)piece, fromSquareMovingPiece] & ~_bitboards.Bitboard_AllPieces;
                while (pieceMoves != 0)
                {
                    var toSquare = BitHelper.BitScanForward(pieceMoves);
                    pieceMoves &= _helperBits.NotIndexMask[toSquare];

                    if ((_helperBits.BetweenMatrix[fromSquareMovingPiece, toSquare] & _bitboards.Bitboard_AllPieces) == 0)
                    {
                        AddMove(piece, (Square)fromSquareMovingPiece, (Square)toSquare, BitPieceType.Empty, color, generalMoveValue);
                    }
                }
            }
        }

        private void GenerateKingMoves(ChessColor color, bool capturesOnly = false)
        {
            Bitboard kingBitboard = _bitboards.Bitboard_Pieces[(int)color, (int)BitPieceType.King];

            Debug.Assert(kingBitboard != 0, "Illegal situation detected: The king is missing on the board.");

            var fromSquareMovingKing = BitHelper.BitScanForward(kingBitboard);

            var kingCaptures = _helperBits.MovesPieces[(int)BitPieceType.King, fromSquareMovingKing] & _bitboards.Bitboard_ColoredPieces[(int)CommonHelper.OtherColor(color)];
            while (kingCaptures != 0)
            {
                var toSquare = BitHelper.BitScanForward(kingCaptures);
                kingCaptures &= _helperBits.NotIndexMask[toSquare];
                var capturedPiece = _bitboards.BoardAllPieces[(int)toSquare];
                AddCapture(BitPieceType.King, (Square)fromSquareMovingKing, (Square)toSquare, capturedPiece, (Square)toSquare, BitPieceType.Empty, color, CaptureValues[(int)BitPieceType.King, (int)capturedPiece]);
            }

            if (capturesOnly)
            {
                return;
            }

            var kingMoves = _helperBits.MovesPieces[(int)BitPieceType.King, fromSquareMovingKing] & ~_bitboards.Bitboard_AllPieces;
            while (kingMoves != 0)
            {
                var toSquare = BitHelper.BitScanForward(kingMoves);
                kingMoves &= _helperBits.NotIndexMask[toSquare];
                AddMove(BitPieceType.King, (Square)fromSquareMovingKing, (Square)toSquare, BitPieceType.Empty, color, generalMoveValue);
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
                var capturedPiece = _bitboards.BoardAllPieces[(int)capturedPawnSquare];
                AddCapture(BitPieceType.Pawn, fromSquareCapturingToLeft, enpassantSquare, capturedPiece, capturedPawnSquare, BitPieceType.Empty, color, CaptureValues[(int)BitPieceType.Pawn, (int)capturedPiece]);
            }

            // capture to the right
            if (fromSquareCapturingToRight != Square.NoSquare && (_bitboards.Bitboard_Pieces[(int)color, (int)BitPieceType.Pawn] & _helperBits.IndexMask[(int)fromSquareCapturingToRight]) != 0)
            {
                var capturedPiece = _bitboards.BoardAllPieces[(int)capturedPawnSquare];
                AddCapture(BitPieceType.Pawn, fromSquareCapturingToRight, enpassantSquare, capturedPiece, capturedPawnSquare, BitPieceType.Empty, color, CaptureValues[(int)BitPieceType.Pawn, (int)capturedPiece]);
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
                AddCastlingMove(color, CastlingType.KingSide, generalMoveValue);
            }

            if (castlingRightQueenSide && (_helperBits.BetweenMatrix[(int)kingSquare, (int)rookQueenSquare] & _bitboards.Bitboard_AllPieces) == 0 &&
                _bitboards.BoardAllPieces[(int)kingSquare] == BitPieceType.King && _bitboards.BoardColor[(int)kingSquare] == color &&
                _bitboards.BoardAllPieces[(int)rookQueenSquare] == BitPieceType.Rook && _bitboards.BoardColor[(int)rookQueenSquare] == color &&
                !CastlingSquaresAttacked(color, CastlingType.QueenSide))
            {
                AddCastlingMove(color, CastlingType.QueenSide, generalMoveValue);
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

            // is attacked by King
            var bitboardAttackingKingToSquare = _helperBits.MovesPieces[(int)BitPieceType.King, (int)square];
            if ((bitboardAttackingKingToSquare & _bitboards.Bitboard_Pieces[(int)otherColor, (int)BitPieceType.King]) != 0)
            {
                return true;
            }

            return false;
        }

        public bool IsCheck(ChessColor color)
        {
            var kingSquare = (Square)BitHelper.BitScanForward(_bitboards.Bitboard_Pieces[(int)color, (int)BitPieceType.King]);
            return IsAttacked(color, kingSquare);
        }

        public bool IsMoveValid(BitMove move)
        {
            bool valid = HasCorrectColorMoved(move);
            return valid;

            ////valid &= GetMoves(this, board, move.SourceFile, move.SourceRank).Contains(move);

            ////board.Move(move);
            ////var king = board.GetKing(move.Color);
            ////valid &= !IsAttacked(board, move.Color, king.File, king.Rank);
            ////board.Back();
            ////return valid;
        }

        private bool HasCorrectColorMoved(BitMove move)
        {
            return move.MovingColor == _bitboards.BoardState.SideToMove;
        }

        private void InitializeCaptureValues()
        {
            for (int i = (int)BitPieceType.Pawn; i <= (int)BitPieceType.King; i++)
            {
                CaptureValues[(int)BitPieceType.Pawn, i] = PawnCaptureValues[i];
                CaptureValues[(int)BitPieceType.Knight, i] = KnightCaptureValues[i];
                CaptureValues[(int)BitPieceType.Bishop, i] = BishopCaptureValues[i];
                CaptureValues[(int)BitPieceType.Rook, i] = RookCaptureValues[i];
                CaptureValues[(int)BitPieceType.Queen, i] = QueenCaptureValues[i];
                CaptureValues[(int)BitPieceType.King, i] = KingCaptureValues[i];
            }
        }

        // value := 100 + (value captured piece - value capturing piece)
        private readonly byte[] PawnCaptureValues = new byte[6] { 100, 120, 120, 140, 180, 0 };
        private readonly byte[] KnightCaptureValues = new byte[6] { 80, 100, 100, 120, 160, 0 };
        private readonly byte[] BishopCaptureValues = new byte[6] { 80, 100, 100, 120, 160, 0 };
        private readonly byte[] RookCaptureValues = new byte[6] { 60, 80, 80, 100, 140, 0 };
        private readonly byte[] QueenCaptureValues = new byte[6] { 20, 40, 40, 60, 100, 0 };
        private readonly byte[] KingCaptureValues = new byte[6] { 90, 90, 90, 90, 90, 0 };

        //////////////////////////////////////////////////////
        // Testing interface
        //////////////////////////////////////////////////////

        internal IEnumerable<BitMove> GetPawnMoves(ChessColor color)
        {
            ClearMoves();
            GeneratePawnMoves(color);
            return _moves;
        }

        internal IEnumerable<BitMove> GetKnightMoves(ChessColor color)
        {
            ClearMoves();
            GenerateKnightMoves(color);
            return _moves;
        }

        internal IEnumerable<BitMove> GetBishopMoves(ChessColor color)
        {
            ClearMoves();
            GenerateSlidingPieceMoves(color, BitPieceType.Bishop, false);
            return _moves;
        }

        internal IEnumerable<BitMove> GetRookMoves(ChessColor color)
        {
            ClearMoves();
            GenerateSlidingPieceMoves(color, BitPieceType.Rook, false);
            return _moves;
        }

        internal IEnumerable<BitMove> GetQueenMoves(ChessColor color)
        {
            ClearMoves();
            GenerateSlidingPieceMoves(color, BitPieceType.Queen, false);
            return _moves;
        }

        internal IEnumerable<BitMove> GetKingMoves(ChessColor color)
        {
            ClearMoves();
            GenerateKingMoves(color);
            return _moves;
        }

        internal IEnumerable<BitMove> GetEnPassant(ChessColor color)
        {
            ClearMoves();
            GenerateEnpassant(color);
            return _moves;
        }

        internal IEnumerable<BitMove> GetCastling(ChessColor color)
        {
            ClearMoves();
            GenerateCastling(color);
            return _moves;
        }
    }
}
