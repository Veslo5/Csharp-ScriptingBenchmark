using MoonSharp.Interpreter;
using ScriptingBenchmark.Shared;

namespace ScriptingBenchmark.Moonsharp;

public class MoonsharpBenchmark : IBenchmarkableAsync
{
    public int LoopCount { get; private set; }

    private string? _CSharpToLangCode;
    private string? _LangToCSharpCode;
    private string? _LangAllocCode;

    private Script? _luaVM;

    public MoonsharpBenchmark(int loopCount)
    {
        LoopCount = loopCount;
    }

    public void Setup()
    {
        _CSharpToLangCode = Codes.GetLuaCSharpToLang();
        _LangToCSharpCode = Codes.GetLuaLangToCSharp(LoopCount);
        _LangAllocCode = Codes.GetLuaAlloc(LoopCount);


        _luaVM = new Script();
        _luaVM.Globals["increment"] = (Func<int, int>)(number => number += 1);
    }


    public void Cleanup()
    {
        
    }

    public int CSharpToLang()
    {
        DynValue result = _luaVM!.DoString(_CSharpToLangCode);

        var number = 0;

        for (int i = 0; i < LoopCount; i++)
        {
            DynValue funcResult = result.Function.Call(DynValue.NewNumber(number));
            number = (int)funcResult.Number;
        }

        return number;
    }

    public int LangToCSharp()
    {
        DynValue result = _luaVM!.DoString(_LangToCSharpCode);
        var number = (int)result.Number;
        return number;
    }

    public string LangAlloc()
    {
        DynValue result = _luaVM!.DoString(_LangAllocCode);
        Table arr = result.Table;
        DynValue arrItem = arr.Get(LoopCount);
        return arrItem.Table.Get("test").String;
    }

    public Task<int> CSharpToLangAsync() => Task.FromResult(CSharpToLang());
    
    public Task<int> LangToCSharpAsync() => Task.FromResult(LangToCSharp());

    public Task<string> LangAllocAsync() => Task.FromResult(LangAlloc());
}