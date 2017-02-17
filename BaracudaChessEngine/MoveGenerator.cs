using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        public List<Move> GetAllMoves(Board board, Definitions.ChessColor color, bool includeCastling = true)
        {
            var allMovesUnchecked = GetAllMovesUnchecked(board, color, includeCastling);
            return allMovesUnchecked;
        }

        private List<Move> GetAllMovesUnchecked(Board board, Definitions.ChessColor color, bool includeCastling = true)
        {
            List<Move> allMoves = new List<Move>();

            for (int file = 1; file <= 8; file++)
            {
                for (int rank = 1; rank <= 8; rank++)
                {
                    if (board.GetColor(file, rank) == color)
                    {
                        allMoves.AddRange(GetMoves(board, file, rank, includeCastling));
                    }
                }
            }

            return allMoves;
        }

        /// <summary>
        /// Returns all pseudo legal moves of that piece. (King might be under attack).
        /// </summary>
        /// <param name="file"></param>
        /// <param name="rank"></param>
        /// <returns></returns>
        /// todo: pawn promotion
        public List<Move> GetMoves(Board board, int file, int rank, bool includeCastling = true)
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
                            moves.Add(new Move(piece, file, rank, targetFile, targetRank, board.GetPiece(targetFile, targetRank)));
                        }
                    }

                    if (!includeCastling)
                    {
                        break;
                    }

                    // Castling
                    if (piece == Definitions.KING.ToString().ToUpper()[0]) // white king
                    {
                        // check for king side castling (0-0)
                        if (board.CastlingRightWhiteKingSide && // castling right
                            file == Helper.FileCharToFile('e') && rank == 1 && // king initial position
                            Definitions.ROOK.ToString().ToUpper()[0] == board.GetPiece(Helper.FileCharToFile('h'), 1) && // rook init position
                            IsFieldsEmpty(board, Helper.FileCharToFile('f'), 1, Helper.FileCharToFile('g')) && // fields between king and rook empty
                            !IsAttacked(board, pieceColor, Helper.FileCharToFile('e'), 1) && // king not attacked
                            !IsAttacked(board, pieceColor, Helper.FileCharToFile('f'), 1)
                            )
                        {
                            moves.Add(new Move(piece, file, rank, Helper.FileCharToFile('g'), 1, Definitions.EmptyField));
                        }

                        // check for queen side castling (0-0-0)
                        if (board.CastlingRightWhiteQueenSide && // castling right
                            file == Helper.FileCharToFile('e') && rank == 1 && // king initial position
                            Definitions.ROOK.ToString().ToUpper()[0] == board.GetPiece(Helper.FileCharToFile('a'), 1) && // rook init position
                            IsFieldsEmpty(board, Helper.FileCharToFile('b'), 1, Helper.FileCharToFile('d')) &&// fields between king and rook empty
                            !IsAttacked(board, pieceColor, Helper.FileCharToFile('e'), 1) && // king not attacked
                            !IsAttacked(board, pieceColor, Helper.FileCharToFile('d'), 1)
                            )
                        {
                            moves.Add(new Move(piece, file, rank, Helper.FileCharToFile('c'), 1, Definitions.EmptyField));
                        }
                    }

                    if (piece == Definitions.KING.ToString().ToLower()[0]) // black king
                    {
                        // check for king side castling (0-0)
                        if (board.CastlingRightBlackKingSide && // castling right
                            file == Helper.FileCharToFile('e') && rank == 8 && // king initial position
                            Definitions.ROOK.ToString().ToLower()[0] == board.GetPiece(Helper.FileCharToFile('h'), 8) && // rook init position
                            IsFieldsEmpty(board, Helper.FileCharToFile('f'), 8, Helper.FileCharToFile('g')) && // fields between king and rook empty
                            !IsAttacked(board, pieceColor, Helper.FileCharToFile('e'), 8) && // king not attacked
                            !IsAttacked(board, pieceColor, Helper.FileCharToFile('f'), 8)    // field next to king not attacked
                        )
                        {
                            moves.Add(new Move(piece, file, rank, Helper.FileCharToFile('g'), 8, Definitions.EmptyField));
                        }

                        // check for queen side castling (0-0-0)
                        if (board.CastlingRightBlackQueenSide && // castling right
                            file == Helper.FileCharToFile('e') && rank == 8 && // king initial position
                            Definitions.ROOK.ToString().ToLower()[0] == board.GetPiece(Helper.FileCharToFile('a'), 8) && // rook init position
                            IsFieldsEmpty(board, Helper.FileCharToFile('b'), 8, Helper.FileCharToFile('d')) && // fields between king and rook empty
                            !IsAttacked(board, pieceColor, Helper.FileCharToFile('e'), 8) && // king not attacked
                            !IsAttacked(board, pieceColor, Helper.FileCharToFile('d'), 8)    // field next to king not attacked
                        )
                        {
                            moves.Add(new Move(piece, file, rank, Helper.FileCharToFile('c'), 8, Definitions.EmptyField));
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
                            moves.Add(new Move(piece, file, rank, targetFile, targetRank, targetPiece));

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
                                moves.Add(new Move(piece, file, rank, targetFile, targetRank, Definitions.EmptyField));
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
                                moves.Add(new Move(piece, file, rank, targetFile, targetRank, Definitions.EmptyField));
                            }
                        }
                        else if (currentSequence == "ul" || currentSequence == "ur" ||
                                 currentSequence == "dl" || currentSequence == "dr") // capture
                        {
                            if (valid && pieceColor == Helper.GetOpositeColor(board.GetColor(targetFile, targetRank)))
                            {
                                moves.Add(new Move(piece, file, rank, targetFile, targetRank, board.GetPiece(targetFile, targetRank)));
                            }
                            else if (valid && targetFile == board.History.LastEnPassantFile && targetRank == board.History.LastEnPassantRank)
                            {
                                char capturedPawn = pieceColor == Definitions.ChessColor.White
                                    ? Definitions.PAWN.ToString().ToLower()[0]
                                    : Definitions.PAWN.ToString().ToUpper()[0];
                                moves.Add(new Move(piece, file, rank, targetFile, targetRank, capturedPawn, true));
                            }

                        }
                    }
                    break;
            }

            return moves;
        }

        public Move GetCorrectMove(Board board, string moveStringUser) // input is like "e2e4"
        {
            if (Move.IsCorrectMove(moveStringUser))
            {
                Move move = new Move(moveStringUser);
                move.MovingPiece = board.GetPiece(move.SourceFile, move.SourceRank);

                if (board.GetColor(move.TargetFile, move.TargetRank) == Definitions.ChessColor.Empty &&
                    board.History.LastEnPassantFile == move.TargetFile && board.History.LastEnPassantRank == move.TargetRank)
                {
                    move.CapturedPiece = board.GetColor(move.SourceFile, move.SourceRank) == Definitions.ChessColor.White
                        ? Definitions.PAWN.ToString().ToLower()[0]
                        : Definitions.PAWN.ToString().ToUpper()[0];
                    move.EnPassant = true;
                }
                else
                {
                    move.CapturedPiece = board.GetPiece(move.TargetFile, move.TargetRank);
                }

                return move;
            }

            return null;
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

        private bool IsFieldsEmpty(Board board, int sourceFile, int sourceRank, int targetFile)
        {
            bool empty = true;

            for (int file = sourceFile; file <= targetFile; file++)
            {
                empty &= board.GetPiece(file, sourceRank) == Definitions.EmptyField;
            }

            return empty;
        }

        private bool IsAttacked(Board board, Definitions.ChessColor color, int file, int rank)
        {
            return board.IsAttacked(color, file, rank);
        }
    }
}
