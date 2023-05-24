using System;
using System.Collections.Generic;

namespace MultiProcessor.DataStructures
{
    public class Node<T>: IComparable<T>
    {
        public T Data { get; set; }
        public Node<T>? Left { get; set; }
        public Node<T>? Right { get; set; }

        public Node(T data)
        {
            Data = data;
            Left = Right = null;
        }
        public Node(T data, Node<T>? left, Node<T>? right)
        {
            Data = data;
            Left = left;
            Right = right;
        }

        public int CompareTo(T other)
        {
            return Comparer<T>.Default.Compare(Data, other);
        }
    }
}