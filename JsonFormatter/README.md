# JsonFormatter（單一專案內使用 xUnit 測試）

本專案示範如何在「不建立額外測試專案」的前提下，於同一個 .NET 專案中使用 xUnit 撰寫並執行單元測試。

## 專案結構

- `JsonFormatter.csproj`：單一 .NET 專案，引用 xUnit 與 `Microsoft.NET.Test.Sdk`。
- `src/JsonFormatterUtil.cs`：範例程式碼，提供 `Pretty(string json)` 將 JSON 美化縮排。
- `Tests/JsonFormatterTests.cs`：xUnit 測試（與程式碼在同一專案中）。

> 注意：這種做法會讓測試類型與產品程式碼編譯進同一個組件。若未來需要發佈/封裝時排除測試，請再依需要調整組建設定。

## 需求

- .NET 8 SDK（或相容版本）

## 安裝與執行測試

在專案根目錄執行：

```bash
dotnet restore
dotnet test
```

`dotnet test` 會偵測本專案內的 xUnit 測試並執行，無需額外建立 `*.Tests` 專案。

## 新增更多測試

- 在 `Tests/` 目錄下新增 `*.cs` 檔，並使用 xUnit 的 `[Fact]` 或 `[Theory]` 撰寫測試。
- 測試檔會與專案一起編譯，`dotnet test` 可直接發現並執行。

## 後續建議

- 若專案規模成長、需要與產品程式碼明確分離測試，可再獨立出測試專案。
- 若需在發佈（`dotnet publish`/`dotnet pack`）時排除測試，可依需求新增條件式編譯或 `ItemGroup` 排除規則。

