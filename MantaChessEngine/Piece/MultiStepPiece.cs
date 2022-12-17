using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MantaCommon;

namespace MantaChessEngine
{
    public abstract class MultiStepPiece : Piece
    {
        /// <summary>
        /// Multistep pieces are queen, rook and bishop
        /// </summary>
        public MultiStepPiece(ChessColor color) : base(color)
        {
        }

        public override List<IMove> GetMoves(MoveGenerator moveGen, IBoard board, int file, int rank, bool includeCastling = true)
        {
            List<IMove> moves = new List<IMove>();
            var directionSequences = GetMoveDirectionSequences();
            foreach (string sequence in directionSequences)
            {
                int currentFile = file;
                int currentRank = rank;
                for (int i = 1; i < 8; i++) // walk in the direction until off board or captured or next is own piece
                {
                    GetEndPosition(currentFile, currentRank, sequence, out int targetFile, out int targetRank, out bool valid);
                    if (!valid)
                    {
                        break;
                    }
                    ChessColor targetColor = board.GetColor(targetFile, targetRank);
                    if (Color == targetColor)
                    {
                        break;
                    }

                    Piece targetPiece = board.GetPiece(targetFile, targetRank);
                    moves.Add(MoveFactory.MakeNormalMove(this, file, rank, targetFile, targetRank, targetPiece));

                    if (ChessColor.Empty != targetColor)
                    {
                        break;
                    }

                    currentFile = targetFile;
                    currentRank = targetRank;
                }
            }

            return moves;
        }
    }
}
