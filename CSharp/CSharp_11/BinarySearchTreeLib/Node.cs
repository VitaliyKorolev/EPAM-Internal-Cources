using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinarySearchTreeLib
{
    [Serializable]
    public class Node<TItem> : IComparable<TItem> 
        where TItem : IComparable<TItem> 
    {
        public TItem Data { get; }
        public Node<TItem> LeftChild { get; set; }
        public Node<TItem> RightChild { get; set; }
        public Node() { }
        public Node(TItem data)
        {
            Data = data;
        }

        public Node(TItem data, Node<TItem> leftChild, Node<TItem> rightChild)
        {
            Data = data;
            LeftChild = leftChild;
            RightChild = rightChild;
        }

        public int CompareTo(TItem other)
        {
            return Data.CompareTo(other);
        }
    }
}
