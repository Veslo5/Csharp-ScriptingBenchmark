// See https://aka.ms/new-console-template for more information

using BenchmarkDotNet.Running;
using ScriptingBenchmark;

// var bm = new Benchmark();
// bm.LoopCount = 1000;

// Console.WriteLine(bm.MondAlloc());
// Console.WriteLine(await bm.LuaAlloc());

// Console.WriteLine(await bm.LuaOut());
// Console.WriteLine(bm.MondOut());

BenchmarkRunner.Run<Benchmark>();


Console.ReadLine();