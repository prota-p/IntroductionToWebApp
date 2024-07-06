# 【C#、Blazor】Webアプリ開発入門編（6）のサンプルコード
## 記事
- [こちら](https://prota-p.com/csharp_web_blazor6_todo_authentication/)の記事を参考にしてください。
## セットアップ手順
- このサンプルには、sqliteファイル（`Data`フォルダ配下の`app.db`）が含まれていません。
- そのため、最初に以下のコマンドで`app.db`を作成します。
    ```
    $ dotnet ef database update
    ```
- DB Browser for SQLiteなどを用いてでテスト用のDBレコードを追加してからアプリを実行してください。