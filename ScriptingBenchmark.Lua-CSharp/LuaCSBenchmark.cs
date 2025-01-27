using Lua;
using Lua.Standard;
using ScriptingBenchmark.Shared;

namespace ScriptingBenchmark.Lua_CSharp;

public class LuaCSBenchmark : IBenchmarkableAsync
{
    public int LoopCount { get; private set; }
    public LuaState LuaVM { get; private set; }
    
    public string CSharpToLangCode { get; private set; }
    public string LangToCSharpCode { get; private set; }
    public string LangAllocCode { get; private set; }
    
    public LuaCSBenchmark(int loopCount)
    {
        LoopCount = loopCount;
    }
    
    public void Setup()
    {
        CSharpToLangCode = Codes.GetLuaCSharpToLang();
        LangToCSharpCode = Codes.GetLuaLangToCSharp(LoopCount);
        LangAllocCode = Codes.GetLuaAlloc(LoopCount);
        
        LuaVM = LuaState.Create();
        LuaVM.OpenTableLibrary();
        
        //Increment function for LuaOUT
        LuaVM.Environment["increment"] = new LuaFunction((context, buffer, ct) =>
        {
            var arg0 = context.GetArgument<int>(0);
            buffer.Span[0] = arg0 += 1;

            return new ValueTask<int>(1);
        });
    }

    public void Cleanup()
    {
        throw new NotImplementedException();
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
        LuaValue[] result = await LuaVM.DoStringAsync(CSharpToLangCode);
        var func = result[0].Read<LuaFunction>();

        var number = 0;

        for (int i = 0; i < LoopCount; i++)
        {
            LuaValue[] funcResult = await func.InvokeAsync(LuaVM, [new LuaValue(number)]);
            number = funcResult[0].Read<int>();
        }

        return number;
    }

    public async Task<int> LangToCSharpAsync()
    {
        LuaValue[] result = await LuaVM.DoStringAsync(LangToCSharpCode);
        var number = result[0].Read<int>();
        return number;
    }

    public async Task<string> LangAllocAsync()
    {
        LuaValue[] result = await LuaVM.DoStringAsync(LangAllocCode);

        var arr = result[0].Read<LuaTable>();
        var arrItem = arr[LoopCount].Read<LuaTable>();
        return arrItem["test"].Read<string>();
    }
}