# codex-demo

一個示範性質的程式碼倉庫，包含以 .NET 8 撰寫的主控台工具 JsonFormatter。JsonFormatter 從標準輸入讀取字串：
- 若為單一 JSON，輸出漂亮縮排的 JSON。
- 若為 JSON Lines（每行一個 JSON），會解析每行並輸出為一個漂亮縮排的 JSON 陣列。
- 若不是有效 JSON/JSONL，輸出錯誤訊息至標準錯誤並以非 0 代碼結束。

## 專案結構

```
codex-demo.sln                 # .NET 解決方案
AGENTS.md                      # 代理人工作說明（語言偏好：繁體中文）
create-project.md              # 最初需求/任務描述（備忘）
JsonFormatter/                 # 主要專案（.NET 8 主控台 + 測試）
  ├─ Program.cs               # 入口點，讀取 stdin、呼叫服務並處理錯誤
  ├─ Services/
  │   ├─ IJsonFormatterService.cs
  │   └─ JsonFormatterService.cs   # 核心邏輯：單一 JSON 與 JSONL 的解析與格式化
  ├─ src/
  │   └─ JsonFormatterUtil.cs  # 公用方法（Pretty JSON）
  ├─ Tests/
  │   └─ JsonFormatterTests.cs # xUnit 測試，與程式碼位於同一專案
  └─ README.md                 # 專案級說明（同倉庫內的補充說明）
```

## 快速開始

需求：
- .NET SDK 8.0 以上

建置：
```
dotnet build -c Release
```

執行（單一 JSON 範例）：
```
echo '{"a":1,"b":[2,3]}' | dotnet run --project JsonFormatter --
```

執行（JSON Lines 範例）：
```
printf '%s\n' '{"a":1}' '{"b":2}' | dotnet run --project JsonFormatter --
# 會輸出為：
# [
#   {
#     "a": 1
#   },
#   {
#     "b": 2
#   }
# ]
```

使用已建置產物執行：
```
echo '{"a":1}' | dotnet JsonFormatter/bin/Release/net8.0/JsonFormatter.dll --
```

測試：
```
dotnet test
```

## 設計與行為

- I/O 模式：自標準輸入讀取字串，將結果輸出至標準輸出；錯誤訊息走標準錯誤。
- 退出碼：成功為 `0`；非有效 JSON/JSONL 等錯誤為非 `0`（目前為 `1`）。
- 判定策略：
  - 先嘗試將輸入解析為單一 JSON；成功則以縮排格式輸出。
  - 若失敗，嘗試將輸入視為 JSON Lines（逐行解析、忽略空白行），成功則輸出為一個縮排的 JSON 陣列。
  - 兩者皆失敗則回報錯誤並以非 0 代碼結束。

關鍵檔案：
- `JsonFormatter/Program.cs`：建立主機、設定 Logging 與 DI，讀取輸入並調用服務。
- `JsonFormatter/Services/JsonFormatterService.cs`：實作格式化邏輯（單一 JSON 與 JSON Lines）。
- `JsonFormatter/Tests/JsonFormatterTests.cs`：xUnit 測試（與程式碼在同一專案中）。

## 開發指引

- 語言：文件與討論以繁體中文（zh-TW）。
- Commit：建議使用 Conventional Commits（feat, fix, docs, chore, refactor, test）。
- 程式風格：C# 12 / .NET 8；單元測試採 xUnit。
- 專案 README：`JsonFormatter/README.md` 提供同專案內撰寫測試的補充說明。

## 後續規劃建議

- 提供 CLI 參數（例如從檔案讀取、輸出壓縮/縮排切換、錯誤處理等選項）。
- 發佈為可執行檔或 dotnet 工具（`dotnet tool`）。
- 新增 CI（GitHub Actions）與程式碼品質工具（Analyzers、Formatter）。
- 視需要補充 `CONTRIBUTING.md`、`CODE_OF_CONDUCT.md` 與 `LICENSE`。

## 授權

尚未指定授權條款（License）。如需明確授權，請補上 `LICENSE` 檔案。

