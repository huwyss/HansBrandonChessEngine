using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Linq;
using static MantaChessEngine.Definitions;

[assembly: InternalsVisibleTo("MantaChessEngineTest")]
namespace MantaChessEngine
{
    public class MoveGenerator : IMoveGenerator
    {
        private MoveFactory _factory;

        public MoveGenerator(MoveFactory factory)
        {
            _factory = factory;
        }

        /// <summary>
        /// Manta has grown up and now is able to return the legal moves only
        /// </summary>
        public List<IMove> GetLegalMoves(IBoard board, ChessColor color)
        {
            var pseudolegalMoves = GetAllMoves(board, color);
            var legalMoves = new List<IMove>();

            foreach (var move in pseudolegalMoves)
            {
                board.Move(move);
                var kingPosition = board.GetKing(color);
                if (!IsAttacked(board, color, kingPosition.File, kingPosition.Rank))
                {
                    legalMoves.Add(move);
                }

                board.Back();
            }

            return legalMoves;
        }

        // Note: Manta is a king capture engine. 
        // This means even if we are in check then also moves that do not remove the check are returned here.
        // However if there is no king then no moves are returned. (Empty list)
        public List<IMove> GetAllMoves(IBoard board, ChessColor color, bool includeCastling = true)
        {
            var allMovesUnchecked = GetAllMovesUnchecked(board, color, includeCastling);
            return allMovesUnchecked;
        }

        private List<IMove> GetAllMovesUnchecked(IBoard board, ChessColor color, bool includeCastling = true)
        {
            bool kingFound = false;
            List<IMove> allMoves = new List<IMove>();

            for (int file = 1; file <= 8; file++)
            {
                for (int rank = 1; rank <= 8; rank++)
                {
                    if (board.GetColor(file, rank) == color)
                    {
                        Piece piece = board.GetPiece(file, rank);
                        
                        if (piece is King)
                        {
                            kingFound = true;
                        }

                        allMoves.AddRange(piece.GetMoves(this, board, file, rank, includeCastling));
                    }
                }
            }

            return kingFound ? allMoves : new List<IMove>();
        }

        public bool IsMoveValid(IBoard board, IMove move)
        {
            bool valid = HasCorrectColorMoved(board, move);
            valid &= move.MovingPiece.GetMoves(this, board, move.SourceFile, move.SourceRank).Contains(move);

            board.Move(move);
            var king = board.GetKing(move.Color);
            valid &= !IsAttacked(board, move.Color, king.File, king.Rank);
            board.Back();
            return valid;
        }

        private bool HasCorrectColorMoved(IBoard board, IMove move)
        {
            return (move.MovingPiece.Color == board.SideToMove);
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

        private bool IsFieldsEmpty(IBoard board, int sourceFile, int sourceRank, int targetFile)
        {
            bool empty = true;

            for (int file = sourceFile; file <= targetFile; file++)
            {
                empty &= board.GetPiece(file, sourceRank) == null; //Definitions.EmptyField;
            }

            return empty;
        }

        /// <summary>
        /// Is the field of color ist attacked by opposite color.
        /// </summary>
        public bool IsAttacked(IBoard board, ChessColor color, int file, int rank)
        {
            // find all oponent moves
            var moves = GetAllMoves(board, Helper.GetOppositeColor(color), false);

            foreach (IMove move in moves)
            {
                if (move.TargetFile == file && move.TargetRank == rank)
                {
                    return true;
                }
            }

            return false;
        }

        public bool IsCheck(IBoard board, ChessColor color)
        {
            // find all oponent moves
            var moves = GetAllMoves(board, Helper.GetOppositeColor(color), false);

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
}
