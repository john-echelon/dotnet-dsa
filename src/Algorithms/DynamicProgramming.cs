using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using DotNetDsa.Utils;
namespace DotNetDsa.Algorithms {
  public class DynamicProgramming {
    public static int[,] KnapsackWithoutRepetitions(int w, Tuple<int, int>[] items) {
      int n = items.Length;
      int[,] arr = new int[w + 1, n + 1];
      for (var i = 1; i <= n; i++) {
        for (var j = 1; j <= w; j++) {
          arr[j, i] = arr[j, i - 1];
          int itemWeight = items[i - 1].Item1;
          int itemValue = items[i - 1].Item2;
          if (itemWeight <= j) {
            int val = arr[j - itemWeight, i - 1] + itemValue;
            if (val > arr[j, i]) {
              arr[j, i] = val;
            }
          }
        }
      }
      return arr;
    }
    public static bool[] KnapsackWithoutRepetitionsBacktrack(int w, Tuple<int, int>[] items) {
      var arr = KnapsackWithoutRepetitions(w, items);
      int n = items.Length;
      int j = w;
      var backtrack = new bool[n];
      for (var i = n; i > 0; i--) {
        int itemWeight = items[i - 1].Item1;
        int itemValue = items[i - 1].Item2;
        if (itemWeight <= j && arr[j - itemWeight, i - 1] + itemValue == arr[j, i]) {
          backtrack[i - 1] = true;
          j-= itemWeight;
        } else {
          backtrack[i - 1] = false;
        }
      }
      return backtrack;
    }
    /*
      This is a slight variation of KnapsackWithoutRepetitions only to demonstrate choosing a different orientation would not affect results,
      where each row denotes item, and col denotes weights.
      The outer loop iterates the items, the inner loop iterates the weights.
     */
    public static int[,] KnapsackWithoutRepetitionsVariant1(int w, Tuple<int, int>[] items) {
      int n = items.Length;
      int[,] arr = new int[n + 1, w + 1];
      for (var j = 1; j <= w; j++) {
        for (var i = 1; i <= n; i++) {
          arr[i, j] = arr[i - 1, j];
          int itemWeight = items[i - 1].Item1;
          int itemValue = items[i - 1].Item2;
          if (itemWeight <= j) {
            int val = arr[i - 1, j - itemWeight] + itemValue;
            if (val > arr[i, j]) {
              arr[i, j] = val;
            }
          }
        }
      }
      return arr;
    }
  }
}