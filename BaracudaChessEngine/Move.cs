using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaracudaChessEngine
{
    

    public class Move : IMove
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
            CapturedPiece = capturedPiece;
        }

        public Move(string moveString)
        {
            if (moveString.Length >= 4)
            {
                SourceFile = Helper.FileCharToFile(moveString[0]);
                SourceRank = int.Parse(moveString[1].ToString());
                TargetFile = Helper.FileCharToFile(moveString[2]);
                TargetRank = int.Parse(moveString[3].ToString());
            }

            if (moveString.Length >= 5)
            {
                CapturedPiece = moveString[4];
            }
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
            equal &= CapturedPiece == other.CapturedPiece;
            return equal;
        }

        public override string ToString()
        {
            string moveString = "";
            moveString += Helper.FileToFileChar(SourceFile);
            moveString += SourceRank.ToString();
            moveString += Helper.FileToFileChar(TargetFile);
            moveString += TargetRank;
            moveString += CapturedPiece;
            return moveString;
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public static bool IsCorrectMove(string moveString)
        {
            bool correct = true;

            if (moveString.Length >= 4)
            {
                correct &= moveString[0] >= 'a';
                correct &= moveString[0] <= 'h';

                correct &= moveString[1] >= '1';
                correct &= moveString[1] <= '8';

                correct &= moveString[2] >= 'a';
                correct &= moveString[2] <= 'h';

                correct &= moveString[3] >= '1';
                correct &= moveString[3] <= '8';
            }
            else
            {
                return false;
            }

            if (moveString.Length >= 5)
            {
                char capturedPiece = moveString[4].ToString().ToLower()[0];
                correct &= capturedPiece == Definitions.KING ||
                           capturedPiece == Definitions.QUEEN ||
                           capturedPiece == Definitions.ROOK ||
                           capturedPiece == Definitions.BISHOP ||
                           capturedPiece == Definitions.KNIGHT ||
                           capturedPiece == Definitions.PAWN;
            }

            return correct;
        }
    }
}
