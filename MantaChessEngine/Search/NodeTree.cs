using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MantaChessEngine
{
    public delegate void TreeVisitor<T>(T nodeData);

    public class NodeTree<T>
    {
        public T Data { get; private set; }
        public NodeTree<T> Parent { get; private set; }

        private LinkedList<NodeTree<T>> children;

        public NodeTree(T data, NodeTree<T> parent )
        {
            Data = data;
            Parent = parent;
            children = new LinkedList<NodeTree<T>>();
        }

        public void AddChild(T data)
        {
            children.AddLast(new NodeTree<T>(data, this));
        }

        public int ChildrenCount { get { return children.Count; } }

        public NodeTree<T> GetChild(int i)
        {
            return children.ElementAt(i);
        }

        public void Traverse(NodeTree<T> node, TreeVisitor<T> visitor)
        {
            visitor(node.Data);
            foreach (NodeTree<T> kid in node.children)
                Traverse(kid, visitor);
        }

    }
}
