using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using Utils;
namespace Benchmark
{
  [MemoryDiagnoser]
  [Config(typeof(Config))]
  [RPlotExporter, RankColumn]
  public class SortBenchmark
  {
    private int[] arr;
    [Params(100, 1000)]
    // [Params(1000000)]
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
      Sorting.Sort.Bubble(arr);
      return arr;
    }
    [Benchmark]
    public int[] SelectionSort()
    {
      Sorting.Sort.Selection(arr);
      return arr;
    }
    [Benchmark]
    public int[] InsertionSort()
    {
      Sorting.Sort.Insertion(arr);
      return arr;
    }
    [Benchmark]
    public int[] QuickSort()
    {
      Sorting.Sort.Quick(arr);
      return arr;
    }
    [Benchmark]
    public int[] MergeSort()
    {
      Sorting.Sort.Merge(arr);
      return arr;
    }
    [Benchmark]
    public int[] HeapSort()
    {
      Sorting.Sort.Heap(arr);
      return arr;
    }
  }
  public class Config : ManualConfig
{
    public Config()
    {
      Add(Job.Core.WithGcForce(false));
      // Add(Job.Core.WithGcForce(true));
    }
}
}