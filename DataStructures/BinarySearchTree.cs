using System;
using System.Collections.Generic;

namespace MultiProcessor.DataStructures
{
    public class BinarySearchTree<T>
    {
        private Node<T>? Root { get; set; }
        public BinarySearchTree()
        {
            Root = null;
        }

        public BinarySearchTree(Node<T>? root)
        {
            Root = root;
        }

        public void Add(T data)
        {
            Root = _add(data, Root);
        }

        public Node<T>? Find(T data, Node<T>? root)
        {
            while (root != null && root.Data != null)
            {
                var cmpValue = root.CompareTo(data);
                if (cmpValue == 0)
                    return root;
                root = cmpValue < 0 ? root.Right : root.Left;
            }
            return root;
        }
        public void Remove(T data)
        {
            var toDelete = Find(data, Root);
            if (toDelete == null)
                return;
            
        }
        private Node<T> _add(T data, Node<T>? current)
        {
            if (current == null)
            {
                current = new Node<T>(data);
                return current;
            }

            var cmpValue = current.CompareTo(data);
            if (cmpValue < 0)
            {
                current.Right = _add(data, current.Right);
            }
            else if(cmpValue > 0)
            {
                current.Left = _add(data, current.Left);
            }
            return current;
        }
        
        private void _print(Node<T>? root)
        {
            Queue<Node<T>> q = new Queue<Node<T>>();
            q.Enqueue(root);
            while (q.Count > 0)
            {
                int curSize = q.Count;
                while (curSize > 0)
                {
                    curSize--;
                    root = q.Dequeue();
                    Console.Write(root.Data + " ");
                    if (root.Left != null)
                    {
                        q.Enqueue(root.Left);
                    }

                    if (root.Right != null)
                    {
                        q.Enqueue(root.Right);
                    }
                }
                Console.WriteLine();
            }
        }
        public void Print()
        {
            _print(Root);
        }
    }
}