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
  public enum TestNameNumeric {
    SieveOfEratosthenes,
    UniversalHashingFamily,
  } 
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
      HelpText = "Sort using [a]ll available sort methods.")]
    public bool All { get; set; }

    [Option('t', "test", Required = false, HelpText = "The name of the [t]est case used to process to standard output.")]
    public IEnumerable<string> TestName { get; set; }
    [Option(
      'r', "range",
      Default = 100,
      HelpText = "Determines the max [r]ange of the generated number value from 0-[range-value].")]
    public int Range { get; set; }
    [Option(
      'c', "count",
      Default = 10,
      HelpText = "The [c]ount of the generated list to be sorted.")]
    public int Count { get; set; }
  }
  [Verb("numeric", HelpText = "Run Numeric tests cases.")]
  class NumericOptions {
    [Option('t', "test", Required = true, HelpText = "The corresponding [t]est case number.")]
    public int TestNumber { get; set; }
    [Option(
      'c', "count",
      Default = 10,
      HelpText = "The [c]ount corresponds to the number operations to perform.")]
    public int Count { get; set; }
    [Option('p', "params", Required = false, HelpText = "Additional [p]arams used for a particular test case.")]
    public IEnumerable<int> Params { get; set; }
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
        (NumericOptions opts) => RunNumeric(opts),
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
    static int RunNumeric(NumericOptions opt) {
      var testName = Enum.GetName(typeof(TestNameNumeric), opt.TestNumber);
      var methodName = "Run" + testName;
      Console.WriteLine("Running test: {0}", testName);
      Type[] parameterTypes = { typeof(NumericOptions) };
      MethodInfo methodInfo = typeof(Program).GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Static);
      methodInfo.Invoke(null, new object[] { opt });
      return 0;
    }
    static void RunSieveOfEratosthenes(NumericOptions opt) {
      var p = opt.Params.ToArray();
      var hasParams = p.Length > 0;
      int n = hasParams ? p[0] : Int32.MaxValue/100;
      int m = opt.Count;
      var arr = Numeric.GetPrimes(n);
      Console.WriteLine("N: {0:N0}. Primes Found: {1:N0}", n, arr.Length);
      string output;
      if (arr.Length - m < 1)  {
        output = Helper.Display(arr);
        Console.WriteLine("Outputting primes: {0}", output);
      }
      else {
        var arr2 = new int[m];
        Array.Copy(arr, arr.Length-m, arr2, 0, m);
        output = Helper.Display(arr2);
        Console.WriteLine("Outputting the last {0} primes: {1}", m, output);
      }
    }
    static void RunUniversalHashingFamily(NumericOptions opt) {
      var hashCount = opt.Count;
      var hashes = new uint[hashCount];
      var randomizer = new Random();
      var hashFunc = Hashing.UniversalHashingFamily();
      for (var i = 0; i < hashCount; i++) {
        hashes[i] = hashFunc((uint) i) % 100;
      }
      Console.WriteLine("Universal Hash Family values for x: 0...{0} ", hashCount);
      var output2 = Helper.Display(hashes);
      Console.WriteLine("{0}", output2);
    }
    static int RunSort(SortOptions opt) {
      Type t = typeof(Sort);
      Type[] genericArguments = new Type[] { typeof(int) };
      var sortMethods = from m in typeof(Sort).GetMethods()
        where m.IsGenericMethod
        let parameters = m.GetParameters()
        where parameters.Length == 1
        select m;
      if (opt.All) {
        foreach(var method in sortMethods) {
          MethodInfo genericMethodInfo = method.MakeGenericMethod(genericArguments);
          RunSort(genericMethodInfo, opt);
        }
      } else {
        foreach(var methodName in opt.TestName) {
          var method = (from m in sortMethods
                    where m.Name.ToLower() == methodName.ToLower()
                    let parameters = m.GetParameters()
                    where parameters.Length == 1
                    select m).Single();
          MethodInfo genericMethodInfo = method.MakeGenericMethod(genericArguments);
          RunSort(genericMethodInfo, opt);
        }
      }
      return 0;
    }
    static void RunSort(MethodInfo act, SortOptions opt) {
      var arr = new int[opt.Count];
      Helper.FillRandom(arr, opt.Range);
      Console.WriteLine("Running {0}...", act.Name);
      var output = Helper.Display(arr);
      Console.WriteLine("Unsorted: {0}", output);
      var type = act.GetType();
      act.Invoke(null, new object[] { arr });
      output = Helper.Display(arr);
      Console.WriteLine("Sorted: {0}", output);
    }
  }
}
