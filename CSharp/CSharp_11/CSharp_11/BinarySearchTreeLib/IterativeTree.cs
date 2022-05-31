using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinarySearchTreeLib
{
    [Serializable]
    public class IterativeTree<TItem> : BinarySearchTree<TItem> where TItem : IComparable<TItem>
    {
        public IterativeTree():base() { }
        public IterativeTree(IEnumerable<TItem> collection) : base(collection) { }
        public IterativeTree(Node<TItem> root) : base(root) { }
        public override void Add(TItem data)
        {
            var node = new Node<TItem>(data);
            if (IsEmpty)
            {
                Root = node;
            }
            else
            {
                Node<TItem> current = Root, parent = null;

                while (current != null)
                {
                    parent = current;
                    if (Compare(data, current) < 0)
                    {
                        current = current.LeftChild;
                    }
                    else
                        current = current.RightChild;
                }

                if (Compare(data, parent) < 0)
                {
                    parent.LeftChild = node;
                }
                else
                    parent.RightChild = node;
            }
        }

        protected override Node<TItem> Find(TItem data, out Node<TItem> parent)
        {
            Node<TItem> current = Root; 
            parent = null;
            while (current != null)
            {
                if (Compare(data, current) == 0)
                {
                    return current;
                }
                if (Compare(data, current) < 0)
                {
                    parent = current;
                    current = current.LeftChild;
                }
                if (Compare(data, current) > 0)
                {
                    parent = current;
                    current = current.RightChild;
                }
            }
            return null;
        }

        public override IEnumerator<TItem> GetEnumerator()
        {
            if (IsEmpty) yield break;

            var stack = new Stack<Node<TItem>>();
            var node = Root;

            while (stack.Count > 0 || node != null)
            {
                if (node == null)
                {
                    node = stack.Pop();
                    yield return node.Data;
                    node = node.RightChild;
                }
                else
                {
                    stack.Push(node);
                    node = node.LeftChild;
                }
            }
        }
    }
}
