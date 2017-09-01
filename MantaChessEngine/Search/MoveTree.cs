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
        private int _currentLevel;

        public MoveTree()
        {
            Root = new TreeNode<MoveBase>(null, null);
            _currentNode = Root;
            _currentChildrenIndex = new List<int>();
            _currentLevel = 0;
        }

        public bool IsRoot()
        {
            return _currentLevel == 0;
        }

        public MoveBase CurrentMove
        {
            get { return _currentNode.Data; }
        }

        public bool HasNextChild()
        {
            if (_currentNode.ChildrenCount == 0)
            {
                return false;
            }

            return _currentNode.ChildrenCount >= _currentChildrenIndex.ElementAt(_currentLevel);
        }

        public void GotoNextChild()
        {
            if (HasNextChild())
            {
                _currentNode = _currentNode.GetChild(_currentChildrenIndex[_currentLevel]);
                _currentChildrenIndex[_currentLevel]++; // next index might point to not existing node. therefore client must call HasNextChild.
                _currentLevel++;
            }
        }

        public void GotoParent()
        {
            _currentNode = _currentNode.Parent;
            _currentLevel--;
        }

        public void AddChildren(IEnumerable<MoveBase> moves)
        {
            if (moves == null)
            {
                return;
            }

            foreach (var move in moves)
            {
                _currentNode.AddChild(move);
            }

            _currentChildrenIndex.Add(0);
        }
    }
}
