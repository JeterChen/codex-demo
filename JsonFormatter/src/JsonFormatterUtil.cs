namespace JsonFormatter;

public static class JsonFormatterUtil
{
    public static string Pretty(string json)
    {
        using var doc = System.Text.Json.JsonDocument.Parse(json);
        return System.Text.Json.JsonSerializer.Serialize(doc, new System.Text.Json.JsonSerializerOptions
        {
            WriteIndented = true
        });
    }
}

