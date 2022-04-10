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
        Minimax,
        MinimaxPosition,
        MinimaxSearchTree,  // do not use
        MinimaxPositionContinueCapture
    }

    public class MantaEngine
    {
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private IMoveGenerator _moveGenerator;
        private MoveFactory _moveFactory;
        private ISearchService _search;
        private IEvaluator _evaluator;
        private Board _board;

        public MantaEngine(EngineType engineType)
        {
            _moveFactory = new MoveFactory();
            _moveGenerator = new MoveGenerator(_moveFactory);

            switch (engineType)
            {
                case EngineType.Random:
                    _search = new SearchRandom(_moveGenerator);
                    break;

                case EngineType.Minimax:
                    _evaluator = new EvaluatorSimple();
                    _search = new SearchMinimax(_evaluator, _moveGenerator);
                    break;

                // strongest --------------------------------
                case EngineType.MinimaxPosition:
                    _evaluator = new EvaluatorPosition();
                    _search = new SearchMinimax(_evaluator, _moveGenerator);
                    break;
                // --------------------------------------
                case EngineType.MinimaxPositionContinueCapture:
                    _evaluator = new EvaluatorPosition();
                    _search = new SearchMinimaxContinueCapture(_evaluator, _moveGenerator);
                    break;

                case EngineType.MinimaxSearchTree:
                    _evaluator = new EvaluatorPosition();
                    _search = new SearchMinimaxTree(_evaluator, _moveGenerator);
                    break;

                default:
                    throw new Exception("No engine type defined.");
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
            return _board.GetPrintString2;
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

        public IEvaluatedMove DoBestMove(Definitions.ChessColor color)
        {
            float score = 0;
            IMove nextMove = _search.Search(_board, color, out score);
            _board.Move(nextMove);
            _log.Debug("Score: " + score);

            return new EvaluatedMove() { Move = nextMove, Score = score };
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

        public void SetMaxSearchDepth(int depth)
        {
            if (_search != null)
            {
                _search.SetMaxDepth(depth);
            }
        }
    }
}
