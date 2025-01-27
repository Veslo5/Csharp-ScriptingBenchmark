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
- 🚧 [Jint](https://github.com/sebastienros/jint) Javascript
- ⏳ [Wren](https://github.com/stevewoolcock/WrenSharp) Wren

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
| Method                | LoopCount | Mean       | Error     | StdDev    | Gen0   | Gen1   | Allocated |
|---------------------- |---------- |-----------:|----------:|----------:|-------:|-------:|----------:|
| LuaCSCSharpToLang     | 100       |  11.090 μs | 0.0721 μs | 0.0675 μs | 0.2136 |      - |   11112 B |
| MondCSharpToLang      | 100       |  67.305 μs | 0.4685 μs | 0.4154 μs | 5.2490 | 0.6104 |  266704 B |
| MoonSharpCSharpToLang | 100       |  26.242 μs | 0.5074 μs | 0.5429 μs | 1.4648 | 0.4883 |   74968 B |
| LuaNETCSharpToLang    | 100       |   7.448 μs | 0.0192 μs | 0.0170 μs |      - |      - |         - |
| JintCSharpToLang      | 100       |  14.412 μs | 0.0791 μs | 0.0740 μs | 1.3123 | 0.0763 |   66064 B |
| LuaCSLangToCSharp     | 100       |   9.568 μs | 0.0319 μs | 0.0266 μs | 0.0305 |      - |    1896 B |
| MondLangToCSharp      | 100       |  75.393 μs | 0.3954 μs | 0.3302 μs | 5.4932 | 0.6104 |  277904 B |
| MoonSharpLangToCSharp | 100       |  36.600 μs | 0.2781 μs | 0.2322 μs | 1.5869 | 0.5493 |   80840 B |
| LuaNETLangToCSharp    | 100       |   5.846 μs | 0.2687 μs | 0.7880 μs |      - |      - |         - |
| JintLangToCSharp      | 100       |  29.099 μs | 0.1613 μs | 0.1508 μs | 1.0376 | 0.0610 |   53240 B |
| LuaCSAlloc            | 100       |  33.095 μs | 0.2635 μs | 0.2336 μs | 1.5869 | 0.6714 |   82208 B |
| MondAlloc             | 100       | 100.053 μs | 0.6818 μs | 0.6377 μs | 7.0801 | 1.5869 |  360976 B |
| MoonSharpAlloc        | 100       |  87.145 μs | 0.6267 μs | 0.5555 μs | 3.2959 | 0.9766 |  167864 B |
| LuaNETAlloc           | 100       |  21.391 μs | 0.3531 μs | 0.2949 μs |      - |      - |      56 B |
| JintAlloc             | 100       |  25.414 μs | 0.0955 μs | 0.0797 μs | 1.5564 | 0.2441 |   78864 B |
