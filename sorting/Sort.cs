using System;
using System.Collections.Generic;
using utils;
namespace sorting {
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
  }
}