using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MantaChessEngine
{
    public class NormalMove : Move
    {
        public NormalMove(char movingPiece, int sourceFile, int sourceRank, int targetFile, int targetRank)
            : base(movingPiece, sourceFile, sourceRank, targetFile, targetRank, Definitions.EmptyField, false)
        {
        }

        public override void ExecuteMove(Board board)
        {
            base.ExecuteMove(board);
        }

        public override void UndoMove(Board board)
        {
            base.UndoMove(board);
        }
    }
}
