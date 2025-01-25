using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using Lua;
using Mond;

namespace ScriptingBenchmark;

[MemoryDiagnoser]
[SimpleJob(RuntimeMoniker.Net90)]
public class Benchmark
{

    [Params(100_000)]
    public int LoopCount;
    
    [Benchmark]
    public async Task<int> LuaIN()
    {
        var state = LuaState.Create();

        var code = """
                   local function increment(x)
                    return x + 1
                   end
                   return increment
                   """;
        
        var result = await state.DoStringAsync(code);
        var func = result[0].Read<LuaFunction>();

        var number = 0;

        for (int i = 0; i < LoopCount; i++)
        {
            var funcResult = await func.InvokeAsync(state, [new LuaValue(number)]);
            number = funcResult[0].Read<int>();
        }

        return number;
    }

    [Benchmark]
    public int MondIN()
    {
        var state = new MondState();

        var code = """
                   var increment = fun(x){
                    return x + 1;
                   };
                   return increment;
                   """;

        var func = state.Run(code);

        var number = 0;

        for (int i = 0; i < LoopCount; i++)
        {
            var funcResult = state.Call(func, number);
            number = (int)funcResult;
        }

        return number;

    }
    
    [Benchmark]
    public async Task<int> LuaOut()
    {
        var state = LuaState.Create();

        var code = $"""
                   numb = 0
                   for i=1,{LoopCount} do
                    numb = increment(numb) 
                   end
                   return numb
                   """;

        state.Environment["increment"] = new LuaFunction((context, buffer, ct) =>
        {
            var arg0 = context.GetArgument<int>(0);
            buffer.Span[0] = arg0 += 1;

            return new(1);
        });
        
        var result = await state.DoStringAsync(code);
        var number = result[0].Read<int>();
        return number;
    }

    [Benchmark]
    public int MondOut()
    {
        var state = new MondState();

        var code = $@"
                   var numb = 0;
                   for (var i = 1; i <= {LoopCount}; i++) {{
                       numb = global.increment(numb);
                   }}
                   return numb;
                   ";

        state["increment"] = MondValue.Function((_, args) =>  args[0] += 1);
        
        var result = state.Run(code);

        var number = (int)result;
        return number;
    }

    
}