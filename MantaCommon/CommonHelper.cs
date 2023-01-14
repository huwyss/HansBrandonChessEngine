using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MantaCommon
{
    public enum ChessColor : byte
    {
        White,
        Black,
        Empty
    }

    public class CommonHelper
    {
        public static ChessColor OtherColor(ChessColor color)
        {
            return color == ChessColor.White ? ChessColor.Black : ChessColor.White;
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
            return piece == CommonDefinitions.KING ||
                   piece == CommonDefinitions.QUEEN ||
                   piece == CommonDefinitions.ROOK ||
                   piece == CommonDefinitions.BISHOP ||
                   piece == CommonDefinitions.KNIGHT ||
                   piece == CommonDefinitions.PAWN;
        }

        private static bool IsValidPromotionPiece(char piece)
        {
            return piece == CommonDefinitions.QUEEN ||
                   piece == CommonDefinitions.ROOK ||
                   piece == CommonDefinitions.BISHOP ||
                   piece == CommonDefinitions.KNIGHT;
        }

        public static Square GetSquare(string fieldString)
        {
            if (fieldString.Length != 2)
            {
                throw new MantaEngineException($"Illegal field string: {fieldString}");
            }

            return (Square)(FileCharToFile(fieldString[0]) + 8 * (int.Parse(fieldString[1].ToString()) - 1));
        }

        private static int FileCharToFile(char fileChar)
        {
            int file = fileChar - 'a';
            return file;
        }

        public static PieceType GetPieceType(char symbol)
        {
            switch (symbol)
            {
                case 'P':
                case 'p':
                    return PieceType.Pawn;
                case 'N':
                case 'n':
                    return PieceType.Knight;
                case 'B':
                case 'b':
                    return PieceType.Bishop;
                case 'R':
                case 'r':
                    return PieceType.Rook;
                case 'Q':
                case 'q':
                    return PieceType.Queen;
                case 'K':
                case 'k':
                    return PieceType.King;
                default:
                    return PieceType.Empty;
            }
        }
    }

    public static class CommonDefinitions
    {
        public static char EmptyField = '.';

        public const char KING = 'k';
        public const char PAWN = 'p';
        public const char KNIGHT = 'n';
        public const char ROOK = 'r';
        public const char QUEEN = 'q';
        public const char BISHOP = 'b';
    }
}
