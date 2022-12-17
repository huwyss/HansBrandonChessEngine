using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MantaCommon;

namespace MantaChessEngine
{
    /// <summary>
    /// Single step piece are Knight and King
    /// </summary>
    public abstract class SingleStepPiece : Piece
    {
        public SingleStepPiece(ChessColor color) : base(color)
        {
        }

        public override List<IMove> GetMoves(MoveGenerator moveGen, IBoard board, int file, int rank, bool includeCastling = true)
        {
            int targetRank;
            int targetFile;
            bool valid;
            List<IMove> moves = new List<IMove>();
            IEnumerable<string> directionSequences = GetMoveDirectionSequences();
            foreach (string sequence in directionSequences)
            {
                GetEndPosition(file, rank, sequence, out targetFile, out targetRank, out valid);
                if (valid && Color != board.GetColor(targetFile, targetRank)) // capture or empty field
                {
                    moves.Add(MoveFactory.MakeNormalMove(this, file, rank, targetFile, targetRank, board.GetPiece(targetFile, targetRank)));
                }
            }

            return moves;
        }
    }
}
