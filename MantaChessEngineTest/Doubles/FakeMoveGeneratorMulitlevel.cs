using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MantaChessEngine;
using MantaCommon;

namespace MantaChessEngineTest.Doubles
{
    public class FakeMoveGeneratorMulitlevel : IMoveGenerator<IMove>
    {
        private List<IEnumerable<IMove>> _listOfListOfMoves = new List<IEnumerable<IMove>>();
        private IEnumerator<IEnumerable<IMove>> _iteratorMoves;
        private IEnumerator<bool> _iteratorIsChecks;

        public FakeMoveGeneratorMulitlevel()
        {
            ReturnsIsValid = true;
            ReturnsIsAttacked = false;
        }

        public void AddGetAllMoves(IEnumerable<IMove> moves)
        {
            _listOfListOfMoves.Add(moves);
            _iteratorMoves = _listOfListOfMoves.GetEnumerator();
        }

        public void SetIsChecks(IEnumerable<bool> isChecksToReturn)
        {
            _iteratorIsChecks = isChecksToReturn.GetEnumerator();
        }

        public IEnumerable<IMove> GetAllMoves(ChessColor color) ////, bool includeCastling = true, bool includePawnMoves = true)
        {
            _iteratorMoves.MoveNext();

            if (_iteratorMoves.Current.Count() == 0)
            {
                return new List<IMove>();
            }

            if (_iteratorMoves.Current.First().MovingColor != color)
            {
                throw new Exception("Expected move of different color!");
            }

            return (List<IMove>)_iteratorMoves.Current;
        }

        public bool ReturnsIsValid { get; set; }
        public bool IsMoveValid(IBoard board, IMove move)
        {
            return ReturnsIsValid;
        }

        public bool ReturnsIsAttacked { get; set; }
        public bool IsAttacked(IBoard board, ChessColor color, int file, int rank)
        {
            return ReturnsIsAttacked;
        }

        public bool ReturnsIsCheck { get; set; }public bool IsCheck(IBoard board, ChessColor color)
        {
            _iteratorIsChecks.MoveNext();
            return _iteratorIsChecks.Current;
        }

        public IEnumerable<IMove> GetAllCaptures(ChessColor color)
        {
            throw new NotImplementedException();
        }

        public bool IsAttacked(ChessColor color, Square square)
        {
            throw new NotImplementedException();
        }

        public bool IsCheck(ChessColor color)
        {
            throw new NotImplementedException();
        }

        public bool IsMoveValid(IMove move)
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
