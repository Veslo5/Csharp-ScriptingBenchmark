# Benchmarks of embeddable languages for C#

Written in .NET 9

## Benchmarks focused on runtime performance
- [CSharpToLang] C# to scripting language fuction call in loop
- [LangToCSharp] Scripting language to C# function call in loop
- [Alloc] scripting language array is filled with simple object in loop and returned to C#

## Scripting languages
- ✔️ [Lua-CSharp](https://github.com/AnnulusGames/Lua-CSharp) Lua 5.2
- ✔️ [Mond](https://github.com/Rohansi/Mond) Mond
- ✔️ [MoonSharp](https://github.com/moonsharp-devs/moonsharp) Lua 5.2
- ✔️ [Lua.NET](https://github.com/tilkinsc/Lua.NET) Lua 5.4
- ✔️ [Jint](https://github.com/sebastienros/jint) Javascript
- ⏳ [Wren](https://github.com/stevewoolcock/WrenSharp) Wren
- ⏳ [NLua](https://github.com/NLua/NLua) Lua 5.2

✔️ Implemented | 🚧 WIP | ⏳ Planned

## Other libraries 
- [BenchmarkDotNet](https://github.com/dotnet/BenchmarkDotNet) for performance and memory allocations tests 
- xUnit for unit testing

## Result Win 11 x64
```

BenchmarkDotNet v0.14.0, Windows 11 (10.0.26100.2894)
AMD Ryzen 7 7800X3D, 1 CPU, 16 logical and 8 physical cores
.NET SDK 9.0.102
  [Host]   : .NET 9.0.1 (9.0.124.61010), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
  .NET 9.0 : .NET 9.0.1 (9.0.124.61010), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI

Job=.NET 9.0  Runtime=.NET 9.0  

```
| Method                      | LoopCount | Mean      | Error     | StdDev    | Rank | Gen0   | Gen1   | Allocated |
|---------------------------- |---------- |----------:|----------:|----------:|-----:|-------:|-------:|----------:|
| LuaCSAlloc                  | 100       | 33.332 μs | 0.2515 μs | 0.2352 μs |    3 | 1.5869 | 0.6714 |   82208 B |
| MondPrecompiledAlloc        | 100       | 31.501 μs | 0.1704 μs | 0.1594 μs |    2 | 1.8311 | 0.2441 |   93584 B |
| MoonSharpAlloc              | 100       | 87.750 μs | 0.6503 μs | 0.5765 μs |    4 | 3.2959 | 0.9766 |  167864 B |
| LuaNETAlloc                 | 100       | 21.026 μs | 0.2461 μs | 0.2182 μs |    1 |      - |      - |      56 B |
| JintAlloc                   | 100       | 21.489 μs | 0.0181 μs | 0.0152 μs |    1 | 1.0376 | 0.0916 |   52704 B |
|                             |           |           |           |           |      |        |        |           |
| LuaCSCSharpToLang           | 100       | 11.109 μs | 0.0254 μs | 0.0237 μs |    3 | 0.2136 |      - |   11112 B |
| MondPrecompiledCSharpToLang | 100       |  6.805 μs | 0.0058 μs | 0.0051 μs |    1 | 0.4654 |      - |   23512 B |
| MoonSharpCSharpToLang       | 100       | 26.376 μs | 0.4529 μs | 0.4015 μs |    5 | 1.4648 | 0.4883 |   74968 B |
| LuaNETCSharpToLang          | 100       |  7.315 μs | 0.0060 μs | 0.0050 μs |    2 |      - |      - |         - |
| JintCSharpToLang            | 100       | 12.820 μs | 0.2553 μs | 0.3494 μs |    4 | 0.9308 |      - |   47200 B |
|                             |           |           |           |           |      |        |        |           |
| LuaCSLangToCSharp           | 100       |  9.560 μs | 0.0147 μs | 0.0138 μs |    2 | 0.0305 |      - |    1896 B |
| MondPrecompiledLangToCSharp | 100       | 10.014 μs | 0.0349 μs | 0.0310 μs |    3 | 0.3052 |      - |   15472 B |
| MoonSharpLangToCSharp       | 100       | 38.061 μs | 0.7301 μs | 0.9234 μs |    5 | 1.5869 | 0.5493 |   80840 B |
| LuaNETLangToCSharp          | 100       |  5.029 μs | 0.0173 μs | 0.0162 μs |    1 |      - |      - |         - |
| JintLangToCSharp            | 100       | 26.109 μs | 0.0946 μs | 0.0885 μs |    4 | 1.0071 |      - |   52056 B |
