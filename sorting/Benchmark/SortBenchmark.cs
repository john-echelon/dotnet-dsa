using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Utils;
namespace Benchmark
{
  [MemoryDiagnoser]
  [CoreJob]
  [RPlotExporter, RankColumn]
  public class SortBenchmark
  {
    private int[] arr;
    [Params(100, 1000, 10000, 100000)]
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
  }
}