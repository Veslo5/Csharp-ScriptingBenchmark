using Mond;
using ScriptingBenchmark.Shared;

namespace ScriptingBenchmark.Mond;

public class MondBenchmark : IBenchmarkableAsync
{
    public int LoopCount { get; private set; }

    private MondState? _mondVM;

    private MondProgram? _CSharpToLangCode;
    private MondProgram? _LangToCSharpCode;
    private MondProgram? _LangAllocCode;

    public MondBenchmark(int loopCount)
    {
        LoopCount = loopCount;
    }

    
    public void Setup()
    {
        _CSharpToLangCode = MondProgram.Compile(Codes.GetMondCSharpToLang());
        _LangToCSharpCode = MondProgram.Compile(Codes.GetMondLangToCSharp(LoopCount));
        _LangAllocCode = MondProgram.Compile(Codes.GetMondAlloc(LoopCount));
        
        _mondVM = new MondState();
        //Increment function for MondOUT
        _mondVM["increment"] = MondValue.Function((_, args) => args[0] += 1);
    }

    public void Cleanup()
    {
        
    }

    public int CSharpToLang()
    {
        MondValue func = _mondVM!.Load(_CSharpToLangCode);

        var number = 0;

        for (int i = 0; i < LoopCount; i++)
        {
            var funcResult = _mondVM!.Call(func, number);
            number = (int)funcResult;
        }

        return number;
    }

    public int LangToCSharp()
    {
        MondValue result = _mondVM!.Load(_LangToCSharpCode);

        var number = (int)result;
        return number;
    }

    public string LangAlloc()
    {
        MondValue result = _mondVM!.Load(_LangAllocCode);

        return result[LoopCount - 1]["test"];
    }

    public Task<int> CSharpToLangAsync() => Task.FromResult(CSharpToLang());
    
    public Task<int> LangToCSharpAsync() => Task.FromResult(LangToCSharp());

    public Task<string> LangAllocAsync() => Task.FromResult(LangAlloc());
    
}
