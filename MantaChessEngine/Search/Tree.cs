using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MantaChessEngine
{
    public delegate void TreeVisitor<T>(T nodeData);

    public class Tree<T>
    {
        private T data;
        private LinkedList<Tree<T>> children;

        public Tree(T data)
        {
            this.data = data;
            children = new LinkedList<Tree<T>>();
        }

        public void AddChild(T data)
        {
            children.AddFirst(new Tree<T>(data));
        }

        public Tree<T> GetChild(int i)
        {
            foreach (Tree<T> n in children)
                if (--i == 0)
                    return n;
            return null;
        }

        public void Traverse(Tree<T> node, TreeVisitor<T> visitor)
        {
            visitor(node.data);
            foreach (Tree<T> kid in node.children)
                Traverse(kid, visitor);
        }

    }
}
