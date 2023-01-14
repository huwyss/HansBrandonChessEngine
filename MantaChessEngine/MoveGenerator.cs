using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Linq;
using static MantaChessEngine.Definitions;
using MantaCommon;

[assembly: InternalsVisibleTo("MantaChessEngineTest")]
[assembly: InternalsVisibleTo("MantaBitboardEngineTest")]
namespace MantaChessEngine
{
    public class MoveGenerator : IMoveGenerator<IMove>
    {
        IMoveValues _values;

        public IEnumerable<IMove> GetAllCaptures(ChessColor color)
        {
            return Enumerable.Empty<IMove>();
        }

        private readonly IBoard _board;

        public MoveGenerator(IBoard board)
        {
            _board = board;
            _values = new MoveValues();
        }

        public IEnumerable<IMove> GetAllMoves(ChessColor color)
        {
            return GetAllMoves(color, true, true).OrderByDescending(m => m.GetMoveValue(_values));
        }

        /// <summary>
        /// Returns all pseudo legal moves of that piece. Pseudo means the king is allowed to be under attack but
        /// otherwise the move must be legal.
        /// </summary>
        public IEnumerable<IMove> GetAllMoves(ChessColor color, bool includeCastling = true, bool includePawnMoves = true)
        {
            var allMovesUnchecked = GetAllMovesUnchecked(color, includeCastling, includePawnMoves);
            return allMovesUnchecked;
        }

        private IEnumerable<IMove> GetAllMovesUnchecked(ChessColor color, bool includeCastling = true, bool includePawnMoves = true)
        {
            bool kingFound = false;
            List<IMove> allMoves = new List<IMove>();

            for (var square = Square.A1; square <= Square.H8; square++)
            {
                if (_board.GetColor(square) == color)
                {
                    Piece piece = _board.GetPiece(square);

                    if (piece is King)
                    {
                        kingFound = true;
                    }

                    if (piece is Pawn && !includePawnMoves)
                    {
                        continue;
                    }

                    allMoves.AddRange(piece.GetMoves(this, _board, square, includeCastling));
                }
            }

            return kingFound ? allMoves : new List<IMove>();
        }

        public bool IsMoveValid(IMove move)
        {
            bool valid = HasCorrectColorMoved(move);
            valid &= move.MovingPiece.GetMoves(this, _board, move.FromSquare).Contains(move);

            _board.Move(move);
            var king = _board.GetKing(move.MovingColor);
            valid &= !IsAttacked(move.MovingColor, king);
            _board.Back();
            return valid;
        }

        private bool HasCorrectColorMoved(IMove move)
        {
            return (move.MovingPiece.Color == _board.BoardState.SideToMove);
        }

        // unit tests need access.
        // valid means move is within board.
        internal void GetEndPosition(int file, int rank, string sequence, out int targetFile, out int targetRank, out bool valid)
        {
            targetFile = file;
            targetRank = rank;

            for (int i = 0; i < sequence.Length; i++)
            {
                char direction = sequence[i];
                switch (direction)
                {
                    case UP:
                        targetRank++;
                        break;
                    case RIGHT:
                        targetFile++;
                        break;
                    case DOWN:
                        targetRank--;
                        break;
                    case LEFT:
                        targetFile--;
                        break;
                    default:
                        break;
                }
            }

            valid = targetFile >= 1 && targetFile <= 8 &&
                    targetRank >= 1 && targetRank <= 8;
        }

        ////private bool IsFieldsEmpty(int sourceFile, int sourceRank, int targetFile)
        ////{
        ////    bool empty = true;

        ////    for (int file = sourceFile; file <= targetFile; file++)
        ////    {
        ////        empty &= _board.GetPiece(file, sourceRank) == null; //Definitions.EmptyField;
        ////    }

        ////    return empty;
        ////}

        /// <summary>
        /// Is the field of color ist attacked by opposite color.
        /// </summary>
        public bool IsAttacked(ChessColor color, Square square)
        {
            // find all oponent moves, without pawn moves
            var moves = GetAllMoves(Helper.GetOppositeColor(color), false, false);

            foreach (IMove move in moves)
            {
                if (move.ToSquare == square)
                {
                    return true;
                }
            }

            // check if there is an attacking pawn diagonal to the position
            if (color == ChessColor.White)
            {
                var piece = Helper.GetFile(square) - 1 >= 1 && Helper.GetRank(square) + 1 <= 8 ? _board.GetPiece(square + 7) : null;
                if (piece is Pawn && piece.Color == ChessColor.Black)
                    return true;

                piece = Helper.GetFile(square) + 1 <= 8 && Helper.GetRank(square) + 1 <= 8 ? _board.GetPiece(square + 9) : null;
                if (piece is Pawn && piece.Color == ChessColor.Black)
                    return true;
            }
            else
            {
                var piece = Helper.GetFile(square) - 1 >= 1 && Helper.GetRank(square) - 1 >= 1 ? _board.GetPiece(square - 9) : null;
                if (piece is Pawn && piece.Color == ChessColor.White)
                    return true;

                piece = Helper.GetFile(square) + 1 <= 8 && Helper.GetRank(square) - 1 >= 1 ? _board.GetPiece(square - 7) : null;
                if (piece is Pawn && piece.Color == ChessColor.White)
                    return true;
            }

            return false;
        }

        public bool IsCheck(ChessColor color)
        {
            // find all oponent moves
            var moves = GetAllMoves(Helper.GetOppositeColor(color), false);

            // if a move ends in king's position then king is in check
            foreach (IMove move in moves)
            {
                if (move.CapturedPiece is King && move.CapturedPiece.Color == color)
                {
                    return true;
                }
            }

            return false;
        }
    }

    public static class MoveValueExtension
    {
        public static int GetMoveValue(this IMove move, IMoveValues val)
        {
            int importance;
            if (move.CapturedPiece != null && move.PromotionPiece != PieceType.Empty)
            {
                importance = val.CapturePromotionValue;
            }
            else if (move.PromotionPiece != PieceType.Empty)
            {
                importance = val.PromotionValue;
            }
            else if (move.CapturedPiece != null)
            {
                importance = val.CaptureValues[(int)move.CapturedPiece.PieceType, (int)move.MovingPiece.PieceType];
            }
            else if (move.MovingPiece is Pawn)
            {
                importance = val.PawnMoveValue;
            }
            else
            {
                importance = val.GeneralMoveValue;
            }

            return importance;
        }
    }
}
