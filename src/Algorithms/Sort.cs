using System;
using System.Buffers;
using System.Collections.Generic;
using DotNetDsa.Utils;
namespace DotNetDsa.Algorithms {
  public static class Sort {
    public static void Bubble<T>(T[] arr) where T: IComparable<T> {
      var n = arr.Length;
      bool swapped = false;
      for(var i = 0; i < n -1; i++) {
        for (var j = n - 1; j > i; --j)
        {
          if (arr[j].CompareTo(arr[j - 1]) < 0)
          {
            Helper.Swap(ref arr[j], ref arr[j - 1]);
            swapped = true;
          }
        }
        if (!swapped) {
          break;
        }
      }
    }
    public static void Selection<T>(T[] arr) where T: IComparable<T> {
      var n = arr.Length;
      for (var i = 0; i< n-1; i++) {
        int min = i;
        int j;
        for (j = i + 1; j < n; j++) {
          if (arr[min].CompareTo(arr[j]) > 0) {
            min = j;
          }
        }
        if (min != i) {
          Helper.Swap(ref arr[i], ref arr[min]);
        }
      }
    }
    public static void Insertion<T>(T[] arr) where T: IComparable<T> {
      var n = arr.Length;
      for (var i = 1; i < n; i++) {
        int j; T temp = arr[i];
        for (j = i; j > 0 && (arr[j].CompareTo(arr[j - 1]) < 0); j--) {
          arr[j] = arr[j - 1];
        }
        arr[j] = temp;
      }
    }
    public static void Quick<T>(T[] arr, int first, int last) where T: IComparable<T>{
      int mid = (first + last) / 2;
      int lower = first + 1, upper = last;
      Helper.Swap(ref arr[first], ref arr[mid]);
      while (lower <= upper) {
        while (arr[lower].CompareTo(arr[first]) < 0 && lower < last)
          lower++;
        while (arr[upper].CompareTo(arr[first]) > 0)
          upper--;
        if (lower < upper) {
          Helper.Swap(ref arr[lower++], ref arr[upper--]);
        }
        else lower++;
      }
      Helper.Swap(ref arr[first], ref arr[upper]);
      if (first < upper - 1) {
        Quick(arr, first, upper - 1);
      }
      if (upper + 1 < last) {
        Quick(arr, upper + 1, last);
      }
    }
    public static void Quick<T>(T[] arr) where T: IComparable<T> {
      if (arr.Length < 2) 
        return;
      Quick(arr, 0, arr.Length - 1);
    }
    public static void Merge<T>(T[] arr, int first, int last) where T: IComparable<T> {
      if (last - first < 2)
        return; 
      int mid = (first + last) / 2;
      Merge(arr, first, mid);
      Merge(arr, mid, last);
      MergeArray(arr, first, last);
    }
    public static void MergeArray<T>(T[] arr, int first, int last) where T: IComparable<T> {
      int mid = (first + last) / 2;
      int i1 = first, i2 = mid, i3 = 0;

      var pool = ArrayPool<T>.Shared;
      var tempSize = last - first;
      var temp = pool.Rent(tempSize);
      while (i1 < mid && i2 < last) {
        if (arr[i1].CompareTo(arr[i2]) < 0) {
          temp[i3++] = arr[i1++];
        } else {
          temp[i3++] = arr[i2++];
        }
      }
      while(i1 < mid) {
          temp[i3++] = arr[i1++];
      }
      while(i2 < last) {
          temp[i3++] = arr[i2++];
      }
      Array.Copy(temp, 0, arr, first, tempSize);
      pool.Return(temp);
    }
    public static void Merge<T>(T[] arr) where T: IComparable<T> {
      Merge(arr, 0, arr.Length);
    }

    public static void BuildHeap<T>(T[] arr) where T: IComparable<T> {
      var n = arr.Length;
      var maxSize = n;
      var currentSize = n;
      for (var i = n / 2; i >= 0; --i) {
        SiftDown(arr, i, currentSize);
      }
    }
    public static void Heap<T>(T[] arr) where T: IComparable<T> {
      int n = arr.Length;
      int currentSize = n;
      BuildHeap(arr);
      for (var i = 0; i < n - 1; i++) {
        Helper.Swap(ref arr[0], ref arr[currentSize - 1]);
        SiftDown(arr, 0, --currentSize);
      }
    }
    private static int LeftChild(int i) {
      int leftIndex = 2*i+1;
      return leftIndex;
    }
    private static int RightChild(int i) {
      int rightIndex = 2*i+2;
      return rightIndex;
    }
    private static int Parent(int i) {
      return (i-1)/2;
    }

    private static void SiftDown<T>(T[] arr, int i, int currentSize) where T: IComparable<T> {
      int left = LeftChild(i), right = RightChild(i);
      int max = i;
      do {
        i = max;
        if (left < currentSize && arr[max].CompareTo(arr[left]) < 0)
          max = left;
        if (right < currentSize && arr[max].CompareTo(arr[right]) < 0)
          max = right;
        if (max != i) {
          Helper.Swap(ref arr[max], ref arr[i]);
          left = LeftChild(max);
          right = RightChild(max);
        }
      } while(max != i);
    }
  }
}