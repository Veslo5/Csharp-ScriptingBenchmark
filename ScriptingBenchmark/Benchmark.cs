using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using Lua;
using Lua.Standard;
using Mond;
using ScriptingBenchmark.Lua_CSharp;
using ScriptingBenchmark.Mond;
using ScriptingBenchmark.Moonsharp;
using ScriptingBenchmark.Shared;

namespace ScriptingBenchmark;

[MemoryDiagnoser]
[SimpleJob(RuntimeMoniker.Net90)]
public class Benchmark
{
    [Params(100)] public int LoopCount;

    public IBenchmarkableAsync LuaCSharpBenchmark { get; private set; }
    public IBenchmarkableAsync MondBenchmark { get; private set; }
    public IBenchmarkableAsync MoonSharpBenchmark { get; private set; }

    [GlobalSetup]
    public void Setup()
    {
        LuaCSharpBenchmark = new LuaCSBenchmark(LoopCount);
        LuaCSharpBenchmark.Setup();
        
        MondBenchmark = new MondBenchmark(LoopCount);
        MondBenchmark.Setup();
        
        MoonSharpBenchmark = new MoonsharpBenchmark(LoopCount);
        MoonSharpBenchmark.Setup();
    }

    // CSharp2Lang
    [Benchmark]
    public async Task<int> LuaCSCSharpToLang() => await LuaCSharpBenchmark.CSharpToLangAsync();
    
    [Benchmark]
    public int MondCSharpToLang() => MondBenchmark.CSharpToLang();
    
    [Benchmark]
    public int MoonSharpCSharpToLang() => MoonSharpBenchmark.CSharpToLang();
    
    
    // Lang2CSharp
    [Benchmark]
    public async Task<int> LuaCSLangToCSharp() => await LuaCSharpBenchmark.LangToCSharpAsync();
    
    [Benchmark]
    public int MondLangToCSharp() => MondBenchmark.LangToCSharp();
    
    [Benchmark]
    public int MoonSharpLangToCSharp() => MoonSharpBenchmark.LangToCSharp();
    

    // Alloc
    [Benchmark]
    public async Task<string> LuaCSAlloc() => await LuaCSharpBenchmark.LangAllocAsync();
    
    [Benchmark]
    public string MondAlloc() => MondBenchmark.LangAlloc();
    
    [Benchmark]
    public string MoonSharpAlloc() => MoonSharpBenchmark.LangAlloc();
}