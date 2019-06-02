# DotNetDsa
Exercises in Data Structures and Algorithms for educational purposes.

## Description - Project Structure 
The C# .NET solution consists of 3 projects:
The /src folder comprises of:
  1. DotNetDsa: The Console application which comprises the various data structures and algorithms along with test cases that can run against the various dsa related exercises.
  2. DotNetDsa.Benchmark: A project dedicated for evaluating the performance and memory benchmarks of various data structures & algorithms.
The /test folder:
  3. DotNetDsa.Tests: Holds unit tests that cover some of the data structures and algorithms under the DotNetDsa project.

### DotNetDsa.Algorithms
  * [Dynamic Programming](src/DotNetDsa/Algorithms/DynamicProgramming.cs)
  * [Hashing](src/DotNetDsa/Algorithms/Hashing.cs)
  * [Numeric](src/DotNetDsa/Algorithms/Numeric.cs)
  * [Sort](src/DotNetDsa/Algorithms/Sort.cs)

### DotNetDsa.DataStructures
  * [Hash Table](src/DotNetDsa/DataStructures/HashTable.cs)
  * [Heap](src/DotNetDsa/DataStructures/Heap.cs)

## Prerequisites
  1. .NET Core >= 2.2

## Installing
```
git clone https://github.com/john-echelon/dotnet-dsa.git
```
Navigate to the DotNetDsa console application. The instruction assumes that you are in the root of the repository.
## Building
This step is unnecessary as dotnet run will implicitly dotnet restore and build the application. However for brevity sake, to build the application run the dotnet run cmd:
```
cd src/DotNetDsa
dotnet build
```
The following instructions assumes that you are in the root of the repository.

> -c|--configuration {Debug|Release}

The following examples demonstrate how to run a program in either debug or release mode.
### Running a project in release mode
Running the help command (note the extra -- qualifier)
```
dotnet run -c release -- --help
```
Running the sort command
```
dotnet run -c release sort --all
```
### Running the project in debug mode
Running the help command (note the extra -- qualifier)
```
dotnet run -- --help
```
Running the sort command
```
dotnet run sort --all
```
### Generating the exe
Netcore apps do not normally generate an exe but rather dlls instead. They are intended to be run by the .NET Core shared run-time. 
However if you want to generate an exe you may run the donet publish command. The following will generate a debug build:
```
dotnet publish -c Debug -r win10-x64
```
For release builds:
```
dotnet publish -c Release  -r win10-x64
```
The remainder of the examples will be run in debug mode unless otherwise indicated.
### The help command
Running the help command (note the extra -- qualifier)
```
dotnet run -- --help
```

This would output the following
```
DotNetDsa 1.0.0
Copyright (C) 2019 DotNetDsa

  sort       Run Sort test cases.

  run        Run tests cases.

  help       Display more information on a specific command.

  version    Display version information.
```
## Getting Started
### The Sort Test Cases
#### Running the sort help command
```
dotnet run -- --help sort
```
This would output the following

```
DotNetDsa 1.0.0
Copyright (C) 2019 DotNetDsa

  -a, --all       (Default: false) Sort using [a]ll available sort methods.

  -t, --test      The name of the [t]est case used to process to standard output.

  -r, --range     (Default: 100) Determines the max [r]ange of the generated number value from 0-[range-value].

  -c, --count     (Default: 10) The [c]ount of numbers to generate.

  -p, --params    Additional [p]arams used for a particular test case.

  --help          Display this help screen.

  --version       Display version information.
```
### Examples
#### Run all sort test cases
```
dotnet run sort -a
dotnet run sort --all
```
#### Run particular test case
```
dotnet run sort -t insertion
dotnet run sort --test insertion
```
#### Run multiple test cases
```
dotnet run sort -t merge bubble quick heap
dotnet run sort --test merge bubble quick heap
```

#### Run sort test case with indicated max generated value
``` 
dotnet run sort -t bubble -r 2000
```

#### Run sort test case with maximum allowed generated value
``` 
dotnet run sort -t bubble -r 2000
```

#### Run sort test case with count of numbers to generate
``` 
dotnet run sort -t bubble -c 20
```

#### Run sort test case with limit on number of results to display
``` 
dotnet run sort -t bubble -p 20
```
### The General Test Case run command
#### Run the test case help command
```
dotnet run -- --help run
```
This would output the following
```
DotNetDsa 1.0.0
Copyright (C) 2019 DotNetDsa

  -t, --test      Required. The corresponding [t]est case number.

  -c, --count     (Default: 10) The [c]ount corresponds to the number operations to perform.

  -p, --params    Additional [p]arams used for a particular test case.

  --help          Display this help screen.

  --version       Display version information.
```
#### Test Case Numbers
    SieveOfEratosthenes = 0,
    UniversalHashingFamily = 1,
    HashTable = 2,
    HashTableDistribution = 3,
    KnapsackWithoutRepetitions = 4,
    EditDistance = 5,
    EditDistanceOnGeneSequence = 6,
    LongestCommonSubsequence = 7,

#### Example - Running Test Case SieveOfEratosthenes (with default test case parameters)
```
dotnet run run -t 0
```
This would output the following
```
Running test: SieveOfEratosthenes
N: 21,474,836. Primes Found: 1,358,124
Outputting the last 10 results: 21474667, 21474683, 21474707, 21474737, 21474751, 21474781, 21474797, 21474799, 21474821, 21474829
```
#### Example - Running Test Case SieveOfEratosthenes (with explicit test case parameters)
Param 0: Sets value of N. Used to find all prime numbers less than N.
Param 1: Number of results to show.
```
dotnet run -- run -t 0 -p 10000 20
```

This would output the following
```
Running test: SieveOfEratosthenes
N: 100,000. Primes Found: 9,592
Outputting the last 20 results: 99787, 99793, 99809, 99817, 99823, 99829, 99833, 99839, 99859, 99871, 99877, 99881, 99901, 99907, 99923, 99929, 99961, 99971, 99989, 99991
```
## Benchmarks
### DotNetDsa.Benchmarks
  * [Numeric Benchmarks](src/DotNetDsa.Benchmark/NumericBenchmark.cs)
  * [Sort Benchmarks](src/DotNetDsa.Benchmark/SortBenchmark.cs)
Navigate to the DotNetDsa benchmark application. The instruction assumes that you are in the root of the repository.
```
cd src/DotNetDsa.Benchmark
```
### Running Benchmarks
Run the following and then follow the indicated instructions.
```
dotnet run -c release
```
> Note: Benchmarks can only be run in release mode.
### Examples using --filter
```
dotnet run -c release --filter *SortBenchmark*
```
```
dotnet run -c release --filter *NumericBenchmark*
```
## Unit Tests
### DotNetDsa.Tests
  * [HashTable Tests](test/DotNetDsa.Tests/DataStructures/HashTableTests.cs)

### Running Unit Tests
Navigate to the DotNetDsa Tests project. The instruction assumes that you are in the root of the repository.
```
cd test/DotNetDsa.Tests
```
To execute unit tests, simply run the following:
```
dotnet test
```
