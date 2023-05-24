using System;
using System.Collections.Generic;

namespace MultiProcessor.DataStructures
{
    public class Heap<T>
    {
        private T[] _heapArray;
        private int _size;
        private readonly Func<T, T, int> _compareFunction;
        public Heap(Func<T, T, int> compareFunction)
        {
            _compareFunction = compareFunction;
            _heapArray = new T[100];
            _size = 1; 
        }
        private void _checkSize()
        {
            if (_size != _heapArray.Length)
                return;
            var copyArray = _heapArray;
            _heapArray = new T[2 * _size];
            for (var i = 0; i < _size; i++)
            {
                _heapArray[i] = copyArray[i];
            }
        }
        public void Add(T data)
        {
            _checkSize();
            _heapArray[_size++] = data;
            _heapifyUp(_size-1);
        }

        public T Peek()
        {
            return _heapArray[1];
        }

        public T Remove()
        {
            var valToReturn = _heapArray[1];
            _heapArray[1] = _heapArray[_size - 1];
            _size--;
            heapifyDown(1);
            return valToReturn;
        }

        private void heapifyDown(int index)
        {
            var children = new int[]{ index * 2, index * 2 + 1 };
            var selectedChild = -1;
            foreach(var child in children)
            {
                if (child >= _size) continue;
                if (selectedChild == -1)
                {
                    selectedChild = child;
                }
                else
                {
                    var cmpValue = _compareFunction.Invoke(_heapArray[selectedChild], _heapArray[child]);
                    if (cmpValue > 0)
                    {
                        selectedChild = child;
                    }
                }
            }
            if (selectedChild == -1)
                return;
            var cmpValue1 = _compareFunction.Invoke(_heapArray[selectedChild], _heapArray[index]);
            if (cmpValue1 >= 0) 
                return;
            (_heapArray[selectedChild], _heapArray[index]) = (_heapArray[index], _heapArray[selectedChild]);
            heapifyDown(selectedChild);
        }

        private void _heapifyUp(int index)
        {
            while (true)
            {
                var parent = index / 2;
                if (parent < 1) return;
                var cmpValue = _compareFunction.Invoke(_heapArray[index], _heapArray[parent]);
                if (cmpValue < 0)
                {
                    (_heapArray[index], _heapArray[parent]) = (_heapArray[parent], _heapArray[index]);
                }
                index = parent;
            }
        }

        public int Size()
        {
            return _size-1;
        }
    }
}