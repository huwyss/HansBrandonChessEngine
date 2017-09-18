using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

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

        // Note: Manta is a king capture engine. 
        // This means even if we are in check then also moves that do not remove the check are returned here.
        public List<MoveBase> GetAllMoves(Board board, Definitions.ChessColor color, bool includeCastling = true)
        {
            var allMovesUnchecked = GetAllMovesUnchecked(board, color, includeCastling);
            return allMovesUnchecked;
        }

        private List<MoveBase> GetAllMovesUnchecked(Board board, Definitions.ChessColor color, bool includeCastling = true)
        {
            List<MoveBase> allMoves = new List<MoveBase>();

            for (int file = 1; file <= 8; file++)
            {
                for (int rank = 1; rank <= 8; rank++)
                {
                    if (board.GetColor(file, rank) == color)
                    {
                        Piece piece = board.GetPiece(file, rank);
                        if (piece is Knight || piece is King)
                        {
                            allMoves.AddRange(piece.GetMoves(this, board, file, rank, includeCastling));
                        }
                        else
                        {
                            allMoves.AddRange(GetMoves(board, file, rank, includeCastling));
                        }
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
        public List<MoveBase> GetMoves(Board board, int file, int rank, bool includeCastling = true)
        {
            List<MoveBase> moves = new List<MoveBase>();
            Piece piece = board.GetPiece(file, rank);
            int targetRank;
            int targetFile;
            bool valid;
            Definitions.ChessColor pieceColor = board.GetColor(file, rank);
            IEnumerable<string> directionSequences;
            char pieceLower = piece != null ? piece.Symbol.ToString().ToLower()[0] : Definitions.EmptyField;
            switch (pieceLower)
            {
                case Definitions.KNIGHT:
                case Definitions.KING:
                    //return piece.GetMoves(this, board, file, rank, includeCastling);
                    //directionSequences = piece.GetMoveDirectionSequences(); 
                    //foreach (string sequence in directionSequences)
                    //{
                    //    GetEndPosition(file, rank, sequence, out targetFile, out targetRank, out valid);
                    //    if (valid && pieceColor != board.GetColor(targetFile, targetRank)) // capture or empty field
                    //    {
                    //        moves.Add(MoveFactory.MakeNormalMove(piece, file, rank, targetFile, targetRank, board.GetPiece(targetFile, targetRank)));
                    //    }
                    //}

                    //if (!includeCastling)
                    //{
                    //    break;
                    //}

                    //// Castling
                    //if (piece is King && piece.Color == Definitions.ChessColor.White) // white king
                    //{
                    //    // check for king side castling (0-0)
                    //    Piece maybeWhiteKingRook = board.GetPiece(Helper.FileCharToFile('h'), 1);
                    //    if (board.CastlingRightWhiteKingSide && // castling right
                    //        file == Helper.FileCharToFile('e') && rank == 1 && // king initial position
                    //        maybeWhiteKingRook is Rook && maybeWhiteKingRook.Color == Definitions.ChessColor.White && // rook init position
                    //        IsFieldsEmpty(board, Helper.FileCharToFile('f'), 1, Helper.FileCharToFile('g')) && // fields between king and rook empty
                    //        !IsAttacked(board, pieceColor, Helper.FileCharToFile('e'), 1) && // king not attacked
                    //        !IsAttacked(board, pieceColor, Helper.FileCharToFile('f'), 1)
                    //        )
                    //    {
                    //        moves.Add(_factory.MakeCastlingMove(CastlingType.WhiteKingSide));
                    //    }

                    //    // check for queen side castling (0-0-0)
                    //    Piece maybeWhiteQueenRook = board.GetPiece(Helper.FileCharToFile('a'), 1);
                    //    if (board.CastlingRightWhiteQueenSide && // castling right
                    //        file == Helper.FileCharToFile('e') && rank == 1 && // king initial position
                    //        maybeWhiteQueenRook is Rook && maybeWhiteQueenRook.Color == Definitions.ChessColor.White && // rook init position
                    //        IsFieldsEmpty(board, Helper.FileCharToFile('b'), 1, Helper.FileCharToFile('d')) &&// fields between king and rook empty
                    //        !IsAttacked(board, pieceColor, Helper.FileCharToFile('e'), 1) && // king not attacked
                    //        !IsAttacked(board, pieceColor, Helper.FileCharToFile('d'), 1)
                    //        )
                    //    {
                    //        moves.Add(_factory.MakeCastlingMove(CastlingType.WhiteQueenSide));
                    //    }
                    //}

                    //Piece maybeBlackKingRook = board.GetPiece(Helper.FileCharToFile('h'), 8);
                    //if (piece is King && piece.Color == Definitions.ChessColor.Black) // black king
                    //{
                    //    // check for king side castling (0-0)
                    //    if (board.CastlingRightBlackKingSide && // castling right
                    //        file == Helper.FileCharToFile('e') && rank == 8 && // king initial position
                    //        maybeBlackKingRook is Rook && maybeBlackKingRook.Color == Definitions.ChessColor.Black && // rook init position
                    //        IsFieldsEmpty(board, Helper.FileCharToFile('f'), 8, Helper.FileCharToFile('g')) && // fields between king and rook empty
                    //        !IsAttacked(board, pieceColor, Helper.FileCharToFile('e'), 8) && // king not attacked
                    //        !IsAttacked(board, pieceColor, Helper.FileCharToFile('f'), 8)    // field next to king not attacked
                    //    )
                    //    {
                    //        moves.Add(_factory.MakeCastlingMove(CastlingType.BlackKingSide));
                    //    }

                    //    // check for queen side castling (0-0-0)
                    //    Piece maybeBlackQueenRook = board.GetPiece(Helper.FileCharToFile('a'), 8);
                    //    if (board.CastlingRightBlackQueenSide && // castling right
                    //        file == Helper.FileCharToFile('e') && rank == 8 && // king initial position
                    //        maybeBlackQueenRook is Rook && maybeBlackQueenRook.Color == Definitions.ChessColor.Black && // rook init position
                    //        IsFieldsEmpty(board, Helper.FileCharToFile('b'), 8, Helper.FileCharToFile('d')) && // fields between king and rook empty
                    //        !IsAttacked(board, pieceColor, Helper.FileCharToFile('e'), 8) && // king not attacked
                    //        !IsAttacked(board, pieceColor, Helper.FileCharToFile('d'), 8)    // field next to king not attacked
                    //    )
                    //    {
                    //        moves.Add(_factory.MakeCastlingMove(CastlingType.BlackQueenSide));
                    //    }
                    //}
                    break;

                case Definitions.ROOK: 
                case Definitions.QUEEN:
                case Definitions.BISHOP:
                    directionSequences = piece.GetMoveDirectionSequences();
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

                            Piece targetPiece = board.GetPiece(targetFile, targetRank);
                            moves.Add(MoveFactory.MakeNormalMove(piece, file, rank, targetFile, targetRank, targetPiece));

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
                    directionSequences = piece.GetMoveDirectionSequences();
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
                                moves.Add(MoveFactory.MakeNormalMove(piece, file, rank, targetFile, targetRank, null));
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
                                moves.Add(MoveFactory.MakeNormalMove(piece, file, rank, targetFile, targetRank, null));
                            }
                        }
                        else if (currentSequence == "ul" || currentSequence == "ur" ||
                                 currentSequence == "dl" || currentSequence == "dr") // capture
                        {
                            if (valid && pieceColor == Helper.GetOpositeColor(board.GetColor(targetFile, targetRank)))
                            {
                                moves.Add(MoveFactory.MakeNormalMove(piece, file, rank, targetFile, targetRank, board.GetPiece(targetFile, targetRank)));
                            }
                            else if (valid && targetFile == board.History.LastEnPassantFile && targetRank == board.History.LastEnPassantRank)
                            {
                                Piece capturedPawn = pieceColor == Definitions.ChessColor.White
                                    ? Piece.MakePiece(Definitions.PAWN.ToString().ToLower()[0])
                                    : Piece.MakePiece(Definitions.PAWN.ToString().ToUpper()[0]);
                                moves.Add(_factory.MakeEnPassantCaptureMove(piece, file, rank, targetFile, targetRank, capturedPawn));
                            }

                        }
                    }
                    break;
            }

            return moves;
        }

        public bool IsMoveValid(Board board, MoveBase move)
        {
            bool valid = HasCorrectColorMoved(board, move);
            valid &= GetMoves(board, move.SourceFile, move.SourceRank).Contains(move);
            return valid;
        }

        private bool HasCorrectColorMoved(Board board, MoveBase move)
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
                empty &= board.GetPiece(file, sourceRank) == null; //Definitions.EmptyField;
            }

            return empty;
        }

        public bool IsAttacked(Board board, Definitions.ChessColor color, int file, int rank)
        {
            // find all oponent moves
            var moves = GetAllMoves(board, Helper.GetOpositeColor(color), false);

            foreach (MoveBase move in moves)
            {
                if (move.TargetFile == file && move.TargetRank == rank)
                {
                    return true;
                }
            }

            return false;
        }

        public bool IsCheck(Board board, Definitions.ChessColor color)
        {
            //char king;

            //if (color == Definitions.ChessColor.White)
            //{
            //    king = Definitions.KING.ToString().ToUpper()[0];
            //}
            //else
            //{
            //    king = Definitions.KING.ToString().ToLower()[0];
            //}

            // find all oponent moves
            var moves = GetAllMoves(board, Helper.GetOpositeColor(color), false);

            // if a move ends in king's position then king is in check
            foreach (MoveBase move in moves)
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
