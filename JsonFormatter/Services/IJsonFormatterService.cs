namespace JsonFormatter.Services;

public interface IJsonFormatterService
{
    // 將輸入視為單一 JSON 字串，回傳縮排後的 JSON。
    // 若遇到無效 JSON，會擲出 JsonException。
    string Format(string input);
}
