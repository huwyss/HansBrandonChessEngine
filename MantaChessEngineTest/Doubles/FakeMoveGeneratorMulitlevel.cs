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
        private List<IEnumerable<IMove>> _listOfListOfCaptures = new List<IEnumerable<IMove>>();
        private IEnumerator<IEnumerable<IMove>> _iteratorCaptures;
        private IEnumerator<bool> _iteratorIsChecks;
        private IEnumerator<bool> _iteratorIsChecksBlack;

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

        public IEnumerable<IMove> GetAllMoves(ChessColor color)
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
        public bool IsAttacked(ChessColor color, int file, int rank)
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

        public IEnumerable<IMove> GetAllCaptures(ChessColor color)
        {
            _iteratorCaptures.MoveNext();

            if (_iteratorCaptures.Current.Count() == 0)
            {
                return new List<IMove>();
            }

            if (_iteratorCaptures.Current.First().MovingColor != color)
            {
                throw new Exception("Expected capture move of different color!");
            }

            return (List<IMove>)_iteratorCaptures.Current;
        }

        public bool IsAttacked(ChessColor color, Square square)
        {
            throw new NotImplementedException();
        }

        public bool IsMoveValid(IMove move)
        {
            throw new NotImplementedException();
        }
    }
}
