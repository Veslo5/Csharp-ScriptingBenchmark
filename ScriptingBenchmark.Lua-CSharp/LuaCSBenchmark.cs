using Lua;
using Lua.CodeAnalysis.Compilation;
using Lua.CodeAnalysis.Syntax;
using Lua.Runtime;
using Lua.Standard;
using ScriptingBenchmark.Shared;

namespace ScriptingBenchmark.Lua_CSharp;

public class LuaCSBenchmark : IBenchmarkableAsync
{
    public int LoopCount { get; private set; }
    private LuaState? _luaVM;
    private readonly LuaValue[] _returnBuffer = new LuaValue[1024];

    private Chunk? _CSharpToLangCode;
    private Chunk? _LangToCSharpCode;
    private Chunk? _LangAllocCode;
    
    public LuaCSBenchmark(int loopCount)
    {
        LoopCount = loopCount;
    }
    
    public void Setup()
    {
        _CSharpToLangCode = Compile(Codes.GetLuaCSharpToLang());
        _LangToCSharpCode = Compile(Codes.GetLuaLangToCSharp(LoopCount));
        _LangAllocCode = Compile(Codes.GetLuaAlloc(LoopCount));
        
        _luaVM = LuaState.Create();
        _luaVM.OpenTableLibrary();
        
        //Increment function for LuaOUT
        _luaVM.Environment["increment"] = new LuaFunction((context, buffer, ct) =>
        {
            var arg0 = context.GetArgument<int>(0);
            buffer.Span[0] = arg0 += 1;

            return new ValueTask<int>(1);
        });
    }

    private static Chunk Compile(string source)
    {
        var syntaxTree = LuaSyntaxTree.Parse(source);
        return LuaCompiler.Default.Compile(syntaxTree);
    }

    public void Cleanup()
    {
    }

    public int CSharpToLang()
    {
        var task = CSharpToLangAsync();
        task.Wait();
        
        return task.Result;
    }

    public int LangToCSharp()
    {
        var task = LangToCSharpAsync();
        task.Wait();
        
        return task.Result;
    }

    public string LangAlloc()
    {
        var task = LangAllocAsync();
        task.Wait();
        
        return task.Result;
    }

    public async Task<int> CSharpToLangAsync()
    {
        var returnCount = await _luaVM!.RunAsync(_CSharpToLangCode!, _returnBuffer);
        if (returnCount != 1)
            throw new InvalidOperationException("Invalid return count");

        var func = _returnBuffer[0].Read<LuaFunction>();
        var number = 0;

        for (int i = 0; i < LoopCount; i++)
        {
            LuaValue[] funcResult = await func.InvokeAsync(_luaVM!, [new LuaValue(number)]);
            number = funcResult[0].Read<int>();
        }

        return number;
    }

    public async Task<int> LangToCSharpAsync()
    {
        var returnCount = await _luaVM!.RunAsync(_LangToCSharpCode!, _returnBuffer);
        if (returnCount != 1)
            throw new InvalidOperationException("Invalid return count");

        var number = _returnBuffer[0].Read<int>();
        return number;
    }

    public async Task<string> LangAllocAsync()
    {
        var returnCount = await _luaVM!.RunAsync(_LangAllocCode!, _returnBuffer);
        if (returnCount != 1)
            throw new InvalidOperationException("Invalid return count");

        var arr = _returnBuffer[0].Read<LuaTable>();
        var arrItem = arr[LoopCount].Read<LuaTable>();
        return arrItem["test"].Read<string>();
    }
}