using Acornima.Ast;
using Jint;
using Jint.Native;
using ScriptingBenchmark.Shared;

namespace ScriptingBenchmark.Jint;

public class JintBenchmark : IBenchmarkableAsync
{
    public int LoopCount { get; private set; }

    public string CSharpToLangCode { get; private set; }
    public string LangToCSharpCode { get; private set; }
    public string LangAllocCode { get; private set; }

    public Prepared<Script> PreparedLangAllocCode { get; private set; }
    public Prepared<Script> PreparedLangToCSharpCode { get; private set; }
    public Prepared<Script> PreparedCSharpToLang { get; private set; }
    
    public JintBenchmark(int loopCount)
    {
        LoopCount = loopCount;
    }

    public void Setup()
    {
        CSharpToLangCode = Codes.GetJavaScriptToLang();
        LangToCSharpCode = Codes.GetJavaScriptLangToCSharp(LoopCount);
        LangAllocCode = Codes.GetJavaScriptAlloc(LoopCount);

        PreparedCSharpToLang = Engine.PrepareScript(CSharpToLangCode);
        PreparedLangToCSharpCode = Engine.PrepareScript(LangToCSharpCode);
        PreparedLangAllocCode = Engine.PrepareScript(LangAllocCode);
        
    }
    
    public void Cleanup()
    {
        
    }

    public int CSharpToLang()
    {
        using var JsVM = new Engine();
        
        JsValue result = JsVM.Evaluate(PreparedCSharpToLang);
        
        var number = 0;

        for (int i = 0; i < LoopCount; i++)
        {
            JsValue funcResult = result.Call(number);
            number = (int)funcResult.AsNumber();
        }

        return number;
    }

    public int LangToCSharp()
    {
        using var JsVM = new Engine();
        JsVM.SetValue("increment", (Func<int, int>)(number => number += 1));

        JsValue result = JsVM.Evaluate(PreparedLangToCSharpCode);
        var number = (int)result.AsNumber();
        return number;
    }

    public string LangAlloc()
    {
        using var JsVM = new Engine();

        JsValue result = JsVM.Evaluate(PreparedLangAllocCode);
        JsArray arr = result.AsArray();
        JsValue arrItem = arr[LoopCount - 1];
        return arrItem.Get("test").AsString();
    }

    public async Task<int> CSharpToLangAsync() => CSharpToLang();

    public async Task<int> LangToCSharpAsync() => LangToCSharp();

    public async Task<string> LangAllocAsync() => LangAlloc();
}