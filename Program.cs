using System;
using Utils;
using Algorithms;
using DataStructures;
using Benchmark;
using BenchmarkDotNet.Running;
using System.Collections.Generic;
using System.Linq;

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
      // RunNumeric();
      RunHashTable();
      #endif
    }
    static void RunHashTable() {
      const int capacity = 10, n = 50;
      var ht = new HashTable<Guid, int>(capacity);
      var keys = new List<Guid>();
      for (var i =0; i < n; i++) {
        var guid = Guid.NewGuid();
        keys.Add(guid);
        ht[guid] = i*2;
      }
      var chainSizes = ht.arr.Select(ele => ele?.Count ?? 0).ToArray();
      Console.WriteLine("HashTable Distribution for cap: {0} n: {1} ", capacity, n);
      var output = Helper.Display(chainSizes);
      Console.WriteLine("{0}", output);
      var values = keys.Select(ele => ht[ele]).ToArray(); 
      var output2 = Helper.Display(values);
      Console.WriteLine("{0}", output2);
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
