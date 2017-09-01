using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MantaChessEngine
{
    public class TreeNode<T>
    {
        public T Data { get; private set; }
        public TreeNode<T> Parent { get; private set; }

        public LinkedList<TreeNode<T>> Children { get; private set; }

        public TreeNode(T data, TreeNode<T> parent )
        {
            Data = data;
            Parent = parent;
            Children = new LinkedList<TreeNode<T>>();
        }

        public void AddChild(T data)
        {
            Children.AddLast(new TreeNode<T>(data, this));
        }

        public int ChildrenCount { get { return Children.Count; } }

        public TreeNode<T> GetChild(int i)
        {
            return Children.ElementAt(i);
        }

        // ---------------------
        // not used so far...
        public delegate void TreeVisitor<T>(T nodeData);
        public void Traverse(TreeNode<T> treeNode, TreeVisitor<T> visitor)
        {
            visitor(treeNode.Data);
            foreach (TreeNode<T> kid in treeNode.Children)
                Traverse(kid, visitor);
        }

    }
}
