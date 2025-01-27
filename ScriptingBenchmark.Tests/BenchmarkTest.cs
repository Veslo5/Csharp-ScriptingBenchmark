namespace ScriptingBenchmark.Tests;

public class BenchmarkTest
{
    public Benchmark Benchmark { get; private set; }


    public BenchmarkTest()
    {
        Benchmark = new Benchmark();
        Benchmark.LoopCount = 100;
        Benchmark.Setup();
    }

    ~BenchmarkTest()
    {
        Benchmark.Cleanup();
    }


    // CSharpToLang
    [Fact]
    public async Task Test_LuaCSCSharpToLang()
    {
        var result = await Benchmark.LuaCSCSharpToLang();
        Assert.Equal(Benchmark.LoopCount, result);
    }

    [Fact]
    public void Test_MondCSharpToLang()
    {
        var result = Benchmark.MondCSharpToLang();
        Assert.Equal(Benchmark.LoopCount, result);
    }

    [Fact]
    public void Test_MoonSharpCSharpToLang()
    {
        var result = Benchmark.MoonSharpCSharpToLang();
        Assert.Equal(Benchmark.LoopCount, result);
    }
    
    [Fact]
    public void Test_LuaNETCSharpToLang()
    {
        var result = Benchmark.LuaNETCSharpToLang();
        Assert.Equal(Benchmark.LoopCount, result);
    }
    
    [Fact]
    public void Test_JintCSharpToLang()
    {
        var result = Benchmark.JintCSharpToLang();
        Assert.Equal(Benchmark.LoopCount, result);
    }

    //LangToCSharp
    
    [Fact]
    public async Task Test_LuaCSLangToCSharp()
    {
        var result = await Benchmark.LuaCSLangToCSharp();
        Assert.Equal(Benchmark.LoopCount, result);
    }

    [Fact]
    public void Test_MondLangToCSharp()
    {
        var result = Benchmark.MondLangToCSharp();
        Assert.Equal(Benchmark.LoopCount, result);
    }

    [Fact]
    public void Test_MoonSharpLangToCSharp()
    {
        var result = Benchmark.MoonSharpLangToCSharp();
        Assert.Equal(Benchmark.LoopCount, result);
    }
    
    [Fact]
    public void Test_LuaNETLangToCSharp()
    {
        var result = Benchmark.LuaNETLangToCSharp();
        Assert.Equal(Benchmark.LoopCount, result);
    }
    
    [Fact]
    public void Test_JintLangToCSharp()
    {
        var result = Benchmark.JintLangToCSharp();
        Assert.Equal(Benchmark.LoopCount, result);
    }

    //AllocTest
    [Fact]
    public async Task Test_LuaCSAlloc()
    {
        var result = await Benchmark.LuaCSAlloc();
        Assert.Equal("hello world 100", result);
    }

    [Fact]
    public void Test_MondAlloc()
    {
        var result = Benchmark.MondAlloc();
        //Mond arrays stars from 0
        Assert.Equal("hello world 99", result);
    }

    [Fact]
    public void Test_MoonSharpAlloc()
    {
        var result = Benchmark.MoonSharpAlloc();
        Assert.Equal("hello world 100", result);
    }
    
    [Fact]
    public void Test_LuaNETAlloc()
    {
        var result = Benchmark.LuaNETAlloc();
        Assert.Equal("hello world 100", result);
    }
    
    [Fact]
    public void Test_JintAlloc()
    {
        var result = Benchmark.JintAlloc();
        //JS arrays stars from 0
        Assert.Equal("hello world 99", result);
    }
}