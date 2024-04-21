using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HansBrandonBitboardEngine;
using HBCommon;

namespace HansBrandonBitboardEngineTest{
    public class FakeBitMoveGeneratorMulitlevel : IMoveGenerator<BitMove>
    {
        private List<IEnumerable<BitMove>> _listOfListOfMoves = new List<IEnumerable<BitMove>>();
        private IEnumerator<IEnumerable<BitMove>> _iteratorMoves;
        private List<IEnumerable<BitMove>> _listOfListOfCaptures = new List<IEnumerable<BitMove>>();
        private IEnumerator<IEnumerable<BitMove>> _iteratorCaptures;
        private IEnumerator<bool> _iteratorIsChecks;
        private IEnumerator<bool> _iteratorIsChecksBlack;

        public FakeBitMoveGeneratorMulitlevel()
        {
            ReturnsIsValid = true;
            ReturnsIsAttacked = false;
        }

        public void AddGetAllMoves(IEnumerable<BitMove> moves)
        {
            _listOfListOfMoves.Add(moves);
            _iteratorMoves = _listOfListOfMoves.GetEnumerator();
        }

        public void AddGetAllCaptures(IEnumerable<BitMove> captures)
        {
            _listOfListOfCaptures.Add(captures);
            _iteratorCaptures = _listOfListOfCaptures.GetEnumerator();
        }

        public void SetIsChecks(ChessColor color, IEnumerable<bool> isChecksToReturn)
        {
            if (color == ChessColor.White)
            {
                _iteratorIsChecks = isChecksToReturn.GetEnumerator();
            }
            else
            {
                _iteratorIsChecksBlack = isChecksToReturn.GetEnumerator();
            }
        }

        public IEnumerable<BitMove> GetAllMoves(ChessColor color)
        {
            _iteratorMoves.MoveNext();

            if (_iteratorMoves.Current.Count() == 0)
            {
                return new List<BitMove>();
            }

            if (_iteratorMoves.Current.First().MovingColor != color)
            {
                throw new Exception("Expected move of different color!");
            }

            return (List<BitMove>)_iteratorMoves.Current;
        }

        public bool ReturnsIsValid { get; set; }
        public bool IsMoveValid(BitMove move)
        {
            return ReturnsIsValid;
        }

        public bool ReturnsIsAttacked { get; set; }
        public bool IsAttacked(ChessColor color, Square square)
        {
            return ReturnsIsAttacked;
        }

        public bool ReturnsIsCheck { get; set; }
        
        public bool IsCheck(ChessColor color)
        {
            if (color == ChessColor.White)
            {
                _iteratorIsChecks.MoveNext();
                return _iteratorIsChecks.Current;
            }
            else
            {
                _iteratorIsChecksBlack.MoveNext();
                return _iteratorIsChecksBlack.Current;
            }
        }

        public IEnumerable<BitMove> GetAllCaptures(ChessColor color)
        {
            _iteratorCaptures.MoveNext();

            if (_iteratorCaptures.Current.Count() == 0)
            {
                return new List<BitMove>();
            }

            if (_iteratorCaptures.Current.First().MovingColor != color)
            {
                throw new Exception("Expected capture move of different color!");
            }

            return (List<BitMove>)_iteratorCaptures.Current;
        }
    }
}
