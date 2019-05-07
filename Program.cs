using System;
using Utils;
using Algorithms;
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
      RunSort(Sort.Heap);
      RunSort(Sort.Bubble);
      RunSort(Sort.Selection);
      RunSort(Sort.Insertion);
      RunSort(Sort.Quick);
      RunSort(Sort.Merge);
      RunNumeric();
      #endif
    }
    static void RunNumeric() {
      var hashCount = 20;
      var hashes = new uint[hashCount];
      var randomizer = new Random();
      var hashFunc = Hashing.UniversalHashingFamily();
      for (var i = 0; i < hashCount; i++) {
        hashes[i] = hashFunc((uint) i) % 100;
      }
      Console.WriteLine("Universal Hash Family values for x: 0...{0} ", hashCount);
      var output2 = Helper.Display(hashes);
      Console.WriteLine("{0}", output2);


      int n = Int32.MaxValue/4, m = 100;
      var arr = Numeric.GetPrimes(n);
      Console.WriteLine("Max integer value:{0:n}", Int32.MaxValue);
      Console.WriteLine("Sieve of Eratosthenes\nN:{0:n}\nPrimes Found:{1:n}", n, arr.Length);
      var arr2 = new int[m];
      Array.Copy(arr, arr.Length-m, arr2, 0, m);
      var output = Helper.Display(arr2);
      Console.WriteLine("{0}", output);
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
      // var summary = BenchmarkRunner.Run<SortBenchmark>();
      var summary = BenchmarkRunner.Run<NumericBenchmark>();
    }
  }
}
