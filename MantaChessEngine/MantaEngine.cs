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
        MinmaxPosition,
        MinmaxSearchTree
    }

    public class MantaEngine
    {
        private MoveGenerator _moveGenerator;
        private MoveFactory _moveFactory;
        private ISearchService _search;
        private IEvaluator _evaluator;
        private Board _board;

        public MantaEngine(EngineType engineType)
        {
            _moveFactory = new MoveFactory();
            _moveGenerator = new MoveGenerator(_moveFactory);

            if (engineType == EngineType.Random)
            {
                _search = new SearchRandom(_moveGenerator);
            }
            else if (engineType == EngineType.DepthHalf)
            {
                _evaluator = new EvaluatorSimple();
                _search = new SearchServiceDepthHalfMove(_evaluator, _moveGenerator);
            }
            else if (engineType == EngineType.DepthOne)
            {
                _evaluator = new EvaluatorSimple();
                _search = new SearchServiceDepthOne(_evaluator, _moveGenerator);
            }
            else if (engineType == EngineType.Minmax)
            {
                _evaluator = new EvaluatorSimple();
                _search = new SearchMinimax(_evaluator, _moveGenerator);
            }
            else if (engineType == EngineType.MinmaxPosition)
            {
                _evaluator = new EvaluatorPosition();
                _search = new SearchMinimax(_evaluator, _moveGenerator);
            }
            else if (engineType == EngineType.MinmaxSearchTree)
            {
                _evaluator = new EvaluatorPosition();
                _search = new SearchMinimaxTree(_evaluator, _moveGenerator);
            }
        }

        public void SetBoard(Board board)
        {
            _board = board;
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
            IMove syntaxCorrectMove = _moveFactory.MakeCorrectMove(_board, moveStringUser);
            if (syntaxCorrectMove == null)
            {
                return false;
            }

            bool valid = _moveGenerator.IsMoveValid(_board, syntaxCorrectMove);
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
            return _moveGenerator.IsCheck(_board, color);
        }

        public void Back()
        {
            _board.Back();
            _board.Back();
        }
    }
}
