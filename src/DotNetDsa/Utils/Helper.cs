using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNetDsa.Utils {
  public static class Helper {
    public static void Swap<T> (ref T lhs, ref T rhs) {
      T temp = lhs;
      lhs = rhs;
      rhs = temp;
    }
    public static void FillRandom(int[] arr, int max) {
      var rand = new Random();
      for(var i =0; i < arr.Length; i++) {
        arr[i] = rand.Next(max + 1);
      }
    }
    public static string Display<T>(T[] arr, string separator = ", ") {
      var str = String.Join(separator , arr);
      var sb = new StringBuilder();
      sb.AppendFormat("{0}", str);
      return sb.ToString();
    }
    public static double Median(List<double> arr) {
      int numCount = arr.Count(); 
      int halfIndex = numCount/2; 
      var sortedNumbers = arr.OrderBy(n=>n); 
      double median; 
      if ((numCount % 2) == 0) 
      { 
        median = (sortedNumbers.ElementAt(halfIndex) + sortedNumbers.ElementAt(halfIndex - 1)) / 2; 
      } else { 
          median = sortedNumbers.ElementAt(halfIndex); 
      } 
      return median;
    }

    public static double Mode(List<double> arr) {
      return arr.GroupBy(num => num).OrderByDescending(g => g.Count()).Select(g => g.Key).FirstOrDefault();
    }
    public static double StandardDeviation(List<double> arr)  
    {  
      double avg = arr.Average();  
      double sumOfDiffs = arr.Sum(num => (num - avg) * (num - avg));
      return Math.Sqrt(sumOfDiffs / arr.Count);  
    }  
  }
}