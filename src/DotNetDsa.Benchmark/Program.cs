using System;
using DotNetDsa.Utils;
using DotNetDsa.Algorithms;
using DotNetDsa.DataStructures;
using BenchmarkDotNet.Running;
using System.Collections.Generic;
using System.Linq;
using CommandLine;

namespace DotNetDsa.Benchmark
{
  class Program
  {
     static void Main(string[] args)
    {
      BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
    }
  }
}
