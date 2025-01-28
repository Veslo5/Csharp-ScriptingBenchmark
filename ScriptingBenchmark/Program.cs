// See https://aka.ms/new-console-template for more information

using BenchmarkDotNet.Running;
using ScriptingBenchmark;
using ScriptingBenchmark.Jint;

// var x = new JintBenchmark(100);
// x.Setup();
//
// Console.WriteLine(x.CSharpToLang());
// Console.WriteLine(x.LangToCSharp());
// Console.WriteLine(x.LangAlloc());


// x.Cleanup();

BenchmarkRunner.Run<Benchmark>();

Console.ReadLine();