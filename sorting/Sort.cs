using System;
using System.Collections.Generic;
using Utils;
namespace Sorting {
  public static class Sort {
    public static void Bubble<T>(T[] arr) where T: IComparable {
      var n = arr.Length;
      for(var i = 0; i < n -1; i++) {
        for (var j = n - 1; j > i; --j)
        {
          if (arr[j].CompareTo(arr[j - 1]) < 0)
          {
            Helper.Swap(ref arr[j], ref arr[j - 1]);
          }
        }
      }
    }
    public static void Selection<T>(T[] arr) where T: IComparable {
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
    public static void Insertion<T>(T[] arr) where T: IComparable {
      var n = arr.Length;
      for (var i = 1; i < n; i++) {
        int j; T temp = arr[i];
        for (j = i; j > 0 && (arr[j].CompareTo(arr[j - 1]) < 0); j--) {
          arr[j] = arr[j - 1];
        }
        arr[j] = temp;
      }
    }
  }
}