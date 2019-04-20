using System;
using Utils;
using Sorting;
using Benchmark;
using BenchmarkDotNet.Running;

namespace dotnet_dsa
{
    class Program
    {
        static void Main(string[] args)
        {
            // var arr = new int[20];
            // Helper.FillRandom(arr, 100);
            // var output = Helper.Display(arr);
            // Console.WriteLine("{0}", output);
            // Console.WriteLine("Sorting...");

            // Sort.Selection(arr);
            // output = Helper.Display(arr);
            // Console.WriteLine("{0}", output);
            #if RELEASE
            RunBenchmarks();
            #else
            RunSort(Sort.Bubble);
            RunSort(Sort.Selection);
            RunSort(Sort.Insertion);
            #endif
        }
        static void RunSort(Action<int[]> act) {
            var arr = new int[20];
            Helper.FillRandom(arr, 100);
            var output = Helper.Display(arr);
            Console.WriteLine("{0}", output);
            var type = act.GetType();
            Console.WriteLine("Running {0}...", act.Method.Name);
            // Sort.Selection(arr);
            act(arr);
            output = Helper.Display(arr);
            Console.WriteLine("{0}", output);
        }
        static void RunBenchmarks() {
            var summary = BenchmarkRunner.Run<SortBenchmark>();
        }
    }
}
