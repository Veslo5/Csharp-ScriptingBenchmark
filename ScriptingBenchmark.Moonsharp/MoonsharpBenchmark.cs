using MoonSharp.Interpreter;
using ScriptingBenchmark.Shared;

namespace ScriptingBenchmark.Moonsharp;

public class MoonsharpBenchmark : IBenchmarkableAsync
{
    public int LoopCount { get; private set; }

    public string CSharpToLangCode { get; private set; }
    public string LangToCSharpCode { get; private set; }
    public string LangAllocCode { get; private set; }

    public Script LuaVM { get; private set; }

    public MoonsharpBenchmark(int loopCount)
    {
        LoopCount = loopCount;
    }

    public void Setup()
    {
        CSharpToLangCode = Codes.GetLuaCSharpToLang();
        LangToCSharpCode = Codes.GetLuaLangToCSharp(LoopCount);
        LangAllocCode = Codes.GetLuaAlloc(LoopCount);


        LuaVM = new Script();
        LuaVM.Globals["increment"] = (Func<int, int>)(number => number += 1);
    }


    public void Cleanup()
    {
        
    }

    public int CSharpToLang()
    {
        DynValue result = LuaVM.DoString(CSharpToLangCode);

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
        DynValue result = LuaVM.DoString(LangToCSharpCode);
        var number = (int)result.Number;
        return number;
    }

    public string LangAlloc()
    {
        DynValue result = LuaVM.DoString(LangAllocCode);
        Table arr = result.Table;
        DynValue arrItem = arr.Get(LoopCount);
        return arrItem.Table.Get("test").String;
    }

    public async Task<int> CSharpToLangAsync() => CSharpToLang();
    
    public async Task<int> LangToCSharpAsync() => LangToCSharp();

    public async Task<string> LangAllocAsync() => LangAlloc();
}