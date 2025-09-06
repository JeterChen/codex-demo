using JsonFormatter;
using Xunit;

public class JsonFormatterTests
{
    [Fact]
    public void Pretty_FormatsMinifiedJson()
    {
        var input = "{\"a\":1,\"b\":[1,2]}";
        var output = JsonFormatterUtil.Pretty(input);

        Assert.Contains("\n", output);
        Assert.Contains("\"a\": 1", output);
        Assert.Contains("\"b\": [", output);
    }

    [Fact]
    public void Pretty_ThrowsOnInvalidJson()
    {
        Assert.Throws<System.Text.Json.JsonException>(() => JsonFormatterUtil.Pretty("{invalid}"));
    }
}

