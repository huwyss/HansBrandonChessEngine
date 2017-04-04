using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MantaChessEngine
{
    public enum EngineType
    {
        Random,
        DepthHalf,
        DepthOne,
        Minmax,
        MinmaxPosition
    }

    public class MantaEngine
    {
        private Board _board;
        private MoveGenerator _moveGenerator;
        private ISearchService _search;
        private IEvaluator _evaluator;
        private MoveFactory _factory;

        public MantaEngine(EngineType engineType)
        {
            if (engineType == EngineType.Random)
            {
                _factory = new MoveFactory();
                _moveGenerator = new MoveGenerator(_factory);
                _board = new Board(_moveGenerator);
                _search = new SearchRandom();
            }
            else if (engineType == EngineType.DepthHalf)
            {
                _moveGenerator = new MoveGenerator(_factory);
                _board = new Board(_moveGenerator);
                _evaluator = new EvaluatorSimple();
                _search = new SearchServiceDepthHalfMove(_evaluator);
            }
            else if (engineType == EngineType.DepthOne)
            {
                _moveGenerator = new MoveGenerator(_factory);
                _board = new Board(_moveGenerator);
                _evaluator = new EvaluatorSimple();
                _search = new SearchServiceDepthOne(_evaluator);
            }
            else if (engineType == EngineType.Minmax)
            {
                _moveGenerator = new MoveGenerator(_factory);
                _board = new Board(_moveGenerator);
                _evaluator = new EvaluatorSimple();
                _search = new SearchMinimax(_evaluator);
            }
            else if (engineType == EngineType.MinmaxPosition)
            {
                _moveGenerator = new MoveGenerator(_factory);
                _board = new Board(_moveGenerator);
                _evaluator = new EvaluatorPosition();
                _search = new SearchMinimax(_evaluator);
            }
        }

        public void SetInitialPosition()
        {
            _board.SetInitialPosition();
        }

        public void SetPosition(string position)
        {
            _board.SetPosition(position);
        }

        public string GetString()
        {
            return _board.GetString;
        }

        public string GetPrintString()
        {
            return _board.GetPrintString;
        }

        public bool Move(string moveStringUser)
        {
            MoveBase syntaxCorrectMove = _board.GetCorrectMove(moveStringUser);
            if (syntaxCorrectMove == null)
            {
                return false;
            }

            bool valid = _board.IsMoveValid(syntaxCorrectMove);
            if (valid)
            {
                _board.Move(syntaxCorrectMove);
            }

            return valid;
        }

        public bool Move(IMove move)
        {
            _board.Move(move);
            return true;
        }

        public bool IsWinner(Definitions.ChessColor color)
        {
            return _board.IsWinner(color);
        }

        public IMove DoBestMove(Definitions.ChessColor color)
        {
            float score = 0;
            IMove nextMove = _search.Search(_board, color, out score);
            _board.Move(nextMove);
            Console.WriteLine("Score: " + score);
            return nextMove;
        }
        
        public Definitions.ChessColor SideToMove()
        {
            return _board.SideToMove;
        }

        public bool IsCheck(Definitions.ChessColor color)
        {
            return _board.IsCheck(color);
        }

        public void Back()
        {
            _board.Back();
            _board.Back();
        }
    }
}
