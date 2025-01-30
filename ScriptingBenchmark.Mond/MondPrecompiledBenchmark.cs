using Mond;
using ScriptingBenchmark.Shared;

namespace ScriptingBenchmark.Mond;

public class MondPrecompiledBenchmark : IBenchmarkableAsync
{
    public int LoopCount { get; private set; }

    public MondState MondVM { get; private set; }
    
    public MondProgram CSharpToLangCode { get; private set; }
    public MondProgram LangToCSharpCode { get; private set; }
    public MondProgram LangAllocCode { get; private set; }

    public MondPrecompiledBenchmark(int loopCount)
    {
        LoopCount = loopCount;
    }

    
    public void Setup()
    {
        CSharpToLangCode = MondProgram.Compile(Codes.GetMondCSharpToLang());
        LangToCSharpCode = MondProgram.Compile(Codes.GetMondLangToCSharp(LoopCount));
        LangAllocCode = MondProgram.Compile(Codes.GetMondAlloc(LoopCount));
        
        MondVM = new MondState();
        //Increment function for MondOUT
        MondVM["increment"] = MondValue.Function((_, args) => args[0] += 1);
    }

    public void Cleanup()
    {
        
    }

    public int CSharpToLang()
    {
        MondValue func = MondVM.Load(CSharpToLangCode);

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
        MondValue result = MondVM.Load(LangToCSharpCode);

        var number = (int)result;
        return number;
    }

    public string LangAlloc()
    {
        MondValue result = MondVM.Load(LangAllocCode);

        return result[LoopCount - 1]["test"];
    }

    public async Task<int> CSharpToLangAsync() => CSharpToLang();
    
    public async Task<int> LangToCSharpAsync() => LangToCSharp();

    public async Task<string> LangAllocAsync() => LangAlloc();
    
}
