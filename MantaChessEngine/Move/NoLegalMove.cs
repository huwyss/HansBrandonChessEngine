using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MantaChessEngine
{
    public class NoLegalMove : MoveBase
    {
        public NoLegalMove() : base(null, 'a', 1, 'a', 1, null)
        { }

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

        public override void ExecuteMove(IBoard board)
        {
        }

        public override void UndoMove(IBoard board)
        {
        }
    }

    public class KingCaptureMove : NoLegalMove
    {
        public KingCaptureMove() : base()
        { }

        public override bool Equals(System.Object obj)
        {
            IMove other = obj as KingCaptureMove;
            if (other != null)
            {
                return true;
            }

            return false;
        }

        public override string ToString()
        {
            return "KingCaptureMove";
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public override void ExecuteMove(IBoard board)
        {
        }

        public override void UndoMove(IBoard board)
        {
        }
    }
}
