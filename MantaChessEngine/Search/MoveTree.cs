﻿////using System;
////using System.Collections.Generic;
////using System.Linq;
////using System.Text;
////using System.Threading;
////using System.Threading.Tasks;

////namespace MantaChessEngine
////{
////    public class MoveTree
////    {
////        public TreeNode<MoveInfo> Root { get; private set; }

////        private TreeNode<MoveInfo> _currentNode;
////        private List<int> _currentChildrenIndex;
////        public int CurrentLevel { get; private set; }

////        public MoveTree()
////        {
////            Root = new TreeNode<MoveInfo>(new MoveInfo(null, 0), null);
////            _currentNode = Root;
////            ResetChildIndex();
////            CurrentLevel = 0;
////        }

////        public void ResetChildIndex()
////        {
////            _currentChildrenIndex = Enumerable.Repeat(0, 20).ToList();
////        }

////        public bool IsCurrentRoot()
////        {
////            return CurrentLevel == 0;
////        }

////        public IMove CurrentMove
////        {
////            get { return _currentNode.Data.Move; }
////        }

////        public float CurrentScore
////        {
////            get { return _currentNode.Data.Score; }
////            set { _currentNode.Data.Score = value; }
////        }

////        public bool HasCurrentNextChild()
////        {
////            if (_currentNode.ChildrenCount == 0)
////            {
////                return false;
////            }

////            return _currentNode.ChildrenCount > _currentChildrenIndex.ElementAt(CurrentLevel);
////        }

////        public void GotoNextChild()
////        {
////            if (HasCurrentNextChild())
////            {
////                _currentNode = _currentNode.GetChild(_currentChildrenIndex[CurrentLevel]);
////                _currentChildrenIndex[CurrentLevel]++; // next index might point to not existing node. therefore client must call HasCurrentNextChild.
////                CurrentLevel++;
////                _currentChildrenIndex[CurrentLevel] = 0;
////            }
////        }

////        public void GotoParent()
////        {
////            _currentNode = _currentNode.Parent;
////            CurrentLevel--;
////        }

////        public void AddChildren(IEnumerable<IMove> moves)
////        {
////            if (moves == null || moves.Count() == 0)
////            {
////                return;
////            }

////            if (_currentNode.ChildrenCount == 0)
////            {
////                _currentChildrenIndex[CurrentLevel] = 0;
////            }

////            foreach (var move in moves)
////            {
////                _currentNode.AddChild(new MoveInfo(move, 0));
////            }
////        }

////        public MoveInfo GetChildMaxMoveInfo()
////        {
////            float currentMaxScore = -10000;
////            IMove currentBestMove = null;

////            foreach (var child in _currentNode.Children)
////            {
////                if (child.Data.Score > currentMaxScore)
////                {
////                    currentMaxScore = child.Data.Score;
////                    currentBestMove = child.Data.Move;
////                }
////            }
////            return new MoveInfo(currentBestMove, currentMaxScore);
////        }

////        public MoveInfo GetChildMinMoveInfo()
////        {
////            float currentMinScore = 10000;
////            IMove currentBestMove = null;

////            foreach (var child in _currentNode.Children)
////            {
////                if (child.Data.Score < currentMinScore)
////                {
////                    currentMinScore = child.Data.Score;
////                    currentBestMove = child.Data.Move;
////                }
////            }
////            return new MoveInfo(currentBestMove, currentMinScore);
////        }

////    }
////}
