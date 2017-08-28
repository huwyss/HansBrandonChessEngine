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
    }
}
