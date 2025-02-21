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
| LuaCSAlloc            | 100       | 26.846 μs | 0.2349 μs | 0.2197 μs |    3 | 1.5869 | 0.6104 |   79936 B |
| MondAlloc             | 100       | 22.629 μs | 0.1171 μs | 0.1038 μs |    2 | 1.3123 | 0.1831 |   66304 B |
| MoonSharpAlloc        | 100       | 68.295 μs | 0.5321 μs | 0.4154 μs |    4 | 3.0518 | 0.7324 |  153544 B |
| LuaNETAlloc           | 100       | 21.299 μs | 0.2738 μs | 0.2287 μs |    1 |      - |      - |      56 B |
| JintAlloc             | 100       | 21.549 μs | 0.1448 μs | 0.1354 μs |    1 | 1.0376 | 0.0916 |   52664 B |
|                       |           |           |           |           |      |        |        |           |
| LuaCSCSharpToLang     | 100       |  9.126 μs | 0.0257 μs | 0.0214 μs |    2 | 0.1831 |      - |    9944 B |
| MondCSharpToLang      | 100       |  7.455 μs | 0.0304 μs | 0.0254 μs |    1 | 0.3204 |      - |   16272 B |
| MoonSharpCSharpToLang | 100       | 13.544 μs | 0.0922 μs | 0.0862 μs |    4 | 1.2817 |      - |   65032 B |
| LuaNETCSharpToLang    | 100       |  7.601 μs | 0.0935 μs | 0.0829 μs |    1 |      - |      - |         - |
| JintCSharpToLang      | 100       | 11.988 μs | 0.0164 μs | 0.0128 μs |    3 | 0.8545 |      - |   43200 B |
|                       |           |           |           |           |      |        |        |           |
| LuaCSLangToCSharp     | 100       |  5.453 μs | 0.0115 μs | 0.0096 μs |    2 |      - |      - |     288 B |
| MondLangToCSharp      | 100       |  6.278 μs | 0.0312 μs | 0.0292 μs |    3 | 0.2060 |      - |   10664 B |
| MoonSharpLangToCSharp | 100       | 19.395 μs | 0.0403 μs | 0.0337 μs |    4 | 1.2512 |      - |   64136 B |
| LuaNETLangToCSharp    | 100       |  5.285 μs | 0.0178 μs | 0.0317 μs |    1 |      - |      - |         - |
| JintLangToCSharp      | 100       | 25.233 μs | 0.5004 μs | 0.8632 μs |    5 | 0.9460 |      - |   48072 B |
