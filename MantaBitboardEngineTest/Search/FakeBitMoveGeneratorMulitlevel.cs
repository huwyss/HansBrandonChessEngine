using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MantaBitboardEngine;
using MantaCommon;

namespace MantaBitboardEngineTest{
    public class FakeBitMoveGeneratorMulitlevel : IMoveGenerator<BitMove>
    {
        private List<IEnumerable<BitMove>> _listOfListOfMoves = new List<IEnumerable<BitMove>>();
        private IEnumerator<IEnumerable<BitMove>> _iteratorMoves;
        private IEnumerator<bool> _iteratorIsChecks;

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

        public void SetIsChecks(IEnumerable<bool> isChecksToReturn)
        {
            _iteratorIsChecks = isChecksToReturn.GetEnumerator();
        }

        public IEnumerable<BitMove> GetAllMoves(ChessColor color) ////, bool includeCastling = true, bool includePawnMoves = true)
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
            _iteratorIsChecks.MoveNext();
            return _iteratorIsChecks.Current;
        }

        public IEnumerable<BitMove> GetAllCaptures(ChessColor color)
        {
            throw new NotImplementedException();
        }

        ////public IEnumerable<IMove> GetLegalMoves(IBoard board, ChessColor color)
        ////{
        ////    _iteratorMoves.MoveNext();

        ////    if (_iteratorMoves.Current.Count() == 0)
        ////    {
        ////        return new List<IMove>();
        ////    }

        ////    if (_iteratorMoves.Current.First().MovingColor != color)
        ////    {
        ////        throw new Exception("Expected move of different color!");
        ////    }

        ////    return (List<IMove>)_iteratorMoves.Current;
        ////}
    }
}
