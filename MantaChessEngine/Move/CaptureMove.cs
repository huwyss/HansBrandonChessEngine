using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MantaChessEngine
{
    public class CaptureMove : Move
    {
        public CaptureMove(char movingPiece, int sourceFile, int sourceRank, int targetFile, int targetRank, char capturedPiece)
            : base(movingPiece, sourceFile, sourceRank, targetFile, targetRank, capturedPiece, false)
        {
        }
    }
}
