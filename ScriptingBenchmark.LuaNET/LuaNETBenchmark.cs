using LuaNET.Lua54;
using ScriptingBenchmark.Shared;

namespace ScriptingBenchmark.LuaNET;

public class LuaNETBenchmark : IBenchmarkableAsync
{
    public int LoopCount { get; private set; }

    private string? _CSharpToLangCode;
    private string? _LangToCSharpCode;
    private string? _LangAllocCode;

    private lua_State L;

    public LuaNETBenchmark(int loopCount)
    {
        LoopCount = loopCount;
    }

    public void Setup()
    {
        _CSharpToLangCode = Codes.GetLuaCSharpToLang();
        _LangToCSharpCode = Codes.GetLuaLangToCSharp(LoopCount);
        _LangAllocCode = Codes.GetLuaAlloc(LoopCount);

        L = Lua.luaL_newstate();
        Lua.luaL_openlibs(L);
        
        Lua.lua_pushcfunction(L, IncrementFunction);
        Lua.lua_setglobal(L, "increment");
    }

    public void Cleanup()
    {
        Lua.lua_close(L);
    }

    public int CSharpToLang()
    {
        //unfortunately I have to do it like this, because for some unknown reason during benchmarking, it throws access violation error on the Lua.lua_pushnumber(L, number) line
        //I could not reproduce this exception without benchmarkDotNet
        var L = Lua.luaL_newstate();
        
        Lua.luaL_loadstring(L, _CSharpToLangCode!);
        Lua.lua_pcall(L, 0, 1, 0);
        
        int number = 0;

        for (int i = 0; i < LoopCount; i++)
        {
            Lua.lua_pushvalue(L, -1);
            Lua.lua_pushnumber(L, number);
            Lua.lua_pcall(L, 1, 1, 0);

            number = (int)Lua.lua_tonumber(L, -1);
            Lua.lua_pop(L, 1);
        }
        
        Lua.lua_close(L);
        return number;
    }

    public int LangToCSharp()
    {
        Lua.luaL_loadstring(L, _LangToCSharpCode!);
        Lua.lua_pcall(L, 0, 1, 0); 

        var number = (int)Lua.lua_tonumber(L, -1); 
        Lua.lua_pop(L, 1); 

        return number;
    }

    public string LangAlloc()
    {
        Lua.luaL_loadstring(L, _LangAllocCode!);
        Lua.lua_pcall(L, 0, 1, 0);
        
        Lua.lua_rawgeti(L, -1, LoopCount);
        Lua.lua_getfield(L, -1, "test");
        
        var result = Lua.lua_tostring(L, -1);
        Lua.lua_pop(L, 2);
        
        return result!;
    }

    public Task<int> CSharpToLangAsync() => Task.FromResult(CSharpToLang());
    
    public Task<int> LangToCSharpAsync() => Task.FromResult(LangToCSharp());

    public Task<string> LangAllocAsync() => Task.FromResult(LangAlloc());
    
    private static int IncrementFunction(lua_State L)
    {
        double number = Lua.lua_tonumber(L, 1);
        Lua.lua_pushnumber(L, number + 1);
        return 1; // Number of return values
    }
}