using System;
using Utils;
using Sorting;
using Benchmark;
using BenchmarkDotNet.Running;

namespace dotnet_dsa
{
  class Program
  {
     static void Main(string[] args)
    {
      #if RELEASE
      RunBenchmarks();
      #else
      RunSort(Sort.Bubble2);
      RunSort(Sort.Selection);
      RunSort(Sort.Insertion);
      RunSort(Sort.Quick);
      RunSort(Sort.Merge);
      #endif
    }
    static void RunSort(Action<int[]> act) {
      var arr = new int[30];
      Helper.FillRandom(arr, 100);
      var output = Helper.Display(arr);
      Console.WriteLine("{0}", output);
      var type = act.GetType();
      Console.WriteLine("Running {0}...", act.Method.Name);
      act(arr);
      output = Helper.Display(arr);
      Console.WriteLine("{0}", output);
    }
    static void RunBenchmarks() {
      var summary = BenchmarkRunner.Run<SortBenchmark>();
    }
  }
}
