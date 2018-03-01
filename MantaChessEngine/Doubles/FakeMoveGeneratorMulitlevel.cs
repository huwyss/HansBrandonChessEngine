using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MantaChessEngine.Doubles
{
    public class FakeMoveGeneratorMulitlevel : IMoveGenerator
    {
        private List<IEnumerable<IMove>> _listOfListOfMoves = new List<IEnumerable<IMove>>();
        private IEnumerator<IEnumerable<IMove>> _iterator;

        public FakeMoveGeneratorMulitlevel()
        {
            ReturnsIsValid = true;
            ReturnsIsAttacked = false;
            ReturnsIsCheck = false;
        }

        public void AddGetAllMoves(IEnumerable<IMove> moves)
        {
            _listOfListOfMoves.Add(moves);
            _iterator = _listOfListOfMoves.GetEnumerator();
        }

        public List<IMove> GetAllMoves(IBoard board, Definitions.ChessColor color, bool includeCastling = true)
        {
            _iterator.MoveNext();

            if (_iterator.Current.First().Color != color)
            {
                throw new Exception("Expected move of different color!");
            }

            return (List<IMove>)_iterator.Current;
        }

        public bool ReturnsIsValid { get; set; }
        public bool IsMoveValid(IBoard board, IMove move)
        {
            return ReturnsIsValid;
        }

        public bool ReturnsIsAttacked { get; set; }
        public bool IsAttacked(IBoard board, Definitions.ChessColor color, int file, int rank)
        {
            return ReturnsIsAttacked;
        }

        public bool ReturnsIsCheck { get; set; }
        public bool IsCheck(IBoard board, Definitions.ChessColor color)
        {
            return ReturnsIsCheck;
        }
    }
}
