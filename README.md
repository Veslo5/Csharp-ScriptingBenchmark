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
| Method                | LoopCount | Mean      | Error     | StdDev    | Gen0   | Gen1   | Allocated |
|---------------------- |---------- |----------:|----------:|----------:|-------:|-------:|----------:|
| LuaCSCSharpToLang     | 100       | 11.254 μs | 0.0200 μs | 0.0177 μs | 0.2136 |      - |   11112 B |
| MondCSharpToLang      | 100       | 67.380 μs | 0.2649 μs | 0.2477 μs | 5.2490 | 0.6104 |  266704 B |
| MoonSharpCSharpToLang | 100       | 25.999 μs | 0.2749 μs | 0.2146 μs | 1.4648 | 0.4883 |   74968 B |
| LuaNETCSharpToLang    | 100       |  7.744 μs | 0.0153 μs | 0.0143 μs |      - |      - |         - |
| LuaCSLangToCSharp     | 100       |  9.453 μs | 0.0142 μs | 0.0133 μs | 0.0305 |      - |    1896 B |
| MondLangToCSharp      | 100       | 74.899 μs | 0.2730 μs | 0.2554 μs | 5.4932 | 0.6104 |  277904 B |
| MoonSharpLangToCSharp | 100       | 36.618 μs | 0.1020 μs | 0.0852 μs | 1.5869 | 0.5493 |   80840 B |
| LuaNETLangToCSharp    | 100       |  5.029 μs | 0.0067 μs | 0.0060 μs |      - |      - |         - |
| LuaCSAlloc            | 100       | 32.178 μs | 0.1017 μs | 0.0794 μs | 1.5869 | 0.6714 |   82208 B |
| MondAlloc             | 100       | 97.960 μs | 0.2427 μs | 0.2027 μs | 7.0801 | 1.5869 |  360976 B |
| MoonSharpAlloc        | 100       | 89.048 μs | 0.8729 μs | 0.7738 μs | 3.2959 | 0.9766 |  167864 B |
| LuaNETAlloc           | 100       | 21.477 μs | 0.3379 μs | 0.2996 μs |      - |      - |      56 B |
