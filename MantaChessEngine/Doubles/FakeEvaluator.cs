using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MantaChessEngine
{
    public class FakeEvaluator : IEvaluator
    {
        private IEnumerator<float> _enumerator;

        public FakeEvaluator(IEnumerable<float> scores)
        {
            _enumerator = scores.GetEnumerator();
        }

        public float Evaluate(IBoard board)
        {
            _enumerator.MoveNext();
            return _enumerator.Current;
        }
    }
}
