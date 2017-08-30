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
        public T Data { get; set; }
        private LinkedList<NodeTree<T>> children;

        public NodeTree(T data)
        {
            this.Data = data;
            children = new LinkedList<NodeTree<T>>();
        }

        public void AddChild(T data)
        {
            children.AddLast(new NodeTree<T>(data));
        }

        public NodeTree<T> GetChild(int i)
        {
            return children.ElementAt(i);
            //foreach (NodeTree<T> n in children)
            //    if (--i == 0)
            //        return n;
            //return null;
        }

        public void Traverse(NodeTree<T> node, TreeVisitor<T> visitor)
        {
            visitor(node.Data);
            foreach (NodeTree<T> kid in node.children)
                Traverse(kid, visitor);
        }

    }
}
