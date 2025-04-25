using System;
using System.Collections.Generic;

public class BinaryHeap<T>
{
    private readonly List<T> _heap;
    private readonly IComparer<T> _comparer;

    public BinaryHeap(IComparer<T> comparer = null)
    {
        _comparer = comparer ?? Comparer<T>.Default;
        _heap = new List<T>();
    }

    public void Insert(T item)
    {
        _heap.Add(item);
        SiftUp(_heap.Count - 1);
    }

    public T ExtractTop()
    {
        if (_heap.Count == 0)
            throw new InvalidOperationException("Heap is empty");

        T top = _heap[0];
        _heap[0] = _heap[_heap.Count - 1];
        _heap.RemoveAt(_heap.Count - 1);
        SiftDown(0);
        return top;
    }

    public T PeekTop()
    {
        if (_heap.Count == 0)
            throw new InvalidOperationException("Heap is empty");
        return _heap[0];
    }

    public int Count => _heap.Count;

    private void SiftUp(int index)
    {
        while (index > 0)
        {
            int parentIndex = (index - 1) / 2;
            if (_comparer.Compare(_heap[index], _heap[parentIndex]) > 0)
            {
                Swap(index, parentIndex);
                index = parentIndex;
            }
            else
            {
                break;
            }
        }
    }

    private void SiftDown(int index)
    {
        int lastIndex = _heap.Count - 1;
        while (true)
        {
            int leftChildIndex = 2 * index + 1;
            int rightChildIndex = 2 * index + 2;
            int largestIndex = index;

            if (leftChildIndex <= lastIndex &&
                _comparer.Compare(_heap[leftChildIndex], _heap[largestIndex]) > 0)
                largestIndex = leftChildIndex;

            if (rightChildIndex <= lastIndex &&
                _comparer.Compare(_heap[rightChildIndex], _heap[largestIndex]) > 0)
                largestIndex = rightChildIndex;

            if (largestIndex != index)
            {
                Swap(index, largestIndex);
                index = largestIndex;
            }
            else
            {
                break;
            }
        }
    }

    private void Swap(int i, int j)
    {
        T temp = _heap[i];
        _heap[i] = _heap[j];
        _heap[j] = temp;
    }
}