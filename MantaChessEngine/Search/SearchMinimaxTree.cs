using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Activation;
using System.Text;
using System.Threading.Tasks;

namespace MantaChessEngine
{
    public enum BuildTreeState
    {
        AddMoves,
        GoDown,
        GoUp
    }

    public class SearchMinimaxTree : ISearchService
    {
        private IEvaluator _evaluator;
        private IMoveGenerator _moveGenerator;
        private BuildTreeState _state;
        private int DEFAULT_LEVEL = 3;
        private Board _board;
        private Definitions.ChessColor _color;
        private MoveTree _tree;
        private int _maxPly;
        private int _posCount;

        public TreeNode<MoveInfo> MoveRoot { get { return _tree.Root; } }

        private IMove _bestMove;

        public SearchMinimaxTree(IEvaluator evaluator, IMoveGenerator generator)
        {
            _evaluator = evaluator;
            _moveGenerator = generator;
            _tree = new MoveTree();

            _maxPly = DEFAULT_LEVEL;
        }

        public void SetLevel(int level)
        {
            _maxPly = level;
        }

        public IMove Search(Board board, Definitions.ChessColor color, out float score)
        {
            _tree = new MoveTree();
            CreateSearchTree(board, color);
            _posCount = 0;
            Evaluate();
            Console.WriteLine($"evaluated positions: {_posCount}");
            IMove bestMove = SelectBestMove();
            score = _tree.Root.Data.Score;

            return bestMove;
        }
        
        internal void CreateSearchTree(Board board, Definitions.ChessColor color)
        {
            _board = board;
            _color = color;
            _state = BuildTreeState.AddMoves;
            _tree.ResetChildIndex();

            while (CreateTreeStep()) { }
        }

        private bool CreateTreeStep()
        {
            switch (_state)
            {
                case BuildTreeState.AddMoves:
                    _tree.AddChildren(_moveGenerator.GetAllMoves(_board, _color));
                    _state = BuildTreeState.GoDown;
                    break;

                case BuildTreeState.GoDown:
                    if (_tree.HasCurrentNextChild())
                    {
                        _tree.GotoNextChild();
                        _board.Move(_tree.CurrentMove);
                        _color = Helper.GetOpositeColor(_color);
                        if (_tree.CurrentLevel < _maxPly)
                        {
                            _state = BuildTreeState.AddMoves;
                            break;
                        }
                        else
                        {
                            //_tree.CurrentScore = _evaluator.Evaluate(_board); // eval
                            _state = BuildTreeState.GoUp;
                            break;
                        }
                    }
                    else
                    {
                        if (_tree.IsCurrentRoot())
                        {
                            return false; // fertig
                        }
                        else
                        {
                            _state = BuildTreeState.GoUp;
                            break;
                        }
                    }
                    break;

                case BuildTreeState.GoUp:
                    _board.Back();
                    _tree.GotoParent();
                    _color = Helper.GetOpositeColor(_color);
                    _state = BuildTreeState.GoDown;
                    break;
            }

            return true;
        }

        internal void Evaluate()
        {
            if (!_tree.IsCurrentRoot())
            {
                throw new Exception("Current node should be at the root but it is not.");
            }

            _state = BuildTreeState.GoDown;
            _tree.ResetChildIndex();
            while (EvaluateTreeStep()) { }
        }

        private bool EvaluateTreeStep()
        {
            switch (_state)
            {
                case BuildTreeState.GoDown:
                    if (_tree.HasCurrentNextChild())
                    {
                        _tree.GotoNextChild();
                        _board.Move(_tree.CurrentMove);
                        _color = Helper.GetOpositeColor(_color);
                        if (_tree.CurrentLevel >= _maxPly)
                        {
                            _tree.CurrentScore = _evaluator.Evaluate(_board);
                            _posCount++;
                            _state = BuildTreeState.GoUp;
                            break;
                        }
                    }
                    else
                    {
                        if (_tree.IsCurrentRoot())
                        {
                            return false; // fertig
                        }
                        else
                        {
                            _state = BuildTreeState.GoUp;
                            break;
                        }
                    }
                    break;

                case BuildTreeState.GoUp:
                    _board.Back();
                    _tree.GotoParent();
                    _color = Helper.GetOpositeColor(_color);
                    _state = BuildTreeState.GoDown;
                    break;
            }
            return true;
        }

        internal IMove SelectBestMove()
        {
            if (!_tree.IsCurrentRoot())
            {
                throw new Exception("Current node should be at the root but it is not.");
            }

            _state = BuildTreeState.GoDown;
            _tree.ResetChildIndex();
            while (SelectBestMoveStep()) { }

            return _bestMove;
        }

        private bool SelectBestMoveStep()
        {
            switch (_state)
            {
                case BuildTreeState.GoDown:
                    if (_tree.HasCurrentNextChild() && _tree.CurrentLevel < _maxPly - 1)
                    {
                        _tree.GotoNextChild();
                        _board.Move(_tree.CurrentMove);
                        _color = Helper.GetOpositeColor(_color);
                        break;
                    }
                    else
                    {
                        if (_color == Definitions.ChessColor.White)
                        {
                            var bestMoveInfo = _tree.GetChildMaxMoveInfo();
                            _tree.CurrentScore = bestMoveInfo.Score;
                            _bestMove = bestMoveInfo.Move;
                        }
                        else
                        {
                            var bestMoveInfo = _tree.GetChildMinMoveInfo();
                            _tree.CurrentScore = bestMoveInfo.Score;
                            _bestMove = bestMoveInfo.Move;
                        }
                        _state = BuildTreeState.GoUp;
                        
                        if (_tree.IsCurrentRoot())
                        {
                            return false; // fertig
                        }
                        break;
                    }
                    break;

                case BuildTreeState.GoUp:
                    _board.Back();
                    _tree.GotoParent();
                    _color = Helper.GetOpositeColor(_color);
                    _state = BuildTreeState.GoDown;
                    break;
            }
            return true;
        }

        private float InitBestScoreSofar(Definitions.ChessColor color)
        {
            if (color == Definitions.ChessColor.White)
            {
                return -10000;
            }
            else
            {
                return 10000;
            }
        }

        private bool IsBestMoveSofar(Definitions.ChessColor color, float bestScoreSoFar, float currentScore)
        {
            if (color == Definitions.ChessColor.White)
            {
                if (currentScore > bestScoreSoFar)
                {
                    return true;
                }
            }
            else
            {
                if (currentScore < bestScoreSoFar)
                {
                    return true;
                }
            }

            return false;
        }
    }
}

