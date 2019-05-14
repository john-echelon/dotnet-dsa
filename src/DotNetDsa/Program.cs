using System;
using DotNetDsa.Utils;
using DotNetDsa.Algorithms;
using DotNetDsa.DataStructures;
// using BenchmarkDotNet.Running;
using System.Collections.Generic;
using System.Linq;
using CommandLine;
using System.Reflection;

namespace DotNetDsa
{
  public class StubHashTable<TKey, TValue>: HashTable<TKey,TValue> where TKey: IComparable<TKey> {
    public List<KeyValuePair<TKey, TValue>>[] Store {
      get {
        return arr;
      }
    }
    public StubHashTable(int capacity = 10, float loadFactor = 1.0f): base(capacity, loadFactor){
    }
  }
  // TODO: Use cmd line args to invoke menu options
  class Program
  {
  [Verb("sort", HelpText = "Run Sort test cases.")]
  class SortOptions {
    [Option(
      'a', "all",
      Default = false,
      HelpText = "Sort using all available sort methods.")]
    public bool All { get; set; }

    [Option('m', "method", Required = false, HelpText = "The name of the sort [m]ethod used to process to standard output.")]
    public IEnumerable<string> SortMethods { get; set; }
  }
  [Verb("numeric", HelpText = "Run Numeric tests cases.")]
  class NumericOptions {
    // TODO: Options here
  }
  [Verb("hash", HelpText = "Run Hash tests cases.")]
  class HashOptions {
    // TODO: Options here
  }
  [Verb("dp", HelpText = "Run Dynamic Programming tests cases.")]
  class DynamicProgrammingOptions {
    // TODO: Options here
  }
     static int Main(string[] args)
    {
      return CommandLine.Parser.Default.ParseArguments<SortOptions, NumericOptions, HashOptions>(args)
      .MapResult(
        (SortOptions opts) => RunSort(opts),
        errs => 1);
      /*
      // RunNumeric();
      // RunHashTable();
      RunDP();
      */
    }
    static void RunDP() {
      var items = new Tuple<int, int>[] { 
        new Tuple<int, int>(10, 60),
        new Tuple<int, int>(20, 100),
        new Tuple<int, int>(30, 120),
      };
      int[] knapsackWeights = { 10, 20, 30, 40, 50, 60 }; 
      foreach(var capacity in knapsackWeights) {
        var result = DynamicProgramming.KnapsackWithoutRepetitions(capacity, items);
        Console.WriteLine("Knapsack without Repetitions, knapsack capacity {0}, max total value {1}", capacity, result[capacity, items.Length]);
        var backtrack = DynamicProgramming.KnapsackWithoutRepetitionsBacktrack(capacity, items);
        var output = Helper.Display(backtrack);
        Console.WriteLine("{0}", output);
        // var result = DynamicProgramming.KnapsackWithoutRepetitionsVariant1(capacity, items);
        // Console.WriteLine("Knapsack without Repetitions, knapsack capacity {0}, max total value {1}", capacity, result[items.Length, capacity]);
      }
      string a = "editing";
      string b = "distance";
      var editDistResult = DynamicProgramming.EditDistance(a, b);
      Console.WriteLine("Editing Distance of {0}, {1}: {2}", a, b, editDistResult[a.Length, b.Length]);
      var sequencePairs = GenerateNucleotideSequencePairs(10, 15);
      foreach(var pair in sequencePairs) {
        editDistResult = DynamicProgramming.EditDistance(pair.Item1, pair.Item2);
        Console.WriteLine("Editing Distance of {0}, {1}: {2}", pair.Item1, pair.Item2, editDistResult[pair.Item1.Length, pair.Item2.Length]);
      }
    }
    static Tuple<string, string>[] GenerateNucleotideSequencePairs(int count, int length) {
      var sequencePairs = new Tuple<string, string>[count];
      for (var i = 0; i < sequencePairs.Length; i++) {
        sequencePairs[i] = new Tuple<string, string> (GenerateNucleotideSequence(length), GenerateNucleotideSequence(length));
      }
      return sequencePairs;
    }
    static string GenerateNucleotideSequence(int length) {
      char[] nucleobase = { 'G', 'A', 'T', 'C'};
      char[] sequence = new char[length];
      var rand = new Random();
      for (var i = 0; i < length; i++){
        sequence[i] = nucleobase[rand.Next() % 4];
      }
      return new string(sequence);
    }
    static void RunHashTable() {
      const int capacity = 10, n = 50;
      var ht = new StubHashTable<Guid, int>(capacity, 10);
      var keys = new List<Guid>();
      for (var i =0; i < n; i++) {
        var guid = Guid.NewGuid();
        keys.Add(guid);
        ht[guid] = i*2;
      }
      var chainSizes = ht.Store.Select(ele => ele?.Count ?? 0).ToArray();
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
    static int RunSort(SortOptions opt) {
      if (opt.All) {
        RunSort(Sort.Heap);
        RunSort(Sort.Bubble);
        RunSort(Sort.Selection);
        RunSort(Sort.Insertion);
        RunSort(Sort.Quick);
        RunSort(Sort.Merge);
      } else {
        Type t = typeof(Sort);
        Type[] genericArguments = new Type[] { typeof(int) };
        foreach(var methodName in opt.SortMethods) {
          var meths = from m in typeof(Sort).GetMethods()
                    where m.Name == methodName && m.IsGenericMethod
                    let parameters = m.GetParameters()
                    where parameters.Length == 1
                    select m;
          MethodInfo genericMethodInfo = meths.Single().MakeGenericMethod(genericArguments);
          RunSort(genericMethodInfo);
        }
      }
      return 0;
    }
    static void RunSort(MethodInfo act) {
      var arr = new int[30];
      Helper.FillRandom(arr, 100);
      var output = Helper.Display(arr);
      Console.WriteLine("{0}", output);
      var type = act.GetType();
      Console.WriteLine("Running {0}...", act.Name);
      act.Invoke(null, new object[] { arr });
      output = Helper.Display(arr);
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
  }
}
