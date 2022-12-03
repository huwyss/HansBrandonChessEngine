using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MantaChessEngine.BitboardEngine
{
    public class BitHelper
    {
        public static BitColor OtherColor(BitColor color)
        {
            return color == BitColor.White ? BitColor.Black : BitColor.White;
        }
    }
}
