using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("BaracudaChessEngineTest")]
namespace BaracudaChessEngine
{
    public class MoveGenerator
    {
        // Note: Baracuda is a king capture engine. 
        // This means even if we are in check then also moves that do not remove the check are returned here.
        public List<Move> GetAllMoves(Board board, Definitions.ChessColor color)
        {
            var allMovesUnchecked = GetAllMovesUnchecked(board, color);
            return allMovesUnchecked;
        }

        private List<Move> GetAllMovesUnchecked(Board board, Definitions.ChessColor color)
        {
            List<Move> allMoves = new List<Move>();

            for (int file = 1; file <= 8; file++)
            {
                for (int rank = 1; rank <= 8; rank++)
                {
                    if (board.GetColor(file, rank) == color)
                    {
                        allMoves.AddRange(GetMoves(board, file, rank));
                    }
                }
            }

            return allMoves;
        }

        /// <summary>
        /// Returns all moves of that piece.
        /// </summary>
        /// <param name="file"></param>
        /// <param name="rank"></param>
        /// <returns></returns>
        /// todo: castling
        /// todo: capture en passant
        /// todo: pawn promotion
        public List<Move> GetMoves(Board board, int file, int rank)
        {
            List<Move> moves = new List<Move>();
            char piece = board.GetPiece(file, rank);
            int targetRank;
            int targetFile;
            bool valid;
            Definitions.ChessColor pieceColor = board.GetColor(file, rank);
            List<string> directionSequences;
            char pieceLower = piece.ToString().ToLower()[0];
            switch (pieceLower)
            {
                case Definitions.KNIGHT:
                case Definitions.KING:
                    directionSequences = GetMoveDirectionSequence(pieceLower);
                    foreach (string sequence in directionSequences)
                    {
                        GetEndPosition(file, rank, sequence, out targetFile, out targetRank, out valid);
                        if (valid && pieceColor != board.GetColor(targetFile, targetRank)) // capture or empty field
                        {
                            moves.Add(new Move(file, rank, targetFile, targetRank, board.GetPiece(targetFile, targetRank)));
                        }
                    }
                    break;

                case Definitions.ROOK: 
                case Definitions.QUEEN:
                case Definitions.BISHOP:
                    directionSequences = GetMoveDirectionSequence(pieceLower);
                    foreach (string sequence in directionSequences)
                    {
                        int currentFile = file;
                        int currentRank = rank;
                        for (int i = 1; i < 8; i++) // walk in the direction until off board or captured or next is own piece
                        {
                            GetEndPosition(currentFile, currentRank, sequence, out targetFile, out targetRank, out valid);
                            if (!valid)
                            {
                                break;
                            }
                            Definitions.ChessColor targetColor = board.GetColor(targetFile, targetRank);
                            if (pieceColor == targetColor)
                            {
                                break;
                            }

                            char targetPiece = board.GetPiece(targetFile, targetRank);
                            moves.Add(new Move(file, rank, targetFile, targetRank, targetPiece));

                            if (Definitions.ChessColor.Empty != targetColor)
                            {
                                break;
                            }

                            currentFile = targetFile;
                            currentRank = targetRank;
                        }
                    }
                    break;

                case Definitions.PAWN:
                    directionSequences = GetMoveDirectionSequence(pieceLower);
                    int twoFieldMoveInitRank = 2;
                    foreach (string sequence in directionSequences)
                    {
                        string currentSequence = sequence;
                        if (pieceColor == Definitions.ChessColor.Black)
                        {
                            currentSequence = sequence.Replace('u', 'd');
                            twoFieldMoveInitRank = 7;
                        }

                        GetEndPosition(file, rank, currentSequence, out targetFile, out targetRank, out valid);
                        if (currentSequence == "u" || currentSequence == "d") // walk straight one field
                        {
                            if (valid && board.GetColor(targetFile, targetRank) == Definitions.ChessColor.Empty) // empty field
                            {
                                moves.Add(new Move(file, rank, targetFile, targetRank, Definitions.EmptyField));
                            }
                        }
                        else if ((currentSequence == "uu" || currentSequence == "dd") && rank == twoFieldMoveInitRank) // walk straight two fields
                        {
                            int targetFile2 = 0;
                            int targetRank2 = 0;
                            bool valid2 = false;
                            string currentSequence2 = currentSequence == "uu" ? "u" : "d";
                            GetEndPosition(file, rank, currentSequence2, out targetFile2, out targetRank2, out valid2);
                            if ((valid && board.GetColor(targetFile, targetRank) == Definitions.ChessColor.Empty) && // end field is empty
                                (valid2 && board.GetColor(targetFile2, targetRank2) == Definitions.ChessColor.Empty)) // field between current and end field is also empty
                            {
                                moves.Add(new Move(file, rank, targetFile, targetRank, Definitions.EmptyField));
                            }
                        }
                        else if (currentSequence == "ul" || currentSequence == "ur" ||
                                 currentSequence == "dl" || currentSequence == "dr") // capture
                        {
                            if (valid && pieceColor != board.GetColor(targetFile, targetRank) && board.GetColor(targetFile, targetRank) != Definitions.ChessColor.Empty) // other color
                            {
                                moves.Add(new Move(file, rank, targetFile, targetRank, board.GetPiece(targetFile, targetRank)));
                            }
                        }
                    }
                    break;
            }

            return moves;
        }

        public Move GetValidMove(Board board, string moveStringUser) // input is like "e2e4"
        {
            Move move = new Move(moveStringUser);
            move.CapturedPiece = board.GetPiece(move.TargetFile, move.TargetRank);
            return move;
        }

        public bool IsMoveValid(Board board, Move move)
        {
            bool valid = GetMoves(board, move.SourceFile, move.SourceRank).Contains(move);
            return valid;
        }

        private List<string> GetMoveDirectionSequence(char piece)
        {
            List<string> sequence;
            switch (piece)
            {
                case Definitions.KNIGHT:
                    sequence = new List<string>() { "uul", "uur", "rru", "rrd", "ddr", "ddl", "lld", "llu" }; // up up left, up up right, ...
                    break;
                case Definitions.ROOK:
                    sequence = new List<string>() { "u", "r", "d", "l" }; // up, right, down, left
                    break;
                case Definitions.QUEEN:
                case Definitions.KING:
                    sequence = new List<string>() { "u", "ur", "r", "rd", "d", "dl", "l", "lu" }; // up, up right, right, right down, ...
                    break;
                case Definitions.BISHOP:
                    sequence = new List<string>() { "ur", "rd", "dl", "lu" }; // up right, right down, down left, left up
                    break;
                case Definitions.PAWN:
                    sequence = new List<string>() { "u", "uu", "ul", "ur" }; // up, up up, up left, up right
                    break;

                default:
                    sequence = new List<string>();
                    break;
            }

            return sequence;
        }

        // unit tests need access.
        internal void GetEndPosition(int file, int rank, string sequence, out int targetFile, out int targetRank, out bool valid)
        {
            targetFile = file;
            targetRank = rank;

            for (int i = 0; i < sequence.Length; i++)
            {
                char direction = sequence[i];
                switch (direction)
                {
                    case Definitions.UP:
                        targetRank++;
                        break;
                    case Definitions.RIGHT:
                        targetFile++;
                        break;
                    case Definitions.DOWN:
                        targetRank--;
                        break;
                    case Definitions.LEFT:
                        targetFile--;
                        break;
                    default:
                        break;
                }
            }

            valid = targetFile >= 1 && targetFile <= 8 &&
                    targetRank >= 1 && targetRank <= 8;
        }
    }
}
