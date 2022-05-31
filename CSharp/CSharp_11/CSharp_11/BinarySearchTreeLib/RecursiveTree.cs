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

        public override IEnumerator<TItem> GetEnumerator()
        {
            return GetEnumeratorRecursivelly(Root);
        }
        private IEnumerator<TItem> GetEnumeratorRecursivelly(Node<TItem> current)
        {
            if (IsEmpty) yield break;
            if (current.LeftChild != null)
            {
                var leftTree = GetEnumeratorRecursivelly(current.LeftChild);
                while(leftTree.MoveNext())
                {
                    yield return leftTree.Current;
                }
            }
            yield return current.Data;
            if (current.RightChild != null)
            {
                var rightTree = GetEnumeratorRecursivelly(current.RightChild); ;
                while (rightTree.MoveNext())
                {
                    yield return rightTree.Current;
                }
            }
        }
        protected override Node<TItem> Find(TItem data, out Node<TItem> parent)
        {
            return Find(data, Root, null, out parent);
        }
        private Node<TItem> Find(TItem data, Node<TItem> current, Node<TItem> parrentOfcurrent, out Node<TItem> parent)
        {
            parent = parrentOfcurrent;
            if (Compare(data, current) == 0)
            {
                return current;
            }
            if (Compare(data, current) < 0)
            {
                return Find(data, current.LeftChild, current, out parent);
            }
            if (Compare(data, current) > 0)
            {
                return Find(data, current.RightChild, current, out parent);
            }
            return null;
        }
    }
}
