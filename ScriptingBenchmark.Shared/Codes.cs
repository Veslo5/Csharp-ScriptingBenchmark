namespace ScriptingBenchmark;

public class Codes
{
    public static string GetLuaCSharpToLang()
    {
        return """
               local function increment(x)
                return x + 1
               end
               return increment
               """;
    }

    public static string GetMondCSharpToLang()
    {
        return """
               var increment = fun(x){
                return x + 1;
               };
               return increment;
               """;
    }

    public static string GetJavaScriptToLang()
    {
        return """
               const increment = function(x){
                 return x + 1;
               };
               return increment;
               """;
    }

    public static string GetLuaLangToCSharp(int loopCount)
    {
        return $"""
                local increment = increment
                numb = 0
                for i=1,{loopCount} do
                 numb = increment(numb) 
                end
                return numb
                """;
    }

    public static string GetMondLangToCSharp(int loopCount)
    {
        return $@"
                   const increment = global.increment;
                   var numb = 0;
                   for (var i = 1; i <= {loopCount}; i++) {{
                       numb = increment(numb);
                   }}
                   return numb;
                   ";
    }

    public static string GetJavaScriptLangToCSharp(int loopCount)
    {
        return $@"
                   return function(){{
                        const increment = increment; 
                        let numb = 0;
                        for (let i = 0; i < {loopCount}; i++) {{
                          numb = increment(numb);
                        }}
                        return numb;
                   }}
                   ";
    }

    public static string GetLuaAlloc(int loopCount)
    {
        return @$"
                    arr = {{}}
                    for i=1,{loopCount} do
                        table.insert(arr, {{test = ""hello world "" .. i}}) 
                    end
                    return arr
                   ";
    }

    public static string GetMondAlloc(int loopCount)
    {
        return @$" 
                   var arr = [];
                   for (var i = 0; i < {loopCount}; i++) {{
                    arr.add({{test: ""hello world "" + i}});    
                   }}
                   return arr;
                   ";
    }

    public static string GetJavaScriptAlloc(int loopCount)
    {
        return @$" 
                  return function(){{
                        const arr = [];
                        for (let i = 0; i < {loopCount}; i++) {{
                          arr.push({{test: ""hello world "" + i}});
                        }}
                        return arr;
                  }}
                  ";
    }
}