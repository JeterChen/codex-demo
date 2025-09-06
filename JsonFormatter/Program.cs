using JsonFormatter.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

var builder = Host.CreateApplicationBuilder(args);

// Logging: Console logger
builder.Logging.ClearProviders();
builder.Logging.AddSimpleConsole(options =>
{
    options.SingleLine = false;
    options.TimestampFormat = "HH:mm:ss ";
});

// DI: register services
builder.Services.AddSingleton<IJsonFormatterService, JsonFormatterService>();

using var host = builder.Build();

var logger = host.Services.GetRequiredService<ILoggerFactory>().CreateLogger("App");

// 依 create-project.md：從 Console 讀取字串，判斷是否為 JSON，若是則格式化後輸出
try
{
    var service = host.Services.GetRequiredService<IJsonFormatterService>();

    var input = await Console.In.ReadToEndAsync();
    if (string.IsNullOrWhiteSpace(input))
    {
        Environment.ExitCode = 0;
        return;
    }

    try
    {
        var formatted = service.Format(input);
        Console.WriteLine(formatted);
        Environment.ExitCode = 0;
    }
    catch (System.Text.Json.JsonException jex)
    {
        logger.LogError(jex, "輸入不是有效的 JSON：{Message}", jex.Message);
        Console.Error.WriteLine("輸入不是有效的 JSON。");
        Environment.ExitCode = 1;
    }
}
catch (Exception ex)
{
    logger.LogError(ex, "處理輸入時發生未預期錯誤");
    Environment.ExitCode = 1;
}
