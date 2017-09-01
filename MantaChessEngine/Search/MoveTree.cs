using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MantaChessEngine
{
    public class MoveTree
    {
        public TreeNode<MoveBase> Root { get; private set; }

        private TreeNode<MoveBase> _currentNode;
        private List<int> _currentChildrenIndex;
        public int CurrentLevel { get; private set; }

        public MoveTree()
        {
            Root = new TreeNode<MoveBase>(null, null);
            _currentNode = Root;
            _currentChildrenIndex = new List<int> {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
            CurrentLevel = 0;
        }

        public bool IsCurrentRoot()
        {
            return CurrentLevel == 0;
        }

        public MoveBase CurrentMove
        {
            get { return _currentNode.Data; }
        }

        public bool HasCurrentNextChild()
        {
            if (_currentNode.ChildrenCount == 0)
            {
                return false;
            }

            return _currentNode.ChildrenCount > _currentChildrenIndex.ElementAt(CurrentLevel);
        }

        public void GotoNextChild()
        {
            if (HasCurrentNextChild())
            {
                _currentNode = _currentNode.GetChild(_currentChildrenIndex[CurrentLevel]);
                _currentChildrenIndex[CurrentLevel]++; // next index might point to not existing node. therefore client must call HasCurrentNextChild.
                CurrentLevel++;
            }
        }

        public void GotoParent()
        {
            _currentNode = _currentNode.Parent;
            CurrentLevel--;
        }

        public void AddChildren(IEnumerable<MoveBase> moves)
        {
            if (moves == null || moves.Count() == 0)
            {
                return;
            }

            if (_currentNode.ChildrenCount == 0)
            {
                _currentChildrenIndex[CurrentLevel] = 0;
            }

            foreach (var move in moves)
            {
                _currentNode.AddChild(move);
            }
        }

    }
}
