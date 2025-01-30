using Mond;
using ScriptingBenchmark.Shared;

namespace ScriptingBenchmark.Mond;

[Obsolete("Use MondPrecompiledBenchmark instead")]
public class MondBenchmark : IBenchmarkableAsync
{
    public int LoopCount { get; private set; }

    public MondState MondVM { get; private set; }
    
    public string CSharpToLangCode { get; private set; }
    public string LangToCSharpCode { get; private set; }
    public string LangAllocCode { get; private set; }

    public MondBenchmark(int loopCount)
    {
        LoopCount = loopCount;
    }

    
    public void Setup()
    {
        CSharpToLangCode = Codes.GetMondCSharpToLang();
        LangToCSharpCode = Codes.GetMondLangToCSharp(LoopCount);
        LangAllocCode = Codes.GetMondAlloc(LoopCount);
        
        MondVM = new MondState();
        //Increment function for MondOUT
        MondVM["increment"] = MondValue.Function((_, args) => args[0] += 1);
    }

    public void Cleanup()
    {
        
    }

    public int CSharpToLang()
    {
        MondValue func = MondVM.Run(CSharpToLangCode);

        var number = 0;

        for (int i = 0; i < LoopCount; i++)
        {
            var funcResult = MondVM.Call(func, number);
            number = (int)funcResult;
        }

        return number;
    }

    public int LangToCSharp()
    {
        MondValue result = MondVM.Run(LangToCSharpCode);

        var number = (int)result;
        return number;
    }

    public string LangAlloc()
    {
        MondValue result = MondVM.Run(LangAllocCode);

        return result[LoopCount - 1]["test"];
    }

    public async Task<int> CSharpToLangAsync() => CSharpToLang();
    
    public async Task<int> LangToCSharpAsync() => LangToCSharp();

    public async Task<string> LangAllocAsync() => LangAlloc();
    
}