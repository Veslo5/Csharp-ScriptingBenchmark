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
| Method                | LoopCount | Mean      | Error     | StdDev    | Rank | Gen0   | Gen1   | Allocated |
|---------------------- |---------- |----------:|----------:|----------:|-----:|-------:|-------:|----------:|
| LuaCSAlloc            | 100       | 32.464 μs | 0.2302 μs | 0.1922 μs |    4 | 1.5869 | 0.6714 |   82208 B |
| MondAlloc             | 100       | 23.726 μs | 0.3698 μs | 0.3278 μs |    3 | 1.3123 | 0.1831 |   66304 B |
| MoonSharpAlloc        | 100       | 86.257 μs | 0.7771 μs | 0.6489 μs |    5 | 3.2959 | 0.9766 |  167864 B |
| LuaNETAlloc           | 100       | 20.921 μs | 0.2650 μs | 0.2213 μs |    1 |      - |      - |      56 B |
| JintAlloc             | 100       | 21.646 μs | 0.1745 μs | 0.1632 μs |    2 | 1.0376 | 0.0916 |   52664 B |
|                       |           |           |           |           |      |        |        |           |
| LuaCSCSharpToLang     | 100       | 10.914 μs | 0.0292 μs | 0.0228 μs |    3 | 0.2136 |      - |   11112 B |
| MondCSharpToLang      | 100       |  7.616 μs | 0.0117 μs | 0.0091 μs |    2 | 0.3204 |      - |   16272 B |
| MoonSharpCSharpToLang | 100       | 25.583 μs | 0.0737 μs | 0.0576 μs |    5 | 1.4648 | 0.4883 |   74968 B |
| LuaNETCSharpToLang    | 100       |  7.277 μs | 0.0297 μs | 0.0248 μs |    1 |      - |      - |         - |
| JintCSharpToLang      | 100       | 11.940 μs | 0.0179 μs | 0.0140 μs |    4 | 0.8545 |      - |   43200 B |
|                       |           |           |           |           |      |        |        |           |
| LuaCSLangToCSharp     | 100       |  8.863 μs | 0.0090 μs | 0.0075 μs |    3 | 0.0305 |      - |    2112 B |
| MondLangToCSharp      | 100       |  6.011 μs | 0.0252 μs | 0.0468 μs |    2 | 0.2060 |      - |   10664 B |
| MoonSharpLangToCSharp | 100       | 34.818 μs | 0.6818 μs | 0.7578 μs |    5 | 1.5259 | 0.7324 |   77800 B |
| LuaNETLangToCSharp    | 100       |  5.167 μs | 0.0099 μs | 0.0077 μs |    1 |      - |      - |         - |
| JintLangToCSharp      | 100       | 23.606 μs | 0.2133 μs | 0.1782 μs |    4 | 0.9460 |      - |   48072 B |
