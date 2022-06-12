using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ApiClientTests.Extentions;
public static class OutputExtention
{
    private static readonly JsonSerializerOptions jOptions = new JsonSerializerOptions
    {
        Converters = { new JsonStringEnumConverter() },
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        WriteIndented = true,
    };
    public static void WriteJson(this ITestOutputHelper helper, object? obj)
    {
        string s = System.Text.Json.JsonSerializer.Serialize(obj, jOptions);
        helper.WriteLine(s);
    }
}
