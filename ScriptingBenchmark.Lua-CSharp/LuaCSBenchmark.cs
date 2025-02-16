using Lua;
using Lua.Standard;
using ScriptingBenchmark.Shared;

namespace ScriptingBenchmark.Lua_CSharp;

public class LuaCSBenchmark : IBenchmarkableAsync
{
    public int LoopCount { get; private set; }
    private LuaState? _luaVM;

    private string? _CSharpToLangCode;
    private string? _LangToCSharpCode;
    private string? _LangAllocCode;
    
    public LuaCSBenchmark(int loopCount)
    {
        LoopCount = loopCount;
    }
    
    public void Setup()
    {
        _CSharpToLangCode = Codes.GetLuaCSharpToLang();
        _LangToCSharpCode = Codes.GetLuaLangToCSharp(LoopCount);
        _LangAllocCode = Codes.GetLuaAlloc(LoopCount);
        
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
        LuaValue[] result = await _luaVM!.DoStringAsync(_CSharpToLangCode!);
        var func = result[0].Read<LuaFunction>();

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
        LuaValue[] result = await _luaVM!.DoStringAsync(_LangToCSharpCode!);
        var number = result[0].Read<int>();
        return number;
    }

    public async Task<string> LangAllocAsync()
    {
        LuaValue[] result = await _luaVM!.DoStringAsync(_LangAllocCode!);

        var arr = result[0].Read<LuaTable>();
        var arrItem = arr[LoopCount].Read<LuaTable>();
        return arrItem["test"].Read<string>();
    }
}