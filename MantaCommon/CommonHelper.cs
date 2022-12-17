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
    }

    public static class CommonDefinitions
    {
        public static char EmptyField = '.';
    }
}
