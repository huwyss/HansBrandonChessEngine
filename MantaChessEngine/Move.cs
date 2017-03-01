using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace MantaChessEngine
{
    public class Move : IMove
    {
        public char MovingPiece { get; set; }
        public int SourceFile { get; set; }
        public int SourceRank { get; set; }
        public int TargetFile { get; set; }
        public int TargetRank { get; set; }
        public char CapturedPiece { get; set; }
        public bool EnPassant { get; set; }

        public Definitions.ChessColor Color
        {
            get
            {
                Definitions.ChessColor color = (MovingPiece >= 'A' && MovingPiece <= 'Z')
                    ? Definitions.ChessColor.White
                    : Definitions.ChessColor.Black;
                return color;
            }
        }

        public int CapturedFile // same as target file
        {
            get { return TargetFile; }
            
        } 

        public int CapturedRank // mostly this is the same as target rank but for en passant capture it is different
        {
            get
            {
                if (EnPassant)
                {
                    if (Color == Definitions.ChessColor.White)
                    {
                        return TargetRank - 1;
                    }

                    return TargetRank + 1;
                }

                return TargetRank;
            }
        }

        

        public Move(char movingPiece, int sourceFile, int sourceRank, int targetFile, int targetRank, char capturedPiece, bool enPassant = false)
        {
            MovingPiece = movingPiece;
            SourceFile = sourceFile;
            SourceRank = sourceRank;
            TargetFile = targetFile;
            TargetRank = targetRank;
            CapturedPiece = capturedPiece;
            EnPassant = enPassant;
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

            if (moveString.Length >= 6)
            {
                if (moveString[5] == 'e')
                {
                    EnPassant = true;
                }
            }

            MovingPiece = (char)0;
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
            equal &= EnPassant == other.EnPassant;

            // note: only check MovingPiece if they are set in both objects
            // new Move("a2a3") is equal to new Move('p', a, 2, a, 3, nocapture, enpassant=false)
            // --> this is useful for tests!
            if (MovingPiece != (char) 0 && other.MovingPiece != (char) 0)
            {
                equal &= MovingPiece == other.MovingPiece;
            }
            
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
            moveString += EnPassant ? "e" : "";
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
