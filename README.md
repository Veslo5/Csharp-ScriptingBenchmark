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

BenchmarkDotNet v0.14.0, Windows 11 (10.0.26100.3476)
AMD Ryzen 7 7800X3D, 1 CPU, 16 logical and 8 physical cores
.NET SDK 9.0.104
  [Host]   : .NET 9.0.3 (9.0.325.11113), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
  .NET 9.0 : .NET 9.0.3 (9.0.325.11113), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI

Job=.NET 9.0  Runtime=.NET 9.0  

```
| Method                | LoopCount | Mean      | Error     | StdDev    | Rank | Gen0   | Gen1   | Allocated |
|---------------------- |---------- |----------:|----------:|----------:|-----:|-------:|-------:|----------:|
| LuaCSAlloc            | 100       | 27.608 μs | 0.1302 μs | 0.1154 μs |    3 | 1.5869 | 0.6104 |   79936 B |
| MondAlloc             | 100       | 21.392 μs | 0.1182 μs | 0.0987 μs |    1 | 1.0376 | 0.1221 |   53264 B |
| MoonSharpAlloc        | 100       | 70.000 μs | 0.2028 μs | 0.1897 μs |    4 | 3.0518 | 0.7324 |  153544 B |
| LuaNETAlloc           | 100       | 21.101 μs | 0.2174 μs | 0.1815 μs |    1 |      - |      - |      56 B |
| JintAlloc             | 100       | 24.316 μs | 0.0336 μs | 0.0298 μs |    2 | 1.0376 | 0.0916 |   52600 B |
|                       |           |           |           |           |      |        |        |           |
| LuaCSCSharpToLang     | 100       |  9.140 μs | 0.0246 μs | 0.0230 μs |    3 | 0.1831 |      - |    9944 B |
| MondCSharpToLang      | 100       |  6.971 μs | 0.0017 μs | 0.0014 μs |    1 |      - |      - |     112 B |
| MoonSharpCSharpToLang | 100       | 13.566 μs | 0.0908 μs | 0.0849 μs |    5 | 1.2817 |      - |   65032 B |
| LuaNETCSharpToLang    | 100       |  7.412 μs | 0.0083 μs | 0.0073 μs |    2 |      - |      - |         - |
| JintCSharpToLang      | 100       | 11.498 μs | 0.0312 μs | 0.0292 μs |    4 | 0.7324 |      - |   36800 B |
|                       |           |           |           |           |      |        |        |           |
| LuaCSLangToCSharp     | 100       |  5.318 μs | 0.0020 μs | 0.0016 μs |    2 |      - |      - |     288 B |
| MondLangToCSharp      | 100       |  5.073 μs | 0.0275 μs | 0.0244 μs |    1 |      - |      - |         - |
| MoonSharpLangToCSharp | 100       | 19.966 μs | 0.3306 μs | 0.2931 μs |    3 | 1.2512 |      - |   64136 B |
| LuaNETLangToCSharp    | 100       |  5.220 μs | 0.0122 μs | 0.0108 μs |    2 |      - |      - |         - |
| JintLangToCSharp      | 100       | 25.553 μs | 0.5028 μs | 1.1246 μs |    4 | 0.8240 |      - |   41608 B |
