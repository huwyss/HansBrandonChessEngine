using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MantaChessEngine
{
    public class NoLegalMove : IMove
    {
        public override bool Equals(System.Object obj)
        {
            IMove other = obj as NoLegalMove;
            if (other != null)
            {
                return true;
            }

            return false;
        }

        public override string ToString()
        {
            return "NoLegalMove";
        }
        
        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }
    }
}
