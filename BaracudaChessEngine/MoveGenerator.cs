using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaracudaChessEngine
{
    public class MoveGenerator
    {
        Board _board;

        public void SetBoard(Board board)
        {
            _board = board;
        }

        //public List<Move> GetAllMoves()
        //{
        //    List<Move> moves = new List<Move>();
        //    return moves;
        //}

        /// <summary>
        /// Returns all moves of that piece.
        /// </summary>
        /// <param name="file"></param>
        /// <param name="rank"></param>
        /// <returns></returns>
        /// todo: castling
        /// todo: pawn moves: move straight, move straight 2x, capture diagonal, capture en passant
        public List<Move> GetMoves(int file, int rank)
        {
            List<Move> moves = new List<Move>();
            char piece = _board.GetPiece(file, rank);
            int targetRank;
            int targetFile;
            bool valid;
            Definitions.ChessColor pieceColor = _board.GetColor(file, rank);
            List<string> directionSequences;
            char pieceLower = piece.ToString().ToLower()[0];
            switch (pieceLower)
            {
                case 'n': // Knight
                case 'k': // King
                    directionSequences = Helper.GetMoveDirectionSequence(pieceLower);
                    foreach (string sequence in directionSequences)
                    {
                        Helper.GetEndPosition(file, rank, sequence, out targetFile, out targetRank, out valid);
                        if (valid && pieceColor != _board.GetColor(targetFile, targetRank)) // capture or empty field
                        {
                            moves.Add(new Move(file, rank, targetFile, targetRank, _board.GetPiece(targetFile, targetRank)));
                        }
                    }
                    break;

                case 'r': // Rook
                case 'q': // queen
                case 'b': // Bishop
                    directionSequences = Helper.GetMoveDirectionSequence(pieceLower);
                    foreach (string sequence in directionSequences)
                    {
                        int currentFile = file;
                        int currentRank = rank;
                        for (int i = 1; i < 8; i++) // walk in the direction until off board or captured or next is own piece
                        {
                            Helper.GetEndPosition(currentFile, currentRank, sequence, out targetFile, out targetRank, out valid);
                            if (!valid)
                            {
                                break;
                            }
                            Definitions.ChessColor targetColor = _board.GetColor(targetFile, targetRank);
                            if (pieceColor == targetColor)
                            {
                                break;
                            }

                            char targetPiece = _board.GetPiece(targetFile, targetRank);
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

            }

            return moves;
        }
    }
}
