using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaracudaChessEngine
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
    }
}
