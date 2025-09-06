use zh-tw

---

程式碼庫說明（自動附註）

- 總覽：此倉庫目前僅包含兩個檔案，未含實際應用程式原始碼。
- 專案結構：
  - `AGENTS.md`：代理人工作說明文件；既有內容為「use zh-tw」，表示互動語言偏好為繁體中文。以下為本次自動附加的說明。
  - `a.md`：內容為「OK」，未見與建置或執行相關之設定或程式碼。
- 建置與執行：未發現建置腳本、相依套件管理檔或可執行入口點（如 `src/`、`package.json`、`pyproject.toml`、`go.mod`、`*.sln`/`*.csproj` 等）。
- 開發流程：尚未定義；建議新增 `README.md` 說明專案目標、使用方式與開發規範，並建立基本專案骨架（例如 `src/` 目錄與相依管理檔）。
- 建議後續：
  - 明確化專案類型與技術棧（Node/Go/Python/.NET 等）。
  - 新增測試與 CI 設定（如 GitHub Actions）與 lint/format 工具。
  - 補充治理文件（`CONTRIBUTING.md`、`CODE_OF_CONDUCT.md`）與授權條款（`LICENSE`）。


---

程式碼庫說明（補充 by Agent）

- 總覽：目前為極簡倉庫，僅含文字與說明檔，未包含可執行的應用程式碼或建置設定。
- 目錄與檔案：
  - `AGENTS.md`：代理人工作說明與語言偏好（繁體中文）。
  - `a.md`：內容為「OK」，無其他作用。
- 建置與執行：尚未有相依管理檔或執行入口（例如 `package.json`、`pyproject.toml`、`go.mod`、`.sln/.csproj`），因此無法直接建置或執行。
- 開發流程與規範：未定義；可先以簡單規範上路，例如：
  - 語言使用繁體中文。
  - Commit 採用 Conventional Commits（feat, fix, docs, chore, refactor, test）。
- 後續建議：
  - 明確化技術棧（Node/Go/Python/.NET 等）與專案目標。
  - 新增 `README.md`、建立 `src/`、`tests/`、`docs/` 目錄。
  - 建立相依與建置設定（如 `package.json` 或 `pyproject.toml`），加上基本測試與 CI。
  - 視需要補上 `CONTRIBUTING.md`、`CODE_OF_CONDUCT.md`、`LICENSE`。
