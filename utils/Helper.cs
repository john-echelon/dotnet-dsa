using System;
using System.Linq;
using System.Text;

namespace utils {
  public static class Helper {
    private static Random rand = new Random();
    public static void Swap<T> (ref T lhs, ref T rhs) {
      T temp = lhs;
      lhs = rhs;
      rhs = temp;
    }
    public static void FillRandom(int[] arr, int max) {
      for(var i =0; i < arr.Length; i++) {
        arr[i] = rand.Next(max + 1);
      }
    }
    public static string Display<T>(T[] arr) {
      var str = String.Join(", ", arr);
      var sb = new StringBuilder();
      sb.AppendFormat("{0}", str);
      return sb.ToString();
    }
  }
}