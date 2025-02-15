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
| LuaCSAlloc            | 100       | 32.499 μs | 0.0648 μs | 0.0541 μs |    3 | 1.5869 | 0.6714 |   82208 B |
| MondAlloc             | 100       | 23.446 μs | 0.1239 μs | 0.1099 μs |    2 | 1.3123 | 0.1831 |   66304 B |
| MoonSharpAlloc        | 100       | 88.155 μs | 1.2514 μs | 1.1705 μs |    4 | 3.2959 | 0.9766 |  167864 B |
| LuaNETAlloc           | 100       | 22.075 μs | 0.4148 μs | 0.3880 μs |    1 |      - |      - |      56 B |
| JintAlloc             | 100       | 21.366 μs | 0.0523 μs | 0.0436 μs |    1 | 1.0376 | 0.0916 |   52704 B |
|                       |           |           |           |           |      |        |        |           |
| LuaCSCSharpToLang     | 100       | 12.544 μs | 0.0430 μs | 0.0381 μs |    3 | 0.2136 |      - |   11112 B |
| MondCSharpToLang      | 100       |  7.464 μs | 0.0135 μs | 0.0113 μs |    2 | 0.3204 |      - |   16272 B |
| MoonSharpCSharpToLang | 100       | 26.232 μs | 0.5128 μs | 0.5266 μs |    4 | 1.4648 | 0.4883 |   74968 B |
| LuaNETCSharpToLang    | 100       |  7.275 μs | 0.0219 μs | 0.0194 μs |    1 |      - |      - |         - |
| JintCSharpToLang      | 100       | 12.479 μs | 0.0630 μs | 0.0526 μs |    3 | 0.9308 |      - |   47200 B |
|                       |           |           |           |           |      |        |        |           |
| LuaCSLangToCSharp     | 100       |  9.495 μs | 0.0134 μs | 0.0112 μs |    3 | 0.0305 |      - |    1896 B |
| MondLangToCSharp      | 100       |  6.469 μs | 0.0109 μs | 0.0091 μs |    2 | 0.2060 |      - |   10664 B |
| MoonSharpLangToCSharp | 100       | 36.387 μs | 0.1711 μs | 0.1336 μs |    5 | 1.5869 | 0.5493 |   80840 B |
| LuaNETLangToCSharp    | 100       |  5.045 μs | 0.0098 μs | 0.0076 μs |    1 |      - |      - |         - |
| JintLangToCSharp      | 100       | 25.879 μs | 0.1879 μs | 0.1758 μs |    4 | 1.0071 |      - |   52056 B |
