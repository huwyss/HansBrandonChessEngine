using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaracudaChessEngine
{
    public class Move
    {
        public int SourceFile { get; set; }
        // char SourceFileChar { get; set; }
        public int SourceRank { get; set; }
        public int TargetFile { get; set; }
        // char TargetFileChar { get; set; }
        public int TargetRank { get; set; }
        public char CapturedPiece { get; set; }

        public Move(int sourceFile, int sourceRank, int targetFile, int targetRank, char capturedPiece)
        {
            SourceFile = sourceFile;
            SourceRank = sourceRank;
            TargetFile = targetFile;
            TargetRank = targetRank;
        }

        public override bool Equals(System.Object obj)
        {
            // If parameter is null return false.
            if (obj == null)
            {
                return false;
            }

            // If parameter cannot be cast to Move return false.
            Move other = obj as Move;
            if ((System.Object)other == null)
            {
                return false;
            }

            bool equal = SourceFile == other.SourceFile;
            equal &= SourceRank == other.SourceRank;
            equal &= TargetFile == other.TargetFile;
            equal &= TargetRank == other.TargetRank;
            return equal;
        }
    }
}
