using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HBCommon;

namespace HansBrandonChessEngine
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

        /// <summary>
        /// Returns Rank 1 .. 8
        /// </summary>
        public static int GetRank(Square square)
        {
            var file = (int)(square) % 8;
            return (int)(square - file) / 8 + 1;
        }

        /// <summary>
        /// Returns File 1 .. 8
        /// </summary>
        public static int GetFile(Square square)
        {
            var file = (int)(square) % 8;
            return file + 1;
        }
    }
}
