using System;
using DotNetDsa.Utils;
using DotNetDsa.Algorithms;
using DotNetDsa.DataStructures;
using System.Collections.Generic;
using System.Linq;
using CommandLine;
using System.Reflection;
using System.Text;

namespace DotNetDsa
{
  public enum TestCaseName {
    SieveOfEratosthenes,
    UniversalHashingFamily,
    HashTable,
    HashTableDistribution,
    KnapsackWithoutRepetitions,
    EditDistance,
    EditDistanceOnGeneSequence,
    LongestCommonSubsequence,
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
    interface ICommandLineOption {
      int TestNumber { get; set; }
    }
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
    [Verb("run", HelpText = "Run tests cases.")]
    class TestCaseOptions: ICommandLineOption {
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
      return CommandLine.Parser.Default.ParseArguments<SortOptions, TestCaseOptions>(args)
      .MapResult(
        (SortOptions opts) => RunSort(opts),
        (TestCaseOptions opts) => Run(opts),
        errs => 1);
    }
    static void RunKnapsackWithoutRepetitions(TestCaseOptions opt) {
      Console.Write("Run using defaults? (Y/N):");
      var runDefaults = Console.ReadLine().Trim();

      Tuple<int,int>[] items; 
      int[] knapsackWeights;
      if(runDefaults.ToLower().Equals("y") || runDefaults == string.Empty) {
        items = new Tuple<int, int>[] { 
          new Tuple<int, int>(10, 60),
          new Tuple<int, int>(20, 100),
          new Tuple<int, int>(30, 120),
        };
        knapsackWeights = new int[]{ 10, 20, 30, 40, 50, 60 }; 
      } else {
        Console.Write("Enter num knapsack items:");
        var numItems = Convert.ToInt32(Console.ReadLine().Trim());
        items = new Tuple<int, int>[numItems];
        for (var i = 0; i < numItems; i++) {
          Console.Write("\nEnter weight and value for item {0}:", i + 1);
          var knapItem = ReadLineParseInts();
          items[i] = new Tuple<int, int>(Convert.ToInt32(knapItem[0]), Convert.ToInt32(knapItem[1]));
        }
        Console.Write("\nEnter optimal knapsack weights to test:");
        var weights = ReadLineParseInts();
        knapsackWeights = new int[weights.Length];
        for (var i = 0; i < weights.Length; i++) {
          knapsackWeights[i] = Convert.ToInt32(weights[i]);
        }
      }
      foreach(var capacity in knapsackWeights) {
        var result = DynamicProgramming.KnapsackWithoutRepetitions(capacity, items);
        Console.WriteLine("Knapsack without Repetitions, knapsack capacity {0}, max total value {1}", capacity, result[capacity, items.Length]);
        var backtrack = DynamicProgramming.KnapsackWithoutRepetitionsBacktrack(capacity, items);
        var output = Helper.Display(backtrack);
        Console.WriteLine("{0}", output);
      }
    }
    static void RunEditDistance(TestCaseOptions opt) {
      Console.Write("Run using defaults? (Y/N):");
      var runDefaults = Console.ReadLine().Trim();
      string a, b;
      a = "editing";
      b = "distance";
      if(runDefaults.ToLower().Equals("n")) {
        Console.Write("\nEnter first string:");
        a = Console.ReadLine();
        Console.Write("\nEnter second string:");
        b = Console.ReadLine();
      }
      var editDistResult = DynamicProgramming.EditDistance(a, b);
      Console.WriteLine("Editing Distance of {0}, {1}: {2}", a, b, editDistResult[a.Length, b.Length]);

      var sb = new StringBuilder();
      var backtrackResult = DynamicProgramming.EditDistanceBacktrack(editDistResult, a, b);
      foreach(var alignment in backtrackResult) {
        sb.AppendFormat("[{0}:{1}] ", alignment.Item1, alignment.Item2);
      }
      Console.WriteLine("Outputting Alignment {0}", sb.ToString());
    }
    static void RunEditDistanceOnGeneSequence(TestCaseOptions opt) {
      Console.Write("Run using defaults? (Y/N):");
      var runDefaults = Console.ReadLine().Trim();
      int count = 10, length = 15;
      if(runDefaults.ToLower().Equals("n")) {
        Console.Write("\nEnter sequence count and length:");
        var inputs = ReadLineParseInts();
        count = inputs[0];
        length = inputs[1];
      }
      var sequencePairs = GenerateNucleotideSequencePairs(count, length);
      foreach(var pair in sequencePairs) {
        var editDistResult = DynamicProgramming.EditDistance(pair.Item1, pair.Item2);
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
    static void RunLongestCommonSubsequence(TestCaseOptions opt) {
      Console.Write("Run using defaults? (Y/N):");
      var runDefaults = Console.ReadLine().Trim();
      string a, b;
      a = "editing";
      b = "distance";
      if(runDefaults.ToLower().Equals("n")) {
        Console.Write("\nEnter first string:");
        a = Console.ReadLine();
        Console.Write("\nEnter second string:");
        b = Console.ReadLine();
      }
      var result = DynamicProgramming.LongestCommonSubsequence(a, b);
      Console.WriteLine("LCS for input sequences {0}, {1}: {2}", a, b, result[a.Length, b.Length]);
    }
    static void RunUniversalHashingFamily(TestCaseOptions opt) {
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
    static void RunHashTableDistribution(TestCaseOptions opt) {
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
      Console.WriteLine("Mean: {0} Median: {1} Mode: {2}", chainLengths.Average(), Helper.Median(chainLengths), Helper.Mode(chainLengths));
      var sd = Helper.StandardDeviation(chainLengths);
      Console.WriteLine("Standard Deviation {0}", sd);
    }
    static void RunHashTable(TestCaseOptions opt) {
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
    static void RunSieveOfEratosthenes(TestCaseOptions opt) {
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
    static int Run<TOptions>(TOptions opt) where TOptions: ICommandLineOption {
      var testName = Enum.GetName(typeof(TestCaseName), opt.TestNumber);
      var methodName = "Run" + testName;
      Console.WriteLine("Running test: {0}", testName);
      Type[] parameterTypes = { typeof(TOptions) };
      MethodInfo methodInfo = typeof(Program).GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Static);
      methodInfo.Invoke(null, new object[] { opt });
      return 0;
    }
    static int[] ReadLineParseInts() {
      return Console
        .ReadLine()
        .Split(new Char[] {' '}, StringSplitOptions.RemoveEmptyEntries)
        .Select(item => int.Parse(item))
        .ToArray();
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
