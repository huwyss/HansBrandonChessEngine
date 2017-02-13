using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaracudaChessEngine
{
    public interface IMove
    {
        bool Equals(System.Object obj);
        string ToString();
        int GetHashCode();
    }
}
