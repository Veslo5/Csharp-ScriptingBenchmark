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
| LuaCSAlloc            | 100       | 32.548 μs | 0.1575 μs | 0.1315 μs |    3 | 1.5869 | 0.6714 |   82208 B |
| MondAlloc             | 100       | 22.326 μs | 0.0524 μs | 0.0437 μs |    2 | 1.3123 | 0.1831 |   66304 B |
| MoonSharpAlloc        | 100       | 85.300 μs | 0.3447 μs | 0.2691 μs |    4 | 3.2959 | 0.9766 |  167864 B |
| LuaNETAlloc           | 100       | 20.703 μs | 0.3304 μs | 0.2580 μs |    1 |      - |      - |      56 B |
| JintAlloc             | 100       | 21.044 μs | 0.0609 μs | 0.0475 μs |    1 | 1.0376 | 0.0916 |   52704 B |
|                       |           |           |           |           |      |        |        |           |
| LuaCSCSharpToLang     | 100       | 10.859 μs | 0.0357 μs | 0.0298 μs |    2 | 0.2136 |      - |   11112 B |
| MondCSharpToLang      | 100       |  7.353 μs | 0.0317 μs | 0.0296 μs |    1 | 0.3204 |      - |   16272 B |
| MoonSharpCSharpToLang | 100       | 26.017 μs | 0.3846 μs | 0.3598 μs |    4 | 1.4648 | 0.4883 |   74968 B |
| LuaNETCSharpToLang    | 100       |  7.329 μs | 0.0605 μs | 0.0472 μs |    1 |      - |      - |         - |
| JintCSharpToLang      | 100       | 13.333 μs | 0.0833 μs | 0.0738 μs |    3 | 0.9308 |      - |   47200 B |
|                       |           |           |           |           |      |        |        |           |
| LuaCSLangToCSharp     | 100       |  8.658 μs | 0.0195 μs | 0.0163 μs |    3 | 0.0305 |      - |    2112 B |
| MondLangToCSharp      | 100       |  6.044 μs | 0.0116 μs | 0.0097 μs |    2 | 0.2060 |      - |   10664 B |
| MoonSharpLangToCSharp | 100       | 34.323 μs | 0.1073 μs | 0.0837 μs |    5 | 1.5259 | 0.7324 |   77800 B |
| LuaNETLangToCSharp    | 100       |  5.098 μs | 0.0130 μs | 0.0101 μs |    1 |      - |      - |         - |
| JintLangToCSharp      | 100       | 27.350 μs | 0.4745 μs | 0.3963 μs |    4 | 1.0376 |      - |   52112 B |
