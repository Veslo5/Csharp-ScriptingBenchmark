using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using Lua;
using Lua.Standard;
using Mond;
using ScriptingBenchmark.Jint;
using ScriptingBenchmark.Lua_CSharp;
using ScriptingBenchmark.LuaNET;
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
    public IBenchmarkableAsync LuaNETBenchmark { get; private set; }
    public IBenchmarkableAsync JintBenchmark { get; private set; }

    [GlobalSetup]
    public void Setup()
    {
        LuaCSharpBenchmark = new LuaCSBenchmark(LoopCount);
        LuaCSharpBenchmark.Setup();

        MondBenchmark = new MondBenchmark(LoopCount);
        MondBenchmark.Setup();

        MoonSharpBenchmark = new MoonsharpBenchmark(LoopCount);
        MoonSharpBenchmark.Setup();

        LuaNETBenchmark = new LuaNETBenchmark(LoopCount);
        LuaNETBenchmark.Setup();
        
        JintBenchmark = new JintBenchmark(LoopCount);
        JintBenchmark.Setup();
    }

    [GlobalCleanup]
    public void Cleanup()
    {
        LuaCSharpBenchmark.Cleanup();
        MondBenchmark.Cleanup();
        MoonSharpBenchmark.Cleanup();
        LuaNETBenchmark.Cleanup();
        JintBenchmark.Cleanup();
    }

    // CSharp2Lang
    [Benchmark]
    public async Task<int> LuaCSCSharpToLang() => await LuaCSharpBenchmark.CSharpToLangAsync();
    
    [Benchmark]
    public int MondCSharpToLang() => MondBenchmark.CSharpToLang();
    
    [Benchmark]
    public int MoonSharpCSharpToLang() => MoonSharpBenchmark.CSharpToLang();
    
    [Benchmark]
    public int LuaNETCSharpToLang() => LuaNETBenchmark.CSharpToLang();
    
    [Benchmark]
    public int JintCSharpToLang() => JintBenchmark.CSharpToLang();
    
    // Lang2CSharp
    [Benchmark]
    public async Task<int> LuaCSLangToCSharp() => await LuaCSharpBenchmark.LangToCSharpAsync();
    
    [Benchmark]
    public int MondLangToCSharp() => MondBenchmark.LangToCSharp();
    
    [Benchmark]
    public int MoonSharpLangToCSharp() => MoonSharpBenchmark.LangToCSharp();
    
    [Benchmark]
    public int LuaNETLangToCSharp() => LuaNETBenchmark.LangToCSharp();
    
    [Benchmark]
    public int JintLangToCSharp() => JintBenchmark.LangToCSharp();
    
    // Alloc
    [Benchmark]
    public async Task<string> LuaCSAlloc() => await LuaCSharpBenchmark.LangAllocAsync();
    
    [Benchmark]
    public string MondAlloc() => MondBenchmark.LangAlloc();
    
    [Benchmark]
    public string MoonSharpAlloc() => MoonSharpBenchmark.LangAlloc();
    
    [Benchmark]
    public string LuaNETAlloc() => LuaNETBenchmark.LangAlloc();
    
    [Benchmark]
    public string JintAlloc() => JintBenchmark.LangAlloc();
}