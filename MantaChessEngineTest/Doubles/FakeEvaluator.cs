using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MantaChessEngine;

namespace MantaChessEngineTest.Doubles
{
    public class FakeEvaluator : IEvaluator
    {
        private IEnumerator<int> _enumerator;

        public FakeEvaluator(IEnumerable<int> scores)
        {
            _enumerator = scores.GetEnumerator();
            EvaluateCalledCounter = 0;
        }

        public int Evaluate(IBoard board)
        {
            EvaluateCalledCounter++;
            _enumerator.MoveNext();
            return _enumerator.Current;
        }

        public int EvaluateCalledCounter {get;set;}
    }
}
