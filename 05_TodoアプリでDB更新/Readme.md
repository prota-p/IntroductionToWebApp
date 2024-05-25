# 【C#、Blazor】Webアプリ開発入門編（5）のサンプルコード
## 記事
- [こちら](https://prota-p.com/csharp_web_blazor5_todo_update/)の記事を参考にしてください。
## セットアップ手順
- このサンプル（演習05-1,05-2）には、sqliteファイル（`Data`フォルダ配下の`app.db`）が含まれていません。
- そのため、最初に以下のコマンドで`app.db`を作成します。
    ```
    $ dotnet ef database update
    ```