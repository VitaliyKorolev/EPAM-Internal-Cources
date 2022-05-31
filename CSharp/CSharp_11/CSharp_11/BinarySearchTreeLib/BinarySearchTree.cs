using System;
using System.Collections;
using System.Collections.Generic;


namespace BinarySearchTreeLib
{
	[Serializable]
	public abstract class BinarySearchTree<TItem> : IEnumerable<TItem> where TItem : IComparable<TItem>
	{
		public Node<TItem> Root { get; /*???*/ set; }
		public bool IsEmpty
		{
			get
			{
				if (Root == null)
					return true;
				return false;
			}
		}
		public BinarySearchTree() { }
		public BinarySearchTree(Node<TItem> root) 
		{
			Root = root;
		}
		public BinarySearchTree(IEnumerable<TItem> collection):this()
		{
			AddRange(collection);
		}

		public abstract void Add(TItem data);

		public void AddRange(IEnumerable<TItem> data)
		{
			foreach (TItem item in data)
			{
				Add(item);
			}
		}

		public Node<TItem> Find(TItem data)
		{
			if (IsEmpty)
				return null;
			else
				return Find(data, out Node<TItem> parent);
		}
		protected abstract Node<TItem> Find(TItem data, out Node<TItem> parent);

		public abstract IEnumerator<TItem> GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public TItem GetMax()
		{
			var current = Root;
			while (current.RightChild != null)
			{
				current = current.RightChild;
			}
			return current.Data;
		}

		public TItem GetMin()
		{
			var current = Root;
			while (current.LeftChild != null)
			{
				current = current.LeftChild;
			}
			return current.Data;
		}

		protected int Compare(TItem data, Node<TItem> node)
		{
			if (node == null)
			{
				return 1;
			}
			return data.CompareTo(node.Data);
		}

		public bool Remove(TItem data)
		{
			Node<TItem> parent;
			Node<TItem> node = Find(data, out parent);
			if (node == null)
				return false;

			if ((node.LeftChild == null) && (node.RightChild == null)) //Its a Leaf node
			{
				RemoveNodeHavingNoChildren(node, parent);
			}
			else if ((node.LeftChild == null) || (node.RightChild == null)) //It has either Left orRight child
			{
				RemoveNodeHavingOneChild(node, parent);
			}
			else
				RemoveNodeHavingBothChildren(node, parent);
			return true;
		}

		private void RemoveNodeHavingNoChildren(Node<TItem> nodeToRemove, Node<TItem> parent)
		{
			if (parent.LeftChild == nodeToRemove)
				parent.LeftChild = null;
			else
				parent.RightChild = null;
		}

		private void RemoveNodeHavingOneChild(Node<TItem> nodeToRemove, Node<TItem> parent)
		{
			Node<TItem> tempChild = nodeToRemove.LeftChild == null ? nodeToRemove.RightChild : nodeToRemove.LeftChild;
			if (nodeToRemove == parent.LeftChild)
				parent.LeftChild = tempChild;
			else
				parent.RightChild = tempChild;
		}

		private void RemoveNodeHavingBothChildren(Node<TItem> nodeToRemove, Node<TItem> parent)
		{
			Node<TItem> successor = GetSuccesor(nodeToRemove);
			//if the current node is the root node then the new root is the successor node
			if (nodeToRemove == Root)
			{
				Root = successor;
			}
			else if (parent.LeftChild == nodeToRemove)
			{//if this is the left child set the parents left child node as the successor node
				parent.LeftChild = successor;
			}
			else
			{//if this is the right child set the parents right child node as the successor node
				parent.RightChild = successor;
			}
		}
		private Node<TItem> GetSuccesor(Node<TItem> node)
		{
			Node<TItem> parentOfSuccessor = node;
			Node<TItem> successor = node;
			Node<TItem> current = node.RightChild;

			//starting at the right child we go down every left child node
			while (current != null)
			{
				parentOfSuccessor = successor;
				successor = current;
				current = current.LeftChild;// go to next left node
			}
			//if the succesor is not just the right node then
			if (successor != node.RightChild)
			{
				//set the Left node on the parent node of the succesor node to the right child node of the successor in case it has one
				parentOfSuccessor.LeftChild = successor.RightChild;
				//attach the right child node of the node being deleted to the successors right node
				successor.RightChild = node.RightChild;
			}
			//attach the left child node of the node being deleted to the successors leftnode node
			successor.LeftChild = node.LeftChild;

			return successor;
		}
	}
}

