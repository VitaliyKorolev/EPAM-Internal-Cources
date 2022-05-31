using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinarySearchTreeLib
{
    [Serializable]
    public class RecursiveTree<TItem> : BinarySearchTree<TItem> where TItem :IComparable<TItem>
    {
        public RecursiveTree() : base() { }
        public RecursiveTree(IEnumerable<TItem> collection) : base(collection) { }
        public RecursiveTree(Node<TItem> root) : base(root) { }
        public override void Add(TItem data)
        {
            if (IsEmpty)
            {
                Root = new Node<TItem>(data);
            }
            else
            {
                Add(data, Root);
            }
        }
        private void Add(TItem data, Node<TItem> current)
        {
            if (Compare(data, current) < 0)
            {
                if (current.LeftChild == null)
                    current.LeftChild = new Node<TItem>(data);
                else
                    Add(data, current.LeftChild);
            }

            if (Compare(data, current) > 0)
            {
                if (current.RightChild == null)
                    current.RightChild = new Node<TItem>(data);
                else
                    Add(data, current.RightChild);
            }
            if(Compare(data, current) == 0)
            {
                throw new InvalidOperationException("This value is already in the tree");
            }
        }

        public override Node<TItem> Find(TItem data)
        {
            if (IsEmpty)
            {
                return null;
            }
            else
            {
                Node<TItem> parent;
                return Find(data, out parent);
            }
        }

        public override IEnumerator<TItem> GetEnumerator()
        {
            if (Root.LeftChild != null)
            {
                RecursiveTree<TItem> leftTree = new RecursiveTree<TItem>(Root.LeftChild);
                foreach (var el in leftTree)
                {
                    yield return el;
                }
            }
            yield return Root.Data;
            if (Root.RightChild != null)
            {
                RecursiveTree<TItem> rightTree = new RecursiveTree<TItem>(Root.RightChild);
                foreach (var el in rightTree)
                {
                    yield return el;
                }
            }
        }
        protected override Node<TItem> Find(TItem data, out Node<TItem> parent)
        {
            if(Compare(data, Root) == 0)
            {
                parent = null;
                return Root;
            }
            return Find(data, Root, out parent);
        }
        private Node<TItem> Find(TItem data, Node<TItem> current, out Node<TItem> parent)
        {
            parent = current;
            if (Compare(data, current.LeftChild) == 0)
            {
                return current.LeftChild;
            }
            else if (Compare(data, current.RightChild) == 0)
            {
                return current.RightChild;
            }
            else if (Compare(data, current) < 0)
            {
                return Find(data, current.LeftChild, out parent);
            }
            else
            {
                return Find(data, current.RightChild, out parent);
            }
        }
        //protected override Node<TItem> Find(TItem data, out Node<TItem> parent)
        //{
        //    parent = null;
        //    Stack<Node<TItem>> stack = new Stack<Node<TItem>>();
        //    return Find(data, Root, out parent, stack);
        //}
        //private Node<TItem> Find(TItem data, Node<TItem> current, out Node<TItem> parent, Stack<Node<TItem>> stack)
        //{
        //    if (Compare(data, current) == 0)
        //    {
        //        if (stack.Count != 0)
        //            parent = stack.Pop();
        //        else
        //            parent = null;
        //        return current;
        //    }
        //    else if (Compare(data, current) < 0)
        //    {
        //        if (current.LeftChild == null)
        //        {
        //            parent = null;
        //            return null;
        //        }
        //        else
        //        {
        //            stack.Push(current);
        //            return Find(data, current.LeftChild, out parent, stack);
        //        }
        //    }

        //    else 
        //    {
        //        if (current.RightChild == null)
        //        {
        //            parent = null;
        //            return null;
        //        }
        //        else
        //        {
        //            stack.Push(current);
        //            return Find(data, current.RightChild, out parent, stack);
        //        }
        //    }
        //}
    }
}
