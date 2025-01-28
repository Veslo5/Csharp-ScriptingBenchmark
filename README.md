# Benchmarks of embeddable languages for C#

Written in .NET 9

## Tests
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
| Method                | LoopCount | Mean      | Error     | StdDev    | Median    | Rank | Gen0   | Gen1   | Allocated |
|---------------------- |---------- |----------:|----------:|----------:|----------:|-----:|-------:|-------:|----------:|
| LuaCSAlloc            | 100       | 33.243 μs | 0.5183 μs | 0.4849 μs | 33.301 μs |    3 | 1.5869 | 0.6714 |   82208 B |
| MondAlloc             | 100       | 98.487 μs | 0.4832 μs | 0.4520 μs | 98.455 μs |    5 | 7.0801 | 1.5869 |  360976 B |
| MoonSharpAlloc        | 100       | 87.192 μs | 0.6413 μs | 0.5685 μs | 87.148 μs |    4 | 3.2959 | 0.9766 |  167864 B |
| LuaNETAlloc           | 100       | 21.426 μs | 0.2290 μs | 0.2030 μs | 21.483 μs |    1 |      - |      - |      56 B |
| JintAlloc             | 100       | 23.479 μs | 0.1428 μs | 0.1336 μs | 23.490 μs |    2 | 1.4343 | 0.2136 |   73120 B |
|                       |           |           |           |           |           |      |        |        |           |
| LuaCSCSharpToLang     | 100       | 11.241 μs | 0.0187 μs | 0.0175 μs | 11.243 μs |    2 | 0.2136 |      - |   11112 B |
| MondCSharpToLang      | 100       | 66.219 μs | 0.0847 μs | 0.0661 μs | 66.228 μs |    5 | 5.2490 | 0.6104 |  266704 B |
| MoonSharpCSharpToLang | 100       | 26.028 μs | 0.4940 μs | 0.5073 μs | 25.812 μs |    4 | 1.4648 | 0.4883 |   74968 B |
| LuaNETCSharpToLang    | 100       |  7.368 μs | 0.0235 μs | 0.0208 μs |  7.371 μs |    1 |      - |      - |         - |
| JintCSharpToLang      | 100       | 13.023 μs | 0.0476 μs | 0.0446 μs | 13.023 μs |    3 | 1.2360 | 0.0610 |   62312 B |
|                       |           |           |           |           |           |      |        |        |           |
| LuaCSLangToCSharp     | 100       |  9.674 μs | 0.1879 μs | 0.2164 μs |  9.591 μs |    2 | 0.0305 |      - |    1896 B |
| MondLangToCSharp      | 100       | 78.351 μs | 1.8935 μs | 5.5830 μs | 75.107 μs |    5 | 5.4932 | 0.6104 |  277904 B |
| MoonSharpLangToCSharp | 100       | 37.548 μs | 0.6114 μs | 0.5420 μs | 37.513 μs |    4 | 1.5869 | 0.5493 |   80840 B |
| LuaNETLangToCSharp    | 100       |  5.186 μs | 0.0200 μs | 0.0187 μs |  5.191 μs |    1 |      - |      - |         - |
| JintLangToCSharp      | 100       | 26.634 μs | 0.1059 μs | 0.0991 μs | 26.612 μs |    3 | 0.9460 | 0.0305 |   48144 B |
