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

        public static Definitions.ChessColor GetOppositeColor(Definitions.ChessColor color)
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
            bool correct = IsSourceAndTargetPositionValid(moveString);

            if (moveString.Length >= 5)
            {
                char capturedPiece = moveString[4].ToString().ToLower()[0];
                correct &= IsValidPiece(capturedPiece);
            }

            return correct;
        }

        public static bool IsCorrectMoveUci(string moveStringUci)
        {
            bool correct = IsSourceAndTargetPositionValid(moveStringUci);

            if (moveStringUci.Length == 5)
            {
                char promotionPiece = moveStringUci[4].ToString().ToLower()[0];
                correct &= IsValidPromotionPiece(promotionPiece);
            }

            return correct;
        }

        private static bool IsSourceAndTargetPositionValid(string moveString)
        {
            if (moveString.Length < 4)
            {
                return false;
            }

            return moveString[0] >= 'a' && moveString[0] <= 'h'
                && moveString[1] >= '1' && moveString[1] <= '8'
                && moveString[2] >= 'a' && moveString[2] <= 'h'
                && moveString[3] >= '1' && moveString[3] <= '8';
        }

        private static bool IsValidPiece(char piece)
        {
            return piece == Definitions.KING ||
                   piece == Definitions.QUEEN ||
                   piece == Definitions.ROOK ||
                   piece == Definitions.BISHOP ||
                   piece == Definitions.KNIGHT ||
                   piece == Definitions.PAWN;
        }

        private static bool IsValidPromotionPiece(char piece)
        {
            return piece == Definitions.QUEEN ||
                   piece == Definitions.ROOK ||
                   piece == Definitions.BISHOP ||
                   piece == Definitions.KNIGHT;
        }
    }
}
