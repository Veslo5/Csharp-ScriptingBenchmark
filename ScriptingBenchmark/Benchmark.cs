using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using Lua;
using Lua.Standard;
using Mond;

namespace ScriptingBenchmark;

[MemoryDiagnoser]
[SimpleJob(RuntimeMoniker.Net90)]
public class Benchmark
{
    [Params(10, 100, 1000)] public int LoopCount;
    
    public LuaState LuaVM { get; set; }

    public MondState MondVM { get; set; }

    public string LuaINCode { get; set; }
    public string LuaOUTCode { get; set; }
    public string LuaAllocCode { get; set; }
    
    public string MondINCode { get; set; }
    public string MondOUTCode { get; set; }
    public string MondAllocCode { get; set; }

    [GlobalSetup]
    public void Setup()
    {
        LuaINCode = Codes.GetLuaIN();
        LuaOUTCode = Codes.GetLuaOUT(LoopCount);
        LuaAllocCode = Codes.GetLuaAlloc(LoopCount);
        
        MondINCode = Codes.GetMondIN();
        MondOUTCode = Codes.GetMondOUT(LoopCount);
        MondAllocCode = Codes.GetMondAlloc(LoopCount);
        
        LuaVM = LuaState.Create();
        LuaVM.OpenTableLibrary();
        
        //Increment function for LuaOUT
        LuaVM.Environment["increment"] = new LuaFunction((context, buffer, ct) =>
        {
            var arg0 = context.GetArgument<int>(0);
            buffer.Span[0] = arg0 += 1;

            return new(1);
        });
        

        MondVM = new MondState();
        //Increment function for MondOUT
        MondVM["increment"] = MondValue.Function((_, args) => args[0] += 1);

    }

    [Benchmark]
    public async Task<int> LuaIN()
    {
        var result = await LuaVM.DoStringAsync(LuaINCode);
        var func = result[0].Read<LuaFunction>();

        var number = 0;

        for (int i = 0; i < LoopCount; i++)
        {
            var funcResult = await func.InvokeAsync(LuaVM, [new LuaValue(number)]);
            number = funcResult[0].Read<int>();
        }

        return number;
    }

    [Benchmark]
    public int MondIN()
    {
        var func = MondVM.Run(MondINCode);

        var number = 0;

        for (int i = 0; i < LoopCount; i++)
        {
            var funcResult = MondVM.Call(func, number);
            number = (int)funcResult;
        }

        return number;
    }

    [Benchmark]
    public async Task<int> LuaOut()
    {
        var result = await LuaVM.DoStringAsync(LuaOUTCode);
        var number = result[0].Read<int>();
        return number;
    }

    [Benchmark]
    public int MondOut()
    {
        var result = MondVM.Run(MondOUTCode);

        var number = (int)result;
        return number;
    }

    [Benchmark]
    public async Task<string> LuaAlloc()
    {
        var result = await LuaVM.DoStringAsync(LuaAllocCode);

        var arr = result[0].Read<LuaTable>();
        var arrItem = arr[LoopCount].Read<LuaTable>();
        return arrItem["test"].Read<string>();
    }

    [Benchmark]
    public string MondAlloc()
    {
        var result = MondVM.Run(MondAllocCode);

        return result[LoopCount - 1]["test"];
    }
}