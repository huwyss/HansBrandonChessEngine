using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MantaCommon;

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

        public static ChessColor GetOppositeColor(ChessColor color)
        {
            ChessColor oposite = ChessColor.Empty;

            if (color == ChessColor.White)
            {
                oposite = ChessColor.Black;
            }
            else if (color == ChessColor.Black)
            {
                oposite = ChessColor.White;
            }

            return oposite;
        }

        public static ChessColor GetPieceColor(char piece)
        {
            return piece < 'a' ? ChessColor.White : ChessColor.Black;
        }
    }
}
