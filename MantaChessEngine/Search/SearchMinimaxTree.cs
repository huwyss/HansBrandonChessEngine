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

        public TreeNode<MoveBase> MoveRoot { get { return _tree.Root; } }

        public SearchMinimaxTree(IEvaluator evaluator, IMoveGenerator generator)
        {
            _evaluator = evaluator;
            _moveGenerator = generator;
        }

        public IMove Search(Board board, Definitions.ChessColor color, out float score)
        {

            // todo
            // 0. MoveTree mit test erstellen. braucht noch GetSibling und GetParent
            // 1. Create complete tree with all possible moves of required depth --> test
            // 2. take tree and evaluate each position. --> test
            // Auf diese Weise können beide schritte getestet werden
            // und später können opitimiertere Suchen implementiert werden mit dem gleichen bereits
            // erstellten kompletten Baum.

            MoveBase bestMove = null;
            float bestScore = InitBestScoreSofar(color);

            CreateSearchTree(board, color);

            score = bestScore;
            return bestMove;
        }

        private Board _board;
        private Definitions.ChessColor _color;
        private MoveTree _tree;
        private int _maxPly = 2; // 2 half moves depth

        internal MoveTree CreateSearchTree(Board board, Definitions.ChessColor color)
        {
            _tree = new MoveTree();
            _board = board;
            _color = color;

            while (AlgoStep()) { }

            return _tree;
        }

        private bool AlgoStep()
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

        //internal TreeNode<MoveBase> CreateSearchTree2(Board board, Definitions.ChessColor color)
        //{
        //    _movesRoot = new TreeNode<MoveBase>(null, null); // no content, no parent

        //    var possibleFirstMoves = _moveGenerator.GetAllMoves(board, color);
        //    for (int i = 0; i < possibleFirstMoves.Count; i++)
        //    {
        //        _movesRoot.AddChild(possibleFirstMoves[i]);
        //        board.CurrentMove(possibleFirstMoves[i]);
        //        var secondColor = Helper.GetOpositeColor(color);
        //        var possibleSecondMoves = _moveGenerator.GetAllMoves(board, secondColor);

        //        for (int j = 0; j < possibleSecondMoves.Count; j++)
        //        {
        //            var secondMoveNode = _movesRoot.GetChild(i);
        //            secondMoveNode.AddChild(possibleSecondMoves[j]);
        //        }

        //        board.Back();
        //    }

        //    return _movesRoot;
        //}

        internal void Evaluate()
        {
            //float scoreCurrentMove = _evaluator.Evaluate(board);
            //if (IsBestMoveSofar(color, bestScore, scoreCurrentMove))
            //{
            //    bestMove = possibleSecondMoves[j];
            //    bestScore = scoreCurrentMove;
            //}
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

