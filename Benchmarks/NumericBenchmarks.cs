using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using Algorithms;
using Utils;
namespace Benchmark
{
  [MemoryDiagnoser]
  [Config(typeof(Config))]
  [RPlotExporter, RankColumn]
  public class NumericBenchmark
  {
    private bool[] arr;
    [Params(1000, 10000, 100000, 1000000, 10000000, 100000000, 1000000000)]
    public int N;
    [GlobalSetup]
    public void Setup()
    {
      arr = new bool[N+1];
    }

    [Benchmark]
    public bool[] SieveOfEratosthenes()
    {
      Numeric.SieveOfEratosthenes(arr);
      return arr;
    }
  }
}
