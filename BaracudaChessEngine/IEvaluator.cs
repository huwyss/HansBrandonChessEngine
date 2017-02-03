using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaracudaChessEngine
{
    interface IEvaluator
    {
        Score Evaluate(Board board);
    }
}
