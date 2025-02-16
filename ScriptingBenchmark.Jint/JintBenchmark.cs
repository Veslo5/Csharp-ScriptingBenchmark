using Acornima.Ast;
using Jint;
using Jint.Native;
using ScriptingBenchmark.Shared;

namespace ScriptingBenchmark.Jint;

public class JintBenchmark : IBenchmarkableAsync
{
    public int LoopCount { get; private set; }

    private string? _CSharpToLangCode;
    private string? _LangToCSharpCode;
    private string? _LangAllocCode;

    private Prepared<Script>? _preparedLangAllocCode;
    private Prepared<Script>? _preparedLangToCSharpCode;
    private Prepared<Script>? _preparedCSharpToLang;

    private JsValue? _preparedCSharpToLangResult;
    private JsValue? _preparedLangToCSharpCodeResult;
    private JsValue? _preparedLangAllocCodeResult;
    private Engine? _jsVM;

    public JintBenchmark(int loopCount)
    {
        LoopCount = loopCount;
    }

    public void Setup()
    {
        _CSharpToLangCode = Codes.GetJavaScriptToLang();
        _LangToCSharpCode = Codes.GetJavaScriptLangToCSharp(LoopCount);
        _LangAllocCode = Codes.GetJavaScriptAlloc(LoopCount);

        _preparedCSharpToLang = Engine.PrepareScript(_CSharpToLangCode);
        _preparedLangToCSharpCode = Engine.PrepareScript(_LangToCSharpCode);
        _preparedLangAllocCode = Engine.PrepareScript(_LangAllocCode);

        _jsVM = new Engine();
        _jsVM.SetValue("increment", (Func<int, int>)(number => number += 1));

        _preparedCSharpToLangResult = _jsVM.Evaluate(_preparedCSharpToLang.Value);
        _preparedLangToCSharpCodeResult = _jsVM.Evaluate(_preparedLangToCSharpCode.Value);
        _preparedLangAllocCodeResult = _jsVM.Evaluate(_preparedLangAllocCode.Value);
    }

    public void Cleanup()
    {
        _jsVM?.Dispose();
    }

    public int CSharpToLang()
    {
        var number = 0;

        for (int i = 0; i < LoopCount; i++)
        {
            JsValue funcResult = _preparedCSharpToLangResult!.Call(number);
            number = (int)funcResult.AsNumber();
        }

        return number;
    }

    public int LangToCSharp()
    {
        var number = (int)_preparedLangToCSharpCodeResult!.Call().AsNumber();
        return number;
    }

    public string LangAlloc()
    {
        JsArray arr = _preparedLangAllocCodeResult!.Call().AsArray();
        JsValue arrItem = arr[LoopCount - 1];
        return arrItem.Get("test").AsString();
    }

    public Task<int> CSharpToLangAsync() => Task.FromResult(CSharpToLang());

    public Task<int> LangToCSharpAsync() => Task.FromResult(LangToCSharp());

    public Task<string> LangAllocAsync() => Task.FromResult(LangAlloc());
}