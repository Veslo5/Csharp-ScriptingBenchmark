namespace ScriptingBenchmark.Shared;

public interface IBenchmarkable
{
    void Setup();
    void Cleanup();
    int CSharpToLang();
    int LangToCSharp();
    string LangAlloc();
}