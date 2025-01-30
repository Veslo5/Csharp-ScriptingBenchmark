// See https://aka.ms/new-console-template for more information

using BenchmarkDotNet.Running;
using ScriptingBenchmark;

BenchmarkRunner.Run<Benchmark>();

Console.ReadLine();