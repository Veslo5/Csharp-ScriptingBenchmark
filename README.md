# Benchmarks of embeddable languages for C#

Written in .NET 9

## Tests
- [CSharpToLang] C# to scripting language fuction call in lopp
- [LangToCSharp] Scripting language to C# function call in lopp
- [Alloc] scripting language array is filled with simple object in loop and returned to C#

## Libraries
- [Lua-CSharp](https://github.com/AnnulusGames/Lua-CSharp)
- [Mond](https://github.com/Rohansi/Mond)
- [MoonSharp](https://github.com/moonsharp-devs/moonsharp)

Uses [BenchmarkDotNet](https://github.com/dotnet/BenchmarkDotNet) for performance and memory allocations tests and xUnit for unit testing

## Result Win 11 x64
```

BenchmarkDotNet v0.14.0, Windows 11 (10.0.26100.2894)
AMD Ryzen 7 7800X3D, 1 CPU, 16 logical and 8 physical cores
.NET SDK 9.0.102
  [Host]   : .NET 9.0.1 (9.0.124.61010), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
  .NET 9.0 : .NET 9.0.1 (9.0.124.61010), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI

Job=.NET 9.0  Runtime=.NET 9.0  

```
| Method                | LoopCount | Mean      | Error     | StdDev    | Gen0   | Gen1   | Allocated |
|---------------------- |---------- |----------:|----------:|----------:|-------:|-------:|----------:|
| LuaCSCSharpToLang     | 100       | 11.384 μs | 0.1768 μs | 0.1568 μs | 0.2136 |      - |  10.85 KB |
| MondCSharpToLang      | 100       | 69.351 μs | 0.5821 μs | 0.5445 μs | 5.2490 | 0.6104 | 260.45 KB |
| MoonSharpCSharpToLang | 100       | 26.180 μs | 0.2466 μs | 0.2059 μs | 1.4648 | 0.4883 |  73.21 KB |
| LuaCSLangToCSharp     | 100       |  9.581 μs | 0.0674 μs | 0.0598 μs | 0.0305 |      - |   1.85 KB |
| MondLangToCSharp      | 100       | 74.882 μs | 0.2448 μs | 0.2290 μs | 5.4932 | 0.6104 | 271.39 KB |
| MoonSharpLangToCSharp | 100       | 36.477 μs | 0.1181 μs | 0.1047 μs | 1.5869 | 0.5493 |  78.95 KB |
| LuaCSAlloc            | 100       | 32.462 μs | 0.1698 μs | 0.1418 μs | 1.5869 | 0.6714 |  80.28 KB |
| MondAlloc             | 100       | 98.259 μs | 0.5261 μs | 0.4921 μs | 7.0801 | 1.5869 | 352.52 KB |
| MoonSharpAlloc        | 100       | 86.725 μs | 0.2118 μs | 0.1878 μs | 3.2959 | 0.9766 | 163.93 KB |
