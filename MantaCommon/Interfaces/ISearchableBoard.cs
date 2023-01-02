using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MantaCommon
{
    public interface ISearchableBoard<TMove>
    {
        void Move(TMove nextMove);

        void Back();
    }
}
