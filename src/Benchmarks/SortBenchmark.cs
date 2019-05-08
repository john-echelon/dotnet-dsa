using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using DotNetDsa.Algorithms;
using DotNetDsa.Utils;
namespace DotNetDsa.Benchmark
{
  [MemoryDiagnoser]
  [Config(typeof(Config))]
  [RPlotExporter, RankColumn]
  public class SortBenchmark
  {
    private int[] arr;
    [Params(100)]
    // [Params(100000, 1000000, 10000000)]
    public int N;
    [GlobalSetup]
    public void Setup()
    {
      arr = new int[N];
      Helper.FillRandom(arr, 100);
    }

    [Benchmark]
    public int[] BubbleSort()
    {
      Sort.Bubble(arr);
      return arr;
    }
    [Benchmark]
    public int[] SelectionSort()
    {
      Sort.Selection(arr);
      return arr;
    }
    [Benchmark]
    public int[] InsertionSort()
    {
      Sort.Insertion(arr);
      return arr;
    }
    [Benchmark]
    public int[] QuickSort()
    {
      Sort.Quick(arr);
      return arr;
    }
    [Benchmark]
    public int[] MergeSort()
    {
      Sort.Merge(arr);
      return arr;
    }
    [Benchmark]
    public int[] HeapSort()
    {
      Sort.Heap(arr);
      return arr;
    }
  }
  public class Config : ManualConfig
  {
    public Config()
    {
      Add(Job.Core.WithGcForce(false));
    }
  }
}