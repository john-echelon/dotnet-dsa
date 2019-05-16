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
  } 
  public enum TestNameHashing {
    UniversalHashingFamily,
    HashTable,
    HashTableDistribution
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
        HelpText = "The [c]ount of numbers to generate.")]
      public int Count { get; set; }
      [Option('p', "params", Required = false, HelpText = "Additional [p]arams used for a particular test case.")]
      public IEnumerable<int> Params { get; set; }
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
    class HashingOptions {
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
    [Verb("dp", HelpText = "Run Dynamic Programming tests cases.")]
    class DynamicProgrammingOptions {
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
    static int Main(string[] args)
    {
      return CommandLine.Parser.Default.ParseArguments<SortOptions, NumericOptions, HashingOptions>(args)
      .MapResult(
        (SortOptions opts) => RunSort(opts),
        (NumericOptions opts) => RunNumeric(opts),
        (HashingOptions opts) => RunHashing(opts),
        errs => 1);
      /*
      TODO: Build CLI driven versions of
      RunHashTable();
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
    static int RunHashing(HashingOptions opt) {
      var testName = Enum.GetName(typeof(TestNameHashing), opt.TestNumber);
      var methodName = "Run" + testName;
      Console.WriteLine("Running test: {0}", testName);
      Type[] parameterTypes = { typeof(HashingOptions) };
      MethodInfo methodInfo = typeof(Program).GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Static);
      methodInfo.Invoke(null, new object[] { opt });
      return 0;
    }
    static void RunUniversalHashingFamily(HashingOptions opt) {
      int[] testParams = new int[] { 100, 100, 10 };
      SetupParams(opt.Params, testParams);
      var hashCount = testParams[0];
      var bucketSize = testParams[1];
      var numResults = testParams[2];
      var hashes = new uint[hashCount];
      var hashFunc = Hashing.UniversalHashingFamily();
      for (var i = 0; i < hashCount; i++) {
        hashes[i] = hashFunc((uint) i) % (uint) bucketSize;
      }
      Console.WriteLine("Universal Hash Family values for x: [0-{0}] ", hashCount);
      DisplayResults(hashes, numResults);
    }
    static void RunHashTableDistribution(HashingOptions opt) {
      int[] testParams = new int[] { 50, 10 };
      SetupParams(opt.Params, testParams);
      int n = testParams[0];
      int capacity = testParams[1];
      float loadFactor = n;
      var ht = new StubHashTable<Guid, int>(capacity, loadFactor);
      var keys = new List<Guid>();
      for (var i =0; i < n; i++) {
        var guid = Guid.NewGuid();
        keys.Add(guid);
        ht[guid] = i;
      }
      var chainLengths = ht.Store.Select(ele => (double) (ele?.Count ?? 0)).ToList();
      Console.WriteLine("HashTable Distribution for n: {0} capacity: {1}", n, capacity);
      Console.WriteLine("Mean: {0} Median: {1} Mode {2}", chainLengths.Average(), Helper.Median(chainLengths), Helper.Mode(chainLengths));
      var sd = Helper.StandardDeviation(chainLengths);
      Console.WriteLine("Standard Deviation {0}", sd);
    }
    static void RunHashTable(HashingOptions opt) {
      int[] testParams = new int[] { 50, 10, 10 };
      SetupParams(opt.Params, testParams);
      int n = testParams[0];
      int capacity = testParams[1];
      int numResults = testParams[2];
      float loadFactor = 1f;
      var ht = new StubHashTable<Guid, int>(capacity, loadFactor);
      var keys = new List<Guid>();
      for (var i =0; i < n; i++) {
        var guid = Guid.NewGuid();
        keys.Add(guid);
        ht[guid] = i;
      }
      var chainSizes = ht.Store.Select(ele => ele?.Count ?? 0).ToArray();
      Console.WriteLine("HashTable Entries for n entries: {0} capacity: {1} ", n, capacity);
      var arr = new string[keys.Count];
      for(var i = 0; i < keys.Count; i++) {
        arr[i] = string.Format("{0}: {1}", keys[i], ht[keys[i]]);
      }
      DisplayResults(arr, numResults, "\n");
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
      int[] testParams = new int[] { Int32.MaxValue/100, 10 };
      SetupParams(opt.Params, testParams);
      int numPrimes = testParams[0];
      int numResults = testParams[1];
      var arr = Numeric.GetPrimes(numPrimes);
      Console.WriteLine("N: {0:N0}. Primes Found: {1:N0}", testParams[0], arr.Length);
      DisplayResults(arr, numResults);
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
      int[] testParams = new int[] { 20 };
      SetupParams(opt.Params, testParams);
      var numResults = testParams[0];

      var arr = new int[opt.Count];
      Helper.FillRandom(arr, opt.Range);
      Console.WriteLine("Running {0}...", act.Name);
      var output = Helper.Display(arr);
      Console.WriteLine("Unsorted: {0}", output);
      var type = act.GetType();
      act.Invoke(null, new object[] { arr });
      DisplayResults(arr, numResults);
    }
    static void SetupParams(IEnumerable<int> optionParams, int[] testCaseParams) {
      var p = optionParams.ToArray();
      for (var i = 0; i < testCaseParams.Length && i < p.Length; i++) {
        testCaseParams[i] = p[i];
      }
    }
    static void DisplayResults<T>(T[] arr, int numResults, string separator = ", ") {
      string output;
      if (arr.Length - numResults < 1)  {
        output = Helper.Display(arr, separator);
        Console.WriteLine("Outputting results: {0}", output);
      }
      else {
        var arr2 = new T[numResults];
        Array.Copy(arr, arr.Length-numResults, arr2, 0, numResults);
        output = Helper.Display(arr2, separator);
        Console.WriteLine("Outputting the last {0} results: {1}", numResults, output);
      }
    }
  }
}
