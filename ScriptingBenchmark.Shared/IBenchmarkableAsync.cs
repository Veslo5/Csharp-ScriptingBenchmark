namespace ScriptingBenchmark.Shared;

public interface IBenchmarkableAsync : IBenchmarkable
{
    Task<int> CSharpToLangAsync();
    Task<int> LangToCSharpAsync();
    Task<string> LangAllocAsync();
}