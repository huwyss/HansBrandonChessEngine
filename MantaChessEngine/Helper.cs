using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MantaChessEngine
{
    public class Helper
    {
        public static char FileToFileChar(int file)
        {
            char fileChar = (char)(file - 1 + 'a');
            return fileChar;
        }

        public static int FileCharToFile(char fileChar)
        {
            int file = fileChar + 1 - 'a';
            return file;
        }

        public static Definitions.ChessColor GetOpositeColor(Definitions.ChessColor color)
        {
            Definitions.ChessColor oposite = Definitions.ChessColor.Empty;

            if (color == Definitions.ChessColor.White)
            {
                oposite = Definitions.ChessColor.Black;
            }
            else if (color == Definitions.ChessColor.Black)
            {
                oposite = Definitions.ChessColor.White;
            }

            return oposite;
        }

        public static Definitions.ChessColor GetPieceColor(char piece)
        {
            return piece < 'a' ? Definitions.ChessColor.White : Definitions.ChessColor.Black; 
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
