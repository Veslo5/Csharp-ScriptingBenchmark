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
    
    public JsValue PreparedCSharpToLangResult { get; private set; }
    public JsValue PreparedLangToCSharpCodeResult { get; private set; }
    public JsValue PreparedLangAllocCodeResult { get; private set; }

    
    public Engine JsVM { get; private set; }
    
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
        
        JsVM = new Engine();
        JsVM.SetValue("increment", (Func<int, int>)(number => number += 1));
        
        PreparedCSharpToLangResult = JsVM.Evaluate(PreparedCSharpToLang); 
        PreparedLangToCSharpCodeResult = JsVM.Evaluate(PreparedLangToCSharpCode);
        PreparedLangAllocCodeResult = JsVM.Evaluate(PreparedLangAllocCode);

    }
    
    public void Cleanup()
    {
        JsVM.Dispose();;
    }

    public int CSharpToLang()
    {
        var number = 0;

        for (int i = 0; i < LoopCount; i++)
        {
            JsValue funcResult = PreparedCSharpToLangResult.Call(number);
            number = (int)funcResult.AsNumber();
        }

        return number;
    }

    public int LangToCSharp()
    {
        var number =  (int)PreparedLangToCSharpCodeResult.Call().AsNumber();
        return number;
    }

    public string LangAlloc()
    {
        JsArray arr = PreparedLangAllocCodeResult.Call().AsArray();
        JsValue arrItem = arr[LoopCount - 1];
        return arrItem.Get("test").AsString();
    }

    public async Task<int> CSharpToLangAsync() => CSharpToLang();

    public async Task<int> LangToCSharpAsync() => LangToCSharp();

    public async Task<string> LangAllocAsync() => LangAlloc();
}