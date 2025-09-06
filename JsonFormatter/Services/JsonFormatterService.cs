using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace JsonFormatter.Services;

public class JsonFormatterService(ILogger<JsonFormatterService> logger) : IJsonFormatterService
{
    private readonly ILogger<JsonFormatterService> _logger = logger;

    public string Format(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            return string.Empty;
        }

        // 先嘗試當作單一 JSON 解析
        if (TryFormatSingleJson(input, out var singleFormatted))
        {
            return singleFormatted!;
        }

        // 若非單一 JSON，嘗試以 JSON Lines 逐行解析並輸出為 JSON 陣列
        if (TryFormatJsonLines(input, out var jsonlFormatted))
        {
            return jsonlFormatted!;
        }

        // 兩者皆失敗則視為無效 JSON 輸入
        var ex = new JsonException("輸入不是有效的 JSON 或 JSON Lines 格式");
        _logger.LogError(ex, "輸入不是有效的 JSON 或 JSON Lines 格式");
        throw ex;
    }

    private static bool TryFormatSingleJson(string input, out string? formatted)
    {
        try
        {
            using var doc = JsonDocument.Parse(input);
            var options = new JsonSerializerOptions { WriteIndented = true };
            formatted = JsonSerializer.Serialize(doc.RootElement, options);
            return true;
        }
        catch (JsonException)
        {
            formatted = null;
            return false;
        }
    }

    private static bool TryFormatJsonLines(string input, out string? formatted)
    {
        formatted = null;

        // 以換行切分，忽略空白行
        var lines = input
            .Split(new[] { "\r\n", "\n" }, StringSplitOptions.None)
            .Select(l => l.Trim())
            .Where(l => !string.IsNullOrWhiteSpace(l))
            .ToList();

        if (lines.Count == 0)
        {
            return false;
        }

        var elements = new List<JsonElement>(lines.Count);
        foreach (var line in lines)
        {
            try
            {
                using var lineDoc = JsonDocument.Parse(line);
                elements.Add(lineDoc.RootElement.Clone());
            }
            catch (JsonException)
            {
                // 任一行不是 JSON 則判定不是 JSONL
                elements.Clear();
                return false;
            }
        }

        // 將多行 JSON 輸出為一個漂亮縮排的 JSON 陣列
        using var stream = new MemoryStream();
        using (var writer = new Utf8JsonWriter(stream, new JsonWriterOptions { Indented = true }))
        {
            writer.WriteStartArray();
            foreach (var el in elements)
            {
                el.WriteTo(writer);
            }
            writer.WriteEndArray();
        }

        formatted = Encoding.UTF8.GetString(stream.ToArray());
        return true;
    }
}
