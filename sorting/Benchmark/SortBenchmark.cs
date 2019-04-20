using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Utils;
namespace Benchmark {
    [CoreJob]
    [RPlotExporter, RankColumn]
    public class SortBenchmark {
        private int[] arr;
        [Params(1000, 10000)]
        public int N;
        [GlobalSetup]
        public void Setup()
        {
            arr = new int[N];
            Helper.FillRandom(arr, 100);
        }

        [Benchmark]
        public int[] BubbleSort() {
            Sorting.Sort.Bubble(arr);
            return arr;
        }
        [Benchmark]
        public int[]SelectionSort() {
            Sorting.Sort.Selection(arr);
            return arr;
        }
        [Benchmark]
        public int[]InsertionSort() {
            Sorting.Sort.Insertion(arr);
            return arr;
        }
    }
}                                                                                                                                                                           