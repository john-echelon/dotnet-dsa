
using System;
using System.Buffers;
using System.Collections.Generic;
using Utils;
namespace DataStructures {
  public class Heap<T> where T: IComparable<T>{
    private int maxSize, currentSize;
    T[] arr;
    public Heap(int capacity) {
      maxSize = capacity;
      arr = new T[maxSize];
      currentSize = 1;
    }
    public Heap(T[] arr) {
      var n = arr.Length;
      maxSize = n;
      arr = new T[maxSize];
      currentSize = 1;
      for (var i = n - 1; i > n / 2; --i) {
        SiftDown(i);
      }
    }
    public static void HeapSort(T[] arr) {
    }
    private int LeftChild(int i) {
      int leftIndex = 2*i+1;
      return leftIndex;
    }
    private int RightChild(int i) {
      int rightIndex = 2*i+2;
      return rightIndex;
    }
    private int Parent(int i) {
      return (i-1)/2;
    }
    private void SiftDown(int i) {
      int left = LeftChild(i), right = RightChild(i);
      int max = i;
      if (left < currentSize && arr[max].CompareTo(arr[left]) < 0)
        max = left;
      if (right < currentSize && arr[max].CompareTo(arr[right]) < 0)
        max = right;
      if (max != i) {
        Helper.Swap(ref arr[max], ref arr[i]);
        SiftDown(max);
      }
    }
    private void SiftUp(int i) {
      int parent = Parent(i);
      if (parent < 0)
        return;
      if (arr[i].CompareTo(arr[parent]) > 0) {
        Helper.Swap(ref arr[i], ref arr[parent]);
        SiftUp(parent);
      }
    }
    public void Add(T value) {
      if (currentSize < maxSize) {
        arr[++currentSize] = value;
        SiftUp(currentSize-1);
      }
    }
    public T ExtractMax() {
      var val = arr[0];
      Remove(0);
      return val;
    }
    public void ChangePriority(int i, T p) {
      var val = arr[i];
      arr[i] = p; 
      if (arr[i].CompareTo(val) > 0)
        SiftUp(i);
      else SiftDown(i);
    }
    public void Remove(int i) {
      var val = arr[i];
      Helper.Swap(ref arr[i], ref arr[currentSize-1]);
      currentSize--;
      if (arr[i].CompareTo(val) > 0)
        SiftUp(i);
      else SiftDown(i);
    }

  }
}